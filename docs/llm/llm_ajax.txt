# Ajax Component

## Overview
The Ajax component in BootstrapBlazor provides a convenient way to make asynchronous HTTP requests directly from Blazor components without writing JavaScript interop code. It encapsulates the functionality for sending various types of HTTP requests (GET, POST, PUT, DELETE, etc.) and handling responses, making it easier to interact with web APIs and services. The component supports features like request parameters, headers, timeout configuration, and response handling.

## Key Features
- **Multiple HTTP Methods**: Support for GET, POST, PUT, DELETE, PATCH, HEAD, and OPTIONS
- **Request Configuration**: Customizable URL, parameters, headers, and content type
- **Response Handling**: Built-in handling for different response types (JSON, text, blob)
- **Error Management**: Comprehensive error handling and status code checking
- **Timeout Control**: Configurable request timeout
- **Loading Indicator**: Optional loading state during request processing
- **Cancellation Support**: Ability to cancel ongoing requests
- **File Upload**: Support for file uploads with progress tracking
- **Authentication**: Built-in support for authentication headers
- **Retry Mechanism**: Optional automatic retry for failed requests
- **Event Callbacks**: Events for request lifecycle (before, success, error, complete)

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `Url` | `string` | `null` | The URL to send the request to |
| `Method` | `HttpMethod` | `HttpMethod.Get` | The HTTP method to use (GET, POST, PUT, DELETE, etc.) |
| `Parameters` | `Dictionary<string, object>` | `null` | Request parameters to be sent |
| `Headers` | `Dictionary<string, string>` | `null` | HTTP headers to include with the request |
| `ContentType` | `string` | `"application/json"` | Content type of the request |
| `Timeout` | `int` | `30000` | Request timeout in milliseconds |
| `WithCredentials` | `bool` | `false` | Whether to include credentials with cross-origin requests |
| `ResponseType` | `ResponseType` | `ResponseType.Json` | Expected response type (Json, Text, Blob, etc.) |
| `ShowLoading` | `bool` | `false` | Whether to show a loading indicator during the request |
| `EnableErrorNotification` | `bool` | `true` | Whether to show error notifications for failed requests |
| `RetryTimes` | `int` | `0` | Number of times to retry a failed request |
| `RetryInterval` | `int` | `1000` | Interval between retries in milliseconds |

## Events

| Event | Description |
| --- | --- |
| `OnBeforeSend` | Triggered before the request is sent, allows for request modification |
| `OnSuccess` | Triggered when the request is successful |
| `OnError` | Triggered when the request fails |
| `OnComplete` | Triggered when the request completes (success or failure) |
| `OnProgress` | Triggered during file upload to report progress |

## Usage Examples

### Example 1: Basic GET Request
```razor
@using BootstrapBlazor.Components

<div class="mb-3">
    <Button Color="Color.Primary" OnClick="FetchData">Fetch Data</Button>
</div>

@if (data != null)
{
    <div class="mt-3">
        <pre>@data</pre>
    </div>
}

<Ajax @ref="ajaxRef" />

@code {
    private Ajax? ajaxRef;
    private string? data;
    
    private async Task FetchData()
    {
        if (ajaxRef != null)
        {
            var result = await ajaxRef.GetAsync("https://api.example.com/data");
            if (result.Success)
            {
                data = System.Text.Json.JsonSerializer.Serialize(result.Data, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
            }
        }
    }
}
```

### Example 2: POST Request with Parameters
```razor
@using BootstrapBlazor.Components

<div class="mb-3">
    <div class="row g-3">
        <div class="col-md-6">
            <BootstrapInput @bind-Value="user.Name" DisplayText="Name" />
        </div>
        <div class="col-md-6">
            <BootstrapInput @bind-Value="user.Email" DisplayText="Email" />
        </div>
    </div>
    <div class="mt-3">
        <Button Color="Color.Primary" OnClick="SubmitData">Submit</Button>
    </div>
</div>

<Ajax @ref="ajaxRef" 
      OnSuccess="HandleSuccess" 
      OnError="HandleError" 
      ShowLoading="true" />

@code {
    private Ajax? ajaxRef;
    private UserModel user = new();
    
    private async Task SubmitData()
    {
        if (ajaxRef != null)
        {
            var parameters = new Dictionary<string, object>
            {
                ["name"] = user.Name,
                ["email"] = user.Email
            };
            
            await ajaxRef.PostAsync("https://api.example.com/users", parameters);
        }
    }
    
    private Task HandleSuccess(AjaxResult result)
    {
        // Handle successful response
        ToastService.Success("User created successfully");
        return Task.CompletedTask;
    }
    
    private Task HandleError(AjaxResult result)
    {
        // Handle error response
        ToastService.Error($"Error: {result.Error}");
        return Task.CompletedTask;
    }
    
    private class UserModel
    {
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
    }
}
```

### Example 3: File Upload with Progress
```razor
@using BootstrapBlazor.Components
@inject ToastService ToastService

<div class="mb-3">
    <h3>File Upload Example</h3>
    <div class="mb-3">
        <InputFile OnChange="HandleFileSelected" />
    </div>
    <div class="mb-3">
        <Button Color="Color.Primary" OnClick="UploadFile" Disabled="@(selectedFile == null)">Upload</Button>
    </div>
    
    @if (uploadProgress > 0 && uploadProgress < 100)
    {
        <div class="mt-3">
            <Progress Value="uploadProgress" ShowLabel="true" />
        </div>
    }
</div>

<Ajax @ref="ajaxRef" 
      OnProgress="HandleProgress" 
      OnSuccess="HandleUploadSuccess" 
      OnError="HandleUploadError" />

@code {
    private Ajax? ajaxRef;
    private IBrowserFile? selectedFile;
    private int uploadProgress;
    
    private void HandleFileSelected(InputFileChangeEventArgs e)
    {
        selectedFile = e.File;
        uploadProgress = 0;
    }
    
    private async Task UploadFile()
    {
        if (ajaxRef != null && selectedFile != null)
        {
            var formData = new MultipartFormDataContent();
            var fileContent = new StreamContent(selectedFile.OpenReadStream(maxAllowedSize: 10485760)); // 10MB max
            formData.Add(fileContent, "file", selectedFile.Name);
            
            await ajaxRef.PostAsync("https://api.example.com/upload", formData);
        }
    }
    
    private Task HandleProgress(ProgressEventArgs args)
    {
        uploadProgress = args.Percentage;
        StateHasChanged();
        return Task.CompletedTask;
    }
    
    private Task HandleUploadSuccess(AjaxResult result)
    {
        uploadProgress = 100;
        ToastService.Success("File uploaded successfully");
        return Task.CompletedTask;
    }
    
    private Task HandleUploadError(AjaxResult result)
    {
        uploadProgress = 0;
        ToastService.Error($"Upload failed: {result.Error}");
        return Task.CompletedTask;
    }
}
```

### Example 4: Request with Custom Headers and Timeout
```razor
@using BootstrapBlazor.Components

<div class="mb-3">
    <Button Color="Color.Primary" OnClick="FetchWithHeaders">Fetch with Custom Headers</Button>
</div>

<Ajax @ref="ajaxRef" 
      Headers="customHeaders" 
      Timeout="5000" 
      OnSuccess="HandleSuccess" 
      OnError="HandleError" 
      OnComplete="HandleComplete" />

@code {
    private Ajax? ajaxRef;
    private Dictionary<string, string> customHeaders = new()
    {
        ["X-API-Key"] = "your-api-key-here",
        ["Accept-Language"] = "en-US",
        ["Custom-Header"] = "custom-value"
    };
    private bool isLoading;
    
    private async Task FetchWithHeaders()
    {
        if (ajaxRef != null)
        {
            isLoading = true;
            await ajaxRef.GetAsync("https://api.example.com/protected-data");
        }
    }
    
    private Task HandleSuccess(AjaxResult result)
    {
        // Process successful response
        return Task.CompletedTask;
    }
    
    private Task HandleError(AjaxResult result)
    {
        if (result.Status == 408) // Timeout
        {
            ToastService.Warning("Request timed out. Please try again.");
        }
        else
        {
            ToastService.Error($"Error: {result.Error}");
        }
        return Task.CompletedTask;
    }
    
    private Task HandleComplete(AjaxResult result)
    {
        isLoading = false;
        return Task.CompletedTask;
    }
}
```

### Example 5: Cancellable Request
```razor
@using BootstrapBlazor.Components
@using System.Threading

<div class="mb-3">
    <div class="d-flex gap-2">
        <Button Color="Color.Primary" OnClick="StartLongRequest" Disabled="@isLoading">Start Long Request</Button>
        <Button Color="Color.Danger" OnClick="CancelRequest" Disabled="@(!isLoading)">Cancel Request</Button>
    </div>
</div>

<div class="mt-3">
    <p>Status: @status</p>
</div>

<Ajax @ref="ajaxRef" 
      OnSuccess="HandleSuccess" 
      OnError="HandleError" 
      OnComplete="HandleComplete" />

@code {
    private Ajax? ajaxRef;
    private bool isLoading;
    private string status = "Ready";
    private CancellationTokenSource? cts;
    
    private async Task StartLongRequest()
    {
        if (ajaxRef != null && !isLoading)
        {
            isLoading = true;
            status = "Processing...";
            
            cts = new CancellationTokenSource();
            try
            {
                await ajaxRef.GetAsync("https://api.example.com/long-operation", cancellationToken: cts.Token);
            }
            catch (OperationCanceledException)
            {
                status = "Request cancelled";
                isLoading = false;
            }
        }
    }
    
    private void CancelRequest()
    {
        cts?.Cancel();
        status = "Cancelling...";
    }
    
    private Task HandleSuccess(AjaxResult result)
    {
        status = "Request completed successfully";
        return Task.CompletedTask;
    }
    
    private Task HandleError(AjaxResult result)
    {
        status = $"Error: {result.Error}";
        return Task.CompletedTask;
    }
    
    private Task HandleComplete(AjaxResult result)
    {
        isLoading = false;
        return Task.CompletedTask;
    }
}
```

### Example 6: Retry Mechanism for Failed Requests
```razor
@using BootstrapBlazor.Components

<div class="mb-3">
    <Button Color="Color.Primary" OnClick="FetchWithRetry">Fetch with Retry</Button>
</div>

<div class="mt-3">
    <p>Status: @status</p>
    <p>Attempt: @attempt of @maxRetries</p>
</div>

<Ajax @ref="ajaxRef" 
      RetryTimes="3" 
      RetryInterval="2000" 
      OnBeforeSend="HandleBeforeSend" 
      OnSuccess="HandleSuccess" 
      OnError="HandleError" />

@code {
    private Ajax? ajaxRef;
    private string status = "Ready";
    private int attempt = 0;
    private int maxRetries = 3;
    
    private async Task FetchWithRetry()
    {
        if (ajaxRef != null)
        {
            attempt = 0;
            status = "Starting request...";
            await ajaxRef.GetAsync("https://api.example.com/unreliable-endpoint");
        }
    }
    
    private Task HandleBeforeSend()
    {
        attempt++;
        status = $"Sending request (Attempt {attempt})...";
        return Task.CompletedTask;
    }
    
    private Task HandleSuccess(AjaxResult result)
    {
        status = "Request succeeded!";
        return Task.CompletedTask;
    }
    
    private Task HandleError(AjaxResult result)
    {
        if (attempt <= maxRetries)
        {
            status = $"Attempt {attempt} failed. Retrying in 2 seconds...";
        }
        else
        {
            status = "All retry attempts failed.";
        }
        return Task.CompletedTask;
    }
}
```

### Example 7: Using Ajax Service for Global Configuration
```razor
@page "/ajax-service-demo"
@using BootstrapBlazor.Components
@inject AjaxService AjaxService
@inject ToastService ToastService

<h3>Ajax Service Example</h3>

<div class="mb-3">
    <div class="d-flex gap-2">
        <Button Color="Color.Primary" OnClick="GetUsers">Get Users</Button>
        <Button Color="Color.Success" OnClick="GetProducts">Get Products</Button>
        <Button Color="Color.Info" OnClick="GetOrders">Get Orders</Button>
    </div>
</div>

<div class="mt-3">
    @if (!string.IsNullOrEmpty(resultData))
    {
        <pre>@resultData</pre>
    }
</div>

@code {
    private string? resultData;
    
    protected override void OnInitialized()
    {
        // Configure global Ajax settings
        AjaxService.Configure(options =>
        {
            options.BaseUrl = "https://api.example.com";
            options.DefaultHeaders = new Dictionary<string, string>
            {
                ["Authorization"] = "Bearer your-token-here",
                ["Accept"] = "application/json"
            };
            options.Timeout = 10000;
            options.EnableErrorNotification = true;
        });
    }
    
    private async Task GetUsers()
    {
        try
        {
            // Uses the base URL from configuration
            var result = await AjaxService.GetAsync("/users");
            if (result.Success)
            {
                resultData = System.Text.Json.JsonSerializer.Serialize(result.Data, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
            }
        }
        catch (Exception ex)
        {
            ToastService.Error($"Error: {ex.Message}");
        }
    }
    
    private async Task GetProducts()
    {
        try
        {
            // Override timeout for this specific request
            var result = await AjaxService.GetAsync("/products", new AjaxOption { Timeout = 5000 });
            if (result.Success)
            {
                resultData = System.Text.Json.JsonSerializer.Serialize(result.Data, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
            }
        }
        catch (Exception ex)
        {
            ToastService.Error($"Error: {ex.Message}");
        }
    }
    
    private async Task GetOrders()
    {
        try
        {
            // Add request-specific headers
            var options = new AjaxOption
            {
                Headers = new Dictionary<string, string>
                {
                    ["X-Custom-Header"] = "custom-value"
                }
            };
            
            var result = await AjaxService.GetAsync("/orders", options);
            if (result.Success)
            {
                resultData = System.Text.Json.JsonSerializer.Serialize(result.Data, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
            }
        }
        catch (Exception ex)
        {
            ToastService.Error($"Error: {ex.Message}");
        }
    }
}
```

## CSS Customization

The Ajax component itself doesn't have specific CSS customization options as it's primarily a non-visual component. However, you can customize the loading indicator that appears when `ShowLoading` is set to `true`:

```css
/* Customize the loading indicator */
.bb-loading {
  --bb-loading-background: rgba(0, 0, 0, 0.5);
  --bb-loading-color: #fff;
  --bb-loading-font-size: 1rem;
  --bb-loading-border-radius: 0.25rem;
  --bb-loading-z-index: 1050;
}

.bb-loading-progress {
  --bb-loading-progress-size: 3rem;
  --bb-loading-progress-border-width: 0.25rem;
  --bb-loading-progress-border-color: rgba(255, 255, 255, 0.25);
  --bb-loading-progress-border-left-color: #fff;
}
```

## Notes

### Ajax Service Integration
- The Ajax component works with the `AjaxService` to provide a centralized way to configure and manage HTTP requests.
- You can inject the `AjaxService` into any component to make HTTP requests with global configuration:

```csharp
@inject AjaxService AjaxService

private async Task FetchData()
{
    var result = await AjaxService.GetAsync("https://api.example.com/data");
    // Process result
}
```

### Error Handling
- The Ajax component provides comprehensive error handling through the `OnError` event.
- You can also enable automatic error notifications with `EnableErrorNotification`.
- Common HTTP errors (400, 401, 403, 404, 500) are handled automatically and can be customized.

### Security Considerations
- Always validate and sanitize data received from Ajax requests before displaying or processing it.
- Use HTTPS for all Ajax requests to ensure data security.
- Be cautious with CORS (Cross-Origin Resource Sharing) settings when making requests to different domains.
- Consider implementing CSRF (Cross-Site Request Forgery) protection for sensitive operations.

### Performance Considerations
- Use the `Timeout` property to set appropriate timeouts for different types of requests.
- Consider implementing request caching for frequently accessed data.
- Use the `CancellationToken` parameter to cancel long-running requests when necessary.

### Integration with Other Components
- The Ajax component works well with other BootstrapBlazor components like Button, Form, and Toast.
- Use the `ShowLoading` property to display a loading indicator during requests.
- Combine with the Toast component to display success and error messages.

### Best Practices
- Use the `AjaxService` for application-wide Ajax configuration.
- Implement proper error handling for all requests.
- Use strongly-typed models for request and response data.
- Consider implementing a retry mechanism for unreliable endpoints.
- Use the appropriate HTTP method for each operation (GET for retrieval, POST for creation, etc.).
- Implement proper loading states to improve user experience during requests.