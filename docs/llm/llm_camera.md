# Camera Component

## Overview
The Camera component in BootstrapBlazor provides a way to access and interact with device cameras directly within web applications. It allows capturing photos and videos from the user's device camera, with support for both front and rear cameras, various resolutions, and different capture modes.

## Key Features
- Access to device cameras (front and rear)
- Photo and video capture capabilities
- Customizable resolution and quality settings
- Real-time preview display
- Camera switching functionality
- Flash control (where supported by device)
- Image processing options (filters, cropping, etc.)
- Responsive design for different device sizes
- Event callbacks for capture and error handling

## Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `DeviceId` | `string` | `null` | Specifies the camera device ID to use. If null, the default camera is used. |
| `FacingMode` | `CameraFacingMode` | `CameraFacingMode.User` | Specifies which camera to use on devices with multiple cameras. Options: `User` (front) or `Environment` (rear). |
| `Width` | `int` | `640` | The width of the camera viewport in pixels. |
| `Height` | `int` | `480` | The height of the camera viewport in pixels. |
| `ShowControls` | `bool` | `true` | Whether to show the camera control buttons (capture, switch camera, etc.). |
| `AutoStart` | `bool` | `false` | Whether to automatically start the camera when the component is initialized. |
| `VideoEnabled` | `bool` | `false` | Whether to enable video recording mode (as opposed to photo mode). |
| `ShowFlashButton` | `bool` | `true` | Whether to show the flash control button (if the device supports it). |
| `ShowSwitchButton` | `bool` | `true` | Whether to show the camera switch button (if the device has multiple cameras). |
| `CaptureButtonText` | `string` | `"Capture"` | The text to display on the capture button. |
| `SwitchCameraButtonText` | `string` | `"Switch Camera"` | The text to display on the switch camera button. |
| `FlashButtonText` | `string` | `"Flash"` | The text to display on the flash button. |
| `NoPermissionText` | `string` | `"Camera permission denied"` | The text to display when camera permissions are denied. |
| `ErrorText` | `string` | `"Error accessing camera"` | The text to display when there's an error accessing the camera. |
| `ImageFormat` | `string` | `"image/png"` | The format of the captured image (e.g., "image/png", "image/jpeg"). |
| `ImageQuality` | `double` | `0.9` | The quality of the captured image (0.0 to 1.0, where 1.0 is highest quality). |
| `VideoFormat` | `string` | `"video/webm"` | The format of the recorded video. |
| `VideoConstraints` | `Dictionary<string, object>` | `null` | Additional video constraints for the camera. |

## Events

| Event | Description |
|-------|-------------|
| `OnCameraInitialized` | Triggered when the camera is successfully initialized. |
| `OnCameraError` | Triggered when there's an error initializing or using the camera. |
| `OnPermissionDenied` | Triggered when the user denies camera access permission. |
| `OnImageCaptured` | Triggered when an image is captured, providing the image data. |
| `OnVideoCaptured` | Triggered when video recording is completed, providing the video data. |
| `OnCameraSwitched` | Triggered when the camera is switched between front and rear. |

## Usage Examples

### Example 1: Basic Camera with Photo Capture

```razor
@page "/camera-demo"

<Camera 
    Width="640" 
    Height="480"
    AutoStart="true"
    OnImageCaptured="HandleImageCaptured" />

@if (capturedImageUrl != null)
{
    <div class="mt-3">
        <h5>Captured Image:</h5>
        <img src="@capturedImageUrl" style="max-width: 100%" />
    </div>
}

@code {
    private string capturedImageUrl;

    private void HandleImageCaptured(string imageData)
    {
        capturedImageUrl = imageData;
    }
}
```

### Example 2: Camera with Front/Rear Switching

```razor
<Camera 
    Width="720" 
    Height="540"
    FacingMode="CameraFacingMode.Environment"
    ShowSwitchButton="true"
    SwitchCameraButtonText="Toggle Camera"
    OnCameraSwitched="HandleCameraSwitched"
    OnImageCaptured="HandleImageCaptured" />

@code {
    private string capturedImageUrl;
    private CameraFacingMode currentMode = CameraFacingMode.Environment;

    private void HandleImageCaptured(string imageData)
    {
        capturedImageUrl = imageData;
    }

    private void HandleCameraSwitched(CameraFacingMode mode)
    {
        currentMode = mode;
        Console.WriteLine($"Camera switched to: {mode}");
    }
}
```

### Example 3: Video Recording

```razor
<Camera 
    Width="800" 
    Height="600"
    VideoEnabled="true"
    CaptureButtonText="Record"
    OnVideoCaptured="HandleVideoCaptured" />

@if (videoUrl != null)
{
    <div class="mt-3">
        <h5>Recorded Video:</h5>
        <video controls style="max-width: 100%">
            <source src="@videoUrl" type="video/webm">
        </video>
    </div>
}

@code {
    private string videoUrl;

    private void HandleVideoCaptured(string videoData)
    {
        videoUrl = videoData;
    }
}
```

### Example 4: Custom Camera Controls

```razor
<Camera @ref="cameraRef"
    Width="640" 
    Height="480"
    ShowControls="false"
    AutoStart="true"
    OnImageCaptured="HandleImageCaptured" />

<div class="mt-3 d-flex justify-content-center">
    <Button Color="Color.Primary" OnClick="CaptureImage">Take Photo</Button>
    <Button Color="Color.Secondary" OnClick="SwitchCamera" class="ml-2">Switch Camera</Button>
    <Button Color="Color.Danger" OnClick="StopCamera" class="ml-2">Stop Camera</Button>
</div>

@code {
    private Camera cameraRef;
    private string capturedImageUrl;

    private async Task CaptureImage()
    {
        await cameraRef.CaptureAsync();
    }

    private async Task SwitchCamera()
    {
        await cameraRef.SwitchCameraAsync();
    }

    private async Task StopCamera()
    {
        await cameraRef.StopAsync();
    }

    private void HandleImageCaptured(string imageData)
    {
        capturedImageUrl = imageData;
    }
}
```

### Example 5: Camera with Error Handling

```razor
<Camera 
    Width="640" 
    Height="480"
    AutoStart="true"
    NoPermissionText="Please allow camera access to use this feature"
    ErrorText="Could not initialize camera. Please check your device"
    OnCameraError="HandleCameraError"
    OnPermissionDenied="HandlePermissionDenied"
    OnImageCaptured="HandleImageCaptured" />

<Alert @ref="alertRef" />

@code {
    private Alert alertRef;
    private string capturedImageUrl;

    private void HandleCameraError(string errorMessage)
    {
        alertRef.Show("Camera Error", errorMessage, Color.Danger);
    }

    private void HandlePermissionDenied()
    {
        alertRef.Show("Permission Required", "This feature requires camera permission to work.", Color.Warning);
    }

    private void HandleImageCaptured(string imageData)
    {
        capturedImageUrl = imageData;
    }
}
```

### Example 6: Camera with Image Processing

```razor
<Camera 
    Width="640" 
    Height="480"
    AutoStart="true"
    ImageFormat="image/jpeg"
    ImageQuality="0.8"
    OnImageCaptured="HandleImageCaptured" />

<div class="mt-3">
    <Select TValue="string" @bind-Value="selectedFilter" PlaceHolder="Select Filter">
        <Options>
            <SelectOption Text="No Filter" Value="none" />
            <SelectOption Text="Grayscale" Value="grayscale" />
            <SelectOption Text="Sepia" Value="sepia" />
            <SelectOption Text="Blur" Value="blur" />
        </Options>
    </Select>
</div>

@if (capturedImageUrl != null)
{
    <div class="mt-3">
        <h5>Processed Image:</h5>
        <img src="@capturedImageUrl" style="max-width: 100%; filter: @GetFilterStyle()" />
    </div>
}

@code {
    private string capturedImageUrl;
    private string selectedFilter = "none";

    private void HandleImageCaptured(string imageData)
    {
        capturedImageUrl = imageData;
    }

    private string GetFilterStyle()
    {
        return selectedFilter switch
        {
            "grayscale" => "grayscale(100%)",
            "sepia" => "sepia(100%)",
            "blur" => "blur(5px)",
            _ => "none"
        };
    }
}
```

### Example 7: Responsive Camera with Mobile Detection

```razor
@inject IJSRuntime JSRuntime

<div class="camera-container">
    <Camera 
        Width="@cameraWidth" 
        Height="@cameraHeight"
        AutoStart="true"
        FacingMode="@(isMobile ? CameraFacingMode.Environment : CameraFacingMode.User)"
        ShowSwitchButton="@isMobile"
        OnImageCaptured="HandleImageCaptured" />
</div>

@code {
    private string capturedImageUrl;
    private bool isMobile;
    private int cameraWidth = 640;
    private int cameraHeight = 480;

    protected override async Task OnInitializedAsync()
    {
        isMobile = await JSRuntime.InvokeAsync<bool>("isMobileDevice");
        
        if (isMobile)
        {
            // Use full width on mobile devices
            cameraWidth = 0; // 0 means 100% width
            cameraHeight = 0; // Maintain aspect ratio
        }
    }

    private void HandleImageCaptured(string imageData)
    {
        capturedImageUrl = imageData;
    }
}

@code {
    // Add this JavaScript to your _Host.cshtml or index.html
    /*
    <script>
        window.isMobileDevice = function() {
            return /Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent);
        };
    </script>
    */
}
```

## CSS Customization

The Camera component can be customized using CSS variables:

```css
/* Custom Camera Styling */
.bb-camera {
    --bb-camera-border: 1px solid #ccc;
    --bb-camera-border-radius: 8px;
    --bb-camera-background: #f8f9fa;
    --bb-camera-controls-background: rgba(0, 0, 0, 0.5);
    --bb-camera-controls-color: white;
    --bb-camera-button-background: #007bff;
    --bb-camera-button-color: white;
    --bb-camera-button-hover-background: #0069d9;
}
```

## JavaScript Interop

The Camera component relies on JavaScript interop to access the device's camera hardware through the browser's MediaDevices API. This requires user permission and a secure context (HTTPS) in production environments.

## Accessibility

The Camera component includes ARIA attributes for accessibility and supports keyboard navigation for its controls. Screen readers will announce the purpose of buttons and the current state of the camera.

## Performance Considerations

- Camera access can be resource-intensive, especially on mobile devices
- Consider stopping the camera when not in use to conserve battery and resources
- Adjust resolution settings based on the use case to optimize performance

## Mobile Considerations

- Test on various mobile devices as camera behavior can vary
- Consider orientation changes and how they affect the camera display
- Be aware of permission models which may differ across mobile platforms

## Browser Support

The Camera component requires browser support for the MediaDevices API. This is supported in:
- Chrome 53+
- Firefox 36+
- Safari 11+
- Edge 12+

Older browsers or environments without camera access will display a fallback message.

## Integration with Other Components

The Camera component works well with:
- Form components for submitting captured media
- Modal or Drawer for creating camera overlays
- Upload component for sending captured media to servers
- Image processing components for editing captured photos