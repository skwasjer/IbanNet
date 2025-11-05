using Microsoft.CodeAnalysis;

namespace IbanNet.CodeGen.Extensions;

internal static class SourceProductionContextExtensions
{
    internal static void ReportWarning
    (
        this SourceProductionContext ctx,
        string errorCode,
        string title,
        string message,
        string category,
        Location? location = null
    )
    {
        ctx.ReportDiagnostic(
            Diagnostic.Create(
                new DiagnosticDescriptor(
                    errorCode,
                    title,
                    message,
                    category,
                    DiagnosticSeverity.Warning,
                    true
                ),
                location ?? Location.None
            )
        );
    }
}
