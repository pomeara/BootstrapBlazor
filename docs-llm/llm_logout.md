# Logout Component

## Overview
The Logout component in BootstrapBlazor provides a convenient way to implement user logout functionality in web applications. It offers a customizable logout button or link that can be easily integrated into navigation bars, user menus, or any part of the application where logout functionality is needed. The component handles the logout process, including session termination, token invalidation, and redirection to login or home pages, simplifying the implementation of secure authentication flows.

## Features
- **Customizable Appearance**: Configure as button, link, or custom element with various styles
- **Flexible Authentication Integration**: Works with various authentication providers and frameworks
- **Confirmation Dialog**: Optional confirmation before logout to prevent accidental logouts
- **Redirect Options**: Configurable redirect behavior after successful logout
- **Event Callbacks**: Events for logout process stages (before, during, after)
- **Token Handling**: Automatic clearing of authentication tokens and cookies
- **Session Management**: Proper termination of user sessions
- **Loading State**: Visual feedback during the logout process
- **Error Handling**: Graceful handling of logout failures
- **Accessibility Support**: Keyboard navigation and screen reader compatibility
- **Localization**: Multi-language support for logout text and messages

## Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `Text` | string | "Logout" | The text displayed on the logout button or link |
| `Icon` | string | "fa-solid fa-sign-out-alt" | The icon to display alongside the text |
| `DisplayType` | DisplayType | DisplayType.Button | How to display the logout control (Button, Link, or Custom) |
| `Color` | Color | Color.Primary | The color scheme for the button (when DisplayType is Button) |
| `Size` | Size | Size.None | The size of the logout button or link |
| `ShowConfirm` | bool | false | Whether to show a confirmation dialog before logging out |
| `ConfirmTitle` | string | "Confirm Logout" | The title of the confirmation dialog |
| `ConfirmText` | string | "Are you sure you want to logout?" | The message in the confirmation dialog |
| `ConfirmButtonText` | string | "Logout" | The text on the confirm button in the dialog |
| `CancelButtonText` | string | "Cancel" | The text on the cancel button in the dialog |
| `RedirectUrl` | string | "/" | The URL to redirect to after successful logout |
| `LogoutUrl` | string | "/api/logout" | The API endpoint for the logout request |
| `UseAjax` | bool | true | Whether to use AJAX for the logout request |
| `Method` | HttpMethod | HttpMethod.Post | The HTTP method to use for the logout request |
| `ClearLocalStorage` | bool | true | Whether to clear local storage on logout |
| `ClearSessionStorage` | bool | true | Whether to clear session storage on logout |
| `ClearCookies` | bool | true | Whether to clear cookies on logout |
| `CookiesToClear` | List<string> | null | Specific cookies to clear (if null, clears authentication cookies) |
| `ShowLoading` | bool | true | Whether to show a loading indicator during logout |
| `LoadingText` | string | "Logging out..." | Text to display during the logout process |
| `DisableOnProcessing` | bool | true | Whether to disable the button during the logout process |
| `Class` | string | null | Additional CSS class for the logout component |
| `ChildContent` | RenderFragment | null | Custom content for the logout component |

## Events

| Event | Description |
|-------|-------------|
| `OnLogoutClick` | Triggered when the logout button or link is clicked |
| `OnConfirm` | Triggered when the user confirms the logout in the confirmation dialog |
| `OnCancel` | Triggered when the user cancels the logout in the confirmation dialog |
| `OnLogoutStart` | Triggered when the logout process begins |
| `OnLogoutSuccess` | Triggered when the logout process completes successfully |
| `OnLogoutError` | Triggered when an error occurs during the logout process |
| `OnRedirect` | Triggered before redirecting after logout |

## Usage Examples

### Example 1: Basic Logout Button
```razor
<Logout Text="Sign Out" 
        RedirectUrl="/login" 
        OnLogoutSuccess="HandleLogoutSuccess" />

@code {
    private void HandleLogoutSuccess()
    {
        // Additional cleanup or logging can be done here
        Console.WriteLine("User logged out successfully");
    }
}
```

### Example 2: Logout with Confirmation Dialog
```razor
<Logout Text="Logout" 
        Icon="fa-solid fa-power-off" 
        Color="Color.Danger" 
        ShowConfirm="true" 
        ConfirmTitle="Confirm Logout" 
        ConfirmText="Are you sure you want to end your session? Any unsaved changes will be lost." 
        ConfirmButtonText="Yes, Logout" 
        CancelButtonText="No, Stay Logged In" 
        RedirectUrl="/login" />
```

### Example 3: Custom Styled Logout Link in Navigation
```razor
<Nav>
    <NavItem Text="Home" Url="/" />
    <NavItem Text="Dashboard" Url="/dashboard" />
    <NavItem Text="Profile" Url="/profile" />
    <NavItem>
        <Logout DisplayType="DisplayType.Link" 
                Text="Sign Out" 
                Icon="fa-solid fa-sign-out-alt" 
                Class="nav-logout-link" 
                OnLogoutStart="HandleLogoutStart" 
                OnLogoutSuccess="HandleLogoutSuccess" />
    </NavItem>
</Nav>

@code {
    private void HandleLogoutStart()
    {
        // Save any user state or perform cleanup before logout
    }
    
    private void HandleLogoutSuccess()
    {
        // Additional actions after successful logout
    }
}

<style>
    .nav-logout-link {
        color: #dc3545;
    }
    
    .nav-logout-link:hover {
        color: #bd2130;
        text-decoration: none;
    }
</style>
```

### Example 4: Logout with Custom API Integration
```razor
@inject IJSRuntime JSRuntime
@inject NavigationManager NavigationManager

<Logout Text="Logout" 
        LogoutUrl="/api/auth/logout" 
        Method="HttpMethod.Post" 
        UseAjax="true" 
        ClearLocalStorage="true" 
        ClearSessionStorage="true" 
        ClearCookies="true" 
        CookiesToClear="new List<string> { "auth_token", "refresh_token", "user_session" }" 
        OnLogoutStart="HandleLogoutStart" 
        OnLogoutSuccess="HandleLogoutSuccess" 
        OnLogoutError="HandleLogoutError" />

@code {
    private async Task HandleLogoutStart()
    {
        // Perform any pre-logout actions
        await JSRuntime.InvokeVoidAsync("console.log", "Starting logout process");
    }
    
    private async Task HandleLogoutSuccess()
    {
        // Custom post-logout actions
        await JSRuntime.InvokeVoidAsync("console.log", "Logout successful");
        
        // Custom redirect instead of using the component's built-in redirect
        NavigationManager.NavigateTo("/login?loggedout=true");
    }
    
    private async Task HandleLogoutError(Exception ex)
    {
        // Handle logout errors
        await JSRuntime.InvokeVoidAsync("console.error", $"Logout error: {ex.Message}");
        
        // Fallback logout approach
        await JSRuntime.InvokeVoidAsync("localStorage.clear");
        await JSRuntime.InvokeVoidAsync("sessionStorage.clear");
        NavigationManager.NavigateTo("/login?error=true");
    }
}
```

### Example 5: Logout Button with Custom Content and Loading State
```razor
<Logout ShowLoading="true" 
        LoadingText="Signing out..." 
        DisableOnProcessing="true" 
        RedirectUrl="/login">
    <ChildContent>
        <div class="custom-logout-button">
            <i class="fa-solid fa-power-off me-2"></i>
            <span>End Session</span>
            <small class="ms-2">(Logged in as @Username)</small>
        </div>
    </ChildContent>
</Logout>

@code {
    [Parameter]
    public string Username { get; set; } = "User";
}

<style>
    .custom-logout-button {
        display: flex;
        align-items: center;
        padding: 8px 16px;
        background-color: #f8f9fa;
        border: 1px solid #dee2e6;
        border-radius: 4px;
        cursor: pointer;
        transition: all 0.2s ease;
    }
    
    .custom-logout-button:hover {
        background-color: #e9ecef;
    }
    
    .custom-logout-button small {
        color: #6c757d;
        font-size: 0.75rem;
    }
</style>
```

### Example 6: Logout Integration with Authentication State Provider
```razor
@inject AuthenticationStateProvider AuthStateProvider
@inject IAuthService AuthService

<AuthorizeView>
    <Authorized>
        <div class="user-menu">
            <span class="user-greeting">Hello, @context.User.Identity.Name!</span>
            <Logout Text="Sign Out" 
                    Icon="fa-solid fa-sign-out-alt" 
                    OnLogoutClick="HandleLogoutClick" 
                    OnLogoutSuccess="HandleLogoutSuccess" />
        </div>
    </Authorized>
    <NotAuthorized>
        <a href="/login" class="btn btn-primary">Sign In</a>
    </NotAuthorized>
</AuthorizeView>

@code {
    private async Task HandleLogoutClick()
    {
        // Custom logic before logout
        // For example, saving user preferences or logging activity
        await Task.CompletedTask;
    }
    
    private async Task HandleLogoutSuccess()
    {
        // Notify the authentication state provider about the logout
        if (AuthStateProvider is CustomAuthStateProvider authStateProvider)
        {
            await authStateProvider.MarkUserAsLoggedOut();
        }
        
        // Additional service calls if needed
        await AuthService.ClearUserData();
    }
}

<style>
    .user-menu {
        display: flex;
        align-items: center;
        gap: 1rem;
    }
    
    .user-greeting {
        font-weight: 500;
    }
</style>
```

### Example 7: Responsive Logout Component in Header
```razor
<header class="app-header">
    <div class="logo">
        <img src="/images/logo.svg" alt="Company Logo" />
        <span class="company-name">Company Name</span>
    </div>
    
    <div class="spacer"></div>
    
    <div class="user-section">
        <div class="user-avatar">
            <img src="@UserAvatarUrl" alt="User Avatar" />
        </div>
        
        <div class="user-info d-none d-md-block">
            <div class="user-name">@UserName</div>
            <div class="user-role">@UserRole</div>
        </div>
        
        <div class="logout-container">
            <!-- Desktop version with text -->            
            <div class="d-none d-md-block">
                <Logout Text="Logout" 
                        Icon="fa-solid fa-sign-out-alt" 
                        Size="Size.Small" 
                        ShowConfirm="true" />
            </div>
            
            <!-- Mobile version with icon only -->            
            <div class="d-block d-md-none">
                <Logout Text="" 
                        Icon="fa-solid fa-sign-out-alt" 
                        DisplayType="DisplayType.Button" 
                        Size="Size.Small" 
                        ShowConfirm="true" />
            </div>
        </div>
    </div>
</header>

@code {
    [Parameter]
    public string UserName { get; set; } = "John Doe";
    
    [Parameter]
    public string UserRole { get; set; } = "Administrator";
    
    [Parameter]
    public string UserAvatarUrl { get; set; } = "/images/default-avatar.png";
}

<style>
    .app-header {
        display: flex;
        align-items: center;
        padding: 0.5rem 1rem;
        background-color: #fff;
        box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
    }
    
    .logo {
        display: flex;
        align-items: center;
        gap: 0.5rem;
    }
    
    .logo img {
        height: 32px;
    }
    
    .company-name {
        font-weight: 600;
        font-size: 1.2rem;
    }
    
    .spacer {
        flex: 1;
    }
    
    .user-section {
        display: flex;
        align-items: center;
        gap: 0.75rem;
    }
    
    .user-avatar img {
        width: 40px;
        height: 40px;
        border-radius: 50%;
        object-fit: cover;
    }
    
    .user-info {
        line-height: 1.2;
    }
    
    .user-name {
        font-weight: 500;
    }
    
    .user-role {
        font-size: 0.8rem;
        color: #6c757d;
    }
    
    .logout-container {
        margin-left: 0.5rem;
    }
</style>
```

## CSS Customization

The Logout component can be customized using CSS variables and classes:

```css
/* Custom Logout styling */
.bb-logout {
    --bb-logout-text-color: inherit;
    --bb-logout-icon-margin: 0.5rem;
    --bb-logout-transition-duration: 0.2s;
    --bb-logout-disabled-opacity: 0.65;
    
    color: var(--bb-logout-text-color);
    transition: all var(--bb-logout-transition-duration) ease;
}

/* Icon styling */
.bb-logout-icon {
    margin-right: var(--bb-logout-icon-margin);
}

/* Link styling */
.bb-logout-link {
    text-decoration: none;
    cursor: pointer;
}

.bb-logout-link:hover {
    text-decoration: underline;
}

/* Button styling */
.bb-logout-button {
    /* Uses Bootstrap button styling by default */
}

/* Loading state */
.bb-logout-loading {
    opacity: var(--bb-logout-disabled-opacity);
    pointer-events: none;
}

/* Custom styling for confirmation dialog */
.bb-logout-confirm-dialog .modal-header {
    border-bottom-color: #dee2e6;
}

.bb-logout-confirm-dialog .modal-footer {
    border-top-color: #dee2e6;
}

.bb-logout-confirm-dialog .confirm-button {
    background-color: #dc3545;
    border-color: #dc3545;
}

.bb-logout-confirm-dialog .confirm-button:hover {
    background-color: #c82333;
    border-color: #bd2130;
}
```

## JavaScript Interop

The Logout component uses JavaScript interop for certain features:

```javascript
// This is a simplified example of the JavaScript interop used by the component
window.bootstrapBlazorLogout = {
    // Clear browser storage
    clearStorage: function(options) {
        if (options.clearLocalStorage) {
            localStorage.clear();
        }
        
        if (options.clearSessionStorage) {
            sessionStorage.clear();
        }
        
        return true;
    },
    
    // Clear specific cookies
    clearCookies: function(cookies) {
        if (!cookies || cookies.length === 0) {
            // Clear all cookies
            document.cookie.split(';').forEach(function(c) {
                document.cookie = c.trim().split('=')[0] + '=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;';
            });
        } else {
            // Clear specific cookies
            cookies.forEach(function(cookieName) {
                document.cookie = cookieName + '=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;';
            });
        }
        
        return true;
    },
    
    // Perform AJAX logout request
    logoutAjax: function(url, method, onSuccess, onError) {
        const xhr = new XMLHttpRequest();
        xhr.open(method, url, true);
        xhr.setRequestHeader('Content-Type', 'application/json');
        
        xhr.onload = function() {
            if (xhr.status >= 200 && xhr.status < 300) {
                onSuccess();
            } else {
                onError('Logout request failed: ' + xhr.statusText);
            }
        };
        
        xhr.onerror = function() {
            onError('Network error during logout');
        };
        
        xhr.send();
    },
    
    // Redirect to specified URL
    redirect: function(url) {
        window.location.href = url;
    }
};
```

## Accessibility

The Logout component follows accessibility best practices:

- Uses semantic HTML elements for buttons and links
- Provides proper ARIA attributes for interactive elements
- Ensures keyboard navigation support
- Maintains focus management during the logout process
- Provides clear visual feedback for loading and error states

## Browser Compatibility

The Logout component is compatible with all modern browsers, including:

- Chrome
- Firefox
- Safari
- Edge

## Integration with Other Components

The Logout component works well with many other BootstrapBlazor components:

- Use with `Nav` or `Menu` components for navigation integration
- Combine with `Dropdown` for user menu functionality
- Use with `Modal` for custom confirmation dialogs
- Integrate with `Toast` or `Message` components for logout notifications
- Use with authentication-related components for complete auth flows