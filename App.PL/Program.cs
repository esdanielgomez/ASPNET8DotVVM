using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
namespace App.PL;

public class Program
{
    public static void Main(string[] args)
    {
        BuildHost(args).Run();
    }

    public static IHost BuildHost(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureLogging((context, builder) =>
            {
                builder.AddConsole();
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            .Build();
}
