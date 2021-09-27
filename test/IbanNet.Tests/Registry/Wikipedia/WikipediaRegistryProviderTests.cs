using System.Threading.Tasks;

namespace IbanNet.Registry.Wikipedia
{
    public class WikipediaRegistryProviderTests : BaseRegistryProviderSpec<WikipediaRegistryProvider>
    {
        public WikipediaRegistryProviderTests() : base(105)
        {
        }

        protected override Task<WikipediaRegistryProvider> CreateSubjectAsync()
        {
            return Task.FromResult(new WikipediaRegistryProvider());
        }
    }
}
