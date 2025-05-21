# HtmlTag Component

## Overview
The HtmlTag component in BootstrapBlazor provides a flexible way to render arbitrary HTML elements with dynamic attributes and content. It serves as a lightweight wrapper that allows developers to create any HTML element while leveraging Blazor's component model, enabling easy binding of attributes, styles, and event handlers. This component is particularly useful for creating custom UI elements, implementing specialized rendering requirements, or integrating with third-party libraries that expect specific HTML structures.

## Features
- **Dynamic Element Creation**: Render any HTML element type dynamically
- **Attribute Binding**: Easily bind attributes to HTML elements
- **Style Management**: Apply and manipulate CSS styles programmatically
- **Event Handling**: Attach event handlers to HTML elements
- **Class Management**: Add, remove, and toggle CSS classes
- **Content Projection**: Insert child content or components inside the HTML element
- **Conditional Rendering**: Show or hide elements based on conditions
- **Data Attribute Support**: Add custom data attributes for JavaScript interoperability
- **ARIA Support**: Apply accessibility attributes for better screen reader compatibility

## Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `TagName` | string | "div" | The HTML element tag name to render |
| `Attributes` | Dictionary<string, object> | new() | Collection of HTML attributes to apply to the element |
| `Id` | string | null | The ID attribute of the HTML element |
| `Class` | string | null | CSS class names to apply to the element |
| `Style` | string | null | Inline CSS styles to apply to the element |
| `Role` | string | null | ARIA role attribute for accessibility |
| `Title` | string | null | Title attribute for tooltips |
| `TabIndex` | int? | null | Tab index for keyboard navigation |
| `Hidden` | bool | false | Whether the element should be hidden |
| `DataAttributes` | Dictionary<string, object> | new() | Collection of data-* attributes |
| `AriaAttributes` | Dictionary<string, object> | new() | Collection of aria-* attributes |
| `ChildContent` | RenderFragment | null | Content to be rendered inside the HTML element |

## Events

| Event | Description |
|-------|-------------|
| `OnClick` | Triggered when the element is clicked |
| `OnDoubleClick` | Triggered when the element is double-clicked |
| `OnMouseOver` | Triggered when the mouse pointer moves over the element |
| `OnMouseOut` | Triggered when the mouse pointer moves out of the element |
| `OnFocus` | Triggered when the element receives focus |
| `OnBlur` | Triggered when the element loses focus |
| `OnKeyDown` | Triggered when a key is pressed down while the element has focus |
| `OnKeyUp` | Triggered when a key is released while the element has focus |
| `OnKeyPress` | Triggered when a key is pressed and released while the element has focus |

## Usage Examples

### Example 1: Basic Usage with Different HTML Elements
```razor
<HtmlTag TagName="h1" Class="display-4">This is a heading</HtmlTag>

<HtmlTag TagName="p" Class="lead">
    This is a paragraph with a <HtmlTag TagName="strong">bold</HtmlTag> text.
</HtmlTag>

<HtmlTag TagName="button" Class="btn btn-primary" OnClick="HandleClick">
    Click Me
</HtmlTag>

<HtmlTag TagName="hr" Class="my-4" />

<HtmlTag TagName="ul" Class="list-group">
    <HtmlTag TagName="li" Class="list-group-item">Item 1</HtmlTag>
    <HtmlTag TagName="li" Class="list-group-item">Item 2</HtmlTag>
    <HtmlTag TagName="li" Class="list-group-item">Item 3</HtmlTag>
</HtmlTag>

@code {
    private void HandleClick()
    {
        Console.WriteLine("Button clicked!");
    }
}
```

### Example 2: Dynamic Attributes and Styles
```razor
@code {
    private Dictionary<string, object> customAttributes = new()
    {
        { "data-custom", "value" },
        { "aria-label", "Custom Element" }
    };
    
    private string elementStyle = "color: #007bff; padding: 10px; border: 1px solid #dee2e6; border-radius: 4px;";
    private string elementClass = "custom-element";
    private bool isDisabled = false;
    
    private void ToggleDisabled()
    {
        isDisabled = !isDisabled;
        if (isDisabled)
        {
            customAttributes["disabled"] = "disabled";
            elementClass = "custom-element disabled";
        }
        else
        {
            customAttributes.Remove("disabled");
            elementClass = "custom-element";
        }
    }
}

<HtmlTag TagName="div" Class="mb-3">
    <HtmlTag TagName="button"
             Class="btn btn-secondary mb-3"
             OnClick="ToggleDisabled">
        Toggle Disabled State
    </HtmlTag>
    
    <HtmlTag TagName="input"
             Class="@elementClass"
             Style="@elementStyle"
             Attributes="customAttributes"
             Id="customInput"
             OnFocus="() => Console.WriteLine('Input focused')" />
</HtmlTag>
```

### Example 3: Creating a Custom Card Component
```razor
@code {
    private string cardTitle = "Card Title";
    private string cardText = "Some quick example text to build on the card title and make up the bulk of the card's content.";
    private string imageUrl = "https://via.placeholder.com/300x200";
    private string imageAlt = "Card image cap";
    
    private Dictionary<string, object> cardAttributes = new()
    {
        { "data-card-id", "custom-card-1" }
    };
}

<HtmlTag TagName="div" Class="card" Style="width: 18rem;" Attributes="cardAttributes">
    <HtmlTag TagName="img" Class="card-img-top" Attributes='new Dictionary<string, object> { { "src", imageUrl }, { "alt", imageAlt } }' />
    
    <HtmlTag TagName="div" Class="card-body">
        <HtmlTag TagName="h5" Class="card-title">@cardTitle</HtmlTag>
        <HtmlTag TagName="p" Class="card-text">@cardText</HtmlTag>
        <HtmlTag TagName="a" Class="btn btn-primary" Attributes='new Dictionary<string, object> { { "href", "#" } }' OnClick="() => Console.WriteLine('Card button clicked')">
            Go somewhere
        </HtmlTag>
    </HtmlTag>
</HtmlTag>
```

### Example 4: Creating Accessible Form Elements
```razor
@code {
    private string username = "";
    private string password = "";
    private Dictionary<string, object> formAttributes = new()
    {
        { "novalidate", "novalidate" }
    };
    
    private Dictionary<string, object> usernameInputAttributes = new()
    {
        { "required", "required" },
        { "placeholder", "Enter username" },
        { "autocomplete", "username" }
    };
    
    private Dictionary<string, object> passwordInputAttributes = new()
    {
        { "required", "required" },
        { "placeholder", "Enter password" },
        { "autocomplete", "current-password" }
    };
    
    private void HandleSubmit()
    {
        Console.WriteLine($"Form submitted with username: {username}");
    }
}

<HtmlTag TagName="form" Attributes="formAttributes" OnSubmit="HandleSubmit">
    <HtmlTag TagName="div" Class="mb-3">
        <HtmlTag TagName="label" Class="form-label" Attributes='new Dictionary<string, object> { { "for", "username" } }'>
            Username
        </HtmlTag>
        
        <HtmlTag TagName="input"
                 Class="form-control"
                 Id="username"
                 Attributes="usernameInputAttributes"
                 AriaAttributes='new Dictionary<string, object> { { "aria-required", "true" } }'
                 @bind-value="username" />
    </HtmlTag>
    
    <HtmlTag TagName="div" Class="mb-3">
        <HtmlTag TagName="label" Class="form-label" Attributes='new Dictionary<string, object> { { "for", "password" } }'>
            Password
        </HtmlTag>
        
        <HtmlTag TagName="input"
                 Class="form-control"
                 Id="password"
                 Attributes='new Dictionary<string, object> { { "type", "password" } }'
                 AriaAttributes='new Dictionary<string, object> { { "aria-required", "true" } }'
                 @bind-value="password" />
    </HtmlTag>
    
    <HtmlTag TagName="button" Class="btn btn-primary" Attributes='new Dictionary<string, object> { { "type", "submit" } }'>
        Submit
    </HtmlTag>
</HtmlTag>
```

### Example 5: Creating a Custom Table with Dynamic Data
```razor
@code {
    private List<User> users = new List<User>
    {
        new User { Id = 1, Name = "John Doe", Email = "john@example.com", IsActive = true },
        new User { Id = 2, Name = "Jane Smith", Email = "jane@example.com", IsActive = true },
        new User { Id = 3, Name = "Bob Johnson", Email = "bob@example.com", IsActive = false }
    };
    
    private class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }
}

<HtmlTag TagName="table" Class="table table-striped" AriaAttributes='new Dictionary<string, object> { { "aria-label", "Users table" } }'>
    <HtmlTag TagName="thead">
        <HtmlTag TagName="tr">
            <HtmlTag TagName="th" Attributes='new Dictionary<string, object> { { "scope", "col" } }'>ID</HtmlTag>
            <HtmlTag TagName="th" Attributes='new Dictionary<string, object> { { "scope", "col" } }'>Name</HtmlTag>
            <HtmlTag TagName="th" Attributes='new Dictionary<string, object> { { "scope", "col" } }'>Email</HtmlTag>
            <HtmlTag TagName="th" Attributes='new Dictionary<string, object> { { "scope", "col" } }'>Status</HtmlTag>
            <HtmlTag TagName="th" Attributes='new Dictionary<string, object> { { "scope", "col" } }'>Actions</HtmlTag>
        </HtmlTag>
    </HtmlTag>
    
    <HtmlTag TagName="tbody">
        @foreach (var user in users)
        {
            <HtmlTag TagName="tr" DataAttributes='new Dictionary<string, object> { { "data-user-id", user.Id } }'>
                <HtmlTag TagName="td">@user.Id</HtmlTag>
                <HtmlTag TagName="td">@user.Name</HtmlTag>
                <HtmlTag TagName="td">@user.Email</HtmlTag>
                <HtmlTag TagName="td">
                    <HtmlTag TagName="span" Class="badge @(user.IsActive ? "bg-success" : "bg-danger")">
                        @(user.IsActive ? "Active" : "Inactive")
                    </HtmlTag>
                </HtmlTag>
                <HtmlTag TagName="td">
                    <HtmlTag TagName="button" Class="btn btn-sm btn-primary me-1" OnClick="() => Console.WriteLine($"Edit user {user.Id}")">Edit</HtmlTag>
                    <HtmlTag TagName="button" Class="btn btn-sm btn-danger" OnClick="() => Console.WriteLine($"Delete user {user.Id}")">Delete</HtmlTag>
                </HtmlTag>
            </HtmlTag>
        }
    </HtmlTag>
</HtmlTag>
```

### Example 6: Creating a Custom Navigation Menu
```razor
@code {
    private List<NavItem> navItems = new List<NavItem>
    {
        new NavItem { Text = "Home", Url = "/", IsActive = true },
        new NavItem { Text = "Products", Url = "/products" },
        new NavItem { Text = "Services", Url = "/services" },
        new NavItem { Text = "About", Url = "/about" },
        new NavItem { Text = "Contact", Url = "/contact" }
    };
    
    private class NavItem
    {
        public string Text { get; set; }
        public string Url { get; set; }
        public bool IsActive { get; set; }
    }
    
    private void SetActiveItem(NavItem item)
    {
        foreach (var navItem in navItems)
        {
            navItem.IsActive = navItem == item;
        }
    }
}

<HtmlTag TagName="nav" Class="navbar navbar-expand-lg navbar-light bg-light">
    <HtmlTag TagName="div" Class="container-fluid">
        <HtmlTag TagName="a" Class="navbar-brand" Attributes='new Dictionary<string, object> { { "href", "/" } }'>
            Brand Logo
        </HtmlTag>
        
        <HtmlTag TagName="button" Class="navbar-toggler" 
                 Attributes='new Dictionary<string, object> { 
                     { "type", "button" }, 
                     { "data-bs-toggle", "collapse" }, 
                     { "data-bs-target", "#navbarNav" } 
                 }'
                 AriaAttributes='new Dictionary<string, object> { 
                     { "aria-controls", "navbarNav" }, 
                     { "aria-expanded", "false" }, 
                     { "aria-label", "Toggle navigation" } 
                 }'>
            <HtmlTag TagName="span" Class="navbar-toggler-icon"></HtmlTag>
        </HtmlTag>
        
        <HtmlTag TagName="div" Class="collapse navbar-collapse" Id="navbarNav">
            <HtmlTag TagName="ul" Class="navbar-nav">
                @foreach (var item in navItems)
                {
                    <HtmlTag TagName="li" Class="nav-item">
                        <HtmlTag TagName="a" 
                                 Class="nav-link @(item.IsActive ? "active" : "")" 
                                 Attributes='new Dictionary<string, object> { { "href", item.Url } }'
                                 AriaAttributes='new Dictionary<string, object> { { "aria-current", item.IsActive ? "page" : null } }'
                                 OnClick="() => SetActiveItem(item)">
                            @item.Text
                        </HtmlTag>
                    </HtmlTag>
                }
            </HtmlTag>
        </HtmlTag>
    </HtmlTag>
</HtmlTag>
```

### Example 7: Creating a Custom Modal Dialog
```razor
@code {
    private bool isModalOpen = false;
    
    private void OpenModal()
    {
        isModalOpen = true;
    }
    
    private void CloseModal()
    {
        isModalOpen = false;
    }
}

<HtmlTag TagName="button" Class="btn btn-primary" OnClick="OpenModal">
    Open Modal
</HtmlTag>

@if (isModalOpen)
{
    <HtmlTag TagName="div" Class="modal-backdrop fade show"></HtmlTag>
    
    <HtmlTag TagName="div" 
             Class="modal fade show" 
             Style="display: block;" 
             TabIndex="-1" 
             Role="dialog"
             AriaAttributes='new Dictionary<string, object> { { "aria-modal", "true" } }'>
        <HtmlTag TagName="div" Class="modal-dialog">
            <HtmlTag TagName="div" Class="modal-content">
                <HtmlTag TagName="div" Class="modal-header">
                    <HtmlTag TagName="h5" Class="modal-title">Custom Modal Title</HtmlTag>
                    <HtmlTag TagName="button" 
                             Class="btn-close" 
                             Attributes='new Dictionary<string, object> { { "type", "button" } }'
                             AriaAttributes='new Dictionary<string, object> { { "aria-label", "Close" } }'
                             OnClick="CloseModal">
                    </HtmlTag>
                </HtmlTag>
                
                <HtmlTag TagName="div" Class="modal-body">
                    <HtmlTag TagName="p">This is a custom modal dialog created using the HtmlTag component.</HtmlTag>
                </HtmlTag>
                
                <HtmlTag TagName="div" Class="modal-footer">
                    <HtmlTag TagName="button" Class="btn btn-secondary" OnClick="CloseModal">
                        Close
                    </HtmlTag>
                    <HtmlTag TagName="button" Class="btn btn-primary" OnClick="() => { Console.WriteLine('Save changes'); CloseModal(); }">
                        Save changes
                    </HtmlTag>
                </HtmlTag>
            </HtmlTag>
        </HtmlTag>
    </HtmlTag>
}
```

## CSS Customization

The HtmlTag component doesn't have specific CSS variables or classes of its own since it's a wrapper for standard HTML elements. However, you can apply any CSS customization directly to the rendered elements using the `Class` and `Style` properties or through the `Attributes` dictionary.

```razor
<HtmlTag TagName="div" 
         Class="custom-container" 
         Style="--custom-bg-color: #f5f5f5; background-color: var(--custom-bg-color); padding: 20px;">
    Custom styled content
</HtmlTag>
```

You can also create CSS classes in your stylesheet and apply them to the HtmlTag component:

```css
.custom-element {
    border: 1px solid #dee2e6;
    border-radius: 4px;
    padding: 15px;
    margin-bottom: 1rem;
    transition: all 0.3s ease;
}

.custom-element:hover {
    box-shadow: 0 0.5rem 1rem rgba(0, 0, 0, 0.15);
    transform: translateY(-2px);
}
```

```razor
<HtmlTag TagName="div" Class="custom-element">
    This element will use the custom styling
</HtmlTag>
```

## JavaScript Interop

The HtmlTag component can be used with JavaScript interop by adding event handlers and data attributes that JavaScript code can interact with:

```razor
@inject IJSRuntime JSRuntime

@code {
    private ElementReference elementRef;
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await JSRuntime.InvokeVoidAsync("initializeCustomElement", elementRef);
        }
    }
}

<HtmlTag TagName="div" 
         @ref="elementRef"
         Id="customElement"
         DataAttributes='new Dictionary<string, object> { { "data-custom-config", "{ \"animation\": true, \"speed\": 300 }" } }'>
    Element for JavaScript interop
</HtmlTag>
```

## Accessibility

The HtmlTag component supports accessibility through ARIA attributes and roles. You can set these using the `AriaAttributes` property and the `Role` property:

```razor
<HtmlTag TagName="div"
         Role="region"
         AriaAttributes='new Dictionary<string, object> {
             { "aria-labelledby", "region-title" },
             { "aria-live", "polite" }
         }'>
    <HtmlTag TagName="h2" Id="region-title">Accessible Region</HtmlTag>
    <HtmlTag TagName="p">This region is properly labeled for screen readers.</HtmlTag>
</HtmlTag>
```

## Browser Compatibility

The HtmlTag component is compatible with all modern browsers as it renders standard HTML elements. There are no specific browser compatibility concerns beyond those that would apply to the HTML elements being rendered.

## Integration with Other Components

The HtmlTag component can be used alongside other BootstrapBlazor components to create complex UI structures:

```razor
<HtmlTag TagName="div" Class="custom-container">
    <Alert Color="Color.Info" ShowDismiss="true">
        This is an alert inside a custom container
    </Alert>
    
    <HtmlTag TagName="div" Class="row mt-3">
        <HtmlTag TagName="div" Class="col-md-6">
            <Card>
                <CardTitle>Card in Column 1</CardTitle>
                <CardBody>
                    <p>This is a BootstrapBlazor Card component inside an HtmlTag column.</p>
                </CardBody>
            </Card>
        </HtmlTag>
        
        <HtmlTag TagName="div" Class="col-md-6">
            <Card>
                <CardTitle>Card in Column 2</CardTitle>
                <CardBody>
                    <p>Another Card component in a different column.</p>
                </CardBody>
            </Card>
        </HtmlTag>
    </HtmlTag>
</HtmlTag>
```

The HtmlTag component is particularly useful when you need to create custom layouts or structures that aren't directly provided by the BootstrapBlazor component library.