# RibbonTab Component

## Overview
The RibbonTab component in BootstrapBlazor provides a Microsoft Office-style ribbon interface for organizing commands and controls into logical groups. It offers a comprehensive UI solution for applications that require many features to be accessible in a compact, organized manner. The RibbonTab extends the standard Tab component with additional functionality specifically designed for command-rich interfaces.

## Key Features
- **Office-Style Ribbon Interface**: Mimics the familiar Microsoft Office ribbon UI pattern
- **Collapsible Sections**: Tabs can be collapsed to save screen space when not in use
- **Grouped Commands**: Organizes related commands into logical groups within each tab
- **Quick Access Toolbar**: Optional toolbar for frequently used commands
- **Responsive Design**: Adapts to different screen sizes with appropriate layouts
- **Customizable Appearance**: Supports theming and custom styling
- **Keyboard Navigation**: Full keyboard support for accessibility
- **Icon Support**: Integrates with icons for visual command representation
- **Dynamic Content**: Can dynamically show/hide tabs and commands based on context
- **Tooltips**: Provides helpful tooltips for commands

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `Items` | `IEnumerable<RibbonTabItem>` | `null` | Collection of ribbon tab items to display |
| `ActiveTab` | `string` | `null` | The key of the currently active tab |
| `IsCollapsible` | `bool` | `true` | Whether the ribbon can be collapsed |
| `IsCollapsed` | `bool` | `false` | Whether the ribbon is initially collapsed |
| `QuickAccessCommands` | `IEnumerable<RibbonCommand>` | `null` | Commands to show in the quick access toolbar |
| `ShowQuickAccessToolbar` | `bool` | `true` | Whether to display the quick access toolbar |
| `TabPosition` | `TabPosition` | `Top` | Position of the tabs (Top, Bottom) |
| `GroupTemplate` | `RenderFragment<RibbonGroup>` | `null` | Template for rendering ribbon groups |
| `CommandTemplate` | `RenderFragment<RibbonCommand>` | `null` | Template for rendering individual commands |
| `HeaderTemplate` | `RenderFragment` | `null` | Template for the ribbon header area |
| `FooterTemplate` | `RenderFragment` | `null` | Template for the ribbon footer area |

## Events

| Event | Description |
| --- | --- |
| `OnTabChanged` | Triggered when the active tab changes |
| `OnCollapsedChanged` | Triggered when the ribbon's collapsed state changes |
| `OnCommandExecuted` | Triggered when a ribbon command is executed |
| `OnGroupExpanded` | Triggered when a ribbon group is expanded |
| `OnGroupCollapsed` | Triggered when a ribbon group is collapsed |

## Usage Examples

### Example 1: Basic RibbonTab
```razor
@using BootstrapBlazor.Components

<RibbonTab>
    <Items>
        <RibbonTabItem Text="Home" Icon="fa-solid fa-home">
            <Groups>
                <RibbonGroup Text="Clipboard">
                    <Commands>
                        <RibbonCommand Text="Cut" Icon="fa-solid fa-scissors" OnClick="@(() => ExecuteCommand("Cut"))" />
                        <RibbonCommand Text="Copy" Icon="fa-solid fa-copy" OnClick="@(() => ExecuteCommand("Copy"))" />
                        <RibbonCommand Text="Paste" Icon="fa-solid fa-paste" OnClick="@(() => ExecuteCommand("Paste"))" />
                    </Commands>
                </RibbonGroup>
                <RibbonGroup Text="Font">
                    <Commands>
                        <RibbonCommand Text="Bold" Icon="fa-solid fa-bold" OnClick="@(() => ExecuteCommand("Bold"))" />
                        <RibbonCommand Text="Italic" Icon="fa-solid fa-italic" OnClick="@(() => ExecuteCommand("Italic"))" />
                        <RibbonCommand Text="Underline" Icon="fa-solid fa-underline" OnClick="@(() => ExecuteCommand("Underline"))" />
                    </Commands>
                </RibbonGroup>
            </Groups>
        </RibbonTabItem>
        <RibbonTabItem Text="Insert" Icon="fa-solid fa-plus">
            <Groups>
                <RibbonGroup Text="Tables">
                    <Commands>
                        <RibbonCommand Text="Table" Icon="fa-solid fa-table" OnClick="@(() => ExecuteCommand("Table"))" />
                    </Commands>
                </RibbonGroup>
                <RibbonGroup Text="Illustrations">
                    <Commands>
                        <RibbonCommand Text="Picture" Icon="fa-solid fa-image" OnClick="@(() => ExecuteCommand("Picture"))" />
                        <RibbonCommand Text="Chart" Icon="fa-solid fa-chart-bar" OnClick="@(() => ExecuteCommand("Chart"))" />
                    </Commands>
                </RibbonGroup>
            </Groups>
        </RibbonTabItem>
    </Items>
</RibbonTab>

@code {
    private void ExecuteCommand(string command)
    {
        Console.WriteLine($"Executed command: {command}");
    }
}
```

### Example 2: RibbonTab with Quick Access Toolbar
```razor
<RibbonTab ShowQuickAccessToolbar="true">
    <QuickAccessCommands>
        <RibbonCommand Text="Save" Icon="fa-solid fa-save" OnClick="@(() => ExecuteCommand("Save"))" />
        <RibbonCommand Text="Undo" Icon="fa-solid fa-undo" OnClick="@(() => ExecuteCommand("Undo"))" />
        <RibbonCommand Text="Redo" Icon="fa-solid fa-redo" OnClick="@(() => ExecuteCommand("Redo"))" />
    </QuickAccessCommands>
    <Items>
        <RibbonTabItem Text="Home" Icon="fa-solid fa-home">
            <Groups>
                <RibbonGroup Text="Clipboard">
                    <Commands>
                        <RibbonCommand Text="Cut" Icon="fa-solid fa-scissors" OnClick="@(() => ExecuteCommand("Cut"))" />
                        <RibbonCommand Text="Copy" Icon="fa-solid fa-copy" OnClick="@(() => ExecuteCommand("Copy"))" />
                        <RibbonCommand Text="Paste" Icon="fa-solid fa-paste" OnClick="@(() => ExecuteCommand("Paste"))" />
                    </Commands>
                </RibbonGroup>
            </Groups>
        </RibbonTabItem>
    </Items>
</RibbonTab>
```

### Example 3: Collapsible RibbonTab
```razor
<RibbonTab IsCollapsible="true" IsCollapsed="false" OnCollapsedChanged="@HandleCollapsedChanged">
    <Items>
        <RibbonTabItem Text="Home">
            <Groups>
                <RibbonGroup Text="Editing">
                    <Commands>
                        <RibbonCommand Text="Find" Icon="fa-solid fa-search" />
                        <RibbonCommand Text="Replace" Icon="fa-solid fa-exchange-alt" />
                        <RibbonCommand Text="Select All" Icon="fa-solid fa-object-group" />
                    </Commands>
                </RibbonGroup>
            </Groups>
        </RibbonTabItem>
    </Items>
</RibbonTab>

@code {
    private void HandleCollapsedChanged(bool isCollapsed)
    {
        Console.WriteLine($"Ribbon collapsed state changed to: {isCollapsed}");
    }
}
```

### Example 4: RibbonTab with Large and Small Commands
```razor
<RibbonTab>
    <Items>
        <RibbonTabItem Text="Home">
            <Groups>
                <RibbonGroup Text="Document">
                    <Commands>
                        <RibbonCommand Text="New" Icon="fa-solid fa-file" Size="CommandSize.Large" />
                        <RibbonCommand Text="Open" Icon="fa-solid fa-folder-open" Size="CommandSize.Large" />
                        <RibbonCommand Text="Save" Icon="fa-solid fa-save" Size="CommandSize.Large" />
                        <RibbonCommand Text="Print" Icon="fa-solid fa-print" Size="CommandSize.Small" />
                        <RibbonCommand Text="Preview" Icon="fa-solid fa-eye" Size="CommandSize.Small" />
                        <RibbonCommand Text="Share" Icon="fa-solid fa-share-alt" Size="CommandSize.Small" />
                    </Commands>
                </RibbonGroup>
            </Groups>
        </RibbonTabItem>
    </Items>
</RibbonTab>
```

### Example 5: RibbonTab with Contextual Tabs
```razor
<RibbonTab @bind-ActiveTab="ActiveTab">
    <Items>
        <RibbonTabItem Text="Home" Key="home">
            <Groups>
                <RibbonGroup Text="Clipboard">
                    <Commands>
                        <RibbonCommand Text="Cut" Icon="fa-solid fa-scissors" />
                        <RibbonCommand Text="Copy" Icon="fa-solid fa-copy" />
                        <RibbonCommand Text="Paste" Icon="fa-solid fa-paste" />
                    </Commands>
                </RibbonGroup>
            </Groups>
        </RibbonTabItem>
    </Items>
    <ContextualTabs>
        <RibbonContextualTabGroup Title="Table Tools" Color="#4472C4" Visible="@ShowTableTools">
            <Tabs>
                <RibbonTabItem Text="Design" Key="tableDesign">
                    <Groups>
                        <RibbonGroup Text="Table Styles">
                            <Commands>
                                <RibbonCommand Text="Header Row" Type="CommandType.Toggle" IsChecked="true" />
                                <RibbonCommand Text="Total Row" Type="CommandType.Toggle" />
                                <RibbonCommand Text="Banded Rows" Type="CommandType.Toggle" IsChecked="true" />
                            </Commands>
                        </RibbonGroup>
                    </Groups>
                </RibbonTabItem>
                <RibbonTabItem Text="Layout" Key="tableLayout">
                    <Groups>
                        <RibbonGroup Text="Table">
                            <Commands>
                                <RibbonCommand Text="Delete" Icon="fa-solid fa-trash" />
                                <RibbonCommand Text="Insert Above" Icon="fa-solid fa-arrow-up" />
                                <RibbonCommand Text="Insert Below" Icon="fa-solid fa-arrow-down" />
                            </Commands>
                        </RibbonGroup>
                    </Groups>
                </RibbonTabItem>
            </Tabs>
        </RibbonContextualTabGroup>
    </ContextualTabs>
</RibbonTab>

<Button OnClick="@(() => ShowTableTools = !ShowTableTools)">
    @(ShowTableTools ? "Hide" : "Show") Table Tools
</Button>

@code {
    private string ActiveTab { get; set; } = "home";
    private bool ShowTableTools { get; set; } = false;
}
```

### Example 6: RibbonTab with Split Buttons and Dropdown Menus
```razor
<RibbonTab>
    <Items>
        <RibbonTabItem Text="Home">
            <Groups>
                <RibbonGroup Text="Styles">
                    <Commands>
                        <RibbonCommand Text="Styles" Icon="fa-solid fa-paint-brush" Type="CommandType.SplitButton">
                            <ChildCommands>
                                <RibbonCommand Text="Normal" OnClick="@(() => ApplyStyle("Normal"))" />
                                <RibbonCommand Text="No Spacing" OnClick="@(() => ApplyStyle("No Spacing"))" />
                                <RibbonCommand Text="Heading 1" OnClick="@(() => ApplyStyle("Heading 1"))" />
                                <RibbonCommand Text="Heading 2" OnClick="@(() => ApplyStyle("Heading 2"))" />
                                <RibbonCommand Text="Title" OnClick="@(() => ApplyStyle("Title"))" />
                                <RibbonCommand Text="Subtitle" OnClick="@(() => ApplyStyle("Subtitle"))" />
                            </ChildCommands>
                        </RibbonCommand>
                        <RibbonCommand Text="Format" Icon="fa-solid fa-font" Type="CommandType.DropDown">
                            <ChildCommands>
                                <RibbonCommand Text="Bold" Icon="fa-solid fa-bold" OnClick="@(() => FormatText("Bold"))" />
                                <RibbonCommand Text="Italic" Icon="fa-solid fa-italic" OnClick="@(() => FormatText("Italic"))" />
                                <RibbonCommand Text="Underline" Icon="fa-solid fa-underline" OnClick="@(() => FormatText("Underline"))" />
                                <RibbonCommand Text="Strikethrough" Icon="fa-solid fa-strikethrough" OnClick="@(() => FormatText("Strikethrough"))" />
                            </ChildCommands>
                        </RibbonCommand>
                    </Commands>
                </RibbonGroup>
            </Groups>
        </RibbonTabItem>
    </Items>
</RibbonTab>

@code {
    private void ApplyStyle(string style)
    {
        Console.WriteLine($"Applied style: {style}");
    }
    
    private void FormatText(string format)
    {
        Console.WriteLine($"Applied format: {format}");
    }
}
```

### Example 7: RibbonTab with Custom Templates
```razor
<RibbonTab>
    <HeaderTemplate>
        <div class="ribbon-custom-header">
            <h1>Document Editor</h1>
            <div class="user-info">
                <span>@UserName</span>
                <Avatar Icon="fa-solid fa-user" />
            </div>
        </div>
    </HeaderTemplate>
    
    <Items>
        <RibbonTabItem Text="Home">
            <Groups>
                <RibbonGroup Text="Font">
                    <GroupTemplate>
                        <div class="custom-group-template">
                            <h4>@context.Text</h4>
                            <div class="font-controls">
                                <Select TValue="string" Items="@FontFamilies" @bind-Value="SelectedFont" />
                                <InputNumber TValue="int" @bind-Value="FontSize" Min="8" Max="72" Step="1" />
                                <div class="font-buttons">
                                    <Button Icon="fa-solid fa-bold" IsOutline="true" @onclick="() => ToggleFormat("Bold")" />
                                    <Button Icon="fa-solid fa-italic" IsOutline="true" @onclick="() => ToggleFormat("Italic")" />
                                    <Button Icon="fa-solid fa-underline" IsOutline="true" @onclick="() => ToggleFormat("Underline")" />
                                </div>
                            </div>
                        </div>
                    </GroupTemplate>
                </RibbonGroup>
                
                <RibbonGroup Text="Paragraph">
                    <Commands>
                        <RibbonCommand Text="Align Left" Icon="fa-solid fa-align-left" />
                        <RibbonCommand Text="Align Center" Icon="fa-solid fa-align-center" />
                        <RibbonCommand Text="Align Right" Icon="fa-solid fa-align-right" />
                        <RibbonCommand Text="Justify" Icon="fa-solid fa-align-justify" />
                    </Commands>
                </RibbonGroup>
            </Groups>
        </RibbonTabItem>
    </Items>
    
    <FooterTemplate>
        <div class="ribbon-custom-footer">
            <span>Document Status: @DocumentStatus</span>
            <span>Last Saved: @LastSaved</span>
        </div>
    </FooterTemplate>
</RibbonTab>

@code {
    private string UserName { get; set; } = "John Doe";
    private string DocumentStatus { get; set; } = "Ready";
    private DateTime LastSaved { get; set; } = DateTime.Now.AddMinutes(-5);
    private string SelectedFont { get; set; } = "Arial";
    private int FontSize { get; set; } = 12;
    private List<string> FontFamilies { get; set; } = new List<string> { "Arial", "Times New Roman", "Calibri", "Verdana", "Courier New" };
    
    private void ToggleFormat(string format)
    {
        Console.WriteLine($"Toggled format: {format}");
    }
}
```

## CSS Customization

The RibbonTab component can be customized using CSS variables:

```css
/* RibbonTab custom styling */
.ribbon-tab {
  --bb-ribbon-background: #f3f2f1;
  --bb-ribbon-border-color: #d6d6d6;
  --bb-ribbon-tab-active-color: #2b579a;
  --bb-ribbon-tab-hover-background: #e6e6e6;
  --bb-ribbon-group-border-color: #c8c8c8;
  --bb-ribbon-command-hover-background: #d8d8d8;
  --bb-ribbon-command-active-background: #c0c0c0;
  --bb-ribbon-quick-access-background: #333333;
  --bb-ribbon-quick-access-color: #ffffff;
}

/* Custom styling for contextual tab groups */
.ribbon-contextual-group {
  --bb-ribbon-contextual-background-opacity: 0.1;
}

/* Custom styling for ribbon commands */
.ribbon-command {
  --bb-ribbon-command-icon-size: 1.5rem;
  --bb-ribbon-command-large-icon-size: 2rem;
}
```

## Notes

### Accessibility
- The RibbonTab component supports keyboard navigation for accessibility.
- Use ARIA attributes for better screen reader support.
- Ensure sufficient color contrast for all UI elements.

### Performance
- For applications with many ribbon commands, consider lazy loading tab content.
- Use the `IsLazyLoad` property to defer loading of tab content until the tab is activated.

### Mobile Considerations
- The RibbonTab component adapts to smaller screens by collapsing groups and simplifying the UI.
- Consider using the `IsCollapsed` property by default on mobile devices.
- Test the responsive behavior on various screen sizes.

### Integration with Other Components
- The RibbonTab component works well with other BootstrapBlazor components like Dropdown, Button, and Input.
- Use the `CommandTemplate` to integrate custom components within ribbon commands.

### Best Practices
- Group related commands logically within ribbon groups.
- Use icons consistently to provide visual cues for commands.
- Place frequently used commands in the Quick Access Toolbar.
- Consider using contextual tabs for context-specific functionality.
- Provide tooltips for all commands to improve usability.