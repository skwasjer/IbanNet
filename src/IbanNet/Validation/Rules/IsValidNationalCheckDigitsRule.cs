using IbanNet.Validation.NationalCheckDigits;

namespace IbanNet.Validation.Rules
{
    /// <summary>
	/// Checks that the national check digits are correct
	/// </summary>
	internal class IsValidNationalCheckDigitsRule : IIbanValidationRule
    {
	    private readonly INationalCheckDigitsValidationFactory _nationalCheckDigitsValidationFactory;

	    public IsValidNationalCheckDigitsRule(INationalCheckDigitsValidationFactory nationalCheckDigitsValidationFactory)
	    {
		    _nationalCheckDigitsValidationFactory = nationalCheckDigitsValidationFactory;
	    }

	    public void Validate(ValidationContext context)
	    {
		    INationalCheckDigitsValidator validator =
			    _nationalCheckDigitsValidationFactory.CreateValidator(context.Country.TwoLetterISORegionName);
			
		    if (!validator.Validate(context.Value))
			{
				context.Result = IbanValidationResult.InvalidNationalCheckDigits;
			}
		}
	}
}
