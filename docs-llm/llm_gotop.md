# GoTop Component

## Overview
The GoTop component in BootstrapBlazor provides a convenient way for users to quickly scroll back to the top of a page. It's especially useful for long pages where users might need to navigate back to the top frequently. The component appears when the user scrolls down a certain distance and disappears when near the top of the page, offering a non-intrusive user experience.

## Key Features
- **Automatic Visibility**: Appears automatically when scrolling down and disappears when near the top
- **Customizable Appearance**: Supports custom icons, colors, and styles
- **Smooth Scrolling**: Provides smooth animation when scrolling back to top
- **Positioning Options**: Can be positioned at different corners of the viewport
- **Responsive Design**: Works well on both desktop and mobile devices
- **Customizable Trigger Distance**: Configure when the button appears based on scroll distance
- **Accessibility Support**: Includes proper ARIA attributes for screen readers
- **Animation Effects**: Optional entrance and exit animations
- **Custom Content**: Supports custom content instead of the default icon

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `Target` | `string` | `window` | The target element to monitor for scrolling (CSS selector) |
| `Text` | `string` | `null` | Text to display alongside or instead of the icon |
| `Icon` | `string` | `fa-solid fa-arrow-up` | Icon to display (supports Font Awesome and Bootstrap icons) |
| `Color` | `Color` | `Primary` | Color theme of the button |
| `Placement` | `Placement` | `BottomRight` | Position of the button (TopLeft, TopRight, BottomLeft, BottomRight) |
| `Offset` | `int` | `50` | Distance from the edge of the viewport in pixels |
| `ScrollTop` | `int` | `0` | Target scroll position (0 for the very top) |
| `Visible` | `bool` | `false` | Initial visibility state |
| `VisibleScrollTop` | `int` | `300` | Scroll distance at which the button becomes visible |
| `IsAnimation` | `bool` | `true` | Whether to use smooth scrolling animation |
| `AnimationDuration` | `int` | `500` | Duration of the scroll animation in milliseconds |
| `IsFixedFooter` | `bool` | `false` | When true, adjusts position to account for fixed footers |
| `FooterHeight` | `int` | `0` | Height of the fixed footer in pixels |
| `ChildContent` | `RenderFragment` | `null` | Custom content to display inside the button |

## Events

| Event | Description |
| --- | --- |
| `OnClick` | Triggered when the GoTop button is clicked |
| `OnBeforeScroll` | Triggered before scrolling begins |
| `OnAfterScroll` | Triggered after scrolling completes |

## Usage Examples

### Example 1: Basic GoTop Button
```razor
@using BootstrapBlazor.Components

<div style="height: 2000px; padding: 20px;">
    <h1>Scroll down to see the GoTop button</h1>
    <p>The GoTop button will appear when you scroll down.</p>
    
    <GoTop />
</div>
```

### Example 2: Customized GoTop Button
```razor
<div style="height: 2000px; padding: 20px;">
    <h1>Customized GoTop Button</h1>
    <p>Scroll down to see a customized GoTop button.</p>
    
    <GoTop Icon="fa-solid fa-rocket"
           Text="Top"
           Color="Color.Danger"
           Placement="Placement.BottomLeft"
           Offset="30"
           VisibleScrollTop="200"
           AnimationDuration="800" />
</div>
```

### Example 3: GoTop with Custom Content
```razor
<div style="height: 2000px; padding: 20px;">
    <h1>GoTop with Custom Content</h1>
    <p>Scroll down to see a GoTop button with custom content.</p>
    
    <GoTop>
        <div class="custom-gotop">
            <i class="fa-solid fa-arrow-up"></i>
            <span>Back to Top</span>
        </div>
    </GoTop>
    
    <style>
        .custom-gotop {
            display: flex;
            flex-direction: column;
            align-items: center;
            padding: 8px;
        }
        
        .custom-gotop i {
            font-size: 1.25rem;
            margin-bottom: 4px;
        }
        
        .custom-gotop span {
            font-size: 0.75rem;
        }
    </style>
</div>
```

### Example 4: GoTop with Events
```razor
@using BootstrapBlazor.Components

<div style="height: 2000px; padding: 20px;">
    <h1>GoTop with Events</h1>
    <p>Scroll down and use the GoTop button to see event handling.</p>
    <p>Status: @scrollStatus</p>
    
    <GoTop OnClick="HandleClick"
           OnBeforeScroll="HandleBeforeScroll"
           OnAfterScroll="HandleAfterScroll" />
</div>

@code {
    private string scrollStatus = "Idle";
    
    private Task HandleClick()
    {
        scrollStatus = "Button Clicked";
        return Task.CompletedTask;
    }
    
    private Task HandleBeforeScroll()
    {
        scrollStatus = "Scrolling Started";
        return Task.CompletedTask;
    }
    
    private Task HandleAfterScroll()
    {
        scrollStatus = "Scrolling Completed";
        return Task.CompletedTask;
    }
}
```

### Example 5: GoTop with Custom Target
```razor
<div class="container">
    <div id="scrollableDiv" style="height: 400px; overflow-y: auto; border: 1px solid #ccc; padding: 20px;">
        <h3>Scrollable Container</h3>
        <p>This is a scrollable container with its own GoTop button.</p>
        
        @for (int i = 1; i <= 20; i++)
        {
            <div class="mb-4">
                <h4>Section @i</h4>
                <p>This is section @i content. Scroll down to see more sections.</p>
            </div>
        }
    </div>
    
    <GoTop Target="#scrollableDiv" VisibleScrollTop="100" Offset="20" />
</div>
```

### Example 6: GoTop with Fixed Footer Adjustment
```razor
<div style="height: 2000px; padding: 20px;">
    <h1>GoTop with Fixed Footer</h1>
    <p>Scroll down to see how the GoTop button adjusts for a fixed footer.</p>
    
    <GoTop IsFixedFooter="true" FooterHeight="60" />
    
    <footer style="position: fixed; bottom: 0; left: 0; right: 0; height: 60px; background-color: #f8f9fa; border-top: 1px solid #dee2e6; padding: 1rem; text-align: center;">
        Fixed Footer
    </footer>
</div>
```

### Example 7: Programmatically Controlled GoTop
```razor
@using BootstrapBlazor.Components

<div style="height: 2000px; padding: 20px;">
    <h1>Programmatically Controlled GoTop</h1>
    <p>This example shows how to programmatically control the GoTop component.</p>
    
    <Button Color="Color.Primary" OnClick="ScrollToPosition">Scroll to Middle</Button>
    <Button Color="Color.Success" OnClick="ShowGoTop">Show GoTop</Button>
    <Button Color="Color.Danger" OnClick="HideGoTop">Hide GoTop</Button>
    
    <GoTop @ref="goTopRef" Visible="@isVisible" />
</div>

@code {
    private GoTop goTopRef;
    private bool isVisible = false;
    
    private async Task ScrollToPosition()
    {
        // Scroll to the middle of the page
        await JS.InvokeVoidAsync("window.scrollTo", new { top = 1000, behavior = "smooth" });
    }
    
    private void ShowGoTop()
    {
        isVisible = true;
    }
    
    private void HideGoTop()
    {
        isVisible = false;
    }
}
```

## CSS Customization

The GoTop component can be customized using CSS variables:

```css
/* GoTop custom styling */
.gotop {
  --bb-gotop-size: 3rem;
  --bb-gotop-border-radius: 50%;
  --bb-gotop-background: rgba(13, 110, 253, 0.8);
  --bb-gotop-color: #fff;
  --bb-gotop-hover-background: rgba(13, 110, 253, 1);
  --bb-gotop-hover-color: #fff;
  --bb-gotop-transition: all 0.3s ease-in-out;
  --bb-gotop-shadow: 0 2px 5px rgba(0, 0, 0, 0.2);
  --bb-gotop-z-index: 1050;
  --bb-gotop-offset: 1.5rem;
}

/* Custom GoTop colors */
.gotop-primary {
  --bb-gotop-background: rgba(13, 110, 253, 0.8);
  --bb-gotop-hover-background: rgba(13, 110, 253, 1);
}

.gotop-success {
  --bb-gotop-background: rgba(25, 135, 84, 0.8);
  --bb-gotop-hover-background: rgba(25, 135, 84, 1);
}

.gotop-danger {
  --bb-gotop-background: rgba(220, 53, 69, 0.8);
  --bb-gotop-hover-background: rgba(220, 53, 69, 1);
}

/* Custom GoTop size */
.gotop-lg {
  --bb-gotop-size: 4rem;
  --bb-gotop-icon-size: 1.5rem;
}

.gotop-sm {
  --bb-gotop-size: 2.5rem;
  --bb-gotop-icon-size: 0.875rem;
}
```

## Notes

### Accessibility
- The GoTop component includes appropriate ARIA attributes for screen readers.
- The default button includes an `aria-label="Go to top"` attribute to describe its purpose.
- When using custom content, ensure to include appropriate ARIA attributes for accessibility.

### Performance
- The GoTop component uses event throttling to optimize scroll event handling.
- For pages with complex DOM structures, consider using a higher `VisibleScrollTop` value to reduce unnecessary visibility toggling.

### Mobile Considerations
- On mobile devices, consider using a larger button size for better touch targets.
- Test the component on various mobile devices to ensure it doesn't interfere with important content.

### Integration with Layout Component
- When using the GoTop component with the BootstrapBlazor Layout component, set the `IsFixedFooter` property to `true` if the layout has a fixed footer.
- Adjust the `FooterHeight` property to match the height of the fixed footer.

### JavaScript Interop
- The GoTop component uses JavaScript interop for scroll detection and smooth scrolling.
- Ensure that JavaScript is enabled in the browser for the component to function properly.

### Best Practices
- Use the GoTop component sparingly and only on long pages where scrolling back to the top would be beneficial.
- Position the button where it won't interfere with important content.
- Use a semi-transparent background to ensure the button doesn't completely obscure content underneath.
- Consider using a recognizable icon like an upward arrow for better user understanding.
- Provide a smooth scrolling experience by keeping the default animation enabled.