# Checkbox Component Documentation

## Overview
The Checkbox component in BootstrapBlazor provides a way to allow users to make multiple selections from a set of options or toggle a single option on/off. It's a fundamental form control that can be used individually or as part of a group, offering various styling options and states to enhance user interaction.

## Features
- Single checkbox or checkbox group functionality
- Support for indeterminate state
- Customizable label content and positioning
- Disabled state support
- Inline or stacked layout options
- Data binding capabilities
- Integration with form validation
- Custom styling options
- Support for keyboard navigation

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| Checked | bool | false | Gets or sets whether the checkbox is checked |
| CheckedChanged | EventCallback<bool> | - | Callback when the checked state changes |
| DisplayText | string | null | Text label displayed next to the checkbox |
| ShowAfterLabel | bool | true | Whether to show the label after the checkbox (true) or before (false) |
| IsDisabled | bool | false | Whether the checkbox is disabled |
| IsIndeterminate | bool | false | Whether the checkbox is in an indeterminate state |
| ValidateForm | ValidateForm | null | Reference to parent ValidateForm component for validation |
| ShowBorder | bool | false | Whether to show a border around the checkbox and label |
| Color | Color | Primary | The color theme of the checkbox |

### CheckboxList Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| Items | IEnumerable<TItem> | null | Data source for the checkbox list |
| Values | IEnumerable<TValue> | null | Selected values in the checkbox list |
| ValuesChanged | EventCallback<IEnumerable<TValue>> | - | Callback when selected values change |
| DisplayText | Func<TItem, string> | null | Function to get display text for each item |
| ValueField | string | null | Property name to use as the value field |
| TextField | string | null | Property name to use as the text field |
| IsVertical | bool | false | Whether to display checkboxes vertically |

## Events

| Event | Description |
| --- | --- |
| OnStateChanged | Triggered when the checkbox state changes |
| OnCheckedChanged | Triggered when the checked state changes |
| OnIndeterminateChanged | Triggered when the indeterminate state changes |
| OnValuesChanged | (CheckboxList) Triggered when the selected values change |

## Usage Examples

### Example 1: Basic Checkbox

```razor
<Checkbox @bind-Checked="@isChecked" DisplayText="I agree to the terms and conditions" />

<div class="mt-3">
    Checkbox state: @(isChecked ? "Checked" : "Unchecked")
</div>

@code {
    private bool isChecked = false;
}
```

### Example 2: Checkbox with Different States

```razor
<div class="mb-3">
    <Checkbox @bind-Checked="@checked1" DisplayText="Default Checkbox" />
</div>

<div class="mb-3">
    <Checkbox @bind-Checked="@checked2" DisplayText="Disabled Checkbox" IsDisabled="true" />
</div>

<div class="mb-3">
    <Checkbox @bind-Checked="@checked3" DisplayText="Indeterminate Checkbox" IsIndeterminate="@isIndeterminate" />
    <Button class="ml-3" OnClick="@ToggleIndeterminate">Toggle Indeterminate</Button>
</div>

@code {
    private bool checked1 = false;
    private bool checked2 = true;
    private bool checked3 = false;
    private bool isIndeterminate = true;

    private void ToggleIndeterminate()
    {
        isIndeterminate = !isIndeterminate;
    }
}
```

### Example 3: Checkbox with Custom Styling

```razor
<div class="mb-3">
    <Checkbox @bind-Checked="@checked1" DisplayText="Primary Checkbox" Color="Color.Primary" ShowBorder="true" />
</div>

<div class="mb-3">
    <Checkbox @bind-Checked="@checked2" DisplayText="Success Checkbox" Color="Color.Success" ShowBorder="true" />
</div>

<div class="mb-3">
    <Checkbox @bind-Checked="@checked3" DisplayText="Danger Checkbox" Color="Color.Danger" ShowBorder="true" />
</div>

<div class="mb-3">
    <Checkbox @bind-Checked="@checked4" DisplayText="Warning Checkbox" Color="Color.Warning" ShowBorder="true" />
</div>

<div class="mb-3">
    <Checkbox @bind-Checked="@checked5" DisplayText="Info Checkbox" Color="Color.Info" ShowBorder="true" />
</div>

@code {
    private bool checked1 = true;
    private bool checked2 = true;
    private bool checked3 = true;
    private bool checked4 = true;
    private bool checked5 = true;
}
```

### Example 4: Checkbox with Label Positioning

```razor
<div class="mb-3">
    <Checkbox @bind-Checked="@checked1" DisplayText="Label After (Default)" ShowAfterLabel="true" />
</div>

<div class="mb-3">
    <Checkbox @bind-Checked="@checked2" DisplayText="Label Before" ShowAfterLabel="false" />
</div>

@code {
    private bool checked1 = false;
    private bool checked2 = false;
}
```

### Example 5: Checkbox List from Data Source

```razor
<CheckboxList @bind-Values="@selectedFruits" Items="@fruits" />

<div class="mt-3">
    Selected fruits: @string.Join(", ", selectedFruits)
</div>

@code {
    private List<string> fruits = new List<string> { "Apple", "Banana", "Cherry", "Date", "Elderberry" };
    private List<string> selectedFruits = new List<string> { "Apple", "Cherry" };
}
```

### Example 6: Checkbox List with Complex Data and Vertical Layout

```razor
<CheckboxList TItem="Product"
              TValue="int"
              @bind-Values="@selectedProductIds"
              Items="@products"
              TextField="Name"
              ValueField="Id"
              IsVertical="true" />

<div class="mt-3">
    <h5>Selected Products:</h5>
    <ul>
        @foreach (var product in products.Where(p => selectedProductIds.Contains(p.Id)))
        {
            <li>@product.Name ($@product.Price)</li>
        }
    </ul>
</div>

@code {
    private List<Product> products = new List<Product>
    {
        new Product { Id = 1, Name = "Laptop", Price = 999.99m },
        new Product { Id = 2, Name = "Smartphone", Price = 699.99m },
        new Product { Id = 3, Name = "Tablet", Price = 399.99m },
        new Product { Id = 4, Name = "Monitor", Price = 249.99m },
        new Product { Id = 5, Name = "Keyboard", Price = 79.99m }
    };
    
    private List<int> selectedProductIds = new List<int> { 1, 3 };
    
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
```

### Example 7: Checkbox with Form Validation

```razor
<ValidateForm Model="@model" OnValidSubmit="@HandleValidSubmit">
    <div class="mb-3">
        <Checkbox @bind-Checked="@model.AcceptTerms" DisplayText="I accept the terms and conditions" />
        <ValidationMessage For="@(() => model.AcceptTerms)" />
    </div>
    
    <div class="mb-3">
        <CheckboxList TItem="string"
                      TValue="string"
                      @bind-Values="@model.Interests"
                      Items="@interestOptions" />
        <ValidationMessage For="@(() => model.Interests)" />
    </div>
    
    <Button Type="ButtonType.Submit">Submit</Button>
</ValidateForm>

@code {
    private FormModel model = new FormModel();
    private List<string> interestOptions = new List<string> { "Sports", "Music", "Movies", "Reading", "Travel" };
    
    private void HandleValidSubmit()
    {
        // Process the form submission
        Console.WriteLine($"Terms accepted: {model.AcceptTerms}");
        Console.WriteLine($"Interests: {string.Join(", ", model.Interests)}");
    }
    
    public class FormModel
    {
        [Required(ErrorMessage = "You must accept the terms and conditions")]
        [MustBeTrue(ErrorMessage = "You must accept the terms and conditions")]
        public bool AcceptTerms { get; set; }
        
        [Required(ErrorMessage = "Please select at least one interest")]
        [MinLength(1, ErrorMessage = "Please select at least one interest")]
        public List<string> Interests { get; set; } = new List<string>();
    }
    
    // Custom validation attribute
    public class MustBeTrueAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return value is bool b && b;
        }
    }
}
```

## Customization Notes

The Checkbox component can be customized using the following CSS variables:

```css
:root {
    --bb-checkbox-width: 1rem;
    --bb-checkbox-height: 1rem;
    --bb-checkbox-border-radius: 0.25rem;
    --bb-checkbox-border-color: #ced4da;
    --bb-checkbox-background: #fff;
    --bb-checkbox-checked-background: var(--primary);
    --bb-checkbox-checked-border-color: var(--primary);
    --bb-checkbox-disabled-background: #e9ecef;
    --bb-checkbox-disabled-border-color: #ced4da;
    --bb-checkbox-label-margin: 0.5rem;
    --bb-checkbox-transition: background-color 0.15s ease-in-out, border-color 0.15s ease-in-out, box-shadow 0.15s ease-in-out;
}
```

You can also customize the appearance of the Checkbox component by:

1. Using the `Color` property to change the color theme
2. Using the `ShowBorder` property to add a border around the checkbox and label
3. Using the `ShowAfterLabel` property to control label positioning
4. Applying custom CSS classes to the component using the `ClassName` property
5. Using the `CheckboxTemplate` to provide custom content for the checkbox