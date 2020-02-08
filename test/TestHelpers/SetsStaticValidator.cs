using Xunit;

namespace TestHelpers
{
	/// <summary>
	/// Marker to disable parallel tests because it sets static validator.
	/// </summary>
	[CollectionDefinition(nameof(SetsStaticValidator), DisableParallelization = true)]
	public class SetsStaticValidator
	{
	}
}
