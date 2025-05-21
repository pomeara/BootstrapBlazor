# Download Component

## Overview
The Download component in BootstrapBlazor provides a convenient way to trigger file downloads in web applications. It allows users to download files from the server or dynamically generated content without page navigation, supporting various file types and download methods.

## Key Features
- Direct file downloads from server paths
- Dynamic content generation for downloads
- Support for various file types and formats
- Customizable download buttons/links
- Progress tracking for large file downloads
- Configurable file naming
- Event callbacks for download lifecycle
- Support for authentication and authorization
- Batch download capabilities

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `Url` | string | null | The URL or path of the file to download |
| `FileName` | string | null | The name of the file when saved to the user's device |
| `Text` | string | "Download" | The text displayed on the download button/link |
| `Icon` | string | "fa fa-download" | The icon displayed on the download button/link |
| `Color` | Color | Primary | The color of the download button |
| `IsButton` | bool | true | Determines whether to render as a button or link |
| `IsOutline` | bool | false | Renders the button with an outline style when true |
| `Size` | Size | Medium | The size of the download button/link |
| `IsAsync` | bool | false | Enables asynchronous download when true |
| `ShowProgress` | bool | false | Shows a progress indicator for the download |
| `ContentType` | string | null | The MIME type of the file being downloaded |
| `BeforeDownload` | Func<Task<bool>> | null | Callback executed before download starts, can cancel download |
| `OnError` | Func<Exception, Task> | null | Callback executed when an error occurs during download |
| `OnComplete` | Func<Task> | null | Callback executed when download completes |

## Events

| Event | Description |
| --- | --- |
| `OnBeforeDownload` | Triggered before the download starts |
| `OnDownloadError` | Triggered when an error occurs during download |
| `OnDownloadComplete` | Triggered when the download completes successfully |
| `OnProgress` | Triggered during download to report progress |

## Usage Examples

### Example 1: Basic File Download

```razor
@page "/download-demo"

<Download Url="/files/sample.pdf" FileName="sample.pdf" Text="Download PDF" />
```

### Example 2: Customized Download Button

```razor
@page "/custom-download"

<Download Url="/files/data.xlsx"
         FileName="exported-data.xlsx"
         Text="Export Data"
         Icon="fa fa-file-excel"
         Color="Color.Success"
         Size="Size.Large"
         IsOutline="true" />
```

### Example 3: Dynamic Content Download

```razor
@page "/dynamic-download"
@inject IJSRuntime JSRuntime

<Button OnClick="GenerateAndDownload">Generate and Download Report</Button>

@code {
    private async Task GenerateAndDownload()
    {
        // Generate CSV content
        var csvContent = "Name,Age,Email\n";
        csvContent += "John Doe,30,john@example.com\n";
        csvContent += "Jane Smith,25,jane@example.com\n";
        csvContent += "Bob Johnson,45,bob@example.com\n";
        
        // Convert to byte array
        var bytes = System.Text.Encoding.UTF8.GetBytes(csvContent);
        
        // Create a base64 string
        var base64 = Convert.ToBase64String(bytes);
        
        // Trigger download using JavaScript
        await JSRuntime.InvokeVoidAsync("downloadFile", "report.csv", "text/csv", base64);
    }
}

@code {
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            // Add JavaScript function for dynamic downloads
            await JSRuntime.InvokeVoidAsync("eval", @"
                window.downloadFile = function(filename, contentType, base64) {
                    var link = document.createElement('a');
                    link.href = 'data:' + contentType + ';base64,' + base64;
                    link.download = filename;
                    document.body.appendChild(link);
                    link.click();
                    document.body.removeChild(link);
                };
            ");
        }
    }
}
```

### Example 4: Download with Progress Tracking

```razor
@page "/download-progress"
@inject DownloadService DownloadService

<Download @ref="download"
         Url="/files/large-file.zip"
         FileName="large-file.zip"
         Text="Download Large File"
         IsAsync="true"
         ShowProgress="true"
         OnProgress="UpdateProgress"
         OnComplete="DownloadComplete"
         OnError="HandleError" />

<div class="mt-3">
    @if (isDownloading)
    {
        <Progress Value="@progressValue" ShowPercent="true" />
        <div class="text-muted">@progressText</div>
    }
</div>

@code {
    private Download download;
    private bool isDownloading;
    private int progressValue;
    private string progressText;

    private Task UpdateProgress(ProgressEventArgs args)
    {
        isDownloading = true;
        progressValue = args.Percentage;
        progressText = $"Downloaded {args.Loaded / 1024:N0} KB of {args.Total / 1024:N0} KB";
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task DownloadComplete()
    {
        isDownloading = false;
        progressText = "Download complete!";
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task HandleError(Exception ex)
    {
        isDownloading = false;
        progressText = $"Error: {ex.Message}";
        StateHasChanged();
        return Task.CompletedTask;
    }
}
```

### Example 5: Conditional Download with Authentication

```razor
@page "/secure-download"
@inject AuthenticationStateProvider AuthenticationStateProvider
@inject IToastService ToastService

<Download Url="/api/secure-files/confidential.pdf"
         FileName="confidential.pdf"
         Text="Download Confidential Document"
         BeforeDownload="CheckPermission"
         OnError="HandleSecureError" />

@code {
    private async Task<bool> CheckPermission()
    {
        var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;
        
        if (user.Identity.IsAuthenticated && user.IsInRole("Administrators"))
        {
            return true; // Allow download
        }
        else
        {
            ToastService.ShowError("You don't have permission to download this file.");
            return false; // Prevent download
        }
    }

    private Task HandleSecureError(Exception ex)
    {
        ToastService.ShowError($"Download error: {ex.Message}");
        return Task.CompletedTask;
    }
}
```

### Example 6: Multiple File Download Options

```razor
@page "/multiple-downloads"

<Card Title="Download Documentation">
    <Body>
        <p>Please select the documentation format you prefer:</p>
        
        <div class="d-flex flex-wrap gap-2">
            <Download Url="/docs/manual.pdf"
                     FileName="user-manual.pdf"
                     Text="PDF Format"
                     Icon="fa fa-file-pdf"
                     Color="Color.Danger" />
                     
            <Download Url="/docs/manual.docx"
                     FileName="user-manual.docx"
                     Text="Word Format"
                     Icon="fa fa-file-word"
                     Color="Color.Primary" />
                     
            <Download Url="/docs/manual.epub"
                     FileName="user-manual.epub"
                     Text="EPUB Format"
                     Icon="fa fa-book"
                     Color="Color.Success" />
        </div>
    </Body>
</Card>
```

### Example 7: Batch Download with Service Integration

```razor
@page "/batch-download"
@inject DownloadService DownloadService
@inject MessageService MessageService

<Table TItem="FileItem" Items="@files">
    <TableColumns>
        <TableColumn @bind-Field="@context.Name" />
        <TableColumn @bind-Field="@context.Size" />
        <TableColumn @bind-Field="@context.Type" />
        <TableColumn Text="Actions">
            <Button Size="Size.Small" OnClick="() => DownloadSingleFile(context)">
                <i class="fa fa-download"></i> Download
            </Button>
        </TableColumn>
    </TableColumns>
</Table>

<div class="mt-3">
    <Button Color="Color.Primary" OnClick="DownloadSelected" Disabled="@(!HasSelectedFiles)">
        <i class="fa fa-download"></i> Download Selected Files
    </Button>
    <Button Color="Color.Success" OnClick="DownloadAll">
        <i class="fa fa-download"></i> Download All Files
    </Button>
</div>

@code {
    private List<FileItem> files = new List<FileItem>();
    private bool HasSelectedFiles => files.Any(f => f.IsSelected);

    protected override void OnInitialized()
    {
        // Sample file list
        files = new List<FileItem>
        {
            new FileItem { Id = 1, Name = "Report.pdf", Size = "2.5 MB", Type = "PDF", Path = "/files/report.pdf", IsSelected = false },
            new FileItem { Id = 2, Name = "Presentation.pptx", Size = "5.8 MB", Type = "PowerPoint", Path = "/files/presentation.pptx", IsSelected = false },
            new FileItem { Id = 3, Name = "Data.xlsx", Size = "1.2 MB", Type = "Excel", Path = "/files/data.xlsx", IsSelected = false },
            new FileItem { Id = 4, Name = "Image.jpg", Size = "3.7 MB", Type = "Image", Path = "/files/image.jpg", IsSelected = false }
        };
    }

    private async Task DownloadSingleFile(FileItem file)
    {
        try
        {
            await DownloadService.DownloadFileAsync(file.Path, file.Name);
            await MessageService.Show($"{file.Name} download started");
        }
        catch (Exception ex)
        {
            await MessageService.Show($"Error: {ex.Message}", MessageType.Error);
        }
    }

    private async Task DownloadSelected()
    {
        var selectedFiles = files.Where(f => f.IsSelected).ToList();
        if (selectedFiles.Any())
        {
            try
            {
                await DownloadService.DownloadFilesAsync(selectedFiles.Select(f => new FileDownloadInfo
                {
                    Url = f.Path,
                    FileName = f.Name
                }).ToList());
                
                await MessageService.Show($"{selectedFiles.Count} files download started");
            }
            catch (Exception ex)
            {
                await MessageService.Show($"Error: {ex.Message}", MessageType.Error);
            }
        }
    }

    private async Task DownloadAll()
    {
        try
        {
            await DownloadService.DownloadFilesAsync(files.Select(f => new FileDownloadInfo
            {
                Url = f.Path,
                FileName = f.Name
            }).ToList());
            
            await MessageService.Show($"All files download started");
        }
        catch (Exception ex)
        {
            await MessageService.Show($"Error: {ex.Message}", MessageType.Error);
        }
    }

    public class FileItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public string Type { get; set; }
        public string Path { get; set; }
        public bool IsSelected { get; set; }
    }

    public class FileDownloadInfo
    {
        public string Url { get; set; }
        public string FileName { get; set; }
    }
}
```

## CSS Customization

The Download component can be customized using CSS variables and classes:

```css
/* Custom download button styling */
.download-button {
    --bb-download-btn-bg: #4a6cf7;
    --bb-download-btn-color: #ffffff;
    --bb-download-btn-border: none;
    --bb-download-btn-hover-bg: #3a5ce5;
    --bb-download-btn-hover-color: #ffffff;
    --bb-download-btn-active-bg: #2a4cd3;
    --bb-download-btn-active-color: #ffffff;
    --bb-download-btn-disabled-bg: #a0aec0;
    --bb-download-btn-disabled-color: #e2e8f0;
    --bb-download-btn-border-radius: 4px;
    --bb-download-btn-padding: 0.5rem 1rem;
    --bb-download-btn-font-weight: 500;
    --bb-download-btn-transition: all 0.2s ease-in-out;
    --bb-download-btn-box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
    --bb-download-btn-hover-box-shadow: 0 4px 8px rgba(0, 0, 0, 0.15);
}

/* Progress bar styling */
.download-progress {
    --bb-download-progress-height: 8px;
    --bb-download-progress-bg: #e2e8f0;
    --bb-download-progress-fill-bg: #4a6cf7;
    --bb-download-progress-border-radius: 4px;
    --bb-download-progress-transition: width 0.3s ease;
}
```

## JavaScript Interop

The Download component uses JavaScript interop for the following operations:
- Triggering file downloads
- Handling dynamic content downloads
- Tracking download progress
- Managing batch downloads

## Accessibility

The Download component includes the following accessibility features:
- Proper ARIA attributes for screen readers
- Keyboard navigation support
- Focus management
- Visual indicators for download states

## Browser Compatibility

The Download component is compatible with all modern browsers including:
- Chrome
- Firefox
- Safari
- Edge

Note: Internet Explorer has limited support for some advanced features like progress tracking.

## Performance Considerations

- For large files, use the `IsAsync` property to prevent UI freezing
- Consider implementing server-side compression for large downloads
- Use batch downloads carefully as they can consume significant bandwidth
- Implement caching mechanisms for frequently downloaded files

## Integration with Other Components

The Download component works well with:
- Button components for custom download triggers
- Table/DataGrid components for file listings
- Progress components for download tracking
- Toast/Message components for notifications

## Best Practices

1. Always provide meaningful file names using the `FileName` property
2. Set appropriate MIME types with the `ContentType` property
3. Implement error handling for download failures
4. Use progress indicators for large file downloads
5. Consider user permissions before allowing downloads
6. Provide alternative download formats when applicable
7. Implement rate limiting for batch downloads
8. Use secure URLs for sensitive file downloads