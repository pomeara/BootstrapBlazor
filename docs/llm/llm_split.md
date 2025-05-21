# Split Component

## Overview

The Split component in BootstrapBlazor provides a flexible and interactive way to create resizable split panes within your application. It allows users to divide screen real estate between two content areas with a draggable divider, enabling them to adjust the space allocation according to their needs. This component is particularly useful for creating side-by-side views, code editors, file explorers, or any interface where users benefit from controlling the layout proportions.

## Features

- **Resizable Panes**: Drag the divider to adjust the size of each pane
- **Horizontal or Vertical Splitting**: Support for both horizontal and vertical orientation
- **Collapsible Panes**: Optional buttons to collapse either pane completely
- **Minimum Size Constraints**: Set minimum sizes for each pane to prevent them from becoming too small
- **Custom Styling**: Configurable appearance for the divider bar and handles
- **Size Preservation**: Option to maintain original sizes when expanding collapsed panes
- **Responsive Events**: Callbacks for resize and collapse actions
- **Programmatic Control**: Methods to set pane sizes via code
- **Accessibility Support**: Keyboard navigation and ARIA attributes for better accessibility

## Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `IsCollapsible` | `bool` | `false` | Enables collapse functionality with arrow buttons on the divider. |
| `ShowBarHandle` | `bool` | `true` | Determines whether to show the drag handle on the divider bar. |
| `IsKeepOriginalSize` | `bool` | `true` | When enabled, restores the original size when expanding a collapsed pane. |
| `IsVertical` | `bool` | `false` | When true, creates a vertical split (top/bottom); when false, creates a horizontal split (left/right). |
| `Basis` | `string` | `"50%"` | Sets the initial size of the first pane as a percentage or fixed value. |
| `FirstPaneTemplate` | `RenderFragment` | `null` | Content template for the first pane (left or top). |
| `FirstPaneMinimumSize` | `string` | `null` | Minimum size constraint for the first pane (supports px, %, em, rem). |
| `SecondPaneTemplate` | `RenderFragment` | `null` | Content template for the second pane (right or bottom). |
| `SecondPaneMinimumSize` | `string` | `null` | Minimum size constraint for the second pane (supports px, %, em, rem). |

## Events

| Event | Type | Description |
|-------|------|-------------|
| `OnResizedAsync` | `Func<SplitterResizedEventArgs, Task>` | Callback triggered when pane sizes change, providing the new size information. |

## Methods

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| `SetLeftWidth` | `string leftWidth` | `Task` | Sets the width of the left pane programmatically. The parameter can be a percentage or any valid CSS unit. |

## Usage Examples

### Example 1: Basic Horizontal Split

A simple horizontal split with two equal panes:

```razor
<Split>
    <FirstPaneTemplate>
        <div class="p-3">
            <h4>Left Pane</h4>
            <p>This content appears in the left pane.</p>
        </div>
    </FirstPaneTemplate>
    <SecondPaneTemplate>
        <div class="p-3">
            <h4>Right Pane</h4>
            <p>This content appears in the right pane.</p>
        </div>
    </SecondPaneTemplate>
</Split>
```

### Example 2: Vertical Split with Custom Initial Size

A vertical split with the top pane taking 30% of the space:

```razor
<div style="height: 400px;">
    <Split IsVertical="true" Basis="30%">
        <FirstPaneTemplate>
            <div class="p-3">
                <h4>Top Pane (30%)</h4>
                <p>This content appears in the top pane.</p>
            </div>
        </FirstPaneTemplate>
        <SecondPaneTemplate>
            <div class="p-3">
                <h4>Bottom Pane (70%)</h4>
                <p>This content appears in the bottom pane.</p>
            </div>
        </SecondPaneTemplate>
    </Split>
</div>
```

### Example 3: Collapsible Split with Minimum Sizes

A horizontal split with collapsible panes and minimum size constraints:

```razor
<Split IsCollapsible="true" FirstPaneMinimumSize="200px" SecondPaneMinimumSize="300px">
    <FirstPaneTemplate>
        <div class="p-3">
            <h4>Left Pane</h4>
            <p>This pane has a minimum width of 200px.</p>
            <p>Click the arrow buttons on the divider to collapse either pane.</p>
        </div>
    </FirstPaneTemplate>
    <SecondPaneTemplate>
        <div class="p-3">
            <h4>Right Pane</h4>
            <p>This pane has a minimum width of 300px.</p>
        </div>
    </SecondPaneTemplate>
</Split>
```

### Example 4: Split with Resize Event Handling

A split that responds to resize events:

```razor
@code {
    private string _currentSize = "50%";
    
    private async Task OnPaneResized(SplitterResizedEventArgs args)
    {
        _currentSize = args.LeftSize;
        StateHasChanged();
    }
}

<div>
    <p>Current left pane size: @_currentSize</p>
    
    <Split OnResizedAsync="OnPaneResized">
        <FirstPaneTemplate>
            <div class="p-3">
                <h4>Resizable Pane</h4>
                <p>Drag the divider to see the size update above.</p>
            </div>
        </FirstPaneTemplate>
        <SecondPaneTemplate>
            <div class="p-3">
                <h4>Second Pane</h4>
                <p>Content for the second pane.</p>
            </div>
        </SecondPaneTemplate>
    </Split>
</div>
```

### Example 5: Programmatically Controlling Split Size

Controlling the split size through code:

```razor
@code {
    private Split? _splitRef;
    
    private async Task SetSize(string size)
    {
        if (_splitRef != null)
        {
            await _splitRef.SetLeftWidth(size);
        }
    }
}

<div>
    <div class="mb-3">
        <Button OnClick="() => SetSize("25%")">25%</Button>
        <Button OnClick="() => SetSize("50%")">50%</Button>
        <Button OnClick="() => SetSize("75%")">75%</Button>
    </div>
    
    <Split @ref="_splitRef">
        <FirstPaneTemplate>
            <div class="p-3">
                <h4>Controlled Pane</h4>
                <p>Click the buttons above to change this pane's size.</p>
            </div>
        </FirstPaneTemplate>
        <SecondPaneTemplate>
            <div class="p-3">
                <h4>Second Pane</h4>
                <p>Content for the second pane.</p>
            </div>
        </SecondPaneTemplate>
    </Split>
</div>
```

### Example 6: Nested Splits for Complex Layouts

Creating a complex layout with nested splits:

```razor
<div style="height: 500px;">
    <Split Basis="70%">
        <FirstPaneTemplate>
            <Split IsVertical="true" Basis="60%">
                <FirstPaneTemplate>
                    <div class="p-3 bg-light">
                        <h4>Top Left</h4>
                        <p>This is the top-left section.</p>
                    </div>
                </FirstPaneTemplate>
                <SecondPaneTemplate>
                    <div class="p-3 bg-light">
                        <h4>Bottom Left</h4>
                        <p>This is the bottom-left section.</p>
                    </div>
                </SecondPaneTemplate>
            </Split>
        </FirstPaneTemplate>
        <SecondPaneTemplate>
            <div class="p-3 bg-light">
                <h4>Right</h4>
                <p>This is the right section.</p>
            </div>
        </SecondPaneTemplate>
    </Split>
</div>
```

### Example 7: Split in a Card Component

Integrating a Split component within a Card:

```razor
<Card>
    <HeaderTemplate>
        <h5>File Explorer</h5>
    </HeaderTemplate>
    <BodyTemplate>
        <div style="height: 400px;">
            <Split Basis="30%" IsCollapsible="true">
                <FirstPaneTemplate>
                    <div class="p-2">
                        <h6>Folders</h6>
                        <ul class="list-group">
                            <li class="list-group-item">Documents</li>
                            <li class="list-group-item">Images</li>
                            <li class="list-group-item">Videos</li>
                        </ul>
                    </div>
                </FirstPaneTemplate>
                <SecondPaneTemplate>
                    <div class="p-2">
                        <h6>Files</h6>
                        <ul class="list-group">
                            <li class="list-group-item">Report.docx</li>
                            <li class="list-group-item">Presentation.pptx</li>
                            <li class="list-group-item">Budget.xlsx</li>
                        </ul>
                    </div>
                </SecondPaneTemplate>
            </Split>
        </div>
    </BodyTemplate>
</Card>
```

## Customization Notes

### CSS Variables

The Split component uses CSS variables for styling, which can be customized to match your application's theme:

```css
.split {
    --bb-split-bar-bg: #f0f0f0;                /* Divider bar background color */
    --bb-split-bar-hover-bg: #007bff;           /* Divider bar hover background color */
    --bb-split-bar-width: 8px;                  /* Width of the divider bar */
    --bb-split-bar-handle-bg: #e0e0e0;          /* Handle background color */
    --bb-split-bar-handle-color: #a0a0a0;       /* Handle color */
    --bb-split-bar-handle-hover-bg: #ffffff;    /* Handle hover background color */
    --bb-split-bar-handle-hover-color: #ffffff; /* Handle hover color */
    --bb-split-bar-arrow-bg: #ffffff;           /* Arrow button background color */
    --bb-split-bar-arrow-border-color: #d0d0d0; /* Arrow button border color */
    --bb-split-bar-arrow-hover-bg: #007bff;     /* Arrow button hover background color */
    --bb-split-bar-arrow-hover-border-color: #0069d9; /* Arrow button hover border color */
}
```

For dark theme support, additional variables are provided:

```css
[data-bs-theme='dark'] .split {
    --bb-split-bar-bg: #2d3035;                 /* Dark theme divider background */
    --bb-split-bar-handle-bg: #3a3f44;          /* Dark theme handle background */
    --bb-split-bar-handle-color: #6c757d;       /* Dark theme handle color */
    --bb-split-bar-handle-hover-bg: #007bff;    /* Dark theme handle hover background */
    --bb-split-bar-handle-hover-color: #ffffff; /* Dark theme handle hover color */
    --bb-split-bar-arrow-hover-border-color: #0069d9; /* Dark theme arrow hover border */
}
```

### Integration with Other Components

The Split component works well with other BootstrapBlazor components:

- **Card**: Place the Split inside a Card to create panels with headers and footers
- **Tabs**: Use Split within Tabs for complex multi-panel interfaces
- **Tree**: Combine with Tree component to create file explorer-like interfaces
- **Table**: Create side-by-side master-detail views with Table in one pane
- **Editor**: Build code editor interfaces with syntax highlighting in split panes

### Best Practices

1. **Set a Fixed Height**: Always set a height on the container of the Split component, especially for vertical splits
2. **Consider Minimum Sizes**: Set appropriate minimum sizes to prevent panes from becoming too small to be usable
3. **Use Percentage-Based Sizing**: For responsive layouts, use percentage values for the `Basis` property
4. **Limit Nesting Depth**: While nested splits are powerful, limit nesting to avoid performance issues
5. **Test on Different Devices**: Ensure your split layouts work well on various screen sizes
6. **Provide Collapse Options**: For complex interfaces, enable the collapsible feature to let users focus on specific content
7. **Handle Resize Events**: Update dependent components or calculations when panes are resized