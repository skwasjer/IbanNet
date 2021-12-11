#if DEBUG_FLUENTVALIDATION
using FluentValidation;
using IbanNet.FluentValidation;
#else
using IbanNet.DataAnnotations;
using System.ComponentModel.DataAnnotations;
#endif
using IbanNet;

namespace ExampleWebApplication.Models
{
	public class InputModel
	{
#if !DEBUG_FLUENTVALIDATION
		[Required]
		[Iban]
#endif
		public string BankAccountNumber { get; set; }
	}

#if DEBUG_FLUENTVALIDATION
	public class InputModelValidator : AbstractValidator<InputModel>
    {
	    public InputModelValidator(IIbanValidator ibanValidator)
	    {
		    RuleFor(x => x.BankAccountNumber)
			    .NotNull()
			    .NotEmpty()
			    .Iban(ibanValidator);
	    }
    }
#endif
}
