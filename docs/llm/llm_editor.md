# Editor Component Documentation

## Overview
The Editor component in BootstrapBlazor provides a rich text editing experience within web applications. It allows users to create and edit formatted content with a WYSIWYG (What You See Is What You Get) interface. This component is ideal for applications that require content creation capabilities such as content management systems, email composers, comment systems, or any scenario where formatted text input is needed. The Editor offers a comprehensive set of text formatting tools while maintaining an intuitive user experience.

## Features
- Rich text editing with WYSIWYG interface
- Comprehensive formatting options (bold, italic, underline, etc.)
- Paragraph styling and alignment controls
- Heading level selection
- Bulleted and numbered lists
- Image and media embedding
- Hyperlink creation and management
- Code block formatting with syntax highlighting
- Table creation and editing
- Font family and size selection
- Text and background color options
- Undo/redo functionality
- Copy/paste support with format preservation
- Fullscreen editing mode
- Source code editing mode
- Customizable toolbar with button grouping
- Localization support
- Accessibility features
- Mobile-friendly responsive design

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| Value | string | "" | Gets or sets the HTML content of the editor |
| ValueChanged | EventCallback<string> | - | Callback when the content changes |
| Height | int | 300 | Height of the editor in pixels |
| ToolbarItems | IEnumerable<EditorToolbarItem> | Default set | Collection of toolbar items to display |
| IsDisabled | bool | false | Whether the editor is disabled |
| IsReadOnly | bool | false | Whether the editor is read-only |
| Placeholder | string | "" | Placeholder text when the editor is empty |
| UploadUrl | string | null | URL for image upload handling |
| EnableMarkdown | bool | false | Whether to enable Markdown mode |
| EnableSourceCode | bool | true | Whether to enable source code editing mode |
| EnableFullscreen | bool | true | Whether to enable fullscreen editing mode |
| EnableWordCount | bool | false | Whether to display word count |
| MaxLength | int? | null | Maximum character length allowed |
| MinHeight | int | 200 | Minimum height of the editor in pixels |
| MaxHeight | int? | null | Maximum height of the editor in pixels |
| AutoFocus | bool | false | Whether to automatically focus the editor on load |
| ClassName | string | null | Additional CSS class for the editor |

## Events

| Event | Description |
| --- | --- |
| OnValueChanged | Triggered when the content changes |
| OnFocus | Triggered when the editor receives focus |
| OnBlur | Triggered when the editor loses focus |
| OnReady | Triggered when the editor is fully initialized |
| OnImageUpload | Triggered when an image is uploaded |
| OnImageUploadError | Triggered when an image upload fails |
| OnFullscreenChanged | Triggered when fullscreen mode changes |
| OnSourceCodeChanged | Triggered when source code mode changes |

## Usage Examples

### Example 1: Basic Editor

```razor
<Editor @bind-Value="@content" Height="300" />

<div class="mt-3">
    <h5>Preview:</h5>
    <div class="border p-3 rounded">
        @((MarkupString)content)
    </div>
</div>

@code {
    private string content = "<h3>Welcome to the Editor</h3><p>This is a <strong>rich text editor</strong> component for Blazor applications.</p>";
}
```

### Example 2: Custom Toolbar Configuration

```razor
<Editor @bind-Value="@content"
        ToolbarItems="@customToolbarItems"
        Height="250" />

<div class="mt-3">
    <h5>Preview:</h5>
    <div class="border p-3 rounded">
        @((MarkupString)content)
    </div>
</div>

@code {
    private string content = "<p>Edit this content with a simplified toolbar.</p>";
    
    private List<EditorToolbarItem> customToolbarItems = new List<EditorToolbarItem>
    {
        EditorToolbarItem.Bold,
        EditorToolbarItem.Italic,
        EditorToolbarItem.Underline,
        EditorToolbarItem.Separator,
        EditorToolbarItem.Heading,
        EditorToolbarItem.Separator,
        EditorToolbarItem.Paragraph,
        EditorToolbarItem.Separator,
        EditorToolbarItem.UnorderedList,
        EditorToolbarItem.OrderedList,
        EditorToolbarItem.Separator,
        EditorToolbarItem.Link,
        EditorToolbarItem.Image
    };
}
```

### Example 3: Form Integration with Validation

```razor
<ValidateForm Model="@model" OnValidSubmit="@HandleValidSubmit">
    <div class="mb-3">
        <BootstrapInput @bind-Value="@model.Title" placeholder="Article title" />
        <ValidationMessage For="@(() => model.Title)" />
    </div>
    
    <div class="mb-3">
        <label>Content</label>
        <Editor @bind-Value="@model.Content"
                Height="400"
                EnableWordCount="true"
                MaxLength="10000" />
        <ValidationMessage For="@(() => model.Content)" />
    </div>
    
    <Button Type="ButtonType.Submit">Publish Article</Button>
</ValidateForm>

@code {
    private ArticleModel model = new ArticleModel
    {
        Title = "",
        Content = ""
    };
    
    private void HandleValidSubmit()
    {
        // Save the article
        Console.WriteLine($"Article published: {model.Title}");
        Console.WriteLine($"Content length: {model.Content.Length}");
    }
    
    public class ArticleModel
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
        public string Title { get; set; }
        
        [Required(ErrorMessage = "Content is required")]
        [MinLength(50, ErrorMessage = "Content must be at least 50 characters")]
        public string Content { get; set; }
    }
}
```

### Example 4: Read-Only Mode

```razor
<div class="mb-3">
    <Button OnClick="@ToggleEditMode">@(isEditing ? "View" : "Edit")</Button>
</div>

@if (isEditing)
{
    <Editor @bind-Value="@content" Height="300" />
}
else
{
    <Editor Value="@content" IsReadOnly="true" Height="300" />
}

@code {
    private string content = "<h3>Article Title</h3><p>This is a sample article content that demonstrates the read-only mode of the Editor component.</p><ul><li>Point one</li><li>Point two</li><li>Point three</li></ul>";
    private bool isEditing = false;
    
    private void ToggleEditMode()
    {
        isEditing = !isEditing;
    }
}
```

### Example 5: Image Upload Handling

```razor
<Editor @bind-Value="@content"
        UploadUrl="/api/upload"
        OnImageUpload="@HandleImageUpload"
        OnImageUploadError="@HandleImageUploadError" />

<div class="mt-3">
    <h5>Preview:</h5>
    <div class="border p-3 rounded">
        @((MarkupString)content)
    </div>
</div>

@if (!string.IsNullOrEmpty(uploadMessage))
{
    <div class="mt-2 @(uploadSuccess ? "text-success" : "text-danger")">
        @uploadMessage
    </div>
}

@code {
    private string content = "<p>Try uploading an image by clicking the image button in the toolbar.</p>";
    private string uploadMessage = "";
    private bool uploadSuccess = false;
    
    private void HandleImageUpload(EditorUploadEventArgs args)
    {
        uploadMessage = $"Image uploaded successfully: {args.FileName}";
        uploadSuccess = true;
    }
    
    private void HandleImageUploadError(EditorUploadEventArgs args)
    {
        uploadMessage = $"Failed to upload image: {args.Error}";
        uploadSuccess = false;
    }
}
```

### Example 6: Markdown Mode

```razor
<div class="mb-3">
    <Switch @bind-Value="@enableMarkdown" OnText="Markdown" OffText="Rich Text" />
</div>

<Editor @bind-Value="@content"
        EnableMarkdown="@enableMarkdown"
        Height="300" />

<div class="mt-3">
    <h5>Preview:</h5>
    <div class="border p-3 rounded">
        @if (enableMarkdown)
        {
            <MarkdownRenderer Content="@content" />
        }
        else
        {
            @((MarkupString)content)
        }
    </div>
</div>

@code {
    private string content = enableMarkdown ? 
        "# Heading\n\nThis is **bold** and *italic* text.\n\n- List item 1\n- List item 2\n- List item 3" :
        "<h1>Heading</h1><p>This is <strong>bold</strong> and <em>italic</em> text.</p><ul><li>List item 1</li><li>List item 2</li><li>List item 3</li></ul>";
    
    private bool enableMarkdown = false;
    
    protected override void OnParametersSet()
    {
        // Update content format when switching between modes
        if (enableMarkdown && content.StartsWith("<"))
        {
            // Convert HTML to Markdown (simplified example)
            content = "# Heading\n\nThis is **bold** and *italic* text.\n\n- List item 1\n- List item 2\n- List item 3";
        }
        else if (!enableMarkdown && !content.StartsWith("<"))
        {
            // Convert Markdown to HTML (simplified example)
            content = "<h1>Heading</h1><p>This is <strong>bold</strong> and <em>italic</em> text.</p><ul><li>List item 1</li><li>List item 2</li><li>List item 3</li></ul>";
        }
    }
    
    // Note: This example assumes a MarkdownRenderer component exists
    // In a real application, you would need to implement this or use a library
}
```

### Example 7: Collaborative Editing with SignalR

```razor
@inject NavigationManager NavigationManager
@implements IAsyncDisposable

<div class="mb-3">
    <label>Document ID: @documentId</label>
    <div>Users editing: @(connectedUsers.Count)</div>
</div>

<Editor @bind-Value="@content"
        Height="400"
        OnValueChanged="@HandleContentChanged" />

@code {
    private string content = "<p>This is a collaborative document. Changes made by any user will be visible to all.</p>";
    private string documentId = Guid.NewGuid().ToString();
    private HubConnection hubConnection;
    private List<string> connectedUsers = new List<string>();
    private bool isUpdatingFromRemote = false;
    
    protected override async Task OnInitializedAsync()
    {
        hubConnection = new HubConnectionBuilder()
            .WithUrl(NavigationManager.ToAbsoluteUri("/collaborationHub"))
            .Build();
            
        hubConnection.On<string, string, string>("ReceiveContentUpdate", (userId, docId, newContent) =>
        {
            if (docId == documentId && userId != hubConnection.ConnectionId)
            {
                isUpdatingFromRemote = true;
                content = newContent;
                isUpdatingFromRemote = false;
                StateHasChanged();
            }
        });
        
        hubConnection.On<string, string>("UserJoined", (userId, docId) =>
        {
            if (docId == documentId && !connectedUsers.Contains(userId))
            {
                connectedUsers.Add(userId);
                StateHasChanged();
            }
        });
        
        hubConnection.On<string, string>("UserLeft", (userId, docId) =>
        {
            if (docId == documentId && connectedUsers.Contains(userId))
            {
                connectedUsers.Remove(userId);
                StateHasChanged();
            }
        });
        
        await hubConnection.StartAsync();
        await hubConnection.SendAsync("JoinDocument", documentId);
    }
    
    private async Task HandleContentChanged(string newContent)
    {
        if (!isUpdatingFromRemote && hubConnection.State == HubConnectionState.Connected)
        {
            await hubConnection.SendAsync("UpdateContent", documentId, newContent);
        }
    }
    
    public async ValueTask DisposeAsync()
    {
        if (hubConnection is not null)
        {
            await hubConnection.SendAsync("LeaveDocument", documentId);
            await hubConnection.DisposeAsync();
        }
    }
    
    // Note: This example requires a SignalR hub implementation on the server
    // The hub would need to handle the JoinDocument, LeaveDocument, and UpdateContent methods
}
```

## Customization Notes

The Editor component can be customized using the following CSS variables:

```css
:root {
    --bb-editor-border-color: #ced4da;
    --bb-editor-border-radius: 0.25rem;
    --bb-editor-toolbar-background: #f8f9fa;
    --bb-editor-toolbar-border-color: #ced4da;
    --bb-editor-toolbar-button-color: #212529;
    --bb-editor-toolbar-button-hover-background: #e9ecef;
    --bb-editor-toolbar-button-active-background: #dee2e6;
    --bb-editor-toolbar-separator-color: #ced4da;
    --bb-editor-content-background: #fff;
    --bb-editor-content-color: #212529;
    --bb-editor-placeholder-color: #6c757d;
    --bb-editor-focus-border-color: #86b7fe;
    --bb-editor-focus-box-shadow: 0 0 0 0.25rem rgba(13, 110, 253, 0.25);
    --bb-editor-disabled-background: #e9ecef;
    --bb-editor-disabled-color: #6c757d;
    --bb-editor-readonly-background: #f8f9fa;
}
```

Additionally, you can customize the appearance and behavior of the Editor component by:

1. Using the `ToolbarItems` property to customize the toolbar buttons
2. Using the `Height`, `MinHeight`, and `MaxHeight` properties to control the editor size
3. Using the `EnableMarkdown`, `EnableSourceCode`, and `EnableFullscreen` properties to enable/disable specific features
4. Using the `UploadUrl` property to configure image upload handling
5. Using the `MaxLength` and `EnableWordCount` properties for content length management
6. Applying custom CSS classes to the component using the `ClassName` property