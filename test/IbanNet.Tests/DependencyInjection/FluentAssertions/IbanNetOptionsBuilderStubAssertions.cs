using FluentAssertions.Collections;
using FluentAssertions.Primitives;
using IbanNet.Registry;
using IbanNet.Validation.Rules;
using TestHelpers.FluentAssertions;

namespace IbanNet.DependencyInjection.FluentAssertions
{
    public class IbanNetOptionsBuilderStubAssertions<T>
        : ReferenceTypeAssertions<Mock<T>, IbanNetOptionsBuilderStubAssertions<T>>
        where T : class, IIbanNetOptionsBuilder
    {
        public IbanNetOptionsBuilderStubAssertions(Mock<T> instance)
#if FlUENT_ASSERTIONS_5
        {
            Subject = instance;
        }
#else
            : base(instance) { }
#endif

        protected override string Identifier => typeof(T).Name;

        public AndConstraint<GenericCollectionAssertions<IbanCountry>> HaveConfiguredRegistry
        (
            IEnumerable<IbanCountry> registry,
            string because = "",
            params object[] becauseArgs
        )
        {
            AndConstraint<GenericCollectionAssertions<IbanCountry>> innerAssertion = null;
            VerifyCalled(should => innerAssertion = should.HaveRegistry(registry, because, becauseArgs));

            return innerAssertion;
        }

        public AndConstraint<GenericCollectionAssertions<TRule>> HaveConfiguredRule<TRule>
        (
            string because = "",
            params object[] becauseArgs
        )
            where TRule : IIbanValidationRule
        {
            AndConstraint<GenericCollectionAssertions<TRule>> innerAssertion = null;
            VerifyCalled(should => innerAssertion = should.HaveRule<TRule>(because, becauseArgs));

            return innerAssertion;
        }

        private void VerifyCalled(Action<IbanValidatorOptionsAssertions> should)
        {
            Func<Action<DependencyResolverAdapter, IbanValidatorOptions>, bool> assert = configureAction =>
            {
                var opts = new IbanValidatorOptions();
                DependencyResolverAdapter adapter = new Mock<DependencyResolverAdapter>().Object;
                configureAction(adapter, opts);

                should(opts.Should());
                return true;
            };

            Subject.Verify(m => m.Configure(It.Is<Action<DependencyResolverAdapter, IbanValidatorOptions>>(action => assert(action))));
        }
    }
}
