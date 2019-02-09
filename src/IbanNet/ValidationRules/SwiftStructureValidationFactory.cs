using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using IbanNet.Registry;

namespace IbanNet.ValidationRules
{
	/// <summary>
	/// A factory to create validators that are based on the Swift Registry its structure definitions.
	/// </summary>
	internal class SwiftStructureValidationFactory : IStructureValidationFactory
	{
		private class Markers : IEnumerable<char>
		{
			public const char Digits = 'n';
			public const char UppercaseLetters = 'a';
			public const char Alphanumeric = 'c';
			public const char Space = 'e';

			public IEnumerator<char> GetEnumerator()
			{
				yield return Digits;
				yield return UppercaseLetters;
				yield return Alphanumeric;
				yield return Space;
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}
		}

		/// <inheritdoc />
		public IStructureValidator CreateValidator(CountryInfo countryInfo, string structure)
		{
			return new RegexValidator(new Regex(
				BuildStructureRegexPattern(structure),
				RegexOptions.CultureInvariant
			));
		}

		private string BuildStructureRegexPattern(string structure)
		{
			IEnumerable<string> pattern = structure
				.Substring(2)
				.PartitionOn(new Markers().ToArray())
				.Select(GetTestPart);
			return $"^{structure.Substring(0, 2)}{string.Join("", pattern)}$";
		}

		/// <remarks>
		/// https://www.swift.com/standards/data-standards/iban
		/// length
		/// ! = fixed
		/// marker
		/// </remarks>
		private string GetTestPart(string pattern)
		{
			string testPattern;
			char marker = pattern[pattern.Length - 1];
			switch (marker)
			{
				case Markers.Digits:
					testPattern = "0-9";
					break;
				case Markers.UppercaseLetters:
					testPattern = "A-Z";
					break;
				case Markers.Alphanumeric:
					testPattern = "A-Za-z0-9";
					break;
				case Markers.Space:
					testPattern = " ";
					break;
				default:
					throw new ArgumentException($"The pattern {pattern} is invalid.", nameof(pattern));
			}

			string lengthDescriptor = pattern.Substring(0, pattern.Length - 1);
			bool isFixedLength = lengthDescriptor.EndsWith("!");
			int occurrences = int.Parse(
				lengthDescriptor.Substring(0, lengthDescriptor.Length - Convert.ToByte(isFixedLength))
			);

			return $"([{testPattern}]{{{occurrences}}})";
		}
	}
}