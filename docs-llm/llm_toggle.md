# Toggle Component

## Overview
The Toggle component in BootstrapBlazor provides a user-friendly interface for switching between two states. It offers a more visually appealing and intuitive alternative to traditional checkboxes, allowing users to toggle features on or off, enable or disable settings, or switch between two mutually exclusive options. The Toggle component is ideal for binary choices where the visual representation of the state is important.

## Features
- **Binary State Control**: Easily toggle between two states (on/off, true/false)
- **Customizable Appearance**: Options for size, color, and styling
- **Label Support**: Optional labels for both states
- **Icon Integration**: Can display icons for visual reinforcement
- **Disabled State**: Support for disabled/read-only mode
- **Animation Effects**: Smooth transition animations
- **Two-way Binding**: Automatic synchronization with bound values
- **Event Callbacks**: Notifications when state changes
- **Keyboard Accessibility**: Support for keyboard navigation and control

## Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `Value` | bool | false | The current state of the toggle (true = on, false = off) |
| `OnText` | string | "On" | Text displayed when toggle is in the on state |
| `OffText` | string | "Off" | Text displayed when toggle is in the off state |
| `OnColor` | Color | Color.Success | Color of the toggle when in the on state |
| `OffColor` | Color | Color.Secondary | Color of the toggle when in the off state |
| `OnIcon` | string | null | Icon displayed when toggle is in the on state |
| `OffIcon` | string | null | Icon displayed when toggle is in the off state |
| `Size` | Size | Size.Medium | Size of the toggle (Small, Medium, Large) |
| `ShowText` | bool | true | When true, displays the text labels |
| `TextPlacement` | Placement | Placement.Right | Position of the text labels (Left, Right, Top, Bottom) |
| `IsDisabled` | bool | false | When true, the toggle cannot be interacted with |
| `IsReadOnly` | bool | false | When true, the toggle state cannot be changed |
| `AnimationDuration` | int | 200 | Duration of the toggle animation in milliseconds |
| `Width` | int | 0 | Width of the toggle in pixels (0 = auto) |
| `Height` | int | 0 | Height of the toggle in pixels (0 = auto) |

## Events

| Event | Description |
|-------|-------------|
| `OnValueChanged` | Triggered when the toggle state changes |
| `OnValueChanging` | Triggered before the toggle state changes, can be canceled |

## Usage Examples

### Example 1: Basic Toggle

```html
<Toggle @bind-Value="isEnabled" />

<div class="mt-3">
    <p>Current state: @(isEnabled ? "Enabled" : "Disabled")</p>
</div>
```

```csharp
@code {
    private bool isEnabled = false;
}
```

### Example 2: Custom Text and Colors

```html
<Toggle @bind-Value="darkMode"
        OnText="Dark"
        OffText="Light"
        OnColor="Color.Dark"
        OffColor="Color.Light"
        OnValueChanged="HandleThemeChange" />

<div class="mt-3">
    <p>Current theme: @(darkMode ? "Dark mode" : "Light mode")</p>
</div>
```

```csharp
@code {
    private bool darkMode = false;
    
    private void HandleThemeChange(bool value)
    {
        // Apply theme change logic here
        Console.WriteLine($"Theme changed to: {(value ? "Dark" : "Light")}");
    }
}
```

### Example 3: Toggle with Icons

```html
<Toggle @bind-Value="notificationsEnabled"
        OnText="Enabled"
        OffText="Disabled"
        OnIcon="fa-solid fa-bell"
        OffIcon="fa-solid fa-bell-slash"
        OnColor="Color.Warning" />

<div class="mt-3">
    <p>Notifications: @(notificationsEnabled ? "You will receive notifications" : "Notifications are turned off")</p>
</div>
```

```csharp
@code {
    private bool notificationsEnabled = true;
}
```

### Example 4: Different Sizes

```html
<div class="d-flex flex-column gap-3">
    <div>
        <span class="me-2">Small:</span>
        <Toggle @bind-Value="toggleSmall" Size="Size.Small" />
    </div>
    
    <div>
        <span class="me-2">Medium:</span>
        <Toggle @bind-Value="toggleMedium" Size="Size.Medium" />
    </div>
    
    <div>
        <span class="me-2">Large:</span>
        <Toggle @bind-Value="toggleLarge" Size="Size.Large" />
    </div>
</div>
```

```csharp
@code {
    private bool toggleSmall = false;
    private bool toggleMedium = false;
    private bool toggleLarge = false;
}
```

### Example 5: Disabled and Read-Only States

```html
<div class="d-flex flex-column gap-3">
    <div>
        <span class="me-2">Normal:</span>
        <Toggle @bind-Value="normalToggle" />
    </div>
    
    <div>
        <span class="me-2">Disabled:</span>
        <Toggle @bind-Value="disabledToggle" IsDisabled="true" />
    </div>
    
    <div>
        <span class="me-2">Read-only:</span>
        <Toggle @bind-Value="readOnlyToggle" IsReadOnly="true" />
    </div>
</div>
```

```csharp
@code {
    private bool normalToggle = true;
    private bool disabledToggle = true;
    private bool readOnlyToggle = true;
}
```

### Example 6: Toggle in a Form

```html
<Form Model="@formModel">
    <div class="row">
        <div class="col-md-6">
            <FormItem Label="Username" ShowLabel="true">
                <Input @bind-Value="@formModel.Username" />
            </FormItem>
        </div>
        
        <div class="col-md-6">
            <FormItem Label="Email" ShowLabel="true">
                <Input @bind-Value="@formModel.Email" />
            </FormItem>
        </div>
    </div>
    
    <div class="row mt-3">
        <div class="col-md-6">
            <FormItem Label="Subscribe to newsletter" ShowLabel="true">
                <Toggle @bind-Value="@formModel.SubscribeNewsletter"
                        OnText="Yes"
                        OffText="No" />
            </FormItem>
        </div>
        
        <div class="col-md-6">
            <FormItem Label="Receive notifications" ShowLabel="true">
                <Toggle @bind-Value="@formModel.ReceiveNotifications"
                        OnText="Yes"
                        OffText="No" />
            </FormItem>
        </div>
    </div>
    
    <div class="mt-3">
        <Button Text="Submit" OnClick="HandleSubmit" />
    </div>
</Form>

<div class="mt-3" @if="showFormData">
    <h5>Form Data:</h5>
    <pre>@formDataJson</pre>
</div>
```

```csharp
@code {
    private UserPreferences formModel = new UserPreferences();
    private bool showFormData = false;
    private string formDataJson = "";
    
    private void HandleSubmit()
    {
        formDataJson = System.Text.Json.JsonSerializer.Serialize(formModel, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
        showFormData = true;
    }
    
    private class UserPreferences
    {
        public string Username { get; set; } = "";
        public string Email { get; set; } = "";
        public bool SubscribeNewsletter { get; set; } = false;
        public bool ReceiveNotifications { get; set; } = true;
    }
}
```

### Example 7: Multiple Related Toggles

```html
<div class="card">
    <div class="card-header">
        <h5>Application Settings</h5>
    </div>
    <div class="card-body">
        <div class="setting-group">
            <div class="setting-item d-flex justify-content-between align-items-center mb-3">
                <div>
                    <h6 class="mb-0">Dark Mode</h6>
                    <small class="text-muted">Switch between light and dark theme</small>
                </div>
                <Toggle @bind-Value="settings.DarkMode"
                        OnText="On"
                        OffText="Off"
                        OnColor="Color.Dark"
                        OffColor="Color.Light"
                        OnValueChanged="@(v => UpdateSettings("DarkMode", v))" />
            </div>
            
            <div class="setting-item d-flex justify-content-between align-items-center mb-3">
                <div>
                    <h6 class="mb-0">Notifications</h6>
                    <small class="text-muted">Enable or disable all notifications</small>
                </div>
                <Toggle @bind-Value="settings.Notifications"
                        OnValueChanged="@(v => UpdateSettings("Notifications", v))" />
            </div>
            
            <div class="setting-item d-flex justify-content-between align-items-center mb-3">
                <div>
                    <h6 class="mb-0">Email Notifications</h6>
                    <small class="text-muted">Receive notifications via email</small>
                </div>
                <Toggle @bind-Value="settings.EmailNotifications"
                        IsDisabled="!settings.Notifications"
                        OnValueChanged="@(v => UpdateSettings("EmailNotifications", v))" />
            </div>
            
            <div class="setting-item d-flex justify-content-between align-items-center mb-3">
                <div>
                    <h6 class="mb-0">Push Notifications</h6>
                    <small class="text-muted">Receive push notifications in browser</small>
                </div>
                <Toggle @bind-Value="settings.PushNotifications"
                        IsDisabled="!settings.Notifications"
                        OnValueChanged="@(v => UpdateSettings("PushNotifications", v))" />
            </div>
            
            <div class="setting-item d-flex justify-content-between align-items-center">
                <div>
                    <h6 class="mb-0">Data Saving Mode</h6>
                    <small class="text-muted">Reduce data usage in the application</small>
                </div>
                <Toggle @bind-Value="settings.DataSavingMode"
                        OnValueChanged="@(v => UpdateSettings("DataSavingMode", v))" />
            </div>
        </div>
    </div>
    <div class="card-footer">
        <Button Text="Save Settings" OnClick="SaveSettings" />
        <Button Text="Reset to Defaults" OnClick="ResetSettings" Color="Color.Secondary" />
    </div>
</div>
```

```csharp
@code {
    private AppSettings settings = new AppSettings();
    
    private void UpdateSettings(string setting, bool value)
    {
        Console.WriteLine($"Setting '{setting}' changed to: {value}");
        
        // If main notifications are disabled, ensure sub-notification settings are also disabled
        if (setting == "Notifications" && !value)
        {
            settings.EmailNotifications = false;
            settings.PushNotifications = false;
        }
    }
    
    private void SaveSettings()
    {
        // Save settings logic here
        Console.WriteLine("Settings saved");
    }
    
    private void ResetSettings()
    {
        settings = new AppSettings();
        Console.WriteLine("Settings reset to defaults");
    }
    
    private class AppSettings
    {
        public bool DarkMode { get; set; } = false;
        public bool Notifications { get; set; } = true;
        public bool EmailNotifications { get; set; } = true;
        public bool PushNotifications { get; set; } = false;
        public bool DataSavingMode { get; set; } = false;
    }
}
```

## CSS Customization

The Toggle component can be customized using CSS variables and classes:

```css
/* Custom styles for Toggle component */
.bb-toggle {
    /* Component container */
    display: inline-flex;
    align-items: center;
    cursor: pointer;
}

.bb-toggle-disabled {
    /* Disabled state */
    opacity: 0.6;
    cursor: not-allowed;
}

.bb-toggle-switch {
    /* The actual toggle switch */
    position: relative;
    display: inline-block;
    width: var(--bb-toggle-width, 50px);
    height: var(--bb-toggle-height, 24px);
    border-radius: var(--bb-toggle-border-radius, 12px);
    background-color: var(--bb-toggle-bg-off, #ccc);
    transition: background-color var(--bb-toggle-transition, 0.2s);
}

.bb-toggle-switch.active {
    /* Active state */
    background-color: var(--bb-toggle-bg-on, #28a745);
}

.bb-toggle-handle {
    /* The toggle handle/knob */
    position: absolute;
    top: 2px;
    left: 2px;
    width: calc(var(--bb-toggle-height, 24px) - 4px);
    height: calc(var(--bb-toggle-height, 24px) - 4px);
    border-radius: 50%;
    background-color: white;
    box-shadow: 0 1px 3px rgba(0, 0, 0, 0.2);
    transition: transform var(--bb-toggle-transition, 0.2s);
}

.bb-toggle-switch.active .bb-toggle-handle {
    /* Handle position when active */
    transform: translateX(calc(var(--bb-toggle-width, 50px) - var(--bb-toggle-height, 24px)));
}

.bb-toggle-text {
    /* Text label */
    margin-left: var(--bb-toggle-text-margin, 8px);
    font-size: var(--bb-toggle-text-size, 14px);
}

.bb-toggle-icon {
    /* Icon styles */
    display: inline-flex;
    align-items: center;
    justify-content: center;
    margin-right: var(--bb-toggle-icon-margin, 4px);
}

/* Size variations */
.bb-toggle-sm .bb-toggle-switch {
    width: var(--bb-toggle-sm-width, 40px);
    height: var(--bb-toggle-sm-height, 20px);
}

.bb-toggle-lg .bb-toggle-switch {
    width: var(--bb-toggle-lg-width, 60px);
    height: var(--bb-toggle-lg-height, 30px);
}

/* Animation for the handle */
@keyframes toggleOn {
    0% { transform: translateX(0); }
    50% { transform: translateX(calc(var(--bb-toggle-width, 50px) - var(--bb-toggle-height, 24px) + 3px)); }
    100% { transform: translateX(calc(var(--bb-toggle-width, 50px) - var(--bb-toggle-height, 24px))); }
}

@keyframes toggleOff {
    0% { transform: translateX(calc(var(--bb-toggle-width, 50px) - var(--bb-toggle-height, 24px))); }
    50% { transform: translateX(-3px); }
    100% { transform: translateX(0); }
}

.bb-toggle-animated .bb-toggle-handle {
    animation-duration: var(--bb-toggle-animation-duration, 0.3s);
    animation-fill-mode: forwards;
}

.bb-toggle-animated.active .bb-toggle-handle {
    animation-name: toggleOn;
}

.bb-toggle-animated:not(.active) .bb-toggle-handle {
    animation-name: toggleOff;
}
```

## JavaScript Interop

The Toggle component primarily operates without JavaScript, but for animations and advanced interactions, it uses JavaScript interop. You can extend its functionality by using the following methods:

```csharp
// Toggle the state programmatically
await JSRuntime.InvokeVoidAsync("bootstrapBlazor.toggle.toggle", elementRef);

// Set the state programmatically
await JSRuntime.InvokeVoidAsync("bootstrapBlazor.toggle.setState", elementRef, true);

// Get the current state
var state = await JSRuntime.InvokeAsync<bool>("bootstrapBlazor.toggle.getState", elementRef);

// Enable/disable the toggle
await JSRuntime.InvokeVoidAsync("bootstrapBlazor.toggle.setDisabled", elementRef, true);
```

## Accessibility

The Toggle component is designed with accessibility in mind:

- Uses proper ARIA attributes for screen reader compatibility
- Supports keyboard navigation (Tab to focus, Space to toggle)
- Provides sufficient color contrast for visibility
- Includes focus indicators for keyboard users
- Maintains proper tab order in forms

## Browser Compatibility

The Toggle component is compatible with all modern browsers:

- Chrome
- Firefox
- Edge
- Safari
- Opera

For older browsers, the component includes fallback styling to ensure basic functionality and appearance.

## Integration with Other Components

The Toggle component can be integrated with various other BootstrapBlazor components:

- Use with Form components for form inputs
- Combine with Card or Panel for settings sections
- Integrate with Table for row actions or settings
- Pair with Modal or Dialog for configuration options
- Use with List or ListGroup for feature toggles in lists