using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Serilog;
using Serilog.Events;

namespace uk.osric.VideoParty {
    public class Program {
        public static void Main(string[] args) {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .Enrich.WithProperty("App Name", "Video Party")
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            try {
                BuildWebHost(args).Run();
                return;
            } catch (Exception ex) {
                Log.Fatal(ex, "WebHost terminated unexpectedly");
                return;
            } finally {
                Log.CloseAndFlush();
            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                   .UseStartup<Startup>()
                   .UseSerilog()
                   .UseKestrel(options => { 
                       options.AddServerHeader = false;
                       options.UseSystemd();
                   })
                   .Build();


    }
}
