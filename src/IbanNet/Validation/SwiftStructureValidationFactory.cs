using System;
using IbanNet.Registry.Swift;

namespace IbanNet.Validation
{
    /// <summary>
    /// A factory to create validators that are based on the Swift Registry its structure definitions.
    /// </summary>
    [Obsolete("Will be removed in v5.0. For custom patterns, switch to Pattern/ITokenizer.")]
    public class SwiftStructureValidationFactory : IStructureValidationFactory
    {
        private readonly SwiftPatternTokenizer _tokenizer = new SwiftPatternTokenizer();

        /// <inheritdoc />
        // ReSharper disable once InconsistentNaming
        public IStructureValidator CreateValidator(string twoLetterISORegionName, string pattern)
        {
            if (pattern is null)
            {
                throw new ArgumentNullException(nameof(pattern));
            }

            return new StructureValidator(_tokenizer.Tokenize(pattern));
        }
    }
}
