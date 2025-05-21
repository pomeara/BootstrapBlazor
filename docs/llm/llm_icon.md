# Icon Component

## Overview
The Icon component in BootstrapBlazor provides a versatile way to display icons from various icon libraries including Font Awesome, Bootstrap Icons, and Material Design Icons. It offers a simple and consistent API for rendering icons with customizable size, color, and animation effects. The component is lightweight and can be used throughout the application to enhance visual communication and user experience.

## Key Features
- **Multiple Icon Libraries**: Supports Font Awesome, Bootstrap Icons, Material Design Icons, and custom icon sets
- **Customizable Size**: Easily adjust icon size through predefined size options or custom sizing
- **Color Customization**: Apply custom colors to icons
- **Animation Effects**: Built-in support for spinning, pulsing, and other animation effects
- **Rotation and Flipping**: Rotate and flip icons in different directions
- **Stacking**: Combine multiple icons to create composite icons
- **Text Integration**: Combine icons with text for enhanced visual communication
- **Accessibility Support**: Proper ARIA attributes for screen readers
- **CSS Variables**: Customize appearance using CSS variables

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `IconName` | `string` | `null` | The name of the icon to display (e.g., "fa-solid fa-user") |
| `Size` | `IconSize` | `IconSize.None` | Predefined size of the icon (None, ExtraSmall, Small, Medium, Large, ExtraLarge) |
| `Color` | `string` | `null` | Custom color for the icon (CSS color value) |
| `Width` | `string` | `null` | Custom width for the icon |
| `Height` | `string` | `null` | Custom height for the icon |
| `Spin` | `bool` | `false` | When true, applies a spinning animation to the icon |
| `Pulse` | `bool` | `false` | When true, applies a pulsing animation to the icon |
| `Rotate` | `int` | `0` | Rotation angle in degrees (0, 90, 180, 270) |
| `Flip` | `IconFlip` | `IconFlip.None` | Flip direction (None, Horizontal, Vertical, Both) |
| `ChildContent` | `RenderFragment` | `null` | Content to display inside the icon (for stacked icons) |
| `Text` | `string` | `null` | Text to display alongside the icon |
| `TextPosition` | `Placement` | `Placement.Right` | Position of the text relative to the icon (Top, Right, Bottom, Left) |
| `OnClick` | `EventCallback<MouseEventArgs>` | `null` | Event callback for icon click |
| `IsLink` | `bool` | `false` | When true, renders the icon as a clickable link |
| `IsFixWidth` | `bool` | `false` | When true, applies fixed width to the icon |
| `IsBackground` | `bool` | `false` | When true, renders the icon as a background image |
| `Border` | `bool` | `false` | When true, adds a border around the icon |
| `Inverse` | `bool` | `false` | When true, inverts the icon colors |

## Events

| Event | Description |
| --- | --- |
| `OnClick` | Triggered when the icon is clicked |

## Usage Examples

### Example 1: Basic Icons from Different Libraries
```razor
@using BootstrapBlazor.Components

<div class="icon-demo">
    <!-- Font Awesome Icons -->
    <Icon IconName="fa-solid fa-user" />
    <Icon IconName="fa-regular fa-bell" />
    <Icon IconName="fa-brands fa-github" />
    
    <!-- Bootstrap Icons -->
    <Icon IconName="bi-alarm" />
    <Icon IconName="bi-calendar" />
    <Icon IconName="bi-chat" />
    
    <!-- Material Design Icons -->
    <Icon IconName="mdi-account" />
    <Icon IconName="mdi-email" />
    <Icon IconName="mdi-home" />
</div>
```

### Example 2: Icon Sizes
```razor
<div class="icon-size-demo">
    <Icon IconName="fa-solid fa-star" Size="IconSize.ExtraSmall" />
    <Icon IconName="fa-solid fa-star" Size="IconSize.Small" />
    <Icon IconName="fa-solid fa-star" Size="IconSize.Medium" />
    <Icon IconName="fa-solid fa-star" Size="IconSize.Large" />
    <Icon IconName="fa-solid fa-star" Size="IconSize.ExtraLarge" />
    
    <!-- Custom size using Width and Height -->
    <Icon IconName="fa-solid fa-star" Width="3rem" Height="3rem" />
</div>
```

### Example 3: Icon Colors
```razor
<div class="icon-color-demo">
    <Icon IconName="fa-solid fa-heart" Color="red" />
    <Icon IconName="fa-solid fa-circle-check" Color="#28a745" />
    <Icon IconName="fa-solid fa-circle-exclamation" Color="#ffc107" />
    <Icon IconName="fa-solid fa-circle-xmark" Color="#dc3545" />
    <Icon IconName="fa-solid fa-circle-info" Color="#17a2b8" />
</div>
```

### Example 4: Icon Animations
```razor
<div class="icon-animation-demo">
    <Icon IconName="fa-solid fa-spinner" Spin="true" />
    <Icon IconName="fa-solid fa-circle-notch" Spin="true" />
    <Icon IconName="fa-solid fa-sync" Spin="true" />
    <Icon IconName="fa-solid fa-cog" Spin="true" />
    <Icon IconName="fa-solid fa-spinner" Pulse="true" />
</div>
```

### Example 5: Icon Rotation and Flipping
```razor
<div class="icon-transform-demo">
    <!-- Rotation -->
    <Icon IconName="fa-solid fa-arrow-up" />
    <Icon IconName="fa-solid fa-arrow-up" Rotate="90" />
    <Icon IconName="fa-solid fa-arrow-up" Rotate="180" />
    <Icon IconName="fa-solid fa-arrow-up" Rotate="270" />
    
    <!-- Flipping -->
    <Icon IconName="fa-solid fa-car" />
    <Icon IconName="fa-solid fa-car" Flip="IconFlip.Horizontal" />
    <Icon IconName="fa-solid fa-car" Flip="IconFlip.Vertical" />
    <Icon IconName="fa-solid fa-car" Flip="IconFlip.Both" />
</div>
```

### Example 6: Icons with Text
```razor
<div class="icon-text-demo">
    <Icon IconName="fa-solid fa-home" Text="Home" />
    <Icon IconName="fa-solid fa-envelope" Text="Messages" TextPosition="Placement.Right" />
    <Icon IconName="fa-solid fa-arrow-up" Text="Upload" TextPosition="Placement.Bottom" />
    <Icon IconName="fa-solid fa-arrow-down" Text="Download" TextPosition="Placement.Top" />
    <Icon IconName="fa-solid fa-user" Text="Profile" TextPosition="Placement.Left" />
</div>
```

### Example 7: Interactive Icons and Icon Stacking
```razor
@using BootstrapBlazor.Components

<div class="icon-interactive-demo">
    <!-- Clickable Icon -->
    <Icon IconName="fa-solid fa-bell" IsLink="true" OnClick="@HandleIconClick" />
    
    <!-- Stacked Icons -->
    <span class="fa-stack fa-2x">
        <Icon IconName="fa-solid fa-square" class="fa-stack-2x" Color="#17a2b8" />
        <Icon IconName="fa-solid fa-flag" class="fa-stack-1x" Color="white" />
    </span>
    
    <span class="fa-stack fa-2x">
        <Icon IconName="fa-solid fa-circle" class="fa-stack-2x" Color="#dc3545" />
        <Icon IconName="fa-solid fa-ban" class="fa-stack-1x" Color="white" />
    </span>
    
    <!-- Fixed Width Icons in a List -->
    <ul class="fa-ul">
        <li><Icon IconName="fa-solid fa-check" IsFixWidth="true" /> Item 1</li>
        <li><Icon IconName="fa-solid fa-check" IsFixWidth="true" /> Item 2</li>
        <li><Icon IconName="fa-solid fa-times" IsFixWidth="true" /> Item 3</li>
    </ul>
</div>

@code {
    private void HandleIconClick(MouseEventArgs args)
    {
        // Handle icon click event
        Console.WriteLine("Icon clicked!");
    }
}
```

## CSS Customization

The Icon component can be customized using CSS variables:

```css
/* Icon custom styling */
.bb-icon {
  --bb-icon-size-xs: 0.75rem;
  --bb-icon-size-sm: 0.875rem;
  --bb-icon-size-md: 1rem;
  --bb-icon-size-lg: 1.25rem;
  --bb-icon-size-xl: 1.5rem;
  --bb-icon-transition: all 0.2s ease-in-out;
  --bb-icon-spacing: 0.5rem;
  --bb-icon-border-radius: 0.25rem;
  --bb-icon-border-color: #dee2e6;
  --bb-icon-border-width: 1px;
}

/* Custom icon colors */
.bb-icon-primary {
  color: var(--bs-primary);
}

.bb-icon-secondary {
  color: var(--bs-secondary);
}

.bb-icon-success {
  color: var(--bs-success);
}

.bb-icon-danger {
  color: var(--bs-danger);
}

/* Custom animation speed */
.bb-icon-spin {
  animation-duration: var(--bb-icon-spin-duration, 2s);
}

.bb-icon-pulse {
  animation-duration: var(--bb-icon-pulse-duration, 1s);
}
```

## Notes

### Icon Libraries
- The Icon component supports multiple icon libraries, but you need to include the appropriate CSS files for each library in your application.
- For Font Awesome, include the Font Awesome CSS file in your application's `_Host.cshtml` or `index.html` file.
- For Bootstrap Icons, include the Bootstrap Icons CSS file.
- For Material Design Icons, include the Material Design Icons CSS file.

### Accessibility
- When using icons for interactive elements, always provide appropriate ARIA attributes for screen readers.
- For decorative icons, use `aria-hidden="true"` to hide them from screen readers.
- For functional icons without text, provide an `aria-label` attribute to describe the icon's purpose.

### Performance
- Icons can impact performance if overused. Use them judiciously and consider using SVG icons for better performance.
- For applications with many icons, consider using icon sprites or icon fonts to reduce HTTP requests.

### Best Practices
- Use consistent icons throughout your application for a cohesive user experience.
- Choose appropriate icon sizes based on their context and importance.
- Use color to convey meaning, but don't rely solely on color for important information.
- Combine icons with text for better usability, especially for navigation and action buttons.
- Use animations sparingly to avoid distracting users.

### Integration with Other Components
- The Icon component works seamlessly with other BootstrapBlazor components like Button, Alert, and Menu.
- Use the Icon component inside buttons to create icon buttons or buttons with both text and icons.
- Use the Icon component in navigation menus to enhance visual recognition.