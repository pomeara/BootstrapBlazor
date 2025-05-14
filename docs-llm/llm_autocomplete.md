# AutoComplete Component Documentation

## Overview
The AutoComplete component in BootstrapBlazor provides an input field with suggestion capabilities. As users type, it displays a dropdown list of matching options, enhancing the user experience by simplifying data entry and reducing errors. It's particularly useful for forms where users need to select from a large set of predefined options while still allowing free text input.

## Features
- Dynamic filtering of suggestions as users type
- Support for both client-side and server-side data sources
- Customizable item templates for dropdown suggestions
- Minimum character threshold before showing suggestions
- Debounce delay to optimize performance during typing
- Loading indicator for asynchronous data fetching
- Keyboard navigation support (arrow keys, Enter, Escape)
- Optional clear button to reset the input
- Configurable positioning of the dropdown menu
- Support for custom filtering logic

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| Items | IEnumerable<TItem> | null | The data source for suggestions |
| OnSearchAsync | Func<string, Task<IEnumerable<TItem>>> | null | Async callback for server-side filtering |
| DisplayText | Func<TItem, string> | null | Function to determine the display text for each item |
| ValueChanged | EventCallback<TItem> | - | Callback when selected value changes |
| Placeholder | string | "" | Placeholder text for the input field |
| DebounceInterval | int | 300 | Delay in milliseconds before triggering search |
| MinLength | int | 1 | Minimum number of characters before showing suggestions |
| MaxItems | int | 10 | Maximum number of items to display in dropdown |
| NoDataText | string | "No Data" | Text to display when no matching items are found |
| ShowClearButton | bool | false | Whether to show a button to clear the input |
| IsLiveSearch | bool | true | Whether to filter as the user types |
| IsAutoSelectFirstItem | bool | false | Whether to automatically select the first item in the dropdown |
| DropdownWidth | int | null | Custom width for the dropdown menu |
| OnSelectedItemChanged | EventCallback<TItem> | - | Callback when an item is selected |
| OnClearValue | EventCallback | - | Callback when the value is cleared |

## Events

| Event | Description |
| --- | --- |
| OnSelectedItemChanged | Triggered when an item is selected from the dropdown |
| OnSearching | Triggered when a search operation begins |
| OnSearched | Triggered when a search operation completes |
| OnClearValue | Triggered when the input value is cleared |
| OnVisibleChanged | Triggered when the dropdown visibility changes |

## Usage Examples

### Example 1: Basic AutoComplete with Local Data

```razor
<AutoComplete TItem="string"
              Items="@Cities"
              Placeholder="Enter a city name"
              ShowClearButton="true" />

@code {
    private List<string> Cities { get; set; } = new List<string>
    {
        "New York", "Los Angeles", "Chicago", "Houston", "Phoenix",
        "Philadelphia", "San Antonio", "San Diego", "Dallas", "San Jose"
    };
}
```

### Example 2: AutoComplete with Custom Display and Server-Side Filtering

```razor
<AutoComplete TItem="User"
              OnSearchAsync="@OnSearchUsers"
              DisplayText="@(item => $"{item.FirstName} {item.LastName}")"
              Placeholder="Search users..."
              DebounceInterval="500"
              MinLength="2"
              OnSelectedItemChanged="@OnUserSelected" />

@code {
    private async Task<IEnumerable<User>> OnSearchUsers(string searchText)
    {
        // Simulate server-side API call with delay
        await Task.Delay(300);
        
        // In a real application, this would be an API call
        return Users.Where(u => 
            u.FirstName.Contains(searchText, StringComparison.OrdinalIgnoreCase) || 
            u.LastName.Contains(searchText, StringComparison.OrdinalIgnoreCase));
    }
    
    private void OnUserSelected(User user)
    {
        SelectedUser = user;
        // Additional logic when a user is selected
    }
    
    private User SelectedUser { get; set; }
    
    private List<User> Users { get; set; } = new List<User>
    {
        new User { Id = 1, FirstName = "John", LastName = "Doe" },
        new User { Id = 2, FirstName = "Jane", LastName = "Smith" },
        new User { Id = 3, FirstName = "Robert", LastName = "Johnson" },
        // More users...
    };
    
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
```

### Example 3: AutoComplete with Custom Item Template

```razor
<AutoComplete TItem="Country"
              Items="@Countries"
              DisplayText="@(item => item.Name)"
              Placeholder="Select a country">
    <ItemTemplate>
        <div class="d-flex align-items-center">
            <img src="@context.FlagUrl" style="width: 24px; margin-right: 8px;" />
            <span>@context.Name</span>
            <small class="text-muted ml-2">(@context.Code)</small>
        </div>
    </ItemTemplate>
</AutoComplete>

@code {
    private List<Country> Countries { get; set; } = new List<Country>
    {
        new Country { Name = "United States", Code = "US", FlagUrl = "/images/flags/us.png" },
        new Country { Name = "Canada", Code = "CA", FlagUrl = "/images/flags/ca.png" },
        new Country { Name = "United Kingdom", Code = "GB", FlagUrl = "/images/flags/gb.png" },
        new Country { Name = "Australia", Code = "AU", FlagUrl = "/images/flags/au.png" },
        new Country { Name = "Germany", Code = "DE", FlagUrl = "/images/flags/de.png" }
    };
    
    public class Country
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string FlagUrl { get; set; }
    }
}
```

### Example 4: AutoComplete with Validation

```razor
<ValidateForm Model="@model">
    <Field TValue="string">
        <FieldLabel>City</FieldLabel>
        <AutoComplete TItem="string"
                      Items="@Cities"
                      @bind-Value="@model.City"
                      ShowClearButton="true"
                      IsAutoSelectFirstItem="true" />
        <ValidationMessage For="@(() => model.City)" />
    </Field>
    
    <Button Type="ButtonType.Submit">Submit</Button>
</ValidateForm>

@code {
    private FormModel model = new FormModel();
    
    private List<string> Cities { get; set; } = new List<string>
    {
        "New York", "Los Angeles", "Chicago", "Houston", "Phoenix",
        "Philadelphia", "San Antonio", "San Diego", "Dallas", "San Jose"
    };
    
    public class FormModel
    {
        [Required(ErrorMessage = "Please select a city")]
        public string City { get; set; }
    }
}
```

### Example 5: AutoComplete with Loading Indicator and No Results Template

```razor
<AutoComplete TItem="Product"
              OnSearchAsync="@SearchProducts"
              DisplayText="@(item => item.Name)"
              Placeholder="Search products..."
              MinLength="2">
    <LoadingTemplate>
        <div class="p-2 text-center">
            <Spinner Size="Size.Small" /> Loading products...
        </div>
    </LoadingTemplate>
    <NoDataTemplate>
        <div class="p-2 text-center">
            <i class="fa fa-info-circle mr-1"></i> No products found matching your search.
            <div>
                <Button Size="Size.Small" OnClick="@(() => CreateNewProduct(SearchText))">
                    Create "@SearchText"
                </Button>
            </div>
        </div>
    </NoDataTemplate>
</AutoComplete>

@code {
    private string SearchText { get; set; }
    
    private async Task<IEnumerable<Product>> SearchProducts(string searchText)
    {
        SearchText = searchText;
        
        // Simulate network delay
        await Task.Delay(1000);
        
        return Products.Where(p => p.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase));
    }
    
    private void CreateNewProduct(string name)
    {
        // Logic to create a new product
        var newProduct = new Product { Id = Products.Count + 1, Name = name };
        Products.Add(newProduct);
    }
    
    private List<Product> Products { get; set; } = new List<Product>
    {
        new Product { Id = 1, Name = "Laptop" },
        new Product { Id = 2, Name = "Smartphone" },
        new Product { Id = 3, Name = "Tablet" },
        new Product { Id = 4, Name = "Monitor" },
        new Product { Id = 5, Name = "Keyboard" }
    };
    
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
```

### Example 6: AutoComplete with Multiple Data Sources

```razor
<div class="mb-3">
    <RadioGroup @bind-Value="@dataSourceType" TValue="DataSourceType">
        <Radio TValue="DataSourceType" Value="DataSourceType.Local">Local Data</Radio>
        <Radio TValue="DataSourceType" Value="DataSourceType.Remote">Remote Data</Radio>
    </RadioGroup>
</div>

<AutoComplete TItem="string"
              Items="@(dataSourceType == DataSourceType.Local ? LocalItems : null)"
              OnSearchAsync="@(dataSourceType == DataSourceType.Remote ? FetchRemoteData : null)"
              Placeholder="Type to search..."
              ShowClearButton="true" />

@code {
    private DataSourceType dataSourceType = DataSourceType.Local;
    
    private List<string> LocalItems { get; set; } = new List<string>
    {
        "Apple", "Banana", "Cherry", "Date", "Elderberry",
        "Fig", "Grape", "Honeydew", "Kiwi", "Lemon"
    };
    
    private async Task<IEnumerable<string>> FetchRemoteData(string searchText)
    {
        // Simulate API call
        await Task.Delay(600);
        
        // This would be an actual API call in a real application
        var remoteItems = new List<string>
        {
            "Amsterdam", "Berlin", "Copenhagen", "Dublin", "Edinburgh",
            "Frankfurt", "Geneva", "Helsinki", "Istanbul", "Jerusalem"
        };
        
        return remoteItems.Where(item => 
            item.Contains(searchText, StringComparison.OrdinalIgnoreCase));
    }
    
    public enum DataSourceType
    {
        Local,
        Remote
    }
}
```

### Example 7: AutoComplete with Virtualization for Large Datasets

```razor
<AutoComplete TItem="Customer"
              Items="@Customers"
              DisplayText="@(item => item.CompanyName)"
              Placeholder="Search customers..."
              MaxItems="1000"
              IsVirtualization="true"
              VirtualItemHeight="40"
              OnSelectedItemChanged="@OnCustomerSelected" />

@code {
    private List<Customer> Customers { get; set; }
    
    protected override void OnInitialized()
    {
        // Generate a large dataset
        Customers = Enumerable.Range(1, 10000)
            .Select(i => new Customer
            {
                Id = i,
                CompanyName = $"Company {i}",
                ContactName = $"Contact {i}",
                Country = i % 5 == 0 ? "USA" : 
                          i % 5 == 1 ? "Canada" : 
                          i % 5 == 2 ? "UK" : 
                          i % 5 == 3 ? "Germany" : "France"
            })
            .ToList();
    }
    
    private void OnCustomerSelected(Customer customer)
    {
        // Handle customer selection
        Console.WriteLine($"Selected customer: {customer.CompanyName}");
    }
    
    public class Customer
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string Country { get; set; }
    }
}
```

## Customization Notes

The AutoComplete component can be customized using the following CSS variables:

```css
:root {
    --bb-autocomplete-dropdown-max-height: 300px;
    --bb-autocomplete-item-hover-background: rgba(0, 123, 255, 0.1);
    --bb-autocomplete-item-active-background: rgba(0, 123, 255, 0.2);
    --bb-autocomplete-loading-color: #6c757d;
    --bb-autocomplete-clear-button-color: #6c757d;
    --bb-autocomplete-clear-button-hover-color: #343a40;
}
```

Additionally, you can customize the appearance of the AutoComplete component by:

1. Using the `ItemTemplate` to customize how each suggestion item is rendered
2. Using the `LoadingTemplate` to customize the loading indicator
3. Using the `NoDataTemplate` to customize the message shown when no results are found
4. Applying custom CSS classes to the component using the `ClassName` property