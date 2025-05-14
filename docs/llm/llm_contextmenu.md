# ContextMenu Component

## Overview
The ContextMenu component in BootstrapBlazor provides a customizable right-click context menu for any element in your application. It allows users to access contextual actions and commands through a popup menu that appears at the cursor position when right-clicking on designated elements.

## Key Features
- Context-sensitive menu triggered by right-click events
- Support for nested menu items and submenus
- Customizable appearance and positioning
- Dynamic menu item generation based on context
- Keyboard navigation support
- Icon support for menu items
- Disabled state for menu items
- Event callbacks for menu interactions
- Template support for custom menu item rendering

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `Items` | `IEnumerable<ContextMenuItem>` | `null` | Collection of menu items to display in the context menu |
| `Target` | `string` | `null` | CSS selector for the target element(s) that will trigger the context menu |
| `IsShow` | `bool` | `false` | Gets or sets whether the context menu is visible |
| `ShowShadow` | `bool` | `true` | Determines whether to show a shadow effect for the menu |
| `ShowAnimation` | `bool` | `true` | Enables or disables animation when showing/hiding the menu |
| `IsDisabled` | `bool` | `false` | Disables the context menu when set to true |
| `Placement` | `Placement` | `Auto` | Sets the preferred placement of the context menu relative to the cursor |
| `Width` | `int` | `200` | Width of the context menu in pixels |
| `OnBeforeShow` | `Func<Task<bool>>` | `null` | Callback executed before showing the menu, can prevent showing by returning false |
| `OnAfterShow` | `Func<Task>` | `null` | Callback executed after the menu is shown |
| `OnBeforeHide` | `Func<Task<bool>>` | `null` | Callback executed before hiding the menu, can prevent hiding by returning false |
| `OnAfterHide` | `Func<Task>` | `null` | Callback executed after the menu is hidden |
| `OnItemClick` | `Func<ContextMenuItem, Task>` | `null` | Callback executed when a menu item is clicked |

## Events

| Event | Description |
| --- | --- |
| `OnBeforeShowEvent` | Triggered before the context menu is shown |
| `OnAfterShowEvent` | Triggered after the context menu is shown |
| `OnBeforeHideEvent` | Triggered before the context menu is hidden |
| `OnAfterHideEvent` | Triggered after the context menu is hidden |
| `OnItemClickEvent` | Triggered when a menu item is clicked |

## Usage Examples

### Example 1: Basic Context Menu

```razor
@page "/context-menu-demo"

<div id="contextMenuTarget" class="demo-container p-4 border">
    Right-click anywhere in this area to show the context menu.
</div>

<ContextMenu Target="#contextMenuTarget" OnItemClick="OnMenuItemClick">
    <Items>
        <ContextMenuItem Text="View" Icon="fa-solid fa-eye" />
        <ContextMenuItem Text="Edit" Icon="fa-solid fa-edit" />
        <ContextMenuItem Text="Delete" Icon="fa-solid fa-trash" IsDisabled="true" />
        <ContextMenuItem IsDivider="true" />
        <ContextMenuItem Text="Properties" Icon="fa-solid fa-cog" />
    </Items>
</ContextMenu>

@code {
    private async Task OnMenuItemClick(ContextMenuItem item)
    {
        await Task.Delay(10);
        Console.WriteLine($"Menu item clicked: {item.Text}");
    }
}
```

### Example 2: Nested Context Menu

```razor
@page "/nested-context-menu"

<div id="nestedMenuTarget" class="demo-container p-4 border">
    Right-click to show a context menu with nested items.
</div>

<ContextMenu Target="#nestedMenuTarget">
    <Items>
        <ContextMenuItem Text="File" Icon="fa-solid fa-file">
            <Items>
                <ContextMenuItem Text="New" Icon="fa-solid fa-plus" />
                <ContextMenuItem Text="Open" Icon="fa-solid fa-folder-open" />
                <ContextMenuItem Text="Save" Icon="fa-solid fa-save" />
                <ContextMenuItem Text="Save As" Icon="fa-solid fa-save" />
                <ContextMenuItem IsDivider="true" />
                <ContextMenuItem Text="Exit" Icon="fa-solid fa-sign-out-alt" />
            </Items>
        </ContextMenuItem>
        <ContextMenuItem Text="Edit" Icon="fa-solid fa-edit">
            <Items>
                <ContextMenuItem Text="Cut" Icon="fa-solid fa-cut" />
                <ContextMenuItem Text="Copy" Icon="fa-solid fa-copy" />
                <ContextMenuItem Text="Paste" Icon="fa-solid fa-paste" />
                <ContextMenuItem IsDivider="true" />
                <ContextMenuItem Text="Find" Icon="fa-solid fa-search" />
                <ContextMenuItem Text="Replace" Icon="fa-solid fa-exchange-alt" />
            </Items>
        </ContextMenuItem>
        <ContextMenuItem Text="View" Icon="fa-solid fa-eye">
            <Items>
                <ContextMenuItem Text="Zoom In" Icon="fa-solid fa-search-plus" />
                <ContextMenuItem Text="Zoom Out" Icon="fa-solid fa-search-minus" />
                <ContextMenuItem Text="Reset Zoom" Icon="fa-solid fa-undo" />
            </Items>
        </ContextMenuItem>
        <ContextMenuItem Text="Help" Icon="fa-solid fa-question-circle" />
    </Items>
</ContextMenu>
```

### Example 3: Dynamic Context Menu Based on Data

```razor
@page "/dynamic-context-menu"
@inject MessageService MessageService

<Table TItem="Person" Items="@people" ShowToolbar="true">
    <TableColumns>
        <TableColumn @bind-Field="@context.Id" Width="80" />
        <TableColumn @bind-Field="@context.Name" />
        <TableColumn @bind-Field="@context.Age" />
        <TableColumn @bind-Field="@context.Address" />
        <TableColumn Text="Actions" Width="200">
            <Button Size="Size.Small" OnClick="() => ShowContextMenu(context)">
                <i class="fa-solid fa-ellipsis-v"></i>
            </Button>
        </TableColumn>
    </TableColumns>
</Table>

<ContextMenu @ref="contextMenu" OnItemClick="OnMenuItemClick">
    <Items>
        <ContextMenuItem Text="View Details" Icon="fa-solid fa-eye" Value="view" />
        <ContextMenuItem Text="Edit Record" Icon="fa-solid fa-edit" Value="edit" />
        <ContextMenuItem Text="Delete Record" Icon="fa-solid fa-trash" Value="delete" />
        <ContextMenuItem IsDivider="true" />
        <ContextMenuItem Text="Export Data" Icon="fa-solid fa-file-export" Value="export" />
    </Items>
</ContextMenu>

@code {
    private ContextMenu contextMenu;
    private Person selectedPerson;
    private List<Person> people = new List<Person>();

    protected override void OnInitialized()
    {
        for (int i = 1; i <= 10; i++)
        {
            people.Add(new Person
            {
                Id = i,
                Name = $"Person {i}",
                Age = 20 + i,
                Address = $"Address {i}"
            });
        }
    }

    private async Task ShowContextMenu(Person person)
    {
        selectedPerson = person;
        await contextMenu.ShowAsync(new MouseEventArgs());
    }

    private async Task OnMenuItemClick(ContextMenuItem item)
    {
        if (selectedPerson == null) return;

        switch (item.Value?.ToString())
        {
            case "view":
                await MessageService.Show(new MessageOption()
                {
                    Content = $"Viewing details for {selectedPerson.Name}"
                });
                break;
            case "edit":
                await MessageService.Show(new MessageOption()
                {
                    Content = $"Editing {selectedPerson.Name}"
                });
                break;
            case "delete":
                await MessageService.Show(new MessageOption()
                {
                    Content = $"Deleting {selectedPerson.Name}"
                });
                break;
            case "export":
                await MessageService.Show(new MessageOption()
                {
                    Content = $"Exporting data for {selectedPerson.Name}"
                });
                break;
        }
    }

    class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
    }
}
```

### Example 4: Custom Template for Menu Items

```razor
@page "/custom-template-context-menu"

<div id="customTemplateTarget" class="demo-container p-4 border">
    Right-click to show a context menu with custom templates.
</div>

<ContextMenu Target="#customTemplateTarget">
    <Items>
        <ContextMenuItem>
            <Template>
                <div class="d-flex align-items-center">
                    <i class="fa-solid fa-palette me-2"></i>
                    <span>Theme:</span>
                    <div class="ms-2">
                        <div class="btn-group btn-group-sm" role="group">
                            <button type="button" class="btn btn-primary" @onclick="() => ChangeTheme('primary')">Blue</button>
                            <button type="button" class="btn btn-success" @onclick="() => ChangeTheme('success')">Green</button>
                            <button type="button" class="btn btn-danger" @onclick="() => ChangeTheme('danger')">Red</button>
                        </div>
                    </div>
                </div>
            </Template>
        </ContextMenuItem>
        <ContextMenuItem IsDivider="true" />
        <ContextMenuItem>
            <Template>
                <div class="d-flex align-items-center">
                    <i class="fa-solid fa-text-height me-2"></i>
                    <span>Font Size:</span>
                    <div class="ms-2 w-50">
                        <input type="range" class="form-range" min="12" max="24" step="1" @bind="fontSize" @bind:event="oninput" />
                    </div>
                    <span class="ms-2">@fontSize px</span>
                </div>
            </Template>
        </ContextMenuItem>
        <ContextMenuItem IsDivider="true" />
        <ContextMenuItem>
            <Template>
                <div class="d-flex align-items-center">
                    <i class="fa-solid fa-star me-2"></i>
                    <span>Rating:</span>
                    <div class="ms-2">
                        <Rate @bind-Value="rating" AllowHalf="true" />
                    </div>
                </div>
            </Template>
        </ContextMenuItem>
    </Items>
</ContextMenu>

@code {
    private string theme = "primary";
    private int fontSize = 16;
    private double rating = 3.5;

    private void ChangeTheme(string newTheme)
    {
        theme = newTheme;
        // Apply theme change logic here
    }
}
```

### Example 5: Context Menu with Event Callbacks

```razor
@page "/context-menu-events"
@inject MessageService MessageService

<div id="eventCallbackTarget" class="demo-container p-4 border">
    Right-click to show a context menu with event callbacks.
</div>

<ContextMenu Target="#eventCallbackTarget"
             OnBeforeShow="HandleBeforeShow"
             OnAfterShow="HandleAfterShow"
             OnBeforeHide="HandleBeforeHide"
             OnAfterHide="HandleAfterHide"
             OnItemClick="HandleItemClick">
    <Items>
        <ContextMenuItem Text="Option 1" Icon="fa-solid fa-check" />
        <ContextMenuItem Text="Option 2" Icon="fa-solid fa-times" />
        <ContextMenuItem Text="Option 3" Icon="fa-solid fa-cog" />
    </Items>
</ContextMenu>

<div class="mt-3">
    <h5>Event Log:</h5>
    <ul class="list-group">
        @foreach (var log in eventLogs)
        {
            <li class="list-group-item">@log</li>
        }
    </ul>
    <Button Color="Color.Secondary" OnClick="ClearLogs" Class="mt-2">Clear Logs</Button>
</div>

@code {
    private List<string> eventLogs = new List<string>();

    private async Task<bool> HandleBeforeShow()
    {
        AddLog("OnBeforeShow triggered");
        
        // You can conditionally prevent the menu from showing
        var allowShow = true;
        
        if (!allowShow)
        {
            AddLog("Menu showing prevented by OnBeforeShow");
            await MessageService.Show(new MessageOption()
            {
                Content = "Menu showing was prevented"
            });
        }
        
        return allowShow;
    }

    private Task HandleAfterShow()
    {
        AddLog("OnAfterShow triggered - Menu is now visible");
        return Task.CompletedTask;
    }

    private Task<bool> HandleBeforeHide()
    {
        AddLog("OnBeforeHide triggered");
        return Task.FromResult(true); // Allow hiding
    }

    private Task HandleAfterHide()
    {
        AddLog("OnAfterHide triggered - Menu is now hidden");
        return Task.CompletedTask;
    }

    private Task HandleItemClick(ContextMenuItem item)
    {
        AddLog($"Item clicked: {item.Text}");
        return Task.CompletedTask;
    }

    private void AddLog(string message)
    {
        eventLogs.Insert(0, $"[{DateTime.Now:HH:mm:ss}] {message}");
        
        // Keep only the last 10 logs
        if (eventLogs.Count > 10)
        {
            eventLogs.RemoveAt(eventLogs.Count - 1);
        }
        
        StateHasChanged();
    }

    private void ClearLogs()
    {
        eventLogs.Clear();
    }
}
```

### Example 6: Multiple Context Menus for Different Elements

```razor
@page "/multiple-context-menus"
@inject MessageService MessageService

<div class="row">
    <div class="col-md-6">
        <div id="imageContextTarget" class="demo-container p-4 border text-center">
            <img src="/images/placeholder.jpg" style="max-width: 100%" alt="Sample image" />
            <p>Right-click on the image for image-specific options</p>
        </div>
    </div>
    <div class="col-md-6">
        <div id="textContextTarget" class="demo-container p-4 border">
            <h4>Sample Text</h4>
            <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam euismod, nisl eget aliquam ultricies, nunc nisl aliquet nunc, quis aliquam nisl nunc quis nisl.</p>
            <p>Right-click on this text for text-specific options</p>
        </div>
    </div>
</div>

<!-- Image Context Menu -->
<ContextMenu Target="#imageContextTarget" OnItemClick="OnImageMenuItemClick">
    <Items>
        <ContextMenuItem Text="View Image" Icon="fa-solid fa-eye" />
        <ContextMenuItem Text="Save Image" Icon="fa-solid fa-download" />
        <ContextMenuItem Text="Rotate Image" Icon="fa-solid fa-redo" />
        <ContextMenuItem Text="Crop Image" Icon="fa-solid fa-crop" />
        <ContextMenuItem IsDivider="true" />
        <ContextMenuItem Text="Image Properties" Icon="fa-solid fa-info-circle" />
    </Items>
</ContextMenu>

<!-- Text Context Menu -->
<ContextMenu Target="#textContextTarget" OnItemClick="OnTextMenuItemClick">
    <Items>
        <ContextMenuItem Text="Copy" Icon="fa-solid fa-copy" />
        <ContextMenuItem Text="Select All" Icon="fa-solid fa-object-group" />
        <ContextMenuItem IsDivider="true" />
        <ContextMenuItem Text="Font Size" Icon="fa-solid fa-text-height">
            <Items>
                <ContextMenuItem Text="Small" />
                <ContextMenuItem Text="Medium" />
                <ContextMenuItem Text="Large" />
            </Items>
        </ContextMenuItem>
        <ContextMenuItem Text="Text Color" Icon="fa-solid fa-palette">
            <Items>
                <ContextMenuItem Text="Red" />
                <ContextMenuItem Text="Green" />
                <ContextMenuItem Text="Blue" />
                <ContextMenuItem Text="Black" />
            </Items>
        </ContextMenuItem>
    </Items>
</ContextMenu>

@code {
    private async Task OnImageMenuItemClick(ContextMenuItem item)
    {
        await MessageService.Show(new MessageOption()
        {
            Content = $"Image action: {item.Text}"
        });
    }

    private async Task OnTextMenuItemClick(ContextMenuItem item)
    {
        await MessageService.Show(new MessageOption()
        {
            Content = $"Text action: {item.Text}"
        });
    }
}
```

### Example 7: Programmatically Controlled Context Menu

```razor
@page "/programmatic-context-menu"
@inject MessageService MessageService

<div class="mb-3">
    <Button Color="Color.Primary" OnClick="ShowContextMenuAtPosition">Show Context Menu</Button>
    <Button Color="Color.Secondary" OnClick="HideContextMenu">Hide Context Menu</Button>
</div>

<div id="canvasArea" class="position-relative" style="height: 400px; background-color: #f5f5f5; border: 1px solid #ddd;">
    @foreach (var point in clickPoints)
    {
        <div class="position-absolute" style="left: @(point.X - 5)px; top: @(point.Y - 5)px; width: 10px; height: 10px; background-color: red; border-radius: 50%;"
             @onclick="() => ShowContextMenuAtPoint(point)"></div>
    }
</div>

<ContextMenu @ref="contextMenu" OnItemClick="OnMenuItemClick">
    <Items>
        <ContextMenuItem Text="Add Point" Icon="fa-solid fa-plus" Value="add" />
        <ContextMenuItem Text="Remove Point" Icon="fa-solid fa-minus" Value="remove" />
        <ContextMenuItem Text="Clear All Points" Icon="fa-solid fa-trash" Value="clear" />
        <ContextMenuItem IsDivider="true" />
        <ContextMenuItem Text="Change Color" Icon="fa-solid fa-palette">
            <Items>
                <ContextMenuItem Text="Red" Value="red" />
                <ContextMenuItem Text="Green" Value="green" />
                <ContextMenuItem Text="Blue" Value="blue" />
                <ContextMenuItem Text="Yellow" Value="yellow" />
            </Items>
        </ContextMenuItem>
    </Items>
</ContextMenu>

@code {
    private ContextMenu contextMenu;
    private List<Point> clickPoints = new List<Point>();
    private Point currentPoint;

    protected override void OnInitialized()
    {
        // Add some initial points
        clickPoints.Add(new Point { X = 100, Y = 100 });
        clickPoints.Add(new Point { X = 200, Y = 150 });
        clickPoints.Add(new Point { X = 150, Y = 250 });
    }

    private async Task ShowContextMenuAtPosition()
    {
        // Show context menu at a specific position
        var mouseEventArgs = new MouseEventArgs
        {
            ClientX = 200,
            ClientY = 200
        };
        
        await contextMenu.ShowAsync(mouseEventArgs);
    }

    private async Task ShowContextMenuAtPoint(Point point)
    {
        currentPoint = point;
        
        var mouseEventArgs = new MouseEventArgs
        {
            ClientX = point.X,
            ClientY = point.Y
        };
        
        await contextMenu.ShowAsync(mouseEventArgs);
    }

    private async Task HideContextMenu()
    {
        await contextMenu.HideAsync();
    }

    private async Task OnMenuItemClick(ContextMenuItem item)
    {
        var value = item.Value?.ToString();
        
        switch (value)
        {
            case "add":
                var random = new Random();
                clickPoints.Add(new Point { X = random.Next(50, 350), Y = random.Next(50, 350) });
                break;
                
            case "remove":
                if (currentPoint != null)
                {
                    clickPoints.Remove(currentPoint);
                    currentPoint = null;
                }
                break;
                
            case "clear":
                clickPoints.Clear();
                break;
                
            case "red":
            case "green":
            case "blue":
            case "yellow":
                await MessageService.Show(new MessageOption()
                {
                    Content = $"Changed color to {value}"
                });
                break;
        }
    }

    class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}
```

## CSS Customization

The ContextMenu component can be customized using CSS variables and classes:

```css
/* Custom context menu styling */
.context-menu {
    --bb-context-menu-bg: #ffffff;
    --bb-context-menu-border-color: #dee2e6;
    --bb-context-menu-border-radius: 4px;
    --bb-context-menu-box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
    --bb-context-menu-min-width: 180px;
    --bb-context-menu-padding: 0.5rem 0;
    --bb-context-menu-z-index: 1050;
    
    --bb-context-menu-item-padding: 0.5rem 1rem;
    --bb-context-menu-item-hover-bg: #f8f9fa;
    --bb-context-menu-item-active-bg: #e9ecef;
    --bb-context-menu-item-disabled-color: #6c757d;
    --bb-context-menu-item-icon-width: 1.25rem;
    --bb-context-menu-item-icon-margin: 0 0.5rem 0 0;
    
    --bb-context-menu-divider-margin: 0.5rem 0;
    --bb-context-menu-divider-color: #dee2e6;
}

/* Dark theme override */
.context-menu.dark {
    --bb-context-menu-bg: #343a40;
    --bb-context-menu-border-color: #495057;
    --bb-context-menu-box-shadow: 0 2px 10px rgba(0, 0, 0, 0.25);
    
    --bb-context-menu-item-color: #f8f9fa;
    --bb-context-menu-item-hover-bg: #495057;
    --bb-context-menu-item-active-bg: #6c757d;
    --bb-context-menu-item-disabled-color: #adb5bd;
    
    --bb-context-menu-divider-color: #6c757d;
}
```

## JavaScript Interop

The ContextMenu component uses JavaScript interop for positioning, showing, and hiding the menu. These operations are handled internally by the component.

## Accessibility

The ContextMenu component includes the following accessibility features:
- Keyboard navigation (arrow keys, Enter, Escape)
- ARIA roles and attributes for screen readers
- Focus management when opening and closing the menu
- Support for disabled states with appropriate styling

## Browser Compatibility

The ContextMenu component is compatible with all modern browsers including:
- Chrome
- Firefox
- Safari
- Edge

## Performance Considerations

For optimal performance:
- Avoid creating too many nested submenus
- Use lazy loading for dynamic menu items when possible
- Consider using virtualization for very large menu structures
- Implement proper event handling to prevent memory leaks

## Integration with Other Components

The ContextMenu component works well with:
- Table components for row actions
- Tree components for node operations
- Form components for contextual editing
- Button components for triggering context menus programmatically

## Best Practices

1. Keep menu structures simple and intuitive
2. Group related actions together
3. Use icons to improve visual recognition
4. Provide keyboard shortcuts for common actions
5. Use dividers to separate logical groups of actions
6. Implement proper event handling for menu interactions
7. Consider the context when determining which actions to show
8. Provide visual feedback for disabled items