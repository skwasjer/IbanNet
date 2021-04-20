using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IbanNet.Registry;
using IbanNet.Validation.Results;
using IbanNet.Validation.Rules;
using Xunit;

namespace IbanNet.Localisation
{

    public class GermanLanguageTest
    {
        [Fact]
        public void GermanLanguageTestInvalidLenght()
        {
            System.Globalization.CultureInfo before = System.Threading.Thread.CurrentThread.CurrentCulture;
            try
            {
                System.Threading.Thread.CurrentThread.CurrentCulture =
                    new System.Globalization.CultureInfo("de");
                
                IbanNet.Resources.Culture = new CultureInfo("en-us");

                IsValidLengthRule r = new IsValidLengthRule();
                var result = r.Validate(new ValidationRuleContext("AT1234")
                {
                    Country = new IbanCountry("AT")
                });

                var resultTyped = result as InvalidLengthResult;
                if (resultTyped != null)
                {
                    Assert.Equal("Der IBAN hat eine falsche Länge.", resultTyped.ErrorMessage);
                }
            }

            finally
            {
                IbanNet.Resources.Culture = null;
                System.Threading.Thread.CurrentThread.CurrentUICulture = before;
            }
        }

        [Fact]
        public void EnglishLanguageTestInvalidLenght()
        {
            System.Globalization.CultureInfo before = System.Threading.Thread.CurrentThread.CurrentCulture;
            try
            {
                System.Threading.Thread.CurrentThread.CurrentCulture =
                    new System.Globalization.CultureInfo("en-us");

                IbanNet.Resources.Culture = new CultureInfo("en-us");

                Console.WriteLine(IbanNet.Resources.Culture);

                IsValidLengthRule r = new IsValidLengthRule();
                var result = r.Validate(new ValidationRuleContext("AT1234")
                {
                    Country = new IbanCountry("AT")
                });

                IbanNet.Resources.Culture = null;

                var resultTyped = result as InvalidLengthResult;
                if (resultTyped != null)
                {
                    Assert.Equal("The IBAN has an incorrect length.", resultTyped.ErrorMessage);
                }
            }

            finally
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = before;
                IbanNet.Resources.Culture = null;
            }
        }
    }
}
