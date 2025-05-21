# Affix Component

## Overview
The Affix component in BootstrapBlazor provides a way to make elements stick to a specific position on the page during scrolling. It's particularly useful for creating fixed navigation bars, sidebars, or any element that needs to remain visible as the user scrolls through content. The component monitors scroll events and dynamically applies positioning styles based on the scroll position.

## Key Features
- **Fixed Positioning**: Makes elements stick to a specific position during scrolling
- **Offset Configuration**: Customizable top, bottom, left, and right offsets
- **Target Element Support**: Can be affixed relative to a specific container element
- **Responsive Behavior**: Adapts to different screen sizes and orientations
- **Z-Index Control**: Configurable z-index for proper layering
- **Events**: Provides events for state changes (fixed/unfixed)
- **Conditional Affixing**: Can be conditionally enabled/disabled
- **Custom Classes**: Supports custom CSS classes for different states
- **Animation Support**: Optional transition animations when changing states

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `OffsetTop` | `int?` | `null` | Distance in pixels from the top of the viewport when fixed |
| `OffsetBottom` | `int?` | `null` | Distance in pixels from the bottom of the viewport when fixed |
| `OffsetLeft` | `int?` | `null` | Distance in pixels from the left of the viewport when fixed |
| `OffsetRight` | `int?` | `null` | Distance in pixels from the right of the viewport when fixed |
| `Target` | `string` | `null` | CSS selector for the target container element |
| `ZIndex` | `int` | `100` | Z-index value for the fixed element |
| `Disabled` | `bool` | `false` | When true, disables the affixing behavior |
| `ChildContent` | `RenderFragment` | `null` | Content to be affixed |
| `FixedClass` | `string` | `null` | CSS class to apply when the element is fixed |
| `UnfixedClass` | `string` | `null` | CSS class to apply when the element is not fixed |
| `UseAnimation` | `bool` | `false` | When true, applies transition animations |
| `AnimationDuration` | `int` | `300` | Duration of the transition animation in milliseconds |

## Events

| Event | Description |
| --- | --- |
| `OnFixed` | Triggered when the element becomes fixed |
| `OnUnfixed` | Triggered when the element becomes unfixed |
| `OnOffsetChanged` | Triggered when the offset values change |

## Usage Examples

### Example 1: Basic Affix with Top Offset
```razor
@using BootstrapBlazor.Components

<div style="height: 1500px; padding: 20px;">
    <h1>Affix Component Example</h1>
    <p>Scroll down to see the affixed element.</p>
    
    <div style="height: 200px;"></div>
    
    <Affix OffsetTop="20">
        <div class="demo-affix">
            <p>I am affixed 20px from the top of the viewport</p>
        </div>
    </Affix>
    
    <div style="height: 1000px;"></div>
</div>

<style>
    .demo-affix {
        padding: 10px 15px;
        background-color: #f8f9fa;
        border: 1px solid #dee2e6;
        border-radius: 4px;
        box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
    }
</style>
```

### Example 2: Affix with Bottom Offset
```razor
<div style="height: 1500px; padding: 20px;">
    <h1>Bottom Affix Example</h1>
    <p>Scroll down to see the bottom-affixed element.</p>
    
    <div style="height: 1000px;"></div>
    
    <Affix OffsetBottom="20">
        <div class="demo-affix">
            <p>I am affixed 20px from the bottom of the viewport</p>
        </div>
    </Affix>
    
    <div style="height: 200px;"></div>
</div>
```

### Example 3: Affix with Target Container
```razor
<div style="height: 1500px; padding: 20px;">
    <h1>Affix with Target Container</h1>
    <p>The affixed element will only be fixed within its target container.</p>
    
    <div id="affix-container" style="height: 500px; position: relative; border: 1px dashed #ccc; padding: 20px; margin: 50px 0;">
        <h3>Target Container</h3>
        <p>The affix will only work within this container.</p>
        
        <div style="height: 100px;"></div>
        
        <Affix OffsetTop="20" Target="#affix-container">
            <div class="demo-affix">
                <p>I am affixed within the container</p>
            </div>
        </Affix>
        
        <div style="height: 300px;"></div>
    </div>
    
    <div style="height: 500px;">
        <p>The affixed element is no longer fixed when scrolling here.</p>
    </div>
</div>
```

### Example 4: Affix with Events
```razor
@using BootstrapBlazor.Components

<div style="height: 1500px; padding: 20px;">
    <h1>Affix with Events</h1>
    <p>Current state: @affixState</p>
    
    <div style="height: 200px;"></div>
    
    <Affix OffsetTop="20" OnFixed="HandleFixed" OnUnfixed="HandleUnfixed">
        <div class="demo-affix">
            <p>I am affixed with event handling</p>
        </div>
    </Affix>
    
    <div style="height: 1000px;"></div>
</div>

@code {
    private string affixState = "Unfixed";
    
    private Task HandleFixed()
    {
        affixState = "Fixed";
        StateHasChanged();
        return Task.CompletedTask;
    }
    
    private Task HandleUnfixed()
    {
        affixState = "Unfixed";
        StateHasChanged();
        return Task.CompletedTask;
    }
}
```

### Example 5: Affix with Custom Classes
```razor
<div style="height: 1500px; padding: 20px;">
    <h1>Affix with Custom Classes</h1>
    <p>Scroll down to see the affixed element with custom styling.</p>
    
    <div style="height: 200px;"></div>
    
    <Affix OffsetTop="20" FixedClass="affix-fixed" UnfixedClass="affix-unfixed">
        <div class="demo-affix">
            <p>I have custom classes for fixed and unfixed states</p>
        </div>
    </Affix>
    
    <div style="height: 1000px;"></div>
</div>

<style>
    .affix-fixed {
        transition: all 0.3s ease-in-out;
        background-color: #007bff !important;
        color: white !important;
        padding: 15px !important;
        border-radius: 0 !important;
        width: 100%;
        left: 0;
    }
    
    .affix-unfixed {
        transition: all 0.3s ease-in-out;
        background-color: #f8f9fa;
    }
</style>
```

### Example 6: Conditionally Disabled Affix
```razor
@using BootstrapBlazor.Components

<div style="height: 1500px; padding: 20px;">
    <h1>Conditionally Disabled Affix</h1>
    <p>Toggle the affix behavior using the checkbox.</p>
    
    <div class="mb-3">
        <Checkbox @bind-Value="isAffixDisabled" DisplayText="Disable Affix" />
    </div>
    
    <div style="height: 200px;"></div>
    
    <Affix OffsetTop="20" Disabled="@isAffixDisabled">
        <div class="demo-affix">
            <p>@(isAffixDisabled ? "Affix is disabled" : "Affix is enabled")</p>
        </div>
    </Affix>
    
    <div style="height: 1000px;"></div>
</div>

@code {
    private bool isAffixDisabled = false;
}
```

### Example 7: Affix with Animation and Multiple Instances
```razor
<div style="height: 2000px; padding: 20px;">
    <h1>Multiple Affixed Elements with Animation</h1>
    <p>Scroll down to see multiple affixed elements with animations.</p>
    
    <div style="height: 200px;"></div>
    
    <Affix OffsetTop="20" UseAnimation="true" AnimationDuration="500">
        <div class="demo-affix demo-affix-1">
            <p>Top Navigation</p>
            <div class="d-flex gap-3">
                <Button Color="Color.Primary">Home</Button>
                <Button Color="Color.Secondary">About</Button>
                <Button Color="Color.Success">Contact</Button>
            </div>
        </div>
    </Affix>
    
    <div style="height: 500px;"></div>
    
    <Affix OffsetRight="20" OffsetTop="100" UseAnimation="true" AnimationDuration="500">
        <div class="demo-affix demo-affix-2">
            <p>Quick Actions</p>
            <div class="d-flex flex-column gap-2">
                <Button Color="Color.Info" Icon="fa-solid fa-arrow-up" Circle="true" />
                <Button Color="Color.Warning" Icon="fa-solid fa-star" Circle="true" />
                <Button Color="Color.Danger" Icon="fa-solid fa-heart" Circle="true" />
            </div>
        </div>
    </Affix>
    
    <div style="height: 800px;"></div>
    
    <Affix OffsetBottom="20" UseAnimation="true" AnimationDuration="500">
        <div class="demo-affix demo-affix-3">
            <p>Footer Actions</p>
            <div class="d-flex justify-content-between w-100">
                <Button Color="Color.Primary">Previous</Button>
                <Button Color="Color.Success">Save</Button>
                <Button Color="Color.Primary">Next</Button>
            </div>
        </div>
    </Affix>
</div>

<style>
    .demo-affix {
        padding: 10px 15px;
        background-color: #f8f9fa;
        border: 1px solid #dee2e6;
        border-radius: 4px;
        box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
    }
    
    .demo-affix-1 {
        width: 100%;
        left: 0;
        padding: 15px;
        background-color: white;
        z-index: 1000;
    }
    
    .demo-affix-2 {
        padding: 15px;
        background-color: white;
    }
    
    .demo-affix-3 {
        width: 100%;
        left: 0;
        padding: 15px;
        background-color: white;
        z-index: 1000;
    }
</style>
```

## CSS Customization

The Affix component can be customized using CSS variables:

```css
/* Affix custom styling */
.bb-affix {
  --bb-affix-transition: all 0.3s ease-in-out;
  --bb-affix-z-index: 100;
  --bb-affix-background: transparent;
  --bb-affix-shadow: none;
  --bb-affix-fixed-background: white;
  --bb-affix-fixed-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
}

/* Custom affix states */
.bb-affix-fixed {
  transition: var(--bb-affix-transition);
  background-color: var(--bb-affix-fixed-background);
  box-shadow: var(--bb-affix-fixed-shadow);
}

.bb-affix-unfixed {
  transition: var(--bb-affix-transition);
  background-color: var(--bb-affix-background);
  box-shadow: var(--bb-affix-shadow);
}
```

## Notes

### JavaScript Interop
- The Affix component uses JavaScript interop to monitor scroll events and apply positioning.
- Ensure that JavaScript is enabled in the browser for the component to function properly.

### Performance Considerations
- Scroll event handlers are throttled to optimize performance, but excessive use of Affix components on a single page may impact scrolling performance.
- Consider using the `Target` property to limit the scope of scroll event monitoring when possible.

### Accessibility
- When using the Affix component for navigation elements, ensure that the content remains accessible to all users, including those using screen readers.
- Provide sufficient contrast between the affixed element and its background.

### Mobile Considerations
- Test the Affix component on various mobile devices to ensure proper behavior on touch screens.
- Consider using different offset values for different screen sizes using media queries.

### Integration with Other Components
- The Affix component works well with other BootstrapBlazor components like Menu, Tabs, and Button.
- When using Affix with the Layout component, be mindful of z-index values to ensure proper layering.

### Best Practices
- Use the Affix component sparingly and only when it enhances the user experience.
- Avoid affixing large or complex elements that may cause layout shifts when becoming fixed.
- Provide visual cues to indicate when an element becomes affixed, especially for navigation elements.
- Consider the impact on mobile users and ensure that affixed elements don't obscure important content on smaller screens.