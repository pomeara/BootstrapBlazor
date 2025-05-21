# Button Component

## Overview
The Button component in BootstrapBlazor provides a customizable button control that supports various styles, sizes, and states. It's a fundamental UI element used for triggering actions, submitting forms, and navigating between pages. The component offers extensive customization options and integrates seamlessly with other BootstrapBlazor components.

## Key Features
- **Multiple Button Styles**: Supports various visual styles including primary, secondary, success, danger, warning, info, light, dark, and link
- **Size Variations**: Available in different sizes (small, medium, large)
- **Button Types**: Supports regular, outline, and text button types
- **Icon Support**: Can display icons before or after button text
- **Loading State**: Built-in loading state with spinner animation
- **Disabled State**: Can be disabled to prevent user interaction
- **Block Display**: Can span the full width of its container
- **Badge Integration**: Supports badge indicators
- **Tooltip Support**: Can display tooltips on hover
- **Keyboard Accessibility**: Full keyboard navigation support

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `Color` | `Color` | `Primary` | Sets the button color (Primary, Secondary, Success, Danger, Warning, Info, Dark, Light) |
| `ButtonType` | `ButtonType` | `Button` | Sets the button type (Button, Submit, Reset) |
| `ButtonStyle` | `ButtonStyle` | `None` | Sets the button style (None, Outline, Text) |
| `Size` | `Size` | `None` | Sets the button size (None, Small, Medium, Large) |
| `IsDisabled` | `bool` | `false` | When true, disables the button |
| `IsLoading` | `bool` | `false` | When true, displays a loading spinner |
| `IsBlock` | `bool` | `false` | When true, makes the button span the full width of its container |
| `Icon` | `string` | `null` | Sets the icon to display (supports Font Awesome and Bootstrap icons) |
| `IconRight` | `string` | `null` | Sets the icon to display on the right side of the text |
| `Text` | `string` | `null` | Sets the button text |
| `LoadingText` | `string` | `null` | Sets the text to display when the button is in loading state |
| `ChildContent` | `RenderFragment` | `null` | Sets the content of the button |
| `OnClick` | `EventCallback<MouseEventArgs>` | `null` | Event callback for button click |
| `OnClickWithoutRender` | `Func<Task>` | `null` | Event callback for button click without triggering a re-render |
| `StopPropagation` | `bool` | `false` | When true, stops event propagation |
| `PreventDefault` | `bool` | `false` | When true, prevents the default action |
| `IsAsync` | `bool` | `false` | When true, handles the click event asynchronously |
| `AsyncDelay` | `int` | `200` | Sets the delay in milliseconds for async operations |

## Events

| Event | Description |
| --- | --- |
| `OnClick` | Triggered when the button is clicked |
| `OnClickWithoutRender` | Triggered when the button is clicked, but doesn't cause a component re-render |

## Usage Examples

### Example 1: Basic Button
```razor
@using BootstrapBlazor.Components

<Button Color="Color.Primary">Primary Button</Button>
<Button Color="Color.Secondary">Secondary Button</Button>
<Button Color="Color.Success">Success Button</Button>
<Button Color="Color.Danger">Danger Button</Button>
<Button Color="Color.Warning">Warning Button</Button>
<Button Color="Color.Info">Info Button</Button>
<Button Color="Color.Dark">Dark Button</Button>
<Button Color="Color.Light">Light Button</Button>
```

### Example 2: Button Styles
```razor
<Button Color="Color.Primary">Default Button</Button>
<Button Color="Color.Primary" ButtonStyle="ButtonStyle.Outline">Outline Button</Button>
<Button Color="Color.Primary" ButtonStyle="ButtonStyle.Text">Text Button</Button>
```

### Example 3: Button Sizes
```razor
<Button Color="Color.Primary" Size="Size.Small">Small Button</Button>
<Button Color="Color.Primary">Default Size Button</Button>
<Button Color="Color.Primary" Size="Size.Large">Large Button</Button>
```

### Example 4: Buttons with Icons
```razor
<Button Color="Color.Primary" Icon="fa-solid fa-save">Save</Button>
<Button Color="Color.Danger" Icon="fa-solid fa-trash">Delete</Button>
<Button Color="Color.Success" IconRight="fa-solid fa-arrow-right">Next</Button>
<Button Color="Color.Info" Icon="fa-solid fa-info-circle" />  @* Icon-only button *@
```

### Example 5: Loading and Disabled States
```razor
<Button Color="Color.Primary" IsLoading="true">Loading Button</Button>
<Button Color="Color.Primary" IsLoading="true" LoadingText="Processing...">Submit</Button>
<Button Color="Color.Primary" IsDisabled="true">Disabled Button</Button>

@code {
    private bool isLoading = false;
    
    private async Task HandleClick()
    {
        isLoading = true;
        await Task.Delay(2000); // Simulate an operation
        isLoading = false;
    }
}

<Button Color="Color.Primary" IsLoading="@isLoading" OnClick="HandleClick">
    @(isLoading ? "Processing..." : "Click Me")
</Button>
```

### Example 6: Block Button and Button Groups
```razor
<Button Color="Color.Primary" IsBlock="true">Block Button</Button>

<div class="btn-group">
    <Button Color="Color.Primary">Left</Button>
    <Button Color="Color.Primary">Middle</Button>
    <Button Color="Color.Primary">Right</Button>
</div>

<div class="btn-group-vertical">
    <Button Color="Color.Primary">Top</Button>
    <Button Color="Color.Primary">Middle</Button>
    <Button Color="Color.Primary">Bottom</Button>
</div>
```

### Example 7: Button with Event Handling
```razor
@using BootstrapBlazor.Components

<Button Color="Color.Primary" OnClick="HandleButtonClick">Click Me</Button>
<Button Color="Color.Secondary" OnClickWithoutRender="HandleClickWithoutRender">Click Without Render</Button>
<Button Color="Color.Success" IsAsync="true" OnClick="HandleAsyncClick">Async Click</Button>

<div>Click Count: @clickCount</div>

@code {
    private int clickCount = 0;
    
    private void HandleButtonClick(MouseEventArgs args)
    {
        clickCount++;
    }
    
    private async Task HandleClickWithoutRender()
    {
        // This won't trigger a component re-render
        await Task.Delay(100);
        Console.WriteLine("Button clicked without render");
    }
    
    private async Task HandleAsyncClick(MouseEventArgs args)
    {
        // Button will show loading state automatically
        await Task.Delay(1000);
        clickCount++;
    }
}
```

## CSS Customization

The Button component can be customized using CSS variables:

```css
/* Button custom styling */
.btn {
  --bb-button-padding-y: 0.5rem;
  --bb-button-padding-x: 1rem;
  --bb-button-font-size: 1rem;
  --bb-button-border-radius: 0.25rem;
  --bb-button-transition: all 0.2s ease-in-out;
  --bb-button-focus-box-shadow: 0 0 0 0.25rem rgba(13, 110, 253, 0.25);
  
  /* Primary button variables */
  --bb-button-primary-color: #fff;
  --bb-button-primary-bg: #0d6efd;
  --bb-button-primary-border-color: #0d6efd;
  --bb-button-primary-hover-color: #fff;
  --bb-button-primary-hover-bg: #0b5ed7;
  --bb-button-primary-hover-border-color: #0a58ca;
  
  /* Success button variables */
  --bb-button-success-color: #fff;
  --bb-button-success-bg: #198754;
  --bb-button-success-border-color: #198754;
  
  /* Danger button variables */
  --bb-button-danger-color: #fff;
  --bb-button-danger-bg: #dc3545;
  --bb-button-danger-border-color: #dc3545;
}

/* Custom button size */
.btn-custom-size {
  --bb-button-padding-y: 0.75rem;
  --bb-button-padding-x: 2rem;
  --bb-button-font-size: 1.25rem;
}
```

## Notes

### Accessibility
- The Button component is fully accessible and supports keyboard navigation.
- Use appropriate colors to ensure sufficient contrast for users with visual impairments.
- When using icon-only buttons, provide an `aria-label` attribute for screen readers.

### Performance
- For buttons that trigger expensive operations, consider using `IsAsync="true"` to automatically handle the loading state.
- Use `OnClickWithoutRender` for operations that don't need to update the UI to avoid unnecessary renders.

### Integration with Other Components
- The Button component works seamlessly with other BootstrapBlazor components like Modal, Drawer, and Form.
- Use the Button component inside a Form component to create submit and reset buttons.

### Best Practices
- Use appropriate colors to convey the button's purpose (e.g., green for success, red for danger).
- Include icons to enhance visual recognition.
- Provide feedback for long-running operations using the loading state.
- Keep button text concise and action-oriented.
- Group related buttons together using button groups.
- Use consistent button styles throughout your application for a cohesive user experience.