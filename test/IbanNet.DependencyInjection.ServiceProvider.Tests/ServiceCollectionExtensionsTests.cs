using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using TestHelpers;
using Xunit;

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
                Action<IIbanNetOptionsBuilder> builder = _ => { };

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
