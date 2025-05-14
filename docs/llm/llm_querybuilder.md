# QueryBuilder Component

## Overview

The QueryBuilder component in BootstrapBlazor provides a powerful and flexible interface for creating complex query expressions through a visual interface. It allows users to build structured queries with multiple conditions, operators, and groupings without writing code. This component is particularly useful for applications that require advanced filtering, reporting, or data analysis capabilities where users need to define custom search criteria.

## Features

- **Visual Query Construction**: Intuitive interface for building complex queries without coding
- **Hierarchical Condition Groups**: Support for nested condition groups with AND/OR logic
- **Multiple Data Types**: Built-in support for various data types (string, number, date, boolean, etc.)
- **Custom Operators**: Comprehensive set of comparison operators appropriate for each data type
- **Dynamic Field Selection**: Ability to select from available fields or properties to query against
- **Real-time Query Preview**: Live preview of the constructed query in readable format
- **Query Serialization**: Convert queries to/from JSON for saving and loading
- **Customizable UI**: Flexible styling and layout options to match application design

## Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `Fields` | `IEnumerable<QueryField>` | `null` | Collection of available fields that can be used in query conditions. Each field defines its name, data type, and available operators. |
| `Value` | `QueryGroup` | `null` | The current query expression value, representing the root group of conditions. |
| `MaxDepth` | `int` | `3` | Maximum allowed nesting depth for condition groups. |
| `ShowPreview` | `bool` | `true` | Whether to display a preview of the constructed query expression. |
| `PreviewFormat` | `QueryPreviewFormat` | `QueryPreviewFormat.Text` | Format for displaying the query preview (Text, SQL, JSON, etc.). |
| `AllowRootGroupTypeChange` | `bool` | `true` | Whether to allow changing the logical operator (AND/OR) of the root group. |
| `AllowAddGroup` | `bool` | `true` | Whether to allow adding new condition groups. |
| `AllowRemoveGroup` | `bool` | `true` | Whether to allow removing condition groups. |
| `AllowAddRule` | `bool` | `true` | Whether to allow adding new conditions/rules. |
| `AllowRemoveRule` | `bool` | `true` | Whether to allow removing conditions/rules. |
| `ShowGroupActions` | `bool` | `true` | Whether to display action buttons for groups (add, remove, etc.). |
| `ShowRuleActions` | `bool` | `true` | Whether to display action buttons for rules (add, remove, etc.). |
| `ShowValidationMessage` | `bool` | `true` | Whether to display validation messages for invalid conditions. |
| `QueryBuilderTemplate` | `RenderFragment` | `null` | Custom template for the entire query builder component. |
| `GroupTemplate` | `RenderFragment<QueryGroup>` | `null` | Custom template for rendering condition groups. |
| `RuleTemplate` | `RenderFragment<QueryRule>` | `null` | Custom template for rendering individual conditions/rules. |

## Events

| Event | Description |
|-------|-------------|
| `OnValueChanged` | Triggered when the query expression changes. Provides the updated QueryGroup value. |
| `OnQueryPreviewGenerated` | Triggered when a new query preview is generated. Provides the preview text in the specified format. |
| `OnAddGroup` | Triggered when a new condition group is added. Provides the parent group and the newly added group. |
| `OnRemoveGroup` | Triggered when a condition group is removed. Provides the parent group and the removed group. |
| `OnAddRule` | Triggered when a new condition/rule is added. Provides the parent group and the newly added rule. |
| `OnRemoveRule` | Triggered when a condition/rule is removed. Provides the parent group and the removed rule. |
| `OnValidationStateChanged` | Triggered when the validation state of the query builder changes. Provides a boolean indicating if the query is valid. |

## Usage Examples

### Example 1: Basic Query Builder

A simple query builder with string, number, and date fields:

```razor
<QueryBuilder Fields="@_fields" @bind-Value="_query" />

@code {
    private QueryGroup _query = new();
    private List<QueryField> _fields = new()
    {
        new QueryField { Name = "Name", Label = "Customer Name", Type = QueryFieldType.String },
        new QueryField { Name = "Age", Label = "Customer Age", Type = QueryFieldType.Number },
        new QueryField { Name = "JoinDate", Label = "Join Date", Type = QueryFieldType.Date }
    };
}
```

### Example 2: Query Builder with Preview and Export

A query builder that shows a SQL-like preview and allows exporting the query as JSON:

```razor
<div class="card mb-3">
    <div class="card-body">
        <QueryBuilder Fields="@_fields" 
                     @bind-Value="_query" 
                     ShowPreview="true" 
                     PreviewFormat="QueryPreviewFormat.SQL" />
    </div>
</div>

<div class="mb-3">
    <Button Color="Color.Primary" OnClick="ExportQuery">Export Query</Button>
    <Button Color="Color.Secondary" OnClick="ImportQuery">Import Query</Button>
</div>

@if (!string.IsNullOrEmpty(_exportedQuery))
{
    <div class="card">
        <div class="card-header">Exported Query (JSON)</div>
        <div class="card-body">
            <pre>@_exportedQuery</pre>
        </div>
    </div>
}

@code {
    private QueryGroup _query = new();
    private string _exportedQuery;
    private List<QueryField> _fields = new()
    {
        new QueryField { Name = "ProductName", Label = "Product Name", Type = QueryFieldType.String },
        new QueryField { Name = "Price", Label = "Price", Type = QueryFieldType.Number },
        new QueryField { Name = "InStock", Label = "In Stock", Type = QueryFieldType.Boolean },
        new QueryField { Name = "Category", Label = "Category", Type = QueryFieldType.String }
    };

    private void ExportQuery()
    {
        _exportedQuery = System.Text.Json.JsonSerializer.Serialize(_query, new System.Text.Json.JsonSerializerOptions
        {
            WriteIndented = true
        });
    }

    private void ImportQuery()
    {
        // In a real application, you would show a dialog to paste or upload JSON
        // For this example, we'll just create a sample query programmatically
        _query = new QueryGroup
        {
            LogicalOperator = LogicalOperator.And,
            Rules = new List<QueryRule>
            {
                new QueryRule
                {
                    Field = "Price",
                    Operator = ">",
                    Value = "100"
                }
            },
            Groups = new List<QueryGroup>
            {
                new QueryGroup
                {
                    LogicalOperator = LogicalOperator.Or,
                    Rules = new List<QueryRule>
                    {
                        new QueryRule
                        {
                            Field = "Category",
                            Operator = "=",
                            Value = "Electronics"
                        },
                        new QueryRule
                        {
                            Field = "Category",
                            Operator = "=",
                            Value = "Computers"
                        }
                    }
                }
            }
        };
    }
}
```

### Example 3: Integration with Data Grid

Using the QueryBuilder to filter a Table component:

```razor
<div class="row mb-3">
    <div class="col-12">
        <div class="card">
            <div class="card-header">Filter Products</div>
            <div class="card-body">
                <QueryBuilder Fields="@_fields" 
                             @bind-Value="_query" 
                             OnValueChanged="HandleQueryChanged" />
            </div>
        </div>
    </div>
</div>

<Table TItem="Product"
       Items="@_filteredProducts"
       PageSize="10"
       ShowToolbar="true"
       ShowSearch="false"
       ShowRefresh="true"
       OnRefresh="RefreshData">
    <TableColumns>
        <TableColumn @bind-Field="@context.Id" Width="80" />
        <TableColumn @bind-Field="@context.Name" />
        <TableColumn @bind-Field="@context.Category" />
        <TableColumn @bind-Field="@context.Price" FormatString="{0:C}" />
        <TableColumn @bind-Field="@context.InStock" />
    </TableColumns>
</Table>

@code {
    private List<Product> _allProducts = new();
    private List<Product> _filteredProducts = new();
    private QueryGroup _query = new();
    private List<QueryField> _fields = new()
    {
        new QueryField { Name = "Name", Label = "Product Name", Type = QueryFieldType.String },
        new QueryField { Name = "Category", Label = "Category", Type = QueryFieldType.String },
        new QueryField { Name = "Price", Label = "Price", Type = QueryFieldType.Number },
        new QueryField { Name = "InStock", Label = "In Stock", Type = QueryFieldType.Boolean }
    };

    protected override void OnInitialized()
    {
        // Initialize with sample data
        _allProducts = GetSampleProducts();
        _filteredProducts = _allProducts;
    }

    private void HandleQueryChanged(QueryGroup query)
    {
        // Apply the query to filter products
        _filteredProducts = ApplyQuery(_allProducts, query);
    }

    private void RefreshData()
    {
        // Reset the query and show all products
        _query = new QueryGroup();
        _filteredProducts = _allProducts;
    }

    private List<Product> ApplyQuery(List<Product> products, QueryGroup query)
    {
        // In a real application, you would convert the QueryGroup to a LINQ expression
        // For this example, we'll just return all products
        return products;
    }

    private List<Product> GetSampleProducts()
    {
        // Return sample product data
        return new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Category = "Electronics", Price = 999.99m, InStock = true },
            new Product { Id = 2, Name = "Smartphone", Category = "Electronics", Price = 699.99m, InStock = true },
            new Product { Id = 3, Name = "Headphones", Category = "Accessories", Price = 149.99m, InStock = false }
        };
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public bool InStock { get; set; }
    }
}
```

### Example 4: Custom Field Operators

Defining custom operators for specific field types:

```razor
<QueryBuilder Fields="@_fields" @bind-Value="_query" />

@code {
    private QueryGroup _query = new();
    private List<QueryField> _fields = new()
    {
        new QueryField 
        { 
            Name = "Email", 
            Label = "Email Address", 
            Type = QueryFieldType.String,
            Operators = new List<QueryOperator>
            {
                new QueryOperator { Name = "contains", Label = "Contains", ApplicableTypes = new[] { QueryFieldType.String } },
                new QueryOperator { Name = "endsWith", Label = "Ends With", ApplicableTypes = new[] { QueryFieldType.String } },
                new QueryOperator { Name = "=", Label = "Equals", ApplicableTypes = new[] { QueryFieldType.String } },
                new QueryOperator { Name = "isEmail", Label = "Is Valid Email", ApplicableTypes = new[] { QueryFieldType.String } }
            }
        },
        new QueryField 
        { 
            Name = "Score", 
            Label = "Customer Score", 
            Type = QueryFieldType.Number,
            Operators = new List<QueryOperator>
            {
                new QueryOperator { Name = "=", Label = "Equals", ApplicableTypes = new[] { QueryFieldType.Number } },
                new QueryOperator { Name = ">", Label = "Greater Than", ApplicableTypes = new[] { QueryFieldType.Number } },
                new QueryOperator { Name = "<", Label = "Less Than", ApplicableTypes = new[] { QueryFieldType.Number } },
                new QueryOperator { Name = "between", Label = "Between", ApplicableTypes = new[] { QueryFieldType.Number } },
                new QueryOperator { Name = "isTopTier", Label = "Is Top Tier (>90)", ApplicableTypes = new[] { QueryFieldType.Number } }
            }
        }
    };
}
```

### Example 5: Validation and Error Handling

Implementing validation for query conditions:

```razor
<QueryBuilder Fields="@_fields" 
             @bind-Value="_query" 
             ShowValidationMessage="true"
             OnValidationStateChanged="HandleValidationChanged" />

<div class="mt-3">
    <Button Color="Color.Primary" 
            IsDisabled="@(!_isQueryValid)" 
            OnClick="ApplyQuery">Apply Filter</Button>
</div>

@code {
    private QueryGroup _query = new();
    private bool _isQueryValid = true;
    private List<QueryField> _fields = new()
    {
        new QueryField 
        { 
            Name = "Username", 
            Label = "Username", 
            Type = QueryFieldType.String,
            Required = true,
            Validators = new List<QueryFieldValidator>
            {
                new QueryFieldValidator
                {
                    Operator = "contains",
                    Validate = (value) => !string.IsNullOrEmpty(value?.ToString()),
                    Message = "Value cannot be empty"
                },
                new QueryFieldValidator
                {
                    Operator = "startsWith",
                    Validate = (value) => !string.IsNullOrEmpty(value?.ToString()) && value.ToString().Length >= 2,
                    Message = "Prefix must be at least 2 characters"
                }
            }
        },
        new QueryField 
        { 
            Name = "Age", 
            Label = "Age", 
            Type = QueryFieldType.Number,
            Validators = new List<QueryFieldValidator>
            {
                new QueryFieldValidator
                {
                    Operator = ">",
                    Validate = (value) => int.TryParse(value?.ToString(), out int age) && age > 0,
                    Message = "Age must be a positive number"
                },
                new QueryFieldValidator
                {
                    Operator = "between",
                    Validate = (value) => {
                        if (value is string[] values && values.Length == 2)
                        {
                            return int.TryParse(values[0], out int min) && 
                                   int.TryParse(values[1], out int max) && 
                                   min < max;
                        }
                        return false;
                    },
                    Message = "Min value must be less than max value"
                }
            }
        }
    };

    private void HandleValidationChanged(bool isValid)
    {
        _isQueryValid = isValid;
    }

    private void ApplyQuery()
    {
        // Apply the validated query
    }
}
```

### Example 6: Custom Value Editors

Using custom editors for specific field types:

```razor
<QueryBuilder Fields="@_fields" 
             @bind-Value="_query"
             RuleTemplate="@CustomRuleTemplate" />

@code {
    private QueryGroup _query = new();
    private List<QueryField> _fields = new()
    {
        new QueryField { Name = "Name", Label = "Name", Type = QueryFieldType.String },
        new QueryField { Name = "Department", Label = "Department", Type = QueryFieldType.Custom, CustomType = "department" },
        new QueryField { Name = "Status", Label = "Status", Type = QueryFieldType.Custom, CustomType = "status" }
    };

    private RenderFragment<QueryRule> CustomRuleTemplate => rule => @<div class="d-flex align-items-center">
        <Select TValue="string" 
                Items="GetFieldOptions()" 
                @bind-Value="rule.Field" 
                ShowLabel="false" />

        <Select TValue="string" 
                Items="GetOperatorOptions(rule.Field)" 
                @bind-Value="rule.Operator" 
                ShowLabel="false" 
                class="mx-2" />

        @if (GetFieldType(rule.Field) == "department")
        {
            <Select TValue="string" 
                    Items="@_departments" 
                    @bind-Value="rule.Value" 
                    ShowLabel="false" />
        }
        else if (GetFieldType(rule.Field) == "status")
        {
            <div class="btn-group">
                @foreach (var status in _statuses)
                {
                    <Button Color="rule.Value == status ? Color.Primary : Color.Secondary"
                            OnClick="() => rule.Value = status">
                        @status
                    </Button>
                }
            </div>
        }
        else
        {
            <Input TValue="string" @bind-Value="rule.Value" ShowLabel="false" />
        }

        <Button Color="Color.Danger" 
                Icon="fa-solid fa-times" 
                OnClick="() => RemoveRule(rule)" 
                class="ms-2" />
    </div>;

    private List<string> _departments = new() { "Engineering", "Marketing", "Sales", "Support", "HR" };
    private List<string> _statuses = new() { "Active", "Pending", "Suspended", "Closed" };

    private string GetFieldType(string fieldName)
    {
        var field = _fields.FirstOrDefault(f => f.Name == fieldName);
        return field?.CustomType ?? "";
    }

    private List<SelectedItem> GetFieldOptions()
    {
        return _fields.Select(f => new SelectedItem { Value = f.Name, Text = f.Label }).ToList();
    }

    private List<SelectedItem> GetOperatorOptions(string fieldName)
    {
        // Return appropriate operators based on field type
        return new List<SelectedItem>
        {
            new SelectedItem { Value = "=", Text = "Equals" },
            new SelectedItem { Value = "!=", Text = "Not Equals" }
        };
    }

    private void RemoveRule(QueryRule rule)
    {
        // Remove the rule from its parent group
    }
}
```

### Example 7: Nested Condition Groups

Building complex queries with nested condition groups:

```razor
<div class="card">
    <div class="card-header">Advanced Search</div>
    <div class="card-body">
        <QueryBuilder Fields="@_fields" 
                     @bind-Value="_query" 
                     MaxDepth="4"
                     ShowPreview="true" />
    </div>
    <div class="card-footer d-flex justify-content-between">
        <Button Color="Color.Secondary" OnClick="ResetQuery">Reset</Button>
        <Button Color="Color.Primary" OnClick="SaveQuery">Save Query</Button>
    </div>
</div>

@if (_savedQueries.Any())
{
    <div class="mt-3">
        <h5>Saved Queries</h5>
        <div class="list-group">
            @foreach (var (name, query) in _savedQueries)
            {
                <button class="list-group-item list-group-item-action d-flex justify-content-between align-items-center"
                        @onclick="() => LoadQuery(query)">
                    @name
                    <Badge Color="Color.Info">@GetQueryRuleCount(query) conditions</Badge>
                </button>
            }
        </div>
    </div>
}

@code {
    private QueryGroup _query = new();
    private Dictionary<string, QueryGroup> _savedQueries = new();
    private List<QueryField> _fields = new()
    {
        new QueryField { Name = "FirstName", Label = "First Name", Type = QueryFieldType.String },
        new QueryField { Name = "LastName", Label = "Last Name", Type = QueryFieldType.String },
        new QueryField { Name = "Email", Label = "Email", Type = QueryFieldType.String },
        new QueryField { Name = "Age", Label = "Age", Type = QueryFieldType.Number },
        new QueryField { Name = "JoinDate", Label = "Join Date", Type = QueryFieldType.Date },
        new QueryField { Name = "LastLogin", Label = "Last Login", Type = QueryFieldType.DateTime },
        new QueryField { Name = "IsActive", Label = "Is Active", Type = QueryFieldType.Boolean },
        new QueryField { Name = "Subscription", Label = "Subscription", Type = QueryFieldType.String },
        new QueryField { Name = "OrderCount", Label = "Order Count", Type = QueryFieldType.Number }
    };

    private void ResetQuery()
    {
        _query = new QueryGroup();
    }

    private async Task SaveQuery()
    {
        // In a real app, you would show a dialog to name the query
        var name = $"Query {_savedQueries.Count + 1}";
        
        // Clone the query to avoid reference issues
        var serialized = System.Text.Json.JsonSerializer.Serialize(_query);
        var clonedQuery = System.Text.Json.JsonSerializer.Deserialize<QueryGroup>(serialized);
        
        _savedQueries[name] = clonedQuery;
    }

    private void LoadQuery(QueryGroup query)
    {
        // Clone the saved query to avoid reference issues
        var serialized = System.Text.Json.JsonSerializer.Serialize(query);
        _query = System.Text.Json.JsonSerializer.Deserialize<QueryGroup>(serialized);
    }

    private int GetQueryRuleCount(QueryGroup query)
    {
        int count = query.Rules?.Count ?? 0;
        if (query.Groups != null)
        {
            foreach (var group in query.Groups)
            {
                count += GetQueryRuleCount(group);
            }
        }
        return count;
    }
}
```

## CSS Customization

The QueryBuilder component uses the following CSS classes that can be customized:

- `.query-builder`: The main container for the query builder
- `.query-group`: Container for a group of conditions
- `.query-group-header`: Header of a condition group containing the logical operator
- `.query-group-body`: Body of a condition group containing rules and nested groups
- `.query-rule`: Container for an individual condition/rule
- `.query-actions`: Container for action buttons
- `.query-preview`: Container for the query preview

You can override these classes in your application's CSS to customize the appearance of the QueryBuilder component.

## JavaScript Interop

The QueryBuilder component primarily operates on the client side using Blazor's component model. It may use JavaScript interop for certain advanced features like drag-and-drop reordering of conditions or custom value editors.

## Accessibility Considerations

When using the QueryBuilder component, consider the following accessibility best practices:

1. Ensure all form controls have appropriate labels
2. Maintain sufficient color contrast for all UI elements
3. Provide clear error messages for validation issues
4. Ensure keyboard navigation works properly throughout the component
5. Consider adding ARIA attributes for complex interactive elements

## Browser Compatibility

The QueryBuilder component is compatible with all modern browsers that support Blazor WebAssembly or Blazor Server. There are no specific browser compatibility issues to be aware of.

## Integration with Other Components

The QueryBuilder component works well with:

- **Table/DataGrid Components**: For filtering data based on constructed queries
- **Form Components**: For building advanced search forms
- **Button Components**: For triggering query execution or saving queries
- **Modal/Dialog Components**: For managing saved queries or custom value editors

## Best Practices

1. Limit the number of available fields to prevent overwhelming users
2. Provide clear labels and descriptions for fields and operators
3. Consider adding a query preview to help users understand their constructed query
4. Implement validation to prevent invalid queries
5. Allow saving and loading queries for complex use cases
6. Limit the maximum nesting depth to maintain usability