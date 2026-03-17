using Serilog.Events;

namespace LibraryCheckout.Api.Core.Logging;

public static class SerilogExtensions
{
    public static ConfigureHostBuilder UseConfiguredSerilog(this ConfigureHostBuilder hostBuilder)
    {
        hostBuilder.UseSerilog((context, services, loggerConfiguration) =>
        {
            var commitSha = context.Configuration["Build:CommitSha"]
                ?? Environment.GetEnvironmentVariable("GIT_COMMIT_SHA")
                ?? "unknown";

            loggerConfiguration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services)
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithEnvironmentName()
                .Enrich.WithProperty("Application", context.HostingEnvironment.ApplicationName)
                .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                .Enrich.WithProperty("CommitSha", commitSha)
                .WriteTo.Console();
        });

        return hostBuilder;
    }
}
