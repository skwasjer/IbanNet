﻿#if VERIFY_PUBLIC_API
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using PublicApiGenerator;

namespace TestHelpers.Specs;

#if !NET6_0_OR_GREATER && !NET47_OR_GREATER
[UsesVerify]
#endif
public abstract class PublicApiSpec
{
    private readonly Type _assemblyMarkerType;
    private readonly string? _sourceFile;

    private readonly ApiGeneratorOptions _options = new()
    {
        ExcludeAttributes =
        [
            typeof(InternalsVisibleToAttribute).FullName!
        ],
        DenyNamespacePrefixes = []
    };

    protected PublicApiSpec(Type assemblyMarkerType, [CallerFilePath] string? sourceFile = default)
    {
        _assemblyMarkerType = assemblyMarkerType;
        _sourceFile = sourceFile;
    }

    [Fact]
    [Trait("Category", "PublicApi")]
    public Task Api_has_not_changed()
    {
        bool runningOnMono = Type.GetType("Mono.Runtime") != null;
        if (runningOnMono)
        {
            return Task.CompletedTask;
        }

        Assembly sut = _assemblyMarkerType.Assembly;
        var settings = new VerifySettings();

        string sutTargetFramework = GetTargetFramework(sut);
        string testTargetFramework = GetTargetFramework(GetType().Assembly);
        settings.UseFileName(
            sutTargetFramework == testTargetFramework
                ? sutTargetFramework
                : $"{sutTargetFramework}_via_{testTargetFramework}"
        );
        settings.UseDirectory("PublicApi");

        // Act
        string publicApi = sut.GeneratePublicApi(_options);

        // Assert
        // ReSharper disable once ExplicitCallerInfoArgument
#pragma warning disable S3236
        return Verify(publicApi, settings, _sourceFile!);
#pragma warning restore S3236
    }

    private static string GetTargetFramework(Assembly asm)
    {
        return asm.GetCustomAttribute<TargetFrameworkAttribute>()?.FrameworkDisplayName?.Replace(' ', '_')
         ?? throw new InvalidOperationException("Framework display name is required.");
    }
}
#else
namespace TestHelpers.Specs;

// Empty shim for when verification is disabled.
public abstract class PublicApiSpec
{
    // ReSharper disable once UnusedParameter.Local
    protected PublicApiSpec(Type assemblyMarkerType)
    {
    }
}
#endif
