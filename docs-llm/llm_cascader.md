# Cascader Component Documentation

## Overview
The Cascader component in BootstrapBlazor provides a multi-level cascading selection interface for hierarchical data. It allows users to navigate through nested options, making it ideal for selecting items from categories with subcategories, such as location selectors (country/state/city), product categories, or organizational structures. The component displays options in a progressive manner, where each selection reveals the next level of options, creating an intuitive navigation experience for complex hierarchical data.

## Features
- **Multi-level Hierarchical Selection**: Navigate through nested options with progressive disclosure
- **Dynamic Loading Support**: Load child options on demand to improve performance with large datasets
- **Single and Multiple Selection Modes**: Select either one item or multiple items across different levels
- **Custom Option Rendering**: Customize the appearance of options with templates
- **Search Functionality**: Filter options with built-in search capability
- **Disabled Options**: Disable specific options or entire branches
- **Default Values**: Pre-select values when initializing the component
- **Placeholder Support**: Display placeholder text when no value is selected
- **Clear Button**: Option to clear selected values
- **Form Integration**: Seamless integration with form validation
- **Keyboard Navigation**: Navigate options using keyboard shortcuts
- **Responsive Design**: Adapts to different screen sizes

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| Value | TValue | default(TValue) | Gets or sets the selected value |
| ValueChanged | EventCallback<TValue> | - | Callback when the selected value changes |
| ValueExpression | Expression<Func<TValue>> | - | Expression for the bound value |
| Options | IEnumerable<CascaderOption<TValue>> | null | The hierarchical options to display |
| Placeholder | string | "Please select" | Placeholder text when no value is selected |
| ShowClear | bool | true | Whether to show the clear button |
| IsDisabled | bool | false | Whether the component is disabled |
| IsReadOnly | bool | false | Whether the component is read-only |
| IsMultiple | bool | false | Whether multiple selection is enabled |
| ExpandTrigger | ExpandTrigger | Click | How to trigger the expansion of child options (Click, Hover) |
| MaxTagCount | int? | null | Maximum number of tags to show in multiple selection mode |
| DisplayRender | Func<IEnumerable<string>, string> | null | Custom function to format the display text |
| LoadData | Func<CascaderOption<TValue>, Task> | null | Function to dynamically load child options |
| SearchPlaceholder | string | "Search" | Placeholder text for the search input |
| ShowSearch | bool | false | Whether to show search functionality |
| Size | Size | Medium | Size of the component (Small, Medium, Large) |
| ClassName | string | "" | Additional CSS class for the component |

## Events

| Event | Description |
| --- | --- |
| OnValueChanged | Triggered when the selected value changes |
| OnVisibleChanged | Triggered when the dropdown visibility changes |
| OnExpand | Triggered when an option is expanded to show its children |
| OnClear | Triggered when the selection is cleared |
| OnSearch | Triggered when the search text changes |

## Usage Examples

### Example 1: Basic Cascader

```razor
<Cascader TValue="string"
          @bind-Value="@selectedValue"
          Options="@options"
          Placeholder="Select a location" />

<div class="mt-3">
    Selected value: @selectedValue
</div>

@code {
    private string selectedValue;
    private List<CascaderOption<string>> options = new List<CascaderOption<string>>
    {
        new CascaderOption<string>
        {
            Value = "north-america",
            Label = "North America",
            Children = new List<CascaderOption<string>>
            {
                new CascaderOption<string>
                {
                    Value = "usa",
                    Label = "United States",
                    Children = new List<CascaderOption<string>>
                    {
                        new CascaderOption<string> { Value = "new-york", Label = "New York" },
                        new CascaderOption<string> { Value = "california", Label = "California" },
                        new CascaderOption<string> { Value = "texas", Label = "Texas" }
                    }
                },
                new CascaderOption<string>
                {
                    Value = "canada",
                    Label = "Canada",
                    Children = new List<CascaderOption<string>>
                    {
                        new CascaderOption<string> { Value = "ontario", Label = "Ontario" },
                        new CascaderOption<string> { Value = "quebec", Label = "Quebec" },
                        new CascaderOption<string> { Value = "british-columbia", Label = "British Columbia" }
                    }
                }
            }
        },
        new CascaderOption<string>
        {
            Value = "europe",
            Label = "Europe",
            Children = new List<CascaderOption<string>>
            {
                new CascaderOption<string>
                {
                    Value = "uk",
                    Label = "United Kingdom",
                    Children = new List<CascaderOption<string>>
                    {
                        new CascaderOption<string> { Value = "london", Label = "London" },
                        new CascaderOption<string> { Value = "manchester", Label = "Manchester" },
                        new CascaderOption<string> { Value = "liverpool", Label = "Liverpool" }
                    }
                },
                new CascaderOption<string>
                {
                    Value = "france",
                    Label = "France",
                    Children = new List<CascaderOption<string>>
                    {
                        new CascaderOption<string> { Value = "paris", Label = "Paris" },
                        new CascaderOption<string> { Value = "lyon", Label = "Lyon" },
                        new CascaderOption<string> { Value = "marseille", Label = "Marseille" }
                    }
                }
            }
        }
    };
}
```

### Example 2: Multiple Selection

```razor
<Cascader TValue="List<string>"
          @bind-Value="@selectedValues"
          Options="@options"
          IsMultiple="true"
          MaxTagCount="3"
          Placeholder="Select multiple locations" />

<div class="mt-3">
    <h5>Selected values:</h5>
    @if (selectedValues != null && selectedValues.Any())
    {
        <ul>
            @foreach (var value in selectedValues)
            {
                <li>@value</li>
            }
        </ul>
    }
    else
    {
        <p>No locations selected</p>
    }
</div>

@code {
    private List<string> selectedValues = new List<string>();
    private List<CascaderOption<string>> options = new List<CascaderOption<string>>
    {
        // Same options as Example 1
    };
}
```

### Example 3: Dynamic Loading

```razor
<Cascader TValue="string"
          @bind-Value="@selectedValue"
          Options="@options"
          LoadData="@LoadChildOptions"
          Placeholder="Select a category" />

<div class="mt-3">
    Selected category: @selectedValue
</div>

@code {
    private string selectedValue;
    private List<CascaderOption<string>> options = new List<CascaderOption<string>>
    {
        new CascaderOption<string> { Value = "electronics", Label = "Electronics", IsLeaf = false },
        new CascaderOption<string> { Value = "clothing", Label = "Clothing", IsLeaf = false },
        new CascaderOption<string> { Value = "books", Label = "Books", IsLeaf = false }
    };

    private async Task LoadChildOptions(CascaderOption<string> option)
    {
        // Simulate API call delay
        await Task.Delay(500);

        if (option.Value == "electronics")
        {
            option.Children = new List<CascaderOption<string>>
            {
                new CascaderOption<string> { Value = "smartphones", Label = "Smartphones", IsLeaf = false },
                new CascaderOption<string> { Value = "laptops", Label = "Laptops", IsLeaf = false },
                new CascaderOption<string> { Value = "accessories", Label = "Accessories", IsLeaf = true }
            };
        }
        else if (option.Value == "smartphones")
        {
            option.Children = new List<CascaderOption<string>>
            {
                new CascaderOption<string> { Value = "apple", Label = "Apple", IsLeaf = true },
                new CascaderOption<string> { Value = "samsung", Label = "Samsung", IsLeaf = true },
                new CascaderOption<string> { Value = "xiaomi", Label = "Xiaomi", IsLeaf = true }
            };
        }
        else if (option.Value == "laptops")
        {
            option.Children = new List<CascaderOption<string>>
            {
                new CascaderOption<string> { Value = "dell", Label = "Dell", IsLeaf = true },
                new CascaderOption<string> { Value = "hp", Label = "HP", IsLeaf = true },
                new CascaderOption<string> { Value = "lenovo", Label = "Lenovo", IsLeaf = true }
            };
        }
        else if (option.Value == "clothing")
        {
            option.Children = new List<CascaderOption<string>>
            {
                new CascaderOption<string> { Value = "mens", Label = "Men's", IsLeaf = true },
                new CascaderOption<string> { Value = "womens", Label = "Women's", IsLeaf = true },
                new CascaderOption<string> { Value = "kids", Label = "Kids", IsLeaf = true }
            };
        }
        else if (option.Value == "books")
        {
            option.Children = new List<CascaderOption<string>>
            {
                new CascaderOption<string> { Value = "fiction", Label = "Fiction", IsLeaf = true },
                new CascaderOption<string> { Value = "nonfiction", Label = "Non-Fiction", IsLeaf = true },
                new CascaderOption<string> { Value = "education", Label = "Education", IsLeaf = true }
            };
        }
    }
}
```

### Example 4: Custom Display Rendering

```razor
<Cascader TValue="string"
          @bind-Value="@selectedValue"
          Options="@options"
          DisplayRender="@FormatSelection"
          Placeholder="Select a location" />

<div class="mt-3">
    Selected value: @selectedValue
</div>

@code {
    private string selectedValue;
    private List<CascaderOption<string>> options = new List<CascaderOption<string>>
    {
        // Same options as Example 1
    };

    private string FormatSelection(IEnumerable<string> labels)
    {
        return string.Join(" > ", labels);
    }
}
```

### Example 5: Search Functionality

```razor
<Cascader TValue="string"
          @bind-Value="@selectedValue"
          Options="@options"
          ShowSearch="true"
          SearchPlaceholder="Search locations"
          OnSearch="@HandleSearch"
          Placeholder="Select a location" />

<div class="mt-3">
    Selected value: @selectedValue
</div>

@code {
    private string selectedValue;
    private List<CascaderOption<string>> options = new List<CascaderOption<string>>
    {
        // Same options as Example 1
    };

    private void HandleSearch(string searchText)
    {
        Console.WriteLine($"Searching for: {searchText}");
        // Implement search logic if needed
    }
}
```

### Example 6: Disabled Options

```razor
<Cascader TValue="string"
          @bind-Value="@selectedValue"
          Options="@optionsWithDisabled"
          Placeholder="Select a location" />

<div class="mt-3">
    Selected value: @selectedValue
</div>

@code {
    private string selectedValue;
    private List<CascaderOption<string>> optionsWithDisabled = new List<CascaderOption<string>>
    {
        new CascaderOption<string>
        {
            Value = "north-america",
            Label = "North America",
            Children = new List<CascaderOption<string>>
            {
                new CascaderOption<string>
                {
                    Value = "usa",
                    Label = "United States",
                    Children = new List<CascaderOption<string>>
                    {
                        new CascaderOption<string> { Value = "new-york", Label = "New York" },
                        new CascaderOption<string> { Value = "california", Label = "California", IsDisabled = true },
                        new CascaderOption<string> { Value = "texas", Label = "Texas" }
                    }
                },
                new CascaderOption<string>
                {
                    Value = "canada",
                    Label = "Canada",
                    IsDisabled = true,
                    Children = new List<CascaderOption<string>>
                    {
                        new CascaderOption<string> { Value = "ontario", Label = "Ontario" },
                        new CascaderOption<string> { Value = "quebec", Label = "Quebec" },
                        new CascaderOption<string> { Value = "british-columbia", Label = "British Columbia" }
                    }
                }
            }
        },
        new CascaderOption<string>
        {
            Value = "europe",
            Label = "Europe",
            Children = new List<CascaderOption<string>>
            {
                // Same as Example 1
            }
        }
    };
}
```

### Example 7: Form Integration

```razor
<ValidateForm Model="@model" OnValidSubmit="@HandleValidSubmit">
    <div class="mb-3">
        <BootstrapInput @bind-Value="@model.Name" placeholder="Name" />
        <ValidationMessage For="@(() => model.Name)" />
    </div>
    
    <div class="mb-3">
        <label>Location</label>
        <Cascader TValue="string"
                  @bind-Value="@model.Location"
                  Options="@options"
                  Placeholder="Select a location" />
        <ValidationMessage For="@(() => model.Location)" />
    </div>
    
    <Button Type="ButtonType.Submit">Submit</Button>
</ValidateForm>

@code {
    private UserModel model = new UserModel
    {
        Name = "",
        Location = ""
    };
    
    private List<CascaderOption<string>> options = new List<CascaderOption<string>>
    {
        // Same options as Example 1
    };
    
    private void HandleValidSubmit()
    {
        // Process form submission
        Console.WriteLine($"Name: {model.Name}");
        Console.WriteLine($"Location: {model.Location}");
    }
    
    public class UserModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Location is required")]
        public string Location { get; set; }
    }
}
```

## Customization Notes

The Cascader component can be customized using the following CSS variables:

```css
:root {
    --bb-cascader-width: 220px;
    --bb-cascader-height: auto;
    --bb-cascader-border-radius: 4px;
    --bb-cascader-dropdown-width: 220px;
    --bb-cascader-dropdown-max-height: 300px;
    --bb-cascader-dropdown-bg: #fff;
    --bb-cascader-dropdown-border-color: rgba(0, 0, 0, 0.15);
    --bb-cascader-dropdown-shadow: 0 2px 8px rgba(0, 0, 0, 0.15);
    --bb-cascader-item-height: 32px;
    --bb-cascader-item-padding: 5px 12px;
    --bb-cascader-item-hover-bg: rgba(0, 123, 255, 0.1);
    --bb-cascader-item-active-bg: rgba(0, 123, 255, 0.2);
    --bb-cascader-item-active-color: var(--primary);
    --bb-cascader-item-disabled-color: #ccc;
    --bb-cascader-search-height: 32px;
    --bb-cascader-tag-bg: #f0f0f0;
    --bb-cascader-tag-color: #333;
    --bb-cascader-tag-border-radius: 2px;
    --bb-cascader-tag-margin: 2px;
    --bb-cascader-tag-padding: 0 4px;
}
```

Additionally, you can customize the appearance and behavior of the Cascader component by:

1. Using the `DisplayRender` property to customize how selected values are displayed
2. Using the `ExpandTrigger` property to change how child options are expanded
3. Using the `IsMultiple` property to enable multiple selection
4. Using the `MaxTagCount` property to limit the number of visible tags in multiple selection mode
5. Using the `ShowSearch` property to enable search functionality
6. Using the `Size` property to change the component size
7. Applying custom CSS classes to the component using the `ClassName` property