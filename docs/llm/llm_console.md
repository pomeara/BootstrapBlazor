# Console Component

## Overview
The Console component in BootstrapBlazor provides a terminal-like interface for displaying logs, command outputs, and interactive command execution within web applications. It simulates a command-line environment with features like command history, syntax highlighting, and scrollable output display.

## Key Features
- Terminal-like interface for displaying text output
- Support for command history navigation
- Customizable appearance (colors, fonts, size)
- Auto-scrolling capability
- Text filtering and search functionality
- Copy-to-clipboard support
- Clear console option
- Optional command input for interactive use
- Syntax highlighting for different message types
- Virtualized scrolling for performance with large outputs

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `Height` | string | "300px" | Sets the height of the console component |
| `Width` | string | "100%" | Sets the width of the console component |
| `ShowPrompt` | bool | true | Determines whether to show the command prompt |
| `PromptText` | string | ">" | The text to display as the command prompt |
| `EnableInput` | bool | true | Enables or disables user input |
| `MaxHistoryCount` | int | 100 | Maximum number of commands to keep in history |
| `MaxOutputLines` | int | 1000 | Maximum number of lines to display in the console |
| `Theme` | ConsoleTheme | Dark | The color theme of the console (Dark/Light) |
| `AutoScroll` | bool | true | Automatically scrolls to the bottom when new content is added |
| `Items` | IEnumerable<ConsoleItem> | null | Collection of console items to display |
| `CommandExecutor` | Func<string, Task<string>> | null | Function to execute when a command is entered |
| `Filter` | string | null | Text filter to apply to console output |
| `ShowTimestamp` | bool | false | Shows timestamp for each console entry |
| `ReadOnly` | bool | false | Makes the console read-only |

## Events

| Event | Description |
| --- | --- |
| `OnCommandExecuting` | Triggered before a command is executed |
| `OnCommandExecuted` | Triggered after a command is executed |
| `OnClear` | Triggered when the console is cleared |
| `OnFilterChanged` | Triggered when the output filter is changed |
| `OnCopy` | Triggered when content is copied to clipboard |

## Usage Examples

### Example 1: Basic Console Output

```razor
@page "/console-demo"

<Console @ref="console" Height="400px" />

<Button OnClick="AddMessage">Add Message</Button>
<Button OnClick="AddError">Add Error</Button>
<Button OnClick="ClearConsole">Clear</Button>

@code {
    private Console console;

    private void AddMessage()
    {
        console.WriteLine("This is a regular message");
    }

    private void AddError()
    {
        console.WriteError("This is an error message");
    }

    private void ClearConsole()
    {
        console.Clear();
    }
}
```

### Example 2: Interactive Command Console

```razor
@page "/interactive-console"

<Console @ref="console" 
         Height="500px" 
         EnableInput="true"
         PromptText="$"
         CommandExecutor="ExecuteCommand" />

@code {
    private Console console;

    private async Task<string> ExecuteCommand(string command)
    {
        // Process the command and return the result
        switch (command.ToLower())
        {
            case "help":
                return "Available commands: help, version, echo, clear";
            case "version":
                return "Console Demo v1.0.0";
            case "clear":
                console.Clear();
                return string.Empty;
            default:
                if (command.StartsWith("echo "))
                {
                    return command.Substring(5);
                }
                return $"Unknown command: {command}";
        }
    }
}
```

### Example 3: Styled Console with Custom Theme

```razor
<Console Theme="ConsoleTheme.Light"
         Width="800px"
         Height="400px"
         CssClass="my-custom-console"
         ShowTimestamp="true" />

<style>
    .my-custom-console {
        border-radius: 8px;
        font-family: 'Fira Code', monospace;
        box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
    }
</style>
```

### Example 4: Log Viewer with Filtering

```razor
@page "/log-viewer"
@inject LogService LogService

<div class="mb-3">
    <Input @bind-Value="filter" placeholder="Filter logs..." OnInput="ApplyFilter" />
</div>

<Console @ref="console" 
         Height="600px" 
         Filter="@filter"
         EnableInput="false"
         ReadOnly="true"
         OnFilterChanged="HandleFilterChanged" />

<div class="mt-3">
    <Button Color="Color.Primary" OnClick="RefreshLogs">Refresh</Button>
    <Button Color="Color.Secondary" OnClick="ClearLogs">Clear</Button>
    <Button Color="Color.Success" OnClick="CopyLogs">Copy All</Button>
</div>

@code {
    private Console console;
    private string filter;

    protected override async Task OnInitializedAsync()
    {
        await LoadLogs();
    }

    private async Task LoadLogs()
    {
        var logs = await LogService.GetLogsAsync();
        console.Clear();
        
        foreach (var log in logs)
        {
            switch (log.Level.ToLower())
            {
                case "error":
                    console.WriteError(log.Message);
                    break;
                case "warning":
                    console.WriteWarning(log.Message);
                    break;
                case "info":
                    console.WriteLine(log.Message);
                    break;
                default:
                    console.WriteDebug(log.Message);
                    break;
            }
        }
    }

    private void ApplyFilter()
    {
        // Filter is automatically applied by the Console component
    }

    private void HandleFilterChanged(string newFilter)
    {
        // React to filter changes if needed
    }

    private async Task RefreshLogs()
    {
        await LoadLogs();
    }

    private void ClearLogs()
    {
        console.Clear();
    }

    private void CopyLogs()
    {
        console.CopyToClipboard();
    }
}
```

### Example 5: Real-time System Monitoring

```razor
@page "/system-monitor"
@implements IDisposable
@inject SystemMonitorService MonitorService

<Console @ref="console" 
         Height="500px" 
         ShowTimestamp="true"
         MaxOutputLines="5000"
         EnableInput="false" />

@code {
    private Console console;
    private Timer timer;

    protected override void OnInitialized()
    {
        // Update system stats every 2 seconds
        timer = new Timer(UpdateStats, null, 0, 2000);
    }

    private async void UpdateStats(object state)
    {
        var stats = await MonitorService.GetSystemStatsAsync();
        
        await InvokeAsync(() => {
            console.WriteLine($"CPU: {stats.CpuUsage}% | Memory: {stats.MemoryUsage}MB | Disk: {stats.DiskUsage}%");
            
            // Highlight high resource usage
            if (stats.CpuUsage > 90)
            {
                console.WriteWarning($"High CPU usage detected: {stats.CpuUsage}%");
            }
            
            if (stats.MemoryUsage > stats.TotalMemory * 0.9)
            {
                console.WriteError($"Memory usage critical: {stats.MemoryUsage}MB / {stats.TotalMemory}MB");
            }
            
            StateHasChanged();
        });
    }

    public void Dispose()
    {
        timer?.Dispose();
    }
}
```

### Example 6: Command History Navigation

```razor
@page "/command-history"

<Console @ref="console"
         Height="400px"
         EnableInput="true"
         MaxHistoryCount="20"
         CommandExecutor="ExecuteCommand"
         OnCommandExecuted="CommandExecuted" />

<div class="mt-3">
    <h5>Command History:</h5>
    <ul class="list-group">
        @foreach (var cmd in commandHistory)
        {
            <li class="list-group-item d-flex justify-content-between align-items-center">
                @cmd
                <Button Size="Size.Small" OnClick="() => RerunCommand(cmd)">Rerun</Button>
            </li>
        }
    </ul>
</div>

@code {
    private Console console;
    private List<string> commandHistory = new List<string>();

    private async Task<string> ExecuteCommand(string command)
    {
        if (string.IsNullOrWhiteSpace(command))
            return string.Empty;
            
        return $"Executed: {command}";
    }

    private void CommandExecuted(string command)
    {
        if (!string.IsNullOrWhiteSpace(command) && !commandHistory.Contains(command))
        {
            commandHistory.Insert(0, command);
            
            // Keep only the last 20 commands
            if (commandHistory.Count > 20)
            {
                commandHistory.RemoveAt(commandHistory.Count - 1);
            }
        }
    }

    private async Task RerunCommand(string command)
    {
        await console.ExecuteCommandAsync(command);
    }
}
```

### Example 7: Network Diagnostics Console

```razor
@page "/network-diagnostics"
@inject NetworkService NetworkService

<div class="row mb-3">
    <div class="col">
        <Input @bind-Value="host" placeholder="Enter hostname or IP" />
    </div>
    <div class="col-auto">
        <Button Color="Color.Primary" OnClick="RunPing">Ping</Button>
        <Button Color="Color.Secondary" OnClick="RunTraceroute">Traceroute</Button>
        <Button Color="Color.Success" OnClick="RunDnsLookup">DNS Lookup</Button>
    </div>
</div>

<Console @ref="console"
         Height="500px"
         ShowTimestamp="true"
         PromptText="network>" />

@code {
    private Console console;
    private string host = "example.com";

    private async Task RunPing()
    {
        if (string.IsNullOrWhiteSpace(host))
        {
            console.WriteError("Please enter a valid hostname or IP address");
            return;
        }

        console.WriteLine($"Pinging {host}...");
        
        try
        {
            var results = await NetworkService.PingHostAsync(host, 4);
            
            foreach (var result in results)
            {
                if (result.Success)
                {
                    console.WriteLine($"Reply from {result.Address}: time={result.RoundtripTime}ms");
                }
                else
                {
                    console.WriteWarning($"Request timed out for {host}");
                }
            }
            
            var summary = results.Where(r => r.Success).ToList();
            console.WriteLine($"Ping statistics for {host}:");
            console.WriteLine($"    Packets: Sent = 4, Received = {summary.Count}, Lost = {4 - summary.Count} ({(4 - summary.Count) * 25}% loss)");
            
            if (summary.Any())
            {
                console.WriteLine($"Approximate round trip times in milliseconds:");
                console.WriteLine($"    Minimum = {summary.Min(r => r.RoundtripTime)}ms, Maximum = {summary.Max(r => r.RoundtripTime)}ms, Average = {summary.Average(r => r.RoundtripTime):F2}ms");
            }
        }
        catch (Exception ex)
        {
            console.WriteError($"Error: {ex.Message}");
        }
    }

    private async Task RunTraceroute()
    {
        if (string.IsNullOrWhiteSpace(host))
        {
            console.WriteError("Please enter a valid hostname or IP address");
            return;
        }

        console.WriteLine($"Tracing route to {host}...");
        
        try
        {
            var hops = await NetworkService.TracerouteAsync(host);
            
            for (int i = 0; i < hops.Count; i++)
            {
                var hop = hops[i];
                if (hop.Success)
                {
                    console.WriteLine($"{i + 1}  {hop.Address}  {hop.RoundtripTime}ms");
                }
                else
                {
                    console.WriteLine($"{i + 1}  *  Request timed out.");
                }
                
                // Check if we've reached the destination
                if (hop.IsDestination)
                {
                    console.WriteLine($"Trace complete.");
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            console.WriteError($"Error: {ex.Message}");
        }
    }

    private async Task RunDnsLookup()
    {
        if (string.IsNullOrWhiteSpace(host))
        {
            console.WriteError("Please enter a valid hostname or IP address");
            return;
        }

        console.WriteLine($"Looking up DNS records for {host}...");
        
        try
        {
            var records = await NetworkService.DnsLookupAsync(host);
            
            if (records.Any())
            {
                console.WriteLine($"DNS records for {host}:");
                foreach (var record in records)
                {
                    console.WriteLine($"Type: {record.Type}, Value: {record.Value}, TTL: {record.TimeToLive}");
                }
            }
            else
            {
                console.WriteWarning($"No DNS records found for {host}");
            }
        }
        catch (Exception ex)
        {
            console.WriteError($"Error: {ex.Message}");
        }
    }
}
```

## CSS Customization

The Console component can be customized using CSS variables and classes:

```css
/* Custom console styling */
.bootstrap-console {
    --bb-console-bg: #1e1e1e;
    --bb-console-text: #f0f0f0;
    --bb-console-prompt: #569cd6;
    --bb-console-error: #f44747;
    --bb-console-warning: #dcdcaa;
    --bb-console-info: #6a9955;
    --bb-console-debug: #c586c0;
    --bb-console-font: 'Consolas', monospace;
    --bb-console-font-size: 14px;
    --bb-console-line-height: 1.5;
    --bb-console-padding: 12px;
}

/* Light theme override */
.bootstrap-console.console-light {
    --bb-console-bg: #f5f5f5;
    --bb-console-text: #333333;
    --bb-console-prompt: #0066cc;
    --bb-console-error: #cc0000;
    --bb-console-warning: #cc6600;
    --bb-console-info: #006600;
    --bb-console-debug: #660066;
}
```

## JavaScript Interop

The Console component uses JavaScript interop for certain features like scrolling, clipboard operations, and command history navigation. These operations are handled internally by the component.

## Accessibility

The Console component includes the following accessibility features:
- Keyboard navigation for command history (up/down arrows)
- ARIA roles and labels for screen readers
- Focus management for the input field
- High contrast support through custom themes

## Browser Compatibility

The Console component is compatible with all modern browsers including:
- Chrome
- Firefox
- Safari
- Edge

## Performance Considerations

For large output logs, consider:
- Setting appropriate `MaxOutputLines` to limit memory usage
- Using virtualized scrolling for better performance
- Implementing filtering to reduce the rendered content

## Integration with Other Components

The Console component works well with:
- Button components for control actions
- Input components for filtering
- Modal/Dialog components for displaying in a popup
- Tab components for organizing multiple consoles

## Best Practices

1. Set appropriate height and width to avoid layout shifts
2. Use different message types (error, warning, info) for better visual hierarchy
3. Implement command history for better user experience in interactive consoles
4. Consider using a monospace font for better readability
5. Provide clear error messages for failed commands
6. Implement auto-scrolling for real-time logs but allow users to pause scrolling
7. Use timestamps for time-sensitive logging information