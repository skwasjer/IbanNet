namespace IbanNet
{
	/// <summary>
	/// Describes how an IBAN for a given country is defined.
	/// </summary>
    internal sealed class IbanDefinition
    {
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
