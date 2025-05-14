# Label Component

## Overview
The Label component in BootstrapBlazor provides a way to display text labels with various styling options. It's commonly used for displaying form field labels, categorizing content, showing status indicators, or highlighting important information. The component offers customizable appearance, including colors, sizes, and styles, making it versatile for different UI requirements.

## Features
- **Customizable Appearance**: Various color schemes, sizes, and styles
- **Icon Support**: Optional icons can be displayed alongside text
- **Tooltip Integration**: Hover tooltips for additional information
- **Required Field Indication**: Special styling for required form fields
- **Text Truncation**: Handles overflow with ellipsis and tooltips
- **Responsive Design**: Adapts to different screen sizes
- **Accessibility Support**: Proper ARIA attributes for screen readers
- **Display Modes**: Inline or block display options
- **Text Alignment**: Left, center, or right alignment options
- **Dynamic Content**: Supports dynamic text updates
- **Click Events**: Optional click handling for interactive labels

## Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `Text` | string | null | The text content of the label |
| `ForId` | string | null | Associates the label with a form element by ID |
| `ShowLabel` | bool | true | Whether to display the label |
| `Color` | Color | Color.None | The color scheme of the label (Primary, Secondary, Success, Danger, Warning, Info, Dark, Light) |
| `Size` | Size | Size.None | The size of the label (XS, SM, MD, LG, XL) |
| `IsRequired` | bool | false | Whether to show the required field indicator |
| `RequiredText` | string | "*" | The text or symbol used for required field indication |
| `ShowRequiredAfter` | bool | false | Whether to show the required indicator after the label text |
| `ShowTooltip` | bool | false | Whether to show a tooltip on hover |
| `TooltipText` | string | null | The text content of the tooltip |
| `TooltipPlacement` | Placement | Placement.Top | The placement of the tooltip |
| `Icon` | string | null | The icon to display with the label |
| `IconColor` | Color | Color.None | The color of the icon |
| `DisplayText` | string | null | Alternative display text (falls back to Text if not provided) |
| `TextEllipsis` | bool | false | Whether to truncate text with ellipsis when it overflows |
| `MaxWidth` | string | null | Maximum width of the label (e.g., "200px") |
| `TextAlign` | Alignment | Alignment.None | Text alignment within the label |
| `DisplayType` | DisplayType | DisplayType.Inline | Whether to display as inline or block element |
| `Class` | string | null | Additional CSS class for the label |
| `ChildContent` | RenderFragment | null | Content to be rendered inside the label |

## Events

| Event | Description |
|-------|-------------|
| `OnClick` | Triggered when the label is clicked |
| `OnMouseOver` | Triggered when the mouse pointer enters the label area |
| `OnMouseOut` | Triggered when the mouse pointer leaves the label area |

## Usage Examples

### Example 1: Basic Label Usage
```razor
<Label Text="Username" />

<Label Text="Password" IsRequired="true" />

<Label Text="Remember me" ForId="remember-checkbox" />
```

### Example 2: Labels with Different Colors and Sizes
```razor
<div class="label-demo">
    <h5>Label Colors</h5>
    <div class="mb-3">
        <Label Text="Primary" Color="Color.Primary" />
        <Label Text="Secondary" Color="Color.Secondary" />
        <Label Text="Success" Color="Color.Success" />
        <Label Text="Danger" Color="Color.Danger" />
        <Label Text="Warning" Color="Color.Warning" />
        <Label Text="Info" Color="Color.Info" />
        <Label Text="Dark" Color="Color.Dark" />
        <Label Text="Light" Color="Color.Light" />
    </div>
    
    <h5>Label Sizes</h5>
    <div class="mb-3">
        <Label Text="Extra Small" Size="Size.ExtraSmall" />
        <Label Text="Small" Size="Size.Small" />
        <Label Text="Medium" Size="Size.Medium" />
        <Label Text="Large" Size="Size.Large" />
        <Label Text="Extra Large" Size="Size.ExtraLarge" />
    </div>
</div>
```

### Example 3: Labels with Icons
```razor
<div class="label-icon-demo">
    <Label Text="User" Icon="fa-solid fa-user" />
    <Label Text="Settings" Icon="fa-solid fa-cog" IconColor="Color.Primary" />
    <Label Text="Warning" Icon="fa-solid fa-exclamation-triangle" IconColor="Color.Warning" />
    <Label Text="Error" Icon="fa-solid fa-times-circle" IconColor="Color.Danger" />
    <Label Text="Success" Icon="fa-solid fa-check-circle" IconColor="Color.Success" />
</div>
```

### Example 4: Form Field Labels
```razor
<form>
    <div class="form-group row mb-3">
        <Label Text="Username" IsRequired="true" Class="col-sm-2 col-form-label" />
        <div class="col-sm-10">
            <input type="text" class="form-control" id="username" placeholder="Enter username" />
        </div>
    </div>
    
    <div class="form-group row mb-3">
        <Label Text="Email" IsRequired="true" Class="col-sm-2 col-form-label" />
        <div class="col-sm-10">
            <input type="email" class="form-control" id="email" placeholder="Enter email" />
        </div>
    </div>
    
    <div class="form-group row mb-3">
        <Label Text="Password" IsRequired="true" Class="col-sm-2 col-form-label" />
        <div class="col-sm-10">
            <input type="password" class="form-control" id="password" placeholder="Enter password" />
        </div>
    </div>
    
    <div class="form-group row mb-3">
        <Label Text="Address" Class="col-sm-2 col-form-label" />
        <div class="col-sm-10">
            <textarea class="form-control" id="address" rows="3" placeholder="Enter address"></textarea>
        </div>
    </div>
    
    <div class="form-group row">
        <div class="col-sm-10 offset-sm-2">
            <button type="submit" class="btn btn-primary">Submit</button>
        </div>
    </div>
</form>
```

### Example 5: Labels with Tooltips
```razor
<div class="tooltip-demo">
    <Label 
        Text="Hover for more info" 
        ShowTooltip="true" 
        TooltipText="This is additional information displayed in a tooltip" 
        TooltipPlacement="Placement.Top" />
    
    <Label 
        Text="Password requirements" 
        Icon="fa-solid fa-info-circle" 
        IconColor="Color.Info" 
        ShowTooltip="true" 
        TooltipText="Password must be at least 8 characters long and include uppercase, lowercase, numbers, and special characters" 
        TooltipPlacement="Placement.Right" />
    
    <Label 
        Text="API Key" 
        IsRequired="true" 
        ShowTooltip="true" 
        TooltipText="Your API key can be found in your account settings" 
        TooltipPlacement="Placement.Bottom" />
</div>
```

### Example 6: Text Truncation with Ellipsis
```razor
<div style="width: 200px; border: 1px solid #ddd; padding: 10px;">
    <Label 
        Text="This is a very long text that will be truncated with ellipsis when it exceeds the maximum width" 
        TextEllipsis="true" 
        MaxWidth="180px" 
        ShowTooltip="true" 
        TooltipText="This is a very long text that will be truncated with ellipsis when it exceeds the maximum width" />
    
    <div class="mt-3">
        <Label 
            Text="Another example of text truncation with a different color" 
            TextEllipsis="true" 
            MaxWidth="180px" 
            Color="Color.Primary" 
            ShowTooltip="true" />
    </div>
</div>
```

### Example 7: Interactive Labels with Click Events
```razor
@code {
    private string clickedLabel = "None";
    
    private void HandleLabelClick(string labelName)
    {
        clickedLabel = labelName;
    }
}

<div class="interactive-labels">
    <h5>Click on a label:</h5>
    
    <div class="mb-3">
        <Label 
            Text="Click me" 
            Color="Color.Primary" 
            OnClick="() => HandleLabelClick('Primary Label')" 
            Class="clickable-label" />
        
        <Label 
            Text="Or click me" 
            Color="Color.Success" 
            OnClick="() => HandleLabelClick('Success Label')" 
            Class="clickable-label" />
        
        <Label 
            Text="Another option" 
            Color="Color.Warning" 
            OnClick="() => HandleLabelClick('Warning Label')" 
            Class="clickable-label" />
    </div>
    
    <div class="mt-3">
        <p>Last clicked label: <strong>@clickedLabel</strong></p>
    </div>
</div>

<style>
    .clickable-label {
        cursor: pointer;
        transition: transform 0.2s;
    }
    
    .clickable-label:hover {
        transform: scale(1.05);
    }
</style>
```

## CSS Customization

The Label component can be customized using CSS variables and classes:

```css
/* Custom Label styling */
.bb-label {
    --bb-label-font-weight: 500;
    --bb-label-margin-bottom: 0.5rem;
    --bb-label-required-color: #dc3545;
    --bb-label-required-margin: 0.25rem;
    --bb-label-icon-margin: 0.25rem;
    
    font-weight: var(--bb-label-font-weight);
    margin-bottom: var(--bb-label-margin-bottom);
}

/* Required indicator styling */
.bb-label-required {
    color: var(--bb-label-required-color);
    margin-left: var(--bb-label-required-margin);
}

.bb-label-required-after {
    margin-right: var(--bb-label-required-margin);
}

/* Icon styling */
.bb-label-icon {
    margin-right: var(--bb-label-icon-margin);
}

/* Text ellipsis styling */
.bb-label-ellipsis {
    white-space: nowrap;
    overflow: hidden;
    text-overflow: ellipsis;
    display: inline-block;
}

/* Color variations */
.bb-label-primary {
    color: #0d6efd;
}

.bb-label-secondary {
    color: #6c757d;
}

.bb-label-success {
    color: #198754;
}

.bb-label-danger {
    color: #dc3545;
}

.bb-label-warning {
    color: #ffc107;
}

.bb-label-info {
    color: #0dcaf0;
}

.bb-label-dark {
    color: #212529;
}

.bb-label-light {
    color: #f8f9fa;
}

/* Size variations */
.bb-label-xs {
    font-size: 0.75rem;
}

.bb-label-sm {
    font-size: 0.875rem;
}

.bb-label-md {
    font-size: 1rem;
}

.bb-label-lg {
    font-size: 1.25rem;
}

.bb-label-xl {
    font-size: 1.5rem;
}

/* Display types */
.bb-label-block {
    display: block;
}

.bb-label-inline {
    display: inline-block;
}

/* Text alignment */
.bb-label-left {
    text-align: left;
}

.bb-label-center {
    text-align: center;
}

.bb-label-right {
    text-align: right;
}
```

## Accessibility

The Label component follows accessibility best practices:

- Uses the semantic `<label>` HTML element when appropriate
- Provides proper association with form elements using the `for` attribute
- Includes ARIA attributes for elements that aren't native labels
- Ensures sufficient color contrast for text readability
- Provides tooltip information for truncated text

## Browser Compatibility

The Label component is compatible with all modern browsers, including:

- Chrome
- Firefox
- Safari
- Edge

## Integration with Other Components

The Label component works seamlessly with other BootstrapBlazor components:

- Use with `Input`, `Select`, and other form components to provide field labels
- Combine with `Tooltip` for enhanced information display
- Use with `Icon` for visual indicators
- Integrate with `Form` and `ValidateForm` for complete form solutions
- Use with `Table` for column headers or data cell labels