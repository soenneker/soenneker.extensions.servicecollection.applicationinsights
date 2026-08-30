[![](https://img.shields.io/nuget/v/soenneker.extensions.servicecollection.applicationinsights.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.extensions.servicecollection.applicationinsights/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.extensions.servicecollection.applicationinsights/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.extensions.servicecollection.applicationinsights/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.extensions.servicecollection.applicationinsights.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.extensions.servicecollection.applicationinsights/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.extensions.servicecollection.applicationinsights/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.extensions.servicecollection.applicationinsights/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Extensions.ServiceCollection.ApplicationInsights
`IServiceCollection` extensions for registering Application Insights, Azure Monitor OpenTelemetry, correlation, and related telemetry services.

## Installation

```bash
dotnet add package Soenneker.Extensions.ServiceCollection.ApplicationInsights
```

## Usage

```csharp
using Soenneker.Extensions.ServiceCollection.ApplicationInsights;

services.AddApplicationInsights(configuration);
```

Registration is controlled by these keys:

| Key | Effect |
| --- | --- |
| `Azure:AppInsights:Enable` | When false or missing, the method registers nothing. |
| `Azure:AppInsights:ConnectionString` | Assigned to the Azure Monitor exporter; the standard environment variable can also supply it. |
| `Azure:AppInsights:SamplingRatio` | Applied only when the value is between `0` and `1`, inclusive; otherwise the Azure Monitor default remains in effect. |
| `Azure:AppInsights:EnableCorrelationTelemetryInitializer` | Adds the JWT telemetry correlator when true. |

When enabled, the SignalR hub telemetry processor is always registered. The method configures traces, metrics, and logs through the Azure Monitor OpenTelemetry distribution.

Call the extension once during service registration, before building the service provider. It does not create a second service provider or start a background host itself. When disabled, it also skips the optional correlator and SignalR processor.

The connection string can be omitted from application configuration when `APPLICATIONINSIGHTS_CONNECTION_STRING` is supplied through the environment. Do not commit either form of the connection string to source control.

Traces, metrics, and logs can contain request metadata, correlation identifiers, and application log properties that leave the process for Azure Monitor. Review enrichment, JWT correlation, sampling, access, and retention settings before enabling telemetry for sensitive workloads.
