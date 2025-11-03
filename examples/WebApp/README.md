# IbanNet WebApp Example

> **Note**: This example requires **.NET 9.0 SDK** or later.

This example demonstrates IBAN (International Bank Account Number) validation and generation using the IbanNet library integrated with [Ivy Framework](https://github.com/Ivy-Interactive/Ivy-Framework).

## What This Application Does

This web application provides an **IBAN Management** tool that allows users to:

- **Validate IBAN Numbers**: Enter any IBAN number to check its validity and structure
- **Generate Test IBANs**: Create valid test IBAN numbers for any supported country
- **View Detailed Analysis**: Get comprehensive breakdown of IBAN structure, country information, and bank details
- **Copy to Clipboard**: Easily copy IBAN numbers and electronic formats for use in other applications
- **Country Support**: Access to 100+ countries with their specific IBAN formats and validation rules
- **Real-time Validation**: Instant feedback with visual indicators (valid/invalid badges)
- **Interactive UI**: Split-panel layout with input controls on the left and detailed results on the right

## Technical Implementation

- Uses IbanNet's `IbanValidator` for robust IBAN validation
- Implements `IbanGenerator` for creating test IBAN numbers
- Leverages `IbanParser` for detailed IBAN structure analysis
- Integrates `SwiftRegistryProvider` and `WikipediaRegistryProvider` for comprehensive country data
- Creates responsive card-based layout with callout notifications
- Handles clipboard operations for easy IBAN copying
- Supports multiple IBAN formats (Electronic, Print, Obfuscated)
- Provides detailed country information including SEPA membership status

## Prerequisites

- .NET 9.0 SDK
- Ivy Framework (automatically restored via NuGet)

## How to Run

1. **Navigate to the example directory**:

   ```bash
   cd examples/WebApp
   ```

2. **Restore dependencies**:

   ```bash
   dotnet restore
   ```

3. **Run the application**:

   ```bash
   dotnet watch
   ```

4. **Open your browser** to the URL shown in the terminal (typically `http://localhost:5010`)

## Docker Deployment

```bash
cd examples/WebApp
docker build -t ibannet-webapp .
docker run -d -p 8080:80 --name ibannet-webapp ibannet-webapp
```

The application will be available at `http://localhost:8080`.

## Key Features

- **Validation Engine**: Checks IBAN format, country code, check digits, and BBAN structure
- **Generation Tool**: Creates valid test IBANs for development and testing purposes
- **Country Registry**: Access to official IBAN specifications for 100+ countries
- **Format Support**: Multiple display formats for different use cases
- **Error Handling**: Clear error messages with specific validation failure reasons
- **Modern UI**: Clean, intuitive interface with visual feedback and callout notifications

## Learn More

- IbanNet Library: [github.com/skwasjer/IbanNet](https://github.com/skwasjer/IbanNet)
- Ivy Framework: [github.com/Ivy-Interactive/Ivy-Framework](https://github.com/Ivy-Interactive/Ivy-Framework)
- Ivy Documentation: [docs.ivy.app](https://docs.ivy.app)
