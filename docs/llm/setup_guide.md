# BootstrapBlazor Project Setup and Service Installation Guide

This guide provides instructions on how to set up a new project using the BootstrapBlazor library and install the necessary services. For the most up-to-date and comprehensive information, always refer to the official [BootstrapBlazor GitHub repository](https://github.com/dotnetcore/BootstrapBlazor).

## Prerequisites

- .NET SDK (version compatible with BootstrapBlazor, typically .NET 6+)
- An IDE like Visual Studio or VS Code

## Creating a New Blazor Project

1.  **Using .NET CLI:**
    Open your terminal or command prompt and run the following command to create a new Blazor Server project:
    ```bash
    dotnet new blazorserver -o MyBootstrapBlazorApp
    cd MyBootstrapBlazorApp
    ```
    Or, for a Blazor WebAssembly project:
    ```bash
    dotnet new blazorwasm -o MyBootstrapBlazorApp
    cd MyBootstrapBlazorApp
    ```

2.  **Using Visual Studio:**
    - Open Visual Studio.
    - Click on "Create a new project".
    - Search for "Blazor App" and select it.
    - Configure your project name and location.
    - Choose the appropriate Blazor hosting model (Server or WebAssembly).
    - Select the target .NET framework.
    - Click "Create".

## Installing BootstrapBlazor NuGet Package

Once your project is created, you need to install the BootstrapBlazor NuGet package.

1.  **Using .NET CLI:**
    Navigate to your project's root directory in the terminal and run:
    ```bash
    dotnet add package BootstrapBlazor
    ```

2.  **Using Visual Studio NuGet Package Manager:**
    - Right-click on your project in the Solution Explorer.
    - Select "Manage NuGet Packages...".
    - Go to the "Browse" tab and search for "BootstrapBlazor".
    - Select the package and click "Install".

## Configuring Services

After installing the package, you need to configure the necessary services in your application.

### 1. Register BootstrapBlazor Services

Open your `Program.cs` file (or `Startup.cs` for older .NET versions) and add the BootstrapBlazor services.

**For Blazor Server (`Program.cs` in .NET 6+):**
```csharp
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Add BootstrapBlazor services
builder.Services.AddBootstrapBlazor();

// ... other services

var app = builder.Build();

// ... middleware configuration

app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
```

**For Blazor WebAssembly (`Program.cs` in .NET 6+):**
```csharp
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MyBootstrapBlazorApp; // Your project's namespace

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Add BootstrapBlazor services
builder.Services.AddBootstrapBlazor();

await builder.Build().RunAsync();
```

### 2. Add Necessary Usings

In your `_Imports.razor` file, add the following using statements:

```csharp
@using BootstrapBlazor.Components
@using Microsoft.AspNetCore.Components.Web // If not already present
```

### 3. Include CSS and JavaScript Files

Reference the BootstrapBlazor CSS and JavaScript files in your main HTML layout file.

**For Blazor Server (`_Host.cshtml` or `_Layout.cshtml`):**

Ensure you have a link to the BootstrapBlazor CSS. Typically, this is placed in the `<head>` section:
```html
<head>
    <!-- ... other head elements ... -->
    <link href="_content/BootstrapBlazor/css/bootstrap.blazor.bundle.min.css" rel="stylesheet">
    <!-- If you are using a custom theme, include it here -->
    <!-- <link href="_content/BootstrapBlazor/css/motronic.min.css" rel="stylesheet"> -->
</head>
```

And the JavaScript file, usually before the closing `</body>` tag:
```html
<body>
    <!-- ... body content ... -->
    <script src="_content/BootstrapBlazor/js/bootstrap.blazor.bundle.min.js"></script>
</body>
```

**For Blazor WebAssembly (`wwwroot/index.html`):**

In the `<head>` section:
```html
<head>
    <!-- ... other head elements ... -->
    <link href="_content/BootstrapBlazor/css/bootstrap.blazor.bundle.min.css" rel="stylesheet">
    <!-- If you are using a custom theme, include it here -->
    <!-- <link href="_content/BootstrapBlazor/css/motronic.min.css" rel="stylesheet"> -->
</head>
```

And before the closing `</body>` tag:
```html
<body>
    <!-- ... body content ... -->
    <script src="_content/BootstrapBlazor/js/bootstrap.blazor.bundle.min.js"></script>
</body>
```

## Using BootstrapBlazor Components

Once configured, you can start using BootstrapBlazor components in your Razor files. For example, to use a Button component:

```razor
<Button Color="Color.Primary">Click Me</Button>
```

## Further Information

For detailed information on specific components, advanced configurations, theming, and localization, please refer to:

-   **Official Documentation**: [https://www.blazor.zone/](https://www.blazor.zone/)
-   **GitHub Repository**: [https://github.com/dotnetcore/BootstrapBlazor](https://github.com/dotnetcore/BootstrapBlazor)

This setup guide provides the foundational steps. The official resources will offer more in-depth knowledge and updates as the library evolves.