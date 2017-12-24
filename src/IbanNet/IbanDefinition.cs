using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace IbanNet
{
	/// <summary>
	/// Describes how an IBAN for a given country is defined.
	/// </summary>
    internal sealed class IbanDefinition
	{
		private static readonly Regex IsValidStructure = new Regex(@"^([ABCFLUW]\d{2})+$", RegexOptions.CultureInvariant | RegexOptions.Singleline);
		private Regex _structureTest;

		/// <summary>
		/// Gets or sets the country code.
		/// </summary>
		public string CountryCode { get; set; }

		/// <summary>
		/// Gets or sets the IBAN character length.
		/// </summary>
		public int Length { get; set; }

		/// <summary>
		/// Gets or sets the structure of the IBAN.
		/// </summary>
		/// <remarks>
		/// See http://www.tbg5-finance.org/checkiban.js for all structures.
		/// </remarks>
		public string Structure { get; set; }

		/// <summary>
		/// Gets or sets the IBAN example, for verification purposes.
		/// </summary>
		public string Example { get; set; }

		/// <summary>
		/// Gets a regex that can be used to test if an IBAN value has the correct structure.
		/// </summary>
		public Regex StructureTest => _structureTest ?? (
				_structureTest = new Regex(
					BuildStructureRegexPattern("B04" + Structure), 
					RegexOptions.CultureInvariant
				)
			);

		private string BuildStructureRegexPattern(string structure)
		{
			var pattern = structure
				.Partition(3)
				.Select(p => GetTestPart(string.Join("", p.ToList())));
			return $"^{string.Join("", pattern)}$";
		}

		private string GetTestPart(string pattern)
		{
			string testpattern;
			switch (pattern[0])
			{
				case 'A': testpattern = "0-9A-Za-z"; break;
				case 'B': testpattern = "0-9A-Z"; break;
				case 'C': testpattern = "A-Za-z"; break;
				case 'F': testpattern = "0-9"; break;
				case 'L': testpattern = "a-z"; break;
				case 'U': testpattern = "A-Z"; break;
				case 'W': testpattern = "0-9a-z"; break;
				default:
					throw new ArgumentException($"The pattern {pattern} is invalid.", nameof(pattern));
			}

			var occurrences = int.Parse(pattern.Substring(1, 2));
			var regexPattern = $"([{testpattern}]{{{occurrences}}})";
			return regexPattern;
		}

		public bool Validate()
		{
			// Must have a country code.
			// Must have a length > 0.
			// The structure must be a multiple of 3 characters.
			// Must have an example with same length as defined in length property.
			// The structure must not contain invalid characters.
			// The example should pass the structure test.
			return CountryCode?.Length == 2
				&& Length > 0
				&& Structure?.Length % 3 == 0
				&& Example?.Length == Length
				&& IsValidStructure.IsMatch(Structure)
				&& StructureTest.IsMatch(Example);
		}

		/// <summary>Determines whether the specified object is equal to the current object.</summary>
		/// <param name="other">The <see cref="IbanDefinition"/> to compare with the current object. </param>
		/// <returns>true if the specified object  is equal to the current object; otherwise, false.</returns>
		private bool Equals(IbanDefinition other)
		{
			return string.Equals(CountryCode, other.CountryCode) && Length == other.Length && string.Equals(Structure, other.Structure) && string.Equals(Example, other.Example);
		}

		/// <summary>Determines whether the specified object is equal to the current object.</summary>
		/// <param name="obj">The object to compare with the current object. </param>
		/// <returns>true if the specified object  is equal to the current object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != GetType()) return false;
			return Equals((IbanDefinition) obj);
		}

		/// <summary>Serves as the default hash function. </summary>
		/// <returns>A hash code for the current object.</returns>
		public override int GetHashCode()
		{
			unchecked
			{
				// ReSharper disable NonReadonlyMemberInGetHashCode
				var hashCode = (CountryCode != null ? CountryCode.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ Length;
				hashCode = (hashCode * 397) ^ (Structure != null ? Structure.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (Example != null ? Example.GetHashCode() : 0);
				// ReSharper restore NonReadonlyMemberInGetHashCode
				return hashCode;
			}
		}
	}
}
