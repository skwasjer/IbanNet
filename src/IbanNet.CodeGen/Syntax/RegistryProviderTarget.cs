using Microsoft.CodeAnalysis;

namespace IbanNet.CodeGen.Syntax;

internal readonly record struct RegistryProviderTarget
(
    string Namespace,
    string ClassName,
    string MethodName,
    string DataSourceType,
    string InputSourcePath,
    string FullInputSourcePath,
    LocationInfo Location
)
{
    public static bool TryCreate
    (
        ISymbol symbol,
        string dataSourceType,
        string inputSourcePath,
        out RegistryProviderTarget target)
    {
        if (symbol is not IMethodSymbol ms)
        {
            target = default;
            return false;
        }

        Location l = symbol.Locations.First();
        FileLinePositionSpan ls = l.GetLineSpan();
        target = new RegistryProviderTarget(
            ms.ContainingType.ContainingNamespace.ToString(),
            ms.ContainingType.Name,
            ms.Name,
            dataSourceType,
            inputSourcePath,
            Path.Combine(Path.GetDirectoryName(ls.Path)!, inputSourcePath),
            new LocationInfo(ls.Path, l.SourceSpan, ls.Span));
        return true;
    }
}
