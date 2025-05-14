# Message Component Documentation

## Overview
The Message component in BootstrapBlazor provides a way to display global notifications or feedback to users. Unlike the Alert component which is typically embedded in the page layout, Message appears as a floating notification that can be triggered programmatically and automatically disappears after a specified time.

## Features
- Multiple color themes (primary, secondary, success, danger, warning, info, dark)
- Automatic dismissal with configurable timeout
- Customizable positioning (top, bottom)
- Icon support
- HTML content support
- Animation effects
- Programmatic API for showing messages

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| Color | Color | Primary | Sets the color theme of the message |
| Content | string | null | Text content of the message |
| Icon | string | null | Custom icon to display |
| ShowBar | bool | false | Whether to show a colored bar on the left side |
| ShowIcon | bool | false | Whether to show an icon in the message |
| Placement | Placement | Top | Position of the message (Top, Bottom) |
| Delay | int | 3000 | Time in milliseconds before auto-dismissal |

## Static Methods

| Method | Parameters | Description |
| --- | --- | --- |
| Show | string content, Color color | Shows a message with the specified content and color |
| Success | string content | Shows a success message |
| Error | string content | Shows an error message |
| Info | string content | Shows an info message |
| Warning | string content | Shows a warning message |

## Usage Examples

### Example 1: Basic Message Usage

```razor
<Button Color="Color.Primary" OnClick="@ShowPrimaryMessage">Show Primary Message</Button>
<Button Color="Color.Success" OnClick="@ShowSuccessMessage">Show Success Message</Button>
<Button Color="Color.Danger" OnClick="@ShowErrorMessage">Show Error Message</Button>
<Button Color="Color.Warning" OnClick="@ShowWarningMessage">Show Warning Message</Button>
<Button Color="Color.Info" OnClick="@ShowInfoMessage">Show Info Message</Button>

@code {
    private async Task ShowPrimaryMessage()
    {
        await Message.Show("This is a primary message", Color.Primary);
    }

    private async Task ShowSuccessMessage()
    {
        await Message.Success("Operation completed successfully!");
    }

    private async Task ShowErrorMessage()
    {
        await Message.Error("An error occurred while processing your request.");
    }

    private async Task ShowWarningMessage()
    {
        await Message.Warning("Warning: This action cannot be undone.");
    }

    private async Task ShowInfoMessage()
    {
        await Message.Info("Please note that the system will be under maintenance tonight.");
    }
}
```

### Example 2: Message with Icons

```razor
<Button Color="Color.Primary" OnClick="@ShowMessageWithIcon">Show Message with Icon</Button>
<Button Color="Color.Success" OnClick="@ShowSuccessWithIcon">Show Success with Icon</Button>
<Button Color="Color.Danger" OnClick="@ShowErrorWithIcon">Show Error with Icon</Button>

@code {
    private async Task ShowMessageWithIcon()
    {
        var option = new MessageOption
        {
            Content = "This is a message with a custom icon",
            Icon = "fa-solid fa-bell",
            Color = Color.Primary
        };
        await Message.Show(option);
    }

    private async Task ShowSuccessWithIcon()
    {
        var option = new MessageOption
        {
            Content = "Operation completed successfully!",
            ShowIcon = true,
            Color = Color.Success
        };
        await Message.Show(option);
    }

    private async Task ShowErrorWithIcon()
    {
        var option = new MessageOption
        {
            Content = "An error occurred while processing your request.",
            ShowIcon = true,
            Color = Color.Danger
        };
        await Message.Show(option);
    }
}
```

### Example 3: Message with Different Placements

```razor
<Button Color="Color.Primary" OnClick="@ShowTopMessage">Show Top Message</Button>
<Button Color="Color.Primary" OnClick="@ShowBottomMessage">Show Bottom Message</Button>

@code {
    private async Task ShowTopMessage()
    {
        var option = new MessageOption
        {
            Content = "This message appears at the top",
            Placement = Placement.Top,
            Color = Color.Info
        };
        await Message.Show(option);
    }

    private async Task ShowBottomMessage()
    {
        var option = new MessageOption
        {
            Content = "This message appears at the bottom",
            Placement = Placement.Bottom,
            Color = Color.Info
        };
        await Message.Show(option);
    }
}
```

### Example 4: Message with Custom Delay

```razor
<Button Color="Color.Primary" OnClick="@ShowShortMessage">Show Short Message (1s)</Button>
<Button Color="Color.Primary" OnClick="@ShowLongMessage">Show Long Message (10s)</Button>

@code {
    private async Task ShowShortMessage()
    {
        var option = new MessageOption
        {
            Content = "This message will disappear quickly",
            Delay = 1000,
            Color = Color.Warning
        };
        await Message.Show(option);
    }

    private async Task ShowLongMessage()
    {
        var option = new MessageOption
        {
            Content = "This message will stay longer",
            Delay = 10000,
            Color = Color.Warning
        };
        await Message.Show(option);
    }
}
```

### Example 5: Message with HTML Content

```razor
<Button Color="Color.Primary" OnClick="@ShowHtmlMessage">Show HTML Message</Button>

@code {
    private async Task ShowHtmlMessage()
    {
        var option = new MessageOption
        {
            Content = "<strong>Bold text</strong> and <em>italic text</em> in a message",
            IsHtml = true,
            Color = Color.Info
        };
        await Message.Show(option);
    }
}
```

### Example 6: Message with Bar Style

```razor
<Button Color="Color.Primary" OnClick="@ShowBarMessage">Show Bar Message</Button>
<Button Color="Color.Success" OnClick="@ShowSuccessBarMessage">Show Success Bar Message</Button>
<Button Color="Color.Danger" OnClick="@ShowErrorBarMessage">Show Error Bar Message</Button>

@code {
    private async Task ShowBarMessage()
    {
        var option = new MessageOption
        {
            Content = "This is a message with a bar style",
            ShowBar = true,
            Color = Color.Primary
        };
        await Message.Show(option);
    }

    private async Task ShowSuccessBarMessage()
    {
        var option = new MessageOption
        {
            Content = "Operation completed successfully!",
            ShowBar = true,
            Color = Color.Success
        };
        await Message.Show(option);
    }

    private async Task ShowErrorBarMessage()
    {
        var option = new MessageOption
        {
            Content = "An error occurred while processing your request.",
            ShowBar = true,
            Color = Color.Danger
        };
        await Message.Show(option);
    }
}
```

### Example 7: Message in Form Validation

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
            await Message.Success("Login successful!");
            // Redirect or other logic
        }
        else
        {
            await Message.Error("Invalid username or password.");
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

## CSS Customization

The Message component uses the following CSS structure that can be customized:

```css
.message {
    position: fixed;
    left: 1rem;
    right: 1rem;
    z-index: 1090;
    pointer-events: none;
}

.message .alert {
    display: flex;
    align-items: baseline;
    min-width: 160px;
    max-width: 480px;
    white-space: normal;
    opacity: 0;
    top: -20px;
    bottom: unset;
    margin: 1rem auto 0 auto;
    transition: opacity .3s linear, top .3s linear, bottom .3s linear;
    pointer-events: auto;
}

.message .alert.show {
    opacity: 1;
    bottom: unset;
    top: 20px;
}

.message.is-bottom .alert {
    top: unset;
    bottom: -20px;
}

.message.is-bottom .alert.show {
    top: unset;
    bottom: 20px;
}
```

You can override these styles in your own CSS to customize the appearance of the Message component.

## Notes

- The Message component is ideal for providing feedback after user actions, such as form submissions or button clicks.
- Unlike modals or alerts, messages don't interrupt the user's workflow as they appear briefly and disappear automatically.
- For critical errors or information that requires user acknowledgment, consider using Modal or Alert components instead.
- Messages are rendered at the application root level, so they appear above all other content regardless of where they are triggered from.
- The component automatically manages multiple messages, stacking them when multiple are shown simultaneously.