# IpAddress Component

## Overview

The IpAddress component in BootstrapBlazor provides a specialized input control for entering and validating IP addresses. It breaks down the IP address into separate input fields for each octet, making it easier for users to enter valid IP addresses while providing immediate validation and formatting. This component is particularly useful in network configuration interfaces, server administration panels, and any application that requires IP address input.

## Features

- **Structured Input**: Divides IP address into separate input fields for each octet
- **Automatic Navigation**: Automatically moves focus to the next octet when the current one is complete
- **Validation**: Built-in validation to ensure each octet contains valid values (0-255)
- **IPv4 Support**: Specialized for standard IPv4 address format
- **Two-Way Binding**: Supports two-way data binding for easy integration with forms
- **Customizable Appearance**: Various styling options to match application design
- **Disabled State**: Support for disabling the entire control
- **Read-Only Mode**: Option to display IP addresses without allowing editing
- **Keyboard Navigation**: Supports keyboard navigation between octets using arrow keys and tab
- **Copy/Paste Support**: Allows copying and pasting complete IP addresses

## Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `Value` | `string` | `"0.0.0.0"` | The current IP address value. Can be bound using `@bind-Value`. |
| `ValueChanged` | `EventCallback<string>` | - | Event callback invoked when the IP address value changes. |
| `ValueExpression` | `Expression<Func<string>>` | `null` | Expression used for two-way binding of the Value property. |
| `IsDisabled` | `bool` | `false` | Whether the IP address control is disabled. |
| `IsReadOnly` | `bool` | `false` | Whether the IP address control is read-only. |
| `ShowLabel` | `bool` | `false` | Whether to show a label for the IP address control. |
| `DisplayText` | `string` | `"IP Address"` | The display text for the label when `ShowLabel` is true. |
| `PlaceHolder` | `string` | `"0.0.0.0"` | The placeholder text to display when the IP address is empty. |
| `ValidateRules` | `List<ValidateRule>` | `null` | Custom validation rules for the IP address. |
| `Size` | `Size` | `Size.Medium` | The size of the IP address control. Options include Small, Medium, and Large. |
| `AdditionalAttributes` | `Dictionary<string, object>` | `new()` | Additional attributes to apply to the IP address control. |
| `OnValidate` | `Func<Task<bool>>` | `null` | Custom validation function for the IP address. |
| `Class` | `string` | `""` | Additional CSS class(es) to apply to the IP address control. |
| `Style` | `string` | `""` | Additional inline styles to apply to the IP address control. |

## Events

| Event | Description |
|-------|-------------|
| `OnValueChanged` | Triggered when the IP address value changes. Provides the new value. |
| `OnBlur` | Triggered when the IP address control loses focus. |
| `OnFocus` | Triggered when the IP address control receives focus. |
| `OnKeyUp` | Triggered when a key is released while the IP address control has focus. |
| `OnKeyDown` | Triggered when a key is pressed while the IP address control has focus. |
| `OnValidated` | Triggered after the IP address has been validated. Provides the validation result. |

## Usage Examples

### Example 1: Basic IP Address Input

A simple IP address input with default settings:

```razor
<IpAddress @bind-Value="@_ipAddress" />

<div class="mt-3">
    <p>Current IP Address: @_ipAddress</p>
</div>

@code {
    private string _ipAddress = "192.168.1.1";
}
```

### Example 2: IP Address with Label and Validation

Adding a label and validation to the IP address control:

```razor
<ValidateForm Model="@_formModel" OnValidSubmit="HandleValidSubmit">
    <div class="row">
        <div class="col-md-6 mb-3">
            <IpAddress @bind-Value="@_formModel.ServerIpAddress"
                       ShowLabel="true"
                       DisplayText="Server IP Address"
                       IsRequired="true" />
        </div>
    </div>
    
    <Button Color="Color.Primary" ButtonType="ButtonType.Submit">Save Configuration</Button>
</ValidateForm>

<div class="mt-3 @(_formSubmitted ? "d-block" : "d-none")">
    <Alert Color="Color.Success" ShowDismiss="true">
        <p class="mb-0">Configuration saved successfully! Server IP Address: @_formModel.ServerIpAddress</p>
    </Alert>
</div>

@code {
    private ServerConfigModel _formModel = new();
    private bool _formSubmitted = false;

    private void HandleValidSubmit()
    {
        // In a real application, you would save the configuration to a server
        // For this example, we just show a success message
        _formSubmitted = true;
    }

    public class ServerConfigModel
    {
        [Required(ErrorMessage = "Server IP Address is required")]
        [RegularExpression(@"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$", 
                           ErrorMessage = "Please enter a valid IP address")]
        public string ServerIpAddress { get; set; } = "0.0.0.0";
    }
}
```

### Example 3: Multiple IP Address Inputs for Network Configuration

Creating a network configuration form with multiple IP address inputs:

```razor
<div class="card">
    <div class="card-header">
        <h5 class="mb-0">Network Configuration</h5>
    </div>
    <div class="card-body">
        <ValidateForm Model="@_networkConfig" OnValidSubmit="SaveNetworkConfig">
            <div class="row mb-3">
                <div class="col-md-6">
                    <IpAddress @bind-Value="@_networkConfig.IpAddress"
                               ShowLabel="true"
                               DisplayText="IP Address"
                               IsRequired="true" />
                </div>
                <div class="col-md-6">
                    <IpAddress @bind-Value="@_networkConfig.SubnetMask"
                               ShowLabel="true"
                               DisplayText="Subnet Mask"
                               IsRequired="true" />
                </div>
            </div>
            
            <div class="row mb-3">
                <div class="col-md-6">
                    <IpAddress @bind-Value="@_networkConfig.DefaultGateway"
                               ShowLabel="true"
                               DisplayText="Default Gateway"
                               IsRequired="true" />
                </div>
                <div class="col-md-6">
                    <IpAddress @bind-Value="@_networkConfig.PrimaryDns"
                               ShowLabel="true"
                               DisplayText="Primary DNS"
                               IsRequired="true" />
                </div>
            </div>
            
            <div class="row mb-3">
                <div class="col-md-6">
                    <IpAddress @bind-Value="@_networkConfig.SecondaryDns"
                               ShowLabel="true"
                               DisplayText="Secondary DNS (Optional)"
                               IsRequired="false" />
                </div>
            </div>
            
            <div class="d-flex justify-content-between">
                <Button Color="Color.Secondary" OnClick="ResetForm">Reset</Button>
                <Button Color="Color.Primary" ButtonType="ButtonType.Submit">Save Configuration</Button>
            </div>
        </ValidateForm>
    </div>
</div>

<div class="mt-3 @(_configSaved ? "d-block" : "d-none")">
    <Alert Color="Color.Success" ShowDismiss="true">
        <p class="mb-0">Network configuration saved successfully!</p>
    </Alert>
</div>

@code {
    private NetworkConfigModel _networkConfig = new();
    private bool _configSaved = false;

    private void SaveNetworkConfig()
    {
        // In a real application, you would save the configuration to a server
        // For this example, we just show a success message
        _configSaved = true;
    }

    private void ResetForm()
    {
        _networkConfig = new NetworkConfigModel();
        _configSaved = false;
    }

    public class NetworkConfigModel
    {
        [Required(ErrorMessage = "IP Address is required")]
        public string IpAddress { get; set; } = "192.168.1.100";

        [Required(ErrorMessage = "Subnet Mask is required")]
        public string SubnetMask { get; set; } = "255.255.255.0";

        [Required(ErrorMessage = "Default Gateway is required")]
        public string DefaultGateway { get; set; } = "192.168.1.1";

        [Required(ErrorMessage = "Primary DNS is required")]
        public string PrimaryDns { get; set; } = "8.8.8.8";

        public string SecondaryDns { get; set; } = "8.8.4.4";
    }
}
```

### Example 4: IP Address with Different Sizes

Demonstrating different sizes of the IP address control:

```razor
<div class="mb-3">
    <h5>Small Size</h5>
    <IpAddress @bind-Value="@_ipAddress" Size="Size.Small" />
</div>

<div class="mb-3">
    <h5>Medium Size (Default)</h5>
    <IpAddress @bind-Value="@_ipAddress" Size="Size.Medium" />
</div>

<div class="mb-3">
    <h5>Large Size</h5>
    <IpAddress @bind-Value="@_ipAddress" Size="Size.Large" />
</div>

@code {
    private string _ipAddress = "192.168.1.1";
}
```

### Example 5: Read-Only and Disabled IP Address Controls

Showing read-only and disabled states of the IP address control:

```razor
<div class="mb-3">
    <h5>Standard IP Address Input</h5>
    <IpAddress @bind-Value="@_ipAddress" />
</div>

<div class="mb-3">
    <h5>Read-Only IP Address</h5>
    <IpAddress Value="@_ipAddress" IsReadOnly="true" />
</div>

<div class="mb-3">
    <h5>Disabled IP Address</h5>
    <IpAddress Value="@_ipAddress" IsDisabled="true" />
</div>

<div class="mt-3">
    <Button Color="Color.Primary" OnClick="ChangeIpAddress">Change IP Address</Button>
</div>

@code {
    private string _ipAddress = "192.168.1.1";

    private void ChangeIpAddress()
    {
        // Generate a random IP address for demonstration
        var random = new Random();
        _ipAddress = $"{random.Next(1, 255)}.{random.Next(0, 255)}.{random.Next(0, 255)}.{random.Next(1, 255)}";
    }
}
```

### Example 6: IP Address with Event Handling

Implementing event handling with the IP address control:

```razor
<div class="card">
    <div class="card-header">
        <h5 class="mb-0">IP Address Event Handling</h5>
    </div>
    <div class="card-body">
        <IpAddress @bind-Value="@_ipAddress"
                   OnValueChanged="HandleIpChanged"
                   OnBlur="HandleBlur"
                   OnFocus="HandleFocus"
                   OnValidated="HandleValidated" />
    </div>
</div>

<div class="mt-3">
    <h5>Event Log</h5>
    <div class="border p-3 bg-light" style="max-height: 200px; overflow-y: auto;">
        @foreach (var log in _eventLogs.AsEnumerable().Reverse())
        {
            <div class="mb-1">@log</div>
        }
    </div>
</div>

<div class="mt-3">
    <Button Color="Color.Secondary" OnClick="ClearLogs">Clear Logs</Button>
</div>

@code {
    private string _ipAddress = "192.168.1.1";
    private List<string> _eventLogs = new();

    private void HandleIpChanged(string newValue)
    {
        _eventLogs.Add($"[{DateTime.Now:HH:mm:ss}] Value Changed: {newValue}");
    }

    private void HandleBlur()
    {
        _eventLogs.Add($"[{DateTime.Now:HH:mm:ss}] Control lost focus");
    }

    private void HandleFocus()
    {
        _eventLogs.Add($"[{DateTime.Now:HH:mm:ss}] Control gained focus");
    }

    private void HandleValidated(bool isValid)
    {
        _eventLogs.Add($"[{DateTime.Now:HH:mm:ss}] Validation result: {(isValid ? "Valid" : "Invalid")}");
    }

    private void ClearLogs()
    {
        _eventLogs.Clear();
    }
}
```

### Example 7: IP Address Range Configuration

Creating an IP address range configuration interface:

```razor
<div class="card">
    <div class="card-header">
        <h5 class="mb-0">IP Address Range Configuration</h5>
    </div>
    <div class="card-body">
        <ValidateForm Model="@_rangeConfig" OnValidSubmit="SaveRangeConfig">
            <div class="row mb-3">
                <div class="col-md-6">
                    <IpAddress @bind-Value="@_rangeConfig.StartIpAddress"
                               ShowLabel="true"
                               DisplayText="Start IP Address"
                               IsRequired="true"
                               OnValidated="ValidateIpRange" />
                </div>
                <div class="col-md-6">
                    <IpAddress @bind-Value="@_rangeConfig.EndIpAddress"
                               ShowLabel="true"
                               DisplayText="End IP Address"
                               IsRequired="true"
                               OnValidated="ValidateIpRange" />
                </div>
            </div>
            
            <div class="mb-3">
                <div class="form-check form-switch">
                    <input class="form-check-input" type="checkbox" id="excludeDhcp" @bind="_rangeConfig.ExcludeDhcpRange" />
                    <label class="form-check-label" for="excludeDhcp">Exclude DHCP Range</label>
                </div>
            </div>
            
            @if (_rangeConfig.ExcludeDhcpRange)
            {
                <div class="row mb-3">
                    <div class="col-md-6">
                        <IpAddress @bind-Value="@_rangeConfig.DhcpStartIpAddress"
                                   ShowLabel="true"
                                   DisplayText="DHCP Start IP Address"
                                   IsRequired="true" />
                    </div>
                    <div class="col-md-6">
                        <IpAddress @bind-Value="@_rangeConfig.DhcpEndIpAddress"
                                   ShowLabel="true"
                                   DisplayText="DHCP End IP Address"
                                   IsRequired="true" />
                    </div>
                </div>
            }
            
            <div class="mb-3">
                <label class="form-label">IP Address Range Preview</label>
                <div class="border p-3 bg-light">
                    <p class="mb-1">Total IPs in Range: @GetTotalIpsInRange()</p>
                    <p class="mb-1">Available IPs: @GetAvailableIps()</p>
                    <p class="mb-0">Range: @_rangeConfig.StartIpAddress to @_rangeConfig.EndIpAddress</p>
                    @if (_rangeConfig.ExcludeDhcpRange)
                    {
                        <p class="mb-0 text-danger">Excluded DHCP Range: @_rangeConfig.DhcpStartIpAddress to @_rangeConfig.DhcpEndIpAddress</p>
                    }
                </div>
            </div>
            
            <div class="d-flex justify-content-between">
                <Button Color="Color.Secondary" OnClick="ResetForm">Reset</Button>
                <Button Color="Color.Primary" ButtonType="ButtonType.Submit" IsDisabled="@(!_isRangeValid)">Save Configuration</Button>
            </div>
        </ValidateForm>
    </div>
</div>

<div class="mt-3 @(_configSaved ? "d-block" : "d-none")">
    <Alert Color="Color.Success" ShowDismiss="true">
        <p class="mb-0">IP Address Range configuration saved successfully!</p>
    </Alert>
</div>

@code {
    private IpRangeConfigModel _rangeConfig = new();
    private bool _configSaved = false;
    private bool _isRangeValid = true;

    private void SaveRangeConfig()
    {
        // In a real application, you would save the configuration to a server
        // For this example, we just show a success message
        _configSaved = true;
    }

    private void ResetForm()
    {
        _rangeConfig = new IpRangeConfigModel();
        _configSaved = false;
        _isRangeValid = true;
    }

    private void ValidateIpRange(bool isValid)
    {
        if (!isValid)
        {
            _isRangeValid = false;
            return;
        }

        // Check if start IP is less than end IP
        if (!string.IsNullOrEmpty(_rangeConfig.StartIpAddress) && 
            !string.IsNullOrEmpty(_rangeConfig.EndIpAddress))
        {
            var startIp = IpToLong(_rangeConfig.StartIpAddress);
            var endIp = IpToLong(_rangeConfig.EndIpAddress);
            _isRangeValid = startIp < endIp;
        }
        else
        {
            _isRangeValid = true;
        }
    }

    private long IpToLong(string ipAddress)
    {
        var octets = ipAddress.Split('.');
        if (octets.Length != 4) return 0;

        long result = 0;
        for (int i = 0; i < 4; i++)
        {
            if (int.TryParse(octets[i], out int octet))
            {
                result = (result << 8) + octet;
            }
        }
        return result;
    }

    private int GetTotalIpsInRange()
    {
        if (string.IsNullOrEmpty(_rangeConfig.StartIpAddress) || 
            string.IsNullOrEmpty(_rangeConfig.EndIpAddress))
        {
            return 0;
        }

        var startIp = IpToLong(_rangeConfig.StartIpAddress);
        var endIp = IpToLong(_rangeConfig.EndIpAddress);
        return (int)(endIp - startIp + 1);
    }

    private int GetAvailableIps()
    {
        var total = GetTotalIpsInRange();
        if (_rangeConfig.ExcludeDhcpRange && 
            !string.IsNullOrEmpty(_rangeConfig.DhcpStartIpAddress) && 
            !string.IsNullOrEmpty(_rangeConfig.DhcpEndIpAddress))
        {
            var dhcpStartIp = IpToLong(_rangeConfig.DhcpStartIpAddress);
            var dhcpEndIp = IpToLong(_rangeConfig.DhcpEndIpAddress);
            var startIp = IpToLong(_rangeConfig.StartIpAddress);
            var endIp = IpToLong(_rangeConfig.EndIpAddress);

            // Check if DHCP range overlaps with the main range
            if (dhcpStartIp >= startIp && dhcpEndIp <= endIp)
            {
                return total - (int)(dhcpEndIp - dhcpStartIp + 1);
            }
        }
        return total;
    }

    public class IpRangeConfigModel
    {
        [Required(ErrorMessage = "Start IP Address is required")]
        public string StartIpAddress { get; set; } = "192.168.1.1";

        [Required(ErrorMessage = "End IP Address is required")]
        public string EndIpAddress { get; set; } = "192.168.1.254";

        public bool ExcludeDhcpRange { get; set; } = false;

        public string DhcpStartIpAddress { get; set; } = "192.168.1.100";

        public string DhcpEndIpAddress { get; set; } = "192.168.1.150";
    }
}
```

## CSS Customization

The IpAddress component can be customized using the following CSS classes:

- `.ip-address-container`: The main container for the IP address control
- `.ip-address-input`: Individual input fields for each octet
- `.ip-address-separator`: The separator between octets (typically a dot)
- `.ip-address-disabled`: Applied when the control is disabled
- `.ip-address-readonly`: Applied when the control is read-only
- `.ip-address-invalid`: Applied when the control contains invalid input

You can override these classes in your application's CSS to customize the appearance of the IpAddress component. For example:

```css
/* Custom IP address control styling */
.ip-address-container {
    display: inline-flex;
    align-items: center;
    border: 1px solid #ced4da;
    border-radius: 4px;
    padding: 2px;
    background-color: #fff;
}

.ip-address-input {
    width: 3em;
    border: none;
    text-align: center;
    outline: none;
    padding: 0.375rem 0.1rem;
}

.ip-address-separator {
    margin: 0 2px;
    color: #6c757d;
    font-weight: bold;
}

.ip-address-container:focus-within {
    border-color: #86b7fe;
    box-shadow: 0 0 0 0.25rem rgba(13, 110, 253, 0.25);
}

.ip-address-disabled {
    background-color: #e9ecef;
    opacity: 0.65;
}

.ip-address-readonly {
    background-color: #f8f9fa;
}

.ip-address-invalid {
    border-color: #dc3545;
}

.ip-address-invalid:focus-within {
    border-color: #dc3545;
    box-shadow: 0 0 0 0.25rem rgba(220, 53, 69, 0.25);
}
```

## JavaScript Interop

The IpAddress component primarily operates on the client side using Blazor's component model. It may use JavaScript interop for the following features:

- Handling focus management between octet inputs
- Managing clipboard operations for copy/paste functionality
- Implementing keyboard navigation between octets
- Validating input values in real-time

## Accessibility Considerations

When using the IpAddress component, consider the following accessibility best practices:

1. Ensure the component can be navigated using keyboard (Tab to move between octets, arrow keys for navigation within the component)
2. Provide clear labels using the `ShowLabel` and `DisplayText` properties
3. Use sufficient color contrast between the input text and background
4. Ensure validation errors are properly communicated to assistive technologies
5. Consider adding ARIA attributes for better screen reader support

## Browser Compatibility

The IpAddress component is compatible with all modern browsers that support Blazor WebAssembly or Blazor Server. There are no specific browser compatibility issues to be aware of.

## Integration with Other Components

The IpAddress component works well with:

- **Form Components**: For collecting IP address input as part of a larger form
- **Validation Components**: For validating IP address input
- **Button Components**: For form submission or IP address manipulation
- **Alert Components**: For providing feedback on IP address validation
- **Card Components**: For organizing IP address configuration interfaces

## Best Practices

1. Always provide clear labels for IP address inputs to indicate their purpose
2. Use validation to ensure entered IP addresses are valid
3. Consider using read-only mode for displaying IP addresses that shouldn't be edited
4. Provide helpful error messages when validation fails
5. Group related IP address inputs (like network configuration) together
6. Consider the context in which the IP address will be used when setting default values
7. Use consistent styling for all IP address inputs in your application