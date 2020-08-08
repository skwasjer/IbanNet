using System;

namespace IbanNet.Validation
{
    internal struct StructureSegmentTest
    {
        public int Occurrences;

        public Func<char, int, bool> Test;
    }
}
