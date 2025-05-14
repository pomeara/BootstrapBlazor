# Redirect Component

## Overview

The Redirect component in BootstrapBlazor provides a declarative way to navigate between different routes or pages in a Blazor application. It allows developers to perform programmatic navigation without writing JavaScript or complex C# code. This component is particularly useful for scenarios such as authentication redirects, workflow completions, or any situation where automatic navigation is required based on certain conditions.

## Features

- **Declarative Navigation**: Redirect to different routes using a simple component declaration
- **Conditional Redirects**: Perform redirects based on conditions or states
- **Delay Support**: Option to delay the redirect by a specified time period
- **Parameter Passing**: Ability to pass query parameters and route values to the target page
- **Navigation History Control**: Options to replace the current history entry or add a new one
- **External URL Support**: Redirect to external URLs outside of the application
- **Event Callbacks**: Events for monitoring redirect status and completion

## Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `Url` | string | null | The target URL or route to redirect to. This can be a relative path within the application or an absolute URL. |
| `IsExternal` | bool | false | Indicates whether the URL is external to the application. When true, the component will use window.location for navigation instead of the Blazor navigation manager. |
| `DelaySeconds` | int | 0 | The number of seconds to wait before performing the redirect. A value of 0 means immediate redirect. |
| `ReplaceHistoryEntry` | bool | false | When true, replaces the current history entry instead of adding a new one. Useful for login redirects where you don't want the back button to return to the login page. |
| `ForceLoad` | bool | false | When true, forces the browser to load the new page from the server rather than performing a client-side navigation. |
| `QueryParameters` | Dictionary<string, string> | null | A collection of query parameters to append to the URL. |
| `Condition` | bool | true | A condition that determines whether the redirect should occur. The redirect only happens when this is true. |
| `PreventInitialRedirect` | bool | false | When true, prevents the redirect from occurring when the component is first rendered. |

## Events

| Event | Description |
|-------|-------------|
| `OnRedirecting` | Triggered immediately before the redirect occurs. Can be used to perform cleanup or save state. |
| `OnRedirected` | Triggered after the redirect has been initiated. Note that for immediate redirects, component code after this may not execute. |
| `OnRedirectCancelled` | Triggered when a redirect is cancelled due to the Condition property evaluating to false. |
| `OnDelayTick` | Triggered each second during a delayed redirect, providing the remaining seconds count. |

## Usage Examples

### Example 1: Basic Redirect

A simple redirect to another page in the application:

```razor
<Redirect Url="/dashboard" />
```

### Example 2: Delayed Redirect with Countdown

Redirect after a 5-second countdown, displaying the remaining time to the user:

```razor
@page "/operation-complete"

<div class="alert alert-success">
    <h4>Operation Completed Successfully!</h4>
    <p>You will be redirected to the dashboard in @_remainingSeconds seconds...</p>
</div>

<Redirect Url="/dashboard" 
         DelaySeconds="5" 
         OnDelayTick="HandleDelayTick" />

@code {
    private int _remainingSeconds = 5;
    
    private void HandleDelayTick(int remainingSeconds)
    {
        _remainingSeconds = remainingSeconds;
        StateHasChanged();
    }
}
```

### Example 3: Conditional Redirect Based on Authentication

Redirect to the login page if the user is not authenticated:

```razor
@inject AuthenticationStateProvider AuthStateProvider

<Redirect Url="/login" 
         Condition="@(!_isAuthenticated)" 
         ReplaceHistoryEntry="true" />

@code {
    private bool _isAuthenticated;
    
    protected override async Task OnInitializedAsync()
    {
        var authState = await AuthStateProvider.GetAuthenticationStateAsync();
        _isAuthenticated = authState.User.Identity?.IsAuthenticated ?? false;
    }
}
```

### Example 4: Redirect with Query Parameters

Redirect to a details page with specific query parameters:

```razor
@page "/product/redirect/{id:int}"

@code {
    [Parameter]
    public int Id { get; set; }
    
    private Dictionary<string, string> _queryParams;
    
    protected override void OnInitialized()
    {
        _queryParams = new Dictionary<string, string>
        {
            { "view", "detailed" },
            { "source", "redirect" }
        };
    }
}

<Redirect Url="@($"/product/details/{Id}")" 
         QueryParameters="_queryParams" />
```

### Example 5: External URL Redirect

Redirect to an external website:

```razor
<Redirect Url="https://www.example.com" 
         IsExternal="true" 
         DelaySeconds="3" />

<div class="alert alert-info">
    <p>You are being redirected to an external website in a few seconds...</p>
</div>
```

### Example 6: Programmatically Controlled Redirect

Redirect that can be triggered by a button click:

```razor
<div class="card">
    <div class="card-body">
        <h5 class="card-title">Confirm Action</h5>
        <p class="card-text">Are you sure you want to proceed?</p>
        
        <Button Color="Color.Primary" OnClick="Confirm">Yes, Proceed</Button>
        <Button Color="Color.Secondary" OnClick="Cancel">Cancel</Button>
    </div>
</div>

<Redirect Url="/confirmation" 
         Condition="@_shouldRedirect" 
         PreventInitialRedirect="true" />

@code {
    private bool _shouldRedirect = false;
    
    private void Confirm()
    {
        _shouldRedirect = true;
    }
    
    private void Cancel()
    {
        // Navigate elsewhere using NavigationManager
    }
}
```

### Example 7: Multi-Step Form with Redirect on Completion

Redirect after completing a multi-step form process:

```razor
@page "/registration/complete"

<div class="container">
    <div class="progress mb-4">
        <div class="progress-bar" style="width: 100%">Step 3 of 3</div>
    </div>
    
    <div class="alert alert-success">
        <h4>Registration Complete!</h4>
        <p>Your account has been successfully created. You will be redirected to your new dashboard shortly.</p>
    </div>
    
    <Redirect Url="/dashboard/new-user" 
             DelaySeconds="5" 
             OnRedirecting="SaveUserPreferences" />
</div>

@code {
    private async Task SaveUserPreferences()
    {
        // Save any final user preferences before redirecting
        await UserService.SavePreferences();
    }
}
```

## CSS Customization

The Redirect component itself doesn't render any visible UI elements by default, so there are no specific CSS classes to customize. However, you can combine it with other UI components to create custom redirect experiences with visual feedback.

## JavaScript Interop

The Redirect component uses Blazor's NavigationManager internally for client-side navigation. For external URLs, it may use JavaScript interop to call `window.location.href` or similar browser navigation APIs.

## Accessibility Considerations

When using the Redirect component, especially with delays, consider the following accessibility best practices:

1. Always provide clear information about the pending redirect
2. For delayed redirects, show a visible countdown
3. Provide a way for users to cancel automatic redirects when appropriate
4. Ensure that screen readers announce redirect information

## Browser Compatibility

The Redirect component is compatible with all modern browsers that support Blazor WebAssembly or Blazor Server. There are no specific browser compatibility issues to be aware of.

## Integration with Other Components

The Redirect component works well with:

- **Alert/Toast Components**: To provide visual feedback before redirecting
- **Authentication Components**: For login/logout flows
- **Form Components**: To navigate after form submission
- **Workflow Components**: To move between steps in a process

## Best Practices

1. Use `ReplaceHistoryEntry="true"` for login redirects to prevent users from navigating back to the login page after authentication
2. Provide visual feedback for delayed redirects
3. Consider using `ForceLoad="true"` when navigating to pages that need fresh server data
4. Use conditional redirects with caution to avoid redirect loops