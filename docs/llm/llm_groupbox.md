# GroupBox Component

## Overview
The GroupBox component in BootstrapBlazor provides a visual container that groups related content with a titled border. It helps organize UI elements into logical sections, improving the overall layout and usability of forms and complex interfaces. The GroupBox component is particularly useful for creating structured layouts in data entry forms, settings panels, and configuration screens.

## Features
- **Titled Container**: Groups related content with a visible title
- **Collapsible Content**: Optionally allows content to be collapsed/expanded
- **Customizable Appearance**: Configurable border styles, colors, and title formatting
- **Header Actions**: Support for additional actions in the header area
- **Nested Structure**: Ability to nest GroupBox components for hierarchical organization
- **Responsive Design**: Adapts to different screen sizes
- **Accessibility Support**: Proper ARIA attributes for screen readers
- **Animation Effects**: Smooth transitions for collapse/expand operations

## Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `Title` | string | "" | The title text displayed in the header of the GroupBox |
| `TitleTemplate` | RenderFragment | null | Custom template for the GroupBox title |
| `HeaderTemplate` | RenderFragment | null | Custom template for the entire GroupBox header |
| `BodyTemplate` | RenderFragment | null | Custom template for the GroupBox body content |
| `FooterTemplate` | RenderFragment | null | Custom template for the GroupBox footer |
| `ShowFooter` | bool | false | Whether to display the footer section |
| `Collapsible` | bool | false | Whether the GroupBox can be collapsed/expanded |
| `Collapsed` | bool | false | Whether the GroupBox is initially collapsed |
| `CollapseMode` | CollapseMode | CollapseMode.Hide | The mode of collapse (Hide or Height) |
| `ShowBorder` | bool | true | Whether to show the border around the GroupBox |
| `ShowHeader` | bool | true | Whether to show the header section |
| `HeaderTextColor` | Color | Color.None | Color of the header text |
| `HeaderBackgroundColor` | Color | Color.None | Background color of the header |
| `BodyBackgroundColor` | Color | Color.None | Background color of the body |
| `IsCenter` | bool | false | Whether to center the title text |
| `IsBorderless` | bool | false | Whether to remove all borders |
| `IsOutline` | bool | false | Whether to use an outline style |
| `IsSolid` | bool | false | Whether to use a solid background style |
| `ChildContent` | RenderFragment | null | Content to be displayed inside the GroupBox |

## Events

| Event | Description |
|-------|-------------|
| `OnCollapsedChanged` | Triggered when the collapsed state changes |
| `OnCollapsing` | Triggered before the GroupBox begins collapsing |
| `OnCollapsed` | Triggered after the GroupBox has collapsed |
| `OnExpanding` | Triggered before the GroupBox begins expanding |
| `OnExpanded` | Triggered after the GroupBox has expanded |

## Usage Examples

### Example 1: Basic GroupBox
```razor
<GroupBox Title="User Information">
    <div class="row">
        <div class="col-md-6 mb-3">
            <label for="firstName" class="form-label">First Name</label>
            <input type="text" class="form-control" id="firstName" placeholder="Enter first name" />
        </div>
        <div class="col-md-6 mb-3">
            <label for="lastName" class="form-label">Last Name</label>
            <input type="text" class="form-control" id="lastName" placeholder="Enter last name" />
        </div>
    </div>
    <div class="mb-3">
        <label for="email" class="form-label">Email</label>
        <input type="email" class="form-control" id="email" placeholder="Enter email" />
    </div>
</GroupBox>
```

### Example 2: Collapsible GroupBox
```razor
<GroupBox Title="Advanced Settings" 
          Collapsible="true" 
          Collapsed="true"
          OnCollapsedChanged="@(collapsed => Console.WriteLine($"Collapsed state: {collapsed}"))">
    <div class="mb-3">
        <label for="cacheSize" class="form-label">Cache Size (MB)</label>
        <input type="number" class="form-control" id="cacheSize" value="100" />
    </div>
    <div class="mb-3">
        <label for="timeout" class="form-label">Connection Timeout (seconds)</label>
        <input type="number" class="form-control" id="timeout" value="30" />
    </div>
    <div class="form-check mb-3">
        <input class="form-check-input" type="checkbox" id="debugMode" />
        <label class="form-check-label" for="debugMode">Enable Debug Mode</label>
    </div>
</GroupBox>
```

### Example 3: Custom Header Template
```razor
<GroupBox>
    <HeaderTemplate>
        <div class="d-flex justify-content-between align-items-center">
            <h5 class="mb-0">Payment Information</h5>
            <Button Icon="fa-solid fa-question-circle" Color="Color.Link" Size="Size.Small" />
        </div>
    </HeaderTemplate>
    <BodyTemplate>
        <div class="mb-3">
            <label for="cardNumber" class="form-label">Card Number</label>
            <input type="text" class="form-control" id="cardNumber" placeholder="XXXX XXXX XXXX XXXX" />
        </div>
        <div class="row">
            <div class="col-md-6 mb-3">
                <label for="expiryDate" class="form-label">Expiry Date</label>
                <input type="text" class="form-control" id="expiryDate" placeholder="MM/YY" />
            </div>
            <div class="col-md-6 mb-3">
                <label for="cvv" class="form-label">CVV</label>
                <input type="text" class="form-control" id="cvv" placeholder="XXX" />
            </div>
        </div>
    </BodyTemplate>
</GroupBox>
```

### Example 4: Styled GroupBox with Footer
```razor
<GroupBox Title="Project Summary" 
          HeaderTextColor="Color.White" 
          HeaderBackgroundColor="Color.Primary"
          BodyBackgroundColor="Color.Light"
          ShowFooter="true">
    <div class="mb-3">
        <h5>Project Status</h5>
        <Progress Value="75" ShowLabel="true" />
    </div>
    <div class="mb-3">
        <h5>Team Members</h5>
        <ul class="list-group">
            <li class="list-group-item">John Doe (Project Manager)</li>
            <li class="list-group-item">Jane Smith (Developer)</li>
            <li class="list-group-item">Robert Johnson (Designer)</li>
        </ul>
    </div>
    <FooterTemplate>
        <div class="d-flex justify-content-end">
            <Button Text="View Details" Color="Color.Primary" />
        </div>
    </FooterTemplate>
</GroupBox>
```

### Example 5: Nested GroupBoxes
```razor
<GroupBox Title="System Configuration">
    <GroupBox Title="Network Settings" Collapsible="true" Class="mb-3">
        <div class="mb-3">
            <label for="ipAddress" class="form-label">IP Address</label>
            <input type="text" class="form-control" id="ipAddress" value="192.168.1.1" />
        </div>
        <div class="mb-3">
            <label for="subnet" class="form-label">Subnet Mask</label>
            <input type="text" class="form-control" id="subnet" value="255.255.255.0" />
        </div>
    </GroupBox>
    
    <GroupBox Title="Security Settings" Collapsible="true" Class="mb-3">
        <div class="form-check mb-3">
            <input class="form-check-input" type="checkbox" id="firewall" checked />
            <label class="form-check-label" for="firewall">Enable Firewall</label>
        </div>
        <div class="form-check mb-3">
            <input class="form-check-input" type="checkbox" id="encryption" checked />
            <label class="form-check-label" for="encryption">Enable Encryption</label>
        </div>
    </GroupBox>
    
    <GroupBox Title="User Access" Collapsible="true">
        <div class="form-check mb-3">
            <input class="form-check-input" type="checkbox" id="adminAccess" />
            <label class="form-check-label" for="adminAccess">Admin Access</label>
        </div>
        <div class="form-check mb-3">
            <input class="form-check-input" type="checkbox" id="guestAccess" checked />
            <label class="form-check-label" for="guestAccess">Guest Access</label>
        </div>
    </GroupBox>
</GroupBox>
```

### Example 6: Dynamic GroupBoxes
```razor
@code {
    private List<GroupBoxItem> groupBoxItems = new List<GroupBoxItem>
    {
        new GroupBoxItem { Title = "General Information", IsCollapsed = false },
        new GroupBoxItem { Title = "Contact Details", IsCollapsed = true },
        new GroupBoxItem { Title = "Preferences", IsCollapsed = true }
    };
    
    private class GroupBoxItem
    {
        public string Title { get; set; }
        public bool IsCollapsed { get; set; }
    }
    
    private void ToggleCollapse(int index)
    {
        groupBoxItems[index].IsCollapsed = !groupBoxItems[index].IsCollapsed;
    }
}

<div>
    @for (int i = 0; i < groupBoxItems.Count; i++)
    {
        var index = i;
        var item = groupBoxItems[i];
        
        <GroupBox Title="@item.Title" 
                  Collapsible="true" 
                  Collapsed="@item.IsCollapsed"
                  OnCollapsedChanged="@(collapsed => { item.IsCollapsed = collapsed; StateHasChanged(); })"
                  Class="mb-3">
            <div class="p-3">
                <p>Content for @item.Title</p>
                @if (index < groupBoxItems.Count - 1)
                {
                    <Button Text="Next Section" 
                            OnClick="@(() => ToggleCollapse(index + 1))" />
                }
            </div>
        </GroupBox>
    }
</div>
```

### Example 7: GroupBox with Tabs
```razor
<GroupBox Title="Product Information">
    <Tabs>
        <Tab Title="Details">
            <div class="mb-3">
                <label for="productName" class="form-label">Product Name</label>
                <input type="text" class="form-control" id="productName" />
            </div>
            <div class="mb-3">
                <label for="productDescription" class="form-label">Description</label>
                <textarea class="form-control" id="productDescription" rows="3"></textarea>
            </div>
        </Tab>
        <Tab Title="Pricing">
            <div class="mb-3">
                <label for="basePrice" class="form-label">Base Price</label>
                <input type="number" class="form-control" id="basePrice" />
            </div>
            <div class="mb-3">
                <label for="discount" class="form-label">Discount (%)</label>
                <input type="number" class="form-control" id="discount" />
            </div>
        </Tab>
        <Tab Title="Inventory">
            <div class="mb-3">
                <label for="stockQuantity" class="form-label">Stock Quantity</label>
                <input type="number" class="form-control" id="stockQuantity" />
            </div>
            <div class="form-check mb-3">
                <input class="form-check-input" type="checkbox" id="trackInventory" checked />
                <label class="form-check-label" for="trackInventory">Track Inventory</label>
            </div>
        </Tab>
    </Tabs>
</GroupBox>
```

## CSS Customization

The GroupBox component can be customized using CSS variables and classes:

```css
/* Custom GroupBox styling */
.group-box {
    --bb-groupbox-border-color: #dee2e6;
    --bb-groupbox-border-radius: 0.375rem;
    --bb-groupbox-header-bg: #f8f9fa;
    --bb-groupbox-header-color: #212529;
    --bb-groupbox-body-bg: #ffffff;
    --bb-groupbox-body-padding: 1rem;
    --bb-groupbox-title-font-size: 1rem;
    --bb-groupbox-title-font-weight: 500;
}

/* Custom styling for collapsible GroupBox */
.group-box-collapsible .group-box-header {
    cursor: pointer;
}

.group-box-collapsible .group-box-header:hover {
    background-color: #e9ecef;
}

/* Custom styling for solid GroupBox */
.group-box-solid {
    border: none;
    box-shadow: 0 0.125rem 0.25rem rgba(0, 0, 0, 0.075);
}

/* Custom styling for outline GroupBox */
.group-box-outline {
    border-width: 2px;
}
```

## JavaScript Interop

The GroupBox component uses JavaScript interop for collapse/expand animations and height calculations. It provides methods that can be called from C# code:

- `CollapseAsync()`: Collapses the GroupBox
- `ExpandAsync()`: Expands the GroupBox
- `ToggleAsync()`: Toggles between collapsed and expanded states

## Accessibility

The GroupBox component follows accessibility best practices:

- Uses appropriate ARIA roles and attributes
- Maintains keyboard navigation for collapsible headers
- Ensures proper focus management
- Provides visible focus indicators for keyboard users

## Browser Compatibility

The GroupBox component is compatible with all modern browsers. For older browsers, some advanced features like animations may have limited support, but the core functionality remains intact.

## Integration with Other Components

The GroupBox component can be combined with other BootstrapBlazor components:

- Use with `Form` components for structured data entry
- Combine with `Tabs` for multi-section content
- Use with `Table` for grouped data display
- Integrate with `Card` for enhanced visual presentation