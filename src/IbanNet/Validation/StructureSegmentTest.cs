using System;
using System.Diagnostics.CodeAnalysis;

namespace IbanNet.Validation
{
    internal class StructureSegmentTest
    {
        public int Occurrences;

        public bool IsFixedLength = true;

        [AllowNull]
        public Func<char, int, bool> Test = null;
    }
}
