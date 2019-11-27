using System;
using System.Collections.Generic;
using System.Linq;
using IbanNet.Extensions;

namespace IbanNet.Validation
{
	/// <summary>
	/// A factory to create validators that are based on the Swift Registry its structure definitions.
	/// </summary>
	internal class SwiftStructureValidationFactory : IStructureValidationFactory
	{
		private static readonly IDictionary<char, Func<char, bool>> SegmentMap = new Dictionary<char, Func<char, bool>>
		{
			{ 'n', c => c.IsAsciiDigit() },
			{ 'a', c => c.IsUpperAsciiLetter() },
			{ 'c', c => c.IsAlphaNumeric() },
			{ 'e', c => c == ' ' }
		};

		/// <inheritdoc />
		// ReSharper disable once InconsistentNaming
		public IStructureValidator CreateValidator(string twoLetterISORegionName, string structure)
		{
			return new StructureValidator(structure.Substring(0, 2), GetSegments(structure.Substring(2)).ToList());
		}

		private IEnumerable<StructureSegmentTest> GetSegments(string structure)
		{
			return structure
				.PartitionOn(SegmentMap.Keys.ToArray())
				.Select(GetSegmentInfo);
		}

		/// <remarks>
		/// https://www.swift.com/standards/data-standards/iban
		/// length
		/// ! = fixed
		/// marker
		/// </remarks>
		private StructureSegmentTest GetSegmentInfo(string structureSegment)
		{
			char segmentType = structureSegment[structureSegment.Length - 1];
			if (!SegmentMap.TryGetValue(segmentType, out Func<char, bool> characterTest))
			{
				throw new ArgumentException($"The structure segment '{structureSegment}' is invalid.", nameof(structureSegment));
			}

			string lengthDescriptor = structureSegment.Substring(0, structureSegment.Length - 1);
			bool isFixedLength = lengthDescriptor[lengthDescriptor.Length - 1] == '!';
			int occurrences = int.Parse(
				lengthDescriptor.Substring(0, lengthDescriptor.Length - Convert.ToByte(isFixedLength))
			);

			return new StructureSegmentTest
			{
				Occurrences = occurrences,
				Test = characterTest
			};
		}
	}
}
