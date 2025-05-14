# ThemeProvider Component

## Overview
The ThemeProvider component in BootstrapBlazor provides a centralized way to manage and customize the application's theme. It allows developers to dynamically change themes, customize colors, and apply consistent styling across the entire application. This component serves as a wrapper that provides theme context to all child components, enabling seamless theme switching and customization without page reloads.

## Key Features
- **Dynamic Theme Switching**: Change themes at runtime without page reloads
- **Built-in Themes**: Includes light, dark, and auto themes (follows system preference)
- **Custom Theme Support**: Create and register custom themes
- **CSS Variable Management**: Automatically manages CSS variables for theme colors
- **Component-Level Theming**: Apply different themes to specific sections of the application
- **Theme Persistence**: Optional storage of user theme preferences
- **System Theme Detection**: Automatically detect and apply system theme preference
- **Theme Change Events**: Subscribe to theme change notifications
- **Responsive Theming**: Apply different themes based on screen size
- **Runtime Color Customization**: Modify theme colors at runtime

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `Theme` | `string` | `"light"` | The current theme name ("light", "dark", "auto", or custom theme name) |
| `EnableSystemTheme` | `bool` | `true` | When true, enables system theme detection for "auto" theme |
| `ThemeStorage` | `ThemeStorage` | `ThemeStorage.Local` | Storage type for theme preference (None, Local, Session) |
| `StorageKey` | `string` | `"bb-theme"` | Key used for storing theme preference |
| `CustomThemes` | `Dictionary<string, ThemeOption>` | `null` | Dictionary of custom themes with their configurations |
| `ChildContent` | `RenderFragment` | `null` | Content to be rendered within the theme context |
| `OnThemeChanged` | `EventCallback<string>` | `null` | Event triggered when theme changes |
| `PreferredCustomTheme` | `string` | `null` | Name of the preferred custom theme to use |
| `ThemeColorOptions` | `ThemeColorOptions` | `null` | Options for customizing theme colors |

## Events

| Event | Description |
| --- | --- |
| `OnThemeChanged` | Triggered when the theme changes, provides the new theme name |
| `OnSystemThemeChanged` | Triggered when the system theme preference changes |
| `OnThemeColorsChanged` | Triggered when theme colors are modified at runtime |

## Usage Examples

### Example 1: Basic Theme Provider Setup
```razor
@using BootstrapBlazor.Components

<ThemeProvider>
    <App />
</ThemeProvider>

@code {
    // The ThemeProvider wraps the entire application
    // providing theme context to all components
}
```

### Example 2: Theme Switching with Buttons
```razor
@using BootstrapBlazor.Components

<ThemeProvider @bind-Theme="currentTheme">
    <div class="mb-3">
        <h3>Current Theme: @currentTheme</h3>
        <div class="d-flex gap-2">
            <Button Color="Color.Primary" OnClick="() => currentTheme = "light"">Light Theme</Button>
            <Button Color="Color.Secondary" OnClick="() => currentTheme = "dark"">Dark Theme</Button>
            <Button Color="Color.Info" OnClick="() => currentTheme = "auto"">Auto Theme</Button>
        </div>
    </div>
    
    <div class="theme-demo-container p-3 border rounded">
        <h4>Themed Content</h4>
        <p>This content will adapt to the selected theme.</p>
        <div class="d-flex gap-2">
            <Button Color="Color.Primary">Primary Button</Button>
            <Button Color="Color.Success">Success Button</Button>
            <Button Color="Color.Danger">Danger Button</Button>
        </div>
    </div>
</ThemeProvider>

@code {
    private string currentTheme = "light";
}
```

### Example 3: Custom Theme Registration
```razor
@using BootstrapBlazor.Components
@using System.Collections.Generic

<ThemeProvider Theme="customTheme" CustomThemes="customThemes">
    <div class="mb-3">
        <h3>Custom Theme Demo</h3>
        <p>Current theme: @customTheme</p>
        <div class="d-flex gap-2">
            <Button Color="Color.Primary" OnClick="() => customTheme = "light"">Light</Button>
            <Button Color="Color.Secondary" OnClick="() => customTheme = "dark"">Dark</Button>
            <Button Color="Color.Success" OnClick="() => customTheme = "forest"">Forest</Button>
            <Button Color="Color.Info" OnClick="() => customTheme = "ocean"">Ocean</Button>
        </div>
    </div>
    
    <div class="theme-demo-container p-3 border rounded">
        <h4>Themed Content</h4>
        <p>This content will adapt to the selected theme.</p>
        <div class="d-flex gap-2">
            <Button Color="Color.Primary">Primary Button</Button>
            <Button Color="Color.Success">Success Button</Button>
            <Button Color="Color.Danger">Danger Button</Button>
        </div>
    </div>
</ThemeProvider>

@code {
    private string customTheme = "light";
    private Dictionary<string, ThemeOption> customThemes = new()
    {
        ["forest"] = new ThemeOption
        {
            ThemeColors = new Dictionary<string, string>
            {
                ["--bb-primary"] = "#2c7744",
                ["--bb-primary-rgb"] = "44, 119, 68",
                ["--bb-primary-hover"] = "#235c35",
                ["--bb-primary-active"] = "#1d4e2d",
                ["--bb-primary-text"] = "#ffffff",
                ["--bb-body-bg"] = "#f0f7f0",
                ["--bb-body-color"] = "#1d4e2d",
                ["--bb-secondary"] = "#6c8976",
                ["--bb-success"] = "#3e9b5f",
                ["--bb-info"] = "#5bc0de",
                ["--bb-warning"] = "#f0ad4e",
                ["--bb-danger"] = "#d9534f"
            }
        },
        ["ocean"] = new ThemeOption
        {
            ThemeColors = new Dictionary<string, string>
            {
                ["--bb-primary"] = "#1a6b9f",
                ["--bb-primary-rgb"] = "26, 107, 159",
                ["--bb-primary-hover"] = "#155a87",
                ["--bb-primary-active"] = "#124e76",
                ["--bb-primary-text"] = "#ffffff",
                ["--bb-body-bg"] = "#f0f7fc",
                ["--bb-body-color"] = "#124e76",
                ["--bb-secondary"] = "#6c8999",
                ["--bb-success"] = "#3e9b9b",
                ["--bb-info"] = "#5bc0de",
                ["--bb-warning"] = "#f0ad4e",
                ["--bb-danger"] = "#d9534f"
            }
        }
    };
}
```

### Example 4: Theme Persistence with Local Storage
```razor
@using BootstrapBlazor.Components

<ThemeProvider Theme="@theme" 
              ThemeStorage="ThemeStorage.Local" 
              StorageKey="my-app-theme"
              OnThemeChanged="HandleThemeChanged">
    <div class="mb-3">
        <h3>Theme with Persistence</h3>
        <p>Your theme preference will be saved to local storage.</p>
        <div class="d-flex gap-2">
            <Button Color="Color.Primary" OnClick="() => theme = "light"">Light Theme</Button>
            <Button Color="Color.Secondary" OnClick="() => theme = "dark"">Dark Theme</Button>
        </div>
    </div>
    
    <div class="theme-demo-container p-3 border rounded">
        <h4>Themed Content</h4>
        <p>This content will adapt to the selected theme and persist between sessions.</p>
    </div>
</ThemeProvider>

@code {
    private string theme = "light";
    
    private Task HandleThemeChanged(string newTheme)
    {
        theme = newTheme;
        return Task.CompletedTask;
    }
}
```

### Example 5: System Theme Detection
```razor
@using BootstrapBlazor.Components

<ThemeProvider Theme="auto" EnableSystemTheme="true" OnSystemThemeChanged="HandleSystemThemeChanged">
    <div class="mb-3">
        <h3>System Theme Detection</h3>
        <p>Current system theme: @systemTheme</p>
        <p>The theme will automatically follow your system preference.</p>
    </div>
    
    <div class="theme-demo-container p-3 border rounded">
        <h4>Themed Content</h4>
        <p>This content will adapt to your system theme preference.</p>
    </div>
</ThemeProvider>

@code {
    private string systemTheme = "unknown";
    
    private Task HandleSystemThemeChanged(string detectedTheme)
    {
        systemTheme = detectedTheme;
        StateHasChanged();
        return Task.CompletedTask;
    }
}
```

### Example 6: Nested Theme Providers
```razor
@using BootstrapBlazor.Components

<ThemeProvider Theme="light">
    <div class="p-3 border rounded mb-3">
        <h3>Light Theme Section</h3>
        <p>This section uses the light theme.</p>
        <Button Color="Color.Primary">Light Theme Button</Button>
    </div>
    
    <ThemeProvider Theme="dark">
        <div class="p-3 border rounded mb-3">
            <h3>Dark Theme Section</h3>
            <p>This nested section uses the dark theme.</p>
            <Button Color="Color.Primary">Dark Theme Button</Button>
        </div>
    </ThemeProvider>
    
    <ThemeProvider Theme="customTheme" CustomThemes="customThemes">
        <div class="p-3 border rounded">
            <h3>Custom Theme Section</h3>
            <p>This nested section uses a custom theme.</p>
            <Button Color="Color.Primary">Custom Theme Button</Button>
        </div>
    </ThemeProvider>
</ThemeProvider>

@code {
    private string customTheme = "forest";
    private Dictionary<string, ThemeOption> customThemes = new()
    {
        ["forest"] = new ThemeOption
        {
            ThemeColors = new Dictionary<string, string>
            {
                ["--bb-primary"] = "#2c7744",
                ["--bb-primary-rgb"] = "44, 119, 68",
                ["--bb-body-bg"] = "#f0f7f0",
                ["--bb-body-color"] = "#1d4e2d"
            }
        }
    };
}
```

### Example 7: Runtime Color Customization
```razor
@using BootstrapBlazor.Components

<ThemeProvider @bind-Theme="currentTheme" ThemeColorOptions="colorOptions">
    <div class="mb-3">
        <h3>Runtime Color Customization</h3>
        <p>Current theme: @currentTheme</p>
        <div class="d-flex gap-2 mb-3">
            <Button Color="Color.Primary" OnClick="() => currentTheme = "light"">Light</Button>
            <Button Color="Color.Secondary" OnClick="() => currentTheme = "dark"">Dark</Button>
        </div>
        
        <div class="mb-3">
            <h4>Customize Primary Color</h4>
            <div class="d-flex align-items-center gap-2">
                <span>Primary Color:</span>
                <input type="color" value="@primaryColor" @onchange="UpdatePrimaryColor" />
            </div>
        </div>
    </div>
    
    <div class="theme-demo-container p-3 border rounded">
        <h4>Themed Content</h4>
        <p>This content will use the customized colors.</p>
        <div class="d-flex gap-2">
            <Button Color="Color.Primary">Primary Button</Button>
            <Button Color="Color.Success">Success Button</Button>
            <Button Color="Color.Danger">Danger Button</Button>
        </div>
    </div>
</ThemeProvider>

@code {
    private string currentTheme = "light";
    private string primaryColor = "#0d6efd";
    private ThemeColorOptions colorOptions = new();
    
    private void UpdatePrimaryColor(ChangeEventArgs e)
    {
        if (e.Value is string colorValue)
        {
            primaryColor = colorValue;
            
            // Update the primary color in the theme
            colorOptions.Colors["--bb-primary"] = primaryColor;
            
            // Convert hex to RGB for CSS variables
            var rgb = HexToRgb(primaryColor);
            if (rgb != null)
            {
                colorOptions.Colors["--bb-primary-rgb"] = $"{rgb.Value.r}, {rgb.Value.g}, {rgb.Value.b}";
            }
            
            // Generate hover and active colors (slightly darker)
            colorOptions.Colors["--bb-primary-hover"] = AdjustBrightness(primaryColor, -15);
            colorOptions.Colors["--bb-primary-active"] = AdjustBrightness(primaryColor, -25);
            
            StateHasChanged();
        }
    }
    
    private (byte r, byte g, byte b)? HexToRgb(string hex)
    {
        if (string.IsNullOrEmpty(hex))
            return null;
            
        hex = hex.TrimStart('#');
        if (hex.Length != 6)
            return null;
            
        try
        {
            var r = Convert.ToByte(hex.Substring(0, 2), 16);
            var g = Convert.ToByte(hex.Substring(2, 2), 16);
            var b = Convert.ToByte(hex.Substring(4, 2), 16);
            return (r, g, b);
        }
        catch
        {
            return null;
        }
    }
    
    private string AdjustBrightness(string hex, int percentage)
    {
        var rgb = HexToRgb(hex);
        if (rgb == null)
            return hex;
            
        var r = Clamp((int)(rgb.Value.r + rgb.Value.r * percentage / 100.0), 0, 255);
        var g = Clamp((int)(rgb.Value.g + rgb.Value.g * percentage / 100.0), 0, 255);
        var b = Clamp((int)(rgb.Value.b + rgb.Value.b * percentage / 100.0), 0, 255);
        
        return $"#{r:X2}{g:X2}{b:X2}";
    }
    
    private int Clamp(int value, int min, int max)
    {
        return Math.Max(min, Math.Min(max, value));
    }
}
```

## CSS Customization

The ThemeProvider component manages CSS variables for theming. You can customize these variables in your application's CSS:

```css
/* Light theme customization */
:root, [data-bs-theme=light] {
  --bb-primary: #0d6efd;
  --bb-primary-rgb: 13, 110, 253;
  --bb-primary-hover: #0b5ed7;
  --bb-primary-active: #0a58ca;
  --bb-primary-text: #fff;
  --bb-secondary: #6c757d;
  --bb-secondary-rgb: 108, 117, 125;
  --bb-secondary-hover: #5c636a;
  --bb-secondary-active: #565e64;
  --bb-secondary-text: #fff;
  --bb-success: #198754;
  --bb-success-rgb: 25, 135, 84;
  --bb-success-hover: #157347;
  --bb-success-active: #146c43;
  --bb-success-text: #fff;
  --bb-info: #0dcaf0;
  --bb-info-rgb: 13, 202, 240;
  --bb-info-hover: #31d2f2;
  --bb-info-active: #3dd5f3;
  --bb-info-text: #000;
  --bb-warning: #ffc107;
  --bb-warning-rgb: 255, 193, 7;
  --bb-warning-hover: #ffca2c;
  --bb-warning-active: #ffcd39;
  --bb-warning-text: #000;
  --bb-danger: #dc3545;
  --bb-danger-rgb: 220, 53, 69;
  --bb-danger-hover: #bb2d3b;
  --bb-danger-active: #b02a37;
  --bb-danger-text: #fff;
  --bb-light: #f8f9fa;
  --bb-light-rgb: 248, 249, 250;
  --bb-light-hover: #f9fafb;
  --bb-light-active: #f9fafb;
  --bb-light-text: #000;
  --bb-dark: #212529;
  --bb-dark-rgb: 33, 37, 41;
  --bb-dark-hover: #1c1f23;
  --bb-dark-active: #1a1e21;
  --bb-dark-text: #fff;
}

/* Dark theme customization */
[data-bs-theme=dark] {
  --bb-primary: #0d6efd;
  --bb-primary-rgb: 13, 110, 253;
  --bb-primary-hover: #0b5ed7;
  --bb-primary-active: #0a58ca;
  --bb-primary-text: #fff;
  --bb-secondary: #6c757d;
  --bb-secondary-rgb: 108, 117, 125;
  --bb-secondary-hover: #5c636a;
  --bb-secondary-active: #565e64;
  --bb-secondary-text: #fff;
  --bb-success: #198754;
  --bb-success-rgb: 25, 135, 84;
  --bb-success-hover: #157347;
  --bb-success-active: #146c43;
  --bb-success-text: #fff;
  --bb-info: #0dcaf0;
  --bb-info-rgb: 13, 202, 240;
  --bb-info-hover: #31d2f2;
  --bb-info-active: #3dd5f3;
  --bb-info-text: #000;
  --bb-warning: #ffc107;
  --bb-warning-rgb: 255, 193, 7;
  --bb-warning-hover: #ffca2c;
  --bb-warning-active: #ffcd39;
  --bb-warning-text: #000;
  --bb-danger: #dc3545;
  --bb-danger-rgb: 220, 53, 69;
  --bb-danger-hover: #bb2d3b;
  --bb-danger-active: #b02a37;
  --bb-danger-text: #fff;
  --bb-light: #f8f9fa;
  --bb-light-rgb: 248, 249, 250;
  --bb-light-hover: #f9fafb;
  --bb-light-active: #f9fafb;
  --bb-light-text: #000;
  --bb-dark: #212529;
  --bb-dark-rgb: 33, 37, 41;
  --bb-dark-hover: #1c1f23;
  --bb-dark-active: #1a1e21;
  --bb-dark-text: #fff;
  --bb-body-bg: #212529;
  --bb-body-color: #dee2e6;
}
```

## Notes

### Theme Service Integration
- The ThemeProvider component works with the `ThemeService` to manage themes across the application.
- You can inject the `ThemeService` into any component to programmatically change themes:

```csharp
@inject ThemeService ThemeService

private async Task ChangeTheme(string themeName)
{
    await ThemeService.SetThemeAsync(themeName);
}
```

### Accessibility Considerations
- Ensure sufficient color contrast in custom themes for accessibility compliance.
- Test themes with screen readers to verify proper semantic information is preserved.
- Consider providing high-contrast theme options for users with visual impairments.

### Performance Considerations
- Theme switching is optimized to minimize DOM operations.
- Custom themes with many CSS variables may impact initial load performance.
- Consider lazy-loading custom themes that aren't used immediately.

### Browser Compatibility
- The ThemeProvider relies on CSS variables, which are supported in all modern browsers.
- For older browsers, consider providing fallback styles.

### Integration with Other Components
- All BootstrapBlazor components are designed to work with the ThemeProvider.
- Third-party components may require additional styling to properly respond to theme changes.

### Best Practices
- Place the ThemeProvider at the root of your application for consistent theming.
- Use the `OnThemeChanged` event to synchronize theme state across components.
- Leverage the `ThemeStorage` option to persist user theme preferences.
- Use the `EnableSystemTheme` option to respect user system preferences.
- Create a theme toggle component for easy theme switching:

```razor
@inject ThemeService ThemeService

<div class="theme-toggle">
    <Button Icon="fa-solid fa-sun" OnClick="() => SwitchTheme("light")" Circle="true" Visible="@(currentTheme != "light")" />
    <Button Icon="fa-solid fa-moon" OnClick="() => SwitchTheme("dark")" Circle="true" Visible="@(currentTheme != "dark")" />
</div>

@code {
    private string currentTheme = "light";
    
    protected override async Task OnInitializedAsync()
    {
        currentTheme = await ThemeService.GetThemeAsync();
        ThemeService.OnThemeChanged += HandleThemeChanged;
    }
    
    private async Task SwitchTheme(string theme)
    {
        await ThemeService.SetThemeAsync(theme);
    }
    
    private void HandleThemeChanged(object? sender, string theme)
    {
        currentTheme = theme;
        StateHasChanged();
    }
    
    public void Dispose()
    {
        ThemeService.OnThemeChanged -= HandleThemeChanged;
    }
}
```