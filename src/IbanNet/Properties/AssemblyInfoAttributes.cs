﻿using System.Resources;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("IbanNet.Tests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
#if DEBUG
[assembly: InternalsVisibleTo("TemporaryT4Assembly")]
#endif
[assembly: NeutralResourcesLanguage("en")]
[assembly: CLSCompliant(true)]

#if !NET6_0_OR_GREATER
// https://stackoverflow.com/questions/64749385/predefined-type-system-runtime-compilerservices-isexternalinit-is-not-defined
// ReSharper disable once CheckNamespace
namespace System.Runtime.CompilerServices
{
      internal static class IsExternalInit {}
}
#endif
