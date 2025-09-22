namespace IbanNet.Validation.Results;

/// <summary>
/// Encapsulates the result of a validation rule.
/// </summary>
public record ValidationRuleResult
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationRuleResult" />.
    /// </summary>
    // ReSharper disable once MemberCanBeProtected.Global - justification: not extensible for external library users.
    protected internal ValidationRuleResult()
    {
    }

    /// <summary>
    /// Signals the success of the rule.
    /// </summary>
    public static ValidationRuleResult Success => Nested.Instance;

    private static class Nested
    {
        // Do not mark type as beforefieldinit.
        static Nested()
        {
        }

        internal static readonly ValidationRuleResult Instance = new();
    }
}
