using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Soenneker.ApplicationInsights.Correlator.Jwt;
using Soenneker.ApplicationInsights.Processor.SignalR;
using Soenneker.Extensions.Configuration;

namespace Soenneker.Extensions.ServiceCollection.ApplicationInsights;

/// <summary>
/// A collection of helpful IServiceCollection extension methods involving Application Insights
/// </summary>
public static class ServiceCollectionApplicationInsightsExtension
{
    /// <summary>
    /// Adds and configures Application Insights telemetry based on the provided <see cref="IConfiguration"/>. 
    /// Also conditionally registers a custom JWT correlator and SignalR telemetry processor.
    /// </summary>
    /// <param name="services">The service collection to which Application Insights services will be added.</param>
    /// <param name="config">The application configuration used to retrieve Application Insights settings.</param>
    /// <remarks>
    /// - Enables telemetry only if <c>Azure:AppInsights:Enable</c> is <c>true</c> in configuration. <para/>
    /// - Sets the connection string from <c>Azure:AppInsights:ConnectionString</c>. <para/>
    /// - Optionally registers a JWT-based telemetry initializer if <c>Azure:AppInsights:EnableCorrelationTelemetryInitializer</c> is <c>true</c>. <para/>
    /// - Adds a custom SignalR telemetry processor. <para/>
    /// - Disables tracing if telemetry is not enabled (due to a known issue: https://github.com/Microsoft/ApplicationInsights-dotnet/issues/310).
    /// </remarks>
    public static void AddApplicationInsights(this IServiceCollection services, IConfiguration config)
    {
        var enabled = config.GetValue<bool>("Azure:AppInsights:Enable");

        if (enabled)
        {
            services.AddApplicationInsightsTelemetry(o =>
            {
                o.ConnectionString = config.GetValueStrict<string>("Azure:AppInsights:ConnectionString");
                // should get rid of trace issues https://github.com/microsoft/ApplicationInsights-dotnet/issues/2070
                o.EnableActiveTelemetryConfigurationSetup = true;
            });

            var correlationTelemetry = config.GetValue<bool>("Azure:AppInsights:EnableCorrelationTelemetryInitializer");

            if (correlationTelemetry)
                services.AddSingleton<ITelemetryInitializer, JwtTelemetryCorrelator>();

            services.AddApplicationInsightsTelemetryProcessor<SignalRTelemetryProcessor>();
        }
        else
        {
            // https://github.com/Microsoft/ApplicationInsights-dotnet/issues/310
            TelemetryDebugWriter.IsTracingDisabled = true;
        }
    }
}
