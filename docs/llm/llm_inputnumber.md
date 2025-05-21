# InputNumber Component Documentation

## Overview
The InputNumber component in BootstrapBlazor provides a specialized input field for numeric values. It extends the standard input element with features specifically designed for handling numbers, such as increment/decrement buttons, formatting options, and range validation. This component is ideal for collecting precise numerical data in forms, settings panels, or any interface requiring numeric input from users.

## Features
- Two-way data binding for numeric values
- Support for various numeric types (int, double, decimal, etc.)
- Increment and decrement buttons
- Minimum and maximum value constraints
- Step control for increments/decrements
- Decimal precision control
- Numeric formatting options
- Prefix and suffix text or icons
- Clear button option
- Auto-focus capability
- Readonly and disabled states
- Size variants (small, medium, large)
- Form validation integration
- Keyboard navigation support
- Localization support

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| Value | TValue | default(TValue) | Gets or sets the numeric value |
| ValueChanged | EventCallback<TValue> | - | Callback when the value changes |
| ValueExpression | Expression<Func<TValue>> | - | Expression for the bound value |
| Min | TValue | default(TValue) | Minimum allowed value |
| Max | TValue | default(TValue) | Maximum allowed value |
| Step | TValue | default(TValue) | Step value for increment/decrement |
| Placeholder | string | "" | Placeholder text when the input is empty |
| ShowClear | bool | false | Whether to show a clear button |
| IsDisabled | bool | false | Whether the input is disabled |
| IsReadOnly | bool | false | Whether the input is read-only |
| AutoFocus | bool | false | Whether to automatically focus the input on load |
| Size | Size | Medium | The size of the input (Small, Medium, Large) |
| ShowLabel | bool | true | Whether to show the label |
| DisplayText | string | null | The text to display as the label |
| ShowRequiredMark | bool | true | Whether to show a required mark for required fields |
| RequiredMarkText | string | "*" | The text to use for the required mark |
| PrefixText | string | null | Text to display as a prefix |
| SuffixText | string | null | Text to display as a suffix |
| PrefixIcon | string | null | Icon to display as a prefix |
| SuffixIcon | string | null | Icon to display as a suffix |
| Formatter | string | null | Format string for the displayed value |
| DecimalPlaces | int | 2 | Number of decimal places to display |
| OnEnterKey | EventCallback | - | Callback when the Enter key is pressed |
| OnEscKey | EventCallback | - | Callback when the Escape key is pressed |

## Events

| Event | Description |
| --- | --- |
| OnValueChanged | Triggered when the value changes |
| OnFocus | Triggered when the input receives focus |
| OnBlur | Triggered when the input loses focus |
| OnKeyUp | Triggered when a key is released |
| OnKeyDown | Triggered when a key is pressed |
| OnEnterKey | Triggered when the Enter key is pressed |
| OnEscKey | Triggered when the Escape key is pressed |
| OnClear | Triggered when the clear button is clicked |

## Usage Examples

### Example 1: Basic InputNumber

```razor
<InputNumber @bind-Value="@value" />

<div class="mt-3">
    Current value: @value
</div>

@code {
    private int value = 0;
}
```

### Example 2: InputNumber with Min, Max, and Step

```razor
<InputNumber @bind-Value="@temperature" 
            Min="-20" 
            Max="50" 
            Step="0.5" 
            ShowLabel="true" 
            DisplayText="Temperature (°C)" />

<div class="mt-3">
    Current temperature: @temperature°C
</div>

@code {
    private double temperature = 22.5;
}
```

### Example 3: InputNumber with Formatting

```razor
<div class="mb-3">
    <InputNumber @bind-Value="@price" 
                TValue="decimal" 
                Min="0" 
                DecimalPlaces="2" 
                Formatter="C" 
                ShowLabel="true" 
                DisplayText="Price" 
                PrefixText="$" />
</div>

<div class="mb-3">
    <InputNumber @bind-Value="@percentage" 
                TValue="double" 
                Min="0" 
                Max="100" 
                Step="1" 
                Formatter="P0" 
                ShowLabel="true" 
                DisplayText="Percentage" 
                SuffixText="%" />
</div>

<div class="mb-3">
    <InputNumber @bind-Value="@quantity" 
                TValue="int" 
                Min="1" 
                Max="100" 
                Formatter="D2" 
                ShowLabel="true" 
                DisplayText="Quantity" />
</div>

@code {
    private decimal price = 29.99m;
    private double percentage = 75;
    private int quantity = 5;
}
```

### Example 4: InputNumber with Form Validation

```razor
<ValidateForm Model="@model" OnValidSubmit="@HandleValidSubmit">
    <div class="mb-3">
        <InputNumber @bind-Value="@model.Age" 
                    TValue="int" 
                    Min="18" 
                    Max="120" 
                    ShowLabel="true" 
                    DisplayText="Age" />
        <ValidationMessage For="@(() => model.Age)" />
    </div>
    
    <div class="mb-3">
        <InputNumber @bind-Value="@model.Height" 
                    TValue="double" 
                    Min="0" 
                    Step="0.01" 
                    DecimalPlaces="2" 
                    ShowLabel="true" 
                    DisplayText="Height (m)" />
        <ValidationMessage For="@(() => model.Height)" />
    </div>
    
    <div class="mb-3">
        <InputNumber @bind-Value="@model.Weight" 
                    TValue="double" 
                    Min="0" 
                    Step="0.1" 
                    DecimalPlaces="1" 
                    ShowLabel="true" 
                    DisplayText="Weight (kg)" />
        <ValidationMessage For="@(() => model.Weight)" />
    </div>
    
    <Button Type="ButtonType.Submit">Submit</Button>
</ValidateForm>

@code {
    private PersonModel model = new PersonModel();
    
    private void HandleValidSubmit()
    {
        // Process the form data
        Console.WriteLine($"Age: {model.Age}");
        Console.WriteLine($"Height: {model.Height} m");
        Console.WriteLine($"Weight: {model.Weight} kg");
        
        // Calculate BMI
        if (model.Height > 0)
        {
            double bmi = model.Weight / (model.Height * model.Height);
            Console.WriteLine($"BMI: {bmi:F1}");
        }
    }
    
    public class PersonModel
    {
        [Required(ErrorMessage = "Age is required")]
        [Range(18, 120, ErrorMessage = "Age must be between 18 and 120")]
        public int Age { get; set; } = 30;
        
        [Required(ErrorMessage = "Height is required")]
        [Range(0.5, 2.5, ErrorMessage = "Height must be between 0.5 and 2.5 meters")]
        public double Height { get; set; } = 1.75;
        
        [Required(ErrorMessage = "Weight is required")]
        [Range(20, 300, ErrorMessage = "Weight must be between 20 and 300 kg")]
        public double Weight { get; set; } = 70.0;
    }
}
```

### Example 5: Different InputNumber Sizes

```razor
<div class="mb-3">
    <InputNumber @bind-Value="@smallValue" 
                Size="Size.Small" 
                Placeholder="Small input" />
</div>

<div class="mb-3">
    <InputNumber @bind-Value="@mediumValue" 
                Size="Size.Medium" 
                Placeholder="Medium input (default)" />
</div>

<div class="mb-3">
    <InputNumber @bind-Value="@largeValue" 
                Size="Size.Large" 
                Placeholder="Large input" />
</div>

@code {
    private int smallValue = 0;
    private int mediumValue = 0;
    private int largeValue = 0;
}
```

### Example 6: InputNumber with Icons and Clear Button

```razor
<div class="mb-3">
    <InputNumber @bind-Value="@temperature" 
                TValue="double" 
                PrefixIcon="thermometer-half" 
                SuffixText="°C" 
                ShowClear="true" 
                ShowLabel="true" 
                DisplayText="Temperature" />
</div>

<div class="mb-3">
    <InputNumber @bind-Value="@distance" 
                TValue="double" 
                PrefixIcon="ruler" 
                SuffixText="km" 
                ShowClear="true" 
                ShowLabel="true" 
                DisplayText="Distance" />
</div>

<div class="mb-3">
    <InputNumber @bind-Value="@weight" 
                TValue="double" 
                PrefixIcon="weight" 
                SuffixText="kg" 
                ShowClear="true" 
                ShowLabel="true" 
                DisplayText="Weight" />
</div>

@code {
    private double temperature = 22.5;
    private double distance = 10.0;
    private double weight = 75.0;
}
```

### Example 7: Advanced InputNumber Usage

```razor
<div class="card">
    <div class="card-header">Product Configuration</div>
    <div class="card-body">
        <div class="mb-3">
            <InputNumber @bind-Value="@quantity" 
                        TValue="int" 
                        Min="1" 
                        Max="100" 
                        ShowLabel="true" 
                        DisplayText="Quantity" 
                        OnValueChanged="@UpdateTotal" />
        </div>
        
        <div class="mb-3">
            <InputNumber @bind-Value="@unitPrice" 
                        TValue="decimal" 
                        Min="0.01m" 
                        Step="0.01m" 
                        DecimalPlaces="2" 
                        Formatter="C" 
                        ShowLabel="true" 
                        DisplayText="Unit Price" 
                        OnValueChanged="@UpdateTotal" />
        </div>
        
        <div class="mb-3">
            <InputNumber @bind-Value="@discountPercentage" 
                        TValue="double" 
                        Min="0" 
                        Max="100" 
                        Step="1" 
                        SuffixText="%" 
                        ShowLabel="true" 
                        DisplayText="Discount" 
                        OnValueChanged="@UpdateTotal" />
        </div>
        
        <div class="mb-3">
            <InputNumber @bind-Value="@taxRate" 
                        TValue="double" 
                        Min="0" 
                        Max="30" 
                        Step="0.1" 
                        DecimalPlaces="1" 
                        SuffixText="%" 
                        ShowLabel="true" 
                        DisplayText="Tax Rate" 
                        OnValueChanged="@UpdateTotal" />
        </div>
    </div>
    <div class="card-footer">
        <div class="row">
            <div class="col-md-6">
                <p><strong>Subtotal:</strong> @subtotal.ToString("C")</p>
                <p><strong>Discount:</strong> @discount.ToString("C")</p>
                <p><strong>Tax:</strong> @tax.ToString("C")</p>
            </div>
            <div class="col-md-6">
                <h4>Total: @total.ToString("C")</h4>
            </div>
        </div>
    </div>
</div>

@code {
    private int quantity = 1;
    private decimal unitPrice = 29.99m;
    private double discountPercentage = 0;
    private double taxRate = 7.5;
    
    private decimal subtotal = 0;
    private decimal discount = 0;
    private decimal tax = 0;
    private decimal total = 0;
    
    protected override void OnInitialized()
    {
        UpdateTotal();
    }
    
    private void UpdateTotal()
    {
        subtotal = quantity * unitPrice;
        discount = subtotal * (decimal)(discountPercentage / 100);
        decimal afterDiscount = subtotal - discount;
        tax = afterDiscount * (decimal)(taxRate / 100);
        total = afterDiscount + tax;
    }
}
```

## Customization Notes

The InputNumber component can be customized using the following CSS variables:

```css
:root {
    --bb-input-number-height: calc(1.5em + 0.75rem + 2px);
    --bb-input-number-padding-y: 0.375rem;
    --bb-input-number-padding-x: 0.75rem;
    --bb-input-number-font-size: 1rem;
    --bb-input-number-line-height: 1.5;
    --bb-input-number-color: #212529;
    --bb-input-number-bg: #fff;
    --bb-input-number-border-color: #ced4da;
    --bb-input-number-border-width: 1px;
    --bb-input-number-border-radius: 0.25rem;
    --bb-input-number-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);
    --bb-input-number-focus-bg: #fff;
    --bb-input-number-focus-border-color: #86b7fe;
    --bb-input-number-focus-color: #212529;
    --bb-input-number-focus-box-shadow: 0 0 0 0.25rem rgba(13, 110, 253, 0.25);
    --bb-input-number-placeholder-color: #6c757d;
    --bb-input-number-disabled-bg: #e9ecef;
    --bb-input-number-disabled-color: #6c757d;
    --bb-input-number-readonly-bg: #e9ecef;
    --bb-input-number-icon-width: 2.5rem;
    --bb-input-number-icon-color: #6c757d;
    --bb-input-number-icon-hover-color: #212529;
    --bb-input-number-clear-icon-color: #6c757d;
    --bb-input-number-clear-icon-hover-color: #dc3545;
    --bb-input-number-button-width: 2.5rem;
    --bb-input-number-button-color: #6c757d;
    --bb-input-number-button-hover-color: #212529;
    --bb-input-number-button-hover-bg: #e9ecef;
    --bb-input-number-button-active-bg: #dee2e6;
}
```

Additionally, you can customize the appearance and behavior of the InputNumber component by:

1. Using the `Min`, `Max`, and `Step` properties to control the value range and increment/decrement behavior
2. Using the `Formatter` and `DecimalPlaces` properties to format the displayed value
3. Using the `Size` property to adjust the input size
4. Using the `ShowLabel`, `DisplayText`, and `ShowRequiredMark` properties to customize the label
5. Using the `PrefixText`, `SuffixText`, `PrefixIcon`, and `SuffixIcon` properties to add additional elements
6. Using the `ShowClear` property to add a clear button
7. Applying custom CSS classes to the component using the `ClassName` property