namespace IbanNetExample
{
    [App(icon: Icons.PartyPopper, title: "IbanNet Demo")]
    public class IbanNetDemoApp : ViewBase
    {
        private readonly IbanValidator _validator;
        private readonly IbanParser _parser;
        private readonly IbanRegistry _registry;
        private readonly SwiftRegistryProvider _swiftProvider = new();
        private readonly WikipediaRegistryProvider _wikiProvider = new();
        
        /// <summary>Initialize components for IBAN parsing, validation, and registry</summary>
        public IbanNetDemoApp()
        {
            _registry = new IbanRegistry { Providers = { _swiftProvider, _wikiProvider } };
            _parser = new IbanParser(_registry);
            _validator = new IbanValidator();
        }

        public override object? Build()
        {
            var ibanState = UseState<string>();
            var outputState = UseState<string>();
            var badgeState = UseState<string>();
            var selectedCountry = UseState<string>();

            var countries = _registry.OrderBy(c => c.TwoLetterISORegionName).Select(c => c.TwoLetterISORegionName).ToArray();
            var countrySelect = selectedCountry.ToSelectInput(countries.ToOptions());
            var ibanInput = new TextInput(ibanState.Value, e => HandleIbanChanged(e.Value), placeholder: "Enter IBAN here")
                .Invalid(string.IsNullOrEmpty(outputState.Value) ? null : outputState.Value);
            bool hasValidIban = _parser.TryParse(ibanState.Value, out var iban);

            var generateBtn = new Button("Generate Test IBAN", () =>
            {
                try
                {
                    var generator = new IbanGenerator();
                    var generated = generator.Generate(selectedCountry.Value);
                    HandleIbanChanged(generated.ToString());
                    outputState.Set("");
                }
                catch
                {
                    outputState.Set("Cannot generate IBAN for selected country");
                }
            });

            // Left Card - Input and Generation
            var leftCard = new Card(
                Layout.Vertical()
                | Text.H3("IBAN Input & Generator")
                | Text.Muted("IBAN validation and generation tool for testing international bank account numbers")
                | new Spacer()
                | Text.Label("Enter IBAN:")
                | ibanInput
                | Text.Label("Generate Test IBAN:")
                | countrySelect.Placeholder("Select Country")
                | generateBtn
                | new Spacer()
                | Text.Small("This demo uses IbanNet library to validate and generate IBAN numbers.")
			    | Text.Markdown("Built with [Ivy Framework](https://github.com/Ivy-Interactive/Ivy-Framework) and [IbanNet](https://github.com/skwasjer/IbanNet)")
                ).Width(Size.Fraction(0.35f)).Height(Size.Fit().Min(Size.Full()));

            // Right Card - Results
            var rightCard = new Card(
                Layout.Vertical()
                | (hasValidIban
                    ? new IbanDetailsView(iban)
                    :Layout.Vertical()
                    | Text.H3("IBAN Details")
                    | Layout.Vertical()
                        | Callout.Info("Enter a valid IBAN number in the left panel or generate a test IBAN to see detailed information here.", "Instructions")
                        | Callout.Info("IBAN (International Bank Account Number) is a standardized format for bank account identification.", "About IBAN"))
            ).Width(Size.Fraction(0.55f)).Height(Size.Fit().Min(Size.Full()));

            return Layout.Horizontal().Gap(10).Align(Align.TopCenter)
                | leftCard
                | rightCard;

            /// <summary>Handles user input: validates IBAN, updates states and error messages.</summary>
            void HandleIbanChanged(string? value)
            {
                outputState.Set("");
                ibanState.Set(value);
                if (value.Length == 0)
                {
                    badgeState.Set("");

                }
                else
                {
                    var result = _validator.Validate(ibanState.Value);
                    if (result.IsValid)
                    {
                        badgeState.Set("Valid");
                    }
                    else
                    {
                        outputState.Set(result.Error.ErrorMessage);
                        badgeState.Set("Invalid");
                    }

                }
            }
        }
    }

    /// <summary>Displays detailed information about a parsed IBAN</summary>
    public class IbanDetailsView(Iban iban) : ViewBase
    {
        public override object? Build()
        {
            return Layout.Vertical()
                            | Text.H3($"IBAN Info: {iban}")
                            | Text.Muted("Comprehensive breakdown of IBAN structure, country information, bank details, and validation results")
                            | new
                            {
                                Country = new
                                {
                                    iban.Country.EnglishName,
                                    iban.Country.NativeName,
                                    ISO = iban.Country.TwoLetterISORegionName,
                                    IsSepaMember = iban.Country.Sepa.IsMember,
                                }.ToDetails(),
                                BBAN = iban.Bban,
                                iban.BankIdentifier,
                                iban.BranchIdentifier,
                                IbanFormat = new
                                {
                                    Electronic = iban.ToString(IbanFormat.Electronic),
                                    Print = iban.ToString(IbanFormat.Print),
                                    Obfuscated = iban.ToString(IbanFormat.Obfuscated)
                                }.ToDetails()
                                    .Builder(x => x.Electronic, b => b.CopyToClipboard())
                            }.ToDetails();
        }
    }
}
