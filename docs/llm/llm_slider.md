# Slider Component Documentation

## Overview
The Slider component in BootstrapBlazor provides an intuitive interface for selecting numeric values within a specified range. It allows users to make selections by dragging a handle along a track, offering a more visual and interactive alternative to standard numeric inputs. This component is ideal for settings that benefit from visual representation, such as volume controls, price ranges, filter settings, or any scenario where users need to select values from a continuous or stepped range.

## Features
- Single value or range selection modes
- Customizable minimum and maximum values
- Adjustable step increments
- Optional tick marks with customizable intervals
- Tooltip display of current value
- Vertical or horizontal orientation
- Disabled and readonly states
- Custom formatting for displayed values
- Keyboard accessibility for precise adjustments
- Touch-friendly for mobile devices
- Real-time value updates
- Form validation integration
- Customizable appearance (colors, sizes, etc.)
- Support for various numeric types (int, double, decimal)

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| Value | TValue | default(TValue) | Gets or sets the selected value |
| ValueChanged | EventCallback<TValue> | - | Callback when the selected value changes |
| ValueExpression | Expression<Func<TValue>> | - | Expression for the bound value |
| Min | TValue | default(TValue) | Minimum allowed value |
| Max | TValue | default(TValue) | Maximum allowed value |
| Step | TValue | default(TValue) | Step increment value |
| IsRange | bool | false | Whether to enable range selection mode |
| RangeValue | SliderRange<TValue> | null | Range values when in range mode |
| IsVertical | bool | false | Whether to display the slider vertically |
| Height | int | 300 | Height of the slider in vertical mode (pixels) |
| ShowTooltip | bool | true | Whether to show a tooltip with the current value |
| ShowTicks | bool | false | Whether to show tick marks on the track |
| TickStep | TValue | default(TValue) | Interval between tick marks |
| IsDisabled | bool | false | Whether the slider is disabled |
| IsReadOnly | bool | false | Whether the slider is read-only |
| ShowLabel | bool | true | Whether to show the label |
| DisplayText | string | null | The text to display as the label |
| ShowRequiredMark | bool | true | Whether to show a required mark for required fields |
| RequiredMarkText | string | "*" | The text to use for the required mark |
| Formatter | Func<TValue, string> | null | Function to format the displayed value |
| TrackClassName | string | null | CSS class for the slider track |
| HandleClassName | string | null | CSS class for the slider handle |

## Events

| Event | Description |
| --- | --- |
| OnValueChanged | Triggered when the selected value changes |
| OnValueChanging | Triggered continuously while the value is being changed |
| OnDragStart | Triggered when the user starts dragging the handle |
| OnDragEnd | Triggered when the user stops dragging the handle |

## Usage Examples

### Example 1: Basic Slider

```razor
<Slider @bind-Value="@value" Min="0" Max="100" Step="1" />

<div class="mt-3">
    Selected value: @value
</div>

@code {
    private int value = 50;
}
```

### Example 2: Range Slider

```razor
<Slider IsRange="true" @bind-RangeValue="@rangeValue" Min="0" Max="1000" Step="10" />

<div class="mt-3">
    <p>Min value: @rangeValue.Min</p>
    <p>Max value: @rangeValue.Max</p>
    <p>Range: @(rangeValue.Max - rangeValue.Min)</p>
</div>

@code {
    private SliderRange<int> rangeValue = new SliderRange<int> { Min = 200, Max = 800 };
}
```

### Example 3: Slider with Ticks and Custom Formatting

```razor
<Slider @bind-Value="@temperature" 
        Min="-20" 
        Max="40" 
        Step="1" 
        ShowTicks="true" 
        TickStep="5" 
        ShowLabel="true" 
        DisplayText="Temperature" 
        Formatter="@FormatTemperature" />

<div class="mt-3">
    Current temperature: @FormatTemperature(temperature)
</div>

@code {
    private int temperature = 22;
    
    private string FormatTemperature(int value)
    {
        return $"{value}°C";
    }
}
```

### Example 4: Vertical Slider

```razor
<div style="height: 300px;">
    <Slider @bind-Value="@volume" 
            Min="0" 
            Max="100" 
            Step="1" 
            IsVertical="true" 
            Height="300" 
            ShowLabel="true" 
            DisplayText="Volume" 
            Formatter="@FormatVolume" />
</div>

<div class="mt-3">
    Volume: @FormatVolume(volume)
</div>

@code {
    private int volume = 75;
    
    private string FormatVolume(int value)
    {
        return $"{value}%";
    }
}
```

### Example 5: Price Range Filter

```razor
<div class="card">
    <div class="card-header">Price Filter</div>
    <div class="card-body">
        <Slider IsRange="true" 
                @bind-RangeValue="@priceRange" 
                Min="0" 
                Max="1000" 
                Step="10" 
                ShowTicks="true" 
                TickStep="100" 
                Formatter="@FormatPrice" />
        
        <div class="d-flex justify-content-between mt-2">
            <div>@FormatPrice(priceRange.Min)</div>
            <div>@FormatPrice(priceRange.Max)</div>
        </div>
        
        <div class="mt-3">
            <Button Color="Color.Primary" OnClick="@ApplyFilter">Apply Filter</Button>
        </div>
    </div>
</div>

<div class="mt-4">
    @if (isFilterApplied)
    {
        <div class="alert alert-info">
            Showing products with price between @FormatPrice(appliedPriceRange.Min) and @FormatPrice(appliedPriceRange.Max)
        </div>
        
        <div class="row">
            @foreach (var product in FilteredProducts)
            {
                <div class="col-md-4 mb-3">
                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title">@product.Name</h5>
                            <p class="card-text">@FormatPrice(product.Price)</p>
                        </div>
                    </div>
                </div>
            }
        </div>
    }
</div>

@code {
    private SliderRange<decimal> priceRange = new SliderRange<decimal> { Min = 100, Max = 500 };
    private SliderRange<decimal> appliedPriceRange;
    private bool isFilterApplied = false;
    
    private List<Product> Products = new List<Product>
    {
        new Product { Id = 1, Name = "Product A", Price = 99.99m },
        new Product { Id = 2, Name = "Product B", Price = 149.99m },
        new Product { Id = 3, Name = "Product C", Price = 199.99m },
        new Product { Id = 4, Name = "Product D", Price = 249.99m },
        new Product { Id = 5, Name = "Product E", Price = 299.99m },
        new Product { Id = 6, Name = "Product F", Price = 349.99m },
        new Product { Id = 7, Name = "Product G", Price = 399.99m },
        new Product { Id = 8, Name = "Product H", Price = 449.99m },
        new Product { Id = 9, Name = "Product I", Price = 499.99m },
        new Product { Id = 10, Name = "Product J", Price = 549.99m },
        new Product { Id = 11, Name = "Product K", Price = 599.99m },
        new Product { Id = 12, Name = "Product L", Price = 649.99m }
    };
    
    private List<Product> FilteredProducts => Products
        .Where(p => p.Price >= appliedPriceRange.Min && p.Price <= appliedPriceRange.Max)
        .ToList();
    
    private void ApplyFilter()
    {
        appliedPriceRange = new SliderRange<decimal> { Min = priceRange.Min, Max = priceRange.Max };
        isFilterApplied = true;
    }
    
    private string FormatPrice(decimal value)
    {
        return $"${value:F2}";
    }
    
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
```

### Example 6: Slider with Form Validation

```razor
<ValidateForm Model="@model" OnValidSubmit="@HandleValidSubmit">
    <div class="mb-3">
        <Slider @bind-Value="@model.Age" 
                Min="18" 
                Max="100" 
                Step="1" 
                ShowTicks="true" 
                TickStep="10" 
                ShowLabel="true" 
                DisplayText="Age" />
        <ValidationMessage For="@(() => model.Age)" />
    </div>
    
    <div class="mb-3">
        <Slider IsRange="true" 
                @bind-RangeValue="@model.PriceRange" 
                Min="0" 
                Max="1000" 
                Step="10" 
                ShowLabel="true" 
                DisplayText="Price Range" 
                Formatter="@FormatPrice" />
        <ValidationMessage For="@(() => model.PriceRange)" />
    </div>
    
    <Button Type="ButtonType.Submit">Submit</Button>
</ValidateForm>

@code {
    private UserPreferencesModel model = new UserPreferencesModel
    {
        Age = 30,
        PriceRange = new SliderRange<decimal> { Min = 200, Max = 600 }
    };
    
    private void HandleValidSubmit()
    {
        // Process the form data
        Console.WriteLine($"Age: {model.Age}");
        Console.WriteLine($"Price Range: {FormatPrice(model.PriceRange.Min)} - {FormatPrice(model.PriceRange.Max)}");
    }
    
    private string FormatPrice(decimal value)
    {
        return $"${value:F2}";
    }
    
    public class UserPreferencesModel
    {
        [Required(ErrorMessage = "Age is required")]
        [Range(18, 100, ErrorMessage = "Age must be between 18 and 100")]
        public int Age { get; set; }
        
        [Required(ErrorMessage = "Price range is required")]
        [ValidPriceRange(ErrorMessage = "Invalid price range")]
        public SliderRange<decimal> PriceRange { get; set; }
    }
    
    // Custom validation attribute
    public class ValidPriceRangeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is SliderRange<decimal> range)
            {
                if (range.Min < 0 || range.Max > 1000)
                    return new ValidationResult("Price range must be between $0 and $1000");
                    
                if (range.Max <= range.Min)
                    return new ValidationResult("Maximum price must be greater than minimum price");
                    
                if (range.Max - range.Min < 100)
                    return new ValidationResult("Price range must be at least $100");
                    
                return ValidationResult.Success;
            }
            
            return new ValidationResult("Invalid price range value");
        }
    }
}
```

### Example 7: Interactive Color Picker with Sliders

```razor
<div class="card">
    <div class="card-header">RGB Color Picker</div>
    <div class="card-body">
        <div class="mb-3">
            <label>Red (@red)</label>
            <Slider @bind-Value="@red" 
                    Min="0" 
                    Max="255" 
                    Step="1" 
                    TrackClassName="bg-danger" />
        </div>
        
        <div class="mb-3">
            <label>Green (@green)</label>
            <Slider @bind-Value="@green" 
                    Min="0" 
                    Max="255" 
                    Step="1" 
                    TrackClassName="bg-success" />
        </div>
        
        <div class="mb-3">
            <label>Blue (@blue)</label>
            <Slider @bind-Value="@blue" 
                    Min="0" 
                    Max="255" 
                    Step="1" 
                    TrackClassName="bg-primary" />
        </div>
        
        <div class="mb-3">
            <label>Alpha (@alpha)</label>
            <Slider @bind-Value="@alpha" 
                    Min="0" 
                    Max="100" 
                    Step="1" 
                    Formatter="@FormatAlpha" />
        </div>
    </div>
    <div class="card-footer">
        <div class="d-flex align-items-center">
            <div style="width: 100px; height: 100px; border: 1px solid #ccc; margin-right: 1rem; background-color: @ColorString;"></div>
            <div>
                <p>RGB: @RgbString</p>
                <p>RGBA: @RgbaString</p>
                <p>HEX: @HexString</p>
            </div>
        </div>
    </div>
</div>

@code {
    private int red = 255;
    private int green = 0;
    private int blue = 0;
    private int alpha = 100;
    
    private string ColorString => $"rgb({red}, {green}, {blue})";
    private string RgbString => $"rgb({red}, {green}, {blue})";
    private string RgbaString => $"rgba({red}, {green}, {blue}, {alpha / 100.0:F2})";
    private string HexString => $"#{red:X2}{green:X2}{blue:X2}";
    
    private string FormatAlpha(int value)
    {
        return $"{value}%";
    }
}
```

## Customization Notes

The Slider component can be customized using the following CSS variables:

```css
:root {
    --bb-slider-track-height: 4px;
    --bb-slider-track-bg: #e9ecef;
    --bb-slider-track-border-radius: 0.25rem;
    --bb-slider-track-active-bg: #0d6efd;
    --bb-slider-handle-size: 16px;
    --bb-slider-handle-bg: #fff;
    --bb-slider-handle-border-color: #0d6efd;
    --bb-slider-handle-border-width: 2px;
    --bb-slider-handle-border-radius: 50%;
    --bb-slider-handle-box-shadow: 0 0 0 0.25rem rgba(13, 110, 253, 0.25);
    --bb-slider-handle-hover-bg: #f8f9fa;
    --bb-slider-handle-active-bg: #e9ecef;
    --bb-slider-tick-size: 2px;
    --bb-slider-tick-bg: #adb5bd;
    --bb-slider-tick-active-bg: #0d6efd;
    --bb-slider-tooltip-bg: #000;
    --bb-slider-tooltip-color: #fff;
    --bb-slider-tooltip-border-radius: 0.25rem;
    --bb-slider-tooltip-padding: 0.25rem 0.5rem;
    --bb-slider-tooltip-font-size: 0.75rem;
    --bb-slider-tooltip-arrow-size: 6px;
    --bb-slider-disabled-opacity: 0.65;
}
```

Additionally, you can customize the appearance and behavior of the Slider component by:

1. Using the `Min`, `Max`, and `Step` properties to control the value range and increment behavior
2. Using the `IsRange` property to enable range selection mode
3. Using the `IsVertical` and `Height` properties to change the orientation and size
4. Using the `ShowTicks` and `TickStep` properties to add visual tick marks
5. Using the `Formatter` property to customize the displayed value format
6. Using the `TrackClassName` and `HandleClassName` properties to apply custom styles
7. Using the `ShowTooltip` property to control tooltip visibility
8. Applying custom CSS classes to the component using the `ClassName` property