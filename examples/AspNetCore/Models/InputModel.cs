#if DEBUG_FLUENTVALIDATION
using FluentValidation;
using IbanNet.FluentValidation;
#else
using IbanNet.DataAnnotations;
using System.ComponentModel.DataAnnotations;
#endif
using IbanNet;

namespace AspNetCoreExample.Models
{
	public class InputModel
	{
#if !DEBUG_FLUENTVALIDATION
        [Required]
        [Iban(Strict = false)]
#endif
        public string BankAccountNumber { get; set; } = default!;
    }

#if DEBUG_FLUENTVALIDATION
	public class InputModelValidator : AbstractValidator<InputModel>
    {
	    public InputModelValidator(IIbanValidator ibanValidator)
	    {
		    RuleFor(x => x.BankAccountNumber)
			    .NotNull()
			    .NotEmpty()
			    .Iban(ibanValidator, strict: false);
	    }
    }
#endif
}
