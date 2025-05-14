# Filters Component

## Overview
The Filters component in BootstrapBlazor provides a powerful and flexible filtering interface for data collections. It allows users to define and apply multiple filter conditions to datasets, supporting various data types and comparison operators. This component is particularly useful for data-intensive applications where users need to narrow down large datasets based on specific criteria.

## Features
- **Multiple Filter Conditions**: Apply multiple filter criteria simultaneously
- **Dynamic Filter Building**: Add, remove, and modify filter conditions at runtime
- **Various Data Type Support**: Filter string, numeric, date, boolean, and enum values
- **Flexible Operators**: Supports operators like equals, contains, greater than, less than, etc.
- **Predefined Filter Templates**: Common filter patterns available out of the box
- **Custom Filter Rules**: Define custom filtering logic for complex scenarios
- **Filter Groups**: Group multiple conditions with AND/OR logic
- **Responsive Design**: Adapts to different screen sizes
- **Localization Support**: Multilingual filter interface

## Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `FilterItems` | `List<FilterItem>` | `new()` | Collection of filter items defining the available filter fields |
| `FilterRules` | `List<FilterRule>` | `new()` | Collection of filter rules currently applied |
| `ShowResetButton` | bool | true | Whether to show the reset button |
| `ShowSearchButton` | bool | true | Whether to show the search button |
| `ShowAddButton` | bool | true | Whether to show the add filter button |
| `ShowDeleteButton` | bool | true | Whether to show the delete filter button |
| `ShowLabel` | bool | true | Whether to show labels for filter fields |
| `ShowOperator` | bool | true | Whether to show the operator dropdown |
| `ShowLogicItems` | bool | true | Whether to show logic operators (AND/OR) |
| `IsGroup` | bool | false | Whether to enable filter groups |
| `MaxCount` | int | 5 | Maximum number of filter conditions allowed |
| `LabelAlign` | Alignment | `Alignment.None` | Alignment of filter labels |
| `LabelWidth` | int | 80 | Width of filter labels in pixels |
| `ItemWidth` | int | 180 | Width of filter items in pixels |
| `OperatorWidth` | int | 70 | Width of operator dropdown in pixels |
| `ValueWidth` | int | 180 | Width of filter value controls in pixels |
| `LogicWidth` | int | 60 | Width of logic operator controls in pixels |
| `OnFilterChanged` | EventCallback | - | Callback when filter conditions change |

## Events

| Event | Description |
|-------|-------------|
| `OnFilterChanged` | Triggered when filter conditions are changed |
| `OnFilterAdd` | Triggered when a new filter condition is added |
| `OnFilterDelete` | Triggered when a filter condition is deleted |
| `OnFilterReset` | Triggered when filters are reset |
| `OnFilterSearch` | Triggered when the search button is clicked |

## Usage Examples

### Example 1: Basic Filtering
```razor
@using BootstrapBlazor.Components

<Filters @bind-FilterItems="FilterItems" 
         @bind-FilterRules="FilterRules"
         OnFilterSearch="OnFilterSearch" />

@code {
    private List<FilterItem> FilterItems { get; set; } = new List<FilterItem>();
    private List<FilterRule> FilterRules { get; set; } = new List<FilterRule>();
    
    protected override void OnInitialized()
    {
        // Define available filter fields
        FilterItems.Add(new FilterItem() { FieldName = "Name", FieldLabel = "Name", FilterType = FilterType.String });
        FilterItems.Add(new FilterItem() { FieldName = "Age", FieldLabel = "Age", FilterType = FilterType.Number });
        FilterItems.Add(new FilterItem() { FieldName = "Birthday", FieldLabel = "Birthday", FilterType = FilterType.DateTime });
        FilterItems.Add(new FilterItem() { FieldName = "IsActive", FieldLabel = "Is Active", FilterType = FilterType.Boolean });
        
        // Initialize with a default filter
        FilterRules.Add(new FilterRule() { FieldName = "Name", FilterType = FilterType.String, Operator = FilterOperator.Contains, Value = "John" });
    }
    
    private Task OnFilterSearch(FilterEventArgs args)
    {
        // Apply filters to your data source
        Console.WriteLine($"Filtering with {args.FilterRules.Count} rules");
        return Task.CompletedTask;
    }
}
```

### Example 2: Advanced Filtering with Groups
```razor
<Filters @bind-FilterItems="FilterItems" 
         @bind-FilterRules="FilterRules"
         IsGroup="true"
         OnFilterSearch="OnFilterSearch" />

@code {
    private List<FilterItem> FilterItems { get; set; } = new List<FilterItem>();
    private List<FilterRule> FilterRules { get; set; } = new List<FilterRule>();
    
    protected override void OnInitialized()
    {
        // Define available filter fields
        FilterItems.Add(new FilterItem() { FieldName = "Name", FieldLabel = "Name", FilterType = FilterType.String });
        FilterItems.Add(new FilterItem() { FieldName = "Department", FieldLabel = "Department", FilterType = FilterType.String });
        FilterItems.Add(new FilterItem() { FieldName = "Salary", FieldLabel = "Salary", FilterType = FilterType.Number });
        FilterItems.Add(new FilterItem() { FieldName = "HireDate", FieldLabel = "Hire Date", FilterType = FilterType.DateTime });
        
        // Create a filter group
        var group = new FilterGroup() { FilterLogic = FilterLogic.Or };
        group.Filters.Add(new FilterRule() { FieldName = "Department", FilterType = FilterType.String, Operator = FilterOperator.Equal, Value = "IT" });
        group.Filters.Add(new FilterRule() { FieldName = "Department", FilterType = FilterType.String, Operator = FilterOperator.Equal, Value = "HR" });
        
        // Add the group and another standalone rule
        FilterRules.Add(group);
        FilterRules.Add(new FilterRule() { FieldName = "Salary", FilterType = FilterType.Number, Operator = FilterOperator.GreaterThan, Value = 50000 });
    }
    
    private Task OnFilterSearch(FilterEventArgs args)
    {
        // Process filter rules including groups
        return Task.CompletedTask;
    }
}
```

### Example 3: Custom Filter Templates
```razor
<Filters @bind-FilterItems="FilterItems" 
         @bind-FilterRules="FilterRules"
         OnFilterSearch="OnFilterSearch">
    <FilterTemplate Context="context">
        @if (context.FilterItem.FieldName == "Status")
        {
            <Select @bind-Value="context.FilterRule.Value" Items="StatusOptions" />
        }
        else
        {
            <div>@context.DefaultTemplate</div>
        }
    </FilterTemplate>
</Filters>

@code {
    private List<FilterItem> FilterItems { get; set; } = new List<FilterItem>();
    private List<FilterRule> FilterRules { get; set; } = new List<FilterRule>();
    private List<SelectedItem> StatusOptions { get; set; } = new List<SelectedItem>
    {
        new SelectedItem("Active", "Active"),
        new SelectedItem("Inactive", "Inactive"),
        new SelectedItem("Pending", "Pending")
    };
    
    protected override void OnInitialized()
    {
        FilterItems.Add(new FilterItem() { FieldName = "Name", FieldLabel = "Name", FilterType = FilterType.String });
        FilterItems.Add(new FilterItem() { FieldName = "Status", FieldLabel = "Status", FilterType = FilterType.String });
    }
    
    private Task OnFilterSearch(FilterEventArgs args)
    {
        return Task.CompletedTask;
    }
}
```

### Example 4: Integration with Table Component
```razor
<Table TItem="Employee"
       Items="Employees"
       ShowToolbar="true"
       ShowSearch="false">
    <TableToolbarTemplate>
        <Filters @bind-FilterItems="FilterItems" 
                 @bind-FilterRules="FilterRules"
                 OnFilterSearch="OnFilterSearch" />
    </TableToolbarTemplate>
    <TableColumns>
        <TableColumn @bind-Field="@context.Id" Width="80" />
        <TableColumn @bind-Field="@context.Name" />
        <TableColumn @bind-Field="@context.Department" />
        <TableColumn @bind-Field="@context.Salary" />
        <TableColumn @bind-Field="@context.HireDate" />
    </TableColumns>
</Table>

@code {
    private List<Employee> Employees { get; set; } = new List<Employee>();
    private List<Employee> FilteredEmployees { get; set; } = new List<Employee>();
    private List<FilterItem> FilterItems { get; set; } = new List<FilterItem>();
    private List<FilterRule> FilterRules { get; set; } = new List<FilterRule>();
    
    protected override void OnInitialized()
    {
        // Initialize employees data
        Employees = GetEmployees();
        FilteredEmployees = Employees;
        
        // Define filter fields based on Employee properties
        FilterItems.Add(new FilterItem() { FieldName = nameof(Employee.Name), FieldLabel = "Name", FilterType = FilterType.String });
        FilterItems.Add(new FilterItem() { FieldName = nameof(Employee.Department), FieldLabel = "Department", FilterType = FilterType.String });
        FilterItems.Add(new FilterItem() { FieldName = nameof(Employee.Salary), FieldLabel = "Salary", FilterType = FilterType.Number });
        FilterItems.Add(new FilterItem() { FieldName = nameof(Employee.HireDate), FieldLabel = "Hire Date", FilterType = FilterType.DateTime });
    }
    
    private Task OnFilterSearch(FilterEventArgs args)
    {
        // Apply filters to the Employees collection
        FilteredEmployees = FilterService.Apply(Employees, args.FilterRules);
        return Task.CompletedTask;
    }
    
    private List<Employee> GetEmployees()
    {
        // Return sample data
        return new List<Employee>();
    }
    
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public decimal Salary { get; set; }
        public DateTime HireDate { get; set; }
    }
}
```

### Example 5: Responsive Filter Layout
```razor
<div class="row">
    <div class="col-md-12">
        <Filters @bind-FilterItems="FilterItems" 
                 @bind-FilterRules="FilterRules"
                 LabelAlign="Alignment.Left"
                 ItemWidth="150"
                 ValueWidth="150"
                 OnFilterSearch="OnFilterSearch" />
    </div>
</div>

@code {
    private List<FilterItem> FilterItems { get; set; } = new List<FilterItem>();
    private List<FilterRule> FilterRules { get; set; } = new List<FilterRule>();
    
    protected override void OnInitialized()
    {
        FilterItems.Add(new FilterItem() { FieldName = "ProductName", FieldLabel = "Product", FilterType = FilterType.String });
        FilterItems.Add(new FilterItem() { FieldName = "Category", FieldLabel = "Category", FilterType = FilterType.String });
        FilterItems.Add(new FilterItem() { FieldName = "Price", FieldLabel = "Price", FilterType = FilterType.Number });
    }
    
    private Task OnFilterSearch(FilterEventArgs args)
    {
        return Task.CompletedTask;
    }
}
```

### Example 6: Custom Filter Logic
```razor
<Filters @bind-FilterItems="FilterItems" 
         @bind-FilterRules="FilterRules"
         OnFilterSearch="OnFilterSearch" />

@code {
    private List<FilterItem> FilterItems { get; set; } = new List<FilterItem>();
    private List<FilterRule> FilterRules { get; set; } = new List<FilterRule>();
    
    protected override void OnInitialized()
    {
        // Define a custom filter with a custom filter provider
        FilterItems.Add(new FilterItem() 
        { 
            FieldName = "Tags", 
            FieldLabel = "Tags", 
            FilterType = FilterType.String,
            FilterProvider = new CustomArrayFilterProvider()
        });
        
        FilterItems.Add(new FilterItem() { FieldName = "Title", FieldLabel = "Title", FilterType = FilterType.String });
    }
    
    private Task OnFilterSearch(FilterEventArgs args)
    {
        // The custom filter provider will handle the Tags filtering
        return Task.CompletedTask;
    }
    
    // Custom filter provider for array-type fields
    private class CustomArrayFilterProvider : IFilterProvider
    {
        public bool IsMatch(object value, FilterRule rule)
        {
            if (value is string[] tags && rule.Value is string searchTag)
            {
                // Custom logic to search in string arrays
                return tags.Any(tag => tag.Contains(searchTag, StringComparison.OrdinalIgnoreCase));
            }
            return false;
        }
    }
}
```

### Example 7: Localized Filters
```razor
<Filters @bind-FilterItems="FilterItems" 
         @bind-FilterRules="FilterRules"
         OnFilterSearch="OnFilterSearch" />

@code {
    private List<FilterItem> FilterItems { get; set; } = new List<FilterItem>();
    private List<FilterRule> FilterRules { get; set; } = new List<FilterRule>();
    
    [Inject] private IStringLocalizer<Filters> Localizer { get; set; }
    
    protected override void OnInitialized()
    {
        // Use localized labels
        FilterItems.Add(new FilterItem() { FieldName = "Name", FieldLabel = Localizer["Name"], FilterType = FilterType.String });
        FilterItems.Add(new FilterItem() { FieldName = "Age", FieldLabel = Localizer["Age"], FilterType = FilterType.Number });
        FilterItems.Add(new FilterItem() { FieldName = "Birthday", FieldLabel = Localizer["Birthday"], FilterType = FilterType.DateTime });
    }
    
    private Task OnFilterSearch(FilterEventArgs args)
    {
        return Task.CompletedTask;
    }
}
```

## CSS Customization

The Filters component can be customized using CSS variables and classes:

```css
/* Custom filter styling */
.filter-container {
    --bb-filter-border-color: #dee2e6;
    --bb-filter-bg: #f8f9fa;
    --bb-filter-item-spacing: 0.5rem;
}

/* Custom styling for filter buttons */
.filter-container .filter-buttons .btn-primary {
    background-color: #007bff;
    border-color: #007bff;
}

/* Custom styling for filter groups */
.filter-container .filter-group {
    border: 1px dashed #6c757d;
    border-radius: 0.25rem;
    padding: 0.5rem;
}
```

## JavaScript Interop

The Filters component uses JavaScript interop for some advanced features like drag-and-drop reordering of filter conditions and responsive layout adjustments.

## Accessibility

The Filters component is designed with accessibility in mind, providing proper ARIA attributes and keyboard navigation support. Filter controls are properly labeled and can be navigated using keyboard shortcuts.

## Performance Considerations

For applications with a large number of filter fields or complex filter conditions, consider the following performance optimizations:

1. Limit the number of filter conditions using the `MaxCount` property
2. Use server-side filtering for large datasets
3. Implement debouncing for filter changes to reduce the number of filter operations

## Browser Compatibility

The Filters component is compatible with all modern browsers. For older browsers, some advanced features like custom styling may have limited support.

## Integration with Other Components

The Filters component integrates well with other BootstrapBlazor components:

- Use with `Table` for filtered data tables
- Combine with `Pagination` for paginated filtered results
- Use with `Chart` to filter visualized data
- Integrate with `Export` to export filtered data