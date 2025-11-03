# IbanNet 

<img width="1914" height="908" alt="image" src="https://github.com/user-attachments/assets/66a63eda-d58e-4d89-8ce5-243c77fa4a69" />

<img width="1911" height="908" alt="image" src="https://github.com/user-attachments/assets/b2c5179a-8613-4124-b8fd-6891edd9de7d" />

## One-Click Development Environment

[![Open in GitHub Codespaces](https://github.com/codespaces/badge.svg)](https://github.com/codespaces/new?hide_repo_select=true&ref=main&repo=Ivy-Interactive%2FIvy-Examples&machine=standardLinux32gb&devcontainer_path=.devcontainer%2Fibannet-2%2Fdevcontainer.json&location=EuropeWest)

Click the badge above to open Ivy Examples repository in GitHub Codespaces with:
- **.NET 9.0** SDK pre-installed
- **Ready-to-run** development environment
- **No local setup** required

## Created Using Ivy

Web application created using [Ivy-Framework](https://github.com/Ivy-Interactive/Ivy-Framework).

**Ivy** - The ultimate framework for building internal tools with LLM code generation by unifying front-end and back-end into a single C# codebase. With Ivy, you can build robust internal tools and dashboards using C# and AI assistance based on your existing database.
Web application created using [Ivy-Framework](https://github.com/Ivy-Interactive/Ivy-Framework).

**Ivy** - The ultimate framework for building internal tools with LLM code generation by unifying front-end and back-end into a single C# codebase. With Ivy, you can build robust internal tools and dashboards using C# and AI assistance based on your existing database.

Ivy is a web framework for building interactive web applications using C# and .NET.

## Interactive IBAN Validation & Generation Tool

This example demonstrates IBAN (International Bank Account Number) validation and generation using the [IbanNet library](https://github.com/skwasjer/IbanNet) integrated with Ivy. IbanNet is a comprehensive .NET library for validating, parsing, and generating IBAN numbers according to international standards.

**What This Application Does:**

This specific implementation creates an **IBAN Management** application that allows users to:

- **Validate IBAN Numbers**: Enter any IBAN number to check its validity and structure
- **Generate Test IBANs**: Create valid test IBAN numbers for any supported country
- **View Detailed Analysis**: Get comprehensive breakdown of IBAN structure, country information, and bank details
- **Copy to Clipboard**: Easily copy IBAN numbers and electronic formats for use in other applications
- **Country Support**: Access to 100+ countries with their specific IBAN formats and validation rules
- **Real-time Validation**: Instant feedback with visual indicators (valid/invalid badges)
- **Interactive UI**: Split-panel layout with input controls on the left and detailed results on the right

**Technical Implementation:**

- Uses IbanNet's `IbanValidator` for robust IBAN validation
- Implements `IbanGenerator` for creating test IBAN numbers
- Leverages `IbanParser` for detailed IBAN structure analysis
- Integrates `SwiftRegistryProvider` and `WikipediaRegistryProvider` for comprehensive country data
- Creates responsive card-based layout with callout notifications
- Handles clipboard operations for easy IBAN copying
- Supports multiple IBAN formats (Electronic, Print, Obfuscated)
- Provides detailed country information including SEPA membership status

**Key Features:**

- **Validation Engine**: Checks IBAN format, country code, check digits, and BBAN structure
- **Generation Tool**: Creates valid test IBANs for development and testing purposes
- **Country Registry**: Access to official IBAN specifications for 100+ countries
- **Format Support**: Multiple display formats for different use cases
- **Error Handling**: Clear error messages with specific validation failure reasons
- **Modern UI**: Clean, intuitive interface with visual feedback and callout notifications

## How to Run

1. **Prerequisites**: .NET 9.0 SDK
2. **Navigate to the example**:
   ```bash
   cd ibannet-2
   ```
3. **Restore dependencies**:
   ```bash
   dotnet restore
   ```
4. **Run the application**:
   ```bash
   dotnet watch
   ```
5. **Open your browser** to the URL shown in the terminal (typically `http://localhost:5010`)

## How to Deploy

Deploy this example to Ivy's hosting platform:
Deploy this example to Ivy's hosting platform:

1. **Navigate to the example**:
   ```bash
   cd ibannet-2
   ```
2. **Deploy to Ivy hosting**:
   ```bash
   ivy deploy
   ```
This will deploy your IBAN validation application with a single command.

## Learn More

- IbanNet for .NET overview: [github.com/skwasjer/IbanNet](https://github.com/skwasjer/IbanNet)
- Ivy Documentation: [docs.ivy.app](https://docs.ivy.app)