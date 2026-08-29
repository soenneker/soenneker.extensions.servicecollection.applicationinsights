[![](https://img.shields.io/nuget/v/soenneker.extensions.servicecollection.applicationinsights.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.extensions.servicecollection.applicationinsights/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.extensions.servicecollection.applicationinsights/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.extensions.servicecollection.applicationinsights/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.extensions.servicecollection.applicationinsights.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.extensions.servicecollection.applicationinsights/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.extensions.servicecollection.applicationinsights/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.extensions.servicecollection.applicationinsights/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Extensions.ServiceCollection.ApplicationInsights
Registers Azure Monitor OpenTelemetry for Application Insights, plus Soenneker correlation and SignalR telemetry components.

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
| `Azure:AppInsights:SamplingRatio` | Applied only when the value is between `0` and `1`, inclusive. |
| `Azure:AppInsights:EnableCorrelationTelemetryInitializer` | Adds the JWT telemetry correlator when true. |

When enabled, the SignalR hub telemetry processor is always registered. The method configures traces, metrics, and logs through the Azure Monitor OpenTelemetry distribution.
