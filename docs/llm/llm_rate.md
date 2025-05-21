# Rate Component

## Overview

The Rate component in BootstrapBlazor provides an intuitive way for users to provide ratings or scores. It displays a series of symbols (typically stars) that users can interact with to indicate a rating value. This component is commonly used in review systems, feedback forms, and any scenario where a visual rating scale is needed.

## Features

- **Customizable Symbols**: Supports different icons for rating symbols (stars, hearts, etc.)
- **Half-Star Support**: Allows for more granular ratings with half-star increments
- **Read-Only Mode**: Can be used to display ratings without allowing user interaction
- **Custom Colors**: Ability to customize the colors of selected and unselected symbols
- **Tooltips**: Optional tooltips for each rating level
- **Clear Capability**: Option to allow users to clear their rating
- **Keyboard Navigation**: Accessible via keyboard for better accessibility
- **Size Variants**: Multiple size options to fit different UI requirements
- **Count Configuration**: Configurable number of rating symbols
- **Two-Way Binding**: Supports two-way data binding for easy integration with forms

## Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `Value` | `int` or `double` | `0` | The current rating value. Can be bound using `@bind-Value`. |
| `ValueChanged` | `EventCallback<TValue>` | - | Event callback invoked when the rating value changes. |
| `AllowClear` | `bool` | `true` | Whether to allow clearing the rating by clicking the same symbol again. |
| `AllowHalf` | `bool` | `false` | Whether to allow half-star ratings. |
| `Count` | `int` | `5` | The total number of rating symbols to display. |
| `DefaultValue` | `TValue` | `0` | The default value to use when the rating is cleared. |
| `Disabled` | `bool` | `false` | Whether the rating component is disabled. |
| `Icon` | `string` | `fa-solid fa-star` | The icon to use for the rating symbols. |
| `Color` | `string` | `#fadb14` | The color of the selected rating symbols. |
| `Size` | `Size` | `Size.Medium` | The size of the rating component. Options include Small, Medium, and Large. |
| `ShowTooltip` | `bool` | `false` | Whether to show tooltips when hovering over rating symbols. |
| `TooltipTexts` | `IEnumerable<string>` | `null` | Custom tooltip texts for each rating level. |
| `ReadOnly` | `bool` | `false` | Whether the rating component is read-only. |
| `ShowText` | `bool` | `false` | Whether to show text description next to the rating. |
| `TextPosition` | `Placement` | `Placement.Right` | The position of the text description relative to the rating. |
| `Texts` | `IEnumerable<string>` | `null` | Custom text descriptions for each rating level. |
| `ShowScore` | `bool` | `false` | Whether to show the numerical score next to the rating. |
| `ScoreFormat` | `string` | `{0}` | Format string for the displayed score. |
| `ScorePosition` | `Placement` | `Placement.Right` | The position of the score relative to the rating. |

## Events

| Event | Description |
|-------|-------------|
| `OnValueChanged` | Triggered when the rating value changes. Provides the new value. |
| `OnHoverChange` | Triggered when the user hovers over a different rating symbol. Provides the value being hovered over. |
| `OnFocus` | Triggered when the rating component receives focus. |
| `OnBlur` | Triggered when the rating component loses focus. |

## Usage Examples

### Example 1: Basic Rating Component

A simple rating component with default settings:

```razor
<Rate @bind-Value="@_rating" />

<div class="mt-3">
    <p>Current Rating: @_rating</p>
</div>

@code {
    private int _rating = 3;
}
```

### Example 2: Custom Icons and Colors

Customizing the appearance of the rating component:

```razor
<div class="mb-3">
    <h5>Star Rating (Default)</h5>
    <Rate @bind-Value="@_starRating" />
</div>

<div class="mb-3">
    <h5>Heart Rating</h5>
    <Rate @bind-Value="@_heartRating" 
          Icon="fa-solid fa-heart" 
          Color="#ff4d4f" />
</div>

<div class="mb-3">
    <h5>Thumbs Up Rating</h5>
    <Rate @bind-Value="@_thumbsRating" 
          Icon="fa-solid fa-thumbs-up" 
          Color="#52c41a" />
</div>

<div class="mb-3">
    <h5>Flag Rating</h5>
    <Rate @bind-Value="@_flagRating" 
          Icon="fa-solid fa-flag" 
          Color="#1890ff" />
</div>

@code {
    private int _starRating = 3;
    private int _heartRating = 2;
    private int _thumbsRating = 4;
    private int _flagRating = 1;
}
```

### Example 3: Half-Star Ratings

Implementing a rating component that supports half-star increments:

```razor
<Rate @bind-Value="@_rating" 
      AllowHalf="true" 
      ShowScore="true" 
      ScoreFormat="{0:0.0}" />

<div class="mt-3">
    <p>Current Rating: @_rating</p>
    <Button Color="Color.Primary" OnClick="@(() => _rating = 3.5)">Set to 3.5</Button>
    <Button Color="Color.Secondary" OnClick="@(() => _rating = 0)">Clear</Button>
</div>

@code {
    private double _rating = 2.5;
}
```

### Example 4: Read-Only Rating with Text

Displaying a read-only rating with descriptive text:

```razor
<Rate Value="4" 
      ReadOnly="true" 
      ShowText="true" 
      Texts="@_ratingTexts" />

<div class="mt-4">
    <h5>Product Reviews</h5>
    <div class="card mb-2">
        <div class="card-body">
            <div class="d-flex justify-content-between">
                <h6>John Doe</h6>
                <Rate Value="5" ReadOnly="true" Size="Size.Small" />
            </div>
            <p>Great product! Exceeded my expectations.</p>
        </div>
    </div>
    <div class="card mb-2">
        <div class="card-body">
            <div class="d-flex justify-content-between">
                <h6>Jane Smith</h6>
                <Rate Value="3" ReadOnly="true" Size="Size.Small" />
            </div>
            <p>Good product but could be better.</p>
        </div>
    </div>
    <div class="card mb-2">
        <div class="card-body">
            <div class="d-flex justify-content-between">
                <h6>Bob Johnson</h6>
                <Rate Value="4" ReadOnly="true" Size="Size.Small" />
            </div>
            <p>Very satisfied with my purchase.</p>
        </div>
    </div>
</div>

@code {
    private string[] _ratingTexts = new string[] {
        "Terrible",
        "Bad",
        "Normal",
        "Good",
        "Excellent"
    };
}
```

### Example 5: Different Sizes

Demonstrating different sizes of the rating component:

```razor
<div class="mb-3">
    <h5>Small Size</h5>
    <Rate @bind-Value="@_rating" Size="Size.Small" />
</div>

<div class="mb-3">
    <h5>Medium Size (Default)</h5>
    <Rate @bind-Value="@_rating" Size="Size.Medium" />
</div>

<div class="mb-3">
    <h5>Large Size</h5>
    <Rate @bind-Value="@_rating" Size="Size.Large" />
</div>

@code {
    private int _rating = 3;
}
```

### Example 6: Rating with Tooltips

Adding tooltips to provide more context for each rating level:

```razor
<Rate @bind-Value="@_rating" 
      ShowTooltip="true" 
      TooltipTexts="@_tooltipTexts" />

<div class="mt-3">
    <p>How would you rate your experience?</p>
    <p>Selected Rating: @_tooltipTexts[_rating - 1]</p>
</div>

@code {
    private int _rating = 3;
    private string[] _tooltipTexts = new string[] {
        "Very Dissatisfied",
        "Dissatisfied",
        "Neutral",
        "Satisfied",
        "Very Satisfied"
    };
}
```

### Example 7: Interactive Feedback Form

Integrating the rating component into a feedback form:

```razor
<div class="card">
    <div class="card-header">
        <h5>Product Feedback</h5>
    </div>
    <div class="card-body">
        <div class="mb-3">
            <label class="form-label">Product Quality</label>
            <Rate @bind-Value="@_qualityRating" 
                  AllowHalf="true" 
                  ShowText="true" 
                  Texts="@_qualityTexts" />
        </div>
        
        <div class="mb-3">
            <label class="form-label">Customer Service</label>
            <Rate @bind-Value="@_serviceRating" 
                  AllowHalf="true" 
                  ShowText="true" 
                  Texts="@_serviceTexts" />
        </div>
        
        <div class="mb-3">
            <label class="form-label">Value for Money</label>
            <Rate @bind-Value="@_valueRating" 
                  AllowHalf="true" 
                  ShowText="true" 
                  Texts="@_valueTexts" />
        </div>
        
        <div class="mb-3">
            <label class="form-label">Additional Comments</label>
            <textarea class="form-control" rows="3" @bind="_comments"></textarea>
        </div>
        
        <div class="mb-3">
            <label class="form-label">Overall Rating</label>
            <Rate @bind-Value="@_overallRating" 
                  AllowHalf="true" 
                  ShowScore="true" 
                  ScoreFormat="{0:0.0}" />
        </div>
        
        <Button Color="Color.Primary" OnClick="SubmitFeedback">Submit Feedback</Button>
    </div>
</div>

<div class="mt-3 @(_feedbackSubmitted ? "d-block" : "d-none")">
    <Alert Color="Color.Success" ShowDismiss="true">
        Thank you for your feedback! Your ratings have been submitted successfully.
    </Alert>
</div>

@code {
    private double _qualityRating = 0;
    private double _serviceRating = 0;
    private double _valueRating = 0;
    private double _overallRating = 0;
    private string _comments = "";
    private bool _feedbackSubmitted = false;
    
    private string[] _qualityTexts = new string[] {
        "Poor", "Below Average", "Average", "Good", "Excellent"
    };
    
    private string[] _serviceTexts = new string[] {
        "Poor", "Below Average", "Average", "Good", "Excellent"
    };
    
    private string[] _valueTexts = new string[] {
        "Poor", "Below Average", "Average", "Good", "Excellent"
    };
    
    private void SubmitFeedback()
    {
        // Calculate overall rating as average of other ratings
        _overallRating = (_qualityRating + _serviceRating + _valueRating) / 3;
        
        // In a real application, you would send this data to a server
        // For this example, we just show a success message
        _feedbackSubmitted = true;
    }
}
```

## CSS Customization

The Rate component can be customized using the following CSS classes:

- `.rate`: The main container for the rating component
- `.rate-item`: Individual rating symbols
- `.rate-item-active`: Selected rating symbols
- `.rate-item-half`: Half-selected rating symbols (when `AllowHalf` is true)
- `.rate-item-disabled`: Rating symbols when the component is disabled
- `.rate-text`: Text description container
- `.rate-score`: Score display container

You can override these classes in your application's CSS to customize the appearance of the Rate component. For example:

```css
/* Custom rating component styling */
.rate {
    display: inline-flex;
    align-items: center;
    gap: 4px;
}

.rate-item {
    cursor: pointer;
    transition: transform 0.2s;
}

.rate-item:hover {
    transform: scale(1.2);
}

.rate-item-active {
    /* Custom color for selected stars */
}

.rate-text {
    margin-left: 8px;
    font-weight: 500;
}

.rate-score {
    margin-left: 8px;
    font-weight: bold;
}
```

## JavaScript Interop

The Rate component primarily operates on the client side using Blazor's component model. It may use JavaScript interop for the following features:

- Handling hover effects and animations
- Managing keyboard navigation for accessibility
- Implementing touch interactions on mobile devices

## Accessibility Considerations

When using the Rate component, consider the following accessibility best practices:

1. Ensure the component can be navigated using keyboard (Tab to focus, arrow keys to change rating)
2. Provide sufficient color contrast between the rating symbols and background
3. Include descriptive labels or aria attributes for screen readers
4. Consider adding tooltips or text descriptions to provide context for each rating level
5. Ensure that the component's state is properly communicated to assistive technologies

## Browser Compatibility

The Rate component is compatible with all modern browsers that support Blazor WebAssembly or Blazor Server. There are no specific browser compatibility issues to be aware of.

## Integration with Other Components

The Rate component works well with:

- **Form Components**: For collecting user ratings as part of a larger form
- **Card Components**: For displaying product or service ratings
- **Table Components**: For showing ratings in tabular data
- **Feedback Components**: For gathering user feedback
- **Review Systems**: For implementing product or service review functionality

## Best Practices

1. Use clear labels to indicate what is being rated
2. Consider using half-star ratings for more granular feedback when appropriate
3. Provide visual or textual feedback to confirm the selected rating
4. Use consistent rating scales across your application
5. Consider adding tooltips or text descriptions to clarify the meaning of each rating level
6. Use the read-only mode for displaying existing ratings
7. Implement proper validation if the rating is required in a form