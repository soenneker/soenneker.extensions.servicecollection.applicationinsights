using Azure.Monitor.OpenTelemetry.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Soenneker.ApplicationInsights.Correlator.Jwt.Registrars;
using Soenneker.ApplicationInsights.Processor.SignalR.Registrars;

namespace Soenneker.Extensions.ServiceCollection.ApplicationInsights;

/// <summary>
/// A collection of helpful IServiceCollection extension methods involving Application Insights
/// </summary>
public static class ServiceCollectionApplicationInsightsExtension
{
    /// <summary>
    /// Configures Application Insights telemetry for the specified service collection using the provided configuration
    /// settings.
    /// </summary>
    /// <remarks>If Application Insights is disabled in the configuration, no telemetry services are
    /// registered. The method supports optional correlation telemetry and SignalR hub telemetry processing based on
    /// configuration values.</remarks>
    /// <param name="services">The service collection to which Application Insights and related telemetry services will be added.</param>
    /// <param name="config">The configuration object containing Application Insights settings, such as connection strings, enablement flags,
    /// and sampling ratios.</param>
    public static void AddApplicationInsights(this IServiceCollection services, IConfiguration config)
    {
        var enabled = config.GetValue<bool>("Azure:AppInsights:Enable");

        if (!enabled)
            return; // nothing to “disable tracing” anymore; just don’t register OTel.

        // Azure Monitor distribution: traces, metrics, and logs wired to Application Insights.
        services.AddOpenTelemetry()
                .UseAzureMonitor(o =>
                {
                    // Option A: set env var APPLICATIONINSIGHTS_CONNECTION_STRING (recommended in Azure),
                    // Option B: set connection string here:
                    o.ConnectionString = config.GetValue<string>("Azure:AppInsights:ConnectionString");

                    // Optional sampling ratio; the distribution default applies when absent or invalid.
                    var samplingRatio = config.GetValue<float?>("Azure:AppInsights:SamplingRatio");
                    if (samplingRatio is >= 0f and <= 1f)
                        o.SamplingRatio = samplingRatio.Value;
                });

        var correlationTelemetry = config.GetValue<bool>("Azure:AppInsights:EnableCorrelationTelemetryInitializer");

        if (correlationTelemetry)
            services.AddJwtTelemetryCorrelatorAsSingleton();

        services.AddSignalRHubTelemetryProcessor();
    }
}
