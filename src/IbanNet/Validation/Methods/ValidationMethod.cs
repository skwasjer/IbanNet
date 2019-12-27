using System.Collections.Generic;
using IbanNet.Registry;
using IbanNet.Validation.Rules;

namespace IbanNet.Validation.Methods
{
	/// <summary>
	/// The validation method to use.
	/// </summary>
	public abstract class ValidationMethod
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ValidationMethod"/> class.
		/// </summary>
		protected internal ValidationMethod()
		{
		}

		/// <summary>
		/// Gets the validation rules to execute.
		/// </summary>
		internal abstract IEnumerable<IIbanValidationRule> GetRules(IReadOnlyDictionary<string, CountryInfo> ibanRegistry);
	}
}
