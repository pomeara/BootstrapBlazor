# Transfer Component

## Overview
The Transfer component in BootstrapBlazor provides a dual-panel interface that allows users to move items between two lists. It's commonly used for assigning permissions, selecting multiple items from a source list, or any scenario where users need to select and transfer items between collections. The component offers a clean and intuitive interface with customizable options for filtering, styling, and interaction.

## Features
- **Bidirectional Transfer**: Move items from left to right and right to left
- **Filterable Lists**: Search functionality to quickly find items in large collections
- **Customizable Headers**: Set custom titles for both panels
- **Item Selection**: Select individual or multiple items for transfer
- **Bulk Transfer**: Move all items at once with dedicated buttons
- **Custom Item Templates**: Customize the appearance of list items
- **Validation Integration**: Works with form validation
- **Responsive Design**: Adapts to different screen sizes
- **Keyboard Navigation**: Accessible keyboard controls for item selection and transfer
- **Disabled State**: Option to disable the component or specific items
- **Event Callbacks**: Events for monitoring item transfers and selections
- **Custom Styling**: Extensive CSS customization options

## Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `SourceItems` | `IEnumerable<TItem>` | `[]` | Collection of items in the source (left) panel |
| `TargetItems` | `IEnumerable<TItem>` | `[]` | Collection of items in the target (right) panel |
| `SourceTitle` | `string` | "Source" | Title for the source panel |
| `TargetTitle` | `string` | "Target" | Title for the target panel |
| `ShowSearch` | `bool` | `false` | Whether to show search boxes in the panels |
| `SearchPlaceholder` | `string` | "Search" | Placeholder text for the search input |
| `Height` | `string` | `null` | Custom height for the component |
| `ItemTemplate` | `RenderFragment<TItem>` | `null` | Custom template for rendering list items |
| `DisabledItems` | `IEnumerable<TItem>` | `null` | Collection of items that should be disabled |
| `KeyField` | `string` | `null` | Property name to use as the unique key for items |
| `TextField` | `string` | `null` | Property name to use as the display text for items |
| `IsDisabled` | `bool` | `false` | Whether the component is disabled |
| `IsValid` | `bool?` | `null` | Validation state of the component |

## Events

| Event | Description |
|-------|-------------|
| `OnSourceItemsChanged` | Triggered when the source items collection changes |
| `OnTargetItemsChanged` | Triggered when the target items collection changes |
| `OnItemTransferred` | Triggered when an item is transferred between panels |
| `OnItemSelected` | Triggered when an item is selected in either panel |
| `OnSearching` | Triggered when the user types in the search box |

## Usage Examples

### Example 1: Basic Transfer

```razor
<Transfer TItem="TransferItem"
         SourceItems="@sourceItems"
         TargetItems="@targetItems"
         TextField="Name"
         KeyField="Id"
         OnSourceItemsChanged="HandleSourceItemsChanged"
         OnTargetItemsChanged="HandleTargetItemsChanged" />

@code {
    private List<TransferItem> sourceItems = new();
    private List<TransferItem> targetItems = new();
    
    protected override void OnInitialized()
    {
        // Initialize source items
        for (int i = 1; i <= 10; i++)
        {
            sourceItems.Add(new TransferItem { Id = i, Name = $"Item {i}" });
        }
    }
    
    private Task HandleSourceItemsChanged(IEnumerable<TransferItem> items)
    {
        sourceItems = items.ToList();
        return Task.CompletedTask;
    }
    
    private Task HandleTargetItemsChanged(IEnumerable<TransferItem> items)
    {
        targetItems = items.ToList();
        return Task.CompletedTask;
    }
    
    public class TransferItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
```

### Example 2: Transfer with Search

```razor
<Transfer TItem="UserModel"
         SourceItems="@availableUsers"
         TargetItems="@selectedUsers"
         TextField="DisplayName"
         KeyField="UserId"
         SourceTitle="Available Users"
         TargetTitle="Selected Users"
         ShowSearch="true"
         SearchPlaceholder="Search users..."
         OnSourceItemsChanged="HandleAvailableUsersChanged"
         OnTargetItemsChanged="HandleSelectedUsersChanged" />

@code {
    private List<UserModel> availableUsers = new();
    private List<UserModel> selectedUsers = new();
    
    protected override void OnInitialized()
    {
        // Initialize with sample data
        availableUsers = new List<UserModel>
        {
            new UserModel { UserId = 1, DisplayName = "John Doe", Department = "Engineering" },
            new UserModel { UserId = 2, DisplayName = "Jane Smith", Department = "Marketing" },
            new UserModel { UserId = 3, DisplayName = "Robert Johnson", Department = "Finance" },
            new UserModel { UserId = 4, DisplayName = "Emily Davis", Department = "HR" },
            new UserModel { UserId = 5, DisplayName = "Michael Wilson", Department = "Engineering" },
            new UserModel { UserId = 6, DisplayName = "Sarah Brown", Department = "Marketing" },
            new UserModel { UserId = 7, DisplayName = "David Miller", Department = "Finance" },
            new UserModel { UserId = 8, DisplayName = "Jennifer Taylor", Department = "HR" }
        };
    }
    
    private Task HandleAvailableUsersChanged(IEnumerable<UserModel> users)
    {
        availableUsers = users.ToList();
        return Task.CompletedTask;
    }
    
    private Task HandleSelectedUsersChanged(IEnumerable<UserModel> users)
    {
        selectedUsers = users.ToList();
        return Task.CompletedTask;
    }
    
    public class UserModel
    {
        public int UserId { get; set; }
        public string DisplayName { get; set; }
        public string Department { get; set; }
    }
}
```

### Example 3: Custom Item Template

```razor
<Transfer TItem="ProductModel"
         SourceItems="@availableProducts"
         TargetItems="@selectedProducts"
         KeyField="ProductId"
         SourceTitle="Available Products"
         TargetTitle="Selected Products"
         OnSourceItemsChanged="HandleAvailableProductsChanged"
         OnTargetItemsChanged="HandleSelectedProductsChanged">
    <ItemTemplate>
        <div class="d-flex align-items-center">
            <div class="product-color" style="background-color: @context.Color; width: 16px; height: 16px; border-radius: 50%; margin-right: 8px;"></div>
            <div>
                <div><strong>@context.Name</strong></div>
                <small class="text-muted">$@context.Price.ToString("F2")</small>
            </div>
        </div>
    </ItemTemplate>
</Transfer>

@code {
    private List<ProductModel> availableProducts = new();
    private List<ProductModel> selectedProducts = new();
    
    protected override void OnInitialized()
    {
        // Initialize with sample data
        availableProducts = new List<ProductModel>
        {
            new ProductModel { ProductId = 1, Name = "Red Widget", Price = 19.99m, Color = "#FF5252" },
            new ProductModel { ProductId = 2, Name = "Blue Widget", Price = 24.99m, Color = "#536DFE" },
            new ProductModel { ProductId = 3, Name = "Green Widget", Price = 15.99m, Color = "#4CAF50" },
            new ProductModel { ProductId = 4, Name = "Yellow Widget", Price = 29.99m, Color = "#FFD740" },
            new ProductModel { ProductId = 5, Name = "Purple Widget", Price = 34.99m, Color = "#9C27B0" }
        };
    }
    
    private Task HandleAvailableProductsChanged(IEnumerable<ProductModel> products)
    {
        availableProducts = products.ToList();
        return Task.CompletedTask;
    }
    
    private Task HandleSelectedProductsChanged(IEnumerable<ProductModel> products)
    {
        selectedProducts = products.ToList();
        return Task.CompletedTask;
    }
    
    public class ProductModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
    }
}
```

### Example 4: Transfer with Disabled Items

```razor
<Transfer TItem="PermissionModel"
         SourceItems="@availablePermissions"
         TargetItems="@assignedPermissions"
         TextField="Name"
         KeyField="Id"
         DisabledItems="@disabledPermissions"
         SourceTitle="Available Permissions"
         TargetTitle="Assigned Permissions"
         OnSourceItemsChanged="HandleAvailablePermissionsChanged"
         OnTargetItemsChanged="HandleAssignedPermissionsChanged" />

@code {
    private List<PermissionModel> availablePermissions = new();
    private List<PermissionModel> assignedPermissions = new();
    private List<PermissionModel> disabledPermissions = new();
    
    protected override void OnInitialized()
    {
        // Initialize with sample data
        availablePermissions = new List<PermissionModel>
        {
            new PermissionModel { Id = 1, Name = "View Dashboard", Category = "Dashboard" },
            new PermissionModel { Id = 2, Name = "Edit Dashboard", Category = "Dashboard" },
            new PermissionModel { Id = 3, Name = "View Reports", Category = "Reports" },
            new PermissionModel { Id = 4, Name = "Create Reports", Category = "Reports" },
            new PermissionModel { Id = 5, Name = "Edit Reports", Category = "Reports" },
            new PermissionModel { Id = 6, Name = "Delete Reports", Category = "Reports" },
            new PermissionModel { Id = 7, Name = "View Users", Category = "Users" },
            new PermissionModel { Id = 8, Name = "Edit Users", Category = "Users" }
        };
        
        // Set some permissions as disabled (cannot be transferred)
        disabledPermissions = availablePermissions.Where(p => p.Id == 6 || p.Id == 8).ToList();
    }
    
    private Task HandleAvailablePermissionsChanged(IEnumerable<PermissionModel> permissions)
    {
        availablePermissions = permissions.ToList();
        return Task.CompletedTask;
    }
    
    private Task HandleAssignedPermissionsChanged(IEnumerable<PermissionModel> permissions)
    {
        assignedPermissions = permissions.ToList();
        return Task.CompletedTask;
    }
    
    public class PermissionModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
    }
}
```

### Example 5: Transfer with Form Validation

```razor
<ValidateForm Model="@model" OnValidSubmit="HandleValidSubmit">
    <div class="mb-3">
        <BootstrapInput @bind-Value="@model.RoleName" 
                       ShowLabel="true" 
                       DisplayText="Role Name" 
                       Placeholder="Enter role name" />
        <ValidationMessage For="@(() => model.RoleName)" />
    </div>
    
    <div class="mb-3">
        <BootstrapInput @bind-Value="@model.Description" 
                       ShowLabel="true" 
                       DisplayText="Description" 
                       Placeholder="Enter role description" />
        <ValidationMessage For="@(() => model.Description)" />
    </div>
    
    <div class="mb-3">
        <label>Assigned Permissions</label>
        <Transfer TItem="PermissionModel"
                 @bind-TargetItems="@model.Permissions"
                 SourceItems="@availablePermissions"
                 TextField="Name"
                 KeyField="Id"
                 SourceTitle="Available Permissions"
                 TargetTitle="Assigned Permissions"
                 IsValid="@(model.Permissions.Any())"
                 ShowSearch="true" />
        <ValidationMessage For="@(() => model.Permissions)" />
    </div>
    
    <Button Type="ButtonType.Submit">Save Role</Button>
</ValidateForm>

@code {
    private RoleModel model = new();
    private List<PermissionModel> availablePermissions = new();
    
    protected override void OnInitialized()
    {
        // Initialize available permissions
        availablePermissions = new List<PermissionModel>
        {
            new PermissionModel { Id = 1, Name = "View Dashboard" },
            new PermissionModel { Id = 2, Name = "Edit Dashboard" },
            new PermissionModel { Id = 3, Name = "View Reports" },
            new PermissionModel { Id = 4, Name = "Create Reports" },
            new PermissionModel { Id = 5, Name = "Edit Reports" },
            new PermissionModel { Id = 6, Name = "Delete Reports" },
            new PermissionModel { Id = 7, Name = "View Users" },
            new PermissionModel { Id = 8, Name = "Edit Users" }
        };
    }
    
    private void HandleValidSubmit()
    {
        // Process the form data
        Console.WriteLine($"Role: {model.RoleName}");
        Console.WriteLine($"Description: {model.Description}");
        Console.WriteLine($"Permissions: {model.Permissions.Count}");
    }
    
    public class RoleModel
    {
        [Required(ErrorMessage = "Role name is required")]
        public string RoleName { get; set; } = "";
        
        public string Description { get; set; } = "";
        
        [Required(ErrorMessage = "At least one permission must be assigned")]
        [MinLength(1, ErrorMessage = "At least one permission must be assigned")]
        public List<PermissionModel> Permissions { get; set; } = new();
    }
    
    public class PermissionModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
```

### Example 6: Transfer with Event Handling

```razor
<div class="mb-3">
    <Transfer TItem="TeamMemberModel"
             SourceItems="@availableMembers"
             TargetItems="@teamMembers"
             TextField="Name"
             KeyField="Id"
             SourceTitle="Available Members"
             TargetTitle="Team Members"
             OnSourceItemsChanged="HandleAvailableMembersChanged"
             OnTargetItemsChanged="HandleTeamMembersChanged"
             OnItemTransferred="HandleItemTransferred"
             OnItemSelected="HandleItemSelected" />
</div>

<div class="mt-3">
    <h5>Event Log</h5>
    <div class="border p-3 bg-light" style="max-height: 200px; overflow-y: auto;">
        @foreach (var log in eventLogs.AsEnumerable().Reverse())
        {
            <div class="mb-1">@log</div>
        }
    </div>
</div>

@code {
    private List<TeamMemberModel> availableMembers = new();
    private List<TeamMemberModel> teamMembers = new();
    private List<string> eventLogs = new();
    
    protected override void OnInitialized()
    {
        // Initialize with sample data
        availableMembers = new List<TeamMemberModel>
        {
            new TeamMemberModel { Id = 1, Name = "John Doe", Role = "Developer" },
            new TeamMemberModel { Id = 2, Name = "Jane Smith", Role = "Designer" },
            new TeamMemberModel { Id = 3, Name = "Robert Johnson", Role = "QA Engineer" },
            new TeamMemberModel { Id = 4, Name = "Emily Davis", Role = "Product Manager" },
            new TeamMemberModel { Id = 5, Name = "Michael Wilson", Role = "DevOps Engineer" }
        };
    }
    
    private Task HandleAvailableMembersChanged(IEnumerable<TeamMemberModel> members)
    {
        availableMembers = members.ToList();
        LogEvent($"Available members updated. Count: {availableMembers.Count}");
        return Task.CompletedTask;
    }
    
    private Task HandleTeamMembersChanged(IEnumerable<TeamMemberModel> members)
    {
        teamMembers = members.ToList();
        LogEvent($"Team members updated. Count: {teamMembers.Count}");
        return Task.CompletedTask;
    }
    
    private void HandleItemTransferred(TeamMemberModel member)
    {
        LogEvent($"Member transferred: {member.Name} ({member.Role})");
    }
    
    private void HandleItemSelected(TeamMemberModel member)
    {
        LogEvent($"Member selected: {member.Name} ({member.Role})");
    }
    
    private void LogEvent(string message)
    {
        eventLogs.Add($"[{DateTime.Now:HH:mm:ss}] {message}");
        StateHasChanged();
    }
    
    public class TeamMemberModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
    }
}
```

### Example 7: Responsive Transfer with Custom Height

```razor
<div class="mb-3">
    <div class="form-check form-switch">
        <input class="form-check-input" type="checkbox" id="responsiveSwitch" @bind="isResponsive">
        <label class="form-check-label" for="responsiveSwitch">Responsive Mode</label>
    </div>
</div>

<div class="@(isResponsive ? "row" : "")">
    <div class="@(isResponsive ? "col-md-12 col-lg-6 mb-3 mb-lg-0" : "")">
        <Transfer TItem="CategoryItemModel"
                 SourceItems="@availableItems"
                 TargetItems="@selectedItems"
                 TextField="Name"
                 KeyField="Id"
                 SourceTitle="Available Items"
                 TargetTitle="Selected Items"
                 Height="@(isResponsive ? "300px" : "400px")"
                 ShowSearch="true"
                 OnSourceItemsChanged="HandleAvailableItemsChanged"
                 OnTargetItemsChanged="HandleSelectedItemsChanged" />
    </div>
    
    <div class="@(isResponsive ? "col-md-12 col-lg-6" : "mt-3")">
        <div class="card">
            <div class="card-header">
                <h5 class="mb-0">Selected Items Summary</h5>
            </div>
            <div class="card-body">
                @if (selectedItems.Any())
                {
                    <ul class="list-group">
                        @foreach (var category in selectedItems.GroupBy(i => i.Category))
                        {
                            <li class="list-group-item">
                                <strong>@category.Key:</strong> @category.Count() items
                            </li>
                        }
                    </ul>
                    <div class="mt-3">
                        <strong>Total:</strong> @selectedItems.Count items
                    </div>
                }
                else
                {
                    <p class="text-muted">No items selected</p>
                }
            </div>
        </div>
    </div>
</div>

@code {
    private List<CategoryItemModel> availableItems = new();
    private List<CategoryItemModel> selectedItems = new();
    private bool isResponsive = true;
    
    protected override void OnInitialized()
    {
        // Initialize with sample data
        var categories = new[] { "Electronics", "Clothing", "Books", "Home & Garden", "Sports" };
        
        for (int i = 1; i <= 50; i++)
        {
            var category = categories[i % categories.Length];
            availableItems.Add(new CategoryItemModel 
            { 
                Id = i, 
                Name = $"{category} Item {i}", 
                Category = category 
            });
        }
    }
    
    private Task HandleAvailableItemsChanged(IEnumerable<CategoryItemModel> items)
    {
        availableItems = items.ToList();
        return Task.CompletedTask;
    }
    
    private Task HandleSelectedItemsChanged(IEnumerable<CategoryItemModel> items)
    {
        selectedItems = items.ToList();
        return Task.CompletedTask;
    }
    
    public class CategoryItemModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
    }
}
```

## CSS Customization

The Transfer component can be customized using CSS variables:

```css
/* Custom styles for Transfer component */
.transfer {
    --bb-transfer-panel-header-height: 40px;
    --bb-transfer-panel-header-padding: 0.5rem 1rem;
    --bb-transfer-panel-body-padding: 0.5rem;
    --bb-transfer-panel-list-max-height: 300px;
    --bb-transfer-panel-list-min-height: 100px;
    --bb-transfer-panel-item-padding: 0.5rem;
    --bb-transfer-panel-item-margin: 0.25rem 0;
    --bb-transfer-panel-item-width: 100%;
    --bb-transfer-buttons-padding: 0 1rem;
    --bb-transfer-filter-focus-border-color: #0d6efd;
    --bb-transfer-filter-margin: 0 0 0.5rem 0;
    --bb-transfer-height: 400px;
}

/* Custom panel styling */
.transfer-panel {
    /* Panel styling */
}

/* Custom header styling */
.transfer-panel-header {
    /* Header styling */
}

/* Custom item styling */
.transfer-panel-item {
    /* Item styling */
}

/* Custom button styling */
.transfer-buttons .btn {
    /* Button styling */
}

/* Custom search box styling */
.transfer-panel-filter .input-inner {
    /* Search box styling */
}
```

## Accessibility

The Transfer component follows accessibility best practices:

- Proper ARIA attributes for screen readers
- Keyboard navigation support (Tab, Space, Enter, Arrow keys)
- Focus management for interactive elements
- High contrast mode support
- Screen reader announcements for item transfers

## Browser Compatibility

The Transfer component is compatible with all modern browsers:

- Chrome
- Firefox
- Edge
- Safari
- Opera

## Integration with Other Components

The Transfer component can be integrated with various other BootstrapBlazor components:

- Use with Form and ValidateForm for data collection
- Combine with Button for additional actions
- Integrate with Modal or Drawer for popup selection interfaces
- Pair with Alert or Toast for transfer notifications
- Use with Card for styled containers