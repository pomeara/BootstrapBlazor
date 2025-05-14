# ImageViewer Component

## Overview
The ImageViewer component in BootstrapBlazor provides a way to display and interact with images in a web application. It offers features for loading, displaying, and previewing images with various object-fit options, loading states, and error handling. The component is particularly useful for applications that need to display images with consistent styling, responsive behavior, and interactive capabilities.

## Features
- **Image Display**: Renders images with customizable dimensions and styling
- **Loading States**: Shows loading indicators while images are being loaded
- **Error Handling**: Graceful fallback when images fail to load
- **Object-Fit Options**: Multiple fitting modes (fill, contain, cover, none, scale-down)
- **Preview Capability**: Optional click-to-preview functionality for enlarged viewing
- **Responsive Design**: Adapts to different container sizes and screen dimensions
- **Alt Text Support**: Accessibility-friendly with alt text for screen readers
- **Custom Styling**: Extensive customization options through CSS variables
- **Lazy Loading Integration**: Optional integration with lazy loading for performance
- **Event Callbacks**: Notifications for loading, loaded, and error states

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `Src` | `string` | `null` | URL of the image to display |
| `Alt` | `string` | `null` | Alternative text for the image for accessibility |
| `Width` | `string` | `null` | Width of the image (CSS value) |
| `Height` | `string` | `null` | Height of the image (CSS value) |
| `ObjectFit` | `ObjectFitType` | `ObjectFitType.Fill` | How the image should be resized to fit its container |
| `IsPreview` | `bool` | `false` | Whether the image can be clicked to open in preview mode |
| `PreviewTitle` | `string` | `null` | Title to display in the preview mode |
| `ShowLoading` | `bool` | `true` | Whether to show a loading indicator while the image loads |
| `LoadingTemplate` | `RenderFragment` | `null` | Custom template for the loading state |
| `ErrorTemplate` | `RenderFragment` | `null` | Custom template for the error state |
| `Class` | `string` | `""` | Additional CSS class for the component |
| `Style` | `string` | `null` | Additional inline styles for the component |

## Events

| Event | Description |
| --- | --- |
| `OnLoad` | Triggered when the image successfully loads |
| `OnError` | Triggered when the image fails to load |
| `OnPreview` | Triggered when the image is clicked for preview (when IsPreview is true) |

## Usage Examples

### Example 1: Basic Image Display

```razor
<ImageViewer Src="/images/sample.jpg" Alt="Sample image" />
```

This example shows a basic image display with default settings.

### Example 2: Custom Dimensions and Object Fit

```razor
<ImageViewer Src="/images/landscape.jpg"
             Alt="Landscape photo"
             Width="300px"
             Height="200px"
             ObjectFit="ObjectFitType.Cover" />
```

This example demonstrates an image with specific dimensions and the "cover" object-fit mode, which ensures the image covers the entire container while maintaining its aspect ratio.

### Example 3: Image with Preview Capability

```razor
<ImageViewer Src="/images/product.jpg"
             Alt="Product image"
             Width="200px"
             Height="200px"
             IsPreview="true"
             PreviewTitle="Product Details" />
```

This example shows an image that can be clicked to open in a full-screen preview mode with a title.

### Example 4: Custom Loading and Error Templates

```razor
<ImageViewer Src="@imageUrl"
             Alt="Dynamic image"
             Width="400px"
             Height="300px">
    <LoadingTemplate>
        <div class="custom-loader">
            <Spinner />
            <p>Loading your image...</p>
        </div>
    </LoadingTemplate>
    <ErrorTemplate>
        <div class="error-container">
            <i class="fa fa-exclamation-triangle text-danger"></i>
            <p>Failed to load image. <Button Text="Retry" OnClick="ReloadImage" /></p>
        </div>
    </ErrorTemplate>
</ImageViewer>

@code {
    private string imageUrl = "https://example.com/image.jpg";
    
    private void ReloadImage()
    {
        // Reload logic here
        imageUrl = $"{imageUrl}?reload={DateTime.Now.Ticks}";
    }
}
```

This example demonstrates custom templates for both loading and error states, including a retry button in the error template.

### Example 5: Image Gallery with Different Object Fit Options

```razor
<div class="image-gallery">
    <div class="gallery-item">
        <h5>Original (Fill)</h5>
        <ImageViewer Src="/images/sample.jpg" Alt="Fill example" ObjectFit="ObjectFitType.Fill" Height="200px" />
    </div>
    
    <div class="gallery-item">
        <h5>Contain</h5>
        <ImageViewer Src="/images/sample.jpg" Alt="Contain example" ObjectFit="ObjectFitType.Contain" Height="200px" />
    </div>
    
    <div class="gallery-item">
        <h5>Cover</h5>
        <ImageViewer Src="/images/sample.jpg" Alt="Cover example" ObjectFit="ObjectFitType.Cover" Height="200px" />
    </div>
    
    <div class="gallery-item">
        <h5>None</h5>
        <ImageViewer Src="/images/sample.jpg" Alt="None example" ObjectFit="ObjectFitType.None" Height="200px" />
    </div>
    
    <div class="gallery-item">
        <h5>Scale Down</h5>
        <ImageViewer Src="/images/sample.jpg" Alt="Scale down example" ObjectFit="ObjectFitType.ScaleDown" Height="200px" />
    </div>
</div>

<style>
    .image-gallery {
        display: grid;
        grid-template-columns: repeat(auto-fill, minmax(250px, 1fr));
        gap: 20px;
    }
    
    .gallery-item {
        border: 1px solid #ddd;
        border-radius: 4px;
        padding: 10px;
    }
</style>
```

This example shows a gallery of images with different object-fit settings to demonstrate how each option affects the image display.

### Example 6: Responsive Image with Event Handling

```razor
<ImageViewer Src="@currentImage"
             Alt="Dynamic image with events"
             Width="100%"
             Height="auto"
             OnLoad="HandleImageLoad"
             OnError="HandleImageError" />

<div class="mt-3">
    <h5>Image Status</h5>
    <div class="alert @statusAlertClass" role="alert">
        @statusMessage
    </div>
</div>

@code {
    private string currentImage = "/images/sample1.jpg";
    private string statusMessage = "Loading image...";
    private string statusAlertClass = "alert-info";
    
    private void HandleImageLoad()
    {
        statusMessage = $"Image loaded successfully at {DateTime.Now.ToShortTimeString()}";
        statusAlertClass = "alert-success";
    }
    
    private void HandleImageError()
    {
        statusMessage = $"Failed to load image at {DateTime.Now.ToShortTimeString()}";
        statusAlertClass = "alert-danger";
    }
}
```

This example demonstrates how to handle the load and error events of the ImageViewer component to provide user feedback.

### Example 7: Integration with ImagePreviewer for Multiple Images

```razor
<div class="image-gallery">
    @foreach (var image in images)
    {
        <div class="gallery-item">
            <ImageViewer Src="@image.Url"
                         Alt="@image.Description"
                         Width="100%"
                         Height="200px"
                         ObjectFit="ObjectFitType.Cover"
                         IsPreview="true"
                         PreviewTitle="@image.Title"
                         OnPreview="() => ShowImageDetails(image)" />
            <div class="image-caption">@image.Title</div>
        </div>
    }
</div>

@code {
    private List<ImageItem> images = new List<ImageItem>
    {
        new ImageItem { Url = "/images/image1.jpg", Title = "Mountain Landscape", Description = "Beautiful mountain view at sunset" },
        new ImageItem { Url = "/images/image2.jpg", Title = "Ocean Waves", Description = "Waves crashing on a rocky shore" },
        new ImageItem { Url = "/images/image3.jpg", Title = "Forest Path", Description = "Sunlight filtering through trees on a forest path" },
        new ImageItem { Url = "/images/image4.jpg", Title = "City Skyline", Description = "Urban skyline at night with lights" },
        new ImageItem { Url = "/images/image5.jpg", Title = "Desert Dunes", Description = "Sand dunes in a vast desert landscape" }
    };
    
    private void ShowImageDetails(ImageItem image)
    {
        // Additional logic for showing image details
        Console.WriteLine($"Showing details for: {image.Title}");
    }
    
    private class ImageItem
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}

<style>
    .image-gallery {
        display: grid;
        grid-template-columns: repeat(auto-fill, minmax(250px, 1fr));
        gap: 20px;
    }
    
    .gallery-item {
        border: 1px solid #ddd;
        border-radius: 4px;
        overflow: hidden;
    }
    
    .image-caption {
        padding: 10px;
        text-align: center;
        font-weight: bold;
    }
</style>
```

This example shows how to create an image gallery with preview capability, where clicking on an image opens it in a full-screen preview mode.

## Customization

The ImageViewer component can be customized using CSS classes and styles:

```css
.bb-img {
    /* Custom styles for the image container */
}

.bb-img-holder {
    /* Custom styles for the image holder */
    background-color: var(--bs-body-bg);
}

.bb-img-loading {
    /* Custom styles for the loading state */
}

.bb-img-error .bb-img-loading {
    /* Custom styles for the error state */
}

/* Object fit classes */
.obj-fit-fill {
    object-fit: fill;
}

.obj-fit-contain {
    object-fit: contain;
}

.obj-fit-cover {
    object-fit: cover;
}

.obj-fit-none {
    object-fit: none;
}

.obj-fit-scale-down {
    object-fit: scale-down;
}
```

You can override these classes in your CSS to customize the appearance of the ImageViewer component according to your design requirements.

Additionally, you can customize the ImageViewer component by:

1. Using the `LoadingTemplate` and `ErrorTemplate` properties to create custom loading and error states
2. Using the `Width` and `Height` properties to control the dimensions of the image
3. Using the `ObjectFit` property to control how the image fits within its container
4. Using the `IsPreview` property to enable or disable the preview functionality
5. Using the `Class` and `Style` properties to apply custom CSS classes and inline styles