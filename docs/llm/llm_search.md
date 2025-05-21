# Search Component

## Overview

The Search component in BootstrapBlazor provides a flexible and customizable search interface for filtering data or performing search operations within an application. It offers a clean, intuitive user experience with features like instant search, search history, and various input customization options. This component is particularly useful for applications that need to provide users with quick access to search functionality across different sections or data collections.

## Features

- **Instant Search**: Real-time search results as users type
- **Search History**: Optional tracking of recent search queries
- **Customizable Appearance**: Various styling options including size, color, and icon customization
- **Advanced Search Options**: Support for complex search criteria and filters
- **Keyboard Navigation**: Keyboard shortcuts and accessibility features
- **Search Suggestions**: Optional dropdown with search suggestions or autocomplete
- **Placeholder Text**: Customizable placeholder text for better user guidance

## Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `Value` | `string` | `""` | The current search text value. Can be bound using `@bind-Value`. |
| `PlaceHolder` | `string` | `"Search..."` | Placeholder text displayed when the search input is empty. |
| `IgnoreCase` | `bool` | `true` | Whether to ignore case when performing searches. |
| `ShowClearButton` | `bool` | `true` | Whether to show a clear button to reset the search text. |
| `ShowSearchButton` | `bool` | `true` | Whether to show a search button to trigger the search action. |
| `ShowSearchHistory` | `bool` | `false` | Whether to show and track search history. |
| `MaxHistoryCount` | `int` | `10` | Maximum number of search history items to store when history is enabled. |
| `Size` | `Size` | `Size.None` | Size of the search component. Options include Small, Medium, and Large. |
| `Color` | `Color` | `Color.None` | Color theme of the search component. |
| `IsDisabled` | `bool` | `false` | Whether the search component is disabled. |
| `IsReadonly` | `bool` | `false` | Whether the search component is read-only. |
| `SearchIcon` | `string` | `"fa-solid fa-search"` | Icon used for the search button. |
| `ClearIcon` | `string` | `"fa-solid fa-times"` | Icon used for the clear button. |
| `DebounceInterval` | `int` | `300` | Debounce interval in milliseconds for the OnSearch event when typing. |
| `SearchOnInput` | `bool` | `false` | Whether to trigger search while typing (after debounce interval). |
| `SearchOnEnter` | `bool` | `true` | Whether to trigger search when pressing Enter key. |
| `SearchButtonText` | `string` | `null` | Optional text to display on the search button. If null, only the icon is shown. |
| `AdditionalAttributes` | `Dictionary<string, object>` | `null` | Additional attributes to apply to the search input element. |

## Events

| Event | Description |
|-------|-------------|
| `OnSearch` | Triggered when a search is performed, either by clicking the search button, pressing Enter, or typing (if SearchOnInput is true). Provides the current search text. |
| `OnClear` | Triggered when the search text is cleared, either by clicking the clear button or programmatically. |
| `OnValueChanged` | Triggered when the search text value changes. Provides the new search text. |
| `OnFocus` | Triggered when the search input receives focus. |
| `OnBlur` | Triggered when the search input loses focus. |
| `OnKeyUp` | Triggered when a key is released while the search input has focus. Provides the KeyboardEventArgs. |
| `OnKeyDown` | Triggered when a key is pressed while the search input has focus. Provides the KeyboardEventArgs. |
| `OnHistoryItemSelected` | Triggered when a search history item is selected. Provides the selected history item text. |

## Usage Examples

### Example 1: Basic Search

A simple search component with default settings:

```razor
<Search @bind-Value="@_searchText" OnSearch="HandleSearch" />

<div class="mt-3">
    @if (!string.IsNullOrEmpty(_searchText))
    {
        <p>You searched for: <strong>@_searchText</strong></p>
    }
</div>

@code {
    private string _searchText;

    private void HandleSearch(string searchText)
    {
        Console.WriteLine($"Search triggered with text: {searchText}");
        // Perform search operation here
    }
}
```

### Example 2: Customized Search Appearance

A search component with customized appearance and behavior:

```razor
<Search @bind-Value="@_searchText"
        PlaceHolder="Enter keywords..."
        Size="Size.Large"
        Color="Color.Primary"
        SearchIcon="fa-solid fa-magnifying-glass"
        ClearIcon="fa-solid fa-xmark"
        ShowSearchButton="true"
        ShowClearButton="true"
        SearchButtonText="Find"
        OnSearch="HandleSearch"
        OnClear="HandleClear" />

@code {
    private string _searchText;

    private void HandleSearch(string searchText)
    {
        // Perform search operation
    }

    private void HandleClear()
    {
        // Handle clear event
        Console.WriteLine("Search cleared");
    }
}
```

### Example 3: Search with History

Implementing search with history tracking:

```razor
<Search @bind-Value="@_searchText"
        PlaceHolder="Search products..."
        ShowSearchHistory="true"
        MaxHistoryCount="5"
        OnSearch="HandleSearch"
        OnHistoryItemSelected="HandleHistoryItemSelected" />

<div class="mt-3">
    @if (_searchResults.Any())
    {
        <h5>Search Results</h5>
        <ul class="list-group">
            @foreach (var result in _searchResults)
            {
                <li class="list-group-item">@result</li>
            }
        </ul>
    }
</div>

@code {
    private string _searchText;
    private List<string> _searchResults = new();
    private List<string> _products = new() 
    { 
        "Laptop", "Smartphone", "Tablet", "Monitor", 
        "Keyboard", "Mouse", "Headphones", "Speakers" 
    };

    private void HandleSearch(string searchText)
    {
        if (string.IsNullOrEmpty(searchText))
        {
            _searchResults.Clear();
            return;
        }

        _searchResults = _products
            .Where(p => p.Contains(searchText, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    private void HandleHistoryItemSelected(string historyItem)
    {
        Console.WriteLine($"History item selected: {historyItem}");
        // The search text will be automatically updated, and OnSearch will be triggered
    }
}
```

### Example 4: Instant Search with Debounce

Implementing instant search with debounce to reduce unnecessary search operations:

```razor
<Search @bind-Value="@_searchText"
        PlaceHolder="Search as you type..."
        SearchOnInput="true"
        DebounceInterval="500"
        ShowSearchButton="false"
        OnSearch="HandleSearch" />

<div class="mt-3">
    @if (_isSearching)
    {
        <div class="d-flex align-items-center">
            <span class="spinner-border spinner-border-sm me-2" role="status"></span>
            <span>Searching...</span>
        </div>
    }
    else if (_searchResults.Any())
    {
        <div class="list-group">
            @foreach (var result in _searchResults)
            {
                <button class="list-group-item list-group-item-action">@result</button>
            }
        </div>
    }
    else if (!string.IsNullOrEmpty(_searchText))
    {
        <div class="alert alert-info">No results found for "@_searchText"</div>
    }
</div>

@code {
    private string _searchText;
    private List<string> _searchResults = new();
    private bool _isSearching;
    private List<string> _allItems = new() 
    { 
        "Apple", "Banana", "Cherry", "Date", "Elderberry", 
        "Fig", "Grape", "Honeydew", "Kiwi", "Lemon" 
    };

    private async Task HandleSearch(string searchText)
    {
        if (string.IsNullOrEmpty(searchText))
        {
            _searchResults.Clear();
            return;
        }

        _isSearching = true;
        StateHasChanged();

        // Simulate API call delay
        await Task.Delay(300);

        _searchResults = _allItems
            .Where(item => item.Contains(searchText, StringComparison.OrdinalIgnoreCase))
            .ToList();

        _isSearching = false;
    }
}
```

### Example 5: Advanced Search with Filters

Implementing a search with additional filter options:

```razor
<div class="card">
    <div class="card-header">
        <h5 class="mb-0">Product Search</h5>
    </div>
    <div class="card-body">
        <div class="row mb-3">
            <div class="col">
                <Search @bind-Value="@_searchText"
                        PlaceHolder="Search products..."
                        OnSearch="HandleSearch" />
            </div>
        </div>
        
        <div class="row mb-3">
            <div class="col-md-3">
                <div class="form-group">
                    <Select TValue="string" 
                            @bind-Value="@_selectedCategory"
                            Items="@_categories"
                            PlaceHolder="All Categories"
                            OnSelectedItemChanged="HandleCategoryChanged" />
                </div>
            </div>
            <div class="col-md-3">
                <div class="form-group">
                    <Select TValue="decimal?" 
                            @bind-Value="@_maxPrice"
                            Items="@_priceRanges"
                            PlaceHolder="Any Price"
                            OnSelectedItemChanged="HandlePriceChanged" />
                </div>
            </div>
            <div class="col-md-3">
                <div class="form-check">
                    <Checkbox @bind-Value="@_inStockOnly" 
                              Text="In Stock Only"
                              OnStateChanged="HandleStockFilterChanged" />
                </div>
            </div>
            <div class="col-md-3 text-end">
                <Button Color="Color.Secondary" 
                        OnClick="ResetFilters">Reset Filters</Button>
            </div>
        </div>
        
        <div class="row">
            <div class="col">
                @if (_filteredProducts.Any())
                {
                    <Table Items="@_filteredProducts"
                           IsBordered="true"
                           IsStriped="true"
                           IsHoverable="true">
                        <TableColumns>
                            <TableColumn @bind-Field="@context.Name" Width="200" />
                            <TableColumn @bind-Field="@context.Category" Width="150" />
                            <TableColumn @bind-Field="@context.Price" Width="120" FormatString="{0:C2}" />
                            <TableColumn @bind-Field="@context.InStock" Width="100" />
                        </TableColumns>
                    </Table>
                }
                else
                {
                    <div class="alert alert-info">No products match your search criteria.</div>
                }
            </div>
        </div>
    </div>
</div>

@code {
    private string _searchText;
    private string _selectedCategory;
    private decimal? _maxPrice;
    private bool _inStockOnly;
    
    private List<Product> _allProducts = new();
    private List<Product> _filteredProducts = new();
    
    private List<SelectedItem> _categories = new();
    private List<SelectedItem> _priceRanges = new()
    {
        new SelectedItem { Text = "Under $50", Value = "50" },
        new SelectedItem { Text = "Under $100", Value = "100" },
        new SelectedItem { Text = "Under $200", Value = "200" },
        new SelectedItem { Text = "Under $500", Value = "500" }
    };
    
    protected override void OnInitialized()
    {
        // Initialize with sample data
        _allProducts = GetSampleProducts();
        _filteredProducts = _allProducts;
        
        // Extract categories for filter
        _categories = _allProducts
            .Select(p => p.Category)
            .Distinct()
            .Select(c => new SelectedItem { Text = c, Value = c })
            .ToList();
    }
    
    private void HandleSearch(string searchText)
    {
        ApplyFilters();
    }
    
    private void HandleCategoryChanged(SelectedItem item)
    {
        ApplyFilters();
    }
    
    private void HandlePriceChanged(SelectedItem item)
    {
        if (item != null && decimal.TryParse(item.Value?.ToString(), out decimal price))
        {
            _maxPrice = price;
        }
        else
        {
            _maxPrice = null;
        }
        
        ApplyFilters();
    }
    
    private void HandleStockFilterChanged(bool value)
    {
        ApplyFilters();
    }
    
    private void ResetFilters()
    {
        _searchText = string.Empty;
        _selectedCategory = null;
        _maxPrice = null;
        _inStockOnly = false;
        
        _filteredProducts = _allProducts;
    }
    
    private void ApplyFilters()
    {
        _filteredProducts = _allProducts;
        
        // Apply text search
        if (!string.IsNullOrEmpty(_searchText))
        {
            _filteredProducts = _filteredProducts
                .Where(p => p.Name.Contains(_searchText, StringComparison.OrdinalIgnoreCase) ||
                           p.Description.Contains(_searchText, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
        
        // Apply category filter
        if (!string.IsNullOrEmpty(_selectedCategory))
        {
            _filteredProducts = _filteredProducts
                .Where(p => p.Category == _selectedCategory)
                .ToList();
        }
        
        // Apply price filter
        if (_maxPrice.HasValue)
        {
            _filteredProducts = _filteredProducts
                .Where(p => p.Price <= _maxPrice.Value)
                .ToList();
        }
        
        // Apply stock filter
        if (_inStockOnly)
        {
            _filteredProducts = _filteredProducts
                .Where(p => p.InStock)
                .ToList();
        }
    }
    
    private List<Product> GetSampleProducts()
    {
        // Return sample product data
        return new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Category = "Electronics", Price = 999.99m, InStock = true, Description = "High-performance laptop with SSD" },
            new Product { Id = 2, Name = "Smartphone", Category = "Electronics", Price = 699.99m, InStock = true, Description = "Latest model with advanced camera" },
            new Product { Id = 3, Name = "Headphones", Category = "Accessories", Price = 149.99m, InStock = false, Description = "Noise-cancelling wireless headphones" },
            new Product { Id = 4, Name = "Coffee Maker", Category = "Appliances", Price = 89.99m, InStock = true, Description = "Programmable coffee maker with timer" },
            new Product { Id = 5, Name = "Desk Chair", Category = "Furniture", Price = 199.99m, InStock = true, Description = "Ergonomic office chair with lumbar support" },
            new Product { Id = 6, Name = "Tablet", Category = "Electronics", Price = 349.99m, InStock = false, Description = "Lightweight tablet with long battery life" },
            new Product { Id = 7, Name = "Blender", Category = "Appliances", Price = 79.99m, InStock = true, Description = "High-speed blender for smoothies and more" },
            new Product { Id = 8, Name = "Bookshelf", Category = "Furniture", Price = 129.99m, InStock = true, Description = "Modern bookshelf with adjustable shelves" }
        };
    }
    
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public bool InStock { get; set; }
        public string Description { get; set; }
    }
}
```

### Example 6: Search with Suggestions

Implementing a search with auto-suggestions dropdown:

```razor
<div class="position-relative">
    <Search @bind-Value="@_searchText"
            PlaceHolder="Search with suggestions..."
            OnValueChanged="HandleSearchTextChanged"
            OnSearch="HandleSearch" />
    
    @if (_suggestions.Any() && !string.IsNullOrEmpty(_searchText))
    {
        <div class="search-suggestions">
            @foreach (var suggestion in _suggestions)
            {
                <div class="suggestion-item" @onclick="() => SelectSuggestion(suggestion)">
                    <i class="fa-solid fa-search me-2 text-muted"></i>
                    <span>@suggestion</span>
                </div>
            }
        </div>
    }
</div>

<style>
    .search-suggestions {
        position: absolute;
        top: 100%;
        left: 0;
        right: 0;
        z-index: 1000;
        background-color: white;
        border: 1px solid #dee2e6;
        border-top: none;
        border-radius: 0 0 0.25rem 0.25rem;
        box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
        max-height: 300px;
        overflow-y: auto;
    }
    
    .suggestion-item {
        padding: 0.5rem 1rem;
        cursor: pointer;
    }
    
    .suggestion-item:hover {
        background-color: #f8f9fa;
    }
</style>

@code {
    private string _searchText;
    private List<string> _suggestions = new();
    private List<string> _allItems = new()
    {
        "JavaScript", "TypeScript", "React", "Angular", "Vue.js",
        "Node.js", "Express", "MongoDB", "MySQL", "PostgreSQL",
        "Python", "Django", "Flask", "Ruby", "Ruby on Rails",
        "PHP", "Laravel", "Symfony", "C#", ".NET Core",
        "Java", "Spring Boot", "Kotlin", "Swift", "Objective-C"
    };

    private void HandleSearchTextChanged(string searchText)
    {
        if (string.IsNullOrEmpty(searchText))
        {
            _suggestions.Clear();
            return;
        }

        // Generate suggestions based on the search text
        _suggestions = _allItems
            .Where(item => item.Contains(searchText, StringComparison.OrdinalIgnoreCase))
            .Take(5)
            .ToList();
    }

    private void SelectSuggestion(string suggestion)
    {
        _searchText = suggestion;
        _suggestions.Clear();
        HandleSearch(suggestion);
    }

    private void HandleSearch(string searchText)
    {
        Console.WriteLine($"Search performed with: {searchText}");
        // Perform actual search operation
    }
}
```

### Example 7: Search in a Navbar

Implementing a search component in a navigation bar:

```razor
<nav class="navbar navbar-expand-lg navbar-dark bg-dark">
    <div class="container">
        <a class="navbar-brand" href="#">My App</a>
        
        <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarContent">
            <span class="navbar-toggler-icon"></span>
        </button>
        
        <div class="collapse navbar-collapse" id="navbarContent">
            <ul class="navbar-nav me-auto mb-2 mb-lg-0">
                <li class="nav-item">
                    <a class="nav-link active" href="#">Home</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" href="#">Products</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" href="#">Services</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" href="#">About</a>
                </li>
            </ul>
            
            <div class="d-flex">
                <!-- Desktop search -->
                <div class="d-none d-md-block">
                    <Search @bind-Value="@_searchText"
                            PlaceHolder="Search..."
                            Size="Size.Small"
                            Color="Color.Light"
                            OnSearch="HandleSearch" />
                </div>
                
                <!-- Mobile search toggle -->
                <div class="d-md-none">
                    <Button Color="Color.Link" 
                            Class="text-light" 
                            OnClick="ToggleMobileSearch">
                        <i class="fa-solid fa-search"></i>
                    </Button>
                </div>
            </div>
        </div>
    </div>
</nav>

<!-- Mobile search panel -->
@if (_showMobileSearch)
{
    <div class="mobile-search-panel p-2 bg-dark">
        <Search @bind-Value="@_searchText"
                PlaceHolder="Search..."
                Color="Color.Light"
                OnSearch="HandleMobileSearch" />
    </div>
}

<style>
    .mobile-search-panel {
        position: relative;
        z-index: 1000;
        box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
    }
</style>

@code {
    private string _searchText;
    private bool _showMobileSearch;

    private void ToggleMobileSearch()
    {
        _showMobileSearch = !_showMobileSearch;
    }

    private void HandleSearch(string searchText)
    {
        Console.WriteLine($"Desktop search: {searchText}");
        // Perform search operation
    }

    private void HandleMobileSearch(string searchText)
    {
        Console.WriteLine($"Mobile search: {searchText}");
        // Perform search operation
        _showMobileSearch = false; // Hide mobile search after searching
    }
}
```

## CSS Customization

The Search component can be customized using the following CSS classes:

- `.search-bar`: The main container for the search component
- `.search-input`: The input element for entering search text
- `.search-button`: The button element for triggering search
- `.search-clear`: The button element for clearing search text
- `.search-history`: The container for search history items
- `.search-history-item`: Individual search history items

You can override these classes in your application's CSS to customize the appearance of the Search component. For example:

```css
/* Custom search styling */
.search-bar {
    border-radius: 20px;
    box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
}

.search-input {
    border: none;
    padding-left: 15px;
}

.search-button {
    border-radius: 0 20px 20px 0;
}

.search-clear {
    color: #6c757d;
}

.search-history {
    border-radius: 10px;
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
}

.search-history-item:hover {
    background-color: #f8f9fa;
}
```

## JavaScript Interop

The Search component uses JavaScript interop for the following features:

- Handling keyboard events (Enter key for search, Escape key for clear)
- Managing focus and blur events
- Implementing debounce for the search-as-you-type functionality
- Managing search history in browser storage (when enabled)
- Handling dropdown behavior for search suggestions or history

## Accessibility Considerations

When using the Search component, consider the following accessibility best practices:

1. Always provide a descriptive placeholder text to indicate the purpose of the search
2. Ensure sufficient color contrast between the search text and background
3. Use appropriate ARIA attributes for screen readers
4. Ensure keyboard navigation works properly (Tab to focus, Enter to search)
5. Consider adding a visible label for the search input when appropriate

## Browser Compatibility

The Search component is compatible with all modern browsers that support Blazor WebAssembly or Blazor Server. There are no specific browser compatibility issues to be aware of.

## Integration with Other Components

The Search component works well with:

- **Table/DataGrid Components**: For filtering displayed data
- **Pagination Components**: For paginating search results
- **Dropdown Components**: For advanced search options or filters
- **Modal/Dialog Components**: For displaying detailed search results
- **Navigation Components**: For implementing search within navigation bars

## Best Practices

1. Use debounce for search-as-you-type to prevent excessive server requests
2. Provide clear visual feedback during search operations, especially for slower searches
3. Consider implementing search suggestions for better user experience
4. Use appropriate search button size and placement based on the context
5. Implement responsive design for search components that work well on both desktop and mobile
6. Store and display recent searches to help users quickly repeat common searches
7. Provide clear error messages when search operations fail