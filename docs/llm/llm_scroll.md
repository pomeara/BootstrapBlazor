# Scroll Component

## Overview
The Scroll component in BootstrapBlazor provides a customizable scrolling container with enhanced scrolling behavior, including smooth scrolling, virtual scrolling for large datasets, and scroll position tracking. It offers a modern alternative to native browser scrolling with additional features and consistent cross-browser behavior.

## Features
- **Customizable Scrollbars**: Style and customize scrollbar appearance
- **Smooth Scrolling**: Enhanced smooth scrolling behavior
- **Virtual Scrolling**: Efficiently render large datasets by only rendering visible items
- **Scroll Position Tracking**: Monitor and react to scroll position changes
- **Programmatic Scrolling**: Methods to scroll to specific positions or elements
- **Scroll Direction Control**: Ability to enable/disable scrolling in specific directions
- **Auto-scrolling**: Automatic scrolling capabilities for content updates
- **Scroll Events**: Rich event system for scroll interactions

## Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `Height` | string | "" | Sets the height of the scroll container |
| `Width` | string | "" | Sets the width of the scroll container |
| `ChildContent` | RenderFragment | null | Content to be displayed inside the scroll container |
| `IsVertical` | bool | true | Enables vertical scrolling |
| `IsHorizontal` | bool | false | Enables horizontal scrolling |
| `ShowScrollbar` | bool | true | Controls the visibility of scrollbars |
| `ScrollbarHoverShow` | bool | false | Shows scrollbars only on hover |
| `ScrollbarAutoHide` | bool | false | Automatically hides scrollbars after a period of inactivity |
| `ScrollbarAutoHideDelay` | int | 1000 | Delay in milliseconds before scrollbars auto-hide |
| `ScrollbarThumbColor` | string | "" | Color of the scrollbar thumb |
| `ScrollbarTrackColor` | string | "" | Color of the scrollbar track |
| `ScrollbarThickness` | int | 6 | Thickness of the scrollbar in pixels |
| `SmoothScroll` | bool | true | Enables smooth scrolling animation |
| `SmoothScrollDuration` | int | 300 | Duration of smooth scrolling animation in milliseconds |
| `VirtualizeItems` | bool | false | Enables virtual scrolling for large datasets |
| `VirtualItemSize` | int | 50 | Height/width of each virtual item in pixels |
| `VirtualItemCount` | int | 0 | Total number of items for virtual scrolling |
| `VirtualItemBuffer` | int | 2 | Number of items to render outside the visible area |
| `ScrollToTopOnRefresh` | bool | false | Scrolls to top when content is refreshed |
| `DisableOnMobile` | bool | false | Disables custom scrolling on mobile devices |

## Events

| Event | Description |
|-------|-------------|
| `OnScroll` | Triggered when the user scrolls the container |
| `OnScrollStart` | Triggered when scrolling begins |
| `OnScrollEnd` | Triggered when scrolling ends |
| `OnScrollToTop` | Triggered when scrolled to the top |
| `OnScrollToBottom` | Triggered when scrolled to the bottom |
| `OnScrollToLeft` | Triggered when scrolled to the leftmost position |
| `OnScrollToRight` | Triggered when scrolled to the rightmost position |
| `OnVirtualItemsRendered` | Triggered when virtual items are rendered |

## Methods

| Method | Description |
|--------|-------------|
| `ScrollToAsync(double position, bool isVertical = true)` | Scrolls to a specific position |
| `ScrollToElementAsync(ElementReference element)` | Scrolls to bring a specific element into view |
| `ScrollToTopAsync()` | Scrolls to the top of the container |
| `ScrollToBottomAsync()` | Scrolls to the bottom of the container |
| `ScrollToLeftAsync()` | Scrolls to the leftmost position |
| `ScrollToRightAsync()` | Scrolls to the rightmost position |
| `RefreshAsync()` | Refreshes the scroll container |

## Usage Examples

### Example 1: Basic Scroll Container
```razor
<Scroll Height="300px" Width="100%">
    <div class="content-container">
        <p>This is a paragraph with a lot of content that will cause scrolling.</p>
        <!-- More content here -->
    </div>
</Scroll>
```

### Example 2: Custom Scrollbar Styling
```razor
<Scroll Height="400px"
       ScrollbarThumbColor="#007bff"
       ScrollbarTrackColor="#f0f0f0"
       ScrollbarThickness="8">
    <div class="custom-content">
        <!-- Content here -->
    </div>
</Scroll>
```

### Example 3: Virtual Scrolling for Large Lists
```razor
@code {
    private List<string> Items = Enumerable.Range(1, 10000).Select(i => $"Item {i}").ToList();
}

<Scroll Height="500px"
       VirtualizeItems="true"
       VirtualItemSize="40"
       VirtualItemCount="@Items.Count"
       VirtualItemBuffer="5">
    <Virtualize Items="@Items" Context="item">
        <div class="list-item" style="height: 40px;">
            @item
        </div>
    </Virtualize>
</Scroll>
```

### Example 4: Horizontal Scrolling
```razor
<Scroll Width="100%"
       Height="200px"
       IsVertical="false"
       IsHorizontal="true">
    <div class="horizontal-content" style="display: flex; width: 2000px;">
        @for (int i = 1; i <= 20; i++)
        {
            <div class="card" style="min-width: 200px; margin-right: 10px;">
                <div class="card-body">
                    <h5 class="card-title">Card @i</h5>
                    <p class="card-text">Some content for card @i</p>
                </div>
            </div>
        }
    </div>
</Scroll>
```

### Example 5: Scroll Events
```razor
@code {
    private string scrollPosition = "0";
    
    private void HandleScroll(ScrollEventArgs args)
    {
        scrollPosition = $"Vertical: {args.VerticalOffset}px, Horizontal: {args.HorizontalOffset}px";
    }
}

<div>Current Scroll Position: @scrollPosition</div>

<Scroll Height="300px" OnScroll="HandleScroll">
    <div style="height: 1000px; padding: 20px;">
        <h3>Scroll down to see the position change</h3>
        <!-- More content here -->
    </div>
</Scroll>
```

### Example 6: Programmatic Scrolling
```razor
@code {
    private Scroll scrollRef;
    
    private async Task ScrollToPosition()
    {
        await scrollRef.ScrollToAsync(300);
    }
    
    private async Task ScrollToBottom()
    {
        await scrollRef.ScrollToBottomAsync();
    }
}

<div>
    <Button OnClick="ScrollToPosition">Scroll to 300px</Button>
    <Button OnClick="ScrollToBottom">Scroll to Bottom</Button>
</div>

<Scroll @ref="scrollRef" Height="400px">
    <div style="height: 1000px; padding: 20px;">
        <!-- Content here -->
    </div>
</Scroll>
```

### Example 7: Auto-hide Scrollbars
```razor
<Scroll Height="350px"
       ScrollbarAutoHide="true"
       ScrollbarAutoHideDelay="1500"
       ScrollbarHoverShow="true">
    <div class="clean-interface">
        <h4>Clean Interface with Hidden Scrollbars</h4>
        <p>Scrollbars will appear when scrolling and disappear after 1.5 seconds of inactivity.</p>
        <!-- More content here -->
    </div>
</Scroll>
```

## CSS Customization

The Scroll component can be customized using CSS variables:

```css
/* Custom scrollbar styling */
.bb-scroll {
    --bb-scroll-thumb-color: #007bff;
    --bb-scroll-track-color: #f0f0f0;
    --bb-scroll-thumb-hover-color: #0056b3;
    --bb-scroll-thumb-radius: 4px;
}
```

## JavaScript Interop

The Scroll component uses JavaScript interop for smooth scrolling and virtual scrolling functionality. It initializes a JavaScript scroll handler that manages the scrolling behavior and virtual item rendering.

## Accessibility

The Scroll component maintains accessibility by ensuring keyboard navigation works within the scrollable area. It preserves focus management and ensures screen readers can properly announce content changes.

## Performance Considerations

For large datasets, always use the virtual scrolling feature by setting `VirtualizeItems="true"` to improve performance. Adjust the `VirtualItemBuffer` based on your specific needs - larger buffers provide smoother scrolling but consume more memory.

## Browser Compatibility

The Scroll component works across all modern browsers. For older browsers that don't support custom scrollbar styling, it gracefully falls back to native scrollbars.

## Integration with Other Components

The Scroll component can be combined with other BootstrapBlazor components:

- Use with `Table` for scrollable tables with fixed headers
- Combine with `Tree` or `TreeView` for scrollable hierarchical data
- Wrap `List` or `ListView` components for scrollable lists
- Use inside `Modal` or `Drawer` components for scrollable modal content