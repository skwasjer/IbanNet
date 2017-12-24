using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace IbanNet
{
	internal class IbanDefinitions : ReadOnlyDictionary<string, IbanDefinition>
	{
		public IbanDefinitions() : base(GetDefinitions())
		{
		}

		/// <summary>
		/// Partitions the <see cref="DefinitionData"/> and then builds <see cref="IbanDefinition"/>s from the resulting data. The <see cref="DefinitionData"/> is expected to have 8 segments per definition.
		/// </summary>
		/// <returns></returns>
		private static IDictionary<string, IbanDefinition> GetDefinitions()
		{

			var definitions = DefinitionData
				.Partition(8)
				.Select(d =>
				{
					var parts = d.ToList();
					return new IbanDefinition
					{
						CountryCode = (string) parts[0],
						Length = (int) parts[1],
						Structure = (string) parts[2],
						Example = (string) parts[3]
					};
				})
				.OrderBy(d => d.CountryCode)
				.ToDictionary(kvp => kvp.CountryCode);

			// At dev time, ensure all definitions are valid.
			Debug.Assert(definitions.All(kvp => kvp.Value.Validate()), "One or more IBAN definitions are invalid.");

			return definitions;
		}

		/// <summary>
		/// Definitions copied directly from http://www.tbg5-finance.org/checkiban.js.
		/// </summary>
		/// <remarks>
		/// We don't need the columns 4, 5, 6 and 7, but to make updates easier, we just include them and ignore them by code.
		/// </remarks>
		private static readonly object[] DefinitionData =
		{
			"AD", 24, "F04F04A12", "AD1200012030200359100100", "n", "n", "n", "n",
			"AE", 23, "F03F16", "AE070331234567890123456", "n", "n", "n", "n",
			"AL", 28, "F08A16", "AL47212110090000000235698741", "n", "n", "n", "n",
			"AT", 20, "F05F11", "AT611904300234573201", "y", "y", "y", "y",
			"AZ", 28, "U04A20", "AZ21NABZ00000000137010001944", "n", "n", "n", "n",
			"BA", 20, "F03F03F08F02", "BA391290079401028494", "n", "n", "n", "n",
			"BE", 16, "F03F07F02", "BE68539007547034", "y", "y", "y", "y",
			"BG", 22, "U04F04F02A08", "BG80BNBG96611020345678", "y", "y", "y", "n",
			"BH", 22, "U04A14", "BH67BMAG00001299123456", "y", "n", "n", "n",
			"BR", 29, "F08F05F10U01A01", "BR9700360305000010009795493P1", "n", "n", "n", "n",
			"CH", 21, "F05A12", "CH9300762011623852957", "n", "y", "n", "n",
			"CR", 22, "F04F14", "CR05015202001026284066", "n", "n", "n", "n",
			"CY", 28, "F03F05A16", "CY17002001280000001200527600", "y", "y", "y", "y",
			"CZ", 24, "F04F06F10", "CZ6508000000192000145399", "y", "y", "y", "n",
			"DE", 22, "F08F10", "DE89370400440532013000", "y", "y", "y", "y",
			"DK", 18, "F04F09F01", "DK5000400440116243", "y", "y", "y", "n",
			"DO", 28, "U04F20", "DO28BAGR00000001212453611324", "n", "n", "n", "n",
			"EE", 20, "F02F02F11F01", "EE382200221020145685", "y", "y", "y", "y",
			"ES", 24, "F04F04F01F01F10", "ES9121000418450200051332", "y", "y", "y", "y",
			"FI", 18, "F06F07F01", "FI2112345600000785", "y", "y", "y", "y",
			"FO", 18, "F04F09F01", "FO6264600001631634", "n", "y", "n", "n",
			"FR", 27, "F05F05A11F02", "FR1420041010050500013M02606", "y", "y", "y", "y",
			"GB", 22, "U04F06F08", "GB29NWBK60161331926819", "y", "y", "y", "n",
			"GE", 22, "U02F16", "GE29NB0000000101904917", "n", "n", "n", "n",
			"GI", 23, "U04A15", "GI75NWBK000000007099453", "y", "y", "y", "n",
			"GL", 18, "F04F09F01", "GL8964710001000206", "n", "y", "n", "n",
			"GR", 27, "F03F04A16", "GR1601101250000000012300695", "y", "y", "y", "y",
			"GT", 28, "A04A20", "GT82TRAJ01020000001210029690", "n", "n", "n", "n",
			"HR", 21, "F07F10", "HR1210010051863000160", "y", "y", "y", "n",
			"HU", 28, "F03F04F01F15F01", "HU42117730161111101800000000", "y", "y", "y", "n",
			"IE", 22, "U04F06F08", "IE29AIBK93115212345678", "y", "y", "y", "y",
			"IL", 23, "F03F03F13", "IL620108000000099999999", "n", "n", "n", "n",
			"IS", 26, "F04F02F06F10", "IS140159260076545510730339", "n", "y", "n", "n",
			"IT", 27, "U01F05F05A12", "IT60X0542811101000000123456", "y", "y", "y", "y",
			"JO", 30, "U04F04A18", "JO94CBJO0010000000000131000302", "y", "n", "n", "n",
			"KW", 30, "U04A22", "KW81CBKU0000000000001234560101", "y", "n", "n", "n",
			"KZ", 20, "F03A13", "KZ86125KZT5004100100", "n", "n", "n", "n",
			"LB", 28, "F04A20", "LB62099900000001001901229114", "n", "n", "n", "n",
			"LC", 32, "U04A24", "LC55HEMM000100010012001200023015", "n", "n", "n", "n",
			"LI", 21, "F05A12", "LI21088100002324013AA", "y", "y", "y", "n",
			"LT", 20, "F05F11", "LT121000011101001000", "y", "y", "y", "n",
			"LU", 20, "F03A13", "LU280019400644750000", "y", "y", "y", "y",
			"LV", 21, "U04A13", "LV80BANK0000435195001", "y", "y", "y", "n",
			"MC", 27, "F05F05A11F02", "MC5811222000010123456789030", "n", "y", "n", "n",
			"MD", 24, "A02A18", "MD24AG000225100013104168", "n", "n", "n", "n",
			"ME", 22, "F03F13F02", "ME25505000012345678951", "n", "n", "n", "n",
			"MK", 19, "F03A10F02", "MK07250120000058984", "n", "n", "n", "n",
			"MR", 27, "F05F05F11F02", "MR1300020001010000123456753", "n", "n", "n", "n",
			"MT", 31, "U04F05A18", "MT84MALT011000012345MTLCAST001S", "y", "y", "y", "y",
			"MU", 30, "U04F02F02F12F03U03", "MU17BOMM0101101030300200000MUR", "n", "n", "n", "n",
			"NL", 18, "U04F10", "NL91ABNA0417164300", "y", "y", "y", "y",
			"NO", 15, "F04F06F01", "NO9386011117947", "n", "y", "n", "n",
			"PK", 24, "U04A16", "PK36SCBL0000001123456702", "n", "n", "n", "n",
			"PL", 28, "F08F16", "PL61109010140000071219812874", "y", "y", "y", "n",
			"PS", 29, "U04A21", "PS92PALS000000000400123456702", "n", "n", "n", "n",
			"PT", 25, "F04F04F11F02", "PT50000201231234567890154", "y", "y", "y", "y",
			"QA", 29, "U04A21", "QA58DOHB00001234567890ABCDEFG", "y", "n", "n", "n",
			"RO", 24, "U04A16", "RO49AAAA1B31007593840000", "y", "y", "y", "n",
			"RS", 22, "F03F13F02", "RS35260005601001611379", "n", "n", "n", "n",
			"SA", 24, "F02A18", "SA0380000000608010167519", "y", "n", "n", "n",
			"SC", 31, "U04F02F02F16U03", "SC18SSCB11010000000000001497USD", "n", "n", "n", "n",
			"SE", 24, "F03F16F01", "SE4550000000058398257466", "y", "y", "y", "n",
			"SI", 19, "F05F08F02", "SI56263300012039086", "y", "y", "y", "n",
			"SK", 24, "F04F06F10", "SK3112000000198742637541", "y", "y", "y", "y",
			"SM", 27, "U01F05F05A12", "SM86U0322509800000000270100", "n", "y", "n", "n",
			"ST", 25, "F08F11F02", "ST68000100010051845310112", "n", "n", "n", "n",
			"TL", 23, "F03F14F02", "TL380080012345678910157", "n", "n", "n", "n",
			"TN", 24, "F02F03F13F02", "TN5910006035183598478831", "n", "n", "n", "n",
			"TR", 26, "F05A01A16", "TR330006100519786457841326", "y", "n", "n", "n",
			"UA", 29, "F06A19", "UA213996220000026007233566001", "n", "n", "n", "n",
			"VG", 24, "U04F16", "VG96VPVG0000012345678901", "n", "n", "n", "n",
			"XK", 20, "F04F10F02", "XK051212012345678906", "n", "n", "n", "y"
		};
	}
}
