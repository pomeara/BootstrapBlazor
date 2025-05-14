# Notification Component Documentation

## Overview
The Notification component in BootstrapBlazor provides a way to display global notification messages to users. Unlike the Message component which appears in the center of the screen, Notifications typically appear in the corner of the screen and can contain more complex content including titles, descriptions, and custom actions.

## Features
- Multiple color themes (primary, secondary, success, danger, warning, info, dark)
- Customizable positioning (top-right, top-left, bottom-right, bottom-left)
- Automatic dismissal with configurable timeout
- Custom content support including HTML
- Icon support
- Action buttons
- Stacking of multiple notifications
- Programmatic API for showing notifications

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| Color | Color | Primary | Sets the color theme of the notification |
| Title | string | null | Title text for the notification |
| Content | string | null | Content text of the notification |
| Icon | string | null | Custom icon to display |
| Placement | Placement | TopRight | Position of the notification (TopRight, TopLeft, BottomRight, BottomLeft) |
| Delay | int | 4500 | Time in milliseconds before auto-dismissal |
| IsHtml | bool | false | Whether the content contains HTML |
| ShowClose | bool | true | Whether to show the close button |

## Static Methods

| Method | Parameters | Description |
| --- | --- | --- |
| Show | NotificationOption option | Shows a notification with the specified options |
| Success | string content, string title | Shows a success notification |
| Error | string content, string title | Shows an error notification |
| Info | string content, string title | Shows an info notification |
| Warning | string content, string title | Shows a warning notification |

## Usage Examples

### Example 1: Basic Notification

```razor
<Button Color="Color.Primary" OnClick="@ShowPrimaryNotification">Show Primary Notification</Button>
<Button Color="Color.Success" OnClick="@ShowSuccessNotification">Show Success Notification</Button>
<Button Color="Color.Danger" OnClick="@ShowErrorNotification">Show Error Notification</Button>
<Button Color="Color.Warning" OnClick="@ShowWarningNotification">Show Warning Notification</Button>
<Button Color="Color.Info" OnClick="@ShowInfoNotification">Show Info Notification</Button>

@code {
    private async Task ShowPrimaryNotification()
    {
        await Notification.Show(new NotificationOption
        {
            Title = "Primary Notification",
            Content = "This is a primary notification message.",
            Color = Color.Primary
        });
    }

    private async Task ShowSuccessNotification()
    {
        await Notification.Success("Operation completed successfully!", "Success");
    }

    private async Task ShowErrorNotification()
    {
        await Notification.Error("An error occurred while processing your request.", "Error");
    }

    private async Task ShowWarningNotification()
    {
        await Notification.Warning("Warning: This action cannot be undone.", "Warning");
    }

    private async Task ShowInfoNotification()
    {
        await Notification.Info("Please note that the system will be under maintenance tonight.", "Information");
    }
}
```

### Example 2: Notification with Icons

```razor
<Button Color="Color.Primary" OnClick="@ShowNotificationWithIcon">Show Notification with Icon</Button>
<Button Color="Color.Success" OnClick="@ShowSuccessWithIcon">Show Success with Icon</Button>
<Button Color="Color.Danger" OnClick="@ShowErrorWithIcon">Show Error with Icon</Button>

@code {
    private async Task ShowNotificationWithIcon()
    {
        await Notification.Show(new NotificationOption
        {
            Title = "Custom Icon",
            Content = "This is a notification with a custom icon",
            Icon = "fa-solid fa-bell",
            Color = Color.Primary
        });
    }

    private async Task ShowSuccessWithIcon()
    {
        await Notification.Show(new NotificationOption
        {
            Title = "Success",
            Content = "Operation completed successfully!",
            Icon = "fa-solid fa-check-circle",
            Color = Color.Success
        });
    }

    private async Task ShowErrorWithIcon()
    {
        await Notification.Show(new NotificationOption
        {
            Title = "Error",
            Content = "An error occurred while processing your request.",
            Icon = "fa-solid fa-times-circle",
            Color = Color.Danger
        });
    }
}
```

### Example 3: Notification with Different Placements

```razor
<Button Color="Color.Primary" OnClick="@(() => ShowNotificationPlacement(Placement.TopRight))">Top Right</Button>
<Button Color="Color.Primary" OnClick="@(() => ShowNotificationPlacement(Placement.TopLeft))">Top Left</Button>
<Button Color="Color.Primary" OnClick="@(() => ShowNotificationPlacement(Placement.BottomRight))">Bottom Right</Button>
<Button Color="Color.Primary" OnClick="@(() => ShowNotificationPlacement(Placement.BottomLeft))">Bottom Left</Button>

@code {
    private async Task ShowNotificationPlacement(Placement placement)
    {
        await Notification.Show(new NotificationOption
        {
            Title = $"{placement} Notification",
            Content = $"This notification appears at the {placement} of the screen.",
            Placement = placement,
            Color = Color.Info
        });
    }
}
```

### Example 4: Notification with Custom Delay

```razor
<Button Color="Color.Primary" OnClick="@ShowShortNotification">Short Notification (2s)</Button>
<Button Color="Color.Primary" OnClick="@ShowLongNotification">Long Notification (10s)</Button>
<Button Color="Color.Primary" OnClick="@ShowPersistentNotification">Persistent Notification</Button>

@code {
    private async Task ShowShortNotification()
    {
        await Notification.Show(new NotificationOption
        {
            Title = "Short Notification",
            Content = "This notification will disappear quickly",
            Delay = 2000,
            Color = Color.Warning
        });
    }

    private async Task ShowLongNotification()
    {
        await Notification.Show(new NotificationOption
        {
            Title = "Long Notification",
            Content = "This notification will stay longer",
            Delay = 10000,
            Color = Color.Warning
        });
    }

    private async Task ShowPersistentNotification()
    {
        await Notification.Show(new NotificationOption
        {
            Title = "Persistent Notification",
            Content = "This notification will not auto-close. Click the X to dismiss it.",
            Delay = 0, // 0 means it won't auto-close
            Color = Color.Warning
        });
    }
}
```

### Example 5: Notification with HTML Content

```razor
<Button Color="Color.Primary" OnClick="@ShowHtmlNotification">Show HTML Notification</Button>

@code {
    private async Task ShowHtmlNotification()
    {
        await Notification.Show(new NotificationOption
        {
            Title = "HTML Content",
            Content = "<strong>Bold text</strong> and <em>italic text</em> in a notification<br><ul><li>Item 1</li><li>Item 2</li></ul>",
            IsHtml = true,
            Color = Color.Info
        });
    }
}
```

### Example 6: Notification with Action Buttons

```razor
<Button Color="Color.Primary" OnClick="@ShowActionNotification">Show Notification with Actions</Button>

@code {
    private async Task ShowActionNotification()
    {
        await Notification.Show(new NotificationOption
        {
            Title = "Confirm Action",
            Content = "Are you sure you want to delete this item?",
            Color = Color.Warning,
            Delay = 0, // Don't auto-close
            CloseButtonText = "Cancel",
            Actions = new List<NotificationAction>
            {
                new NotificationAction
                {
                    Text = "Delete",
                    Color = Color.Danger,
                    Callback = async () => 
                    {
                        // Perform delete operation
                        await Task.Delay(500); // Simulate operation
                        await Notification.Success("Item deleted successfully!", "Success");
                    }
                }
            }
        });
    }
}
```

### Example 7: Notification in Form Validation

```razor
<Form Model="@model" OnValidSubmit="@HandleValidSubmit">
    <div class="mb-3">
        <BootstrapInput @bind-Value="@model.Username" placeholder="Enter username" />
    </div>
    
    <div class="mb-3">
        <BootstrapInput @bind-Value="@model.Password" type="password" placeholder="Enter password" />
    </div>
    
    <Button Color="Color.Primary" Type="ButtonType.Submit">Submit</Button>
</Form>

@code {
    private LoginModel model = new LoginModel();
    
    private async Task HandleValidSubmit()
    {
        // Simulate API call
        await Task.Delay(1000);
        
        if (model.Username == "admin" && model.Password == "password")
        {
            await Notification.Success("Login successful! Redirecting to dashboard...", "Success");
            // Redirect or other logic
        }
        else
        {
            await Notification.Error("Invalid username or password. Please try again.", "Login Failed");
        }
    }
    
    private class LoginModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = "";
        
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = "";
    }
}
```

## Notes

- Notifications are rendered at the application root level, so they appear above all other content regardless of where they are triggered from.
- The component automatically manages multiple notifications, stacking them when multiple are shown simultaneously.
- For critical errors or information that requires user acknowledgment, consider using Modal or Alert components instead.
- When using `IsHtml="true"`, be careful with the HTML content to avoid XSS vulnerabilities.
- Notifications with actions are useful for providing quick access to related operations without navigating away from the current page.
- The default delay of 4500ms can be customized for each notification, or set to 0 to create persistent notifications that don't auto-close.
- For accessibility, ensure that important information is not only conveyed through notifications, as they may be missed by users with screen readers or those who navigate away quickly.