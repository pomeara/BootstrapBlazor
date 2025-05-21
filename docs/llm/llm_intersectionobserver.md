# IntersectionObserver Component

## Overview
The IntersectionObserver component in BootstrapBlazor provides a wrapper around the browser's Intersection Observer API, allowing developers to efficiently detect when an element enters or exits the viewport. This component is particularly useful for implementing lazy loading, infinite scrolling, visibility-dependent animations, and tracking element visibility for analytics purposes, all without affecting the main thread performance.

## Features
- **Viewport Intersection Detection**: Monitor when elements enter or exit the viewport
- **Lazy Loading Support**: Efficiently load images, videos, or other content only when they become visible
- **Infinite Scrolling**: Trigger content loading when a sentinel element becomes visible
- **Customizable Thresholds**: Configure at what percentage of visibility the callbacks should trigger
- **Root Element Specification**: Observe intersections relative to a specific container instead of the viewport
- **Margin Configuration**: Add margins to expand or shrink the effective root element boundaries
- **Multiple Target Support**: Observe multiple elements with a single observer instance
- **Performance Optimization**: Uses the browser's native IntersectionObserver API for efficient detection without scroll event listeners
- **Callback Events**: Provides events for intersection entry and exit
- **Visibility Ratio**: Reports the exact percentage of the target element that is visible
- **Asynchronous Operation**: Works without blocking the main thread

## Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `ChildContent` | RenderFragment | null | Content to be observed for intersection with the viewport or root element |
| `Root` | ElementReference | null | The element that is used as the viewport for checking visibility. If null, defaults to the browser viewport |
| `RootSelector` | string | null | CSS selector for the root element. Alternative to Root property |
| `RootMargin` | string | "0px" | Margin around the root element. Values are similar to CSS margin property (e.g., "10px 20px 30px 40px") |
| `Thresholds` | double[] | [0] | List of threshold(s) at which to trigger callback. Each threshold is a ratio of intersection area to total bounding box area (0.0 to 1.0) |
| `Enabled` | bool | true | Whether the observer is active |
| `Once` | bool | false | If true, stops observing after the first intersection |
| `ElementSelector` | string | null | CSS selector to identify which child elements to observe. If null, observes the component's root element |
| `ObserveChildren` | bool | false | If true, observes all child elements instead of the component's root element |
| `DisconnectOnDispose` | bool | true | Whether to disconnect the observer when the component is disposed |
| `InitialDelay` | int | 0 | Delay in milliseconds before starting observation after initialization |

## Events

| Event | Description |
|-------|-------------|
| `OnIntersecting` | Triggered when an observed element intersects with the root element or viewport |
| `OnIntersectionChanged` | Triggered when the intersection state of an observed element changes |
| `OnEnterViewport` | Triggered when an observed element enters the viewport or root element |
| `OnExitViewport` | Triggered when an observed element exits the viewport or root element |
| `OnInitialized` | Triggered when the IntersectionObserver is initialized |

## Usage Examples

### Example 1: Basic Lazy Loading of Images
```razor
<IntersectionObserver OnIntersecting="HandleIntersection">
    <div class="lazy-image-container">
        @if (imageLoaded)
        {
            <img src="@imageSrc" alt="Lazy loaded image" />
        }
        else
        {
            <div class="placeholder">Loading...</div>
        }
    </div>
</IntersectionObserver>

@code {
    private bool imageLoaded = false;
    private string imageSrc = "https://example.com/large-image.jpg";
    
    private void HandleIntersection(IntersectionObserverEventArgs args)
    {
        if (args.IsIntersecting && !imageLoaded)
        {
            imageLoaded = true;
            StateHasChanged();
        }
    }
}
```

### Example 2: Infinite Scrolling
```razor
<div class="infinite-scroll-container">
    @foreach (var item in items)
    {
        <div class="item">
            <h3>@item.Title</h3>
            <p>@item.Description</p>
        </div>
    }
    
    <IntersectionObserver OnIntersecting="LoadMoreItems">
        <div class="loading-sentinel">@(isLoading ? "Loading more items..." : "")</div>
    </IntersectionObserver>
</div>

@code {
    private List<ItemModel> items = new List<ItemModel>();
    private bool isLoading = false;
    private int currentPage = 1;
    private bool hasMoreItems = true;
    
    protected override async Task OnInitializedAsync()
    {
        await LoadInitialItems();
    }
    
    private async Task LoadInitialItems()
    {
        items = await FetchItems(1);
    }
    
    private async void LoadMoreItems(IntersectionObserverEventArgs args)
    {
        if (args.IsIntersecting && !isLoading && hasMoreItems)
        {
            try
            {
                isLoading = true;
                StateHasChanged();
                
                currentPage++;
                var newItems = await FetchItems(currentPage);
                
                if (newItems.Count > 0)
                {
                    items.AddRange(newItems);
                }
                else
                {
                    hasMoreItems = false;
                }
            }
            finally
            {
                isLoading = false;
                StateHasChanged();
            }
        }
    }
    
    private async Task<List<ItemModel>> FetchItems(int page)
    {
        // Simulate API call with delay
        await Task.Delay(1000);
        
        // Return 10 items per page, up to 5 pages
        if (page <= 5)
        {
            return Enumerable.Range((page - 1) * 10 + 1, 10)
                .Select(i => new ItemModel
                {
                    Id = i,
                    Title = $"Item {i}",
                    Description = $"This is the description for item {i}"
                })
                .ToList();
        }
        
        return new List<ItemModel>();
    }
    
    private class ItemModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
```

### Example 3: Animate Elements on Scroll
```razor
<div class="scroll-animation-container">
    @for (int i = 1; i <= 10; i++)
    {
        var index = i;
        <IntersectionObserver OnIntersecting="args => HandleAnimation(args, index)" Thresholds="new[] { 0.1 }">
            <div class="animate-item @(animatedItems.Contains(index) ? "animated" : "")">
                <h3>Section @index</h3>
                <p>This section will animate when it enters the viewport.</p>
            </div>
        </IntersectionObserver>
    }
</div>

<style>
    .animate-item {
        opacity: 0;
        transform: translateY(20px);
        transition: opacity 0.5s ease, transform 0.5s ease;
    }
    
    .animate-item.animated {
        opacity: 1;
        transform: translateY(0);
    }
</style>

@code {
    private HashSet<int> animatedItems = new HashSet<int>();
    
    private void HandleAnimation(IntersectionObserverEventArgs args, int itemIndex)
    {
        if (args.IsIntersecting && !animatedItems.Contains(itemIndex))
        {
            animatedItems.Add(itemIndex);
            StateHasChanged();
        }
    }
}
```

### Example 4: Visibility Tracking for Analytics
```razor
@inject IJSRuntime JSRuntime

<div class="analytics-tracking-container">
    <h2>Product Sections</h2>
    
    <IntersectionObserver OnIntersecting="args => TrackVisibility(args, "header")" Thresholds="new[] { 0.5 }" Once="true">
        <div class="product-section header-section">
            <h3>Featured Products</h3>
            <p>Check out our most popular items this month.</p>
        </div>
    </IntersectionObserver>
    
    <IntersectionObserver OnIntersecting="args => TrackVisibility(args, "electronics")" Thresholds="new[] { 0.5 }" Once="true">
        <div class="product-section electronics-section">
            <h3>Electronics</h3>
            <p>The latest gadgets and tech accessories.</p>
        </div>
    </IntersectionObserver>
    
    <IntersectionObserver OnIntersecting="args => TrackVisibility(args, "clothing")" Thresholds="new[] { 0.5 }" Once="true">
        <div class="product-section clothing-section">
            <h3>Clothing</h3>
            <p>Fashion items for all seasons.</p>
        </div>
    </IntersectionObserver>
    
    <IntersectionObserver OnIntersecting="args => TrackVisibility(args, "footer")" Thresholds="new[] { 0.5 }" Once="true">
        <div class="product-section footer-section">
            <h3>Special Offers</h3>
            <p>Limited time deals you don't want to miss.</p>
        </div>
    </IntersectionObserver>
</div>

@code {
    private async void TrackVisibility(IntersectionObserverEventArgs args, string sectionId)
    {
        if (args.IsIntersecting)
        {
            // In a real application, you would call your analytics service
            await JSRuntime.InvokeVoidAsync("console.log", $"Section viewed: {sectionId}");
            
            // Example of tracking with a hypothetical analytics service
            await JSRuntime.InvokeVoidAsync("analyticsService.trackSectionView", sectionId);
        }
    }
}
```

### Example 5: Custom Thresholds and Root Margins
```razor
<div class="threshold-demo-container" style="height: 400px; overflow-y: scroll; border: 1px solid #ccc; padding: 10px;" @ref="scrollContainer">
    <div style="height: 200px;">Scroll down to see the observed element</div>
    
    <IntersectionObserver 
        Root="scrollContainer"
        RootMargin="20px"
        Thresholds="new[] { 0.0, 0.25, 0.5, 0.75, 1.0 }"
        OnIntersectionChanged="HandleIntersectionChange">
        <div class="observed-element" style="height: 200px; background-color: @backgroundColor; transition: background-color 0.3s ease;">
            <h3>Intersection Ratio: @(Math.Round(intersectionRatio * 100, 0))%</h3>
            <p>Scroll to see the background color change based on visibility percentage</p>
        </div>
    </IntersectionObserver>
    
    <div style="height: 600px;">Additional content to enable scrolling</div>
</div>

@code {
    private ElementReference scrollContainer;
    private double intersectionRatio = 0;
    private string backgroundColor => GetColorForRatio(intersectionRatio);
    
    private void HandleIntersectionChange(IntersectionObserverEventArgs args)
    {
        intersectionRatio = args.IntersectionRatio;
        StateHasChanged();
    }
    
    private string GetColorForRatio(double ratio)
    {
        if (ratio < 0.25) return "#ffcccc"; // Light red
        if (ratio < 0.5) return "#ffffcc";  // Light yellow
        if (ratio < 0.75) return "#ccffcc"; // Light green
        return "#ccffff";                    // Light blue
    }
}
```

### Example 6: Multiple Element Observation
```razor
<div class="gallery-container">
    <IntersectionObserver 
        ObserveChildren="true" 
        ElementSelector=".gallery-item"
        OnIntersecting="HandleGalleryItemIntersection"
        Thresholds="new[] { 0.1 }">
        
        @for (int i = 1; i <= 20; i++)
        {
            var itemId = i;
            <div class="gallery-item @(visibleItems.Contains(itemId) ? "visible" : "")" data-item-id="@itemId">
                <div class="gallery-item-content">
                    <h4>Gallery Item @itemId</h4>
                    @if (visibleItems.Contains(itemId))
                    {
                        <img src="https://picsum.photos/id/@(itemId + 100)/300/200" alt="Gallery image @itemId" />
                    }
                    else
                    {
                        <div class="placeholder">Image will load when visible</div>
                    }
                </div>
            </div>
        }
    </IntersectionObserver>
</div>

<style>
    .gallery-container {
        display: grid;
        grid-template-columns: repeat(auto-fill, minmax(300px, 1fr));
        gap: 20px;
    }
    
    .gallery-item {
        opacity: 0;
        transform: scale(0.9);
        transition: opacity 0.5s ease, transform 0.5s ease;
    }
    
    .gallery-item.visible {
        opacity: 1;
        transform: scale(1);
    }
    
    .gallery-item-content {
        border: 1px solid #ddd;
        border-radius: 4px;
        padding: 15px;
        height: 250px;
    }
    
    .placeholder {
        background-color: #f0f0f0;
        height: 200px;
        display: flex;
        align-items: center;
        justify-content: center;
        color: #666;
    }
</style>

@code {
    private HashSet<int> visibleItems = new HashSet<int>();
    
    private void HandleGalleryItemIntersection(IntersectionObserverEventArgs args)
    {
        if (args.IsIntersecting && args.Target is ElementReference element)
        {
            // Extract the item ID from the data attribute
            var itemIdStr = args.Target.GetAttribute("data-item-id");
            if (int.TryParse(itemIdStr, out int itemId))
            {
                visibleItems.Add(itemId);
                StateHasChanged();
            }
        }
    }
}
```

### Example 7: Delayed Initialization and Cleanup
```razor
@implements IDisposable

<div class="delayed-observer-demo">
    <div class="controls">
        <Button Text="Initialize Observer" OnClick="InitializeObserver" Disabled="isObserverInitialized" />
        <Button Text="Disconnect Observer" OnClick="DisconnectObserver" Disabled="!isObserverInitialized" />
        <div class="status">Observer Status: @(isObserverInitialized ? "Active" : "Inactive")</div>
    </div>
    
    <div class="scroll-container" style="height: 400px; overflow-y: auto; border: 1px solid #ddd; margin-top: 20px;">
        <div style="height: 200px;">Scroll down to test the observer</div>
        
        @if (showObserver)
        {
            <IntersectionObserver 
                @ref="observer"
                Enabled="isObserverEnabled"
                InitialDelay="2000"
                DisconnectOnDispose="false"
                OnInitialized="HandleObserverInitialized"
                OnIntersecting="HandleIntersection">
                <div class="target-element" style="height: 200px; background-color: @(isIntersecting ? "#d4edda" : "#f8d7da"); transition: background-color 0.3s ease; padding: 20px;">
                    <h3>Observer Test Element</h3>
                    <p>Current state: @(isIntersecting ? "Visible" : "Not visible")</p>
                    <p>Intersection count: @intersectionCount</p>
                </div>
            </IntersectionObserver>
        }
        
        <div style="height: 600px;">Additional content for scrolling</div>
    </div>
    
    <div class="log-container" style="margin-top: 20px; border: 1px solid #ddd; padding: 10px; max-height: 200px; overflow-y: auto;">
        <h4>Event Log:</h4>
        <ul>
            @foreach (var logEntry in eventLog)
            {
                <li>@logEntry</li>
            }
        </ul>
    </div>
</div>

@code {
    private IntersectionObserver observer;
    private bool showObserver = true;
    private bool isObserverInitialized = false;
    private bool isObserverEnabled = false;
    private bool isIntersecting = false;
    private int intersectionCount = 0;
    private List<string> eventLog = new List<string>();
    
    private void InitializeObserver()
    {
        AddLogEntry("Initializing observer with 2 second delay...");
        isObserverEnabled = true;
        StateHasChanged();
    }
    
    private void DisconnectObserver()
    {
        if (observer != null)
        {
            AddLogEntry("Disconnecting observer...");
            observer.Disconnect();
            isObserverInitialized = false;
            isObserverEnabled = false;
            StateHasChanged();
        }
    }
    
    private void HandleObserverInitialized()
    {
        isObserverInitialized = true;
        AddLogEntry("Observer initialized successfully");
        StateHasChanged();
    }
    
    private void HandleIntersection(IntersectionObserverEventArgs args)
    {
        isIntersecting = args.IsIntersecting;
        if (isIntersecting)
        {
            intersectionCount++;
            AddLogEntry($"Element intersected (Count: {intersectionCount})");
        }
        else
        {
            AddLogEntry("Element exited viewport");
        }
        StateHasChanged();
    }
    
    private void AddLogEntry(string message)
    {
        eventLog.Insert(0, $"[{DateTime.Now.ToString("HH:mm:ss")}] {message}");
        
        // Keep log size manageable
        if (eventLog.Count > 50)
        {
            eventLog.RemoveAt(eventLog.Count - 1);
        }
    }
    
    public void Dispose()
    {
        // The observer will not automatically disconnect due to DisconnectOnDispose="false"
        // We need to manually disconnect it
        observer?.Disconnect();
    }
}
```

## CSS Customization

The IntersectionObserver component doesn't have specific CSS styling as it's primarily a functional component. However, you can style the elements being observed using standard CSS classes and transitions for animation effects when elements enter or exit the viewport.

```css
/* Example of styling for elements that will be observed */
.observed-element {
    transition: opacity 0.5s ease, transform 0.5s ease;
    opacity: 0;
    transform: translateY(20px);
}

.observed-element.visible {
    opacity: 1;
    transform: translateY(0);
}

/* Different animation styles for different elements */
.fade-in {
    transition: opacity 0.8s ease;
    opacity: 0;
}

.fade-in.visible {
    opacity: 1;
}

.slide-in-left {
    transition: transform 0.5s ease;
    transform: translateX(-50px);
    opacity: 0;
}

.slide-in-left.visible {
    transform: translateX(0);
    opacity: 1;
}

.scale-in {
    transition: transform 0.5s ease, opacity 0.5s ease;
    transform: scale(0.8);
    opacity: 0;
}

.scale-in.visible {
    transform: scale(1);
    opacity: 1;
}
```

## JavaScript Interop

The IntersectionObserver component uses JavaScript interop to interact with the browser's native IntersectionObserver API. Here's an example of the JavaScript code that might be used internally:

```javascript
// This is a simplified example of the JavaScript interop used by the component
window.bootstrapBlazorIntersectionObserver = {
    observers: {},
    
    // Create a new IntersectionObserver instance
    create: function (id, dotNetReference, options) {
        if (this.observers[id]) {
            this.disconnect(id);
        }
        
        const observer = new IntersectionObserver((entries) => {
            entries.forEach(entry => {
                // Call back to .NET with intersection data
                dotNetReference.invokeMethodAsync('OnIntersectionChanged', {
                    target: entry.target,
                    isIntersecting: entry.isIntersecting,
                    intersectionRatio: entry.intersectionRatio,
                    boundingClientRect: entry.boundingClientRect,
                    intersectionRect: entry.intersectionRect,
                    rootBounds: entry.rootBounds,
                    time: entry.time
                });
            });
        }, options);
        
        this.observers[id] = {
            instance: observer,
            targets: []
        };
        
        return true;
    },
    
    // Observe a target element
    observe: function (id, element) {
        const observer = this.observers[id];
        if (!observer) return false;
        
        observer.instance.observe(element);
        observer.targets.push(element);
        return true;
    },
    
    // Observe multiple elements matching a selector within a container
    observeMultiple: function (id, container, selector) {
        const observer = this.observers[id];
        if (!observer) return false;
        
        const elements = container.querySelectorAll(selector);
        elements.forEach(element => {
            observer.instance.observe(element);
            observer.targets.push(element);
        });
        
        return elements.length;
    },
    
    // Stop observing a specific target
    unobserve: function (id, element) {
        const observer = this.observers[id];
        if (!observer) return false;
        
        observer.instance.unobserve(element);
        observer.targets = observer.targets.filter(t => t !== element);
        return true;
    },
    
    // Disconnect the observer and stop observing all targets
    disconnect: function (id) {
        const observer = this.observers[id];
        if (!observer) return false;
        
        observer.instance.disconnect();
        delete this.observers[id];
        return true;
    }
};
```

## Accessibility

The IntersectionObserver component itself doesn't directly impact accessibility, but it can be used to implement accessible features:

- Use it to implement progressive loading that doesn't interfere with screen readers
- Ensure that any animations triggered by intersection events don't violate user preferences (respect `prefers-reduced-motion` media query)
- When implementing lazy loading, include appropriate `alt` text for images even before they're loaded
- Consider providing fallback content for users with JavaScript disabled

## Browser Compatibility

The IntersectionObserver API is supported in all modern browsers. For older browsers that don't support the API natively, the component can include a polyfill to ensure functionality across different browser versions.

## Integration with Other Components

The IntersectionObserver component works well with many other BootstrapBlazor components:

- Use with `Image` component for lazy loading
- Combine with `Animation` component for scroll-triggered animations
- Use with `Table` or `List` components for infinite scrolling data loading
- Integrate with `Card` components for revealing content as users scroll
- Use with `Chart` components to load and render charts only when they become visible