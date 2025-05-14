# CountUp Component

## Overview
The CountUp component in BootstrapBlazor provides an animated numerical counter that incrementally counts from a starting value to a target value. It's ideal for dashboards, statistics displays, achievements, milestones, and any scenario where you want to draw attention to numerical data. The component offers smooth animations with configurable easing effects, formatting options, and event handling to create engaging and dynamic number displays.

## Features
- **Animated Counting**: Smooth animation from start value to target value
- **Configurable Duration**: Adjustable animation speed
- **Decimal Support**: Display numbers with customizable decimal places
- **Formatting Options**: Prefix, suffix, separators, and decimal symbols
- **Easing Effects**: Configurable animation easing for natural-looking transitions
- **Grouping Options**: Customizable thousand separators
- **Event Handling**: Callback when animation completes
- **Scroll Spy**: Option to trigger animation when scrolled into view
- **Smart Easing**: Optimized animation for large number changes
- **Custom Numerals**: Support for alternative numeral glyphs
- **Responsive Design**: Works well across different screen sizes
- **Accessibility Support**: Screen reader compatible

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `Value` | `TValue` | Required | The target value to count up to. Must be a numeric type. |
| `Option` | `CountUpOption` | `null` | Configuration options for the counter animation and display. |
| `OnCompleted` | `Func<Task>` | `null` | Callback function that executes when the counting animation completes. |

### CountUpOption Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `StartValue` | `decimal` | `0` | The value to start counting from. |
| `DecimalPlaces` | `int` | `0` | Number of decimal places to show. |
| `Duration` | `float` | `2.0` | Duration of the animation in seconds. |
| `UseEasing` | `bool` | `true` | Whether to use easing animation effects. |
| `UseGrouping` | `bool` | `true` | Whether to use thousand separators. |
| `UseIndianSeparators` | `bool` | `false` | Whether to use Indian numbering system separators. |
| `Separator` | `string` | `,` | The character to use as thousand separator. |
| `Decimal` | `string` | `.` | The character to use as decimal point. |
| `Prefix` | `string` | `""` | Text to display before the number. |
| `Suffix` | `string` | `""` | Text to display after the number. |
| `SmartEasingThreshold` | `int` | `999` | Threshold for smart easing (for large number changes). |
| `SmartEasingAmount` | `int` | `333` | Amount to use for smart easing animation. |
| `EnableScrollSpy` | `bool` | `false` | Whether to start animation when scrolled into view. |
| `ScrollSpyDelay` | `int` | `200` | Delay in milliseconds before starting animation after scrolling into view. |
| `ScrollSpyOnce` | `bool` | `false` | Whether to trigger the animation only once when scrolled into view. |
| `Numerals` | `char[]` | `null` | Array of characters to use as alternative numerals. |

## Events

| Event | Description |
| --- | --- |
| `OnCompleted` | Triggered when the counting animation completes. |

## Usage Examples

### Example 1: Basic CountUp

```razor
<CountUp TValue="int" Value="1000" />
```

This example shows a basic counter that animates from 0 to 1000 with default settings.

### Example 2: CountUp with Custom Duration and Formatting

```razor
@code {
    private CountUpOption option = new CountUpOption
    {
        Duration = 3.5f,
        DecimalPlaces = 1,
        Prefix = "$",
        Suffix = " USD",
        UseGrouping = true
    };
    
    private double value = 12345.6;
}

<CountUp TValue="double" Value="@value" Option="@option" />
```

This example demonstrates a counter with custom duration, decimal places, prefix, suffix, and grouping.

### Example 3: CountUp with Completion Callback

```razor
@code {
    private int counterValue = 100;
    private string completionMessage = "";
    
    private async Task HandleCountUpCompleted()
    {
        completionMessage = $"Counting to {counterValue} completed at {DateTime.Now:HH:mm:ss}";
        await Task.CompletedTask;
    }
}

<CountUp TValue="int" Value="@counterValue" OnCompleted="HandleCountUpCompleted" />

<div class="mt-3">
    <p>@completionMessage</p>
</div>
```

This example shows how to use the completion callback to execute code when the animation finishes.

### Example 4: Interactive CountUp with Value Updates

```razor
@code {
    private double currentValue = 0;
    private double inputValue = 1000;
    private CountUpOption option = new CountUpOption
    {
        DecimalPlaces = 0,
        Duration = 1.5f
    };
    
    private void UpdateValue()
    {
        currentValue = inputValue;
    }
}

<CountUp TValue="double" Value="@currentValue" Option="@option" />

<div class="mt-3">
    <BootstrapInputGroup>
        <BootstrapInputGroupLabel DisplayText="New Value"></BootstrapInputGroupLabel>
        <BootstrapInputNumber @bind-Value="@inputValue"></BootstrapInputNumber>
        <Button Text="Update" OnClick="UpdateValue" />
    </BootstrapInputGroup>
</div>
```

This example demonstrates how to update the counter value interactively using a form input.

### Example 5: Dashboard with Multiple Counters

```razor
@code {
    private CountUpOption fastOption = new CountUpOption
    {
        Duration = 1.0f,
        UseEasing = true
    };
    
    private CountUpOption percentOption = new CountUpOption
    {
        DecimalPlaces = 1,
        Suffix = "%",
        Duration = 2.0f
    };
    
    private CountUpOption currencyOption = new CountUpOption
    {
        Prefix = "$",
        DecimalPlaces = 2,
        UseGrouping = true,
        Duration = 2.5f
    };
}

<div class="row">
    <div class="col-md-3">
        <div class="card">
            <div class="card-body">
                <h5 class="card-title">Total Users</h5>
                <CountUp TValue="int" Value="2458" Option="@fastOption" class="display-4" />
                <p class="text-muted">Active accounts</p>
            </div>
        </div>
    </div>
    
    <div class="col-md-3">
        <div class="card">
            <div class="card-body">
                <h5 class="card-title">Growth Rate</h5>
                <CountUp TValue="double" Value="24.5" Option="@percentOption" class="display-4" />
                <p class="text-muted">Year over year</p>
            </div>
        </div>
    </div>
    
    <div class="col-md-3">
        <div class="card">
            <div class="card-body">
                <h5 class="card-title">Revenue</h5>
                <CountUp TValue="double" Value="15478.65" Option="@currencyOption" class="display-4" />
                <p class="text-muted">Monthly</p>
            </div>
        </div>
    </div>
    
    <div class="col-md-3">
        <div class="card">
            <div class="card-body">
                <h5 class="card-title">Orders</h5>
                <CountUp TValue="int" Value="847" Option="@fastOption" class="display-4" />
                <p class="text-muted">Last 30 days</p>
            </div>
        </div>
    </div>
</div>
```

This example shows how to create a dashboard with multiple counters, each with different formatting options.

### Example 6: CountUp with Scroll Spy

```razor
@code {
    private CountUpOption scrollOption = new CountUpOption
    {
        EnableScrollSpy = true,
        ScrollSpyDelay = 300,
        ScrollSpyOnce = true,
        Duration = 3.0f,
        UseGrouping = true
    };
}

<div style="height: 800px; padding: 20px;">
    <h4>Scroll down to see the counter animate</h4>
</div>

<div class="text-center p-5 bg-light">
    <h3>Our Achievements</h3>
    <div class="row mt-4">
        <div class="col-md-4">
            <i class="fa-solid fa-users fa-3x mb-3"></i>
            <CountUp TValue="int" Value="10000" Option="@scrollOption" class="d-block display-4" />
            <p>Happy Customers</p>
        </div>
        <div class="col-md-4">
            <i class="fa-solid fa-download fa-3x mb-3"></i>
            <CountUp TValue="int" Value="50000" Option="@scrollOption" class="d-block display-4" />
            <p>Downloads</p>
        </div>
        <div class="col-md-4">
            <i class="fa-solid fa-star fa-3x mb-3"></i>
            <CountUp TValue="int" Value="4000" Option="@scrollOption" class="d-block display-4" />
            <p>5-Star Reviews</p>
        </div>
    </div>
</div>
```

This example demonstrates how to use the scroll spy feature to trigger the animation when the counter comes into view.

### Example 7: CountUp with Custom Numerals

```razor
@code {
    private CountUpOption arabicOption = new CountUpOption
    {
        Numerals = new[] { '٠', '١', '٢', '٣', '٤', '٥', '٦', '٧', '٨', '٩' },
        Duration = 2.5f,
        UseGrouping = true
    };
    
    private CountUpOption romanOption = new CountUpOption
    {
        // This is just for demonstration - not actual Roman numerals
        Numerals = new[] { 'N', 'I', 'II', 'III', 'IV', 'V', 'VI', 'VII', 'VIII', 'IX' },
        Duration = 2.5f,
        UseGrouping = false,
        DecimalPlaces = 0
    };
}

<div class="row">
    <div class="col-md-6">
        <div class="card">
            <div class="card-body">
                <h5 class="card-title">Arabic Numerals</h5>
                <CountUp TValue="int" Value="1234" Option="@arabicOption" class="display-4" />
            </div>
        </div>
    </div>
    
    <div class="col-md-6">
        <div class="card">
            <div class="card-body">
                <h5 class="card-title">Custom Numerals</h5>
                <CountUp TValue="int" Value="1234" Option="@romanOption" class="display-4" />
                <p class="text-muted">Note: This is just for demonstration</p>
            </div>
        </div>
    </div>
</div>
```

This example shows how to use custom numerals for displaying numbers in different writing systems.

## Customization Notes

### CSS Customization

The CountUp component doesn't have specific CSS variables, but you can customize its appearance using standard CSS classes:

```css
/* Example custom styling */
.countup-large {
    font-size: 3rem;
    font-weight: bold;
    color: var(--bs-primary);
}

.countup-small {
    font-size: 1.5rem;
    color: var(--bs-secondary);
}

.countup-highlight {
    background-color: rgba(var(--bs-warning-rgb), 0.2);
    padding: 0.5rem;
    border-radius: 0.25rem;
}
```

You can then apply these classes to your CountUp components:

```razor
<CountUp TValue="int" Value="1000" class="countup-large" />
<CountUp TValue="int" Value="500" class="countup-small" />
<CountUp TValue="int" Value="100" class="countup-highlight" />
```

### Integration with Other Components

The CountUp component works well with:

1. **Card Component**: For creating dashboard tiles with counters
2. **Progress Component**: To show progress alongside numerical values
3. **Chart Component**: To complement data visualizations
4. **Icon Component**: To add visual context to the numbers
5. **Tooltip Component**: To provide additional information about the displayed value

### Performance Considerations

1. **Animation Duration**: For large numbers, consider increasing the duration for smoother animation
2. **Smart Easing**: Use the smart easing options for large number changes to optimize animation performance
3. **Scroll Spy**: Use the scroll spy feature with `ScrollSpyOnce` set to `true` to improve performance on pages with many counters

### Accessibility Considerations

1. **Screen Readers**: The component is compatible with screen readers, which will read the final value
2. **Animation**: Consider reducing or disabling animations for users who have indicated a preference for reduced motion
3. **Context**: Always provide context for the numbers through labels, headings, or descriptions