# Toast Component

## Overview
The Toast component in BootstrapBlazor provides a lightweight notification system for displaying brief, auto-expiring messages to users. Toasts are non-intrusive notifications that appear in a designated area of the screen (typically the top-right corner) and automatically disappear after a set duration. They are ideal for providing feedback about operations without interrupting the user's workflow.

## Features
- Multiple color themes (primary, secondary, success, danger, warning, info, dark, light)
- Customizable display duration
- Header and body content customization
- Icon support
- Stacking of multiple toasts
- Placement options (top-right, top-left, bottom-right, bottom-left)
- Auto-hide functionality with configurable delay
- Manual dismissal option
- Animation effects
- Programmatic API for showing and hiding toasts
- Custom templates support

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `Color` | `Color` | `Color.Primary` | Sets the color theme of the toast |
| `Title` | `string` | `null` | Sets the title/header text of the toast |
| `Content` | `string` | `null` | Sets the main content/body text of the toast |
| `Delay` | `int` | `5000` | Sets the auto-hide delay in milliseconds |
| `IsAutoHide` | `bool` | `true` | When true, automatically hides the toast after Delay milliseconds |
| `IsShow` | `bool` | `false` | Controls the visibility of the toast |
| `ShowClose` | `bool` | `true` | When true, displays a close button to manually dismiss the toast |
| `Icon` | `string` | `null` | Sets the icon class to display in the toast header |
| `Placement` | `Placement` | `Placement.TopRight` | Sets the position of the toast on the screen |
| `HeaderTemplate` | `RenderFragment` | `null` | Custom template for the toast header |
| `BodyTemplate` | `RenderFragment` | `null` | Custom template for the toast body |

## Static Methods

| Method | Description |
| --- | --- |
| `Show(string title, string content)` | Shows a primary toast with the specified title and content |
| `ShowSuccess(string title, string content)` | Shows a success toast with the specified title and content |
| `ShowError(string title, string content)` | Shows an error toast with the specified title and content |
| `ShowWarning(string title, string content)` | Shows a warning toast with the specified title and content |
| `ShowInfo(string title, string content)` | Shows an info toast with the specified title and content |

## Events

| Event | Description |
| --- | --- |
| `OnShown` | Triggered when the toast is shown |
| `OnHidden` | Triggered when the toast is hidden |

## Usage Examples

### Example 1: Basic Toast
```csharp
<Button OnClick="ShowBasicToast">Show Basic Toast</Button>

@code {
    private async Task ShowBasicToast()
    {
        await Toast.Show("Notification", "This is a basic toast notification.");
    }
}
```
This example shows a basic toast notification with a title and message using the static Show method.

### Example 2: Different Toast Colors
```csharp
<div class="d-flex flex-wrap gap-2">
    <Button OnClick="ShowSuccessToast" Color="Color.Success">Success</Button>
    <Button OnClick="ShowErrorToast" Color="Color.Danger">Error</Button>
    <Button OnClick="ShowWarningToast" Color="Color.Warning">Warning</Button>
    <Button OnClick="ShowInfoToast" Color="Color.Info">Info</Button>
    <Button OnClick="ShowDarkToast" Color="Color.Dark">Dark</Button>
</div>

@code {
    private async Task ShowSuccessToast()
    {
        await Toast.ShowSuccess("Success", "Operation completed successfully.");
    }
    
    private async Task ShowErrorToast()
    {
        await Toast.ShowError("Error", "Something went wrong.");
    }
    
    private async Task ShowWarningToast()
    {
        await Toast.ShowWarning("Warning", "This action might have consequences.");
    }
    
    private async Task ShowInfoToast()
    {
        await Toast.ShowInfo("Information", "Here's some information you should know.");
    }
    
    private async Task ShowDarkToast()
    {
        await Toast.Show(new ToastOption
        {
            Title = "Dark Toast",
            Content = "This is a dark themed toast.",
            Color = Color.Dark
        });
    }
}
```
This example demonstrates the different color options available for toasts, each with appropriate styling.

### Example 3: Custom Duration and Auto-hide
```csharp
<div class="d-flex flex-wrap gap-2">
    <Button OnClick="ShowQuickToast">Quick Toast (2s)</Button>
    <Button OnClick="ShowLongToast">Long Toast (10s)</Button>
    <Button OnClick="ShowPersistentToast">Persistent Toast</Button>
</div>

@code {
    private async Task ShowQuickToast()
    {
        await Toast.Show(new ToastOption
        {
            Title = "Quick Toast",
            Content = "This toast will disappear in 2 seconds.",
            Delay = 2000, // 2 seconds
            IsAutoHide = true
        });
    }
    
    private async Task ShowLongToast()
    {
        await Toast.Show(new ToastOption
        {
            Title = "Long Toast",
            Content = "This toast will stay visible for 10 seconds.",
            Delay = 10000, // 10 seconds
            IsAutoHide = true
        });
    }
    
    private async Task ShowPersistentToast()
    {
        await Toast.Show(new ToastOption
        {
            Title = "Persistent Toast",
            Content = "This toast will not auto-hide. Click the X to dismiss it.",
            IsAutoHide = false,
            ShowClose = true
        });
    }
}
```
This example shows how to customize the duration of toasts and create persistent toasts that don't automatically hide.

### Example 4: Different Placements
```csharp
<div class="d-flex flex-wrap gap-2">
    <Button OnClick="() => ShowToastWithPlacement(Placement.TopRight)">Top Right</Button>
    <Button OnClick="() => ShowToastWithPlacement(Placement.TopLeft)">Top Left</Button>
    <Button OnClick="() => ShowToastWithPlacement(Placement.BottomRight)">Bottom Right</Button>
    <Button OnClick="() => ShowToastWithPlacement(Placement.BottomLeft)">Bottom Left</Button>
</div>

@code {
    private async Task ShowToastWithPlacement(Placement placement)
    {
        await Toast.Show(new ToastOption
        {
            Title = $"{placement} Toast",
            Content = $"This toast appears in the {placement} corner.",
            Placement = placement,
            Color = Color.Info
        });
    }
}
```
This example demonstrates how to position toasts in different corners of the screen.

### Example 5: Toast with Icons
```csharp
<Button OnClick="ShowIconToasts">Show Toasts with Icons</Button>

@code {
    private async Task ShowIconToasts()
    {
        // Success toast with check icon
        await Toast.Show(new ToastOption
        {
            Title = "Success",
            Content = "Task completed successfully.",
            Color = Color.Success,
            Icon = "fa fa-check-circle"
        });
        
        // Wait a moment before showing the next toast
        await Task.Delay(300);
        
        // Warning toast with warning icon
        await Toast.Show(new ToastOption
        {
            Title = "Warning",
            Content = "Disk space is running low.",
            Color = Color.Warning,
            Icon = "fa fa-exclamation-triangle"
        });
        
        // Wait a moment before showing the next toast
        await Task.Delay(300);
        
        // Info toast with info icon
        await Toast.Show(new ToastOption
        {
            Title = "Information",
            Content = "You have 3 new messages.",
            Color = Color.Info,
            Icon = "fa fa-info-circle"
        });
    }
}
```
This example shows how to add icons to toasts to provide visual cues about the type of notification.

### Example 6: Custom Templates
```csharp
<Button OnClick="ShowCustomTemplateToast">Show Custom Template Toast</Button>

@code {
    private async Task ShowCustomTemplateToast()
    {
        await Toast.Show(new ToastOption
        {
            Title = "New Message",
            Content = "You have received a new message.",
            Color = Color.Primary,
            Delay = 8000,
            HeaderTemplate = builder =>
            {
                builder.OpenElement(0, "div");
                builder.AddAttribute(1, "class", "d-flex align-items-center");
                
                // Avatar
                builder.OpenElement(2, "div");
                builder.AddAttribute(3, "class", "me-2");
                builder.OpenElement(4, "img");
                builder.AddAttribute(5, "src", "/images/avatar.png");
                builder.AddAttribute(6, "class", "rounded-circle");
                builder.AddAttribute(7, "width", "32");
                builder.AddAttribute(8, "height", "32");
                builder.AddAttribute(9, "alt", "User Avatar");
                builder.CloseElement(); // img
                builder.CloseElement(); // div
                
                // Title
                builder.OpenElement(10, "div");
                builder.AddAttribute(11, "class", "fw-bold");
                builder.AddContent(12, "John Doe");
                builder.CloseElement(); // div
                
                // Time
                builder.OpenElement(13, "div");
                builder.AddAttribute(14, "class", "ms-auto small text-muted");
                builder.AddContent(15, "Just now");
                builder.CloseElement(); // div
                
                builder.CloseElement(); // div
            },
            BodyTemplate = builder =>
            {
                builder.OpenElement(0, "div");
                
                // Message content
                builder.OpenElement(1, "p");
                builder.AddContent(2, "Hi there! I wanted to check if you're available for a meeting tomorrow at 2 PM?");
                builder.CloseElement(); // p
                
                // Action buttons
                builder.OpenElement(3, "div");
                builder.AddAttribute(4, "class", "d-flex gap-2");
                
                builder.OpenElement(5, "button");
                builder.AddAttribute(6, "class", "btn btn-sm btn-primary");
                builder.AddAttribute(7, "onclick", EventCallback.Factory.Create(this, () => Reply()));
                builder.AddContent(8, "Reply");
                builder.CloseElement(); // button
                
                builder.OpenElement(9, "button");
                builder.AddAttribute(10, "class", "btn btn-sm btn-outline-secondary");
                builder.AddAttribute(11, "onclick", EventCallback.Factory.Create(this, () => Dismiss()));
                builder.AddContent(12, "Dismiss");
                builder.CloseElement(); // button
                
                builder.CloseElement(); // div
                
                builder.CloseElement(); // div
            }
        });
    }
    
    private void Reply()
    {
        // Handle reply action
    }
    
    private void Dismiss()
    {
        // Handle dismiss action
    }
}
```
This example demonstrates how to create a custom toast template with an avatar, sender name, timestamp, and action buttons.

### Example 7: Toast for Form Validation
```csharp
<Form Model="@model" OnValidSubmit="HandleValidSubmit">
    <div class="mb-3">
        <BootstrapInput @bind-Value="@model.Name" placeholder="Enter your name" />
    </div>
    <div class="mb-3">
        <BootstrapInput @bind-Value="@model.Email" placeholder="Enter your email" />
    </div>
    <Button Type="submit">Submit</Button>
</Form>

@code {
    private FormModel model = new();
    
    private class FormModel
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }
    }
    
    private async Task HandleValidSubmit()
    {
        try
        {
            // Simulate API call
            await Task.Delay(1000);
            
            // Show success toast
            await Toast.ShowSuccess("Form Submitted", $"Thank you, {model.Name}! Your information has been submitted successfully.");
            
            // Reset form
            model = new FormModel();
        }
        catch (Exception ex)
        {
            // Show error toast
            await Toast.ShowError("Submission Failed", $"An error occurred: {ex.Message}");
        }
    }
}
```
This example shows how to use toasts to provide feedback after form submission, showing success or error messages based on the result.

## CSS Customization

The Toast component can be customized using the following CSS variables:

```css
--bb-toast-max-width: 350px;
--bb-toast-padding: 0.75rem;
--bb-toast-border-radius: 0.25rem;
--bb-toast-box-shadow: 0 0.25rem 0.75rem rgba(0, 0, 0, 0.1);
--bb-toast-header-padding-y: 0.25rem;
--bb-toast-header-padding-x: 0.75rem;
--bb-toast-header-background-color: rgba(255, 255, 255, 0.85);
--bb-toast-header-border-color: rgba(0, 0, 0, 0.05);
--bb-toast-body-padding: 0.75rem;
--bb-toast-z-index: 1090;
--bb-toast-primary-background: rgba(0, 123, 255, 0.9);
--bb-toast-primary-color: #fff;
--bb-toast-secondary-background: rgba(108, 117, 125, 0.9);
--bb-toast-secondary-color: #fff;
--bb-toast-success-background: rgba(40, 167, 69, 0.9);
--bb-toast-success-color: #fff;
--bb-toast-danger-background: rgba(220, 53, 69, 0.9);
--bb-toast-danger-color: #fff;
--bb-toast-warning-background: rgba(255, 193, 7, 0.9);
--bb-toast-warning-color: #212529;
--bb-toast-info-background: rgba(23, 162, 184, 0.9);
--bb-toast-info-color: #fff;
--bb-toast-light-background: rgba(248, 249, 250, 0.9);
--bb-toast-light-color: #212529;
--bb-toast-dark-background: rgba(52, 58, 64, 0.9);
--bb-toast-dark-color: #fff;
```

## Notes

1. **Accessibility**: The Toast component includes ARIA attributes for better accessibility. It uses `role="alert"` and `aria-live="assertive"` for important notifications.

2. **Mobile Considerations**: On mobile devices, toasts automatically adjust their size and position to ensure they remain visible without obstructing important content.

3. **Stacking Behavior**: When multiple toasts are shown, they stack vertically in the specified placement area, with the newest toast appearing at the top or bottom depending on the placement.

4. **Performance**: For applications that show many toasts in quick succession, consider implementing a queue system to prevent overwhelming the user with too many notifications at once.

5. **Use Cases**: Toasts are best used for non-critical information that doesn't require user action. For important messages that require user acknowledgment or action, consider using a modal dialog or alert instead.