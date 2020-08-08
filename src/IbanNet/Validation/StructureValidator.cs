using System.Collections.Generic;

namespace IbanNet.Validation
{
    internal class StructureValidator : IStructureValidator
    {
        private readonly List<StructureSegmentTest> _segmentTests;

        public StructureValidator(List<StructureSegmentTest> segmentTests)
        {
            _segmentTests = segmentTests;
        }

        public bool Validate(string iban)
        {
            int pos = 0;
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
