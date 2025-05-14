# Clipboard Component

## Overview
The Clipboard component in BootstrapBlazor provides functionality to copy text to the system clipboard. It offers a simple and intuitive way to enable copy-to-clipboard functionality in web applications, with support for copying text from various sources including input fields, static text, and programmatically generated content.

## Key Features
- One-click copy functionality for text content
- Support for copying from input elements, text areas, or static text
- Visual feedback on copy success or failure
- Customizable trigger elements (buttons, icons, or custom elements)
- Copy confirmation tooltips
- Event callbacks for copy operations
- Accessibility support
- No Flash dependency (uses modern Clipboard API)

## Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `Text` | `string` | `null` | The text to be copied to clipboard when the component is triggered. |
| `Target` | `string` | `null` | CSS selector of the target element whose content will be copied. |
| `TargetId` | `string` | `null` | ID of the target element whose content will be copied. |
| `ButtonText` | `string` | `"Copy"` | Text displayed on the copy button. |
| `ButtonIcon` | `string` | `"copy"` | Icon displayed on the copy button. |
| `ShowTooltip` | `bool` | `true` | Whether to show a tooltip on copy success/failure. |
| `SuccessText` | `string` | `"Copied!"` | Text displayed in the tooltip after successful copy. |
| `FailureText` | `string` | `"Copy failed!"` | Text displayed in the tooltip after failed copy. |
| `TooltipDuration` | `int` | `2000` | Duration in milliseconds to show the tooltip. |
| `Color` | `Color` | `Color.Primary` | Color of the copy button. |
| `IsOutline` | `bool` | `false` | Whether to use an outline style for the button. |
| `IsDisabled` | `bool` | `false` | Whether the copy functionality is disabled. |
| `Size` | `Size` | `Size.None` | Size of the copy button. |
| `CopyMode` | `ClipboardCopyMode` | `ClipboardCopyMode.Text` | Mode for copying content (Text, Html, etc.). |
| `ShowButton` | `bool` | `true` | Whether to show the default copy button. |

## Events

| Event | Description |
|-------|-------------|
| `OnBeforeCopy` | Triggered before the copy operation starts. Can be used to modify the content being copied. |
| `OnCopySuccess` | Triggered when the copy operation succeeds. |
| `OnCopyError` | Triggered when the copy operation fails, with error details. |

## Usage Examples

### Example 1: Basic Clipboard with Static Text

```razor
<div class="mb-3">
    <Clipboard Text="This text will be copied to clipboard" />
</div>
```

### Example 2: Copy from Input Element

```razor
<div class="mb-3">
    <div class="input-group">
        <input type="text" class="form-control" id="textToCopy" value="Copy this text" />
        <Clipboard TargetId="textToCopy" ButtonText="Copy" />
    </div>
</div>
```

### Example 3: Custom Button Styling

```razor
<div class="mb-3">
    <Clipboard 
        Text="Custom styled clipboard button" 
        Color="Color.Success" 
        IsOutline="true"
        Size="Size.Large"
        ButtonIcon="copy"
        ButtonText="Copy Text" />
</div>
```

### Example 4: Clipboard with Event Handling

```razor
<div class="mb-3">
    <Clipboard 
        Text="@clipboardText" 
        OnBeforeCopy="HandleBeforeCopy"
        OnCopySuccess="HandleCopySuccess"
        OnCopyError="HandleCopyError" />
</div>

<Alert @ref="alertRef" />

@code {
    private string clipboardText = "This text will be copied with event handling";
    private Alert alertRef;

    private Task<string> HandleBeforeCopy(string text)
    {
        // You can modify the text before copying
        return Task.FromResult($"{text} (copied at {DateTime.Now})");
    }

    private Task HandleCopySuccess(string text)
    {
        alertRef.Show("Success", "Text copied to clipboard: " + text, Color.Success);
        return Task.CompletedTask;
    }

    private Task HandleCopyError(string errorMessage)
    {
        alertRef.Show("Error", "Failed to copy: " + errorMessage, Color.Danger);
        return Task.CompletedTask;
    }
}
```

### Example 5: Custom Clipboard Trigger Element

```razor
<div class="mb-3">
    <div class="card">
        <div class="card-body">
            <h5 class="card-title">Important Information</h5>
            <p class="card-text" id="cardContent">This is some important information that users might want to copy.</p>
            <Clipboard 
                TargetId="cardContent" 
                ShowButton="false">
                <Button Color="Color.Info">
                    <i class="fa fa-clipboard"></i> Copy Card Content
                </Button>
            </Clipboard>
        </div>
    </div>
</div>
```

### Example 6: Clipboard with Dynamic Content

```razor
<div class="mb-3">
    <div class="form-group">
        <label for="dynamicContent">Edit content to copy:</label>
        <textarea id="dynamicContent" class="form-control" @bind="dynamicContent" rows="3"></textarea>
    </div>
    
    <Clipboard 
        Text="@dynamicContent" 
        ButtonText="Copy Dynamic Content"
        SuccessText="Dynamic content copied!"
        TooltipDuration="3000" />
</div>

@code {
    private string dynamicContent = "This is dynamic content that can be edited before copying.";
}
```

### Example 7: Clipboard in a Data Table

```razor
<Table TItem="UserInfo" Items="@users" IsStriped="true">
    <TableColumns>
        <TableColumn @bind-Field="@context.Id" Width="80" />
        <TableColumn @bind-Field="@context.Name" />
        <TableColumn @bind-Field="@context.Email" />
        <TableColumn Text="Actions" Width="200">
            <Template Context="user">
                <Clipboard 
                    Text="@GetUserDetails(user)" 
                    ButtonText=""
                    ButtonIcon="copy"
                    SuccessText="User details copied!"
                    Color="Color.Info"
                    Size="Size.Small" />
            </Template>
        </TableColumn>
    </TableColumns>
</Table>

@code {
    private List<UserInfo> users = new List<UserInfo>
    {
        new UserInfo { Id = 1, Name = "John Doe", Email = "john@example.com" },
        new UserInfo { Id = 2, Name = "Jane Smith", Email = "jane@example.com" },
        new UserInfo { Id = 3, Name = "Bob Johnson", Email = "bob@example.com" }
    };

    private string GetUserDetails(UserInfo user)
    {
        return $"ID: {user.Id}, Name: {user.Name}, Email: {user.Email}";
    }

    class UserInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
```

## CSS Customization

The Clipboard component can be customized using CSS variables:

```css
/* Custom Clipboard Styling */
.bb-clipboard {
    --bb-clipboard-button-background: #007bff;
    --bb-clipboard-button-color: white;
    --bb-clipboard-button-hover-background: #0069d9;
    --bb-clipboard-button-border-radius: 4px;
    --bb-clipboard-tooltip-background: rgba(0, 0, 0, 0.8);
    --bb-clipboard-tooltip-color: white;
    --bb-clipboard-success-color: #28a745;
    --bb-clipboard-error-color: #dc3545;
}
```

## JavaScript Interop

The Clipboard component uses JavaScript interop to interact with the browser's Clipboard API. This requires a secure context (HTTPS) in production environments for security reasons.

## Accessibility

The Clipboard component includes ARIA attributes for accessibility. The copy button is properly labeled for screen readers, and success/failure states are announced appropriately.

## Performance Considerations

- The Clipboard component is lightweight and has minimal impact on page performance
- For very large text content, consider using a callback approach to generate the content only when needed

## Browser Support

The Clipboard component uses the modern Clipboard API which is supported in:
- Chrome 43+
- Firefox 41+
- Safari 10+
- Edge 12+

For older browsers, a fallback mechanism using document.execCommand('copy') is implemented where possible.

## Integration with Other Components

The Clipboard component works well with:
- Input and TextArea components for copying user input
- Table component for copying row data
- Code component for copying code snippets
- Card component for copying card content