using System.Globalization;
using System.Reflection;

namespace TestHelpers.Specs;

public abstract class LocalizationSpecBase
{
    protected abstract Type ResourceType { get; }

    public virtual void Resource_should_be_localized(string cultureCode, string key, string expectedMessage)
    {
        using var _ = new CultureScope(new CultureInfo(cultureCode));

        ResourceType
            .GetProperty(key, BindingFlags.NonPublic | BindingFlags.Static)
            .Should()
            .NotBeNull()
            .And.BeAssignableTo<PropertyInfo>()
            .Which.GetValue(null)
            .Should()
            .Be(expectedMessage);
    }
}
