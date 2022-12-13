using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using IbanNet.Builders;
using IbanNet.Registry.Patterns;

namespace IbanNet.Registry;

/// <summary>
/// A generator to create random IBAN's.
/// </summary>
public class IbanGenerator : IIbanGenerator
{
    private readonly IIbanRegistry _registry;
    private readonly Generator _generator;

    /// <summary>
    /// Initializes a new instance of the <see cref="IbanGenerator" /> class.
    /// </summary>
    public IbanGenerator()
        : this (IbanRegistry.Default, null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IbanGenerator" /> class using specified <paramref name="registry" />.
    /// </summary>
    /// <param name="registry">The registry containing the IBAN country definitions.</param>
    public IbanGenerator(IIbanRegistry registry)
        // ReSharper disable once IntroduceOptionalParameters.Global
        : this(registry, null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IbanGenerator" /> class using specified <paramref name="registry" />.
    /// </summary>
    /// <param name="registry">The registry containing the IBAN country definitions.</param>
    /// <param name="seed">The generator seed.</param>
    public IbanGenerator(IIbanRegistry registry, int? seed)
    {
        _registry = registry ?? throw new ArgumentNullException(nameof(registry));
        _generator = seed.HasValue ? new Generator(seed.Value) : Generator.Default;
    }

    /// <inheritdoc />
    public Iban Generate(string countryCode)
    {
        if (countryCode is null)
        {
            throw new ArgumentNullException(nameof(countryCode));
        }

        if (!_registry.TryGetValue(countryCode.ToUpperInvariant(), out IbanCountry? country))
        {
            throw new ArgumentException(string.Format(
                    CultureInfo.CurrentCulture,
                    Resources.ArgumentException_The_country_code_0_is_not_registered,
                    countryCode),
                nameof(countryCode)
            );
        }

        Pattern? bbanPattern = country.Bban.Pattern;
        if (bbanPattern is null || bbanPattern is NullPattern)
        {
            throw new InvalidOperationException($"The country '{countryCode}' does not have a BBAN pattern.");
        }

        string ibanStr = country
            .GetIbanBuilder()
            .WithBankAccountNumber(_generator.Random(bbanPattern))
            .Build();
        return new Iban(ibanStr, country);
    }

    [SuppressMessage("Security", "CA5394:Do not use insecure randomness", Justification = "Not used in secure context.")]
    internal sealed class Generator
    {
        internal static readonly Generator Default = new(DateTime.UtcNow.Ticks.GetHashCode());

        private readonly Random _rng;
        private readonly object _rngLock = new();

        private static readonly AsciiCategory[] LetterCategories = { AsciiCategory.LowercaseLetter, AsciiCategory.UppercaseLetter };
        private static readonly AsciiCategory[] AlphaNumericCategories = { AsciiCategory.Digit, AsciiCategory.LowercaseLetter, AsciiCategory.UppercaseLetter };

        internal Generator(int seed)
        {
            _rng = new Random(seed);
        }

        internal string Random(Pattern pattern)
        {
            return string.Join("", pattern.Tokens.Select(Random));
        }

        internal string Random(PatternToken token)
        {
            int iterations = token.MaxLength;
            if (!token.IsFixedLength)
            {
                lock (_rngLock)
                {
                    iterations = _rng.Next(token.MinLength, token.MaxLength);
                }
            }

            int charCount = 0;
            var sb = new StringBuilder(token.MaxLength);
            do
            {
                AsciiCategory category = token.Category
                    switch
                    {
                        AsciiCategory.Letter => GetRandomCategory(LetterCategories),
                        AsciiCategory.AlphaNumeric => GetRandomCategory(AlphaNumericCategories),
                        _ => token.Category
                    };

                char charPos;
                int charRange;
                // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
                switch (category)
                {
                    case AsciiCategory.Digit:
                        charPos = '0';
                        charRange = 10;
                        break;
                    case AsciiCategory.LowercaseLetter:
                        charPos = 'a';
                        charRange = 26;
                        break;
                    case AsciiCategory.UppercaseLetter:
                        charPos = 'A';
                        charRange = 26;
                        break;
                    case AsciiCategory.Space:
                        sb.Append(' ');
                        charCount++;
                        continue;
                    default:
                        // Cannot occur since pattern category is already guarded.
                        throw new InvalidOperationException();
                }

                int offset;
                lock (_rngLock)
                {
                    offset = _rng.Next(0, charRange - 1);
                }

                char randomChar = (char)(charPos + offset);
                sb.Append(randomChar);
                charCount++;
            } while (charCount < iterations);

            return sb.ToString();
        }

        private AsciiCategory GetRandomCategory(IReadOnlyList<AsciiCategory> from)
        {
            int i;
            lock (_rngLock)
            {
                i = _rng.Next(0, from.Count - 1);
            }

            return from[i];
        }
    }
}
