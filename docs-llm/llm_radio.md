# Radio Component

## Overview

The Radio component in BootstrapBlazor provides a way for users to select a single option from a set of predefined choices. Unlike checkboxes which allow multiple selections, radio buttons enforce a single selection within a group. This component is commonly used in forms, surveys, and any interface where users need to make a mutually exclusive choice.

## Features

- **Group Management**: Automatically manages selection state within a radio group
- **Data Binding**: Supports two-way data binding for easy integration with forms
- **Custom Styling**: Various appearance options including colors, sizes, and button styles
- **Disabled State**: Support for disabling individual radio buttons or the entire group
- **Horizontal/Vertical Layout**: Flexible layout options for different UI requirements
- **Label Customization**: Options for positioning and styling labels
- **Dynamic Options**: Support for generating radio buttons from data collections
- **Validation Integration**: Works with form validation systems
- **Keyboard Navigation**: Accessible via keyboard for better accessibility

## Properties

### RadioGroup Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `Items` | `IEnumerable<RadioItem<TValue>>` | `null` | Collection of radio items to display. |
| `Value` | `TValue` | `default` | The currently selected value. Can be bound using `@bind-Value`. |
| `ValueChanged` | `EventCallback<TValue>` | - | Event callback invoked when the selected value changes. |
| `ValueExpression` | `Expression<Func<TValue>>` | `null` | Expression used for two-way binding of the Value property. |
| `DisplayText` | `string` | `null` | The display text for the radio group label. |
| `IsButton` | `bool` | `false` | Whether to display the radio buttons as toggle buttons. |
| `IsDisabled` | `bool` | `false` | Whether the entire radio group is disabled. |
| `IsVertical` | `bool` | `false` | Whether to display the radio buttons vertically instead of horizontally. |
| `Color` | `Color` | `Color.Primary` | The color theme of the radio buttons when `IsButton` is true. |
| `Size` | `Size` | `Size.Medium` | The size of the radio buttons. Options include Small, Medium, and Large. |

### RadioItem Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `Text` | `string` | `null` | The display text for the radio button. |
| `Value` | `TValue` | `default` | The value associated with this radio button. |
| `IsDisabled` | `bool` | `false` | Whether this specific radio button is disabled. |
| `IsChecked` | `bool` | `false` | Whether this radio button is currently selected. |

## Events

| Event | Description |
|-------|-------------|
| `OnValueChanged` | Triggered when the selected value changes. Provides the new value. |
| `OnSelectedItemChanged` | Triggered when the selected item changes. Provides the full RadioItem object. |
| `OnClick` | Triggered when any radio button is clicked. Provides the clicked RadioItem and mouse event args. |

## Usage Examples

### Example 1: Basic Radio Group

A simple radio group with text options:

```razor
<RadioGroup TValue="string" @bind-Value="@_selectedOption">
    <Items>
        <RadioItem Value="option1" Text="Option 1" />
        <RadioItem Value="option2" Text="Option 2" />
        <RadioItem Value="option3" Text="Option 3" />
    </Items>
</RadioGroup>

<div class="mt-3">
    <p>Selected Option: @_selectedOption</p>
</div>

@code {
    private string _selectedOption = "option1";
}
```

### Example 2: Radio Buttons as Toggle Buttons

Displaying radio options as toggle buttons:

```razor
<RadioGroup TValue="string" 
            @bind-Value="@_selectedTheme" 
            IsButton="true" 
            Color="Color.Info">
    <Items>
        <RadioItem Value="light" Text="Light Theme" />
        <RadioItem Value="dark" Text="Dark Theme" />
        <RadioItem Value="system" Text="System Theme" />
    </Items>
</RadioGroup>

<div class="mt-4 p-4 rounded" style="@GetThemeStyle()">
    <h4>Preview</h4>
    <p>This is a preview of the @_selectedTheme theme.</p>
</div>

@code {
    private string _selectedTheme = "light";

    private string GetThemeStyle()
    {
        return _selectedTheme switch
        {
            "light" => "background-color: #ffffff; color: #212121; border: 1px solid #dee2e6;",
            "dark" => "background-color: #212121; color: #ffffff; border: 1px solid #495057;",
            "system" => "background-color: #f5f5f5; color: #424242; border: 1px solid #ced4da;",
            _ => ""
        };
    }
}
```

### Example 3: Vertical Radio Group with Disabled Option

Creating a vertical radio group with a disabled option:

```razor
<RadioGroup TValue="string" 
            @bind-Value="@_selectedPlan" 
            IsVertical="true" 
            DisplayText="Subscription Plans">
    <Items>
        <RadioItem Value="free" Text="Free Plan - $0/month" />
        <RadioItem Value="basic" Text="Basic Plan - $9.99/month" />
        <RadioItem Value="pro" Text="Pro Plan - $19.99/month" />
        <RadioItem Value="enterprise" Text="Enterprise Plan - Contact Sales" IsDisabled="true" />
    </Items>
</RadioGroup>

<div class="mt-3">
    <div class="card">
        <div class="card-header">
            <h5 class="mb-0">@GetPlanName() Features</h5>
        </div>
        <div class="card-body">
            <ul class="list-group list-group-flush">
                @foreach (var feature in GetPlanFeatures())
                {
                    <li class="list-group-item">@feature</li>
                }
            </ul>
        </div>
        <div class="card-footer">
            <Button Color="Color.Primary">Select @GetPlanName() Plan</Button>
        </div>
    </div>
</div>

@code {
    private string _selectedPlan = "free";

    private string GetPlanName()
    {
        return _selectedPlan switch
        {
            "free" => "Free",
            "basic" => "Basic",
            "pro" => "Pro",
            "enterprise" => "Enterprise",
            _ => ""
        };
    }

    private List<string> GetPlanFeatures()
    {
        return _selectedPlan switch
        {
            "free" => new List<string> { "1 User", "Basic Features", "Community Support", "1GB Storage" },
            "basic" => new List<string> { "5 Users", "All Free Features", "Email Support", "10GB Storage" },
            "pro" => new List<string> { "Unlimited Users", "All Basic Features", "Priority Support", "100GB Storage", "Advanced Analytics" },
            "enterprise" => new List<string> { "Unlimited Users", "All Pro Features", "24/7 Support", "Unlimited Storage", "Custom Integrations" },
            _ => new List<string>()
        };
    }
}
```

### Example 4: Different Sizes

Demonstrating different sizes of radio buttons:

```razor
<div class="mb-3">
    <h5>Small Size</h5>
    <RadioGroup TValue="string" @bind-Value="@_selectedOption" Size="Size.Small">
        <Items>
            <RadioItem Value="option1" Text="Option 1" />
            <RadioItem Value="option2" Text="Option 2" />
            <RadioItem Value="option3" Text="Option 3" />
        </Items>
    </RadioGroup>
</div>

<div class="mb-3">
    <h5>Medium Size (Default)</h5>
    <RadioGroup TValue="string" @bind-Value="@_selectedOption" Size="Size.Medium">
        <Items>
            <RadioItem Value="option1" Text="Option 1" />
            <RadioItem Value="option2" Text="Option 2" />
            <RadioItem Value="option3" Text="Option 3" />
        </Items>
    </RadioGroup>
</div>

<div class="mb-3">
    <h5>Large Size</h5>
    <RadioGroup TValue="string" @bind-Value="@_selectedOption" Size="Size.Large">
        <Items>
            <RadioItem Value="option1" Text="Option 1" />
            <RadioItem Value="option2" Text="Option 2" />
            <RadioItem Value="option3" Text="Option 3" />
        </Items>
    </RadioGroup>
</div>

<div class="mb-3">
    <h5>Small Button Style</h5>
    <RadioGroup TValue="string" @bind-Value="@_selectedOption" IsButton="true" Size="Size.Small">
        <Items>
            <RadioItem Value="option1" Text="Option 1" />
            <RadioItem Value="option2" Text="Option 2" />
            <RadioItem Value="option3" Text="Option 3" />
        </Items>
    </RadioGroup>
</div>

<div class="mb-3">
    <h5>Medium Button Style</h5>
    <RadioGroup TValue="string" @bind-Value="@_selectedOption" IsButton="true" Size="Size.Medium">
        <Items>
            <RadioItem Value="option1" Text="Option 1" />
            <RadioItem Value="option2" Text="Option 2" />
            <RadioItem Value="option3" Text="Option 3" />
        </Items>
    </RadioGroup>
</div>

<div class="mb-3">
    <h5>Large Button Style</h5>
    <RadioGroup TValue="string" @bind-Value="@_selectedOption" IsButton="true" Size="Size.Large">
        <Items>
            <RadioItem Value="option1" Text="Option 1" />
            <RadioItem Value="option2" Text="Option 2" />
            <RadioItem Value="option3" Text="Option 3" />
        </Items>
    </RadioGroup>
</div>

@code {
    private string _selectedOption = "option1";
}
```

### Example 5: Dynamic Radio Options from Data

Generating radio options dynamically from a data collection:

```razor
<RadioGroup TValue="int" 
            @bind-Value="@_selectedCategoryId" 
            DisplayText="Product Categories">
    <Items>
        @foreach (var category in _categories)
        {
            <RadioItem Value="@category.Id" Text="@category.Name" />
        }
    </Items>
</RadioGroup>

<div class="mt-3">
    <h5>Products in Selected Category</h5>
    <div class="row">
        @foreach (var product in GetProductsByCategory(_selectedCategoryId))
        {
            <div class="col-md-4 mb-3">
                <div class="card h-100">
                    <div class="card-body">
                        <h5 class="card-title">@product.Name</h5>
                        <p class="card-text">@product.Description</p>
                        <div class="d-flex justify-content-between align-items-center">
                            <span class="price">@product.Price.ToString("C")</span>
                            <Button Color="Color.Primary" Size="Size.Small">Add to Cart</Button>
                        </div>
                    </div>
                </div>
            </div>
        }
    </div>
</div>

@code {
    private int _selectedCategoryId = 1;
    private List<Category> _categories = new();
    private List<Product> _products = new();

    protected override void OnInitialized()
    {
        // Initialize categories
        _categories = new List<Category>
        {
            new Category { Id = 1, Name = "Electronics" },
            new Category { Id = 2, Name = "Clothing" },
            new Category { Id = 3, Name = "Home & Kitchen" },
            new Category { Id = 4, Name = "Books" }
        };

        // Initialize products
        _products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", CategoryId = 1, Price = 899.99m, Description = "High-performance laptop with SSD" },
            new Product { Id = 2, Name = "Smartphone", CategoryId = 1, Price = 499.99m, Description = "Latest model with advanced camera" },
            new Product { Id = 3, Name = "T-Shirt", CategoryId = 2, Price = 19.99m, Description = "Cotton t-shirt, various colors" },
            new Product { Id = 4, Name = "Jeans", CategoryId = 2, Price = 49.99m, Description = "Classic fit denim jeans" },
            new Product { Id = 5, Name = "Coffee Maker", CategoryId = 3, Price = 79.99m, Description = "Programmable coffee maker with timer" },
            new Product { Id = 6, Name = "Blender", CategoryId = 3, Price = 129.99m, Description = "High-speed blender for smoothies" },
            new Product { Id = 7, Name = "Novel", CategoryId = 4, Price = 14.99m, Description = "Bestselling fiction novel" },
            new Product { Id = 8, Name = "Cookbook", CategoryId = 4, Price = 24.99m, Description = "Collection of gourmet recipes" }
        };
    }

    private List<Product> GetProductsByCategory(int categoryId)
    {
        return _products.Where(p => p.CategoryId == categoryId).ToList();
    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}
```

### Example 6: Radio Group with Event Handling

Implementing event handling with a radio group:

```razor
<RadioGroup TValue="string" 
            @bind-Value="@_selectedShippingMethod" 
            DisplayText="Shipping Method"
            OnValueChanged="HandleShippingMethodChanged"
            OnSelectedItemChanged="HandleSelectedItemChanged">
    <Items>
        <RadioItem Value="standard" Text="Standard Shipping (3-5 business days)" />
        <RadioItem Value="express" Text="Express Shipping (1-2 business days)" />
        <RadioItem Value="overnight" Text="Overnight Shipping (Next business day)" />
    </Items>
</RadioGroup>

<div class="mt-3">
    <div class="card">
        <div class="card-header">
            <h5 class="mb-0">Order Summary</h5>
        </div>
        <div class="card-body">
            <div class="d-flex justify-content-between mb-2">
                <span>Subtotal:</span>
                <span>$99.99</span>
            </div>
            <div class="d-flex justify-content-between mb-2">
                <span>Shipping:</span>
                <span>@_shippingCost.ToString("C")</span>
            </div>
            <div class="d-flex justify-content-between mb-2">
                <span>Tax:</span>
                <span>$8.00</span>
            </div>
            <hr />
            <div class="d-flex justify-content-between fw-bold">
                <span>Total:</span>
                <span>@(_subtotal + _shippingCost + _tax).ToString("C")</span>
            </div>
        </div>
        <div class="card-footer">
            <div class="d-flex justify-content-between align-items-center">
                <span>Estimated Delivery: @_estimatedDelivery</span>
                <Button Color="Color.Success">Place Order</Button>
            </div>
        </div>
    </div>
</div>

<div class="mt-3">
    <Alert Color="Color.Info" ShowDismiss="false">
        <p class="mb-0">@_lastActionMessage</p>
    </Alert>
</div>

@code {
    private string _selectedShippingMethod = "standard";
    private decimal _subtotal = 99.99m;
    private decimal _shippingCost = 5.99m;
    private decimal _tax = 8.00m;
    private string _estimatedDelivery = "May 15-17, 2023";
    private string _lastActionMessage = "";

    private void HandleShippingMethodChanged(string value)
    {
        _lastActionMessage = $"Shipping method changed to: {value}";
        
        // Update shipping cost and estimated delivery based on selected method
        switch (value)
        {
            case "standard":
                _shippingCost = 5.99m;
                _estimatedDelivery = "May 15-17, 2023";
                break;
            case "express":
                _shippingCost = 12.99m;
                _estimatedDelivery = "May 13-14, 2023";
                break;
            case "overnight":
                _shippingCost = 24.99m;
                _estimatedDelivery = "May 12, 2023";
                break;
        }
    }

    private void HandleSelectedItemChanged(RadioItem<string> item)
    {
        _lastActionMessage = $"Selected item changed: {item.Text}";
    }
}
```

### Example 7: Radio Group in a Form

Integrating a radio group within a form with validation:

```razor
<ValidateForm Model="@_formModel" OnValidSubmit="HandleValidSubmit">
    <div class="row">
        <div class="col-md-6 mb-3">
            <BootstrapInput @bind-Value="@_formModel.Name" placeholder="Enter your name" />
        </div>
        <div class="col-md-6 mb-3">
            <BootstrapInput @bind-Value="@_formModel.Email" placeholder="Enter your email" />
        </div>
    </div>
    
    <div class="mb-3">
        <RadioGroup TValue="string" 
                    @bind-Value="@_formModel.Gender" 
                    DisplayText="Gender">
            <Items>
                <RadioItem Value="male" Text="Male" />
                <RadioItem Value="female" Text="Female" />
                <RadioItem Value="other" Text="Other" />
                <RadioItem Value="prefer_not_to_say" Text="Prefer not to say" />
            </Items>
        </RadioGroup>
    </div>
    
    <div class="mb-3">
        <RadioGroup TValue="string" 
                    @bind-Value="@_formModel.AgeGroup" 
                    DisplayText="Age Group">
            <Items>
                <RadioItem Value="under18" Text="Under 18" />
                <RadioItem Value="18to24" Text="18-24" />
                <RadioItem Value="25to34" Text="25-34" />
                <RadioItem Value="35to44" Text="35-44" />
                <RadioItem Value="45to54" Text="45-54" />
                <RadioItem Value="55plus" Text="55+" />
            </Items>
        </RadioGroup>
    </div>
    
    <div class="mb-3">
        <RadioGroup TValue="string" 
                    @bind-Value="@_formModel.PreferredContact" 
                    DisplayText="Preferred Contact Method">
            <Items>
                <RadioItem Value="email" Text="Email" />
                <RadioItem Value="phone" Text="Phone" />
                <RadioItem Value="mail" Text="Mail" />
            </Items>
        </RadioGroup>
    </div>
    
    <div class="mb-3">
        <BootstrapInput @bind-Value="@_formModel.Comments" placeholder="Additional comments" rows="3" />
    </div>
    
    <div class="mb-3">
        <RadioGroup TValue="bool" 
                    @bind-Value="@_formModel.SubscribeToNewsletter" 
                    DisplayText="Subscribe to Newsletter">
            <Items>
                <RadioItem Value="true" Text="Yes, I would like to receive updates" />
                <RadioItem Value="false" Text="No, thank you" />
            </Items>
        </RadioGroup>
    </div>
    
    <Button Color="Color.Primary" ButtonType="ButtonType.Submit">Submit</Button>
</ValidateForm>

<div class="mt-3 @(_formSubmitted ? "d-block" : "d-none")">
    <Alert Color="Color.Success" ShowDismiss="true">
        <p class="mb-0">Form submitted successfully! Thank you for your submission.</p>
    </Alert>
</div>

@code {
    private SurveyFormModel _formModel = new();
    private bool _formSubmitted = false;

    private void HandleValidSubmit()
    {
        // In a real application, you would send this data to a server
        // For this example, we just show a success message
        _formSubmitted = true;
    }

    public class SurveyFormModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Gender selection is required")]
        public string Gender { get; set; } = "prefer_not_to_say";

        [Required(ErrorMessage = "Age group selection is required")]
        public string AgeGroup { get; set; } = "25to34";

        [Required(ErrorMessage = "Preferred contact method is required")]
        public string PreferredContact { get; set; } = "email";

        public string Comments { get; set; }

        public bool SubscribeToNewsletter { get; set; } = true;
    }
}
```

## CSS Customization

The Radio component can be customized using the following CSS classes:

- `.radio-group`: The main container for the radio group
- `.radio-group-vertical`: Applied when the radio group is in vertical orientation
- `.radio-item`: Individual radio button containers
- `.radio-button`: Radio buttons when `IsButton` is true
- `.radio-button-checked`: Selected radio buttons when `IsButton` is true
- `.radio-button-disabled`: Disabled radio buttons when `IsButton` is true

You can override these classes in your application's CSS to customize the appearance of the Radio component. For example:

```css
/* Custom radio button styling */
.radio-group {
    display: flex;
    gap: 12px;
}

.radio-group-vertical {
    flex-direction: column;
    gap: 8px;
}

.radio-item {
    display: flex;
    align-items: center;
}

.radio-item input[type="radio"] {
    margin-right: 8px;
}

.radio-button {
    border-radius: 4px;
    padding: 8px 16px;
    transition: all 0.2s ease;
}

.radio-button:hover:not(.radio-button-checked):not(.radio-button-disabled) {
    background-color: rgba(0, 0, 0, 0.05);
}

.radio-button-checked {
    font-weight: 500;
}

.radio-button-disabled {
    opacity: 0.6;
    cursor: not-allowed;
}
```

## JavaScript Interop

The Radio component primarily operates on the client side using Blazor's component model. It may use JavaScript interop for the following features:

- Handling keyboard navigation for accessibility
- Managing focus states
- Implementing custom animations or transitions

## Accessibility Considerations

When using the Radio component, consider the following accessibility best practices:

1. Ensure the component can be navigated using keyboard (Tab to focus, Space to select)
2. Group related radio buttons together using the `RadioGroup` component
3. Provide clear and descriptive labels for each radio option
4. Use sufficient color contrast between the radio buttons and background
5. Ensure that the component's state is properly communicated to assistive technologies
6. Consider using the `DisplayText` property to provide a group label for screen readers

## Browser Compatibility

The Radio component is compatible with all modern browsers that support Blazor WebAssembly or Blazor Server. There are no specific browser compatibility issues to be aware of.

## Integration with Other Components

The Radio component works well with:

- **Form Components**: For collecting user input as part of a larger form
- **Validation Components**: For validating user selections
- **Card Components**: For organizing related radio options
- **Button Components**: When using the button style radio options
- **Alert Components**: For providing feedback based on user selections

## Best Practices

1. Use clear and concise labels for each radio option
2. Group related radio buttons together using the `RadioGroup` component
3. Provide a default selection when appropriate
4. Consider using the button style for more visually prominent options
5. Use vertical layout for longer option text or when space allows
6. Disable options that are not currently available
7. Provide immediate feedback when a selection changes
8. Use consistent styling for radio groups throughout your application