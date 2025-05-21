# ImagePreviewer Component

## Overview
The ImagePreviewer component in BootstrapBlazor provides a full-screen image preview experience with advanced viewing capabilities. It allows users to view images in detail with zoom, rotation, and navigation controls. This component is typically used in conjunction with the ImageViewer component to enhance the image viewing experience in web applications, particularly for galleries, product showcases, and media-rich content.

## Features
- **Full-Screen Preview**: Displays images in a full-screen overlay for detailed viewing
- **Zoom Controls**: Allows users to zoom in and out of images for detailed inspection
- **Rotation Support**: Enables rotating images for better viewing angles
- **Navigation Controls**: Provides controls to navigate between multiple images
- **Keyboard Navigation**: Supports keyboard shortcuts for navigation and control
- **Touch Gestures**: Responsive to touch gestures for mobile device compatibility
- **Image Transition Effects**: Smooth transitions between images and zoom levels
- **Customizable Controls**: Configurable control buttons and actions
- **Responsive Design**: Adapts to different screen sizes and orientations
- **Accessibility Support**: Keyboard navigable and screen reader compatible
- **Close Button**: Easy way to exit the preview mode
- **Fullscreen Toggle**: Option to toggle true fullscreen mode

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `Images` | `IEnumerable<string>` | `null` | Collection of image URLs to display in the previewer |
| `CurrentIndex` | `int` | `0` | Index of the currently displayed image |
| `Visible` | `bool` | `false` | Whether the previewer is currently visible |
| `ShowNavigation` | `bool` | `true` | Whether to show navigation controls for multiple images |
| `ShowActions` | `bool` | `true` | Whether to show action controls (zoom, rotate, etc.) |
| `ShowClose` | `bool` | `true` | Whether to show the close button |
| `ZoomStep` | `double` | `0.1` | The increment/decrement value for zoom operations |
| `MinZoom` | `double` | `0.1` | Minimum zoom level allowed |
| `MaxZoom` | `double` | `10` | Maximum zoom level allowed |
| `RotationStep` | `int` | `90` | The increment/decrement value for rotation in degrees |
| `TransitionDuration` | `int` | `300` | Duration of transitions in milliseconds |
| `BackdropColor` | `string` | `"#000"` | Color of the backdrop behind the image |
| `BackdropOpacity` | `double` | `0.5` | Opacity of the backdrop (0-1) |
| `TitleTemplate` | `RenderFragment` | `null` | Custom template for the image title |
| `Class` | `string` | `""` | Additional CSS class for the component |
| `Style` | `string` | `null` | Additional inline styles for the component |

## Events

| Event | Description |
| --- | --- |
| `OnOpen` | Triggered when the previewer is opened |
| `OnClose` | Triggered when the previewer is closed |
| `OnImageChange` | Triggered when the current image changes |
| `OnZoom` | Triggered when the zoom level changes |
| `OnRotate` | Triggered when the image is rotated |
| `OnFullscreenChange` | Triggered when entering or exiting fullscreen mode |

## Usage Examples

### Example 1: Basic Image Previewer

```razor
<Button OnClick="@ShowPreviewer">Open Image Preview</Button>

<ImagePreviewer @ref="previewerRef"
               Images="@imageUrls"
               OnClose="HandleClose" />

@code {
    private ImagePreviewer previewerRef;
    private List<string> imageUrls = new List<string>
    {
        "/images/sample1.jpg",
        "/images/sample2.jpg",
        "/images/sample3.jpg"
    };
    
    private void ShowPreviewer()
    {
        previewerRef.Show();
    }
    
    private void HandleClose()
    {
        Console.WriteLine("Previewer closed");
    }
}
```

This example shows a basic implementation of the ImagePreviewer component with a button to trigger the preview and a collection of images to display.

### Example 2: Integration with ImageViewer Component

```razor
<div class="image-gallery">
    @for (int i = 0; i < imageUrls.Count; i++)
    {
        var index = i;
        <div class="gallery-item">
            <ImageViewer Src="@imageUrls[index]"
                         Alt="Gallery image @(index + 1)"
                         Width="200px"
                         Height="150px"
                         ObjectFit="ObjectFitType.Cover"
                         IsPreview="true"
                         OnPreview="() => ShowPreview(index)" />
        </div>
    }
</div>

<ImagePreviewer @ref="previewerRef"
               Images="@imageUrls"
               OnImageChange="HandleImageChange" />

@code {
    private ImagePreviewer previewerRef;
    private List<string> imageUrls = new List<string>
    {
        "/images/sample1.jpg",
        "/images/sample2.jpg",
        "/images/sample3.jpg",
        "/images/sample4.jpg",
        "/images/sample5.jpg",
        "/images/sample6.jpg"
    };
    
    private void ShowPreview(int index)
    {
        previewerRef.Show(index);
    }
    
    private void HandleImageChange(int index)
    {
        Console.WriteLine($"Current image index: {index}");
    }
}

<style>
    .image-gallery {
        display: grid;
        grid-template-columns: repeat(auto-fill, minmax(220px, 1fr));
        gap: 15px;
    }
    
    .gallery-item {
        border: 1px solid #ddd;
        border-radius: 4px;
        overflow: hidden;
    }
</style>
```

This example demonstrates how to integrate the ImagePreviewer with multiple ImageViewer components to create a gallery where clicking on any image opens it in the previewer.

### Example 3: Custom Controls and Actions

```razor
<ImagePreviewer @ref="previewerRef"
               Images="@imageUrls"
               ShowActions="true"
               ZoomStep="0.2"
               MinZoom="0.5"
               MaxZoom="5"
               RotationStep="45">
    <TitleTemplate>
        <div class="custom-title">
            <h4>Image @(previewerRef.CurrentIndex + 1) of @imageUrls.Count</h4>
            <p>@imageTitles[previewerRef.CurrentIndex]</p>
        </div>
    </TitleTemplate>
</ImagePreviewer>

<Button Color="Color.Primary" OnClick="@ShowPreviewer">View Gallery</Button>

@code {
    private ImagePreviewer previewerRef;
    private List<string> imageUrls = new List<string>
    {
        "/images/landscape1.jpg",
        "/images/landscape2.jpg",
        "/images/landscape3.jpg"
    };
    
    private List<string> imageTitles = new List<string>
    {
        "Mountain Sunrise",
        "Ocean Sunset",
        "Forest Path"
    };
    
    private void ShowPreviewer()
    {
        previewerRef.Show();
    }
}

<style>
    .custom-title {
        color: white;
        text-align: center;
        padding: 10px;
        background-color: rgba(0, 0, 0, 0.5);
        border-radius: 4px;
    }
</style>
```

This example shows how to customize the ImagePreviewer with custom zoom and rotation settings, as well as a custom title template.

### Example 4: Event Handling and State Management

```razor
<div class="mb-3">
    <Button Color="Color.Primary" OnClick="@ShowPreviewer">Open Image Preview</Button>
</div>

<div class="mb-3">
    <h5>Preview State</h5>
    <div class="alert alert-info">
        <p><strong>Is Open:</strong> @isPreviewOpen</p>
        <p><strong>Current Image:</strong> @(currentImageIndex + 1) of @imageUrls.Count</p>
        <p><strong>Zoom Level:</strong> @zoomLevel.ToString("P0")</p>
        <p><strong>Rotation:</strong> @rotation°</p>
    </div>
</div>

<ImagePreviewer @ref="previewerRef"
               Images="@imageUrls"
               OnOpen="HandleOpen"
               OnClose="HandleClose"
               OnImageChange="HandleImageChange"
               OnZoom="HandleZoom"
               OnRotate="HandleRotate" />

@code {
    private ImagePreviewer previewerRef;
    private List<string> imageUrls = new List<string>
    {
        "/images/sample1.jpg",
        "/images/sample2.jpg",
        "/images/sample3.jpg"
    };
    
    private bool isPreviewOpen = false;
    private int currentImageIndex = 0;
    private double zoomLevel = 1.0;
    private int rotation = 0;
    
    private void ShowPreviewer()
    {
        previewerRef.Show();
    }
    
    private void HandleOpen()
    {
        isPreviewOpen = true;
    }
    
    private void HandleClose()
    {
        isPreviewOpen = false;
        // Reset state when closed
        zoomLevel = 1.0;
        rotation = 0;
    }
    
    private void HandleImageChange(int index)
    {
        currentImageIndex = index;
        // Reset zoom and rotation for new image
        zoomLevel = 1.0;
        rotation = 0;
    }
    
    private void HandleZoom(double level)
    {
        zoomLevel = level;
    }
    
    private void HandleRotate(int degrees)
    {
        rotation = degrees;
    }
}
```

This example demonstrates how to handle various events from the ImagePreviewer component to track and manage the state of the preview.

### Example 5: Programmatic Control

```razor
<div class="mb-3">
    <Button Color="Color.Primary" OnClick="@ShowPreviewer">Open Preview</Button>
</div>

<div class="mb-3 d-flex gap-2">
    <Button Color="Color.Secondary" OnClick="@(() => NavigateImage(-1))" Disabled="@(!isPreviewOpen)">Previous</Button>
    <Button Color="Color.Secondary" OnClick="@(() => NavigateImage(1))" Disabled="@(!isPreviewOpen)">Next</Button>
    <Button Color="Color.Secondary" OnClick="@ZoomIn" Disabled="@(!isPreviewOpen)">Zoom In</Button>
    <Button Color="Color.Secondary" OnClick="@ZoomOut" Disabled="@(!isPreviewOpen)">Zoom Out</Button>
    <Button Color="Color.Secondary" OnClick="@RotateImage" Disabled="@(!isPreviewOpen)">Rotate</Button>
    <Button Color="Color.Danger" OnClick="@ClosePreviewer" Disabled="@(!isPreviewOpen)">Close</Button>
</div>

<ImagePreviewer @ref="previewerRef"
               Images="@imageUrls"
               OnOpen="() => isPreviewOpen = true"
               OnClose="() => isPreviewOpen = false" />

@code {
    private ImagePreviewer previewerRef;
    private List<string> imageUrls = new List<string>
    {
        "/images/sample1.jpg",
        "/images/sample2.jpg",
        "/images/sample3.jpg",
        "/images/sample4.jpg"
    };
    
    private bool isPreviewOpen = false;
    
    private void ShowPreviewer()
    {
        previewerRef.Show();
    }
    
    private void ClosePreviewer()
    {
        previewerRef.Hide();
    }
    
    private void NavigateImage(int direction)
    {
        if (direction < 0)
        {
            previewerRef.Previous();
        }
        else
        {
            previewerRef.Next();
        }
    }
    
    private void ZoomIn()
    {
        previewerRef.ZoomIn();
    }
    
    private void ZoomOut()
    {
        previewerRef.ZoomOut();
    }
    
    private void RotateImage()
    {
        previewerRef.Rotate();
    }
}
```

This example shows how to programmatically control the ImagePreviewer component using external buttons for navigation, zoom, rotation, and closing.

### Example 6: Responsive Design for Different Devices

```razor
<div class="mb-3">
    <Button Color="Color.Primary" OnClick="@ShowPreviewer">Open Image Gallery</Button>
</div>

<ImagePreviewer @ref="previewerRef"
               Images="@imageUrls"
               Class="responsive-previewer"
               Style="@($"--bb-viewer-button-bg: {buttonBackground};")" />

@code {
    private ImagePreviewer previewerRef;
    private List<string> imageUrls = new List<string>
    {
        "/images/landscape1.jpg",
        "/images/portrait1.jpg",
        "/images/landscape2.jpg",
        "/images/portrait2.jpg"
    };
    
    private string buttonBackground = "rgba(0, 0, 0, 0.6)";
    
    private void ShowPreviewer()
    {
        previewerRef.Show();
    }
    
    protected override void OnInitialized()
    {
        // You could adjust settings based on browser detection
        // or use JavaScript interop to detect device capabilities
    }
}

<style>
    /* Base styles */
    .responsive-previewer {
        --button-size: 44px;
    }
    
    /* Tablet adjustments */
    @media (max-width: 768px) {
        .responsive-previewer {
            --button-size: 36px;
        }
    }
    
    /* Mobile adjustments */
    @media (max-width: 480px) {
        .responsive-previewer {
            --button-size: 32px;
        }
        
        .responsive-previewer .bb-viewer-actions {
            width: 220px;
            height: 40px;
        }
    }
</style>
```

This example demonstrates how to create a responsive ImagePreviewer that adapts to different screen sizes using CSS media queries.

### Example 7: Custom Image Loading and Error Handling

```razor
<Button Color="Color.Primary" OnClick="@ShowPreviewer">View Images</Button>

<ImagePreviewer @ref="previewerRef"
               Images="@imageUrls"
               OnImageChange="HandleImageChange">
    <LoadingTemplate>
        <div class="custom-loading">
            <div class="spinner-border text-light" role="status">
                <span class="visually-hidden">Loading...</span>
            </div>
            <p class="mt-2">Loading high-resolution image...</p>
        </div>
    </LoadingTemplate>
    <ErrorTemplate>
        <div class="custom-error">
            <i class="fa fa-exclamation-triangle fa-3x"></i>
            <p class="mt-2">Failed to load image. Please try again later.</p>
            <Button Color="Color.Light" OnClick="@ReloadCurrentImage">Retry</Button>
        </div>
    </ErrorTemplate>
</ImagePreviewer>

@code {
    private ImagePreviewer previewerRef;
    private List<string> imageUrls = new List<string>
    {
        "/images/sample1.jpg",
        "/images/sample2.jpg",
        "/images/sample3.jpg",
        "https://example.com/nonexistent-image.jpg" // This will trigger the error template
    };
    
    private int currentIndex = 0;
    
    private void ShowPreviewer()
    {
        previewerRef.Show();
    }
    
    private void HandleImageChange(int index)
    {
        currentIndex = index;
    }
    
    private void ReloadCurrentImage()
    {
        // Logic to reload the current image
        // This could involve refreshing the URL or other recovery logic
        Console.WriteLine($"Attempting to reload image at index {currentIndex}");
    }
}

<style>
    .custom-loading,
    .custom-error {
        display: flex;
        flex-direction: column;
        align-items: center;
        justify-content: center;
        color: white;
        height: 100%;
        padding: 20px;
        text-align: center;
    }
    
    .custom-error {
        color: #f8d7da;
    }
</style>
```

This example shows how to implement custom loading and error templates for the ImagePreviewer component to provide a better user experience during image loading and error states.

## Customization

The ImagePreviewer component can be customized using CSS variables and classes:

```css
.bb-previewer {
    --bb-viewer-button-bg: rgba(0, 0, 0, 0.5);
    --bb-viewer-border-radius: 4px;
}

/* Mask background */
.bb-viewer-mask {
    /* Custom styles for the backdrop */
}

/* Navigation buttons */
.bb-viewer-btn {
    /* Custom styles for all buttons */
}

.bb-viewer-prev,
.bb-viewer-next {
    /* Custom styles for navigation buttons */
}

.bb-viewer-close {
    /* Custom styles for close button */
}

/* Action buttons container */
.bb-viewer-actions {
    /* Custom styles for the actions container */
}

.bb-viewer-actions-inner {
    /* Custom styles for the actions inner container */
}

/* Fullscreen button */
.bb-viewer-full-screen {
    /* Custom styles for fullscreen button */
}

/* Canvas that holds the image */
.bb-viewer-canvas {
    /* Custom styles for the image canvas */
}

.bb-viewer-canvas > img {
    /* Custom styles for the image itself */
}
```

Additionally, you can customize the ImagePreviewer component by:

1. Using the `TitleTemplate` property to create a custom title display
2. Using the `LoadingTemplate` property to customize the loading state
3. Using the `ErrorTemplate` property to customize the error state
4. Using the `ZoomStep`, `MinZoom`, and `MaxZoom` properties to control zoom behavior
5. Using the `RotationStep` property to control rotation behavior
6. Using the `ShowNavigation`, `ShowActions`, and `ShowClose` properties to control which controls are displayed
7. Using the `Class` and `Style` properties to apply custom CSS classes and inline styles