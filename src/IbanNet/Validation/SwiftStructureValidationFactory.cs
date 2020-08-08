using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using IbanNet.Extensions;

namespace IbanNet.Validation
{
    /// <summary>
    /// A factory to create validators that are based on the Swift Registry its structure definitions.
    /// </summary>
    public class SwiftStructureValidationFactory : IStructureValidationFactory
    {
        private static readonly IDictionary<char, Func<char, int, bool>> SegmentMap = new Dictionary<char, Func<char, int, bool>>
        {
            { 'n', (c, _) => c.IsAsciiDigit() },
            { 'a', (c, _) => c.IsUpperAsciiLetter() },
            { 'c', (c, _) => c.IsAlphaNumeric() },
            { 'e', (c, _) => c == ' ' }
        };

        /// <inheritdoc />
        // ReSharper disable once InconsistentNaming
        public IStructureValidator CreateValidator(string twoLetterISORegionName, string pattern)
        {
            if (pattern is null)
            {
                throw new ArgumentNullException(nameof(pattern));
            }

            return new StructureValidator(GetSegmentTests(pattern).ToList());
        }

        private static IEnumerable<StructureSegmentTest> GetSegmentTests(string pattern)
        {
            // First 2 chars are country code.
            yield return new StructureSegmentTest
            {
                Occurrences = 2,
                Test = (c, i) => char.ToUpperInvariant(c) == char.ToUpperInvariant(pattern[i])
            };

            foreach (StructureSegmentTest test in pattern
                .Substring(2)
                .PartitionOn(SegmentMap.Keys.ToArray())
                .Select(GetSegmentTest))
            {
                yield return test;
            }
        }

        /// <remarks>
        /// https://www.swift.com/standards/data-standards/iban
        /// length
        /// ! = fixed
        /// marker
        /// </remarks>
        private static StructureSegmentTest GetSegmentTest(string pattern)
        {
            char segmentType = pattern[pattern.Length - 1];
            if (!SegmentMap.TryGetValue(segmentType, out Func<char, int, bool> characterTest))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.ArgumentException_The_structure_segment_0_is_invalid, pattern), nameof(pattern));
            }

            string lengthDescriptor = pattern.Substring(0, pattern.Length - 1);
            bool isFixedLength = lengthDescriptor[lengthDescriptor.Length - 1] == '!';
            int occurrences = int.Parse(
                lengthDescriptor.Substring(0, lengthDescriptor.Length - Convert.ToByte(isFixedLength)),
                CultureInfo.InvariantCulture
            );

            return new StructureSegmentTest
            {
                IsFixedLength = isFixedLength,
                Occurrences = occurrences,
                Test = characterTest
            };
        }
    }
}
