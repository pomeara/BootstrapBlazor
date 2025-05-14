# AutoFill Component

## Overview
The AutoFill component in BootstrapBlazor provides automatic form field completion functionality. It helps users fill out forms more quickly by suggesting values based on previously entered data, predefined datasets, or external sources. This component enhances user experience by reducing typing effort and potential errors in data entry, particularly useful for forms with repetitive information or complex inputs.

## Key Features
- **Automatic Suggestions**: Provides real-time suggestions as users type
- **Multiple Data Sources**: Can use local data, remote APIs, or browser autofill data
- **Customizable Matching**: Configurable matching algorithms (starts with, contains, etc.)
- **Keyboard Navigation**: Support for keyboard navigation through suggestions
- **Custom Templates**: Customizable suggestion item templates
- **Filtering Options**: Various filtering strategies for suggestion results
- **Minimum Characters**: Configurable minimum character count before showing suggestions
- **Debounce Control**: Throttling for performance optimization with remote data sources
- **Selection Events**: Events for suggestion selection and form filling
- **Accessibility Support**: ARIA attributes for screen reader compatibility

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `Items` | `IEnumerable<TItem>` | `null` | Collection of items to use as the data source for suggestions |
| `TextField` | `string` | `null` | Field name to display in the suggestion list |
| `ValueField` | `string` | `null` | Field name to use as the value when an item is selected |
| `DisplayText` | `string` | `null` | Label text displayed next to the input field |
| `PlaceHolder` | `string` | `null` | Placeholder text displayed in the input field |
| `MinLength` | `int` | `2` | Minimum number of characters required before showing suggestions |
| `Debounce` | `int` | `300` | Delay in milliseconds before processing input changes |
| `MaxCount` | `int` | `10` | Maximum number of suggestions to display |
| `IsLiveSearch` | `bool` | `true` | Whether to show suggestions as the user types |
| `IgnoreCase` | `bool` | `true` | Whether to ignore case when matching suggestions |
| `IsRemoteSearch` | `bool` | `false` | Whether to use remote search for suggestions |
| `OnRemoteSearchAsync` | `Func<string, Task<IEnumerable<TItem>>>` | `null` | Callback to fetch remote suggestions |
| `IsAutoFocus` | `bool` | `false` | Whether to automatically focus the input field when the component is loaded |
| `IsAutoSelectFirstItem` | `bool` | `false` | Whether to automatically select the first suggestion |
| `ShowClearButton` | `bool` | `true` | Whether to show a clear button to reset the input |
| `ItemTemplate` | `RenderFragment<TItem>` | `null` | Template for rendering suggestion items |

## Events

| Event | Description |
| --- | --- |
| `OnSelectedItemChanged` | Triggered when a suggestion is selected |
| `OnSearchTextChanged` | Triggered when the search text changes |
| `OnClearValue` | Triggered when the input value is cleared |
| `OnFocus` | Triggered when the input field receives focus |
| `OnBlur` | Triggered when the input field loses focus |
| `OnKeyUp` | Triggered when a key is released in the input field |
| `OnKeyDown` | Triggered when a key is pressed in the input field |

## Usage Examples

### Example 1: Basic AutoFill with Local Data
```razor
@using BootstrapBlazor.Components

<AutoFill TItem="string"
          Items="@Cities"
          DisplayText="City"
          PlaceHolder="Enter a city name"
          OnSelectedItemChanged="OnCitySelected" />

@code {
    private List<string> Cities { get; set; } = new List<string>
    {
        "New York", "Los Angeles", "Chicago", "Houston", "Phoenix",
        "Philadelphia", "San Antonio", "San Diego", "Dallas", "San Jose",
        "Austin", "Jacksonville", "Fort Worth", "Columbus", "San Francisco"
    };
    
    private Task OnCitySelected(string city)
    {
        Console.WriteLine($"Selected city: {city}");
        return Task.CompletedTask;
    }
}
```

### Example 2: AutoFill with Complex Objects
```razor
@using BootstrapBlazor.Components

<AutoFill TItem="User"
          Items="@Users"
          TextField="Name"
          ValueField="Id"
          DisplayText="User"
          PlaceHolder="Search for a user"
          OnSelectedItemChanged="OnUserSelected" />

@code {
    private List<User> Users { get; set; } = new List<User>
    {
        new User { Id = 1, Name = "John Smith", Email = "john@example.com" },
        new User { Id = 2, Name = "Jane Doe", Email = "jane@example.com" },
        new User { Id = 3, Name = "Robert Johnson", Email = "robert@example.com" },
        new User { Id = 4, Name = "Emily Davis", Email = "emily@example.com" },
        new User { Id = 5, Name = "Michael Wilson", Email = "michael@example.com" }
    };
    
    private Task OnUserSelected(User user)
    {
        Console.WriteLine($"Selected user: {user.Name} (ID: {user.Id})");
        return Task.CompletedTask;
    }
    
    private class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
    }
}
```

### Example 3: Remote Search AutoFill
```razor
@using BootstrapBlazor.Components
@inject HttpClient Http

<AutoFill TItem="Product"
          IsRemoteSearch="true"
          OnRemoteSearchAsync="SearchProductsAsync"
          TextField="Name"
          ValueField="Id"
          DisplayText="Product"
          PlaceHolder="Search for products"
          MinLength="2"
          Debounce="500"
          OnSelectedItemChanged="OnProductSelected" />

@code {
    private async Task<IEnumerable<Product>> SearchProductsAsync(string searchText)
    {
        if (string.IsNullOrEmpty(searchText))
            return new List<Product>();
            
        // In a real application, this would be an API call
        // For demonstration, we'll simulate a delay
        await Task.Delay(300);
        
        return MockProducts.Where(p => p.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase));
    }
    
    private Task OnProductSelected(Product product)
    {
        Console.WriteLine($"Selected product: {product.Name} (ID: {product.Id})");
        return Task.CompletedTask;
    }
    
    private List<Product> MockProducts { get; set; } = new List<Product>
    {
        new Product { Id = 1, Name = "Laptop", Price = 999.99m },
        new Product { Id = 2, Name = "Smartphone", Price = 699.99m },
        new Product { Id = 3, Name = "Tablet", Price = 349.99m },
        new Product { Id = 4, Name = "Desktop Computer", Price = 1299.99m },
        new Product { Id = 5, Name = "Monitor", Price = 249.99m },
        new Product { Id = 6, Name = "Keyboard", Price = 49.99m },
        new Product { Id = 7, Name = "Mouse", Price = 29.99m },
        new Product { Id = 8, Name = "Headphones", Price = 79.99m },
        new Product { Id = 9, Name = "Speakers", Price = 89.99m },
        new Product { Id = 10, Name = "Printer", Price = 199.99m }
    };
    
    private class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
    }
}
```

### Example 4: Custom Item Template
```razor
@using BootstrapBlazor.Components

<AutoFill TItem="Contact"
          Items="@Contacts"
          TextField="Name"
          ValueField="Id"
          DisplayText="Contact"
          PlaceHolder="Search contacts"
          OnSelectedItemChanged="OnContactSelected">
    <ItemTemplate Context="contact">
        <div class="d-flex align-items-center">
            <div class="contact-avatar" style="background-color: @contact.AvatarColor">
                @contact.Initials
            </div>
            <div class="ms-2">
                <div><strong>@contact.Name</strong></div>
                <div class="small text-muted">@contact.Email</div>
            </div>
        </div>
    </ItemTemplate>
</AutoFill>

<style>
    .contact-avatar {
        width: 32px;
        height: 32px;
        border-radius: 50%;
        display: flex;
        align-items: center;
        justify-content: center;
        color: white;
        font-weight: bold;
    }
</style>

@code {
    private List<Contact> Contacts { get; set; } = new List<Contact>
    {
        new Contact { Id = 1, Name = "John Smith", Email = "john@example.com", AvatarColor = "#4CAF50", Initials = "JS" },
        new Contact { Id = 2, Name = "Jane Doe", Email = "jane@example.com", AvatarColor = "#2196F3", Initials = "JD" },
        new Contact { Id = 3, Name = "Robert Johnson", Email = "robert@example.com", AvatarColor = "#FF9800", Initials = "RJ" },
        new Contact { Id = 4, Name = "Emily Davis", Email = "emily@example.com", AvatarColor = "#E91E63", Initials = "ED" },
        new Contact { Id = 5, Name = "Michael Wilson", Email = "michael@example.com", AvatarColor = "#9C27B0", Initials = "MW" }
    };
    
    private Task OnContactSelected(Contact contact)
    {
        Console.WriteLine($"Selected contact: {contact.Name}");
        return Task.CompletedTask;
    }
    
    private class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string AvatarColor { get; set; } = "";
        public string Initials { get; set; } = "";
    }
}
```

### Example 5: Form Integration
```razor
@using BootstrapBlazor.Components

<ValidateForm Model="@model" OnValidSubmit="HandleValidSubmit">
    <div class="row">
        <div class="col-md-6 mb-3">
            <BootstrapInput @bind-Value="@model.Name" DisplayText="Name" placeholder="Enter your name" />
        </div>
        <div class="col-md-6 mb-3">
            <BootstrapInput @bind-Value="@model.Email" DisplayText="Email" placeholder="Enter your email" />
        </div>
    </div>
    
    <div class="row">
        <div class="col-md-6 mb-3">
            <AutoFill TItem="string"
                      @bind-Value="@model.Country"
                      Items="@Countries"
                      DisplayText="Country"
                      PlaceHolder="Enter your country" />
        </div>
        <div class="col-md-6 mb-3">
            <AutoFill TItem="string"
                      @bind-Value="@model.City"
                      Items="@Cities"
                      DisplayText="City"
                      PlaceHolder="Enter your city" />
        </div>
    </div>
    
    <div class="row">
        <div class="col-12">
            <Button ButtonType="ButtonType.Submit" Color="Color.Primary">Submit</Button>
        </div>
    </div>
</ValidateForm>

<div class="mt-4">
    @if (formSubmitted)
    {
        <Alert ShowDismiss="true" Color="Color.Success">
            <p><strong>Form submitted successfully!</strong></p>
            <p>Name: @model.Name</p>
            <p>Email: @model.Email</p>
            <p>Country: @model.Country</p>
            <p>City: @model.City</p>
        </Alert>
    }
</div>

@code {
    private UserFormModel model = new();
    private bool formSubmitted = false;
    
    private List<string> Countries { get; set; } = new List<string>
    {
        "United States", "Canada", "United Kingdom", "Australia", "Germany",
        "France", "Japan", "China", "Brazil", "India"
    };
    
    private List<string> Cities { get; set; } = new List<string>
    {
        "New York", "Los Angeles", "Chicago", "Toronto", "London",
        "Sydney", "Berlin", "Paris", "Tokyo", "Beijing",
        "São Paulo", "Mumbai"
    };
    
    private Task HandleValidSubmit()
    {
        formSubmitted = true;
        return Task.CompletedTask;
    }
    
    private class UserFormModel
    {
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Country { get; set; } = "";
        public string City { get; set; } = "";
    }
}
```

### Example 6: AutoFill with Validation
```razor
@using BootstrapBlazor.Components
@using System.ComponentModel.DataAnnotations

<ValidateForm Model="@model" OnValidSubmit="HandleValidSubmit">
    <div class="row">
        <div class="col-md-6 mb-3">
            <BootstrapInput @bind-Value="@model.Name" DisplayText="Name" placeholder="Enter your name" />
        </div>
        <div class="col-md-6 mb-3">
            <BootstrapInput @bind-Value="@model.Email" DisplayText="Email" placeholder="Enter your email" />
        </div>
    </div>
    
    <div class="row">
        <div class="col-md-6 mb-3">
            <AutoFill TItem="string"
                      @bind-Value="@model.Country"
                      Items="@Countries"
                      DisplayText="Country"
                      PlaceHolder="Enter your country" />
        </div>
        <div class="col-md-6 mb-3">
            <AutoFill TItem="string"
                      @bind-Value="@model.City"
                      Items="@Cities"
                      DisplayText="City"
                      PlaceHolder="Enter your city" />
        </div>
    </div>
    
    <div class="row">
        <div class="col-12">
            <Button ButtonType="ButtonType.Submit" Color="Color.Primary">Submit</Button>
        </div>
    </div>
</ValidateForm>

@code {
    private ValidatedUserModel model = new();
    
    private List<string> Countries { get; set; } = new List<string>
    {
        "United States", "Canada", "United Kingdom", "Australia", "Germany",
        "France", "Japan", "China", "Brazil", "India"
    };
    
    private List<string> Cities { get; set; } = new List<string>
    {
        "New York", "Los Angeles", "Chicago", "Toronto", "London",
        "Sydney", "Berlin", "Paris", "Tokyo", "Beijing",
        "São Paulo", "Mumbai"
    };
    
    private Task HandleValidSubmit()
    {
        // Process the validated form
        return Task.CompletedTask;
    }
    
    private class ValidatedUserModel
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        public string Name { get; set; } = "";
        
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = "";
        
        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; } = "";
        
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; } = "";
    }
}
```

### Example 7: Cascading AutoFill
```razor
@using BootstrapBlazor.Components

<div class="row">
    <div class="col-md-6 mb-3">
        <AutoFill TItem="string"
                  @bind-Value="selectedCountry"
                  Items="@Countries"
                  DisplayText="Country"
                  PlaceHolder="Select a country"
                  OnSelectedItemChanged="OnCountrySelected" />
    </div>
    <div class="col-md-6 mb-3">
        <AutoFill TItem="string"
                  @bind-Value="selectedCity"
                  Items="@filteredCities"
                  DisplayText="City"
                  PlaceHolder="Select a city"
                  Disabled="@(string.IsNullOrEmpty(selectedCountry))"
                  OnSelectedItemChanged="OnCitySelected" />
    </div>
</div>

<div class="mt-3">
    @if (!string.IsNullOrEmpty(selectedCountry) && !string.IsNullOrEmpty(selectedCity))
    {
        <Alert Color="Color.Info">
            You selected: @selectedCity, @selectedCountry
        </Alert>
    }
</div>

@code {
    private string selectedCountry = "";
    private string selectedCity = "";
    private List<string> filteredCities = new List<string>();
    
    private Dictionary<string, List<string>> CountryCityMap = new Dictionary<string, List<string>>
    {
        ["United States"] = new List<string> { "New York", "Los Angeles", "Chicago", "Houston", "Phoenix" },
        ["Canada"] = new List<string> { "Toronto", "Montreal", "Vancouver", "Calgary", "Ottawa" },
        ["United Kingdom"] = new List<string> { "London", "Manchester", "Birmingham", "Glasgow", "Liverpool" },
        ["Australia"] = new List<string> { "Sydney", "Melbourne", "Brisbane", "Perth", "Adelaide" },
        ["Germany"] = new List<string> { "Berlin", "Munich", "Hamburg", "Cologne", "Frankfurt" }
    };
    
    private List<string> Countries => CountryCityMap.Keys.ToList();
    
    private Task OnCountrySelected(string country)
    {
        selectedCountry = country;
        selectedCity = "";
        
        if (CountryCityMap.TryGetValue(country, out var cities))
        {
            filteredCities = cities;
        }
        else
        {
            filteredCities = new List<string>();
        }
        
        return Task.CompletedTask;
    }
    
    private Task OnCitySelected(string city)
    {
        selectedCity = city;
        return Task.CompletedTask;
    }
}
```

## CSS Customization

The AutoFill component can be customized using CSS variables:

```css
/* AutoFill custom styling */
.bb-autofill {
  --bb-autofill-border-color: #ced4da;
  --bb-autofill-border-radius: 0.25rem;
  --bb-autofill-box-shadow: 0 0.125rem 0.25rem rgba(0, 0, 0, 0.075);
  --bb-autofill-z-index: 1000;
  --bb-autofill-max-height: 300px;
  --bb-autofill-item-padding: 0.5rem 1rem;
  --bb-autofill-item-hover-bg: #f8f9fa;
  --bb-autofill-item-active-bg: #e9ecef;
  --bb-autofill-item-active-color: #495057;
}

/* Custom styling for the dropdown */
.bb-autofill-menu {
  border: 1px solid var(--bb-autofill-border-color);
  border-radius: var(--bb-autofill-border-radius);
  box-shadow: var(--bb-autofill-box-shadow);
  z-index: var(--bb-autofill-z-index);
  max-height: var(--bb-autofill-max-height);
  overflow-y: auto;
}

/* Custom styling for dropdown items */
.bb-autofill-item {
  padding: var(--bb-autofill-item-padding);
}

.bb-autofill-item:hover {
  background-color: var(--bb-autofill-item-hover-bg);
}

.bb-autofill-item.active {
  background-color: var(--bb-autofill-item-active-bg);
  color: var(--bb-autofill-item-active-color);
}
```

## Notes

### Performance Considerations
- For large datasets, consider using `IsRemoteSearch` with server-side filtering to improve performance.
- The `Debounce` property helps reduce the number of search operations when typing quickly.
- Set an appropriate `MinLength` value to avoid unnecessary searches for very short inputs.

### Accessibility
- The AutoFill component includes ARIA attributes for screen reader compatibility.
- Keyboard navigation is supported for accessibility (arrow keys, Enter, Escape).
- Ensure sufficient color contrast for the dropdown items and selected state.

### Mobile Considerations
- The dropdown positioning adapts to the viewport size on mobile devices.
- Touch interactions are supported for selecting items from the dropdown.
- Consider using a larger `MinLength` on mobile to reduce the keyboard display frequency.

### Integration with Forms
- The AutoFill component works seamlessly with the ValidateForm component.
- It supports data binding with the `@bind-Value` directive.
- Validation attributes can be applied to the bound property in the model.

### Best Practices
- Use the `TextField` and `ValueField` properties when working with complex objects.
- Provide meaningful placeholder text to guide users on what to enter.
- Consider using the `ItemTemplate` for rich, informative suggestion items.
- For related fields (like country/city), implement cascading AutoFill components.
- Use the `OnSelectedItemChanged` event to perform actions when a selection is made.