using IbanNet.CodeGen.Swift;

namespace IbanNet.CodeGen;

internal interface IRegistryDataSource
{
    bool IsDataSource(string path);
    SwiftCsvRecord[] GetCountryDefinitions(string text);
}
