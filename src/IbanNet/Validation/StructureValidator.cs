using System.Collections.Generic;
using System.Linq;

namespace IbanNet.Validation
{
    internal class StructureValidator : IStructureValidator
    {
        private readonly List<StructureSegmentTest> _segmentTests;
        private readonly bool _fixedLength;

        public StructureValidator(List<StructureSegmentTest> segmentTests)
        {
            _segmentTests = segmentTests;
            _fixedLength = _segmentTests.All(t => t.IsFixedLength);
        }

        public bool Validate(string iban)
        {
            // Short-circuit, if all tests are fixed length, use faster validation.
            return _fixedLength ? ValidateFixedLength(iban) : ValidateNonFixedLength(iban);
        }

        private bool ValidateFixedLength(string iban)
        {
            int pos = 0;
            int segmentIndex = 0;
            // ReSharper disable once ForCanBeConvertedToForeach - justification : performance critical
            for (; segmentIndex < _segmentTests.Count; segmentIndex++)
            {
                StructureSegmentTest expectedStructureSegment = _segmentTests[segmentIndex];
                for (int occurrence = 0; occurrence < expectedStructureSegment.Occurrences; occurrence++)
                {
                    char c = iban[pos];
                    if (!expectedStructureSegment.Test(c, pos))
                    {
                        return false;
                    }

                    pos++;
                }
            }

            return iban.Length == pos && segmentIndex == _segmentTests.Count;
        }

        private bool ValidateNonFixedLength(string iban)
        {
            int pos = 0;
            int segmentIndex = 0;
            for (; segmentIndex < _segmentTests.Count; segmentIndex++)
            {
                StructureSegmentTest expectedStructureSegment = _segmentTests[segmentIndex];
                if (expectedStructureSegment.IsFixedLength)
                {
                    if (!ProcessFixedLengthTest(expectedStructureSegment, iban, ref pos))
                    {
                        return false;
                    }
                }
                else
                {
                    ProcessNonFixedLengthTest(expectedStructureSegment, iban, ref pos);
                }
            }

            return iban.Length == pos && segmentIndex == _segmentTests.Count;
        }

        private static bool ProcessFixedLengthTest(StructureSegmentTest test, string value, ref int pos)
        {
            if (pos + test.Occurrences > value.Length)
            {
                return false;
            }

            for (int occurrence = 0; occurrence < test.Occurrences; occurrence++)
            {
                char c = value[pos];
                if (!test.Test(c, pos))
                {
                    return false;
                }

                pos++;
            }

            return true;
        }

        private static bool ProcessNonFixedLengthTest(StructureSegmentTest test, string value, ref int pos)
        {
            for (int occurrence = 0; occurrence < test.Occurrences; occurrence++)
            {
                if (pos >= value.Length)
                {
                    return true;
                }

                char c = value[pos];
                if (!test.Test(c, pos))
                {
                    return false;
                }

                pos++;
            }

            return true;
        }
    }
}
