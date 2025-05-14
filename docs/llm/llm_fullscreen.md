# FullScreen Component

## Overview
The FullScreen component in BootstrapBlazor provides a simple and intuitive way to toggle fullscreen mode for any content or element on a webpage. It allows users to expand content to occupy the entire screen and exit fullscreen mode with ease, enhancing the viewing experience for media content, dashboards, presentations, or any content that benefits from maximized screen real estate.

## Features
- **Toggle Fullscreen**: Easily switch between normal and fullscreen modes
- **Target Specification**: Make specific elements or the entire page fullscreen
- **Keyboard Support**: Exit fullscreen with ESC key (browser standard)
- **Fullscreen API Integration**: Seamless integration with browser's Fullscreen API
- **State Tracking**: Monitor and react to fullscreen state changes
- **Customizable Triggers**: Use buttons, icons, or custom elements to trigger fullscreen
- **Cross-Browser Compatibility**: Works across modern browsers with appropriate fallbacks
- **Responsive Behavior**: Adapts content appropriately when entering fullscreen mode

## Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `TargetId` | string | null | ID of the element to make fullscreen. If not specified, the component's parent element is used |
| `IsFullScreen` | bool | false | Gets or sets whether the target element is in fullscreen mode |
| `FullScreenText` | string | "Fullscreen" | Text to display for entering fullscreen mode |
| `ExitFullScreenText` | string | "Exit Fullscreen" | Text to display for exiting fullscreen mode |
| `ShowText` | bool | true | Whether to show text alongside the fullscreen icon |
| `Icon` | string | "fa-solid fa-expand" | Icon for the fullscreen button in normal mode |
| `ExitIcon` | string | "fa-solid fa-compress" | Icon for the fullscreen button in fullscreen mode |
| `ButtonClass` | string | "btn-primary" | CSS class for the fullscreen button |
| `ButtonSize` | Size | Size.None | Size of the fullscreen button |
| `AutoHideControls` | bool | false | Whether to automatically hide controls when in fullscreen mode |
| `HideDelay` | int | 3000 | Delay in milliseconds before hiding controls when AutoHideControls is true |
| `DisableScrolling` | bool | true | Whether to disable page scrolling when in fullscreen mode |
| `ChildContent` | RenderFragment | null | Content to be displayed inside the fullscreen container |

## Events

| Event | Description |
|-------|-------------|
| `OnFullScreenChange` | Triggered when fullscreen state changes |
| `OnEnterFullScreen` | Triggered when entering fullscreen mode |
| `OnExitFullScreen` | Triggered when exiting fullscreen mode |
| `OnFullScreenError` | Triggered when an error occurs during fullscreen operations |

## Usage Examples

### Example 1: Basic Fullscreen Button
```razor
<FullScreen>
    <div class="content-container p-4">
        <h3>Fullscreen Content</h3>
        <p>This content can be viewed in fullscreen mode by clicking the button above.</p>
        <img src="/images/sample.jpg" class="img-fluid" alt="Sample Image" />
    </div>
</FullScreen>
```

### Example 2: Custom Fullscreen Trigger
```razor
<FullScreen>
    <div class="video-player-container">
        <div class="video-controls">
            <Button Icon="fa-solid fa-play" Text="Play" />
            <Button Icon="fa-solid fa-pause" Text="Pause" />
            <FullScreenButton Icon="fa-solid fa-expand" ExitIcon="fa-solid fa-compress" ShowText="false" />
        </div>
        <div class="video-content">
            <img src="/images/video-placeholder.jpg" class="img-fluid" alt="Video Placeholder" />
        </div>
    </div>
</FullScreen>
```

### Example 3: Fullscreen with State Tracking
```razor
@code {
    private bool isFullScreen;
    
    private void HandleFullScreenChange(bool fullscreen)
    {
        isFullScreen = fullscreen;
        StateHasChanged();
    }
}

<div class="dashboard-container">
    <div class="dashboard-header d-flex justify-content-between align-items-center">
        <h2>Analytics Dashboard</h2>
        <FullScreen OnFullScreenChange="HandleFullScreenChange" ButtonSize="Size.Small">
            <div class="dashboard-content @(isFullScreen ? "fullscreen-mode" : "")">
                <div class="row">
                    <div class="col-md-6">
                        <div class="card">
                            <div class="card-body">
                                <h5 class="card-title">Sales Overview</h5>
                                <p>Chart content here</p>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="card">
                            <div class="card-body">
                                <h5 class="card-title">Traffic Sources</h5>
                                <p>Chart content here</p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </FullScreen>
    </div>
</div>
```

### Example 4: Targeting Specific Element
```razor
<div class="container">
    <div id="presentation-slide" class="presentation-content">
        <h2>Quarterly Results</h2>
        <div class="chart-container">
            <p>Chart content here</p>
        </div>
    </div>
    
    <FullScreen TargetId="presentation-slide" ButtonClass="btn-outline-primary">
        <Button Text="View Slide Fullscreen" />
    </FullScreen>
</div>
```

### Example 5: Auto-hiding Controls in Fullscreen Mode
```razor
<FullScreen AutoHideControls="true" HideDelay="2000">
    <div class="image-gallery">
        <div class="fullscreen-controls">
            <Button Icon="fa-solid fa-arrow-left" ShowText="false" />
            <Button Icon="fa-solid fa-arrow-right" ShowText="false" />
        </div>
        <img src="/images/gallery/image1.jpg" class="img-fluid" alt="Gallery Image" />
    </div>
</FullScreen>
```

### Example 6: Fullscreen API with JavaScript Interop
```razor
@inject IJSRuntime JSRuntime

@code {
    private FullScreen fullScreenRef;
    
    private async Task ToggleFullScreenProgrammatically()
    {
        await fullScreenRef.ToggleFullScreenAsync();
    }
    
    private async Task HandleKeyPress(KeyboardEventArgs args)
    {
        if (args.Key == "f" && args.CtrlKey)
        {
            await ToggleFullScreenProgrammatically();
        }
    }
}

<div @onkeydown="HandleKeyPress" tabindex="0">
    <Button Text="Toggle Fullscreen (Ctrl+F)" OnClick="ToggleFullScreenProgrammatically" />
    
    <FullScreen @ref="fullScreenRef" OnEnterFullScreen="() => Console.WriteLine('Entered fullscreen')" OnExitFullScreen="() => Console.WriteLine('Exited fullscreen')">
        <div class="content-area p-4">
            <h3>Interactive Content</h3>
            <p>Press Ctrl+F or use the button above to toggle fullscreen mode.</p>
        </div>
    </FullScreen>
</div>
```

### Example 7: Responsive Fullscreen Content
```razor
<style>
    .responsive-container {
        padding: 1rem;
    }
    
    .fullscreen .responsive-container {
        padding: 2rem;
        font-size: 1.2rem;
    }
    
    .fullscreen .responsive-container h2 {
        font-size: 2.5rem;
    }
</style>

@code {
    private bool isFullScreen;
    
    private void HandleFullScreenChange(bool fullscreen)
    {
        isFullScreen = fullscreen;
        StateHasChanged();
    }
}

<FullScreen OnFullScreenChange="HandleFullScreenChange">
    <div class="responsive-container @(isFullScreen ? "fullscreen" : "")">
        <h2>Responsive Content</h2>
        <p>This content adapts its styling when in fullscreen mode.</p>
        <div class="row mt-4">
            <div class="col-md-4">
                <div class="card">
                    <div class="card-body">
                        <h5 class="card-title">Feature 1</h5>
                        <p>Feature description</p>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="card">
                    <div class="card-body">
                        <h5 class="card-title">Feature 2</h5>
                        <p>Feature description</p>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="card">
                    <div class="card-body">
                        <h5 class="card-title">Feature 3</h5>
                        <p>Feature description</p>
                    </div>
                </div>
            </div>
        </div>
    </div>
</FullScreen>
```

## CSS Customization

The FullScreen component can be customized using CSS variables and classes:

```css
/* Custom fullscreen button styling */
.fullscreen-button {
    --bb-fullscreen-btn-bg: #007bff;
    --bb-fullscreen-btn-color: #ffffff;
    --bb-fullscreen-btn-hover-bg: #0056b3;
    --bb-fullscreen-btn-hover-color: #ffffff;
    --bb-fullscreen-btn-border-radius: 4px;
}

/* Styling for content in fullscreen mode */
.fullscreen-content {
    background-color: #ffffff;
    transition: all 0.3s ease;
}

/* Fullscreen mode specific styling */
:fullscreen .fullscreen-content {
    padding: 2rem;
    max-width: 100%;
}
```

## JavaScript Interop

The FullScreen component uses JavaScript interop to interact with the browser's Fullscreen API. It provides methods that can be called from C# code:

- `EnterFullScreenAsync()`: Enters fullscreen mode
- `ExitFullScreenAsync()`: Exits fullscreen mode
- `ToggleFullScreenAsync()`: Toggles between fullscreen and normal modes
- `IsFullScreenAsync()`: Checks if currently in fullscreen mode

## Accessibility

The FullScreen component follows accessibility best practices:

- Provides proper ARIA attributes for fullscreen controls
- Maintains keyboard navigation in fullscreen mode
- Ensures focus management when entering/exiting fullscreen
- Provides visible focus indicators for keyboard users

## Browser Compatibility

The FullScreen component works in all modern browsers that support the Fullscreen API. For older browsers without fullscreen support, the component gracefully degrades and may display a message about limited functionality.

## Integration with Other Components

The FullScreen component can be combined with other BootstrapBlazor components:

- Use with `Modal` for fullscreen modal dialogs
- Combine with `Carousel` for fullscreen image galleries
- Use with `Video` for fullscreen video playback
- Integrate with `Table` for fullscreen data tables