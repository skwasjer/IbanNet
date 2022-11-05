using FluentAssertions.Collections;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using IbanNet;
using IbanNet.Registry;
using IbanNet.Validation.Rules;

namespace TestHelpers.FluentAssertions;

public class IbanValidatorOptionsAssertions
    : ReferenceTypeAssertions<IbanValidatorOptions, IbanValidatorOptionsAssertions>
{
    public IbanValidatorOptionsAssertions(IbanValidatorOptions instance)
        : base(instance) { }

    protected override string Identifier => "ibanValidatorOptions";

    public AndConstraint<GenericCollectionAssertions<IbanCountry>> HaveRegistry
    (
        IEnumerable<IbanCountry> registry,
        string because = "",
        params object[] becauseArgs
    )
    {
        Subject.Registry.Should().BeEquivalentTo(registry, because, becauseArgs);
        return new AndConstraint<GenericCollectionAssertions<IbanCountry>>(
            new GenericCollectionAssertions<IbanCountry>(Subject.Registry)
        );
    }

    public AndConstraint<GenericCollectionAssertions<TRule>> HaveRule<TRule>
    (
        string because = "",
        params object[] becauseArgs
    )
        where TRule : IIbanValidationRule
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .Given(() => Subject.Rules)
            .ForCondition(rules => rules.OfType<TRule>().Any())
            .FailWith("Expected {context:options} to have a rule of type {0}{reason}, but found {1}.",
                _ => typeof(TRule),
                rules => rules.Select(r => r.GetType()));

        return new AndConstraint<GenericCollectionAssertions<TRule>>(
            new GenericCollectionAssertions<TRule>(Subject.Rules.OfType<TRule>())
        );
    }
}