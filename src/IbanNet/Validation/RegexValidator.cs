using System;
using System.Text.RegularExpressions;

namespace IbanNet.Validation
{
	/// <summary>
	/// A validator that uses a regex to validate IBAN's.
	/// </summary>
	internal class RegexValidator : IStructureValidator
	{
		private readonly Regex _regex;

		internal RegexValidator(Regex regex)
		{
			_regex = regex ?? throw new ArgumentNullException(nameof(regex));
		}

		/// <inheritdoc />
		public bool Validate(string iban)
		{
			return _regex.IsMatch(iban);
		}
	}
}