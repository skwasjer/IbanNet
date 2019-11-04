using System;
using System.Collections.Generic;
using System.Text;

namespace IbanNet.CheckDigits.Calculators
{
	internal class Mod97From98CheckDigitsCalculator : Mod97CheckDigitsCalculator
	{
		protected override int Calculate(string digits)
		{
			return 98 - base.Calculate(digits);
		}
	}
}
