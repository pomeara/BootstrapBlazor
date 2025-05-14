# Tooltip Component Documentation

## Overview
The Tooltip component in BootstrapBlazor provides a way to display informative text when users hover over, focus on, or tap an element. Tooltips are useful for providing additional context or explanations without cluttering the interface.

## Features
- Multiple placement options (top, bottom, left, right)
- Custom content support
- HTML content support
- Customizable trigger actions (hover, focus, click)
- Delay options for showing and hiding
- Programmatic control (show/hide)

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| Title | string | null | Text content of the tooltip |
| Placement | Placement | Top | Position of the tooltip relative to the target element |
| IsHtml | bool | false | Whether the tooltip content contains HTML |
| Trigger | Trigger | Hover | Event that triggers the tooltip (Hover, Focus, Click, Manual) |
| Delay | int | 0 | Delay in milliseconds before showing the tooltip |
| IsDisabled | bool | false | Whether the tooltip is disabled |
| IsInvalidTooltip | bool | false | Whether to show the tooltip as an invalid/error tooltip |

## Events

| Event | Description |
| --- | --- |
| OnShow | Triggered when the tooltip is shown |
| OnShown | Triggered after the tooltip is fully shown |
| OnHide | Triggered when the tooltip is hidden |
| OnHidden | Triggered after the tooltip is fully hidden |

## Methods

| Method | Description |
| --- | --- |
| Show() | Shows the tooltip |
| Hide() | Hides the tooltip |
| Toggle() | Toggles the tooltip visibility |
| Update() | Updates the tooltip position |

## Usage Examples

### Example 1: Basic Tooltip

```razor
<Tooltip Title="This is a tooltip">
    <Button>Hover me</Button>
</Tooltip>

<Tooltip Title="Left tooltip" Placement="Placement.Left">
    <Button>Left</Button>
</Tooltip>

<Tooltip Title="Top tooltip" Placement="Placement.Top">
    <Button>Top</Button>
</Tooltip>

<Tooltip Title="Bottom tooltip" Placement="Placement.Bottom">
    <Button>Bottom</Button>
</Tooltip>

<Tooltip Title="Right tooltip" Placement="Placement.Right">
    <Button>Right</Button>
</Tooltip>
```

### Example 2: Tooltip with HTML Content

```razor
<Tooltip Title="<strong>Bold text</strong> and <em>italic text</em>" IsHtml="true">
    <Button>Hover for HTML tooltip</Button>
</Tooltip>

<Tooltip IsHtml="true" Title="<div class='text-center'><h5>Formatted Tooltip</h5><p>This tooltip has custom formatting</p><hr><small>Additional information</small></div>">
    <Button>Rich HTML Tooltip</Button>
</Tooltip>
```

### Example 3: Tooltip with Different Triggers

```razor
<Tooltip Title="Hover tooltip" Trigger="Trigger.Hover">
    <Button>Hover me</Button>
</Tooltip>

<Tooltip Title="Focus tooltip" Trigger="Trigger.Focus">
    <Button>Focus me</Button>
</Tooltip>

<Tooltip Title="Click tooltip" Trigger="Trigger.Click">
    <Button>Click me</Button>
</Tooltip>

<Tooltip @ref="manualTooltipRef" Title="Manual tooltip" Trigger="Trigger.Manual">
    <Button @onclick="ToggleTooltip">Toggle tooltip</Button>
</Tooltip>

@code {
    private Tooltip? manualTooltipRef;

    private void ToggleTooltip()
    {
        manualTooltipRef?.Toggle();
    }
}
```

### Example 4: Tooltip with Delay

```razor
<Tooltip Title="Instant tooltip" Delay="0">
    <Button>No delay</Button>
</Tooltip>

<Tooltip Title="Delayed tooltip" Delay="1000">
    <Button>1 second delay</Button>
</Tooltip>

<Tooltip Title="Long delayed tooltip" Delay="3000">
    <Button>3 second delay</Button>
</Tooltip>
```

### Example 5: Tooltip for Form Validation

```razor
<Form Model="@model">
    <div class="mb-3">
        <BootstrapInput @bind-Value="@model.Username" placeholder="Enter username">
            <Tooltip IsInvalidTooltip="true" Title="Username is required">
                <span></span>
            </Tooltip>
        </BootstrapInput>
    </div>
    
    <div class="mb-3">
        <BootstrapInput @bind-Value="@model.Email" placeholder="Enter email">
            <Tooltip IsInvalidTooltip="true" Title="Please enter a valid email address">
                <span></span>
            </Tooltip>
        </BootstrapInput>
    </div>
    
    <Button Color="Color.Primary" Type="ButtonType.Submit">Submit</Button>
</Form>

@code {
    private UserModel model = new UserModel();
    
    private class UserModel
    {
        public string Username { get; set; } = "";
        public string Email { get; set; } = "";
    }
}
```

### Example 6: Tooltip with Custom Styling

```razor
<style>
    .custom-tooltip {
        --bs-tooltip-bg: var(--bs-success);
        --bs-tooltip-color: white;
        --bs-tooltip-max-width: 300px;
    }
    
    .warning-tooltip {
        --bs-tooltip-bg: var(--bs-warning);
        --bs-tooltip-color: black;
    }
</style>

<Tooltip Title="Custom styled tooltip" Class="custom-tooltip">
    <Button>Success style</Button>
</Tooltip>

<Tooltip Title="Warning styled tooltip" Class="warning-tooltip">
    <Button>Warning style</Button>
</Tooltip>
```

### Example 7: Tooltip for Help Text

```razor
<div class="mb-3">
    <label class="form-label">API Key
        <Tooltip Title="Your API key can be found in your account settings page">
            <i class="fa-solid fa-question-circle text-primary"></i>
        </Tooltip>
    </label>
    <BootstrapInput placeholder="Enter API key" />
</div>

<div class="mb-3">
    <label class="form-label">Advanced Settings
        <Tooltip Title="These settings are for advanced users only. Incorrect configuration may cause system instability." Placement="Placement.Right">
            <i class="fa-solid fa-exclamation-circle text-warning"></i>
        </Tooltip>
    </label>
    <div class="form-check">
        <input class="form-check-input" type="checkbox" id="enableAdvanced">
        <label class="form-check-label" for="enableAdvanced">Enable advanced features</label>
    </div>
</div>
```

## CSS Customization

The Tooltip component uses Bootstrap's tooltip styling with some additional customizations:

```css
:not(.is-tips) > span[data-bs-toggle="tooltip"] {
    display: inline-block;
}

.dropdown-item > span[data-bs-toggle="tooltip"] {
    display: flex;
    align-items: center;
}

.tooltip.is-invalid {
    --bs-tooltip-bg: var(--bs-danger);
}

.input-group > [data-bs-toggle]:not(:last-child) > .form-control {
    border-top-right-radius: 0;
    border-bottom-right-radius: 0;
}

[data-bs-toggle="tooltip"]:has(.is-display) {
    overflow: hidden;
}
```

You can override these styles or use Bootstrap's tooltip CSS variables to customize the appearance of tooltips:

```css
:root {
    --bs-tooltip-zindex: 1080;
    --bs-tooltip-max-width: 200px;
    --bs-tooltip-padding-x: 0.5rem;
    --bs-tooltip-padding-y: 0.25rem;
    --bs-tooltip-margin: 0;
    --bs-tooltip-font-size: 0.875rem;
    --bs-tooltip-color: #fff;
    --bs-tooltip-bg: #000;
    --bs-tooltip-border-radius: 0.375rem;
    --bs-tooltip-opacity: 0.9;
    --bs-tooltip-arrow-width: 0.8rem;
    --bs-tooltip-arrow-height: 0.4rem;
}
```

## Notes

- Tooltips are implemented using Bootstrap's tooltip component and require JavaScript interop to function.
- For accessibility, tooltips should provide supplementary information and not be the only way to access critical information.
- When using `IsHtml="true"`, be careful with the HTML content to avoid XSS vulnerabilities.
- Tooltips are automatically positioned to stay within the viewport, even if the specified placement would cause them to overflow.
- For form validation, the `IsInvalidTooltip` property can be used to style the tooltip as an error message.
- Tooltips can be used on almost any element, but avoid using them on disabled elements as they may not be accessible.