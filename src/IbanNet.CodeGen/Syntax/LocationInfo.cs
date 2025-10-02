using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace IbanNet.CodeGen.Syntax;

internal readonly record struct LocationInfo
(
    string Path,
    TextSpan Text,
    LinePositionSpan LinePosition
)
{
    public static implicit operator Location(LocationInfo li)
    {
        return Location.Create(li.Path, li.Text, li.LinePosition);
    }
}
