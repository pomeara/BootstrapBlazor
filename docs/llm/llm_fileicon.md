# FileIcon Component

## Overview
The FileIcon component in BootstrapBlazor provides a visual representation of different file types through icons. It automatically displays appropriate icons based on file extensions, enhancing the user interface when working with file systems, document management, or file upload features.

## Key Features
- Automatic icon selection based on file extension
- Support for common file types (documents, images, videos, etc.)
- Customizable icon size and appearance
- Optional file name display
- Support for custom icon mappings
- Fallback icon for unknown file types
- Tooltip support for additional information
- Click event handling for file interaction

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `FileName` | `string` | `null` | The file name or path used to determine the appropriate icon |
| `Extension` | `string` | `null` | File extension to use for icon selection (overrides extension from FileName) |
| `Size` | `IconSize` | `IconSize.Normal` | Size of the file icon (Small, Normal, Large, ExtraLarge) |
| `ShowFileName` | `bool` | `false` | Whether to display the file name alongside the icon |
| `ShowExtension` | `bool` | `true` | Whether to display the file extension in the file name |
| `MaxFileNameLength` | `int` | `20` | Maximum length of displayed file name before truncation |
| `CustomIconMap` | `Dictionary<string, string>` | `null` | Custom mapping of file extensions to icon classes |
| `FallbackIcon` | `string` | `"fa fa-file"` | Icon to display for unknown file types |
| `ShowTooltip` | `bool` | `true` | Whether to show tooltip with file information on hover |
| `TooltipText` | `string` | `null` | Custom tooltip text (defaults to file name if null) |
| `IsDisabled` | `bool` | `false` | Whether the file icon is in a disabled state |
| `OnClick` | `EventCallback<MouseEventArgs>` | `null` | Event callback for when the file icon is clicked |

## Events

| Event | Description |
| --- | --- |
| `OnFileIconClick` | Triggered when the file icon is clicked |

## Usage Examples

### Example 1: Basic File Icon Display

```razor
@page "/file-icon-demo"

<div class="file-icon-demo">
    <h5>Common File Types</h5>
    <div class="d-flex flex-wrap gap-4">
        <FileIcon FileName="document.pdf" />
        <FileIcon FileName="spreadsheet.xlsx" />
        <FileIcon FileName="presentation.pptx" />
        <FileIcon FileName="image.jpg" />
        <FileIcon FileName="video.mp4" />
        <FileIcon FileName="archive.zip" />
        <FileIcon FileName="code.cs" />
        <FileIcon FileName="text.txt" />
    </div>
</div>
```

### Example 2: File Icons with Names

```razor
@page "/file-icon-with-name-demo"

<div class="file-icon-demo">
    <h5>File Icons with Names</h5>
    <div class="d-flex flex-wrap gap-4">
        <FileIcon FileName="project-report.pdf" ShowFileName="true" />
        <FileIcon FileName="financial-data.xlsx" ShowFileName="true" />
        <FileIcon FileName="company-presentation.pptx" ShowFileName="true" MaxFileNameLength="15" />
        <FileIcon FileName="product-image.png" ShowFileName="true" />
    </div>
</div>
```

### Example 3: Different Icon Sizes

```razor
@page "/file-icon-sizes-demo"

<div class="file-icon-demo">
    <h5>Small Icons</h5>
    <div class="d-flex flex-wrap gap-3 align-items-center mb-4">
        <FileIcon FileName="document.pdf" Size="IconSize.Small" />
        <FileIcon FileName="spreadsheet.xlsx" Size="IconSize.Small" />
        <FileIcon FileName="image.jpg" Size="IconSize.Small" />
        <FileIcon FileName="video.mp4" Size="IconSize.Small" />
    </div>
    
    <h5>Normal Icons (Default)</h5>
    <div class="d-flex flex-wrap gap-3 align-items-center mb-4">
        <FileIcon FileName="document.pdf" />
        <FileIcon FileName="spreadsheet.xlsx" />
        <FileIcon FileName="image.jpg" />
        <FileIcon FileName="video.mp4" />
    </div>
    
    <h5>Large Icons</h5>
    <div class="d-flex flex-wrap gap-3 align-items-center mb-4">
        <FileIcon FileName="document.pdf" Size="IconSize.Large" />
        <FileIcon FileName="spreadsheet.xlsx" Size="IconSize.Large" />
        <FileIcon FileName="image.jpg" Size="IconSize.Large" />
        <FileIcon FileName="video.mp4" Size="IconSize.Large" />
    </div>
    
    <h5>Extra Large Icons</h5>
    <div class="d-flex flex-wrap gap-3 align-items-center">
        <FileIcon FileName="document.pdf" Size="IconSize.ExtraLarge" />
        <FileIcon FileName="spreadsheet.xlsx" Size="IconSize.ExtraLarge" />
        <FileIcon FileName="image.jpg" Size="IconSize.ExtraLarge" />
        <FileIcon FileName="video.mp4" Size="IconSize.ExtraLarge" />
    </div>
</div>
```

### Example 4: Custom Icon Mappings

```razor
@page "/custom-file-icon-demo"

<div class="file-icon-demo">
    <h5>Custom File Icon Mappings</h5>
    <div class="d-flex flex-wrap gap-4">
        @foreach (var file in customFiles)
        {
            <div class="text-center">
                <FileIcon FileName="@file" 
                         ShowFileName="true"
                         CustomIconMap="@customIconMap" />
            </div>
        }
    </div>
</div>

@code {
    private List<string> customFiles = new List<string>
    {
        "config.json",
        "data.csv",
        "script.ps1",
        "styles.scss",
        "docker-compose.yml"
    };
    
    private Dictionary<string, string> customIconMap = new Dictionary<string, string>
    {
        { ".json", "fa fa-cogs" },
        { ".csv", "fa fa-table" },
        { ".ps1", "fa fa-terminal" },
        { ".scss", "fa fa-paint-brush" },
        { ".yml", "fa fa-ship" }
    };
}
```

### Example 5: File Icons with Click Handling

```razor
@page "/interactive-file-icon-demo"

<div class="file-icon-demo">
    <h5>Interactive File Icons</h5>
    <div class="d-flex flex-wrap gap-4 mb-4">
        @foreach (var file in documentFiles)
        {
            <div class="text-center">
                <FileIcon FileName="@file" 
                         ShowFileName="true"
                         OnClick="@(() => HandleFileClick(file))" />
            </div>
        }
    </div>
    
    @if (!string.IsNullOrEmpty(selectedFile))
    {
        <Alert ShowDismiss="true" Color="Color.Info">
            <div>You selected: <strong>@selectedFile</strong></div>
        </Alert>
    }
</div>

@code {
    private List<string> documentFiles = new List<string>
    {
        "annual-report.pdf",
        "budget.xlsx",
        "meeting-notes.docx",
        "presentation.pptx",
        "readme.txt"
    };
    
    private string selectedFile;
    
    private void HandleFileClick(string fileName)
    {
        selectedFile = fileName;
    }
}
```

### Example 6: File Icons in a File Browser

```razor
@page "/file-browser-demo"

<Card>
    <HeaderTemplate>
        <div class="d-flex justify-content-between align-items-center">
            <div>File Browser</div>
            <div>
                <Button Color="Color.Primary" Icon="fa fa-upload" Text="Upload" />
                <Button Color="Color.Secondary" Icon="fa fa-folder-plus" Text="New Folder" />
            </div>
        </div>
    </HeaderTemplate>
    <BodyTemplate>
        <div class="file-browser">
            <div class="folder-path mb-3 p-2 bg-light rounded">
                <i class="fa fa-folder-open me-2"></i> /Documents/Projects
            </div>
            
            <div class="folder-section mb-4">
                <h6><i class="fa fa-folder me-2"></i> Folders</h6>
                <div class="d-flex flex-wrap gap-4">
                    @foreach (var folder in folders)
                    {
                        <div class="text-center folder-item">
                            <i class="fa fa-folder fa-3x mb-2 text-warning"></i>
                            <div>@folder</div>
                        </div>
                    }
                </div>
            </div>
            
            <div class="files-section">
                <h6><i class="fa fa-file me-2"></i> Files</h6>
                <div class="d-flex flex-wrap gap-4">
                    @foreach (var file in files)
                    {
                        <div class="text-center file-item">
                            <FileIcon FileName="@file" 
                                     Size="IconSize.Large"
                                     ShowFileName="true" />
                        </div>
                    }
                </div>
            </div>
        </div>
    </BodyTemplate>
</Card>

<style>
    .folder-item, .file-item {
        width: 100px;
        cursor: pointer;
        padding: 8px;
        border-radius: 4px;
    }
    
    .folder-item:hover, .file-item:hover {
        background-color: #f8f9fa;
    }
</style>

@code {
    private List<string> folders = new List<string>
    {
        "Marketing",
        "Development",
        "Research",
        "Resources"
    };
    
    private List<string> files = new List<string>
    {
        "project-plan.docx",
        "budget.xlsx",
        "presentation.pptx",
        "logo.png",
        "requirements.pdf",
        "notes.txt",
        "data.csv",
        "demo.mp4"
    };
}
```

### Example 7: File Icons in Upload Component

```razor
@page "/upload-with-icons-demo"

<div class="upload-demo">
    <h5>File Upload with Preview Icons</h5>
    
    <div class="upload-zone p-4 mb-4 border rounded text-center">
        <i class="fa fa-cloud-upload-alt fa-3x mb-3 text-primary"></i>
        <p>Drag files here or click to browse</p>
        <InputFile OnChange="@HandleFileSelection" multiple class="d-none" id="fileInput" />
        <Button Color="Color.Primary" OnClick="@(() => JSRuntime.InvokeVoidAsync("document.getElementById('fileInput').click()"))">
            Browse Files
        </Button>
    </div>
    
    @if (selectedFiles.Any())
    {
        <h6>Selected Files</h6>
        <div class="selected-files">
            @foreach (var file in selectedFiles)
            {
                <div class="file-item d-flex align-items-center p-2 border-bottom">
                    <FileIcon FileName="@file.Name" Size="IconSize.Small" />
                    <div class="ms-3 flex-grow-1">
                        <div>@file.Name</div>
                        <small class="text-muted">@FormatFileSize(file.Size)</small>
                    </div>
                    <Button Color="Color.Danger" Icon="fa fa-times" Circle="true" Size="Size.Small"
                            OnClick="@(() => RemoveFile(file))" />
                </div>
            }
        </div>
        
        <div class="mt-3 d-flex justify-content-end">
            <Button Color="Color.Success" Icon="fa fa-upload" Text="Upload Files" />
        </div>
    }
</div>

@code {
    private List<UploadFile> selectedFiles = new List<UploadFile>();
    
    private async Task HandleFileSelection(InputFileChangeEventArgs e)
    {
        foreach (var file in e.GetMultipleFiles())
        {
            selectedFiles.Add(new UploadFile
            {
                Name = file.Name,
                Size = file.Size,
                ContentType = file.ContentType
            });
        }
    }
    
    private void RemoveFile(UploadFile file)
    {
        selectedFiles.Remove(file);
    }
    
    private string FormatFileSize(long bytes)
    {
        string[] sizes = { "B", "KB", "MB", "GB" };
        int order = 0;
        double size = bytes;
        
        while (size >= 1024 && order < sizes.Length - 1)
        {
            order++;
            size = size / 1024;
        }
        
        return $"{size:0.##} {sizes[order]}";
    }
    
    private class UploadFile
    {
        public string Name { get; set; }
        public long Size { get; set; }
        public string ContentType { get; set; }
    }
}
```

## CSS Customization

The FileIcon component can be customized using CSS variables and classes:

```css
/* Custom FileIcon styling */
.bb-file-icon {
    --bb-file-icon-size-small: 16px;
    --bb-file-icon-size-normal: 24px;
    --bb-file-icon-size-large: 36px;
    --bb-file-icon-size-extra-large: 48px;
    
    --bb-file-icon-color-pdf: #f40f02;
    --bb-file-icon-color-word: #2b579a;
    --bb-file-icon-color-excel: #217346;
    --bb-file-icon-color-powerpoint: #d24726;
    --bb-file-icon-color-image: #0066ff;
    --bb-file-icon-color-video: #ff0066;
    --bb-file-icon-color-audio: #ffcc00;
    --bb-file-icon-color-archive: #ffa500;
    --bb-file-icon-color-code: #333333;
    --bb-file-icon-color-text: #666666;
    --bb-file-icon-color-unknown: #999999;
    
    --bb-file-icon-font-size: 0.875rem;
    --bb-file-icon-font-weight: normal;
    --bb-file-icon-text-color: #212529;
    --bb-file-icon-disabled-opacity: 0.65;
}
```

## JavaScript Interop

The FileIcon component uses JavaScript interop for the following operations:
- Tooltip initialization and management
- Click event handling

## Accessibility

The FileIcon component includes the following accessibility features:
- Proper ARIA attributes for screen readers
- Keyboard navigation support
- Descriptive tooltips for additional context

## Browser Compatibility

The FileIcon component is compatible with all modern browsers including:
- Chrome
- Firefox
- Safari
- Edge

## Performance Considerations

- For large file lists, consider implementing virtualization
- Use appropriate icon sizes based on the context
- Limit the number of custom icon mappings to improve rendering performance

## Integration with Other Components

The FileIcon component works well with:
- Upload components for file selection previews
- Table components for file listings
- Card components for file galleries
- List components for file browsers

## Best Practices

1. Use appropriate icon sizes based on the context
2. Show file names for better user understanding
3. Implement click handlers for interactive file operations
4. Use custom icon mappings for specialized file types
5. Provide tooltips with additional file information
6. Consider accessibility when implementing file interactions
7. Use consistent icon styles throughout the application