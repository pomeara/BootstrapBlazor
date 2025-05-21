# QRCode Component

## Overview
The QRCode component in BootstrapBlazor provides a simple and efficient way to generate and display QR codes within a Blazor application. QR codes (Quick Response codes) are two-dimensional barcodes that can store various types of data, such as URLs, text, contact information, or any other string data. This component makes it easy to dynamically create QR codes that can be scanned by mobile devices, enabling seamless connections between digital and physical experiences.

## Features
- **Dynamic QR Code Generation**: Generate QR codes from any text or URL input
- **Customizable Size**: Adjust dimensions to fit your layout requirements
- **Color Customization**: Configure foreground and background colors
- **Error Correction Levels**: Multiple error correction options for reliability
- **Logo Integration**: Overlay logos or images on top of QR codes
- **Responsive Design**: Automatically adjust to container size
- **Export Capabilities**: Save QR codes as images
- **Real-time Updates**: Regenerate QR codes when input changes
- **Border Customization**: Adjust margin and border options
- **Accessibility Support**: Proper alt text for screen readers

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `Value` | `string` | `""` | The text or URL to encode in the QR code |
| `Size` | `int` | `128` | The size of the QR code in pixels (width and height) |
| `ForegroundColor` | `string` | `"#000000"` | The color of the QR code modules (dark squares) |
| `BackgroundColor` | `string` | `"#FFFFFF"` | The background color of the QR code |
| `ErrorCorrectionLevel` | `ErrorCorrectionLevel` | `ErrorCorrectionLevel.M` | Error correction level (L: 7%, M: 15%, Q: 25%, H: 30%) |
| `Logo` | `string` | `null` | URL of the logo to overlay on the QR code |
| `LogoSize` | `int` | `0` | Size of the logo in pixels (0 means auto-calculated) |
| `LogoBorderWidth` | `int` | `0` | Width of the border around the logo |
| `LogoBorderColor` | `string` | `"#FFFFFF"` | Color of the border around the logo |
| `Margin` | `int` | `4` | Margin around the QR code in modules (white space) |
| `RoundedCorners` | `bool` | `false` | Whether to use rounded corners for QR code modules |
| `Alt` | `string` | `"QR Code"` | Alternative text for accessibility |
| `ClassName` | `string` | `""` | Additional CSS class for the component |

## Events

| Event | Description |
| --- | --- |
| `OnGenerated` | Triggered when the QR code has been successfully generated |
| `OnError` | Triggered when an error occurs during QR code generation |

## Usage Examples

### Example 1: Basic QR Code

```razor
<QRCode Value="https://www.example.com" />
```

This example shows a basic QR code that encodes a URL. When scanned, it will direct users to the specified website.

### Example 2: Customized QR Code with Colors

```razor
<QRCode Value="@contactInfo"
        Size="200"
        ForegroundColor="#1E88E5"
        BackgroundColor="#F5F5F5"
        ErrorCorrectionLevel="ErrorCorrectionLevel.H" />

@code {
    private string contactInfo = "BEGIN:VCARD\nVERSION:3.0\nN:Doe;John;;;\nFN:John Doe\nTEL;TYPE=CELL:+1-555-123-4567\nEMAIL:john.doe@example.com\nEND:VCARD";
}
```

This example demonstrates a QR code with custom colors and size that encodes contact information in vCard format. The high error correction level ensures the QR code can be read even if partially damaged or obscured.

### Example 3: QR Code with Logo

```razor
<QRCode Value="https://www.mycompany.com"
        Size="250"
        Logo="/images/company-logo.png"
        LogoSize="60"
        LogoBorderWidth="2"
        LogoBorderColor="#FFFFFF"
        ErrorCorrectionLevel="ErrorCorrectionLevel.H" />
```

This example shows a QR code with a company logo overlaid in the center. The high error correction level compensates for the logo covering part of the QR code pattern.

### Example 4: Dynamic QR Code with Binding

```razor
<div class="mb-3">
    <label for="qrInput" class="form-label">Enter text to encode:</label>
    <input id="qrInput" class="form-control" @bind-value="qrValue" @bind-value:event="oninput" />
</div>

<QRCode Value="@qrValue"
        Size="180"
        OnGenerated="HandleQRGenerated" />

<div class="mt-3" if="@isGenerated">
    <p class="text-success">QR Code generated successfully!</p>
</div>

@code {
    private string qrValue = "Hello, World!";
    private bool isGenerated = false;
    
    private void HandleQRGenerated()
    {
        isGenerated = true;
        StateHasChanged();
    }
}
```

This example demonstrates a dynamic QR code that updates in real-time as the user types in an input field, with an event handler that responds when the QR code is successfully generated.

### Example 5: QR Code with Rounded Corners

```razor
<QRCode Value="https://www.example.com/product/12345"
        Size="200"
        ForegroundColor="#673AB7"
        RoundedCorners="true"
        Margin="6" />
```

This example shows a QR code with rounded corners for a more modern look, encoding a product URL with a custom foreground color and increased margin.

### Example 6: Responsive QR Code

```razor
<div class="responsive-qr-container">
    <QRCode Value="@wifiConfig"
            Size="@GetQRSize()"
            ForegroundColor="#212121"
            BackgroundColor="#FAFAFA"
            Alt="WiFi Connection QR Code" />
</div>

@code {
    private string wifiConfig = "WIFI:S:MyNetwork;T:WPA;P:mypassword;;";
    
    private int GetQRSize()
    {
        // In a real implementation, this could use JavaScript interop to get container width
        return Math.Min(window.innerWidth * 0.8, 300);
    }
}

<style>
    .responsive-qr-container {
        width: 100%;
        max-width: 300px;
        margin: 0 auto;
    }
</style>
```

This example demonstrates a responsive QR code that adjusts its size based on the container width, encoding WiFi network credentials that can be scanned to connect to a network.

### Example 7: QR Code with Export Functionality

```razor
<div class="mb-3">
    <QRCode @ref="qrCodeRef"
            Value="@productUrl"
            Size="250"
            ForegroundColor="#00796B"
            BackgroundColor="#E0F2F1" />
</div>

<div class="mb-3">
    <Button Color="Color.Primary" OnClick="ExportQRCode">Download QR Code</Button>
</div>

@code {
    private QRCode qrCodeRef;
    private string productUrl = "https://www.example.com/products/special-offer";
    
    private async Task ExportQRCode()
    {
        // In a real implementation, this would use JavaScript interop to export the QR code as an image
        await JSRuntime.InvokeVoidAsync("exportQRCodeAsImage", qrCodeRef.Element, "product-qr-code.png");
    }
}
```

This example shows a QR code with functionality to export it as an image file, which could be used for printing or sharing.

## Customization Notes

### CSS Variables

The QRCode component can be customized using CSS variables:

```css
:root {
    --bb-qrcode-border-radius: 0;
    --bb-qrcode-module-border-radius: 0;
    --bb-qrcode-shadow: none;
    --bb-qrcode-transition: none;
    --bb-qrcode-hover-transform: none;
}
```

You can override these variables in your CSS to customize the appearance of the QR code component.

### Customization Tips

1. **Error Correction Level**: Choose the appropriate error correction level based on your use case:
   - Level L (Low): 7% of codewords can be restored - use for clean environments with minimal risk of damage
   - Level M (Medium): 15% of codewords can be restored - good for most applications
   - Level Q (Quartile): 25% of codewords can be restored - use when adding a logo or when QR codes might get dirty
   - Level H (High): 30% of codewords can be restored - use for industrial environments or when adding larger logos

2. **Logo Size**: When adding a logo, keep it small enough (generally under 25% of the QR code area) and use a high error correction level to ensure the QR code remains scannable.

3. **Color Contrast**: Maintain high contrast between foreground and background colors for better scanning reliability. Dark modules on a light background work best.

4. **Testing**: Always test your QR codes with multiple scanning apps and devices to ensure compatibility.

5. **Size Considerations**: QR codes should be large enough to be easily scanned. For printed materials, a minimum size of 2 x 2 cm is recommended, with larger sizes for greater scanning distances.

### Integration with Other Components

The QRCode component works well with:

1. **Card Component**: For displaying QR codes with contextual information
2. **Modal Component**: For showing QR codes in pop-up dialogs
3. **Tab Component**: For organizing multiple QR codes
4. **Print Component**: For printing QR codes
5. **Button Component**: For triggering QR code generation or export

### Browser Compatibility

The QRCode component is compatible with all modern browsers that support SVG or Canvas rendering. For older browsers, a fallback image can be provided using the `Alt` property.