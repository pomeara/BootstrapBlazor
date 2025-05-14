# IFrame Component

## Overview
The IFrame component in BootstrapBlazor provides a wrapper around the HTML `<iframe>` element, allowing developers to embed external content or pages within a Blazor application. This component simplifies the process of working with iframes by providing a clean API for setting attributes, handling events, and managing iframe content. It's particularly useful for integrating third-party content, displaying documents, embedding maps, or creating sandboxed environments within your application.

## Features
- **External Content Integration**: Embed external websites, documents, or resources
- **Responsive Design**: Automatically adjust iframe dimensions based on container size
- **Event Handling**: Capture and respond to iframe loading and interaction events
- **Security Controls**: Configure sandbox permissions and content security policies
- **Attribute Management**: Easily set and update iframe attributes
- **Dynamic Source**: Change the iframe source dynamically based on application state
- **Loading Indicators**: Show loading states while iframe content is being loaded
- **Error Handling**: Gracefully handle loading errors and provide fallback content
- **Two-way Communication**: Facilitate communication between the parent application and iframe content

## Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `Src` | string | "" | The URL of the content to embed in the iframe |
| `SrcDoc` | string | null | HTML content to display in the iframe (alternative to Src) |
| `Width` | string | "100%" | Width of the iframe |
| `Height` | string | "400px" | Height of the iframe |
| `AllowFullScreen` | bool | false | Whether to allow the iframe to be displayed in fullscreen mode |
| `Sandbox` | string | null | Security restrictions for the iframe content |
| `Allow` | string | null | Feature policy for the iframe |
| `Name` | string | null | Name of the iframe for targeting in forms or scripts |
| `Title` | string | null | Title attribute for accessibility |
| `Loading` | string | null | Loading strategy ("eager" or "lazy") |
| `ReferrerPolicy` | string | null | Referrer policy for the iframe |
| `ShowBorder` | bool | true | Whether to display a border around the iframe |
| `ShowLoading` | bool | true | Whether to show a loading indicator while content is loading |
| `LoadingText` | string | "Loading..." | Text to display while the iframe is loading |
| `ErrorText` | string | "Failed to load content" | Text to display if the iframe fails to load |
| `Class` | string | null | Additional CSS classes to apply to the iframe container |
| `Style` | string | null | Additional inline styles to apply to the iframe container |
| `Attributes` | Dictionary<string, object> | new() | Additional attributes to apply to the iframe element |

## Events

| Event | Description |
|-------|-------------|
| `OnLoad` | Triggered when the iframe content has finished loading |
| `OnError` | Triggered when the iframe content fails to load |
| `OnBeforeUnload` | Triggered before the iframe content is unloaded |
| `OnResize` | Triggered when the iframe is resized |
| `OnMessage` | Triggered when the iframe sends a message to the parent window |

## Usage Examples

### Example 1: Basic IFrame Usage
```razor
<IFrame Src="https://www.example.com"
        Width="100%"
        Height="500px"
        AllowFullScreen="true"
        OnLoad="() => Console.WriteLine("IFrame loaded successfully")" />
```

### Example 2: IFrame with Loading Indicator and Error Handling
```razor
<IFrame Src="@iframeUrl"
        Width="100%"
        Height="600px"
        ShowLoading="true"
        LoadingText="Loading external content..."
        OnLoad="HandleIFrameLoaded"
        OnError="HandleIFrameError" />

@code {
    private string iframeUrl = "https://www.example.com/embed";
    
    private void HandleIFrameLoaded()
    {
        Console.WriteLine("IFrame content loaded successfully");
    }
    
    private void HandleIFrameError()
    {
        Console.WriteLine("Failed to load IFrame content");
        // Optionally set a fallback URL
        iframeUrl = "https://www.example.com/fallback";
    }
}
```

### Example 3: Secure IFrame with Sandbox Restrictions
```razor
<IFrame Src="https://external-content.com"
        Width="100%"
        Height="500px"
        Sandbox="allow-scripts allow-same-origin"
        ReferrerPolicy="no-referrer"
        Allow="camera 'none'; microphone 'none'; geolocation 'none'"
        Title="Securely embedded external content" />
```

### Example 4: Responsive IFrame with Dynamic Content
```razor
@inject IJSRuntime JSRuntime

<div class="iframe-container" style="position: relative; width: 100%; height: 0; padding-bottom: 56.25%;">
    <IFrame @ref="responsiveIFrame"
            Src="@currentSource"
            Style="position: absolute; top: 0; left: 0; width: 100%; height: 100%;"
            AllowFullScreen="true"
            OnLoad="HandleIFrameLoaded" />
</div>

<div class="mt-3">
    <Button Text="Load Map" OnClick="() => ChangeSource('https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d387193.3059353029!2d-74.25986548248684!3d40.69714941680757!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x89c24fa5d33f083b%3A0xc80b8f06e177fe62!2sNew%20York%2C%20NY%2C%20USA!5e0!3m2!1sen!2s!4v1627309750687!5m2!1sen!2s')" />
    <Button Text="Load Video" OnClick="() => ChangeSource('https://www.youtube.com/embed/dQw4w9WgXcQ')" />
    <Button Text="Load Document" OnClick="() => ChangeSource('https://docs.google.com/document/d/e/2PACX-1vT1uZRkGnLZzqZr4Ht4QQsWDk_Lpq-kgXXdTbWUZYuH7jNDASFdbfLgZlUYNjOCFcF0eNQI-3Vqzh_F/pub?embedded=true')" />
</div>

@code {
    private IFrame responsiveIFrame;
    private string currentSource = "about:blank";
    private bool isLoading = false;
    
    private void ChangeSource(string newSource)
    {
        currentSource = newSource;
        isLoading = true;
        StateHasChanged();
    }
    
    private void HandleIFrameLoaded()
    {
        isLoading = false;
        StateHasChanged();
    }
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            // Optional: Add a resize observer to adjust iframe height based on content
            await JSRuntime.InvokeVoidAsync("addIFrameResizeListener", responsiveIFrame.Element);
        }
    }
}
```

### Example 5: IFrame Communication with PostMessage
```razor
@inject IJSRuntime JSRuntime

<IFrame @ref="communicationIFrame"
        Src="https://trusted-domain.com/embedded-page"
        Width="100%"
        Height="500px"
        OnLoad="SetupIFrameCommunication"
        OnMessage="HandleIFrameMessage" />

<div class="mt-3">
    <div class="mb-2">
        <input @bind="messageText" class="form-control" placeholder="Enter message to send to iframe" />
    </div>
    <Button Text="Send Message to IFrame" OnClick="SendMessageToIFrame" />
</div>

<div class="mt-3">
    <h5>Messages from IFrame:</h5>
    <ul class="list-group">
        @foreach (var message in messagesFromIFrame)
        {
            <li class="list-group-item">@message</li>
        }
    </ul>
</div>

@code {
    private IFrame communicationIFrame;
    private string messageText = "";
    private List<string> messagesFromIFrame = new List<string>();
    
    private async Task SetupIFrameCommunication()
    {
        // Register a message event listener
        await JSRuntime.InvokeVoidAsync("setupIFrameMessageListener");
    }
    
    private async Task SendMessageToIFrame()
    {
        if (!string.IsNullOrEmpty(messageText))
        {
            await JSRuntime.InvokeVoidAsync("sendMessageToIFrame", 
                communicationIFrame.Element, 
                new { type = "message", content = messageText });
            messageText = "";
        }
    }
    
    private void HandleIFrameMessage(MessageEventArgs args)
    {
        if (args.Data != null)
        {
            messagesFromIFrame.Add(args.Data.ToString());
            StateHasChanged();
        }
    }
    
    public class MessageEventArgs
    {
        public object Data { get; set; }
        public string Origin { get; set; }
        public string Source { get; set; }
    }
}

@* Add this JavaScript to your _Host.cshtml or index.html *@
@* 
<script>
    window.setupIFrameMessageListener = function() {
        window.addEventListener('message', function(event) {
            // Verify the origin for security
            if (event.origin === 'https://trusted-domain.com') {
                DotNet.invokeMethodAsync('YourAppNamespace', 'ReceiveIFrameMessage', {
                    data: event.data,
                    origin: event.origin,
                    source: 'iframe'
                });
            }
        });
    };
    
    window.sendMessageToIFrame = function(iframeElement, message) {
        if (iframeElement && iframeElement.contentWindow) {
            iframeElement.contentWindow.postMessage(message, 'https://trusted-domain.com');
        }
    };
</script>
*@
```

### Example 6: IFrame for Document Viewer
```razor
<div class="card">
    <div class="card-header">
        <h5>Document Viewer</h5>
    </div>
    <div class="card-body p-0">
        <IFrame Src="@documentUrl"
                Width="100%"
                Height="600px"
                ShowBorder="false"
                ShowLoading="true"
                LoadingText="Loading document..."
                OnLoad="() => documentLoaded = true"
                OnError="HandleDocumentError" />
    </div>
    <div class="card-footer d-flex justify-content-between">
        <div>
            <Button Text="Previous" Disabled="@(!documentLoaded || currentDocIndex == 0)" OnClick="LoadPreviousDocument" />
            <Button Text="Next" Disabled="@(!documentLoaded || currentDocIndex >= documents.Count - 1)" OnClick="LoadNextDocument" />
        </div>
        <Button Text="Download" Icon="fa-solid fa-download" OnClick="DownloadDocument" />
    </div>
</div>

@code {
    private List<string> documents = new List<string>
    {
        "https://example.com/docs/document1.pdf",
        "https://example.com/docs/document2.pdf",
        "https://example.com/docs/document3.pdf"
    };
    
    private int currentDocIndex = 0;
    private string documentUrl => documents[currentDocIndex];
    private bool documentLoaded = false;
    
    private void LoadPreviousDocument()
    {
        if (currentDocIndex > 0)
        {
            currentDocIndex--;
            documentLoaded = false;
        }
    }
    
    private void LoadNextDocument()
    {
        if (currentDocIndex < documents.Count - 1)
        {
            currentDocIndex++;
            documentLoaded = false;
        }
    }
    
    private void HandleDocumentError()
    {
        // Handle error, maybe show a message or try to load a cached version
    }
    
    private void DownloadDocument()
    {
        // Implement download functionality
    }
}
```

### Example 7: IFrame for Embedded Analytics Dashboard
```razor
@inject IJSRuntime JSRuntime
@inject AuthenticationStateProvider AuthStateProvider

<div class="analytics-container">
    <div class="row mb-3">
        <div class="col">
            <h3>Analytics Dashboard</h3>
        </div>
        <div class="col-auto">
            <div class="btn-group">
                <Button Text="Daily" OnClick="() => ChangeTimeframe('daily')" Color="@GetTimeframeButtonColor('daily')" />
                <Button Text="Weekly" OnClick="() => ChangeTimeframe('weekly')" Color="@GetTimeframeButtonColor('weekly')" />
                <Button Text="Monthly" OnClick="() => ChangeTimeframe('monthly')" Color="@GetTimeframeButtonColor('monthly')" />
            </div>
            <Button Text="Refresh" Icon="fa-solid fa-sync" OnClick="RefreshDashboard" Class="ms-2" />
        </div>
    </div>
    
    <IFrame @ref="dashboardIFrame"
            Src="@dashboardUrl"
            Width="100%"
            Height="700px"
            OnLoad="InitializeDashboard"
            OnMessage="HandleDashboardMessage" />
</div>

@code {
    private IFrame dashboardIFrame;
    private string currentTimeframe = "weekly";
    private string dashboardUrl => $"https://analytics-platform.com/embed?timeframe={currentTimeframe}&token={securityToken}";
    private string securityToken;
    
    protected override async Task OnInitializedAsync()
    {
        // Get authentication token for the embedded dashboard
        var authState = await AuthStateProvider.GetAuthenticationStateAsync();
        if (authState.User.Identity.IsAuthenticated)
        {
            // Generate or retrieve a token for the iframe authentication
            securityToken = await GenerateSecurityTokenAsync();
        }
    }
    
    private async Task<string> GenerateSecurityTokenAsync()
    {
        // Implementation to generate a secure token for iframe authentication
        // This is just a placeholder
        return "sample-security-token";
    }
    
    private void ChangeTimeframe(string timeframe)
    {
        currentTimeframe = timeframe;
    }
    
    private Color GetTimeframeButtonColor(string timeframe)
    {
        return currentTimeframe == timeframe ? Color.Primary : Color.Secondary;
    }
    
    private async Task RefreshDashboard()
    {
        // Refresh the iframe content
        await JSRuntime.InvokeVoidAsync("refreshIFrame", dashboardIFrame.Element);
    }
    
    private async Task InitializeDashboard()
    {
        // Initialize dashboard with additional configuration
        await JSRuntime.InvokeVoidAsync("initializeDashboard", dashboardIFrame.Element);
    }
    
    private void HandleDashboardMessage(MessageEventArgs args)
    {
        // Handle messages from the dashboard iframe
        Console.WriteLine($"Received message from dashboard: {args.Data}");
    }
    
    public class MessageEventArgs
    {
        public object Data { get; set; }
        public string Origin { get; set; }
    }
}

@* Add this JavaScript to your _Host.cshtml or index.html *@
@* 
<script>
    window.refreshIFrame = function(iframeElement) {
        if (iframeElement && iframeElement.contentWindow) {
            iframeElement.src = iframeElement.src;
        }
    };
    
    window.initializeDashboard = function(iframeElement) {
        if (iframeElement && iframeElement.contentWindow) {
            // Send initialization data to the iframe
            iframeElement.contentWindow.postMessage({
                type: 'initialize',
                theme: 'light',
                language: 'en'
            }, '*');
        }
    };
</script>
*@
```

## CSS Customization

The IFrame component can be customized using CSS variables and classes:

```css
/* Custom IFrame container styling */
.bb-iframe-container {
    --bb-iframe-border-color: #dee2e6;
    --bb-iframe-border-radius: 0.375rem;
    --bb-iframe-box-shadow: 0 0.125rem 0.25rem rgba(0, 0, 0, 0.075);
    --bb-iframe-bg: #ffffff;
    --bb-iframe-loading-bg: rgba(255, 255, 255, 0.8);
    --bb-iframe-loading-color: #007bff;
    
    border: 1px solid var(--bb-iframe-border-color);
    border-radius: var(--bb-iframe-border-radius);
    box-shadow: var(--bb-iframe-box-shadow);
    background-color: var(--bb-iframe-bg);
    overflow: hidden;
    position: relative;
}

/* Loading indicator styling */
.bb-iframe-loading {
    position: absolute;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    display: flex;
    align-items: center;
    justify-content: center;
    background-color: var(--bb-iframe-loading-bg);
    color: var(--bb-iframe-loading-color);
    z-index: 1;
}

/* Responsive iframe container (16:9 aspect ratio) */
.bb-iframe-responsive {
    position: relative;
    width: 100%;
    padding-bottom: 56.25%; /* 16:9 aspect ratio */
    height: 0;
    overflow: hidden;
}

.bb-iframe-responsive iframe {
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    border: 0;
}
```

You can apply these styles to your IFrame component:

```razor
<div class="bb-iframe-container">
    <IFrame Src="https://www.example.com" Width="100%" Height="400px" />
</div>

<!-- For responsive iframe with 16:9 aspect ratio -->
<div class="bb-iframe-responsive">
    <IFrame Src="https://www.youtube.com/embed/dQw4w9WgXcQ" Width="100%" Height="100%" />
</div>
```

## JavaScript Interop

The IFrame component uses JavaScript interop for advanced functionality like communication with the iframe content, resizing based on content, and handling events. Here are some common JavaScript functions that can be used with the IFrame component:

```javascript
// Resize iframe based on content height
window.resizeIFrameToContent = function(iframeElement) {
    if (iframeElement && iframeElement.contentWindow) {
        try {
            const height = iframeElement.contentWindow.document.body.scrollHeight;
            iframeElement.style.height = height + 'px';
        } catch (e) {
            // Cross-origin restrictions may prevent accessing contentWindow.document
            console.error('Could not resize iframe: ', e);
        }
    }
};

// Send message to iframe content
window.sendMessageToIFrame = function(iframeElement, message, targetOrigin = '*') {
    if (iframeElement && iframeElement.contentWindow) {
        iframeElement.contentWindow.postMessage(message, targetOrigin);
    }
};

// Set up listener for messages from iframe
window.setupIFrameMessageListener = function(dotNetReference) {
    window.addEventListener('message', function(event) {
        // Optionally verify the origin for security
        dotNetReference.invokeMethodAsync('HandleIFrameMessage', {
            data: event.data,
            origin: event.origin
        });
    });
};
```

## Accessibility

The IFrame component supports accessibility through proper attributes and ARIA roles:

```razor
<IFrame Src="https://www.example.com"
        Title="Example Website Content"
        AriaLabel="Embedded content from Example.com"
        TabIndex="0"
        Loading="lazy" />
```

For better accessibility:

1. Always provide a descriptive `Title` attribute
2. Use `AriaLabel` for screen readers
3. Ensure the iframe content is keyboard navigable
4. Consider adding a text alternative or description for users who cannot view the iframe content

## Browser Compatibility

The IFrame component is compatible with all modern browsers. However, some advanced features may have limitations:

- The `sandbox` attribute is supported in all modern browsers but has varying levels of support for specific values
- The `allow` attribute (Feature Policy) has different implementation levels across browsers
- The `loading="lazy"` attribute is not supported in all browsers, but will gracefully fall back

For older browsers, basic iframe functionality will work, but some security features and performance optimizations may not be available.

## Integration with Other Components

The IFrame component can be combined with other BootstrapBlazor components:

```razor
<Card>
    <CardTitle>Embedded Map</CardTitle>
    <CardBody>
        <IFrame Src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d387193.3059353029!2d-74.25986548248684!3d40.69714941680757!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x89c24fa5d33f083b%3A0xc80b8f06e177fe62!2sNew%20York%2C%20NY%2C%20USA!5e0!3m2!1sen!2s!4v1627309750687!5m2!1sen!2s"
                Width="100%"
                Height="300px"
                AllowFullScreen="true" />
    </CardBody>
    <CardFooter>
        <Button Text="View Larger Map" Icon="fa-solid fa-external-link-alt" />
    </CardFooter>
</Card>
```

You can also use the IFrame component with tabs, accordions, or modals to create more complex UI structures:

```razor
<Tabs>
    <Tab Title="Map">
        <IFrame Src="https://www.google.com/maps/embed?..." Width="100%" Height="400px" />
    </Tab>
    <Tab Title="Video">
        <IFrame Src="https://www.youtube.com/embed/..." Width="100%" Height="400px" />
    </Tab>
    <Tab Title="Document">
        <IFrame Src="https://docs.google.com/document/d/e/..." Width="100%" Height="400px" />
    </Tab>
</Tabs>
```