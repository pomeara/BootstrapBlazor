# LazyLoad Component

## Overview
The LazyLoad component in BootstrapBlazor provides a way to defer loading of content until it's needed, typically when it enters the viewport. This component is particularly useful for optimizing page load performance by delaying the loading of off-screen images, videos, iframes, or other resource-intensive content until the user scrolls to them. By implementing lazy loading, applications can reduce initial page load time, save bandwidth, and improve overall user experience.

## Features
- **Viewport-Based Loading**: Loads content only when it enters the viewport
- **Customizable Thresholds**: Configure at what percentage of visibility the content should load
- **Multiple Content Types**: Support for images, videos, iframes, and custom content
- **Placeholder Support**: Display placeholder content while the actual content is loading
- **Loading Indicators**: Optional loading spinners or custom loading states
- **Error Handling**: Fallback options when content fails to load
- **Customizable Offset**: Preload content before it enters the viewport
- **Animation Effects**: Smooth transitions when content appears
- **Event Callbacks**: Notifications for loading states and visibility changes
- **Performance Optimization**: Uses IntersectionObserver API for efficient detection
- **Responsive Design**: Adapts to different screen sizes and orientations

## Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `ChildContent` | RenderFragment | null | The content to be lazy loaded |
| `Placeholder` | RenderFragment | null | Content to display while the actual content is loading |
| `Threshold` | double | 0.0 | Percentage of the target's visibility the observer's callback should be executed (0.0 to 1.0) |
| `RootMargin` | string | "0px" | Margin around the root element (similar to CSS margin property) |
| `Enabled` | bool | true | Whether lazy loading is enabled |
| `LoadingTemplate` | RenderFragment | null | Custom template for loading state |
| `ErrorTemplate` | RenderFragment | null | Custom template for error state |
| `LoadDelay` | int | 0 | Delay in milliseconds before loading content after it becomes visible |
| `FadeIn` | bool | false | Whether to apply fade-in animation when content appears |
| `FadeInDuration` | int | 300 | Duration of fade-in animation in milliseconds |
| `RetryCount` | int | 0 | Number of retry attempts if loading fails |
| `RetryDelay` | int | 1000 | Delay between retry attempts in milliseconds |
| `LoadingText` | string | "Loading..." | Text to display during loading state |
| `ErrorText` | string | "Failed to load content" | Text to display when content fails to load |
| `OnceOnly` | bool | true | Whether to load the content only once or reload when it re-enters viewport |
| `PreloadOffset` | string | "0px" | Distance from the viewport at which to start preloading content |
| `LoadingClass` | string | "bb-lazy-loading" | CSS class applied during loading state |
| `LoadedClass` | string | "bb-lazy-loaded" | CSS class applied after content is loaded |
| `ErrorClass` | string | "bb-lazy-error" | CSS class applied when content fails to load |

## Events

| Event | Description |
|-------|-------------|
| `OnVisibilityChanged` | Triggered when the visibility of the lazy-loaded content changes |
| `OnLoading` | Triggered when the content starts loading |
| `OnLoaded` | Triggered when the content has successfully loaded |
| `OnError` | Triggered when the content fails to load |
| `OnRetry` | Triggered when a retry attempt is made after a loading failure |

## Usage Examples

### Example 1: Basic Image Lazy Loading
```razor
<div class="image-gallery">
    @for (int i = 1; i <= 20; i++)
    {
        <div class="image-container">
            <LazyLoad>
                <Placeholder>
                    <div class="placeholder-box">
                        <span>Loading image...</span>
                    </div>
                </Placeholder>
                <img src="https://picsum.photos/id/@(i + 100)/800/600" alt="Lazy loaded image @i" />
            </LazyLoad>
        </div>
    }
</div>

<style>
    .image-gallery {
        display: grid;
        grid-template-columns: repeat(auto-fill, minmax(300px, 1fr));
        gap: 20px;
    }
    
    .image-container {
        border: 1px solid #ddd;
        border-radius: 4px;
        overflow: hidden;
    }
    
    .placeholder-box {
        width: 100%;
        height: 200px;
        background-color: #f0f0f0;
        display: flex;
        align-items: center;
        justify-content: center;
        color: #666;
    }
    
    img {
        width: 100%;
        height: auto;
        display: block;
    }
</style>
```

### Example 2: Lazy Loading with Custom Loading and Error Templates
```razor
@code {
    private bool simulateError = false;
    
    private void ToggleErrorSimulation()
    {
        simulateError = !simulateError;
    }
}

<div class="controls mb-3">
    <Button Text="@(simulateError ? "Disable Error Simulation" : "Enable Error Simulation")" 
            OnClick="ToggleErrorSimulation" 
            Color="@(simulateError ? Color.Danger : Color.Primary)" />
</div>

<div class="lazy-load-demo">
    <LazyLoad FadeIn="true" 
              FadeInDuration="500"
              RetryCount="3"
              RetryDelay="2000">
        <LoadingTemplate>
            <div class="custom-loading">
                <Spinner Type="SpinnerType.Border" Color="Color.Primary" />
                <p>Loading your content...</p>
            </div>
        </LoadingTemplate>
        <ErrorTemplate>
            <div class="custom-error">
                <i class="fa-solid fa-exclamation-triangle text-danger"></i>
                <p>Oops! We couldn't load this content.</p>
                <Button Text="Try Again" Color="Color.Warning" Size="Size.Small" />
            </div>
        </ErrorTemplate>
        
        @if (simulateError)
        {
            <img src="https://non-existent-image-url.jpg" alt="This image will fail to load" />
        }
        else
        {
            <img src="https://picsum.photos/800/400" alt="Random image" />
        }
    </LazyLoad>
</div>

<style>
    .lazy-load-demo {
        border: 1px solid #ddd;
        border-radius: 4px;
        padding: 20px;
        max-width: 800px;
    }
    
    .custom-loading, .custom-error {
        height: 300px;
        display: flex;
        flex-direction: column;
        align-items: center;
        justify-content: center;
        text-align: center;
        background-color: #f8f9fa;
        border-radius: 4px;
    }
    
    .custom-loading p, .custom-error p {
        margin-top: 1rem;
    }
    
    .custom-error i {
        font-size: 3rem;
        margin-bottom: 1rem;
    }
</style>
```

### Example 3: Lazy Loading Video Content
```razor
<div class="video-container">
    <LazyLoad Threshold="0.25" 
              LoadDelay="500"
              FadeIn="true">
        <Placeholder>
            <div class="video-placeholder">
                <i class="fa-solid fa-film"></i>
                <p>Video will load when scrolled into view</p>
            </div>
        </Placeholder>
        
        <video controls width="100%" height="auto">
            <source src="https://sample-videos.com/video123/mp4/720/big_buck_bunny_720p_1mb.mp4" type="video/mp4">
            Your browser does not support the video tag.
        </video>
    </LazyLoad>
</div>

<style>
    .video-container {
        max-width: 800px;
        margin: 40px auto;
        border: 1px solid #ddd;
        border-radius: 4px;
        overflow: hidden;
    }
    
    .video-placeholder {
        height: 450px;
        background-color: #2c3e50;
        color: white;
        display: flex;
        flex-direction: column;
        align-items: center;
        justify-content: center;
    }
    
    .video-placeholder i {
        font-size: 3rem;
        margin-bottom: 1rem;
    }
</style>
```

### Example 4: Lazy Loading Components with Different Thresholds
```razor
<div class="threshold-demo">
    <h3>Scroll down to see components load at different thresholds</h3>
    
    <div class="spacer"></div>
    
    <div class="threshold-container">
        <h4>Threshold: 0.0 (Loads as soon as any part is visible)</h4>
        <LazyLoad Threshold="0.0" FadeIn="true">
            <div class="demo-box bg-primary">
                <span>Threshold: 0.0</span>
            </div>
        </LazyLoad>
    </div>
    
    <div class="threshold-container">
        <h4>Threshold: 0.25 (Loads when 25% visible)</h4>
        <LazyLoad Threshold="0.25" FadeIn="true">
            <div class="demo-box bg-success">
                <span>Threshold: 0.25</span>
            </div>
        </LazyLoad>
    </div>
    
    <div class="threshold-container">
        <h4>Threshold: 0.5 (Loads when 50% visible)</h4>
        <LazyLoad Threshold="0.5" FadeIn="true">
            <div class="demo-box bg-info">
                <span>Threshold: 0.5</span>
            </div>
        </LazyLoad>
    </div>
    
    <div class="threshold-container">
        <h4>Threshold: 0.75 (Loads when 75% visible)</h4>
        <LazyLoad Threshold="0.75" FadeIn="true">
            <div class="demo-box bg-warning">
                <span>Threshold: 0.75</span>
            </div>
        </LazyLoad>
    </div>
    
    <div class="threshold-container">
        <h4>Threshold: 1.0 (Loads only when fully visible)</h4>
        <LazyLoad Threshold="1.0" FadeIn="true">
            <div class="demo-box bg-danger">
                <span>Threshold: 1.0</span>
            </div>
        </LazyLoad>
    </div>
</div>

<style>
    .threshold-demo {
        max-width: 800px;
        margin: 0 auto;
    }
    
    .spacer {
        height: 500px;
    }
    
    .threshold-container {
        margin-bottom: 100px;
    }
    
    .demo-box {
        height: 200px;
        display: flex;
        align-items: center;
        justify-content: center;
        color: white;
        font-size: 1.5rem;
        border-radius: 4px;
    }
</style>
```

### Example 5: Lazy Loading with Event Handling
```razor
@code {
    private List<string> eventLog = new List<string>();
    private int visibleCount = 0;
    
    private void HandleVisibilityChanged(bool isVisible)
    {
        if (isVisible)
        {
            visibleCount++;
            AddLogEntry($"Content became visible (Total visible: {visibleCount})");
        }
        else
        {
            visibleCount--;
            AddLogEntry($"Content went out of view (Total visible: {visibleCount})");
        }
    }
    
    private void HandleLoading()
    {
        AddLogEntry("Content started loading");
    }
    
    private void HandleLoaded()
    {
        AddLogEntry("Content successfully loaded");
    }
    
    private void HandleError()
    {
        AddLogEntry("Error loading content");
    }
    
    private void AddLogEntry(string message)
    {
        eventLog.Insert(0, $"[{DateTime.Now.ToString("HH:mm:ss")}] {message}");
        StateHasChanged();
    }
}

<div class="event-demo">
    <div class="row">
        <div class="col-md-6">
            <div class="content-container">
                <LazyLoad OnVisibilityChanged="isVisible => HandleVisibilityChanged(isVisible)"
                          OnLoading="HandleLoading"
                          OnLoaded="HandleLoaded"
                          OnError="HandleError"
                          FadeIn="true"
                          OnceOnly="false">
                    <img src="https://picsum.photos/600/400" alt="Event demo image" />
                </LazyLoad>
            </div>
            <p class="text-muted mt-2">Scroll this image in and out of view to see events triggered</p>
        </div>
        
        <div class="col-md-6">
            <div class="event-log">
                <h5>Event Log</h5>
                <div class="log-container">
                    @if (eventLog.Any())
                    {
                        <ul class="list-group">
                            @foreach (var entry in eventLog)
                            {
                                <li class="list-group-item">@entry</li>
                            }
                        </ul>
                    }
                    else
                    {
                        <p class="text-muted">No events logged yet</p>
                    }
                </div>
            </div>
        </div>
    </div>
</div>

<style>
    .event-demo {
        max-width: 1000px;
        margin: 0 auto;
    }
    
    .content-container {
        border: 1px solid #ddd;
        border-radius: 4px;
        overflow: hidden;
    }
    
    .event-log {
        border: 1px solid #ddd;
        border-radius: 4px;
        padding: 15px;
        height: 100%;
    }
    
    .log-container {
        max-height: 400px;
        overflow-y: auto;
    }
</style>
```

### Example 6: Lazy Loading iFrames
```razor
<div class="iframe-demo">
    <h3>Scroll down to load embedded content</h3>
    
    <div class="spacer"></div>
    
    <div class="iframe-container">
        <h4>Google Maps</h4>
        <LazyLoad FadeIn="true" 
                  LoadingText="Loading map..." 
                  PreloadOffset="200px">
            <LoadingTemplate>
                <div class="iframe-placeholder">
                    <Spinner />
                    <p>Loading Google Maps...</p>
                </div>
            </LoadingTemplate>
            
            <iframe 
                src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d387193.3059353029!2d-74.25986548248684!3d40.69714941680757!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x89c24fa5d33f083b%3A0xc80b8f06e177fe62!2sNew%20York%2C%20NY%2C%20USA!5e0!3m2!1sen!2sca!4v1619544993358!5m2!1sen!2sca" 
                width="100%" 
                height="450" 
                style="border:0;" 
                allowfullscreen="" 
                loading="lazy">
            </iframe>
        </LazyLoad>
    </div>
    
    <div class="spacer"></div>
    
    <div class="iframe-container">
        <h4>YouTube Video</h4>
        <LazyLoad FadeIn="true" 
                  LoadingText="Loading video..." 
                  PreloadOffset="200px">
            <LoadingTemplate>
                <div class="iframe-placeholder">
                    <Spinner />
                    <p>Loading YouTube video...</p>
                </div>
            </LoadingTemplate>
            
            <iframe 
                width="100%" 
                height="450" 
                src="https://www.youtube.com/embed/dQw4w9WgXcQ" 
                title="YouTube video player" 
                frameborder="0" 
                allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" 
                allowfullscreen>
            </iframe>
        </LazyLoad>
    </div>
</div>

<style>
    .iframe-demo {
        max-width: 800px;
        margin: 0 auto;
    }
    
    .spacer {
        height: 500px;
    }
    
    .iframe-container {
        margin-bottom: 100px;
        border: 1px solid #ddd;
        border-radius: 4px;
        padding: 20px;
    }
    
    .iframe-placeholder {
        width: 100%;
        height: 450px;
        background-color: #f8f9fa;
        display: flex;
        flex-direction: column;
        align-items: center;
        justify-content: center;
    }
    
    .iframe-placeholder p {
        margin-top: 1rem;
    }
</style>
```

### Example 7: Lazy Loading Card Components
```razor
@code {
    private List<CardData> cards = new List<CardData>();
    
    protected override void OnInitialized()
    {
        // Generate sample card data
        for (int i = 1; i <= 20; i++)
        {
            cards.Add(new CardData
            {
                Id = i,
                Title = $"Card {i}",
                Description = $"This is the description for card {i}. It contains some sample text to demonstrate lazy loading of card components.",
                ImageUrl = $"https://picsum.photos/id/{i + 200}/800/600",
                Tags = GenerateRandomTags()
            });
        }
    }
    
    private List<string> GenerateRandomTags()
    {
        var allTags = new[] { "Technology", "Nature", "Business", "Travel", "Food", "Art", "Science", "Health", "Sports" };
        var random = new Random();
        var count = random.Next(1, 4); // 1 to 3 tags
        
        return Enumerable.Range(0, count)
            .Select(_ => allTags[random.Next(allTags.Length)])
            .Distinct()
            .ToList();
    }
    
    private class CardData
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public List<string> Tags { get; set; }
    }
}

<div class="card-demo">
    <h3>Lazy Loaded Cards</h3>
    <p>Scroll down to load more cards</p>
    
    <div class="card-grid">
        @foreach (var card in cards)
        {
            <LazyLoad FadeIn="true" 
                      FadeInDuration="400" 
                      Threshold="0.1" 
                      PreloadOffset="100px">
                <Placeholder>
                    <div class="card-placeholder">
                        <div class="placeholder-header"></div>
                        <div class="placeholder-image"></div>
                        <div class="placeholder-content">
                            <div class="placeholder-line"></div>
                            <div class="placeholder-line"></div>
                            <div class="placeholder-line"></div>
                        </div>
                        <div class="placeholder-footer"></div>
                    </div>
                </Placeholder>
                
                <div class="card">
                    <div class="card-header">
                        <h5>@card.Title</h5>
                    </div>
                    <img src="@card.ImageUrl" class="card-img-top" alt="@card.Title" />
                    <div class="card-body">
                        <p class="card-text">@card.Description</p>
                    </div>
                    <div class="card-footer">
                        @foreach (var tag in card.Tags)
                        {
                            <Badge Color="Color.Primary" Text="@tag" Class="me-1" />
                        }
                    </div>
                </div>
            </LazyLoad>
        }
    </div>
</div>

<style>
    .card-demo {
        max-width: 1200px;
        margin: 0 auto;
    }
    
    .card-grid {
        display: grid;
        grid-template-columns: repeat(auto-fill, minmax(300px, 1fr));
        gap: 20px;
        margin-top: 20px;
    }
    
    .card, .card-placeholder {
        height: 100%;
        border-radius: 4px;
        overflow: hidden;
    }
    
    .card-placeholder {
        border: 1px solid #ddd;
        background-color: #f8f9fa;
    }
    
    .placeholder-header, .placeholder-footer {
        height: 50px;
        background-color: #e9ecef;
    }
    
    .placeholder-image {
        height: 200px;
        background-color: #dee2e6;
    }
    
    .placeholder-content {
        padding: 20px;
    }
    
    .placeholder-line {
        height: 15px;
        margin-bottom: 10px;
        background-color: #e9ecef;
        border-radius: 4px;
    }
    
    .placeholder-line:last-child {
        width: 70%;
    }
</style>
```

## CSS Customization

The LazyLoad component can be customized using CSS variables and classes:

```css
/* Custom LazyLoad styling */
.bb-lazy-container {
    --bb-lazy-fade-duration: 300ms;
    --bb-lazy-loading-bg: #f8f9fa;
    --bb-lazy-loading-text-color: #6c757d;
    --bb-lazy-error-bg: #f8d7da;
    --bb-lazy-error-text-color: #dc3545;
    
    position: relative;
    overflow: hidden;
}

/* Loading state styling */
.bb-lazy-loading {
    background-color: var(--bb-lazy-loading-bg);
    color: var(--bb-lazy-loading-text-color);
    display: flex;
    align-items: center;
    justify-content: center;
    min-height: 100px;
}

/* Error state styling */
.bb-lazy-error {
    background-color: var(--bb-lazy-error-bg);
    color: var(--bb-lazy-error-text-color);
    padding: 1rem;
    text-align: center;
    border-radius: 4px;
}

/* Fade-in animation */
.bb-lazy-fade-in {
    animation: bbLazyFadeIn var(--bb-lazy-fade-duration) ease-in-out;
}

@keyframes bbLazyFadeIn {
    from { opacity: 0; }
    to { opacity: 1; }
}

/* Placeholder styling */
.bb-lazy-placeholder {
    transition: opacity 0.3s ease-out;
}

.bb-lazy-loaded .bb-lazy-placeholder {
    opacity: 0;
    position: absolute;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    z-index: -1;
}
```

## JavaScript Interop

The LazyLoad component uses JavaScript interop to interact with the browser's IntersectionObserver API. Here's an example of the JavaScript code that might be used internally:

```javascript
// This is a simplified example of the JavaScript interop used by the component
window.bootstrapBlazorLazyLoad = {
    observers: {},
    
    // Initialize a new IntersectionObserver for a lazy load component
    initialize: function (id, dotNetReference, options) {
        if (this.observers[id]) {
            this.disconnect(id);
        }
        
        const observer = new IntersectionObserver((entries) => {
            entries.forEach(entry => {
                // Call back to .NET with intersection data
                dotNetReference.invokeMethodAsync('OnIntersectionChanged', {
                    isIntersecting: entry.isIntersecting,
                    intersectionRatio: entry.intersectionRatio
                });
            });
        }, {
            threshold: options.threshold || 0,
            rootMargin: options.rootMargin || '0px'
        });
        
        this.observers[id] = {
            instance: observer,
            element: null
        };
        
        return true;
    },
    
    // Observe a target element
    observe: function (id, element) {
        const observer = this.observers[id];
        if (!observer) return false;
        
        observer.instance.observe(element);
        observer.element = element;
        return true;
    },
    
    // Stop observing and clean up
    disconnect: function (id) {
        const observer = this.observers[id];
        if (!observer) return false;
        
        observer.instance.disconnect();
        delete this.observers[id];
        return true;
    },
    
    // Apply fade-in animation to an element
    applyFadeIn: function (element, duration) {
        if (!element) return false;
        
        element.style.animation = `bbLazyFadeIn ${duration}ms ease-in-out`;
        element.classList.add('bb-lazy-fade-in');
        return true;
    }
};
```

## Accessibility

The LazyLoad component follows accessibility best practices:

- Provides appropriate ARIA attributes for loading and error states
- Ensures that lazy-loaded images have proper alt text
- Maintains keyboard navigation and focus management
- Provides fallback content for users with JavaScript disabled
- Uses semantic HTML elements for placeholders and loading indicators

## Browser Compatibility

The LazyLoad component relies on the IntersectionObserver API, which is supported in all modern browsers. For older browsers that don't support this API natively, the component can include a polyfill to ensure functionality across different browser versions.

## Integration with Other Components

The LazyLoad component works well with many other BootstrapBlazor components:

- Use with `Image` component for lazy loading images
- Combine with `Card` component for lazy loading card content
- Use with `Table` or `List` components for lazy loading list items
- Integrate with `Tabs` or `Collapse` components for lazy loading tab or panel content
- Use with `IFrame` component for lazy loading embedded content