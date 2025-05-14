# Reconnector Component

## Overview

The Reconnector component in BootstrapBlazor provides an automatic reconnection mechanism for applications that rely on persistent connections, such as SignalR hubs or WebSocket connections. When a connection is lost due to network issues, server restarts, or other disruptions, the Reconnector attempts to reestablish the connection automatically according to configurable retry policies. This component enhances application resilience and improves user experience by minimizing connection-related disruptions.

## Features

- **Automatic Reconnection**: Detects connection loss and attempts to reconnect automatically
- **Configurable Retry Policy**: Customizable retry attempts, intervals, and backoff strategies
- **Connection State Monitoring**: Real-time tracking and reporting of connection status
- **Visual Feedback**: Optional UI indicators showing connection state to users
- **Event Callbacks**: Comprehensive event system for connection lifecycle management
- **Multiple Connection Support**: Ability to monitor and reconnect multiple connection types
- **Customizable Timeout Handling**: Configure how long to attempt reconnection before giving up

## Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `ConnectionType` | `ReconnectorType` | `ReconnectorType.SignalR` | Specifies the type of connection to monitor and reconnect (SignalR, WebSocket, etc.). |
| `HubUrl` | `string` | `null` | The URL of the SignalR hub or WebSocket endpoint to connect to. |
| `AutoReconnect` | `bool` | `true` | Whether to automatically attempt reconnection when a connection is lost. |
| `MaxRetryAttempts` | `int` | `5` | Maximum number of reconnection attempts before giving up. Set to -1 for unlimited retries. |
| `InitialRetryInterval` | `int` | `1000` | Initial delay in milliseconds between reconnection attempts. |
| `MaxRetryInterval` | `int` | `30000` | Maximum delay in milliseconds between reconnection attempts when using backoff. |
| `BackoffMultiplier` | `double` | `1.5` | Multiplier applied to the retry interval after each failed attempt when using exponential backoff. |
| `ReconnectStrategy` | `ReconnectStrategy` | `ReconnectStrategy.ExponentialBackoff` | Strategy for timing reconnection attempts (Fixed, Linear, ExponentialBackoff). |
| `ShowIndicator` | `bool` | `true` | Whether to show a visual indicator of the connection state. |
| `IndicatorPosition` | `Placement` | `Placement.TopRight` | Position of the connection state indicator on the screen. |
| `ConnectionOptions` | `object` | `null` | Additional options to pass to the connection when establishing or reestablishing it. |
| `ReconnectingTemplate` | `RenderFragment` | `null` | Custom template for the reconnecting state indicator. |
| `DisconnectedTemplate` | `RenderFragment` | `null` | Custom template for the disconnected state indicator. |
| `ConnectedTemplate` | `RenderFragment` | `null` | Custom template for the connected state indicator. |

## Events

| Event | Description |
|-------|-------------|
| `OnConnecting` | Triggered when a connection attempt is starting. |
| `OnConnected` | Triggered when a connection is successfully established. |
| `OnDisconnected` | Triggered when a connection is lost or closed. Provides the reason for disconnection. |
| `OnReconnecting` | Triggered when a reconnection attempt is starting. Provides the current retry count. |
| `OnReconnected` | Triggered when a reconnection attempt succeeds. |
| `OnReconnectionFailed` | Triggered when all reconnection attempts have failed. |
| `OnConnectionStateChanged` | Triggered whenever the connection state changes. Provides the new state. |
| `OnRetryIntervalCalculated` | Triggered when a new retry interval is calculated. Allows customizing the interval. |

## Usage Examples

### Example 1: Basic SignalR Reconnector

A simple reconnector for a SignalR hub connection:

```razor
<Reconnector ConnectionType="ReconnectorType.SignalR"
            HubUrl="/chathub"
            AutoReconnect="true"
            MaxRetryAttempts="10"
            OnConnected="HandleConnected"
            OnDisconnected="HandleDisconnected" />

@code {
    private async Task HandleConnected()
    {
        await MessageService.Show(new MessageOption()
        {
            Content = "Connected to chat server",
            Icon = "fa-solid fa-check-circle",
            Color = Color.Success
        });
    }

    private async Task HandleDisconnected(string reason)
    {
        await MessageService.Show(new MessageOption()
        {
            Content = $"Disconnected from chat server: {reason}",
            Icon = "fa-solid fa-exclamation-circle",
            Color = Color.Warning
        });
    }
}
```

### Example 2: Custom Reconnection Strategy

Implementing a custom reconnection strategy with exponential backoff:

```razor
<Reconnector ConnectionType="ReconnectorType.WebSocket"
            HubUrl="wss://api.example.com/live"
            ReconnectStrategy="ReconnectStrategy.ExponentialBackoff"
            InitialRetryInterval="2000"
            MaxRetryInterval="60000"
            BackoffMultiplier="2.0"
            MaxRetryAttempts="-1"
            OnRetryIntervalCalculated="CustomizeRetryInterval"
            OnReconnecting="HandleReconnecting" />

@code {
    private int CustomizeRetryInterval(int calculatedInterval, int attemptCount)
    {
        // Add jitter to prevent reconnection storms
        var jitter = new Random().Next(-500, 500);
        return calculatedInterval + jitter;
    }

    private void HandleReconnecting(int attemptCount)
    {
        Console.WriteLine($"Reconnection attempt #{attemptCount}");
    }
}
```

### Example 3: Custom Connection State Indicators

Providing custom templates for different connection states:

```razor
<Reconnector ConnectionType="ReconnectorType.SignalR"
            HubUrl="/datahub"
            ShowIndicator="true"
            IndicatorPosition="Placement.BottomLeft">
    <ConnectedTemplate>
        <div class="connection-indicator connected">
            <i class="fa-solid fa-wifi"></i>
            <span>Connected</span>
        </div>
    </ConnectedTemplate>
    <ReconnectingTemplate>
        <div class="connection-indicator reconnecting">
            <i class="fa-solid fa-sync fa-spin"></i>
            <span>Reconnecting (@context.AttemptCount of @context.MaxAttempts)</span>
        </div>
    </ReconnectingTemplate>
    <DisconnectedTemplate>
        <div class="connection-indicator disconnected">
            <i class="fa-solid fa-wifi-slash"></i>
            <span>Disconnected: @context.Reason</span>
            <Button Size="Size.Small" OnClick="@context.TryReconnect">Retry</Button>
        </div>
    </DisconnectedTemplate>
</Reconnector>

<style>
    .connection-indicator {
        padding: 8px 12px;
        border-radius: 4px;
        display: flex;
        align-items: center;
        gap: 8px;
        font-size: 14px;
    }
    
    .connection-indicator.connected {
        background-color: rgba(40, 167, 69, 0.2);
        color: #28a745;
    }
    
    .connection-indicator.reconnecting {
        background-color: rgba(255, 193, 7, 0.2);
        color: #ffc107;
    }
    
    .connection-indicator.disconnected {
        background-color: rgba(220, 53, 69, 0.2);
        color: #dc3545;
    }
</style>
```

### Example 4: Multiple Connection Monitoring

Monitoring multiple connections with a shared status display:

```razor
@page "/dashboard"

<div class="connection-status-panel">
    <h5>System Connections</h5>
    <div class="connection-list">
        @foreach (var conn in _connections)
        {
            <div class="connection-item @GetConnectionClass(conn.Key)">
                <span>@conn.Key:</span>
                <span>@conn.Value</span>
            </div>
        }
    </div>
</div>

<Reconnector @ref="_dataReconnector"
            ConnectionType="ReconnectorType.SignalR"
            HubUrl="/datahub"
            ShowIndicator="false"
            OnConnectionStateChanged="state => UpdateConnectionState(\"Data Service\", state)" />

<Reconnector @ref="_notificationReconnector"
            ConnectionType="ReconnectorType.SignalR"
            HubUrl="/notificationhub"
            ShowIndicator="false"
            OnConnectionStateChanged="state => UpdateConnectionState(\"Notifications\", state)" />

<Reconnector @ref="_chatReconnector"
            ConnectionType="ReconnectorType.WebSocket"
            HubUrl="wss://chat.example.com"
            ShowIndicator="false"
            OnConnectionStateChanged="state => UpdateConnectionState(\"Chat Service\", state)" />

@code {
    private Reconnector _dataReconnector;
    private Reconnector _notificationReconnector;
    private Reconnector _chatReconnector;
    private Dictionary<string, string> _connections = new();

    protected override void OnInitialized()
    {
        _connections.Add("Data Service", "Connecting...");
        _connections.Add("Notifications", "Connecting...");
        _connections.Add("Chat Service", "Connecting...");
    }

    private void UpdateConnectionState(string connectionName, ConnectionState state)
    {
        _connections[connectionName] = state.ToString();
        StateHasChanged();
    }

    private string GetConnectionClass(string connectionName)
    {
        return _connections[connectionName] switch
        {
            "Connected" => "connected",
            "Reconnecting" => "reconnecting",
            "Disconnected" => "disconnected",
            _ => ""
        };
    }
}

<style>
    .connection-status-panel {
        background-color: #f8f9fa;
        border-radius: 4px;
        padding: 16px;
        margin-bottom: 20px;
    }
    
    .connection-list {
        display: flex;
        flex-direction: column;
        gap: 8px;
    }
    
    .connection-item {
        display: flex;
        justify-content: space-between;
        padding: 8px;
        border-radius: 4px;
    }
    
    .connection-item.connected {
        background-color: rgba(40, 167, 69, 0.1);
    }
    
    .connection-item.reconnecting {
        background-color: rgba(255, 193, 7, 0.1);
    }
    
    .connection-item.disconnected {
        background-color: rgba(220, 53, 69, 0.1);
    }
</style>
```

### Example 5: Integration with Loading Service

Using the Reconnector with a loading indicator during reconnection attempts:

```razor
@inject LoadingService LoadingService

<Reconnector ConnectionType="ReconnectorType.SignalR"
            HubUrl="/apihub"
            ShowIndicator="false"
            OnReconnecting="ShowLoading"
            OnReconnected="HideLoading"
            OnReconnectionFailed="HideLoading" />

@code {
    private async Task ShowLoading(int attemptCount)
    {
        await LoadingService.Show(new LoadingOption
        {
            Title = "Reconnecting",
            Content = $"Attempting to restore connection (Attempt {attemptCount})...",
            IsAutoHide = false
        });
    }

    private async Task HideLoading()
    {
        await LoadingService.Hide();
    }
}
```

### Example 6: Reconnector with User-Triggered Manual Reconnection

Allowing users to manually trigger reconnection attempts:

```razor
<div class="card">
    <div class="card-header d-flex justify-content-between align-items-center">
        <span>Real-time Data</span>
        <div>
            <span class="connection-badge @(_isConnected ? "connected" : "disconnected")">
                @(_isConnected ? "Connected" : "Disconnected")
            </span>
            @if (!_isConnected)
            {
                <Button Size="Size.Small" 
                        Color="Color.Primary" 
                        OnClick="TriggerReconnect"
                        IsDisabled="_isReconnecting">
                    @if (_isReconnecting)
                    {
                        <i class="fa-solid fa-spinner fa-spin me-1"></i>
                        <span>Reconnecting...</span>
                    }
                    else
                    {
                        <i class="fa-solid fa-plug me-1"></i>
                        <span>Reconnect</span>
                    }
                </Button>
            }
        </div>
    </div>
    <div class="card-body">
        <!-- Real-time data content -->
    </div>
</div>

<Reconnector @ref="_reconnector"
            ConnectionType="ReconnectorType.SignalR"
            HubUrl="/datahub"
            AutoReconnect="false"
            ShowIndicator="false"
            OnConnected="HandleConnected"
            OnDisconnected="HandleDisconnected"
            OnReconnecting="() => _isReconnecting = true"
            OnReconnected="() => _isReconnecting = false"
            OnReconnectionFailed="() => _isReconnecting = false" />

@code {
    private Reconnector _reconnector;
    private bool _isConnected;
    private bool _isReconnecting;

    private void HandleConnected()
    {
        _isConnected = true;
        _isReconnecting = false;
        StateHasChanged();
    }

    private void HandleDisconnected(string reason)
    {
        _isConnected = false;
        _isReconnecting = false;
        StateHasChanged();
    }

    private async Task TriggerReconnect()
    {
        _isReconnecting = true;
        await _reconnector.TryReconnectAsync();
    }
}

<style>
    .connection-badge {
        padding: 4px 8px;
        border-radius: 4px;
        font-size: 12px;
        margin-right: 8px;
    }
    
    .connection-badge.connected {
        background-color: rgba(40, 167, 69, 0.2);
        color: #28a745;
    }
    
    .connection-badge.disconnected {
        background-color: rgba(220, 53, 69, 0.2);
        color: #dc3545;
    }
</style>
```

### Example 7: Reconnector with Connection Health Monitoring

Implementing a health check system with the Reconnector:

```razor
@page "/system-monitor"
@implements IDisposable

<div class="health-monitor">
    <div class="health-status">
        <h4>System Health: @_systemStatus</h4>
        <div class="health-indicator @GetHealthClass()"></div>
    </div>
    
    <div class="health-metrics">
        <div class="metric">
            <span>Uptime:</span>
            <span>@_uptime</span>
        </div>
        <div class="metric">
            <span>Last Disconnection:</span>
            <span>@(_lastDisconnect?.ToString("g") ?? "None")</span>
        </div>
        <div class="metric">
            <span>Reconnection Attempts:</span>
            <span>@_reconnectionAttempts</span>
        </div>
        <div class="metric">
            <span>Connection Stability:</span>
            <div class="progress">
                <div class="progress-bar @GetStabilityClass()" 
                     style="width: @_connectionStability%">
                    @_connectionStability%
                </div>
            </div>
        </div>
    </div>
</div>

<Reconnector @ref="_reconnector"
            ConnectionType="ReconnectorType.SignalR"
            HubUrl="/systemhub"
            MaxRetryAttempts="-1"
            OnConnected="HandleConnected"
            OnDisconnected="HandleDisconnected"
            OnReconnecting="HandleReconnecting"
            OnReconnected="HandleReconnected" />

@code {
    private Reconnector _reconnector;
    private string _systemStatus = "Initializing...";
    private DateTime? _connectionStart;
    private DateTime? _lastDisconnect;
    private string _uptime = "--";
    private int _reconnectionAttempts;
    private int _connectionStability = 100;
    private Timer _uptimeTimer;

    protected override void OnInitialized()
    {
        _uptimeTimer = new Timer(UpdateUptime, null, 0, 1000);
    }

    private void UpdateUptime(object state)
    {
        if (_connectionStart.HasValue)
        {
            var uptime = DateTime.Now - _connectionStart.Value;
            _uptime = $"{uptime.Days}d {uptime.Hours}h {uptime.Minutes}m {uptime.Seconds}s";
            InvokeAsync(StateHasChanged);
        }
    }

    private void HandleConnected()
    {
        _systemStatus = "Online";
        _connectionStart = DateTime.Now;
        StateHasChanged();
    }

    private void HandleDisconnected(string reason)
    {
        _systemStatus = "Offline";
        _lastDisconnect = DateTime.Now;
        
        // Reduce stability score on disconnection
        _connectionStability = Math.Max(0, _connectionStability - 10);
        
        StateHasChanged();
    }

    private void HandleReconnecting(int attemptCount)
    {
        _systemStatus = "Reconnecting...";
        _reconnectionAttempts++;
        StateHasChanged();
    }

    private void HandleReconnected()
    {
        _systemStatus = "Online";
        
        // Gradually recover stability after successful reconnection
        _connectionStability = Math.Min(100, _connectionStability + 5);
        
        StateHasChanged();
    }

    private string GetHealthClass()
    {
        return _systemStatus switch
        {
            "Online" => "healthy",
            "Reconnecting..." => "degraded",
            "Offline" => "unhealthy",
            _ => ""
        };
    }

    private string GetStabilityClass()
    {
        return _connectionStability switch
        {
            >= 80 => "bg-success",
            >= 50 => "bg-warning",
            _ => "bg-danger"
        };
    }

    public void Dispose()
    {
        _uptimeTimer?.Dispose();
    }
}

<style>
    .health-monitor {
        background-color: #f8f9fa;
        border-radius: 8px;
        padding: 20px;
        box-shadow: 0 2px 4px rgba(0,0,0,0.1);
    }
    
    .health-status {
        display: flex;
        align-items: center;
        justify-content: space-between;
        margin-bottom: 20px;
        padding-bottom: 15px;
        border-bottom: 1px solid #dee2e6;
    }
    
    .health-indicator {
        width: 24px;
        height: 24px;
        border-radius: 50%;
    }
    
    .health-indicator.healthy {
        background-color: #28a745;
        box-shadow: 0 0 10px #28a745;
    }
    
    .health-indicator.degraded {
        background-color: #ffc107;
        box-shadow: 0 0 10px #ffc107;
    }
    
    .health-indicator.unhealthy {
        background-color: #dc3545;
        box-shadow: 0 0 10px #dc3545;
    }
    
    .health-metrics {
        display: flex;
        flex-direction: column;
        gap: 15px;
    }
    
    .metric {
        display: flex;
        justify-content: space-between;
        align-items: center;
    }
</style>
```

## CSS Customization

The Reconnector component uses the following CSS classes that can be customized:

- `.reconnector`: The main container for the reconnector component
- `.reconnector-indicator`: Container for the connection state indicator
- `.reconnector-indicator-connected`: Applied when the connection is established
- `.reconnector-indicator-reconnecting`: Applied when reconnection is in progress
- `.reconnector-indicator-disconnected`: Applied when the connection is lost

You can override these classes in your application's CSS to customize the appearance of the Reconnector component.

## JavaScript Interop

The Reconnector component uses JavaScript interop to monitor and manage connection states. It interacts with the browser's WebSocket API or the SignalR client library depending on the connection type. The component handles the following JavaScript events:

- Connection establishment and configuration
- Connection state change detection
- Reconnection attempt scheduling and execution
- Connection timeout management

## Accessibility Considerations

When using the Reconnector component, consider the following accessibility best practices:

1. Ensure connection state indicators have sufficient color contrast
2. Provide text alternatives for connection state icons
3. Use ARIA attributes to announce connection state changes to screen readers
4. Ensure manual reconnection controls are keyboard accessible
5. Consider adding sound notifications for critical connection state changes

## Browser Compatibility

The Reconnector component is compatible with all modern browsers that support Blazor WebAssembly or Blazor Server. The underlying WebSocket API is supported in all major browsers. For older browsers, consider implementing fallback mechanisms or displaying compatibility warnings.

## Integration with Other Components

The Reconnector component works well with:

- **SignalR Components**: For real-time communication features
- **Alert/Toast Components**: For displaying connection state notifications
- **Loading Components**: For indicating reconnection attempts
- **Dashboard Components**: For system health monitoring
- **Form Components**: For configuring connection parameters

## Best Practices

1. Configure reasonable retry limits and intervals to prevent excessive reconnection attempts
2. Implement exponential backoff with jitter to prevent reconnection storms
3. Provide clear visual feedback about connection state to users
4. Handle reconnection failures gracefully with user-friendly error messages
5. Consider implementing offline functionality for critical features
6. Use the OnConnectionStateChanged event to synchronize UI state with connection state
7. Test reconnection scenarios thoroughly, including various network conditions