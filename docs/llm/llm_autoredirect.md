# AutoRedirect Component

## Overview
The AutoRedirect component in BootstrapBlazor provides automatic navigation functionality within a Blazor application. It allows developers to implement automatic redirects after a specified delay, which is useful for scenarios such as redirecting users after successful form submissions, displaying temporary messages before navigation, implementing session timeouts, or creating guided user flows. This component enhances user experience by automating navigation without requiring manual user interaction.

## Key Features
- **Timed Redirects**: Automatically redirects users after a configurable delay
- **Multiple Navigation Methods**: Supports both relative and absolute URL navigation
- **Conditional Redirection**: Can be enabled or disabled based on conditions
- **Countdown Display**: Optional countdown timer to show time remaining before redirect
- **Navigation Events**: Events for navigation start and completion
- **Cancellation Support**: Ability to cancel pending redirects
- **Integration with Blazor Router**: Seamless integration with Blazor's navigation system
- **Query Parameter Support**: Ability to include query parameters in the redirect URL
- **Fragment Support**: Support for URL fragments (hash navigation)
- **State Preservation**: Option to preserve or replace browser history state

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `Url` | `string` | `null` | The URL to redirect to (can be relative or absolute) |
| `Delay` | `int` | `3000` | Delay in milliseconds before the redirect occurs |
| `Enabled` | `bool` | `true` | Whether the automatic redirect is enabled |
| `ShowCountdown` | `bool` | `false` | Whether to display a countdown timer |
| `CountdownFormat` | `string` | `"Redirecting in {0} seconds..."` | Format string for the countdown display |
| `ForceLoad` | `bool` | `false` | Whether to force a page reload instead of client-side navigation |
| `Replace` | `bool` | `false` | Whether to replace the current history entry instead of adding a new one |
| `QueryParameters` | `Dictionary<string, object>` | `null` | Query parameters to include in the redirect URL |
| `Fragment` | `string` | `null` | URL fragment (hash) to include in the redirect URL |
| `ChildContent` | `RenderFragment` | `null` | Content to display while waiting for the redirect |

## Events

| Event | Description |
| --- | --- |
| `OnRedirectStart` | Triggered when the redirect countdown begins |
| `OnRedirectComplete` | Triggered when the redirect has been executed |
| `OnRedirectCancelled` | Triggered when the redirect is cancelled |

## Usage Examples

### Example 1: Basic Redirect After Delay
```razor
@page "/redirect-demo"
@using BootstrapBlazor.Components
@inject NavigationManager NavigationManager

<div class="card">
    <div class="card-header">Auto Redirect Demo</div>
    <div class="card-body">
        <p>You will be redirected to the home page in 5 seconds.</p>
        
        <AutoRedirect Url="/" Delay="5000" />
    </div>
</div>
```

### Example 2: Redirect with Countdown Display
```razor
@page "/redirect-with-countdown"
@using BootstrapBlazor.Components
@inject NavigationManager NavigationManager

<div class="card">
    <div class="card-header">Auto Redirect with Countdown</div>
    <div class="card-body">
        <p>This page demonstrates an automatic redirect with a countdown display.</p>
        
        <AutoRedirect Url="/" 
                      Delay="10000" 
                      ShowCountdown="true" 
                      CountdownFormat="Redirecting to home page in {0} seconds..." />
        
        <div class="mt-3">
            <Button Color="Color.Secondary" OnClick="CancelRedirect">Cancel Redirect</Button>
        </div>
    </div>
</div>

@code {
    private AutoRedirect? autoRedirectRef;
    
    private void CancelRedirect()
    {
        autoRedirectRef?.Cancel();
    }
}
```

### Example 3: Conditional Redirect After Form Submission
```razor
@page "/contact-form"
@using BootstrapBlazor.Components
@inject NavigationManager NavigationManager

<div class="card">
    <div class="card-header">Contact Form</div>
    <div class="card-body">
        <ValidateForm Model="@formModel" OnValidSubmit="HandleValidSubmit">
            <div class="mb-3">
                <BootstrapInput @bind-Value="@formModel.Name" DisplayText="Name" placeholder="Enter your name" />
            </div>
            
            <div class="mb-3">
                <BootstrapInput @bind-Value="@formModel.Email" DisplayText="Email" placeholder="Enter your email" />
            </div>
            
            <div class="mb-3">
                <Textarea @bind-Value="@formModel.Message" DisplayText="Message" placeholder="Enter your message" rows="5" />
            </div>
            
            <Button ButtonType="ButtonType.Submit" Color="Color.Primary">Submit</Button>
        </ValidateForm>
        
        @if (formSubmitted)
        {
            <Alert Color="Color.Success" ShowDismiss="false" class="mt-3">
                <p>Thank you for your message! You will be redirected to the confirmation page shortly.</p>
                
                <AutoRedirect @ref="autoRedirectRef"
                              Url="/contact-confirmation"
                              Delay="3000"
                              ShowCountdown="true"
                              OnRedirectComplete="HandleRedirectComplete" />
            </Alert>
        }
    </div>
</div>

@code {
    private ContactFormModel formModel = new();
    private bool formSubmitted = false;
    private AutoRedirect? autoRedirectRef;
    
    private void HandleValidSubmit()
    {
        // Process form submission
        formSubmitted = true;
    }
    
    private void HandleRedirectComplete()
    {
        Console.WriteLine("Redirect completed");
    }
    
    private class ContactFormModel
    {
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Message { get; set; } = "";
    }
}
```

### Example 4: Redirect with Query Parameters and Fragment
```razor
@page "/redirect-with-params"
@using BootstrapBlazor.Components
@inject NavigationManager NavigationManager

<div class="card">
    <div class="card-header">Redirect with Parameters</div>
    <div class="card-body">
        <p>You will be redirected to the search page with parameters in 5 seconds.</p>
        
        <AutoRedirect Url="/search"
                      Delay="5000"
                      QueryParameters="@queryParams"
                      Fragment="results"
                      ShowCountdown="true" />
    </div>
</div>

@code {
    private Dictionary<string, object> queryParams = new()
    {
        ["q"] = "blazor components",
        ["category"] = "documentation",
        ["page"] = 1
    };
}
```

### Example 5: Session Timeout with Auto Redirect
```razor
@page "/session-timeout"
@using BootstrapBlazor.Components
@using System.Timers
@implements IDisposable
@inject NavigationManager NavigationManager

<div class="card">
    <div class="card-header">Session Timeout Demo</div>
    <div class="card-body">
        <p>This page demonstrates a session timeout with automatic redirect.</p>
        <p>Current session status: <strong>@sessionStatus</strong></p>
        <p>Time remaining: <strong>@timeRemaining seconds</strong></p>
        
        <div class="mb-3">
            <Button Color="Color.Primary" OnClick="ResetSession">Reset Session</Button>
        </div>
        
        @if (showRedirect)
        {
            <Alert Color="Color.Warning">
                <p>Your session is about to expire. You will be redirected to the login page.</p>
                
                <AutoRedirect @ref="autoRedirectRef"
                              Url="/login"
                              Delay="10000"
                              ShowCountdown="true"
                              CountdownFormat="Redirecting in {0} seconds..."
                              OnRedirectStart="HandleRedirectStart"
                              OnRedirectComplete="HandleRedirectComplete" />
                
                <div class="mt-2">
                    <Button Color="Color.Secondary" OnClick="CancelRedirect">Stay Logged In</Button>
                </div>
            </Alert>
        }
    </div>
</div>

@code {
    private string sessionStatus = "Active";
    private int sessionDuration = 30; // seconds
    private int timeRemaining;
    private bool showRedirect = false;
    private Timer? sessionTimer;
    private AutoRedirect? autoRedirectRef;
    
    protected override void OnInitialized()
    {
        timeRemaining = sessionDuration;
        
        sessionTimer = new Timer(1000);
        sessionTimer.Elapsed += OnTimerElapsed;
        sessionTimer.Start();
    }
    
    private void OnTimerElapsed(object? sender, ElapsedEventArgs e)
    {
        timeRemaining--;
        
        if (timeRemaining <= 10 && !showRedirect)
        {
            showRedirect = true;
            sessionStatus = "Expiring";
        }
        
        if (timeRemaining <= 0)
        {
            sessionTimer?.Stop();
            sessionStatus = "Expired";
        }
        
        InvokeAsync(StateHasChanged);
    }
    
    private void ResetSession()
    {
        timeRemaining = sessionDuration;
        showRedirect = false;
        sessionStatus = "Active";
        autoRedirectRef?.Cancel();
    }
    
    private void CancelRedirect()
    {
        ResetSession();
    }
    
    private void HandleRedirectStart()
    {
        Console.WriteLine("Redirect countdown started");
    }
    
    private void HandleRedirectComplete()
    {
        Console.WriteLine("Redirect completed");
    }
    
    public void Dispose()
    {
        sessionTimer?.Stop();
        sessionTimer?.Dispose();
    }
}
```

### Example 6: Multi-Step Form with Auto Navigation
```razor
@page "/multi-step-form/{Step:int}"
@using BootstrapBlazor.Components
@inject NavigationManager NavigationManager

<div class="card">
    <div class="card-header">Multi-Step Form - Step @Step of 3</div>
    <div class="card-body">
        @switch (Step)
        {
            case 1:
                <div class="step-content">
                    <h4>Step 1: Personal Information</h4>
                    <div class="mb-3">
                        <BootstrapInput @bind-Value="@formData.Name" DisplayText="Name" placeholder="Enter your name" />
                    </div>
                    <div class="mb-3">
                        <BootstrapInput @bind-Value="@formData.Email" DisplayText="Email" placeholder="Enter your email" />
                    </div>
                    <Button Color="Color.Primary" OnClick="() => CompleteStep(1)">Next</Button>
                </div>
                break;
                
            case 2:
                <div class="step-content">
                    <h4>Step 2: Address Information</h4>
                    <div class="mb-3">
                        <BootstrapInput @bind-Value="@formData.Address" DisplayText="Address" placeholder="Enter your address" />
                    </div>
                    <div class="mb-3">
                        <BootstrapInput @bind-Value="@formData.City" DisplayText="City" placeholder="Enter your city" />
                    </div>
                    <div class="d-flex gap-2">
                        <Button Color="Color.Secondary" OnClick="() => NavigateToStep(1)">Previous</Button>
                        <Button Color="Color.Primary" OnClick="() => CompleteStep(2)">Next</Button>
                    </div>
                </div>
                break;
                
            case 3:
                <div class="step-content">
                    <h4>Step 3: Review & Submit</h4>
                    <div class="mb-3">
                        <p><strong>Name:</strong> @formData.Name</p>
                        <p><strong>Email:</strong> @formData.Email</p>
                        <p><strong>Address:</strong> @formData.Address</p>
                        <p><strong>City:</strong> @formData.City</p>
                    </div>
                    <div class="d-flex gap-2">
                        <Button Color="Color.Secondary" OnClick="() => NavigateToStep(2)">Previous</Button>
                        <Button Color="Color.Success" OnClick="SubmitForm">Submit</Button>
                    </div>
                </div>
                break;
        }
        
        @if (formSubmitted)
        {
            <Alert Color="Color.Success" ShowDismiss="false" class="mt-3">
                <p>Form submitted successfully! You will be redirected to the confirmation page.</p>
                
                <AutoRedirect Url="/form-confirmation"
                              Delay="3000"
                              ShowCountdown="true" />
            </Alert>
        }
    </div>
</div>

@code {
    [Parameter]
    public int Step { get; set; } = 1;
    
    private FormData formData = new();
    private bool formSubmitted = false;
    
    private void CompleteStep(int currentStep)
    {
        // Validate current step data
        NavigateToStep(currentStep + 1);
    }
    
    private void NavigateToStep(int step)
    {
        NavigationManager.NavigateTo($"/multi-step-form/{step}");
    }
    
    private void SubmitForm()
    {
        // Process form submission
        formSubmitted = true;
    }
    
    private class FormData
    {
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Address { get; set; } = "";
        public string City { get; set; } = "";
    }
}
```

### Example 7: Maintenance Mode Redirect
```razor
@page "/maintenance-check"
@using BootstrapBlazor.Components
@inject NavigationManager NavigationManager
@inject HttpClient Http

<div class="card">
    <div class="card-header">Maintenance Check</div>
    <div class="card-body">
        @if (isLoading)
        {
            <div class="text-center">
                <Spinner />
                <p>Checking system status...</p>
            </div>
        }
        else if (isInMaintenance)
        {
            <Alert Color="Color.Warning">
                <h4>System Maintenance</h4>
                <p>The system is currently undergoing scheduled maintenance.</p>
                <p>Estimated completion time: @maintenanceEndTime</p>
                
                <AutoRedirect Url="/maintenance"
                              Delay="5000"
                              ShowCountdown="true"
                              CountdownFormat="Redirecting to maintenance page in {0} seconds..." />
            </Alert>
        }
        else
        {
            <Alert Color="Color.Success">
                <h4>System Available</h4>
                <p>All systems are operational. You will be redirected to the dashboard.</p>
                
                <AutoRedirect Url="/dashboard"
                              Delay="3000"
                              ShowCountdown="true" />
            </Alert>
        }
    </div>
</div>

@code {
    private bool isLoading = true;
    private bool isInMaintenance = false;
    private string maintenanceEndTime = "";
    
    protected override async Task OnInitializedAsync()
    {
        await Task.Delay(1500); // Simulate API call
        
        // Simulate checking maintenance status
        // In a real app, this would be an API call
        Random random = new Random();
        isInMaintenance = random.Next(2) == 0; // 50% chance of maintenance mode
        
        if (isInMaintenance)
        {
            // Calculate a future time for maintenance end
            maintenanceEndTime = DateTime.Now.AddHours(2).ToString("hh:mm tt");
        }
        
        isLoading = false;
    }
}
```

## CSS Customization

The AutoRedirect component itself doesn't have specific visual elements to style, but you can customize the countdown display using CSS:

```css
/* Styling for the countdown display */
.bb-auto-redirect-countdown {
  font-weight: bold;
  color: #dc3545;
  margin-top: 0.5rem;
  animation: pulse 1s infinite;
}

@keyframes pulse {
  0% {
    opacity: 1;
  }
  50% {
    opacity: 0.7;
  }
  100% {
    opacity: 1;
  }
}

/* Styling for the container */
.bb-auto-redirect {
  padding: 0.5rem;
  border-radius: 0.25rem;
  background-color: rgba(255, 243, 205, 0.5);
}
```

## Notes

### Navigation Behavior
- By default, the AutoRedirect component uses Blazor's client-side navigation, which preserves the application state.
- When `ForceLoad` is set to `true`, the component performs a full page reload, which resets the application state.
- The `Replace` property determines whether the navigation adds a new entry to the browser history or replaces the current one.

### Integration with Blazor Router
- The AutoRedirect component works with Blazor's routing system and can navigate to any route defined in your application.
- For external URLs (starting with "http://" or "https://"), the component will perform a full page navigation.

### Security Considerations
- Be cautious when using AutoRedirect with user-provided URLs to prevent open redirect vulnerabilities.
- Always validate and sanitize URLs before using them with the AutoRedirect component.

### Performance Considerations
- The countdown timer uses JavaScript interop, which may have a small performance impact.
- For very short delays (less than 1 second), consider disabling the countdown display.

### Accessibility
- When using AutoRedirect, ensure that users have enough time to read any important information before being redirected.
- Consider providing a way to cancel or delay the redirect for users who need more time.
- The countdown display helps users understand when the redirect will occur, improving the user experience.

### Best Practices
- Use AutoRedirect sparingly and only when it enhances the user experience.
- Always provide clear information about where the user will be redirected to.
- Consider using the `OnRedirectStart` event to log navigation for analytics purposes.
- For critical operations, provide a way to cancel the redirect.
- Use appropriate delay times based on the context (longer for important messages, shorter for simple confirmations).