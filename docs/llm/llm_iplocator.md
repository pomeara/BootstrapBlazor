# IPLocator Component

## Overview
The IPLocator component in BootstrapBlazor provides functionality to determine geographical information based on IP addresses. It allows developers to easily retrieve location data such as country, region, city, coordinates, timezone, and ISP information for a given IP address. This component is particularly useful for applications that need to customize content based on user location, implement geo-targeting features, or collect analytics data with geographical context.

## Features
- **IP Address Lookup**: Retrieve geographical information for any valid IP address
- **Current User IP Detection**: Automatically detect and locate the current user's IP address
- **Multiple Data Sources**: Support for various IP geolocation data providers
- **Caching Mechanism**: Cache lookup results to improve performance and reduce API calls
- **Asynchronous Processing**: Non-blocking IP lookup operations
- **Fallback Providers**: Automatic fallback to alternative providers if primary source fails
- **Customizable Display**: Flexible options for displaying location information
- **Offline Database Support**: Optional support for offline IP database files
- **Privacy Controls**: Configurable privacy settings for handling sensitive location data
- **Event Notifications**: Events for successful lookups, errors, and data changes

## Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `IPAddress` | string | null | The IP address to locate. If not specified, the component will attempt to detect the current user's IP |
| `Provider` | IPLocatorProvider | IPLocatorProvider.Default | The geolocation data provider to use for lookups |
| `ApiKey` | string | null | API key for the selected provider (if required) |
| `EnableCaching` | bool | true | Whether to cache lookup results to improve performance |
| `CacheExpiration` | TimeSpan | 24 hours | How long to cache IP lookup results |
| `Timeout` | int | 5000 | Timeout in milliseconds for API requests |
| `ShowLoading` | bool | true | Whether to show a loading indicator during lookups |
| `LoadingText` | string | "Locating..." | Text to display while performing a lookup |
| `ErrorText` | string | "Location lookup failed" | Text to display when a lookup fails |
| `DisplayFormat` | string | "{City}, {Region}, {Country}" | Format string for displaying location information |
| `ShowMap` | bool | false | Whether to display a map with the located position |
| `MapWidth` | string | "100%" | Width of the map if ShowMap is true |
| `MapHeight` | string | "300px" | Height of the map if ShowMap is true |
| `MapZoom` | int | 10 | Zoom level of the map if ShowMap is true |
| `MapProvider` | MapProvider | MapProvider.OpenStreetMap | The map provider to use if ShowMap is true |
| `OfflineDatabasePath` | string | null | Path to an offline IP database file (if using offline mode) |
| `UseOfflineDatabase` | bool | false | Whether to use an offline database instead of online API |
| `AutoLocate` | bool | false | Whether to automatically perform a lookup when the component initializes |
| `ChildContent` | RenderFragment | null | Content to be displayed inside the component |
| `LocationTemplate` | RenderFragment<IPLocation> | null | Custom template for displaying location information |

## Events

| Event | Description |
|-------|-------------|
| `OnLocationFound` | Triggered when location information is successfully retrieved |
| `OnLocationError` | Triggered when an error occurs during the lookup process |
| `OnLocationChanged` | Triggered when the location information changes |
| `OnLookupStart` | Triggered when a lookup operation begins |
| `OnLookupComplete` | Triggered when a lookup operation completes (regardless of success or failure) |

## Usage Examples

### Example 1: Basic IP Location Lookup
```razor
<IPLocator IPAddress="8.8.8.8" OnLocationFound="HandleLocationFound" />

@code {
    private IPLocation location;
    
    private void HandleLocationFound(IPLocation result)
    {
        location = result;
        StateHasChanged();
    }
}

@if (location != null)
{
    <div class="mt-3">
        <h5>Location Information:</h5>
        <p><strong>IP Address:</strong> @location.IPAddress</p>
        <p><strong>Country:</strong> @location.Country (@location.CountryCode)</p>
        <p><strong>Region:</strong> @location.Region</p>
        <p><strong>City:</strong> @location.City</p>
        <p><strong>Coordinates:</strong> @location.Latitude, @location.Longitude</p>
        <p><strong>Timezone:</strong> @location.Timezone</p>
        <p><strong>ISP:</strong> @location.ISP</p>
    </div>
}
```

### Example 2: Auto-Detect Current User's Location
```razor
<IPLocator AutoLocate="true"
           ShowLoading="true"
           LoadingText="Detecting your location..."
           OnLocationFound="HandleLocationFound"
           OnLocationError="HandleLocationError" />

@code {
    private IPLocation userLocation;
    private string errorMessage;
    
    private void HandleLocationFound(IPLocation result)
    {
        userLocation = result;
        errorMessage = null;
        StateHasChanged();
    }
    
    private void HandleLocationError(string error)
    {
        errorMessage = error;
        StateHasChanged();
    }
}

@if (errorMessage != null)
{
    <div class="alert alert-danger mt-3">
        <p>@errorMessage</p>
    </div>
}

@if (userLocation != null)
{
    <div class="alert alert-success mt-3">
        <p>We detected you're browsing from <strong>@userLocation.City, @userLocation.Region, @userLocation.Country</strong>.</p>
    </div>
}
```

### Example 3: Custom Location Display Template
```razor
<IPLocator IPAddress="93.184.216.34" AutoLocate="true">
    <LocationTemplate Context="location">
        <div class="card">
            <div class="card-header bg-primary text-white">
                <h5 class="mb-0">IP Location: @location.IPAddress</h5>
            </div>
            <div class="card-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="mb-3">
                            <i class="fa-solid fa-globe me-2"></i>
                            <strong>Country:</strong> @location.Country
                            @if (!string.IsNullOrEmpty(location.CountryCode))
                            {
                                <img src="/flags/@(location.CountryCode.ToLower()).svg" alt="@location.Country Flag" height="20" class="ms-2" />
                            }
                        </div>
                        <div class="mb-3">
                            <i class="fa-solid fa-map-marker-alt me-2"></i>
                            <strong>Region:</strong> @location.Region
                        </div>
                        <div class="mb-3">
                            <i class="fa-solid fa-city me-2"></i>
                            <strong>City:</strong> @location.City
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="mb-3">
                            <i class="fa-solid fa-location-crosshairs me-2"></i>
                            <strong>Coordinates:</strong> @location.Latitude, @location.Longitude
                        </div>
                        <div class="mb-3">
                            <i class="fa-solid fa-clock me-2"></i>
                            <strong>Timezone:</strong> @location.Timezone
                        </div>
                        <div class="mb-3">
                            <i class="fa-solid fa-network-wired me-2"></i>
                            <strong>ISP:</strong> @location.ISP
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </LocationTemplate>
</IPLocator>
```

### Example 4: IP Location with Map Display
```razor
<IPLocator IPAddress="8.8.8.8"
           ShowMap="true"
           MapWidth="100%"
           MapHeight="400px"
           MapZoom="12"
           MapProvider="MapProvider.GoogleMaps"
           OnLocationFound="HandleLocationFound" />

@code {
    private IPLocation location;
    
    private void HandleLocationFound(IPLocation result)
    {
        location = result;
        StateHasChanged();
    }
}

@if (location != null)
{
    <div class="mt-3">
        <h5>Location Details:</h5>
        <p>@location.City, @location.Region, @location.Country</p>
        <p><small class="text-muted">Coordinates: @location.Latitude, @location.Longitude</small></p>
    </div>
}
```

### Example 5: Multiple IP Address Lookup
```razor
@code {
    private List<string> ipAddresses = new List<string>
    {
        "8.8.8.8",
        "1.1.1.1",
        "93.184.216.34"
    };
    
    private Dictionary<string, IPLocation> locationResults = new Dictionary<string, IPLocation>();
    private Dictionary<string, string> locationErrors = new Dictionary<string, string>();
    private string currentIp;
    
    private async Task LookupAllIPs()
    {
        locationResults.Clear();
        locationErrors.Clear();
        
        foreach (var ip in ipAddresses)
        {
            currentIp = ip;
            await ipLocatorRef.LookupAsync(ip);
        }
    }
    
    private void HandleLocationFound(IPLocation result)
    {
        locationResults[result.IPAddress] = result;
        StateHasChanged();
    }
    
    private void HandleLocationError(string error)
    {
        locationErrors[currentIp] = error;
        StateHasChanged();
    }
}

<IPLocator @ref="ipLocatorRef"
           OnLocationFound="HandleLocationFound"
           OnLocationError="HandleLocationError" />

<div class="mb-3">
    <Button Text="Lookup All IPs" OnClick="LookupAllIPs" />
</div>

<Table TItem="KeyValuePair<string, IPLocation>"
       Items="locationResults"
       IsStriped="true"
       IsBordered="true">
    <TableColumns>
        <TableColumn @bind-Field="@context.Key" Text="IP Address" Width="150" />
        <TableColumn Text="Country" Width="150">
            <Template Context="data">
                @data.Value.Value.Country (@data.Value.Value.CountryCode)
            </Template>
        </TableColumn>
        <TableColumn Text="City/Region" Width="200">
            <Template Context="data">
                @data.Value.Value.City, @data.Value.Value.Region
            </Template>
        </TableColumn>
        <TableColumn Text="Coordinates" Width="200">
            <Template Context="data">
                @data.Value.Value.Latitude, @data.Value.Value.Longitude
            </Template>
        </TableColumn>
        <TableColumn Text="ISP">
            <Template Context="data">
                @data.Value.Value.ISP
            </Template>
        </TableColumn>
    </TableColumns>
</Table>

@if (locationErrors.Any())
{
    <div class="mt-3">
        <h5>Errors:</h5>
        <ul class="list-group">
            @foreach (var error in locationErrors)
            {
                <li class="list-group-item list-group-item-danger">@error.Key: @error.Value</li>
            }
        </ul>
    </div>
}
```

### Example 6: IP Location with Offline Database
```razor
@inject IWebHostEnvironment Environment

@code {
    private string offlineDatabasePath;
    private IPLocation location;
    private string errorMessage;
    
    protected override void OnInitialized()
    {
        offlineDatabasePath = Path.Combine(Environment.ContentRootPath, "wwwroot", "data", "ip-database.mmdb");
    }
    
    private void HandleLocationFound(IPLocation result)
    {
        location = result;
        errorMessage = null;
        StateHasChanged();
    }
    
    private void HandleLocationError(string error)
    {
        errorMessage = error;
        StateHasChanged();
    }
}

<IPLocator UseOfflineDatabase="true"
           OfflineDatabasePath="@offlineDatabasePath"
           AutoLocate="true"
           OnLocationFound="HandleLocationFound"
           OnLocationError="HandleLocationError" />

<div class="mt-3">
    <h5>IP Location (Offline Mode)</h5>
    
    @if (errorMessage != null)
    {
        <div class="alert alert-danger">
            <p>@errorMessage</p>
        </div>
    }
    
    @if (location != null)
    {
        <div class="alert alert-info">
            <p><strong>IP:</strong> @location.IPAddress</p>
            <p><strong>Location:</strong> @location.City, @location.Region, @location.Country</p>
            <p><small class="text-muted">Using offline database: @offlineDatabasePath</small></p>
        </div>
    }
</div>
```

### Example 7: IP Location for Analytics Dashboard
```razor
@inject IJSRuntime JSRuntime

@code {
    private List<VisitorData> visitors = new List<VisitorData>();
    private IPLocator ipLocatorRef;
    private bool isLoading = false;
    private string currentVisitorId;
    
    private class VisitorData
    {
        public string Id { get; set; }
        public string IPAddress { get; set; }
        public DateTime VisitTime { get; set; }
        public string PageVisited { get; set; }
        public IPLocation Location { get; set; }
    }
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            // Simulate loading visitor data from a database
            await LoadVisitorData();
        }
    }
    
    private async Task LoadVisitorData()
    {
        isLoading = true;
        StateHasChanged();
        
        // Simulate API call to get visitor data
        await Task.Delay(1000);
        
        visitors = new List<VisitorData>
        {
            new VisitorData { Id = "v1", IPAddress = "8.8.8.8", VisitTime = DateTime.Now.AddHours(-1), PageVisited = "/products" },
            new VisitorData { Id = "v2", IPAddress = "1.1.1.1", VisitTime = DateTime.Now.AddHours(-2), PageVisited = "/home" },
            new VisitorData { Id = "v3", IPAddress = "93.184.216.34", VisitTime = DateTime.Now.AddHours(-3), PageVisited = "/contact" }
        };
        
        isLoading = false;
        StateHasChanged();
        
        // Lookup locations for each visitor
        foreach (var visitor in visitors)
        {
            await LookupVisitorLocation(visitor);
        }
    }
    
    private async Task LookupVisitorLocation(VisitorData visitor)
    {
        currentVisitorId = visitor.Id;
        await ipLocatorRef.LookupAsync(visitor.IPAddress);
    }
    
    private void HandleLocationFound(IPLocation result)
    {
        var visitor = visitors.FirstOrDefault(v => v.IPAddress == result.IPAddress);
        if (visitor != null)
        {
            visitor.Location = result;
            StateHasChanged();
        }
    }
    
    private async Task ShowVisitorOnMap(VisitorData visitor)
    {
        if (visitor.Location != null)
        {
            await JSRuntime.InvokeVoidAsync("showLocationOnMap", 
                visitor.Location.Latitude, 
                visitor.Location.Longitude, 
                $"Visitor from {visitor.Location.City}, {visitor.Location.Country}");
        }
    }
}

<div class="analytics-dashboard">
    <h3>Visitor Analytics</h3>
    
    <IPLocator @ref="ipLocatorRef"
               OnLocationFound="HandleLocationFound" />
    
    @if (isLoading)
    {
        <div class="text-center my-4">
            <Spinner />
            <p>Loading visitor data...</p>
        </div>
    }
    else
    {
        <div class="row mt-4">
            <div class="col-md-8">
                <Table TItem="VisitorData"
                       Items="visitors"
                       IsStriped="true">
                    <TableColumns>
                        <TableColumn @bind-Field="@context.VisitTime" Text="Visit Time" FormatString="{0:HH:mm:ss}" Width="120" />
                        <TableColumn @bind-Field="@context.IPAddress" Text="IP Address" Width="150" />
                        <TableColumn @bind-Field="@context.PageVisited" Text="Page" Width="150" />
                        <TableColumn Text="Location" Width="250">
                            <Template Context="data">
                                @if (data.Location != null)
                                {
                                    <span>@data.Location.City, @data.Location.Country</span>
                                }
                                else
                                {
                                    <span class="text-muted">Locating...</span>
                                }
                            </Template>
                        </TableColumn>
                        <TableColumn Text="Actions" Width="100">
                            <Template Context="data">
                                <Button Icon="fa-solid fa-map-marker-alt"
                                        Size="Size.Small"
                                        Color="Color.Info"
                                        OnClick="() => ShowVisitorOnMap(data)"
                                        Disabled="@(data.Location == null)" />
                            </Template>
                        </TableColumn>
                    </TableColumns>
                </Table>
            </div>
            <div class="col-md-4">
                <div id="visitorMap" style="height: 400px; border: 1px solid #dee2e6; border-radius: 4px;"></div>
            </div>
        </div>
    }
</div>

@* Add this JavaScript to your _Host.cshtml or index.html *@
@* 
<script>
    // Initialize map (using Leaflet.js as an example)
    let map;
    let markers = [];
    
    document.addEventListener('DOMContentLoaded', function() {
        initMap();
    });
    
    function initMap() {
        if (document.getElementById('visitorMap')) {
            map = L.map('visitorMap').setView([0, 0], 2);
            
            L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
                attribution: '&copy; OpenStreetMap contributors'
            }).addTo(map);
        }
    }
    
    window.showLocationOnMap = function(lat, lng, title) {
        if (!map) initMap();
        
        // Clear existing markers
        markers.forEach(marker => map.removeLayer(marker));
        markers = [];
        
        // Add new marker
        const marker = L.marker([lat, lng]).addTo(map);
        marker.bindPopup(title).openPopup();
        markers.push(marker);
        
        // Center map on marker
        map.setView([lat, lng], 10);
    };
</script>
*@
```

## CSS Customization

The IPLocator component can be customized using CSS variables and classes:

```css
/* Custom IPLocator styling */
.bb-ip-locator {
    --bb-ip-locator-bg: #f8f9fa;
    --bb-ip-locator-border-color: #dee2e6;
    --bb-ip-locator-border-radius: 0.375rem;
    --bb-ip-locator-padding: 1rem;
    --bb-ip-locator-loading-color: #007bff;
    --bb-ip-locator-error-color: #dc3545;
    
    background-color: var(--bb-ip-locator-bg);
    border: 1px solid var(--bb-ip-locator-border-color);
    border-radius: var(--bb-ip-locator-border-radius);
    padding: var(--bb-ip-locator-padding);
}

/* Loading indicator styling */
.bb-ip-locator-loading {
    color: var(--bb-ip-locator-loading-color);
}

/* Error message styling */
.bb-ip-locator-error {
    color: var(--bb-ip-locator-error-color);
}

/* Map container styling */
.bb-ip-locator-map {
    margin-top: 1rem;
    border: 1px solid var(--bb-ip-locator-border-color);
    border-radius: var(--bb-ip-locator-border-radius);
    overflow: hidden;
}
```

## JavaScript Interop

The IPLocator component uses JavaScript interop for certain features:

```javascript
// Get client IP address
window.getClientIPAddress = async function() {
    try {
        const response = await fetch('https://api.ipify.org?format=json');
        const data = await response.json();
        return data.ip;
    } catch (error) {
        console.error('Error getting client IP:', error);
        return null;
    }
};

// Initialize map for IP location
window.initIPLocationMap = function(elementId, lat, lng, title, zoom) {
    if (!window.L) {
        console.error('Leaflet.js is not loaded');
        return;
    }
    
    const mapElement = document.getElementById(elementId);
    if (!mapElement) return;
    
    const map = L.map(elementId).setView([lat, lng], zoom || 10);
    
    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '&copy; OpenStreetMap contributors'
    }).addTo(map);
    
    const marker = L.marker([lat, lng]).addTo(map);
    if (title) marker.bindPopup(title).openPopup();
    
    return map;
};
```

## Accessibility

The IPLocator component follows accessibility best practices:

- Provides proper ARIA attributes for loading states and error messages
- Ensures map displays have appropriate alt text and descriptions
- Maintains keyboard navigation for interactive elements
- Provides visible focus indicators for keyboard users

## Browser Compatibility

The IPLocator component is compatible with all modern browsers. For older browsers, some advanced features like map displays may have limited support, but the core IP location functionality will work in any browser that supports Blazor.

## Integration with Other Components

The IPLocator component can be combined with other BootstrapBlazor components:

- Use with `Table` for displaying location data in tabular format
- Combine with `Card` for styled location information display
- Use with `Modal` or `Drawer` for on-demand location lookups
- Integrate with `Chart` for visualizing visitor location statistics