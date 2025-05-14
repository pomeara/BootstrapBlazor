# Circle Component

## Overview
The Circle component in BootstrapBlazor provides a circular progress indicator that visually represents completion, percentage, or any other numerical value in a circular format. It's particularly useful for displaying progress in a more compact and visually appealing way than traditional progress bars, making it ideal for dashboards, statistics panels, or any interface where space efficiency and visual impact are important.

## Features
- **Circular Progress Display**: Shows progress or values in a circular format
- **Customizable Appearance**: Configurable colors, sizes, and styles
- **Multiple Themes**: Support for different color themes to indicate status or categories
- **Animated Transitions**: Smooth animations when values change
- **Text Display Options**: Configurable text display in the center of the circle
- **Gradient Support**: Optional gradient colors for enhanced visual appeal
- **Responsive Design**: Automatically adapts to container size
- **Custom Formatting**: Flexible formatting options for displayed values
- **Thickness Control**: Adjustable stroke width for the circle
- **Interactive States**: Optional hover and focus states
- **Accessibility Support**: Proper ARIA attributes for screen readers

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `Value` | `double` | `0` | The current value to display (between 0 and 100) |
| `MaxValue` | `double` | `100` | The maximum possible value |
| `Color` | `Color` | `Color.Primary` | The color theme of the circle |
| `Size` | `int` | `120` | The diameter of the circle in pixels |
| `StrokeWidth` | `int` | `6` | The width of the circle's stroke |
| `ShowText` | `bool` | `true` | Whether to show the text value in the center |
| `Format` | `Func<double, string>` | `null` | Custom formatter for the displayed text |
| `TrailColor` | `string` | `#f5f5f5` | Color of the trail (background circle) |
| `GradientColor` | `string` | `null` | Secondary color for gradient effect |
| `Animation` | `bool` | `true` | Whether to animate value changes |
| `AnimationDuration` | `int` | `1000` | Duration of the animation in milliseconds |
| `ChildContent` | `RenderFragment` | `null` | Custom content to display inside the circle |

## Events

| Event | Description |
| --- | --- |
| `OnValueChanged` | Triggered when the circle's value changes |
| `OnAnimationEnd` | Triggered when the animation completes |

## Usage Examples

### Example 1: Basic Circle Progress

```razor
<Circle Value="75" />
```

This example shows a basic circle progress indicator with 75% completion. It uses the default primary color and displays the percentage text in the center.

### Example 2: Colored Circles with Different Values

```razor
<div class="d-flex flex-wrap gap-3">
    <Circle Value="25" Color="Color.Success" />
    <Circle Value="50" Color="Color.Info" />
    <Circle Value="75" Color="Color.Warning" />
    <Circle Value="100" Color="Color.Danger" />
</div>
```

This example demonstrates multiple circle indicators with different color themes and values, arranged in a flex container with spacing.

### Example 3: Custom Size and Stroke Width

```razor
<div class="d-flex align-items-center gap-4">
    <Circle Value="80" Size="60" StrokeWidth="3" />
    <Circle Value="80" Size="120" StrokeWidth="6" />
    <Circle Value="80" Size="180" StrokeWidth="10" />
</div>
```

This example shows three circle indicators with the same value but different sizes and stroke widths, demonstrating how to adjust the visual appearance.

### Example 4: Custom Text Formatting

```razor
<Circle Value="42.5" Format="@FormatAsDecimal" />

@code {
    private string FormatAsDecimal(double value)
    {
        return value.ToString("F1") + "%";
    }
}
```

This example demonstrates a circle with custom text formatting, displaying the value with one decimal place.

### Example 5: Circle with Custom Content

```razor
<Circle Value="65" ShowText="false" Size="150">
    <div class="text-center">
        <div style="font-size: 24px; font-weight: bold;">65%</div>
        <div style="font-size: 12px;">Complete</div>
    </div>
</Circle>
```

This example shows a circle with custom content inside instead of the default percentage text, allowing for more complex information display.

### Example 6: Dashboard with Multiple Metrics

```razor
<div class="row">
    <div class="col-md-3">
        <div class="card">
            <div class="card-body text-center">
                <Circle Value="@cpuUsage" Color="@GetColorForValue(cpuUsage)" Size="100" />
                <h5 class="mt-3">CPU Usage</h5>
            </div>
        </div>
    </div>
    <div class="col-md-3">
        <div class="card">
            <div class="card-body text-center">
                <Circle Value="@memoryUsage" Color="@GetColorForValue(memoryUsage)" Size="100" />
                <h5 class="mt-3">Memory Usage</h5>
            </div>
        </div>
    </div>
    <div class="col-md-3">
        <div class="card">
            <div class="card-body text-center">
                <Circle Value="@diskUsage" Color="@GetColorForValue(diskUsage)" Size="100" />
                <h5 class="mt-3">Disk Usage</h5>
            </div>
        </div>
    </div>
    <div class="col-md-3">
        <div class="card">
            <div class="card-body text-center">
                <Circle Value="@networkUsage" Color="@GetColorForValue(networkUsage)" Size="100" />
                <h5 class="mt-3">Network Usage</h5>
            </div>
        </div>
    </div>
</div>

@code {
    private double cpuUsage = 45;
    private double memoryUsage = 72;
    private double diskUsage = 85;
    private double networkUsage = 30;
    
    private Color GetColorForValue(double value)
    {
        if (value < 50) return Color.Success;
        if (value < 75) return Color.Warning;
        return Color.Danger;
    }
}
```

This example demonstrates a dashboard with multiple circle indicators displaying different system metrics, each with a color that reflects the severity of the value.

### Example 7: Animated Progress with Events

```razor
<div class="mb-3">
    <Button Color="Color.Primary" OnClick="IncrementValue">Increment</Button>
    <Button Color="Color.Secondary" OnClick="DecrementValue" Class="ms-2">Decrement</Button>
    <Button Color="Color.Danger" OnClick="ResetValue" Class="ms-2">Reset</Button>
</div>

<Circle @ref="circleRef" 
       Value="@currentValue" 
       Animation="true" 
       AnimationDuration="800"
       OnValueChanged="HandleValueChanged"
       OnAnimationEnd="HandleAnimationEnd" />

<div class="mt-3">
    <h5>Events:</h5>
    <div class="border p-3 bg-light" style="max-height: 150px; overflow-y: auto;">
        @foreach (var log in eventLogs.AsEnumerable().Reverse())
        {
            <div class="mb-1">@log</div>
        }
    </div>
</div>

@code {
    private Circle circleRef;
    private double currentValue = 0;
    private List<string> eventLogs = new();
    
    private void IncrementValue()
    {
        if (currentValue < 100)
            currentValue = Math.Min(100, currentValue + 10);
    }
    
    private void DecrementValue()
    {
        if (currentValue > 0)
            currentValue = Math.Max(0, currentValue - 10);
    }
    
    private void ResetValue()
    {
        currentValue = 0;
    }
    
    private void HandleValueChanged(double newValue)
    {
        eventLogs.Add($"[{DateTime.Now:HH:mm:ss}] Value changed to {newValue}%");
    }
    
    private void HandleAnimationEnd()
    {
        eventLogs.Add($"[{DateTime.Now:HH:mm:ss}] Animation completed");
    }
}
```

This example shows an interactive circle progress with buttons to change the value and event handlers to log value changes and animation completion.

## Customization Notes

### CSS Variables

The Circle component can be customized using CSS variables:

```css
:root {
    --bb-circle-trail-color: #f5f5f5;
    --bb-circle-stroke-linecap: round;
    --bb-circle-text-color: #666;
    --bb-circle-text-font-size: 24px;
    --bb-circle-text-font-weight: 500;
    --bb-circle-primary-color: var(--bs-primary);
    --bb-circle-secondary-color: var(--bs-secondary);
    --bb-circle-success-color: var(--bs-success);
    --bb-circle-info-color: var(--bs-info);
    --bb-circle-warning-color: var(--bs-warning);
    --bb-circle-danger-color: var(--bs-danger);
    --bb-circle-light-color: var(--bs-light);
    --bb-circle-dark-color: var(--bs-dark);
}
```

You can override these variables in your CSS to customize the appearance of the Circle component according to your design requirements.

### Customization Tips

1. **Size Considerations**: Choose an appropriate size for your circle based on its context. Smaller circles work well for inline indicators, while larger ones are better for focal points.

2. **Text Formatting**: Use the `Format` property to customize how values are displayed, especially for non-percentage values.

3. **Color Semantics**: Use colors consistently to convey meaning. For example, use success (green) for good values, warning (yellow) for values that need attention, and danger (red) for critical values.

4. **Custom Content**: For complex displays, use the `ChildContent` property to create custom layouts inside the circle.

5. **Animation Timing**: Adjust the `AnimationDuration` property based on the context. Faster animations (300-500ms) work well for frequent updates, while slower animations (800-1200ms) are better for emphasizing changes.

6. **Accessibility**: Always ensure that information conveyed by the circle is also available in text form for screen readers.