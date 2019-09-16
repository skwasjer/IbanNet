namespace IbanNet.CheckDigits.Calculators
{
	/// <summary>
	/// Computes the expected national check digits using MOD-11.
	/// </summary>
	/// <remarks>
	/// https://no.wikipedia.org/wiki/MOD11
	/// </remarks>
	internal class Mod11CheckDigitsCalculator : CheckDigitsCalculator
	{
		protected override string ConvertFrom(string input)
		{
			int sum = 0;
			int pos = 0;
			for (int i = input.Length - 1; i >= 0; i--)
			{
				char c = input[i];
				int weight = pos++ % 6 + 2;
				sum += (char.ToUpperInvariant(c) - CharCode0) * weight;
			}

			return sum.ToString();
		}

		protected override int Calculate(string digits)
		{
			return 11 - int.Parse(digits) % 11;
		}
	}
}