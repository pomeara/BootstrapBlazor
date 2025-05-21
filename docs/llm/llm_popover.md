# Popover Component

## Overview
The Popover component in BootstrapBlazor provides a way to display additional content that appears when a user clicks or hovers over a trigger element. It's commonly used for displaying contextual information, tooltips with rich content, form validation messages, or any supplementary information that doesn't need to be visible at all times. Popovers help keep the interface clean while still providing access to additional details when needed.

## Features
- **Multiple Trigger Options**: Click, hover, focus, or manual triggering
- **Customizable Placement**: Top, right, bottom, left positioning with start/end variations
- **Rich Content Support**: Can contain text, HTML, forms, or other components
- **Header and Body Sections**: Structured content areas with optional titles
- **Dismissible Options**: Close on outside click, escape key, or manually
- **Animation Effects**: Smooth entrance and exit animations
- **Backdrop Support**: Optional backdrop for modal-like experience
- **Custom Styling**: Extensive theming and styling options
- **Dynamic Positioning**: Automatically adjusts position based on available space
- **Accessibility Support**: Proper ARIA attributes for screen readers
- **Programmatic Control**: Methods to show, hide, or toggle the popover

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `Title` | `string` | `null` | Title text for the popover header |
| `Content` | `string` | `null` | Text content for the popover body |
| `Placement` | `Placement` | `Placement.Auto` | Position of the popover relative to the trigger element |
| `Trigger` | `Trigger` | `Trigger.Click` | Event that causes the popover to show (Click, Hover, Focus, Manual) |
| `IsHtml` | `bool` | `false` | Whether the content contains HTML that should be rendered |
| `IsAnimation` | `bool` | `true` | Whether to show animation effects when showing/hiding |
| `IsBackdrop` | `bool` | `false` | Whether to show a backdrop behind the popover |
| `Delay` | `int` | `0` | Delay in milliseconds before showing the popover |
| `HideDelay` | `int` | `0` | Delay in milliseconds before hiding the popover |
| `IsArrow` | `bool` | `true` | Whether to show an arrow pointing to the trigger element |
| `IsAutoHide` | `bool` | `true` | Whether the popover should automatically hide when clicked outside |
| `IsDisabled` | `bool` | `false` | Whether the popover functionality is disabled |
| `HeaderTemplate` | `RenderFragment` | `null` | Custom template for the popover header |
| `BodyTemplate` | `RenderFragment` | `null` | Custom template for the popover body |
| `ChildContent` | `RenderFragment` | `null` | Content of the trigger element |

## Events

| Event | Description |
| --- | --- |
| `OnShow` | Triggered before the popover is shown |
| `OnShown` | Triggered after the popover is fully shown |
| `OnHide` | Triggered before the popover begins to hide |
| `OnHidden` | Triggered after the popover is fully hidden |
| `OnVisibleChanged` | Triggered when the visibility state changes |

## Usage Examples

### Example 1: Basic Popover

```razor
<Popover Title="Simple Popover" Content="This is a basic popover with some text content.">
    <Button>Click me</Button>
</Popover>
```

This example shows a basic popover that appears when the button is clicked. It has a title and simple text content.

### Example 2: Popover with Different Placements

```razor
<div class="d-flex justify-content-between">
    <Popover Title="Top Placement" Content="This popover appears above the button." Placement="Placement.Top">
        <Button>Top</Button>
    </Popover>
    
    <Popover Title="Right Placement" Content="This popover appears to the right of the button." Placement="Placement.Right">
        <Button>Right</Button>
    </Popover>
    
    <Popover Title="Bottom Placement" Content="This popover appears below the button." Placement="Placement.Bottom">
        <Button>Bottom</Button>
    </Popover>
    
    <Popover Title="Left Placement" Content="This popover appears to the left of the button." Placement="Placement.Left">
        <Button>Left</Button>
    </Popover>
</div>
```

This example demonstrates popovers with different placement options, showing how they can appear in different positions relative to the trigger element.

### Example 3: Popover with HTML Content

```razor
<Popover IsHtml="true" Title="Formatted Content" Content="<p>This content has <strong>HTML formatting</strong> and can include <span class='text-danger'>styled text</span>.</p><ul><li>List item 1</li><li>List item 2</li></ul>">
    <Button>Show HTML Content</Button>
</Popover>
```

This example shows a popover with HTML content, allowing for rich formatting within the popover body.

### Example 4: Popover with Custom Templates

```razor
<Popover>
    <HeaderTemplate>
        <div class="d-flex align-items-center">
            <i class="fa fa-info-circle me-2"></i>
            <span>Custom Header</span>
        </div>
    </HeaderTemplate>
    <BodyTemplate>
        <div>
            <p>This popover has custom templates for both the header and body.</p>
            <div class="progress mt-2">
                <div class="progress-bar" role="progressbar" style="width: 75%" aria-valuenow="75" aria-valuemin="0" aria-valuemax="100">75%</div>
            </div>
            <div class="mt-2">
                <Button Size="Size.Small" Color="Color.Primary">Action</Button>
            </div>
        </div>
    </BodyTemplate>
    <Button>Custom Templates</Button>
</Popover>
```

This example demonstrates a popover with custom templates for both the header and body, allowing for complex content and interactive elements within the popover.

### Example 5: Popover with Different Triggers

```razor
<div class="d-flex gap-3">
    <Popover Title="Click Trigger" Content="This popover appears when you click the button." Trigger="Trigger.Click">
        <Button>Click Me</Button>
    </Popover>
    
    <Popover Title="Hover Trigger" Content="This popover appears when you hover over the button." Trigger="Trigger.Hover">
        <Button>Hover Me</Button>
    </Popover>
    
    <Popover Title="Focus Trigger" Content="This popover appears when the input receives focus." Trigger="Trigger.Focus">
        <Input Placeholder="Focus Me" />
    </Popover>
</div>
```

This example shows popovers with different trigger options, demonstrating how they can be activated by different user interactions.

### Example 6: Popover with Backdrop

```razor
<Popover Title="Important Information" IsBackdrop="true" Placement="Placement.Bottom">
    <HeaderTemplate>
        <div class="text-danger">Important Notice</div>
    </HeaderTemplate>
    <BodyTemplate>
        <div>
            <p>This popover has a backdrop that dims the rest of the page, similar to a modal dialog.</p>
            <p>It helps focus the user's attention on the popover content.</p>
            <div class="d-flex justify-content-end mt-3">
                <Button Size="Size.Small" Color="Color.Secondary" OnClick="() => popoverRef.Hide()">Dismiss</Button>
            </div>
        </div>
    </BodyTemplate>
    <Button Color="Color.Danger">Show Important Notice</Button>
</Popover>

@code {
    private Popover popoverRef;
}
```

This example demonstrates a popover with a backdrop that dims the rest of the page, creating a modal-like experience to focus attention on the popover content.

### Example 7: Programmatically Controlled Popover

```razor
<div class="mb-3">
    <Button Color="Color.Primary" OnClick="() => popoverRef.Show()">Show Popover</Button>
    <Button Color="Color.Secondary" OnClick="() => popoverRef.Hide()">Hide Popover</Button>
    <Button Color="Color.Info" OnClick="() => popoverRef.Toggle()">Toggle Popover</Button>
</div>

<Popover @ref="popoverRef" 
         Title="Programmatic Control" 
         Content="This popover is controlled programmatically."
         Trigger="Trigger.Manual"
         OnShow="HandleShow"
         OnHide="HandleHide">
    <Button Color="Color.Success">Target Element</Button>
</Popover>

<div class="mt-3">
    <p>Popover Status: @status</p>
</div>

@code {
    private Popover popoverRef;
    private string status = "Hidden";
    
    private void HandleShow()
    {
        status = "Showing";
    }
    
    private void HandleHide()
    {
        status = "Hidden";
    }
}
```

This example shows how to programmatically control a popover using reference methods and event handlers.

## Customization Notes

### CSS Variables

The Popover component can be customized using CSS variables:

```css
:root {
    --bs-popover-min-width: 120px;
    --bs-popover-max-width: 276px;
    --bs-popover-header-font-size: 12px;
    --bs-popover-header-padding-x: 0.5rem;
    --bs-popover-header-padding-y: 0.5rem;
    --bs-popover-body-padding-x: 0.5rem;
    --bs-popover-body-padding-y: 0.5rem;
    --bb-popover-buttons-justify-content: flex-end;
    --bb-popover-buttons-margin: 0.5rem 0 0 0;
    --bb-popover-buttons-padding: 0.25rem 0.5rem;
    --bb-popover-buttons-button-margin-left: 0.5rem;
    --bb-popover-body-span-margin-left: 0.5rem;
}
```

You can override these variables in your CSS to customize the appearance of the Popover component according to your design requirements.

### Customization Tips

1. **Placement Considerations**: Choose an appropriate placement based on the context where the popover appears. For example, use top or bottom placements for elements near the edges of the screen.

2. **Content Length**: Keep popover content concise. For longer content, consider using a modal or drawer component instead.

3. **Trigger Selection**: Choose the appropriate trigger based on the use case:
   - Click: For important information that requires deliberate user action
   - Hover: For supplementary information that should be easily accessible
   - Focus: For form field explanations or validation messages

4. **Accessibility**: Ensure that information in popovers is also accessible to screen readers, either through ARIA attributes or by providing alternative access methods.

5. **Mobile Considerations**: On mobile devices, hover triggers may not work as expected. Consider using click triggers for mobile-friendly interfaces.

6. **Animation**: Use animations judiciously. They can enhance the user experience but may be distracting if overused.

7. **Integration with Forms**: When using popovers with form elements, consider how they interact with validation messages and other form feedback mechanisms.

### Integration with Other Components

The Popover component works well with:

1. **Button Components**: For triggering popovers
2. **Form Components**: For providing additional information about form fields
3. **Table Components**: For showing additional details about table rows or cells
4. **Icon Components**: For creating icon-triggered popovers
5. **Card Components**: For providing additional information about card content