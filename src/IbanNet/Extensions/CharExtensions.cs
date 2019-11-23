using System.Diagnostics;

namespace IbanNet.Extensions
{
	/// <summary>
	/// Taken from MS source for <see cref="char"/>.
	/// </summary>
	[DebuggerNonUserCode]
	internal static class CharExtensions
	{
		public static bool IsInRange(this char c, char min, char max)
		{
			// ReSharper disable RedundantCast - justification: more clear this way.
			return (uint)c - (uint)min <= (uint)max - (uint)min;
			// ReSharper restore RedundantCast
		}

		public static bool IsLatin1(this char ch)
		{
			return ch <= 'ÿ';
		}

		public static bool IsAscii(this char ch)
		{
			return ch <= '\x007F';
		}

		public static bool IsAsciiLetter(this char ch)
		{
			ch |= ' ';
			return IsInRange(ch, 'a', 'z');
		}

		public static bool IsAsciiDigit(this char ch)
		{
			return IsInRange(ch, '0', '9');
		}
	}
}
