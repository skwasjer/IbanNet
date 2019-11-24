using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using IbanNet.Extensions;

namespace IbanNet.Validation
{
	/// <summary>
	/// A factory to create validators that are based on the Swift Registry its structure definitions.
	/// </summary>
	internal class SwiftStructureValidationFactory : IStructureValidationFactory
	{
		private static readonly IDictionary<char, string> RegexMap = new Dictionary<char, string>
		{
			{ 'n', "[0-9]" },
			{ 'a', "[A-Z]" },
			{ 'c', "[A-Za-z0-9]" },
			{ 'e', "[ ]" }
		};

		/// <inheritdoc />
		// ReSharper disable once InconsistentNaming
		public IStructureValidator CreateValidator(string twoLetterISORegionName, string structure)
		{
			return new RegexValidator(new Regex(
				BuildStructureRegexPattern(structure),
				RegexOptions.CultureInvariant
				 | RegexOptions.ExplicitCapture
#if !NETSTANDARD1_2
				 | RegexOptions.Compiled
#endif
			));
		}

		private string BuildStructureRegexPattern(string structure)
		{
			IEnumerable<string> structureSegments = structure
				.Substring(2)
				.PartitionOn(RegexMap.Keys.ToArray())
				.Select(GetRegexPattern);
			return $"^{structure.Substring(0, 2)}{string.Join("", structureSegments)}$";
		}

		/// <remarks>
		/// https://www.swift.com/standards/data-standards/iban
		/// length
		/// ! = fixed
		/// marker
		/// </remarks>
		private string GetRegexPattern(string structureSegment)
		{
			char marker = structureSegment[structureSegment.Length - 1];
			if (!RegexMap.TryGetValue(marker, out string regexPattern))
			{
				throw new ArgumentException($"The structure segment '{structureSegment}' is invalid.", nameof(structureSegment));
			}

			string lengthDescriptor = structureSegment.Substring(0, structureSegment.Length - 1);
			bool isFixedLength = lengthDescriptor.EndsWith("!");
			int occurrences = int.Parse(
				lengthDescriptor.Substring(0, lengthDescriptor.Length - Convert.ToByte(isFixedLength))
			);

			return $"({regexPattern}{{{occurrences}}})";
		}
	}
}
