# Segmented Component

## Overview

The Segmented component in BootstrapBlazor provides a compact control for selecting between multiple mutually exclusive options. It offers a more space-efficient alternative to radio buttons or select dropdowns, presenting options in a horizontal, connected group of segments. This component is particularly useful for toggling between different views, modes, or filter options in a user interface.

## Features

- **Mutually Exclusive Selection**: Only one option can be selected at a time
- **Compact Layout**: Space-efficient alternative to radio buttons or dropdowns
- **Customizable Appearance**: Various styling options including size, color, and border radius
- **Icon Support**: Options to display icons alongside or instead of text
- **Disabled States**: Support for disabling the entire control or individual options
- **Data Binding**: Two-way binding for selected value
- **Keyboard Navigation**: Support for keyboard interaction and accessibility
- **Responsive Design**: Adapts to different screen sizes

## Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `Items` | `IEnumerable<SegmentedItem>` | `null` | Collection of items to display as options in the segmented control. |
| `Value` | `TValue` | `default` | The currently selected value. Can be bound using `@bind-Value`. |
| `ValueChanged` | `EventCallback<TValue>` | - | Event callback invoked when the selected value changes. |
| `ValueExpression` | `Expression<Func<TValue>>` | `null` | Expression used for two-way binding of the Value property. |
| `OnSelectedItemChanged` | `EventCallback<SegmentedItem<TValue>>` | - | Event callback invoked when the selected item changes, providing the full item object. |
| `Size` | `Size` | `Size.Medium` | Size of the segmented control. Options include Small, Medium, and Large. |
| `Color` | `Color` | `Color.Primary` | Color theme of the segmented control. |
| `IsDisabled` | `bool` | `false` | Whether the entire segmented control is disabled. |
| `IsVertical` | `bool` | `false` | Whether to display the segments vertically instead of horizontally. |
| `ShowBorder` | `bool` | `true` | Whether to show a border around the segmented control. |
| `BorderRadius` | `int` | `4` | Border radius in pixels for the segmented control. |
| `ActiveBackgroundColor` | `string` | `null` | Custom background color for the active segment. |
| `ActiveTextColor` | `string` | `null` | Custom text color for the active segment. |
| `InactiveBackgroundColor` | `string` | `null` | Custom background color for inactive segments. |
| `InactiveTextColor` | `string` | `null` | Custom text color for inactive segments. |
| `Class` | `string` | `""` | Additional CSS class(es) to apply to the segmented control. |
| `Style` | `string` | `""` | Additional inline styles to apply to the segmented control. |

## Events

| Event | Description |
|-------|-------------|
| `OnValueChanged` | Triggered when the selected value changes. Provides the new value. |
| `OnSelectedItemChanged` | Triggered when the selected item changes. Provides the full SegmentedItem object. |
| `OnClick` | Triggered when any segment is clicked. Provides the clicked SegmentedItem and mouse event args. |
| `OnFocus` | Triggered when the segmented control receives focus. |
| `OnBlur` | Triggered when the segmented control loses focus. |

## Usage Examples

### Example 1: Basic Segmented Control

A simple segmented control with text options:

```razor
<Segmented TValue="string" @bind-Value="@_selectedView">
    <Items>
        <SegmentedItem Value="list" Text="List View" />
        <SegmentedItem Value="grid" Text="Grid View" />
        <SegmentedItem Value="table" Text="Table View" />
    </Items>
</Segmented>

<div class="mt-3">
    <p>Selected View: @_selectedView</p>
</div>

@code {
    private string _selectedView = "list";
}
```

### Example 2: Segmented Control with Icons

A segmented control with icons and text:

```razor
<Segmented TValue="string" @bind-Value="@_selectedAlignment" Color="Color.Secondary">
    <Items>
        <SegmentedItem Value="left" Text="Left" Icon="fa-solid fa-align-left" />
        <SegmentedItem Value="center" Text="Center" Icon="fa-solid fa-align-center" />
        <SegmentedItem Value="right" Text="Right" Icon="fa-solid fa-align-right" />
        <SegmentedItem Value="justify" Text="Justify" Icon="fa-solid fa-align-justify" />
    </Items>
</Segmented>

<div class="mt-3">
    <p style="text-align: @_selectedAlignment">This text is aligned according to the selected option.</p>
</div>

@code {
    private string _selectedAlignment = "left";
}
```

### Example 3: Segmented Control with Different Sizes

Demonstrating different sizes of the segmented control:

```razor
<div class="mb-3">
    <h5>Small Size</h5>
    <Segmented TValue="string" @bind-Value="@_selectedOption" Size="Size.Small">
        <Items>
            <SegmentedItem Value="option1" Text="Option 1" />
            <SegmentedItem Value="option2" Text="Option 2" />
            <SegmentedItem Value="option3" Text="Option 3" />
        </Items>
    </Segmented>
</div>

<div class="mb-3">
    <h5>Medium Size (Default)</h5>
    <Segmented TValue="string" @bind-Value="@_selectedOption" Size="Size.Medium">
        <Items>
            <SegmentedItem Value="option1" Text="Option 1" />
            <SegmentedItem Value="option2" Text="Option 2" />
            <SegmentedItem Value="option3" Text="Option 3" />
        </Items>
    </Segmented>
</div>

<div class="mb-3">
    <h5>Large Size</h5>
    <Segmented TValue="string" @bind-Value="@_selectedOption" Size="Size.Large">
        <Items>
            <SegmentedItem Value="option1" Text="Option 1" />
            <SegmentedItem Value="option2" Text="Option 2" />
            <SegmentedItem Value="option3" Text="Option 3" />
        </Items>
    </Segmented>
</div>

@code {
    private string _selectedOption = "option1";
}
```

### Example 4: Segmented Control with Custom Styling

Customizing the appearance of the segmented control:

```razor
<Segmented TValue="string" 
           @bind-Value="@_selectedTheme"
           BorderRadius="20"
           ActiveBackgroundColor="#6200ea"
           ActiveTextColor="#ffffff"
           InactiveBackgroundColor="#f5f5f5"
           InactiveTextColor="#757575"
           Style="min-width: 300px;">
    <Items>
        <SegmentedItem Value="light" Text="Light Theme" Icon="fa-solid fa-sun" />
        <SegmentedItem Value="dark" Text="Dark Theme" Icon="fa-solid fa-moon" />
        <SegmentedItem Value="system" Text="System Theme" Icon="fa-solid fa-laptop" />
    </Items>
</Segmented>

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
            "light" => "background-color: #ffffff; color: #212121;",
            "dark" => "background-color: #212121; color: #ffffff;",
            "system" => "background-color: #f5f5f5; color: #424242;",
            _ => ""
        };
    }
}
```

### Example 5: Segmented Control with Disabled Options

Implementing a segmented control with some disabled options:

```razor
<Segmented TValue="string" @bind-Value="@_selectedPlan" Color="Color.Success">
    <Items>
        <SegmentedItem Value="free" Text="Free" />
        <SegmentedItem Value="basic" Text="Basic" />
        <SegmentedItem Value="pro" Text="Pro" />
        <SegmentedItem Value="enterprise" Text="Enterprise" IsDisabled="true" />
    </Items>
</Segmented>

<div class="mt-3">
    <div class="card">
        <div class="card-header">
            <h5 class="mb-0">@GetPlanName() Plan Features</h5>
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
            <Button Color="Color.Success">Select @GetPlanName() Plan</Button>
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

### Example 6: Vertical Segmented Control

Creating a vertical segmented control for navigation:

```razor
<div class="row">
    <div class="col-md-3">
        <Segmented TValue="string" 
                   @bind-Value="@_selectedSection" 
                   IsVertical="true"
                   OnSelectedItemChanged="HandleSectionChanged">
            <Items>
                <SegmentedItem Value="dashboard" Text="Dashboard" Icon="fa-solid fa-chart-line" />
                <SegmentedItem Value="profile" Text="Profile" Icon="fa-solid fa-user" />
                <SegmentedItem Value="settings" Text="Settings" Icon="fa-solid fa-cog" />
                <SegmentedItem Value="notifications" Text="Notifications" Icon="fa-solid fa-bell" />
                <SegmentedItem Value="help" Text="Help & Support" Icon="fa-solid fa-question-circle" />
            </Items>
        </Segmented>
    </div>
    <div class="col-md-9">
        <div class="card">
            <div class="card-header">
                <h5 class="mb-0">@GetSectionTitle()</h5>
            </div>
            <div class="card-body">
                @switch (_selectedSection)
                {
                    case "dashboard":
                        <p>Welcome to your dashboard. Here you can see an overview of your account.</p>
                        <div class="row">
                            <div class="col-md-4 mb-3">
                                <div class="border rounded p-3 text-center">
                                    <h3>42</h3>
                                    <p class="mb-0">Active Projects</p>
                                </div>
                            </div>
                            <div class="col-md-4 mb-3">
                                <div class="border rounded p-3 text-center">
                                    <h3>128</h3>
                                    <p class="mb-0">Total Tasks</p>
                                </div>
                            </div>
                            <div class="col-md-4 mb-3">
                                <div class="border rounded p-3 text-center">
                                    <h3>86%</h3>
                                    <p class="mb-0">Completion Rate</p>
                                </div>
                            </div>
                        </div>
                        break;
                    case "profile":
                        <p>Manage your profile information and account settings.</p>
                        <div class="mb-3">
                            <label class="form-label">Name</label>
                            <input type="text" class="form-control" value="John Doe" />
                        </div>
                        <div class="mb-3">
                            <label class="form-label">Email</label>
                            <input type="email" class="form-control" value="john.doe@example.com" />
                        </div>
                        break;
                    case "settings":
                        <p>Configure your application settings and preferences.</p>
                        <div class="mb-3">
                            <div class="form-check form-switch">
                                <input class="form-check-input" type="checkbox" id="darkMode" checked />
                                <label class="form-check-label" for="darkMode">Dark Mode</label>
                            </div>
                        </div>
                        <div class="mb-3">
                            <div class="form-check form-switch">
                                <input class="form-check-input" type="checkbox" id="notifications" checked />
                                <label class="form-check-label" for="notifications">Email Notifications</label>
                            </div>
                        </div>
                        break;
                    case "notifications":
                        <p>View and manage your notifications.</p>
                        <div class="list-group">
                            <div class="list-group-item">
                                <div class="d-flex w-100 justify-content-between">
                                    <h6 class="mb-1">New comment on your post</h6>
                                    <small>3 hours ago</small>
                                </div>
                                <p class="mb-1">John commented on your recent post.</p>
                            </div>
                            <div class="list-group-item">
                                <div class="d-flex w-100 justify-content-between">
                                    <h6 class="mb-1">Task assigned to you</h6>
                                    <small>1 day ago</small>
                                </div>
                                <p class="mb-1">You have been assigned a new task.</p>
                            </div>
                        </div>
                        break;
                    case "help":
                        <p>Get help and support for using the application.</p>
                        <div class="accordion" id="helpAccordion">
                            <div class="accordion-item">
                                <h2 class="accordion-header">
                                    <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#collapseOne">
                                        How do I create a new project?
                                    </button>
                                </h2>
                                <div id="collapseOne" class="accordion-collapse collapse show" data-bs-parent="#helpAccordion">
                                    <div class="accordion-body">
                                        To create a new project, go to the Dashboard and click the "New Project" button.
                                    </div>
                                </div>
                            </div>
                            <div class="accordion-item">
                                <h2 class="accordion-header">
                                    <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#collapseTwo">
                                        How do I invite team members?
                                    </button>
                                </h2>
                                <div id="collapseTwo" class="accordion-collapse collapse" data-bs-parent="#helpAccordion">
                                    <div class="accordion-body">
                                        You can invite team members from the Project Settings page by entering their email addresses.
                                    </div>
                                </div>
                            </div>
                        </div>
                        break;
                }
            </div>
        </div>
    </div>
</div>

@code {
    private string _selectedSection = "dashboard";

    private string GetSectionTitle()
    {
        return _selectedSection switch
        {
            "dashboard" => "Dashboard",
            "profile" => "User Profile",
            "settings" => "Application Settings",
            "notifications" => "Notifications",
            "help" => "Help & Support",
            _ => ""
        };
    }

    private void HandleSectionChanged(SegmentedItem<string> item)
    {
        Console.WriteLine($"Navigated to: {item.Text}");
        // Additional navigation logic if needed
    }
}
```

### Example 7: Segmented Control with Dynamic Items

Creating a segmented control with dynamically generated items:

```razor
@page "/filter-view"

<div class="card">
    <div class="card-header">
        <h5 class="mb-0">Product Categories</h5>
    </div>
    <div class="card-body">
        <Segmented TValue="string" 
                   @bind-Value="@_selectedCategory"
                   OnValueChanged="HandleCategoryChanged">
            <Items>
                <SegmentedItem Value="all" Text="All Products" />
                @foreach (var category in _categories)
                {
                    <SegmentedItem Value="@category.Id.ToString()" Text="@category.Name" />
                }
            </Items>
        </Segmented>
        
        <div class="mt-4">
            <h6>Filter by Price Range</h6>
            <Segmented TValue="string" 
                       @bind-Value="@_selectedPriceRange"
                       Size="Size.Small"
                       Color="Color.Secondary"
                       OnValueChanged="HandlePriceRangeChanged">
                <Items>
                    <SegmentedItem Value="all" Text="All Prices" />
                    @foreach (var range in _priceRanges)
                    {
                        <SegmentedItem Value="@range.Id" Text="@range.Label" />
                    }
                </Items>
            </Segmented>
        </div>
        
        <div class="mt-4">
            <div class="row">
                @if (_filteredProducts.Any())
                {
                    @foreach (var product in _filteredProducts)
                    {
                        <div class="col-md-4 mb-3">
                            <div class="card h-100">
                                <div class="card-body">
                                    <h5 class="card-title">@product.Name</h5>
                                    <h6 class="card-subtitle mb-2 text-muted">@product.Category</h6>
                                    <p class="card-text">@product.Description</p>
                                    <div class="d-flex justify-content-between align-items-center">
                                        <span class="price">@product.Price.ToString("C")</span>
                                        <Button Color="Color.Primary" Size="Size.Small">Add to Cart</Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    }
                }
                else
                {
                    <div class="col-12">
                        <div class="alert alert-info">
                            No products match your current filters.
                        </div>
                    </div>
                }
            </div>
        </div>
    </div>
</div>

@code {
    private string _selectedCategory = "all";
    private string _selectedPriceRange = "all";
    private List<Category> _categories = new();
    private List<PriceRange> _priceRanges = new();
    private List<Product> _allProducts = new();
    private List<Product> _filteredProducts = new();

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

        // Initialize price ranges
        _priceRanges = new List<PriceRange>
        {
            new PriceRange { Id = "under50", Label = "Under $50", MaxPrice = 50 },
            new PriceRange { Id = "50to100", Label = "$50 - $100", MinPrice = 50, MaxPrice = 100 },
            new PriceRange { Id = "100to200", Label = "$100 - $200", MinPrice = 100, MaxPrice = 200 },
            new PriceRange { Id = "over200", Label = "Over $200", MinPrice = 200 }
        };

        // Initialize products
        _allProducts = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Category = "Electronics", Price = 899.99m, Description = "High-performance laptop with SSD" },
            new Product { Id = 2, Name = "Smartphone", Category = "Electronics", Price = 499.99m, Description = "Latest model with advanced camera" },
            new Product { Id = 3, Name = "T-Shirt", Category = "Clothing", Price = 19.99m, Description = "Cotton t-shirt, various colors" },
            new Product { Id = 4, Name = "Jeans", Category = "Clothing", Price = 49.99m, Description = "Classic fit denim jeans" },
            new Product { Id = 5, Name = "Coffee Maker", Category = "Home & Kitchen", Price = 79.99m, Description = "Programmable coffee maker with timer" },
            new Product { Id = 6, Name = "Blender", Category = "Home & Kitchen", Price = 129.99m, Description = "High-speed blender for smoothies" },
            new Product { Id = 7, Name = "Novel", Category = "Books", Price = 14.99m, Description = "Bestselling fiction novel" },
            new Product { Id = 8, Name = "Cookbook", Category = "Books", Price = 24.99m, Description = "Collection of gourmet recipes" }
        };

        // Initial filtering
        ApplyFilters();
    }

    private void HandleCategoryChanged(string category)
    {
        _selectedCategory = category;
        ApplyFilters();
    }

    private void HandlePriceRangeChanged(string priceRange)
    {
        _selectedPriceRange = priceRange;
        ApplyFilters();
    }

    private void ApplyFilters()
    {
        // Start with all products
        _filteredProducts = _allProducts;

        // Apply category filter
        if (_selectedCategory != "all")
        {
            var categoryName = _categories.FirstOrDefault(c => c.Id.ToString() == _selectedCategory)?.Name;
            if (!string.IsNullOrEmpty(categoryName))
            {
                _filteredProducts = _filteredProducts.Where(p => p.Category == categoryName).ToList();
            }
        }

        // Apply price range filter
        if (_selectedPriceRange != "all")
        {
            var priceRange = _priceRanges.FirstOrDefault(r => r.Id == _selectedPriceRange);
            if (priceRange != null)
            {
                _filteredProducts = _filteredProducts.Where(p => 
                    (priceRange.MinPrice == null || p.Price >= priceRange.MinPrice) &&
                    (priceRange.MaxPrice == null || p.Price <= priceRange.MaxPrice)
                ).ToList();
            }
        }
    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class PriceRange
    {
        public string Id { get; set; }
        public string Label { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}
```

## CSS Customization

The Segmented component can be customized using the following CSS classes:

- `.segmented-control`: The main container for the segmented control
- `.segmented-item`: Individual segment items
- `.segmented-item-active`: The currently selected segment
- `.segmented-item-disabled`: Disabled segment items
- `.segmented-vertical`: Applied when the control is in vertical orientation

You can override these classes in your application's CSS to customize the appearance of the Segmented component. For example:

```css
/* Custom segmented control styling */
.segmented-control {
    border-radius: 8px;
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
    overflow: hidden;
}

.segmented-item {
    transition: all 0.2s ease;
    padding: 8px 16px;
}

.segmented-item:hover:not(.segmented-item-active):not(.segmented-item-disabled) {
    background-color: rgba(0, 0, 0, 0.05);
}

.segmented-item-active {
    font-weight: 500;
    box-shadow: 0 1px 3px rgba(0, 0, 0, 0.2);
}

.segmented-item-disabled {
    opacity: 0.6;
    cursor: not-allowed;
}

.segmented-vertical .segmented-item {
    padding: 12px 16px;
}
```

## JavaScript Interop

The Segmented component primarily operates on the client side using Blazor's component model. It may use JavaScript interop for the following features:

- Handling keyboard navigation between segments
- Managing focus states for accessibility
- Implementing smooth transitions and animations
- Handling touch events on mobile devices

## Accessibility Considerations

When using the Segmented component, consider the following accessibility best practices:

1. Ensure the component can be navigated using keyboard (Tab to focus, arrow keys to navigate between options)
2. Provide sufficient color contrast between text and background for all states
3. Consider adding ARIA attributes for better screen reader support
4. Ensure that disabled segments are properly indicated both visually and programmatically
5. Use appropriate text labels that clearly describe the purpose of each segment

## Browser Compatibility

The Segmented component is compatible with all modern browsers that support Blazor WebAssembly or Blazor Server. There are no specific browser compatibility issues to be aware of.

## Integration with Other Components

The Segmented component works well with:

- **Content Components**: For switching between different content views
- **Form Components**: As an alternative to radio buttons or select dropdowns
- **Navigation Components**: For simple tab-like navigation
- **Filter Components**: For selecting filter options in data displays
- **Layout Components**: For toggling between different layout modes

## Best Practices

1. Use clear, concise labels for each segment to indicate its purpose
2. Limit the number of segments to maintain usability (typically 2-5 options)
3. Consider using icons alongside text for better visual recognition
4. Ensure segments have sufficient width to accommodate their content
5. Use consistent styling across your application for all segmented controls
6. Consider the vertical orientation for space-constrained interfaces or when used as a sidebar navigation
7. Provide visual feedback when segments are hovered or focused