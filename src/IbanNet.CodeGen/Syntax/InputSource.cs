namespace IbanNet.CodeGen.Syntax;

internal readonly record struct InputSource
(
    string FullName,
    string Text
)
{
    public string Name { get => Path.GetFileName(FullName); }

    public string RegistryVersion
    {
        get => string.IsNullOrEmpty(Name)
            ? null!
            : Name.Split('.')[1];
    }
}
