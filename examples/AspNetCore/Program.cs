using Microsoft.AspNetCore;

namespace AspNetCoreExample;

public static class Program
{
    public static void Main(string[] args)
    {
        BuildWebHost(args).Run();
    }

    private static IWebHost BuildWebHost(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .Build();
}
