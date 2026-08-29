[![](https://img.shields.io/nuget/v/soenneker.extensions.servicecollection.applicationinsights.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.extensions.servicecollection.applicationinsights/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.extensions.servicecollection.applicationinsights/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.extensions.servicecollection.applicationinsights/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.extensions.servicecollection.applicationinsights.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.extensions.servicecollection.applicationinsights/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.extensions.servicecollection.applicationinsights/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.extensions.servicecollection.applicationinsights/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Extensions.ServiceCollection.ApplicationInsights
A collection of helpful IServiceCollection extension methods involving Application Insights.

## Installation

```bash
dotnet add package Soenneker.Extensions.ServiceCollection.ApplicationInsights
```

## Quick start

```csharp
using Soenneker.Extensions.ServiceCollection.ApplicationInsights;

// Given an existing IServiceCollection named services:
services.AddApplicationInsights(config);
```

## Common operations

- `AddApplicationInsights()` - Configures Application Insights telemetry for the specified service collection using the provided configuration settings.
