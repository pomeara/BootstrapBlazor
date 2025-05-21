# Switch Component Documentation

## Overview
The Switch component in BootstrapBlazor provides a toggle control that allows users to choose between two mutually exclusive states (typically on/off). It offers a more intuitive and visually appealing alternative to checkboxes for binary choices. This component is ideal for enabling or disabling settings, toggling features, or any scenario where users need to make a simple yes/no or true/false selection with immediate visual feedback.

## Features
- Two-state toggle (on/off, true/false)
- Customizable on/off text labels
- Color customization for different states
- Size variants (small, medium, large)
- Disabled state support
- Loading state indication
- Custom icon support
- Two-way data binding
- Form validation integration
- Keyboard accessibility
- Touch-friendly for mobile devices
- Animation effects during state changes
- Event callbacks for state changes

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| Value | bool | false | Gets or sets the switch state (true for on, false for off) |
| ValueChanged | EventCallback<bool> | - | Callback when the switch state changes |
| ValueExpression | Expression<Func<bool>> | - | Expression for the bound value |
| OnText | string | "On" | Text to display when the switch is on |
| OffText | string | "Off" | Text to display when the switch is off |
| OnColor | Color | Primary | Color of the switch when it is on |
| OffColor | Color | Secondary | Color of the switch when it is off |
| ShowInnerText | bool | false | Whether to show text inside the switch |
| Size | Size | Medium | The size of the switch (Small, Medium, Large) |
| IsDisabled | bool | false | Whether the switch is disabled |
| IsLoading | bool | false | Whether to show a loading indicator |
| OnIcon | string | null | Icon to display when the switch is on |
| OffIcon | string | null | Icon to display when the switch is off |
| ShowLabel | bool | true | Whether to show the label |
| DisplayText | string | null | The text to display as the label |
| ShowRequiredMark | bool | true | Whether to show a required mark for required fields |
| RequiredMarkText | string | "*" | The text to use for the required mark |

## Events

| Event | Description |
| --- | --- |
| OnValueChanged | Triggered when the switch state changes |
| OnClick | Triggered when the switch is clicked |

## Usage Examples

### Example 1: Basic Switch

```razor
<Switch @bind-Value="@isEnabled" />

<div class="mt-3">
    Feature is @(isEnabled ? "enabled" : "disabled")
</div>

@code {
    private bool isEnabled = false;
}
```

### Example 2: Switch with Custom Text

```razor
<Switch @bind-Value="@isDarkMode" 
        OnText="Dark" 
        OffText="Light" 
        ShowInnerText="true" 
        ShowLabel="true" 
        DisplayText="Theme Mode" />

<div class="mt-3 p-3" style="@($"background-color: {(isDarkMode ? "#333" : "#fff")}")">
    <p style="@($"color: {(isDarkMode ? "#fff" : "#333")}")">
        This text changes color based on the selected theme mode.
    </p>
</div>

@code {
    private bool isDarkMode = false;
}
```

### Example 3: Switch with Custom Colors and Icons

```razor
<Switch @bind-Value="@isOnline" 
        OnText="Online" 
        OffText="Offline" 
        OnColor="Color.Success" 
        OffColor="Color.Danger" 
        OnIcon="wifi" 
        OffIcon="wifi-off" 
        ShowLabel="true" 
        DisplayText="Status" />

<div class="mt-3">
    <div class="@(isOnline ? "text-success" : "text-danger")">
        <i class="@(isOnline ? "fa fa-wifi" : "fa fa-wifi-slash") me-2"></i>
        You are currently @(isOnline ? "online" : "offline")
    </div>
</div>

@code {
    private bool isOnline = true;
}
```

### Example 4: Different Switch Sizes

```razor
<div class="mb-3">
    <Switch @bind-Value="@smallSwitch" 
            Size="Size.Small" 
            OnText="S" 
            OffText="S" 
            ShowInnerText="true" />
    <span class="ms-2">Small Switch</span>
</div>

<div class="mb-3">
    <Switch @bind-Value="@mediumSwitch" 
            Size="Size.Medium" 
            OnText="M" 
            OffText="M" 
            ShowInnerText="true" />
    <span class="ms-2">Medium Switch (default)</span>
</div>

<div class="mb-3">
    <Switch @bind-Value="@largeSwitch" 
            Size="Size.Large" 
            OnText="L" 
            OffText="L" 
            ShowInnerText="true" />
    <span class="ms-2">Large Switch</span>
</div>

@code {
    private bool smallSwitch = true;
    private bool mediumSwitch = true;
    private bool largeSwitch = true;
}
```

### Example 5: Switch with Form Validation

```razor
<ValidateForm Model="@model" OnValidSubmit="@HandleValidSubmit">
    <div class="mb-3">
        <BootstrapInput @bind-Value="@model.Name" 
                       ShowLabel="true" 
                       DisplayText="Name" 
                       Placeholder="Enter your name" />
        <ValidationMessage For="@(() => model.Name)" />
    </div>
    
    <div class="mb-3">
        <BootstrapInput @bind-Value="@model.Email" 
                       Type="email" 
                       ShowLabel="true" 
                       DisplayText="Email" 
                       Placeholder="Enter your email" />
        <ValidationMessage For="@(() => model.Email)" />
    </div>
    
    <div class="mb-3">
        <Switch @bind-Value="@model.SubscribeToNewsletter" 
                ShowLabel="true" 
                DisplayText="Subscribe to newsletter" />
        <ValidationMessage For="@(() => model.SubscribeToNewsletter)" />
    </div>
    
    <div class="mb-3">
        <Switch @bind-Value="@model.AcceptTerms" 
                ShowLabel="true" 
                DisplayText="I accept the terms and conditions" 
                OnColor="Color.Success" />
        <ValidationMessage For="@(() => model.AcceptTerms)" />
    </div>
    
    <Button Type="ButtonType.Submit">Submit</Button>
</ValidateForm>

@code {
    private UserModel model = new UserModel();
    
    private void HandleValidSubmit()
    {
        // Process the form data
        Console.WriteLine($"Name: {model.Name}");
        Console.WriteLine($"Email: {model.Email}");
        Console.WriteLine($"Subscribe to newsletter: {model.SubscribeToNewsletter}");
        Console.WriteLine($"Accept terms: {model.AcceptTerms}");
    }
    
    public class UserModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = "";
        
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = "";
        
        public bool SubscribeToNewsletter { get; set; } = false;
        
        [Required(ErrorMessage = "You must accept the terms and conditions")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "You must accept the terms and conditions")]
        public bool AcceptTerms { get; set; } = false;
    }
}
```

### Example 6: Disabled and Loading States

```razor
<div class="mb-3">
    <Switch @bind-Value="@isFeatureEnabled" 
            OnText="Enabled" 
            OffText="Disabled" 
            ShowInnerText="true" 
            ShowLabel="true" 
            DisplayText="Feature Status" />
</div>

<div class="mb-3">
    <Switch @bind-Value="@isFeatureEnabled" 
            OnText="Enabled" 
            OffText="Disabled" 
            ShowInnerText="true" 
            IsDisabled="true" 
            ShowLabel="true" 
            DisplayText="Disabled Switch (cannot be changed)" />
</div>

<div class="mb-3">
    <Switch @bind-Value="@isLoading" 
            OnText="Loading" 
            OffText="Ready" 
            ShowInnerText="true" 
            ShowLabel="true" 
            DisplayText="Toggle Loading State" />
</div>

<div class="mb-3">
    <Switch @bind-Value="@isFeatureEnabled" 
            OnText="Enabled" 
            OffText="Disabled" 
            ShowInnerText="true" 
            IsLoading="@isLoading" 
            ShowLabel="true" 
            DisplayText="Feature with Loading State" />
</div>

@code {
    private bool isFeatureEnabled = true;
    private bool isLoading = false;
}
```

### Example 7: Interactive Settings Panel

```razor
<div class="card">
    <div class="card-header">
        <h5>Application Settings</h5>
    </div>
    <div class="card-body">
        <h6>Appearance</h6>
        <div class="mb-3 d-flex justify-content-between align-items-center">
            <span>Dark Mode</span>
            <Switch @bind-Value="@settings.DarkMode" 
                    OnColor="Color.Dark" 
                    OffColor="Color.Light" 
                    OnIcon="moon" 
                    OffIcon="sun" 
                    OnValueChanged="@UpdateSettings" />
        </div>
        
        <div class="mb-3 d-flex justify-content-between align-items-center">
            <span>High Contrast</span>
            <Switch @bind-Value="@settings.HighContrast" 
                    IsDisabled="@(!settings.DarkMode)" 
                    OnValueChanged="@UpdateSettings" />
        </div>
        
        <h6 class="mt-4">Notifications</h6>
        <div class="mb-3 d-flex justify-content-between align-items-center">
            <span>Email Notifications</span>
            <Switch @bind-Value="@settings.EmailNotifications" 
                    OnValueChanged="@UpdateSettings" />
        </div>
        
        <div class="mb-3 d-flex justify-content-between align-items-center">
            <span>Push Notifications</span>
            <Switch @bind-Value="@settings.PushNotifications" 
                    OnValueChanged="@UpdateSettings" />
        </div>
        
        <div class="mb-3 d-flex justify-content-between align-items-center">
            <span>Sound Alerts</span>
            <Switch @bind-Value="@settings.SoundAlerts" 
                    OnValueChanged="@UpdateSettings" />
        </div>
        
        <h6 class="mt-4">Privacy</h6>
        <div class="mb-3 d-flex justify-content-between align-items-center">
            <span>Show Online Status</span>
            <Switch @bind-Value="@settings.ShowOnlineStatus" 
                    OnValueChanged="@UpdateSettings" />
        </div>
        
        <div class="mb-3 d-flex justify-content-between align-items-center">
            <span>Allow Data Collection</span>
            <Switch @bind-Value="@settings.AllowDataCollection" 
                    OnValueChanged="@UpdateSettings" />
        </div>
    </div>
    <div class="card-footer">
        <div class="d-flex justify-content-between">
            <Button Color="Color.Secondary" OnClick="@ResetSettings">Reset to Defaults</Button>
            <Button Color="Color.Primary" OnClick="@SaveSettings" IsDisabled="@(!settingsChanged)">Save Changes</Button>
        </div>
    </div>
</div>

@if (showSavedMessage)
{
    <div class="alert alert-success mt-3">
        Settings saved successfully!
    </div>
}

@code {
    private AppSettings settings = new AppSettings();
    private AppSettings originalSettings = new AppSettings();
    private bool settingsChanged = false;
    private bool showSavedMessage = false;
    
    private void UpdateSettings()
    {
        // Check if settings have changed from original
        settingsChanged = !settings.Equals(originalSettings);
    }
    
    private void ResetSettings()
    {
        settings = new AppSettings();
        settingsChanged = !settings.Equals(originalSettings);
    }
    
    private void SaveSettings()
    {
        // Save settings to storage or API
        Console.WriteLine("Saving settings:");
        Console.WriteLine($"Dark Mode: {settings.DarkMode}");
        Console.WriteLine($"High Contrast: {settings.HighContrast}");
        Console.WriteLine($"Email Notifications: {settings.EmailNotifications}");
        Console.WriteLine($"Push Notifications: {settings.PushNotifications}");
        Console.WriteLine($"Sound Alerts: {settings.SoundAlerts}");
        Console.WriteLine($"Show Online Status: {settings.ShowOnlineStatus}");
        Console.WriteLine($"Allow Data Collection: {settings.AllowDataCollection}");
        
        // Update original settings to match current
        originalSettings = settings.Clone();
        settingsChanged = false;
        
        // Show saved message
        showSavedMessage = true;
        
        // Hide message after 3 seconds
        Task.Delay(3000).ContinueWith(_ => {
            showSavedMessage = false;
            StateHasChanged();
        });
    }
    
    public class AppSettings
    {
        public bool DarkMode { get; set; } = false;
        public bool HighContrast { get; set; } = false;
        public bool EmailNotifications { get; set; } = true;
        public bool PushNotifications { get; set; } = true;
        public bool SoundAlerts { get; set; } = false;
        public bool ShowOnlineStatus { get; set; } = true;
        public bool AllowDataCollection { get; set; } = false;
        
        public AppSettings Clone()
        {
            return new AppSettings
            {
                DarkMode = this.DarkMode,
                HighContrast = this.HighContrast,
                EmailNotifications = this.EmailNotifications,
                PushNotifications = this.PushNotifications,
                SoundAlerts = this.SoundAlerts,
                ShowOnlineStatus = this.ShowOnlineStatus,
                AllowDataCollection = this.AllowDataCollection
            };
        }
        
        public override bool Equals(object obj)
        {
            if (obj is AppSettings other)
            {
                return DarkMode == other.DarkMode &&
                       HighContrast == other.HighContrast &&
                       EmailNotifications == other.EmailNotifications &&
                       PushNotifications == other.PushNotifications &&
                       SoundAlerts == other.SoundAlerts &&
                       ShowOnlineStatus == other.ShowOnlineStatus &&
                       AllowDataCollection == other.AllowDataCollection;
            }
            return false;
        }
        
        public override int GetHashCode()
        {
            return HashCode.Combine(DarkMode, HighContrast, EmailNotifications, 
                PushNotifications, SoundAlerts, ShowOnlineStatus, AllowDataCollection);
        }
    }
}
```

## Customization Notes

The Switch component can be customized using the following CSS variables:

```css
:root {
    --bb-switch-width: 3rem;
    --bb-switch-height: 1.5rem;
    --bb-switch-padding: 0.125rem;
    --bb-switch-border-radius: 0.75rem;
    --bb-switch-bg: #e9ecef;
    --bb-switch-checked-bg: #0d6efd;
    --bb-switch-handle-width: 1.25rem;
    --bb-switch-handle-height: 1.25rem;
    --bb-switch-handle-border-radius: 50%;
    --bb-switch-handle-bg: #fff;
    --bb-switch-handle-box-shadow: 0 0.125rem 0.25rem rgba(0, 0, 0, 0.075);
    --bb-switch-focus-box-shadow: 0 0 0 0.25rem rgba(13, 110, 253, 0.25);
    --bb-switch-transition-duration: 0.15s;
    --bb-switch-transition-timing-function: ease-in-out;
    --bb-switch-disabled-opacity: 0.65;
    --bb-switch-text-color: #fff;
    --bb-switch-font-size: 0.75rem;
    --bb-switch-font-weight: 600;
    --bb-switch-icon-size: 0.875rem;
}
```

Additionally, you can customize the appearance and behavior of the Switch component by:

1. Using the `OnText` and `OffText` properties to customize the displayed text
2. Using the `OnColor` and `OffColor` properties to change the switch colors
3. Using the `Size` property to adjust the switch size
4. Using the `ShowInnerText` property to display text inside the switch
5. Using the `OnIcon` and `OffIcon` properties to add icons
6. Using the `ShowLabel`, `DisplayText`, and `ShowRequiredMark` properties to customize the label
7. Using the `IsDisabled` and `IsLoading` properties to control the switch state
8. Applying custom CSS classes to the component using the `ClassName` property