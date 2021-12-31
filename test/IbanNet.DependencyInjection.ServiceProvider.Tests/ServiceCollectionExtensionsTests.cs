using Microsoft.Extensions.DependencyInjection;
using TestHelpers;

namespace IbanNet.DependencyInjection.ServiceProvider
{
    public class ServiceCollectionExtensionsTests
    {
        public class NullArgumentTests : IbanNetOptionsBuilderTests
        {
            [Theory]
            [MemberData(nameof(AddIbanNetNullTestCases))]
            public void Given_null_instance_when_calling_method_it_should_throw(params object[] args)
            {
                NullArgumentTest.Execute(args);
            }

            public static IEnumerable<object[]> AddIbanNetNullTestCases()
            {
                IServiceCollection services = new ServiceCollection();
#pragma warning disable IDE0039 // Use local function
                // ReSharper disable once ConvertToLocalFunction
                Action<IIbanNetOptionsBuilder> builder = _ => { };
#pragma warning restore IDE0039 // Use local function

                return new NullArgumentTestCases
                {
                    // Instance
                    DelegateTestCase.Create(
                        ServiceCollectionExtensions.AddIbanNet,
                        services),
                    DelegateTestCase.Create(
                        ServiceCollectionExtensions.AddIbanNet,
                        services,
                        true),
                    DelegateTestCase.Create(
                        ServiceCollectionExtensions.AddIbanNet,
                        services,
                        builder),
                    DelegateTestCase.Create(
                        ServiceCollectionExtensions.AddIbanNet,
                        services,
                        builder,
                        true),
                }.Flatten();
            }
        }
    }
}
