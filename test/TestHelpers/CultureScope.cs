using System.Globalization;

namespace TestHelpers;

public sealed record CultureScope : IDisposable
{
    private readonly CultureInfo _oldCulture;
    private readonly bool _useUiCulture;

    public CultureScope(CultureInfo culture, bool uiCulture = true)
    {
        _useUiCulture = uiCulture;
        _oldCulture = uiCulture ? CultureInfo.CurrentUICulture : CultureInfo.CurrentCulture;
        SetCulture(culture);
    }

    public void Dispose()
    {
        SetCulture(_oldCulture);
    }

    private void SetCulture(CultureInfo newCulture)
    {
        if (_useUiCulture)
        {
            CultureInfo.CurrentUICulture = newCulture;
        }
        else
        {
            CultureInfo.CurrentCulture = newCulture;
        }
    }
}
