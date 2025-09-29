using System.Reflection;
using System.Runtime.Versioning;

namespace TestHelpers;

public static class VerifyHelpers
{
    public static VerifySettings GetDefaultSettings(Type assemblyMarkerType)
    {
        Assembly sut = assemblyMarkerType.Assembly;
        var settings = new VerifySettings();

        settings.AddExtraSettings(s =>
        {
            s.Converters.Add(new VerifyPatternJsonConverter());
            s.Converters.Add(new VerifyPatternTokenJsonConverter());
        });

        settings.DontScrubDateTimes();

        string sutTargetFramework = GetTargetFramework(sut);
        string testTargetFramework = GetTargetFramework(typeof(VerifyHelpers).Assembly);
        settings.UseFileName(
            sutTargetFramework == testTargetFramework
                ? sutTargetFramework
                : $"{sutTargetFramework}_via_{testTargetFramework}"
        );
        return settings;
    }

    private static string GetTargetFramework(Assembly asm)
    {
        return asm.GetCustomAttribute<TargetFrameworkAttribute>()?.FrameworkDisplayName?.Replace(' ', '_')
         ?? throw new InvalidOperationException("Framework display name is required.");
    }
}
