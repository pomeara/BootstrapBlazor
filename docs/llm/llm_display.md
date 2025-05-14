# Display Component

## Overview
The Display component in BootstrapBlazor provides a way to present read-only data in a formatted manner. It's designed to show values without allowing user interaction or editing, making it ideal for detail views, read-only forms, and data presentation scenarios. The component supports various data types, formatting options, and can be integrated with other form components to create mixed read-only and editable interfaces.

## Features
- **Multiple Data Type Support**: Handles various data types including strings, numbers, dates, enumerations, and collections
- **Value Formatting**: Customizable formatting for different data types
- **Label Integration**: Optional label display with tooltip support
- **Lookup Capability**: Value translation using lookup dictionaries
- **Tooltip Support**: Optional tooltips for displaying full content when text is truncated
- **Form Integration**: Seamless integration with form components
- **Enumeration Display**: Automatic display of enumeration display names
- **Collection Rendering**: Comma-separated display of collection values
- **Null Value Handling**: Graceful display of null or empty values
- **Responsive Design**: Adapts to different screen sizes
- **Accessibility Support**: Screen reader compatible

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `Value` | `TValue?` | `null` | The value to display. Can be of any type specified by the generic parameter TValue. |
| `ValueChanged` | `EventCallback<TValue?>` | - | Callback when the value changes (for two-way binding). |
| `ValueExpression` | `Expression<Func<TValue?>>` | `null` | Expression that identifies the bound value (for form integration). |
| `ShowLabel` | `bool?` | `null` | Whether to show a label. If null, inherits from parent form components. |
| `ShowLabelTooltip` | `bool?` | `null` | Whether to show a tooltip on the label. If null, inherits from parent form components. |
| `DisplayText` | `string?` | `null` | Text to display as the label. If null and ValueExpression is provided, uses the display name from the bound property. |
| `ShowTooltip` | `bool` | `false` | Whether to show a tooltip with the full value when hovering over the displayed value. |
| `FormatString` | `string?` | `null` | Format string to apply to the value (e.g., "yyyy-MM-dd" for dates). |
| `Formatter` | `Func<TValue?, string?>?` | `null` | Custom function to format the value as a string. |
| `FormatterAsync` | `Func<TValue?, Task<string?>>?` | `null` | Asynchronous custom function to format the value as a string. |
| `Lookup` | `IEnumerable<SelectedItem>?` | `null` | Collection of items to use for lookup-based value translation. |
| `LookupService` | `ILookupService?` | `null` | Service to use for lookup-based value translation. |
| `LookupServiceKey` | `string?` | `null` | Key to use with the LookupService. |
| `LookupServiceData` | `object?` | `null` | Additional data to pass to the LookupService. |
| `LookupStringComparison` | `StringComparison` | `Ordinal` | String comparison mode to use for lookups. |
| `TypeResolver` | `Func<object?, Type?, Type?, Type?>?` | `null` | Function to resolve the type of complex objects for proper display. |

## Events

The Display component doesn't have specific events, as it's primarily a read-only component. However, it supports the standard component lifecycle events.

## Usage Examples

### Example 1: Basic Display

```razor
<Display TValue="string" Value="Hello, world!" />
```

This example shows a basic display of a string value without any formatting or labels.

### Example 2: Display with Label

```razor
<Display TValue="string" 
         Value="John Doe" 
         ShowLabel="true" 
         DisplayText="Customer Name" />
```

This example shows a display with a label, which is useful in forms or detail views.

### Example 3: Date Formatting

```razor
<Display TValue="DateTime" 
         Value="@DateTime.Now" 
         FormatString="yyyy-MM-dd HH:mm:ss" 
         ShowLabel="true" 
         DisplayText="Current Date and Time" />
```

This example demonstrates how to format a date value using a format string.

### Example 4: Custom Formatting

```razor
@code {
    private decimal price = 1234.56m;
    
    private string FormatPrice(decimal? value)
    {
        return value.HasValue ? $"${value:N2}" : "";
    }
}

<Display TValue="decimal" 
         Value="@price" 
         Formatter="@FormatPrice" 
         ShowLabel="true" 
         DisplayText="Product Price" />
```

This example shows how to use a custom formatter function to display a price with currency symbol.

### Example 5: Enumeration Display

```razor
@code {
    private enum Status
    {
        [Display(Name = "Pending Approval")]
        PendingApproval,
        
        [Display(Name = "In Progress")]
        InProgress,
        
        [Display(Name = "Completed")]
        Completed,
        
        [Display(Name = "Cancelled")]
        Cancelled
    }
    
    private Status currentStatus = Status.InProgress;
}

<Display TValue="Status" 
         Value="@currentStatus" 
         ShowLabel="true" 
         DisplayText="Order Status" />
```

This example demonstrates how the Display component automatically shows the display name of enumeration values.

### Example 6: Lookup-based Display

```razor
@code {
    private string countryCode = "US";
    
    private List<SelectedItem> countries = new List<SelectedItem>
    {
        new SelectedItem("US", "United States"),
        new SelectedItem("CA", "Canada"),
        new SelectedItem("UK", "United Kingdom"),
        new SelectedItem("AU", "Australia"),
        new SelectedItem("JP", "Japan")
    };
}

<Display TValue="string" 
         Value="@countryCode" 
         Lookup="@countries" 
         ShowLabel="true" 
         DisplayText="Country" />
```

This example shows how to use the Lookup property to translate codes or IDs into human-readable values.

### Example 7: Collection Display

```razor
@code {
    private List<string> tags = new List<string> { "Blazor", "WebAssembly", "UI Components", ".NET" };
}

<Display TValue="List<string>" 
         Value="@tags" 
         ShowLabel="true" 
         DisplayText="Tags" 
         ShowTooltip="true" />
```

This example demonstrates how the Display component can show collections as comma-separated values with an optional tooltip.

## Customization Notes

### CSS Variables

The Display component uses the following CSS classes that can be customized:

```css
.form-control.is-display {
    /* Styles for the display container */
    background-color: var(--bs-form-control-disabled-bg, #e9ecef);
    border: var(--bs-border-width) solid var(--bs-border-color);
    padding: 0.375rem 0.75rem;
    border-radius: var(--bs-border-radius);
    overflow: hidden;
    text-overflow: ellipsis;
    white-space: nowrap;
}

.is-display {
    /* General styles for display elements */
    cursor: default;
    user-select: text;
}
```

### Integration with Other Components

The Display component works well with:

1. **ValidateForm**: For creating forms with read-only fields
2. **EditorForm**: For mixed read-only and editable forms
3. **BootstrapInputGroup**: For creating input groups with labels and read-only values
4. **Tooltip**: For showing full content on hover
5. **Table**: For displaying read-only data in table cells

### Best Practices

1. **Use with Forms**: When creating detail views or partially editable forms, use Display components for read-only fields and input components for editable fields.

2. **Formatting Complex Data**: For complex data types, provide custom formatters to ensure proper display.

3. **Tooltips for Long Content**: Enable the ShowTooltip property when displaying potentially long content that might be truncated.

4. **Lookup for Code Translation**: Use the Lookup property or LookupService for translating codes, IDs, or other non-human-readable values into user-friendly text.

5. **Accessibility**: Ensure that labels are provided for screen readers, especially when ShowLabel is false.

6. **Responsive Design**: Consider how the displayed content will appear on different screen sizes, and use appropriate CSS for responsive behavior.