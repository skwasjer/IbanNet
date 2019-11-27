using System.Diagnostics.CodeAnalysis;

namespace IbanNet.Extensions
{
	internal static class StringExtensions
	{
		/// <summary>
		/// Removes all whitespace in the string or returns null if the value is null.
		/// </summary>
		internal static string? StripWhitespaceOrNull([NotNullIfNotNull("value")] this string? value)
		{
			if (value == null)
			{
				return null;
			}

			var buffer = new char[value.Length];
			int pos = 0;
			// ReSharper disable once ForCanBeConvertedToForeach - justification : performance
			for (int i = 0; i < value.Length; i++)
			{
				char c = value[i];
				if (!c.IsWhitespace())
				{
					buffer[pos++] = c;
				}
			}

			return new string(buffer, 0, pos);
		}
	}
}
