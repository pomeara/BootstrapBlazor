# ConnectionHub Component

## Overview
The ConnectionHub component in BootstrapBlazor provides real-time communication capabilities between the server and client using SignalR. It establishes and manages persistent connections, enabling applications to implement features like real-time notifications, live updates, chat functionality, and collaborative editing without requiring page refreshes.

## Key Features
- Real-time bidirectional communication between server and client
- Automatic connection management and reconnection handling
- Support for different transport protocols (WebSockets, Server-Sent Events, Long Polling)
- Group-based messaging for targeted communication
- Connection state monitoring and event handling
- Scalable architecture for high-traffic applications
- Secure communication with authentication and authorization support
- Customizable reconnection strategies

## Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `HubUrl` | `string` | `"/signalr"` | The URL of the SignalR hub endpoint. |
| `AutoReconnect` | `bool` | `true` | Whether to automatically attempt reconnection when the connection is lost. |
| `ReconnectInterval` | `int` | `5000` | The interval in milliseconds between reconnection attempts. |
| `MaxReconnectAttempts` | `int` | `10` | The maximum number of reconnection attempts before giving up. |
| `TransportType` | `HttpTransportType` | `HttpTransportType.WebSockets` | The transport protocol to use for the connection. |
| `ConnectionTimeout` | `TimeSpan` | `TimeSpan.FromSeconds(30)` | The timeout period for connection attempts. |
| `KeepAliveInterval` | `TimeSpan` | `TimeSpan.FromSeconds(15)` | The interval at which keep-alive messages are sent. |
| `HandshakeTimeout` | `TimeSpan` | `TimeSpan.FromSeconds(15)` | The timeout for the initial handshake. |
| `EnableDetailedErrors` | `bool` | `false` | Whether to enable detailed error messages. |
| `LogLevel` | `LogLevel` | `LogLevel.Information` | The log level for connection-related events. |
| `IsConnected` | `bool` | `false` | Read-only property indicating whether the connection is currently established. |
| `ConnectionId` | `string` | `null` | Read-only property containing the unique identifier for the current connection. |

## Events

| Event | Description |
|-------|-------------|
| `OnConnected` | Triggered when a connection is successfully established. |
| `OnDisconnected` | Triggered when the connection is closed or lost. |
| `OnReconnecting` | Triggered when the component is attempting to reconnect. |
| `OnReconnected` | Triggered when the connection is successfully reestablished. |
| `OnError` | Triggered when an error occurs during connection or message processing. |
| `OnMessageReceived` | Triggered when a message is received from the server. |

## Usage Examples

### Example 1: Basic ConnectionHub Setup

```razor
@page "/connection-demo"
@using Microsoft.AspNetCore.SignalR.Client

<div class="mb-3">
    <h3>Connection Status: @connectionStatus</h3>
    <Button Color="Color.Primary" OnClick="SendMessage">Send Test Message</Button>
</div>

<ConnectionHub @ref="hubConnection"
               HubUrl="/chathub"
               OnConnected="HandleConnected"
               OnDisconnected="HandleDisconnected"
               OnMessageReceived="HandleMessage" />

@code {
    private ConnectionHub hubConnection;
    private string connectionStatus = "Disconnected";

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await hubConnection.StartAsync();
        }
    }

    private void HandleConnected()
    {
        connectionStatus = "Connected";
        StateHasChanged();
    }

    private void HandleDisconnected(Exception ex)
    {
        connectionStatus = $"Disconnected: {ex?.Message ?? "Unknown reason"}";
        StateHasChanged();
    }

    private void HandleMessage(string user, string message)
    {
        // Process incoming message
        Console.WriteLine($"{user}: {message}");
    }

    private async Task SendMessage()
    {
        if (hubConnection.IsConnected)
        {
            await hubConnection.InvokeAsync("SendMessage", "User", "Hello from Blazor!");
        }
    }
}
```

### Example 2: Group-Based Communication

```razor
<div class="mb-3">
    <div class="form-group">
        <label for="groupName">Group Name:</label>
        <input id="groupName" class="form-control" @bind="groupName" />
    </div>
    <div class="form-group">
        <label for="message">Message:</label>
        <input id="message" class="form-control" @bind="message" />
    </div>
    <div class="btn-group">
        <Button Color="Color.Success" OnClick="JoinGroup">Join Group</Button>
        <Button Color="Color.Danger" OnClick="LeaveGroup">Leave Group</Button>
        <Button Color="Color.Primary" OnClick="SendGroupMessage">Send to Group</Button>
    </div>
</div>

<div class="mt-3">
    <h4>Messages:</h4>
    <ul class="list-group">
        @foreach (var msg in messages)
        {
            <li class="list-group-item">@msg</li>
        }
    </ul>
</div>

<ConnectionHub @ref="hubConnection"
               HubUrl="/grouphub"
               OnConnected="HandleConnected"
               OnMessageReceived="HandleGroupMessage" />

@code {
    private ConnectionHub hubConnection;
    private string groupName = "DefaultGroup";
    private string message = "";
    private List<string> messages = new List<string>();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await hubConnection.StartAsync();
        }
    }

    private void HandleConnected()
    {
        messages.Add("Connected to hub");
        StateHasChanged();
    }

    private void HandleGroupMessage(string group, string user, string message)
    {
        messages.Add($"[{group}] {user}: {message}");
        StateHasChanged();
    }

    private async Task JoinGroup()
    {
        if (hubConnection.IsConnected)
        {
            await hubConnection.InvokeAsync("JoinGroup", groupName);
            messages.Add($"Joined group: {groupName}");
            StateHasChanged();
        }
    }

    private async Task LeaveGroup()
    {
        if (hubConnection.IsConnected)
        {
            await hubConnection.InvokeAsync("LeaveGroup", groupName);
            messages.Add($"Left group: {groupName}");
            StateHasChanged();
        }
    }

    private async Task SendGroupMessage()
    {
        if (hubConnection.IsConnected && !string.IsNullOrEmpty(message))
        {
            await hubConnection.InvokeAsync("SendGroupMessage", groupName, "User", message);
            message = "";
        }
    }
}
```

### Example 3: Reconnection Handling

```razor
<div class="mb-3">
    <h3>Connection Status: @connectionStatus</h3>
    <div class="progress">
        <div class="progress-bar @progressBarClass" role="progressbar" style="width: @progressWidth%" aria-valuenow="@progressWidth" aria-valuemin="0" aria-valuemax="100"></div>
    </div>
</div>

<ConnectionHub @ref="hubConnection"
               HubUrl="/notificationhub"
               AutoReconnect="true"
               ReconnectInterval="3000"
               MaxReconnectAttempts="5"
               OnConnected="HandleConnected"
               OnDisconnected="HandleDisconnected"
               OnReconnecting="HandleReconnecting"
               OnReconnected="HandleReconnected"
               OnError="HandleError" />

@code {
    private ConnectionHub hubConnection;
    private string connectionStatus = "Initializing";
    private string progressBarClass = "bg-warning";
    private int progressWidth = 0;
    private int reconnectAttempt = 0;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await hubConnection.StartAsync();
        }
    }

    private void HandleConnected()
    {
        connectionStatus = "Connected";
        progressBarClass = "bg-success";
        progressWidth = 100;
        reconnectAttempt = 0;
        StateHasChanged();
    }

    private void HandleDisconnected(Exception ex)
    {
        connectionStatus = $"Disconnected: {ex?.Message ?? "Unknown reason"}";
        progressBarClass = "bg-danger";
        progressWidth = 0;
        StateHasChanged();
    }

    private void HandleReconnecting(Exception ex)
    {
        reconnectAttempt++;
        connectionStatus = $"Reconnecting (Attempt {reconnectAttempt}/5)...";
        progressBarClass = "bg-warning";
        progressWidth = reconnectAttempt * 20;
        StateHasChanged();
    }

    private void HandleReconnected(string connectionId)
    {
        connectionStatus = $"Reconnected (ID: {connectionId})";
        progressBarClass = "bg-success";
        progressWidth = 100;
        StateHasChanged();
    }

    private void HandleError(Exception ex)
    {
        connectionStatus = $"Error: {ex.Message}";
        progressBarClass = "bg-danger";
        StateHasChanged();
    }
}
```

### Example 4: Real-time Notifications

```razor
@page "/notifications"
@using System.Collections.ObjectModel

<div class="mb-3">
    <h3>Real-time Notifications</h3>
    <Button Color="Color.Primary" OnClick="SubscribeToNotifications">Subscribe</Button>
    <Button Color="Color.Secondary" OnClick="UnsubscribeFromNotifications">Unsubscribe</Button>
</div>

<div class="notification-container">
    @foreach (var notification in notifications)
    {
        <div class="notification-item @notification.Type.ToString().ToLower()">
            <div class="notification-header">
                <span class="notification-title">@notification.Title</span>
                <span class="notification-time">@notification.Timestamp.ToString("HH:mm:ss")</span>
            </div>
            <div class="notification-body">@notification.Message</div>
        </div>
    }
</div>

<ConnectionHub @ref="hubConnection"
               HubUrl="/notificationhub"
               OnConnected="HandleConnected"
               OnMessageReceived="HandleNotification" />

@code {
    private ConnectionHub hubConnection;
    private ObservableCollection<NotificationModel> notifications = new ObservableCollection<NotificationModel>();
    private bool isSubscribed = false;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await hubConnection.StartAsync();
        }
    }

    private void HandleConnected()
    {
        Console.WriteLine("Connected to notification hub");
    }

    private void HandleNotification(NotificationModel notification)
    {
        notifications.Insert(0, notification);
        
        // Keep only the latest 10 notifications
        while (notifications.Count > 10)
        {
            notifications.RemoveAt(notifications.Count - 1);
        }
        
        StateHasChanged();
    }

    private async Task SubscribeToNotifications()
    {
        if (hubConnection.IsConnected && !isSubscribed)
        {
            await hubConnection.InvokeAsync("SubscribeToNotifications");
            isSubscribed = true;
            
            // Add a local notification
            notifications.Insert(0, new NotificationModel
            {
                Title = "System",
                Message = "Subscribed to notifications",
                Type = NotificationType.Info,
                Timestamp = DateTime.Now
            });
            StateHasChanged();
        }
    }

    private async Task UnsubscribeFromNotifications()
    {
        if (hubConnection.IsConnected && isSubscribed)
        {
            await hubConnection.InvokeAsync("UnsubscribeFromNotifications");
            isSubscribed = false;
            
            // Add a local notification
            notifications.Insert(0, new NotificationModel
            {
                Title = "System",
                Message = "Unsubscribed from notifications",
                Type = NotificationType.Warning,
                Timestamp = DateTime.Now
            });
            StateHasChanged();
        }
    }

    public class NotificationModel
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public NotificationType Type { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }

    public enum NotificationType
    {
        Info,
        Success,
        Warning,
        Error
    }
}
```

### Example 5: Server-Side Implementation

```csharp
// ChatHub.cs
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace YourNamespace.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task LeaveGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task SendGroupMessage(string groupName, string user, string message)
        {
            await Clients.Group(groupName).SendAsync("ReceiveGroupMessage", groupName, user, message);
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("Connected", Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
        }
    }
}
```

### Example 6: Integration with Authentication

```razor
@page "/secure-hub"
@using Microsoft.AspNetCore.SignalR.Client
@using Microsoft.AspNetCore.Authorization
@attribute [Authorize]

<div class="mb-3">
    <h3>Secure Connection Hub</h3>
    <p>Welcome, @context.User.Identity.Name!</p>
    <Button Color="Color.Primary" OnClick="SendSecureMessage">Send Secure Message</Button>
</div>

<div class="mt-3">
    <h4>Secure Messages:</h4>
    <ul class="list-group">
        @foreach (var msg in secureMessages)
        {
            <li class="list-group-item">@msg</li>
        }
    </ul>
</div>

<ConnectionHub @ref="hubConnection"
               HubUrl="/securehub"
               OnConnected="HandleConnected"
               OnMessageReceived="HandleSecureMessage" />

@code {
    [CascadingParameter]
    private Task<AuthenticationState> authStateTask { get; set; }
    
    private ConnectionHub hubConnection;
    private List<string> secureMessages = new List<string>();
    private string currentUser;

    protected override async Task OnInitializedAsync()
    {
        var authState = await authStateTask;
        currentUser = authState.User.Identity.Name;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            // The ConnectionHub component will automatically include the auth token
            await hubConnection.StartAsync();
        }
    }

    private void HandleConnected()
    {
        secureMessages.Add("Secure connection established");
        StateHasChanged();
    }

    private void HandleSecureMessage(string user, string message)
    {
        secureMessages.Add($"{user}: {message}");
        StateHasChanged();
    }

    private async Task SendSecureMessage()
    {
        if (hubConnection.IsConnected)
        {
            await hubConnection.InvokeAsync("SendSecureMessage", currentUser, "This is a secure message");
        }
    }
}
```

### Example 7: Streaming Data

```razor
@page "/data-stream"
@using System.Threading

<div class="mb-3">
    <h3>Real-time Data Stream</h3>
    <div class="btn-group">
        <Button Color="Color.Success" OnClick="StartStream" Disabled="@isStreaming">Start Stream</Button>
        <Button Color="Color.Danger" OnClick="StopStream" Disabled="@(!isStreaming)">Stop Stream</Button>
    </div>
</div>

<div class="mt-3">
    <h4>Live Data:</h4>
    <div class="chart-container" style="height: 300px;">
        <canvas id="dataChart"></canvas>
    </div>
</div>

<ConnectionHub @ref="hubConnection"
               HubUrl="/streamhub"
               OnConnected="HandleConnected"
               OnMessageReceived="HandleDataPoint" />

@code {
    private ConnectionHub hubConnection;
    private bool isStreaming = false;
    private CancellationTokenSource cts;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await hubConnection.StartAsync();
            await JSRuntime.InvokeVoidAsync("initChart");
        }
    }

    private void HandleConnected()
    {
        Console.WriteLine("Connected to stream hub");
    }

    private async void HandleDataPoint(DataPoint point)
    {
        await JSRuntime.InvokeVoidAsync("updateChart", point.Timestamp, point.Value);
    }

    private async Task StartStream()
    {
        if (hubConnection.IsConnected && !isStreaming)
        {
            cts = new CancellationTokenSource();
            await hubConnection.InvokeAsync("StartStream", cts.Token);
            isStreaming = true;
        }
    }

    private async Task StopStream()
    {
        if (hubConnection.IsConnected && isStreaming)
        {
            cts?.Cancel();
            await hubConnection.InvokeAsync("StopStream");
            isStreaming = false;
        }
    }

    public class DataPoint
    {
        public DateTime Timestamp { get; set; }
        public double Value { get; set; }
    }

    // JavaScript interop for chart rendering would be needed
    // @inject IJSRuntime JSRuntime
}
```

## CSS Customization

The ConnectionHub component is primarily a non-visual component, but you can style the connection status indicators and related UI elements:

```css
/* Connection status indicators */
.connection-status {
    padding: 8px 12px;
    border-radius: 4px;
    margin-bottom: 1rem;
    font-weight: 500;
}

.connection-status.connected {
    background-color: var(--bs-success-bg-subtle);
    color: var(--bs-success);
    border: 1px solid var(--bs-success-border-subtle);
}

.connection-status.disconnected {
    background-color: var(--bs-danger-bg-subtle);
    color: var(--bs-danger);
    border: 1px solid var(--bs-danger-border-subtle);
}

.connection-status.reconnecting {
    background-color: var(--bs-warning-bg-subtle);
    color: var(--bs-warning);
    border: 1px solid var(--bs-warning-border-subtle);
}

/* Notification styling from Example 4 */
.notification-container {
    max-height: 400px;
    overflow-y: auto;
}

.notification-item {
    padding: 10px;
    margin-bottom: 8px;
    border-radius: 4px;
    border-left: 4px solid #ccc;
}

.notification-item.info {
    background-color: #e8f4fd;
    border-left-color: #0d6efd;
}

.notification-item.success {
    background-color: #d1e7dd;
    border-left-color: #198754;
}

.notification-item.warning {
    background-color: #fff3cd;
    border-left-color: #ffc107;
}

.notification-item.error {
    background-color: #f8d7da;
    border-left-color: #dc3545;
}

.notification-header {
    display: flex;
    justify-content: space-between;
    margin-bottom: 5px;
}

.notification-title {
    font-weight: bold;
}

.notification-time {
    font-size: 0.8rem;
    color: #6c757d;
}
```

## Server Configuration

To use the ConnectionHub component, you need to configure SignalR on the server side in your `Startup.cs` or `Program.cs` file:

```csharp
// Program.cs (for .NET 6+)
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
 builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSignalR(); // Add SignalR services

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapHub<ChatHub>("/chathub"); // Map your hub endpoints
app.MapHub<NotificationHub>("/notificationhub");
app.MapFallbackToPage("/_Host");

app.Run();
```

## Performance Considerations

- **Connection Pooling**: For applications with many components that need real-time updates, consider using a single shared ConnectionHub instance rather than creating multiple connections.
- **Message Size**: Keep message payloads small to minimize bandwidth usage, especially for high-frequency updates.
- **Throttling**: Implement throttling for high-frequency events to prevent overwhelming the client or server.
- **Selective Updates**: Use groups and targeted messages rather than broadcasting to all clients when possible.

## Security Considerations

- Always validate and sanitize data received from clients before processing or broadcasting it.
- Implement proper authentication and authorization for your SignalR hubs.
- Consider using HTTPS for all SignalR connections to prevent data interception.
- Be cautious about exposing sensitive information through real-time updates.

## Browser Compatibility

The ConnectionHub component relies on SignalR, which supports various transport methods to ensure compatibility across browsers:

- WebSockets (modern browsers)
- Server-Sent Events (fallback)
- Long Polling (fallback for older browsers)

SignalR automatically negotiates the best available transport method based on browser capabilities.

## Integration with Other Components

The ConnectionHub component works well with:

- Notification components for real-time alerts
- Chat and messaging interfaces
- Dashboards with live data updates
- Collaborative editing tools
- Real-time analytics displays
- Multi-user games and interactive applications