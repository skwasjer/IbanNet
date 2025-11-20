using System.Resources;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("IbanNet.Tests")]
[assembly: InternalsVisibleTo("IbanNet.Benchmark")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
#if DEBUG
[assembly: InternalsVisibleTo("IbanNet.CodeGen")]
#endif
[assembly: NeutralResourcesLanguage("en")]
[assembly: CLSCompliant(true)]
