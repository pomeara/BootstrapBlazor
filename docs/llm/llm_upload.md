# Upload Component

## Overview
The Upload component in BootstrapBlazor provides a flexible and user-friendly interface for uploading files in Blazor applications. It supports various display modes, file validation, progress tracking, and customization options. This component is essential for scenarios where users need to upload images, documents, or other file types as part of form submissions or content management workflows.

## Features
- **Multiple Display Modes**: List, Card, Avatar, and Drag-and-Drop interfaces
- **File Type Validation**: Restrict uploads to specific file types
- **Size Limitations**: Set maximum file size constraints
- **Multiple File Support**: Upload single or multiple files
- **Progress Tracking**: Visual feedback during upload process
- **Preview Capabilities**: Image preview for supported file types
- **Custom Templates**: Customize the appearance of upload items
- **Drag and Drop**: Intuitive drag-and-drop file upload interface
- **Form Integration**: Works with form validation
- **Event Callbacks**: Events for monitoring upload status and completion
- **File Management**: Delete, download, and browse uploaded files
- **Accessibility Support**: Keyboard navigation and screen reader compatibility
- **Responsive Design**: Adapts to different screen sizes

## Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `DisplayMode` | `UploadMode` | `UploadMode.List` | Display mode of the upload component (List, Card, Avatar, Drop) |
| `AcceptFileTypes` | `string` | `null` | Accepted file types (e.g., ".jpg,.png,.gif") |
| `MaxFileSize` | `int` | `0` | Maximum file size in KB (0 means no limit) |
| `MaxFileCount` | `int` | `0` | Maximum number of files allowed (0 means no limit) |
| `IsMultiple` | `bool` | `false` | Whether multiple file selection is allowed |
| `ShowProgress` | `bool` | `true` | Whether to show upload progress |
| `ShowDeleteButton` | `bool` | `true` | Whether to show delete button for uploaded files |
| `ShowDownloadButton` | `bool` | `false` | Whether to show download button for uploaded files |
| `ShowUploadButton` | `bool` | `true` | Whether to show the upload button |
| `ShowPreviewImage` | `bool` | `true` | Whether to show image previews for supported file types |
| `IsDisabled` | `bool` | `false` | Whether the component is disabled |
| `IsDrop` | `bool` | `false` | Whether to enable drag-and-drop upload |
| `UploadUrl` | `string` | `null` | Server endpoint URL for file uploads |
| `Files` | `IEnumerable<UploadFile>` | `[]` | Collection of uploaded files |
| `IsCircle` | `bool` | `false` | Whether to display avatar uploads as circles (for Avatar mode) |
| `IsSingle` | `bool` | `false` | Whether to allow only a single file upload |
| `BrowserButtonText` | `string` | "Browse" | Text for the browse button |
| `UploadButtonText` | `string` | "Upload" | Text for the upload button |
| `PlaceHolder` | `string` | "Click or drag files here to upload" | Placeholder text for drag-drop area |
| `ItemTemplate` | `RenderFragment<UploadFile>` | `null` | Custom template for rendering upload items |

## Events

| Event | Description |
|-------|-------------|
| `OnChange` | Triggered when files are selected |
| `OnDelete` | Triggered when a file is deleted |
| `OnUpload` | Triggered when files start uploading |
| `OnUploaded` | Triggered when files are successfully uploaded |
| `OnError` | Triggered when an error occurs during upload |
| `OnProgress` | Triggered during upload progress |
| `OnValidate` | Triggered when validating files before upload |
| `OnDownload` | Triggered when a file is downloaded |

## Usage Examples

### Example 1: Basic File Upload

```razor
<Upload AcceptFileTypes=".jpg,.png,.gif"
        MaxFileSize="5120"
        OnChange="HandleFileChange"
        OnUploaded="HandleUploaded"
        OnError="HandleError" />

@code {
    private void HandleFileChange(IEnumerable<UploadFile> files)
    {
        // Handle selected files
        foreach (var file in files)
        {
            Console.WriteLine($"Selected file: {file.FileName}, Size: {file.Size} bytes");
        }
    }
    
    private Task HandleUploaded(IEnumerable<UploadFile> files)
    {
        // Handle successfully uploaded files
        foreach (var file in files)
        {
            Console.WriteLine($"Uploaded file: {file.FileName}, URL: {file.Url}");
        }
        return Task.CompletedTask;
    }
    
    private Task HandleError(string errorMessage)
    {
        // Handle upload errors
        Console.WriteLine($"Upload error: {errorMessage}");
        return Task.CompletedTask;
    }
}
```

### Example 2: Card Mode with Image Preview

```razor
<Upload DisplayMode="UploadMode.Card"
        AcceptFileTypes=".jpg,.png,.gif"
        MaxFileSize="5120"
        ShowPreviewImage="true"
        IsMultiple="true"
        MaxFileCount="5"
        OnChange="HandleFileChange"
        OnUploaded="HandleUploaded" />

@code {
    private void HandleFileChange(IEnumerable<UploadFile> files)
    {
        // Handle selected files
    }
    
    private Task HandleUploaded(IEnumerable<UploadFile> files)
    {
        // Handle successfully uploaded files
        return Task.CompletedTask;
    }
}
```

### Example 3: Avatar Upload

```razor
<Upload DisplayMode="UploadMode.Avatar"
        AcceptFileTypes=".jpg,.png,.gif"
        MaxFileSize="1024"
        IsCircle="true"
        IsSingle="true"
        ShowDeleteButton="true"
        OnChange="HandleFileChange"
        OnUploaded="HandleUploaded"
        OnDelete="HandleDelete" />

@code {
    private void HandleFileChange(IEnumerable<UploadFile> files)
    {
        // Handle selected file
    }
    
    private Task HandleUploaded(IEnumerable<UploadFile> files)
    {
        // Handle successfully uploaded file
        return Task.CompletedTask;
    }
    
    private Task HandleDelete(UploadFile file)
    {
        // Handle file deletion
        Console.WriteLine($"Deleted file: {file.FileName}");
        return Task.CompletedTask;
    }
}
```

### Example 4: Drag and Drop Upload

```razor
<Upload IsDrop="true"
        AcceptFileTypes=".jpg,.png,.pdf,.docx"
        MaxFileSize="10240"
        IsMultiple="true"
        PlaceHolder="Drag files here or click to upload"
        OnChange="HandleFileChange"
        OnUploaded="HandleUploaded"
        OnProgress="HandleProgress" />

@code {
    private void HandleFileChange(IEnumerable<UploadFile> files)
    {
        // Handle selected files
    }
    
    private Task HandleUploaded(IEnumerable<UploadFile> files)
    {
        // Handle successfully uploaded files
        return Task.CompletedTask;
    }
    
    private Task HandleProgress(UploadProgressEventArgs args)
    {
        // Handle upload progress
        Console.WriteLine($"File: {args.FileName}, Progress: {args.Percentage}%");
        return Task.CompletedTask;
    }
}
```

### Example 5: Custom Upload with Validation

```razor
<ValidateForm Model="@model" OnValidSubmit="HandleValidSubmit">
    <div class="mb-3">
        <BootstrapInput @bind-Value="@model.Title" 
                       ShowLabel="true" 
                       DisplayText="Title" 
                       Placeholder="Enter document title" />
        <ValidationMessage For="@(() => model.Title)" />
    </div>
    
    <div class="mb-3">
        <label>Document File</label>
        <Upload @bind-Files="@model.Documents"
                AcceptFileTypes=".pdf,.docx,.xlsx"
                MaxFileSize="5120"
                IsMultiple="false"
                ShowUploadButton="false"
                OnValidate="HandleValidate"
                IsValid="@(model.Documents.Any())"
                OnChange="HandleFileChange" />
        <ValidationMessage For="@(() => model.Documents)" />
    </div>
    
    <Button Type="ButtonType.Submit">Submit</Button>
</ValidateForm>

@code {
    private DocumentModel model = new();
    
    private Task HandleValidate(UploadFile file)
    {
        // Custom validation logic
        if (file.Size > 5 * 1024 * 1024) // 5MB
        {
            file.ValidateStatus = false;
            file.ValidateMessage = "File size exceeds 5MB limit";
        }
        else if (!new[] { ".pdf", ".docx", ".xlsx" }.Contains(Path.GetExtension(file.FileName).ToLower()))
        {
            file.ValidateStatus = false;
            file.ValidateMessage = "Only PDF, DOCX, and XLSX files are allowed";
        }
        else
        {
            file.ValidateStatus = true;
        }
        
        return Task.CompletedTask;
    }
    
    private void HandleFileChange(IEnumerable<UploadFile> files)
    {
        model.Documents = files.ToList();
    }
    
    private void HandleValidSubmit()
    {
        // Process the form data
        Console.WriteLine($"Title: {model.Title}");
        Console.WriteLine($"Document: {model.Documents.FirstOrDefault()?.FileName}");
    }
    
    public class DocumentModel
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; } = "";
        
        [Required(ErrorMessage = "Document file is required")]
        public List<UploadFile> Documents { get; set; } = new();
    }
}
```

### Example 6: Custom Item Template

```razor
<Upload DisplayMode="UploadMode.List"
        AcceptFileTypes=".jpg,.png,.pdf,.docx"
        MaxFileSize="10240"
        IsMultiple="true"
        OnChange="HandleFileChange"
        OnUploaded="HandleUploaded">
    <ItemTemplate>
        <div class="d-flex align-items-center p-2 border-bottom">
            <div class="me-3">
                @if (context.FileType.StartsWith("image/"))
                {
                    <img src="@context.Url" style="width: 40px; height: 40px; object-fit: cover;" class="rounded" />
                }
                else
                {
                    <i class="@GetFileIcon(context.FileName)" style="font-size: 24px;"></i>
                }
            </div>
            <div class="flex-grow-1">
                <div class="fw-bold">@context.FileName</div>
                <div class="small text-muted">@FormatFileSize(context.Size) - @context.UploadTime.ToString("yyyy-MM-dd HH:mm")</div>
            </div>
            <div>
                <Button Color="Color.Primary" Size="Size.Small" OnClick="() => DownloadFile(context)" Class="me-2">
                    <i class="fa-solid fa-download"></i>
                </Button>
                <Button Color="Color.Danger" Size="Size.Small" OnClick="() => DeleteFile(context)">
                    <i class="fa-solid fa-trash"></i>
                </Button>
            </div>
        </div>
    </ItemTemplate>
</Upload>

@code {
    private void HandleFileChange(IEnumerable<UploadFile> files)
    {
        // Handle selected files
    }
    
    private Task HandleUploaded(IEnumerable<UploadFile> files)
    {
        // Handle successfully uploaded files
        return Task.CompletedTask;
    }
    
    private string GetFileIcon(string fileName)
    {
        var extension = Path.GetExtension(fileName).ToLower();
        return extension switch
        {
            ".pdf" => "fa-solid fa-file-pdf text-danger",
            ".docx" or ".doc" => "fa-solid fa-file-word text-primary",
            ".xlsx" or ".xls" => "fa-solid fa-file-excel text-success",
            ".pptx" or ".ppt" => "fa-solid fa-file-powerpoint text-warning",
            _ => "fa-solid fa-file text-secondary"
        };
    }
    
    private string FormatFileSize(long bytes)
    {
        string[] sizes = { "B", "KB", "MB", "GB" };
        int order = 0;
        double size = bytes;
        while (size >= 1024 && order < sizes.Length - 1)
        {
            order++;
            size /= 1024;
        }
        return $"{size:0.##} {sizes[order]}";
    }
    
    private void DownloadFile(UploadFile file)
    {
        // Download file logic
    }
    
    private void DeleteFile(UploadFile file)
    {
        // Delete file logic
    }
}
```

### Example 7: Upload with Server Integration

```razor
<Upload UploadUrl="/api/upload"
        AcceptFileTypes=".jpg,.png,.pdf"
        MaxFileSize="10240"
        IsMultiple="true"
        ShowProgress="true"
        OnChange="HandleFileChange"
        OnUploaded="HandleUploaded"
        OnError="HandleError"
        OnProgress="HandleProgress">
    <UploadButtonTemplate>
        <Button Color="Color.Primary">
            <i class="fa-solid fa-cloud-upload-alt me-2"></i>Upload Files
        </Button>
    </UploadButtonTemplate>
</Upload>

<div class="mt-3">
    <h5>Upload Status</h5>
    @if (uploadStatus.Any())
    {
        <div class="list-group">
            @foreach (var status in uploadStatus)
            {
                <div class="list-group-item d-flex justify-content-between align-items-center">
                    <div>
                        <span class="@(status.Success ? "text-success" : "text-danger")">
                            <i class="@(status.Success ? "fa-solid fa-check-circle" : "fa-solid fa-times-circle")"></i>
                        </span>
                        <span class="ms-2">@status.FileName</span>
                    </div>
                    <span class="@(status.Success ? "badge bg-success" : "badge bg-danger")">@status.Message</span>
                </div>
            }
        </div>
    }
    else
    {
        <p class="text-muted">No uploads yet</p>
    }
</div>

@code {
    private List<UploadStatus> uploadStatus = new();
    
    private void HandleFileChange(IEnumerable<UploadFile> files)
    {
        // Clear previous status
        uploadStatus.Clear();
    }
    
    private Task HandleUploaded(IEnumerable<UploadFile> files)
    {
        foreach (var file in files)
        {
            uploadStatus.Add(new UploadStatus
            {
                FileName = file.FileName,
                Success = true,
                Message = "Uploaded successfully"
            });
        }
        return Task.CompletedTask;
    }
    
    private Task HandleError(string errorMessage)
    {
        uploadStatus.Add(new UploadStatus
        {
            FileName = "Unknown",
            Success = false,
            Message = errorMessage
        });
        return Task.CompletedTask;
    }
    
    private Task HandleProgress(UploadProgressEventArgs args)
    {
        // Update progress
        return Task.CompletedTask;
    }
    
    private class UploadStatus
    {
        public string FileName { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
```

## CSS Customization

The Upload component can be customized using CSS variables:

```css
/* Custom styles for Upload component */
.upload {
    --bb-upload-body-margin-top: 1rem;
    --bb-upload-body-list-max-height: 300px;
    --bb-upload-body-list-item-padding: 0.5rem;
    --bb-upload-body-list-item-body-padding: 0 0.5rem;
    --bb-upload-body-list-item-hover-color: #0d6efd;
    --bb-upload-card-width: 148px;
    --bb-upload-card-height: 148px;
    --bb-upload-card-shadow: 0 1px 3px rgba(0,0,0,0.12);
    --bb-upload-card-padding: 0.5rem;
    --bb-upload-card-margin: 0 0.5rem 0.5rem 0;
    --bb-upload-card-item-width: 120px;
    --bb-upload-drop-height: 180px;
    --bb-upload-drop-footer-font-size: 0.75rem;
    --bb-upload-drop-footer-margin-top: 0.5rem;
}

/* Custom list item styling */
.upload .upload-body.is-list .upload-item {
    /* List item styling */
}

/* Custom card styling */
.upload .upload-body.is-card .upload-item {
    /* Card item styling */
}

/* Custom avatar styling */
.upload .upload-body.is-avatar .upload-item {
    /* Avatar item styling */
}

/* Custom drop area styling */
.upload.is-drop .upload-drop-body {
    /* Drop area styling */
}
```

## Accessibility

The Upload component follows accessibility best practices:

- Proper ARIA attributes for screen readers
- Keyboard navigation support
- Focus management for interactive elements
- High contrast mode support
- Screen reader announcements for upload status

## Browser Compatibility

The Upload component is compatible with all modern browsers:

- Chrome
- Firefox
- Edge
- Safari
- Opera

## Integration with Other Components

The Upload component can be integrated with various other BootstrapBlazor components:

- Use with Form and ValidateForm for data collection
- Combine with Button for custom upload triggers
- Integrate with Modal or Drawer for popup upload interfaces
- Pair with Alert or Toast for upload notifications
- Use with Card for styled containers