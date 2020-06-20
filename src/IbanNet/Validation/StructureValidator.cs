using System.Collections.Generic;

namespace IbanNet.Validation
{
    internal class StructureValidator : IStructureValidator
    {
        private readonly string _expectedCountryCode;
        private readonly List<StructureSegmentTest> _segmentTests;

        public StructureValidator(string expectedCountryCode, List<StructureSegmentTest> segmentTests)
        {
            _expectedCountryCode = expectedCountryCode;
            _segmentTests = segmentTests;
        }

        public bool Validate(string iban)
        {
            if (iban[0] != _expectedCountryCode[0] && iban[1] != _expectedCountryCode[1])
            {
                return false;
            }

            int pos = 2;
            // ReSharper disable once ForCanBeConvertedToForeach - justification : performance critical
            for (int segmentIndex = 0; segmentIndex < _segmentTests.Count; segmentIndex++)
            {
                StructureSegmentTest expectedStructureSegment = _segmentTests[segmentIndex];
                for (int occurrence = 0; occurrence < expectedStructureSegment.Occurrences; occurrence++)
                {
                    char c = iban[pos++];
                    if (!expectedStructureSegment.Test(c))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
