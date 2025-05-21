# Geolocation Component

## Overview
The Geolocation component in BootstrapBlazor provides a convenient wrapper around the browser's Geolocation API, allowing developers to easily access and monitor a user's geographical location within Blazor applications. This component simplifies the process of requesting location permissions, retrieving coordinates, handling errors, and tracking location changes, making it ideal for location-aware web applications.

## Features
- **Current Location Access**: Retrieve the user's current geographical coordinates
- **Location Tracking**: Monitor changes in the user's location over time
- **Permission Handling**: Manage geolocation permission requests and states
- **Error Handling**: Gracefully handle geolocation errors and permission denials
- **Configurable Accuracy**: Adjust location accuracy based on application needs
- **Position Caching**: Optional caching of location data to reduce API calls
- **Distance Calculation**: Calculate distances between geographical coordinates
- **Address Lookup**: Optional reverse geocoding to convert coordinates to addresses
- **Map Integration**: Easy integration with mapping components and services

## Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `EnableHighAccuracy` | bool | false | Indicates whether to use high accuracy for location data |
| `Timeout` | int | 10000 | Maximum time (in milliseconds) to wait for a location response |
| `MaximumAge` | int | 0 | Maximum age (in milliseconds) of cached location data that can be used |
| `WatchPosition` | bool | false | Whether to continuously monitor location changes |
| `AutoLocate` | bool | false | Whether to automatically request location on component initialization |
| `ShowLocationButton` | bool | true | Whether to show a button to trigger location requests |
| `LocationButtonText` | string | "Get Location" | Text for the location request button |
| `LocationButtonIcon` | string | "fa-solid fa-location-dot" | Icon for the location request button |
| `LocationButtonColor` | Color | Color.Primary | Color of the location request button |
| `ShowCoordinates` | bool | false | Whether to display the coordinates on the component |
| `CoordinatesFormat` | string | "Lat: {0}, Lng: {1}" | Format string for displaying coordinates |
| `ShowAccuracy` | bool | false | Whether to display accuracy information |
| `AccuracyFormat` | string | "Accuracy: {0} meters" | Format string for displaying accuracy |
| `ShowTimestamp` | bool | false | Whether to display the timestamp of the location data |
| `TimestampFormat` | string | "yyyy-MM-dd HH:mm:ss" | Format string for displaying timestamp |
| `ShowError` | bool | true | Whether to display error messages |
| `ErrorDisplayMode` | DisplayMode | DisplayMode.Alert | How to display error messages |
| `ChildContent` | RenderFragment | null | Content to be displayed inside the component |

## Events

| Event | Description |
|-------|-------------|
| `OnLocationChanged` | Triggered when the user's location changes |
| `OnLocationError` | Triggered when an error occurs during location retrieval |
| `OnPermissionChanged` | Triggered when the geolocation permission state changes |
| `OnLocationRequest` | Triggered when a location request is initiated |
| `OnLocationReceived` | Triggered when location data is successfully received |

## Usage Examples

### Example 1: Basic Location Retrieval
```razor
@using BootstrapBlazor.Components

<Geolocation OnLocationChanged="HandleLocationChanged" />

@code {
    private double latitude;
    private double longitude;
    
    private void HandleLocationChanged(GeolocationPosition position)
    {
        latitude = position.Coords.Latitude;
        longitude = position.Coords.Longitude;
        StateHasChanged();
    }
}

<div class="mt-3">
    @if (latitude != 0 && longitude != 0)
    {
        <p>Your current location:</p>
        <p>Latitude: @latitude</p>
        <p>Longitude: @longitude</p>
    }
</div>
```

### Example 2: Location Tracking with High Accuracy
```razor
<Geolocation EnableHighAccuracy="true"
             WatchPosition="true"
             ShowCoordinates="true"
             ShowAccuracy="true"
             OnLocationChanged="HandleLocationChanged" />

@code {
    private List<GeolocationPosition> locationHistory = new List<GeolocationPosition>();
    
    private void HandleLocationChanged(GeolocationPosition position)
    {
        locationHistory.Add(position);
        StateHasChanged();
    }
}

<div class="mt-3">
    <h5>Location History</h5>
    @if (locationHistory.Any())
    {
        <table class="table table-sm">
            <thead>
                <tr>
                    <th>Time</th>
                    <th>Latitude</th>
                    <th>Longitude</th>
                    <th>Accuracy</th>
                </tr>
            </thead>
            <tbody>
                @foreach (var location in locationHistory)
                {
                    <tr>
                        <td>@location.Timestamp.ToLocalTime().ToString("HH:mm:ss")</td>
                        <td>@location.Coords.Latitude.ToString("F6")</td>
                        <td>@location.Coords.Longitude.ToString("F6")</td>
                        <td>@location.Coords.Accuracy.ToString("F1") m</td>
                    </tr>
                }
            </tbody>
        </table>
    }
    else
    {
        <p>No location data recorded yet.</p>
    }
</div>
```

### Example 3: Custom Location Button and Error Handling
```razor
<Geolocation @ref="geolocation"
             ShowLocationButton="false"
             OnLocationChanged="HandleLocationChanged"
             OnLocationError="HandleLocationError" />

<div class="d-flex gap-2 mb-3">
    <Button Text="Get My Location"
            Icon="fa-solid fa-map-marker-alt"
            Color="Color.Success"
            OnClick="RequestLocation" />
            
    <Button Text="Clear"
            Icon="fa-solid fa-eraser"
            Color="Color.Secondary"
            OnClick="ClearLocation" />
</div>

@if (!string.IsNullOrEmpty(errorMessage))
{
    <Alert Color="Color.Danger" ShowDismiss="true" OnDismiss="() => errorMessage = null">
        <p>@errorMessage</p>
    </Alert>
}

@if (currentLocation != null)
{
    <div class="card">
        <div class="card-body">
            <h5 class="card-title">Your Location</h5>
            <p>Latitude: @currentLocation.Coords.Latitude.ToString("F6")</p>
            <p>Longitude: @currentLocation.Coords.Longitude.ToString("F6")</p>
            <p>Accuracy: @currentLocation.Coords.Accuracy.ToString("F1") meters</p>
            <p>Timestamp: @currentLocation.Timestamp.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss")</p>
        </div>
    </div>
}

@code {
    private Geolocation geolocation;
    private GeolocationPosition currentLocation;
    private string errorMessage;
    
    private async Task RequestLocation()
    {
        errorMessage = null;
        await geolocation.GetCurrentPositionAsync();
    }
    
    private void ClearLocation()
    {
        currentLocation = null;
        errorMessage = null;
    }
    
    private void HandleLocationChanged(GeolocationPosition position)
    {
        currentLocation = position;
        StateHasChanged();
    }
    
    private void HandleLocationError(GeolocationPositionError error)
    {
        switch (error.Code)
        {
            case 1:
                errorMessage = "Permission denied. Please allow location access.";
                break;
            case 2:
                errorMessage = "Location unavailable. Please try again.";
                break;
            case 3:
                errorMessage = "Location request timed out. Please try again.";
                break;
            default:
                errorMessage = $"Error getting location: {error.Message}";
                break;
        }
        StateHasChanged();
    }
}
```

### Example 4: Distance Calculation Between Points
```razor
<Geolocation @ref="geolocation"
             LocationButtonText="Get Current Location"
             LocationButtonIcon="fa-solid fa-crosshairs"
             OnLocationChanged="HandleLocationChanged" />

<div class="row mt-3">
    <div class="col-md-6">
        <div class="card">
            <div class="card-header">Current Location</div>
            <div class="card-body">
                @if (currentLocation != null)
                {
                    <p>Latitude: @currentLocation.Coords.Latitude.ToString("F6")</p>
                    <p>Longitude: @currentLocation.Coords.Longitude.ToString("F6")</p>
                }
                else
                {
                    <p>Click the button to get your location.</p>
                }
            </div>
        </div>
    </div>
    
    <div class="col-md-6">
        <div class="card">
            <div class="card-header">Destination</div>
            <div class="card-body">
                <div class="mb-3">
                    <label for="destLat" class="form-label">Latitude</label>
                    <input type="number" class="form-control" id="destLat" @bind="destinationLat" step="0.000001" />
                </div>
                <div class="mb-3">
                    <label for="destLng" class="form-label">Longitude</label>
                    <input type="number" class="form-control" id="destLng" @bind="destinationLng" step="0.000001" />
                </div>
                <Button Text="Calculate Distance" OnClick="CalculateDistance" />
            </div>
        </div>
    </div>
</div>

@if (distance.HasValue)
{
    <div class="alert alert-info mt-3">
        <p>Distance to destination: @(distance.Value.ToString("F2")) kilometers</p>
    </div>
}

@code {
    private Geolocation geolocation;
    private GeolocationPosition currentLocation;
    private double destinationLat;
    private double destinationLng;
    private double? distance;
    
    private void HandleLocationChanged(GeolocationPosition position)
    {
        currentLocation = position;
        StateHasChanged();
    }
    
    private void CalculateDistance()
    {
        if (currentLocation != null)
        {
            distance = CalculateHaversineDistance(
                currentLocation.Coords.Latitude, currentLocation.Coords.Longitude,
                destinationLat, destinationLng);
        }
    }
    
    // Haversine formula to calculate distance between two points on Earth
    private double CalculateHaversineDistance(double lat1, double lon1, double lat2, double lon2)
    {
        const double earthRadius = 6371; // in kilometers
        
        var dLat = ToRadians(lat2 - lat1);
        var dLon = ToRadians(lon2 - lon1);
        
        var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
                
        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        return earthRadius * c;
    }
    
    private double ToRadians(double degrees)
    {
        return degrees * Math.PI / 180;
    }
}
```

### Example 5: Reverse Geocoding with External API
```razor
@inject HttpClient Http

<Geolocation OnLocationChanged="HandleLocationChanged"
             LocationButtonText="Find My Address"
             LocationButtonIcon="fa-solid fa-map-pin" />

@if (isLoading)
{
    <div class="d-flex justify-content-center mt-3">
        <Spinner />
    </div>
}

@if (!string.IsNullOrEmpty(address))
{
    <div class="alert alert-success mt-3">
        <h5>Your Address</h5>
        <p>@address</p>
    </div>
}

@if (!string.IsNullOrEmpty(errorMessage))
{
    <div class="alert alert-danger mt-3">
        <p>@errorMessage</p>
    </div>
}

@code {
    private bool isLoading;
    private string address;
    private string errorMessage;
    
    private async void HandleLocationChanged(GeolocationPosition position)
    {
        try
        {            
            isLoading = true;
            errorMessage = null;
            StateHasChanged();
            
            // Note: You would need to replace this with your actual geocoding API
            // This is just an example using OpenStreetMap's Nominatim API
            var lat = position.Coords.Latitude;
            var lng = position.Coords.Longitude;
            var url = $"https://nominatim.openstreetmap.org/reverse?format=json&lat={lat}&lon={lng}&zoom=18&addressdetails=1";
            
            var response = await Http.GetFromJsonAsync<GeocodingResponse>(url);
            address = response.DisplayName;
        }
        catch (Exception ex)
        {
            errorMessage = $"Error getting address: {ex.Message}";
            address = null;
        }
        finally
        {
            isLoading = false;
            StateHasChanged();
        }
    }
    
    private class GeocodingResponse
    {
        [JsonPropertyName("display_name")]
        public string DisplayName { get; set; }
    }
}
```

### Example 6: Geolocation with Map Integration
```razor
@inject IJSRuntime JSRuntime

<div class="row">
    <div class="col-md-4">
        <Geolocation @ref="geolocation"
                     EnableHighAccuracy="true"
                     OnLocationChanged="HandleLocationChanged" />
                     
        @if (currentLocation != null)
        {
            <div class="mt-3">
                <p><strong>Latitude:</strong> @currentLocation.Coords.Latitude.ToString("F6")</p>
                <p><strong>Longitude:</strong> @currentLocation.Coords.Longitude.ToString("F6")</p>
                <p><strong>Accuracy:</strong> @currentLocation.Coords.Accuracy.ToString("F1") meters</p>
            </div>
            
            <div class="mt-3">
                <Button Text="Center Map" OnClick="CenterMap" />
            </div>
        }
    </div>
    
    <div class="col-md-8">
        <div id="map" style="height: 400px; width: 100%;"></div>
    </div>
</div>

@code {
    private Geolocation geolocation;
    private GeolocationPosition currentLocation;
    private bool mapInitialized;
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await InitializeMapAsync();
            mapInitialized = true;
        }
    }
    
    private async Task InitializeMapAsync()
    {
        // This is a simplified example. In a real application, you would use a proper map library
        // like Leaflet.js, Google Maps, or OpenLayers
        await JSRuntime.InvokeVoidAsync("initMap");
    }
    
    private async void HandleLocationChanged(GeolocationPosition position)
    {
        currentLocation = position;
        StateHasChanged();
        
        if (mapInitialized)
        {
            await UpdateMapMarkerAsync(position.Coords.Latitude, position.Coords.Longitude);
        }
    }
    
    private async Task CenterMap()
    {
        if (currentLocation != null && mapInitialized)
        {
            await JSRuntime.InvokeVoidAsync("centerMap", 
                currentLocation.Coords.Latitude, 
                currentLocation.Coords.Longitude);
        }
    }
    
    private async Task UpdateMapMarkerAsync(double lat, double lng)
    {
        await JSRuntime.InvokeVoidAsync("updateMapMarker", lat, lng);
    }
}

@* Add this script section to your _Host.cshtml or index.html *@
@* 
<script>
    // Simple map implementation using Leaflet.js
    let map;
    let marker;
    
    window.initMap = function() {
        // Load Leaflet CSS and JS if not already loaded
        if (!document.getElementById('leaflet-css')) {
            const link = document.createElement('link');
            link.id = 'leaflet-css';
            link.rel = 'stylesheet';
            link.href = 'https://unpkg.com/leaflet@1.7.1/dist/leaflet.css';
            document.head.appendChild(link);
            
            const script = document.createElement('script');
            script.src = 'https://unpkg.com/leaflet@1.7.1/dist/leaflet.js';
            document.head.appendChild(script);
            
            script.onload = initializeLeafletMap;
        } else {
            initializeLeafletMap();
        }
    };
    
    function initializeLeafletMap() {
        // Initialize the map
        map = L.map('map').setView([0, 0], 2);
        
        L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
            attribution: '&copy; OpenStreetMap contributors'
        }).addTo(map);
        
        marker = L.marker([0, 0]).addTo(map);
    }
    
    window.updateMapMarker = function(lat, lng) {
        if (map && marker) {
            marker.setLatLng([lat, lng]);
        }
    };
    
    window.centerMap = function(lat, lng) {
        if (map) {
            map.setView([lat, lng], 15);
        }
    };
</script>
*@
```

### Example 7: Geofencing with Geolocation
```razor
<Geolocation WatchPosition="true"
             EnableHighAccuracy="true"
             OnLocationChanged="HandleLocationChanged" />

<div class="row mt-3">
    <div class="col-md-6">
        <div class="card">
            <div class="card-header">Geofence Configuration</div>
            <div class="card-body">
                <div class="mb-3">
                    <label for="centerLat" class="form-label">Center Latitude</label>
                    <input type="number" class="form-control" id="centerLat" @bind="centerLat" step="0.000001" />
                </div>
                <div class="mb-3">
                    <label for="centerLng" class="form-label">Center Longitude</label>
                    <input type="number" class="form-control" id="centerLng" @bind="centerLng" step="0.000001" />
                </div>
                <div class="mb-3">
                    <label for="radius" class="form-label">Radius (meters)</label>
                    <input type="number" class="form-control" id="radius" @bind="radiusMeters" min="10" />
                </div>
                <Button Text="Set Geofence" OnClick="SetGeofence" />
            </div>
        </div>
    </div>
    
    <div class="col-md-6">
        <div class="card">
            <div class="card-header">Status</div>
            <div class="card-body">
                @if (currentLocation != null)
                {
                    <p><strong>Current Location:</strong></p>
                    <p>Latitude: @currentLocation.Coords.Latitude.ToString("F6")</p>
                    <p>Longitude: @currentLocation.Coords.Longitude.ToString("F6")</p>
                    
                    @if (geofenceActive)
                    {
                        <div class="alert @(isInsideGeofence ? "alert-success" : "alert-warning") mt-3">
                            <p><strong>@(isInsideGeofence ? "Inside" : "Outside") Geofence</strong></p>
                            <p>Distance to center: @distanceToCenter.ToString("F1") meters</p>
                        </div>
                    }
                }
                else
                {
                    <p>Waiting for location data...</p>
                }
            </div>
        </div>
    </div>
</div>

@code {
    private GeolocationPosition currentLocation;
    private double centerLat;
    private double centerLng;
    private double radiusMeters = 100;
    private bool geofenceActive;
    private bool isInsideGeofence;
    private double distanceToCenter;
    
    private void SetGeofence()
    {
        geofenceActive = true;
        CheckGeofence();
    }
    
    private void HandleLocationChanged(GeolocationPosition position)
    {
        currentLocation = position;
        
        if (geofenceActive)
        {
            CheckGeofence();
        }
        
        StateHasChanged();
    }
    
    private void CheckGeofence()
    {
        if (currentLocation != null && geofenceActive)
        {
            // Calculate distance between current location and geofence center
            distanceToCenter = CalculateDistance(
                currentLocation.Coords.Latitude, currentLocation.Coords.Longitude,
                centerLat, centerLng);
                
            // Check if inside geofence
            isInsideGeofence = distanceToCenter <= radiusMeters;
        }
    }
    
    // Calculate distance between two points in meters
    private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
    {
        const double earthRadius = 6371000; // in meters
        
        var dLat = ToRadians(lat2 - lat1);
        var dLon = ToRadians(lon2 - lon1);
        
        var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
                
        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        return earthRadius * c;
    }
    
    private double ToRadians(double degrees)
    {
        return degrees * Math.PI / 180;
    }
}
```

## CSS Customization

The Geolocation component can be customized using CSS variables and classes:

```css
/* Custom geolocation button styling */
.geolocation-button {
    --bb-geolocation-btn-bg: #28a745;
    --bb-geolocation-btn-color: #ffffff;
    --bb-geolocation-btn-hover-bg: #218838;
    --bb-geolocation-btn-hover-color: #ffffff;
    --bb-geolocation-btn-border-radius: 4px;
}

/* Custom styling for coordinates display */
.geolocation-coordinates {
    font-family: monospace;
    background-color: #f8f9fa;
    padding: 0.5rem;
    border-radius: 4px;
    margin-top: 0.5rem;
}

/* Custom styling for error messages */
.geolocation-error {
    color: #dc3545;
    margin-top: 0.5rem;
}
```

## JavaScript Interop

The Geolocation component uses JavaScript interop to interact with the browser's Geolocation API. It provides methods that can be called from C# code:

- `GetCurrentPositionAsync()`: Gets the current position
- `StartWatchingPositionAsync()`: Starts continuous location monitoring
- `StopWatchingPositionAsync()`: Stops continuous location monitoring
- `CheckPermissionStateAsync()`: Checks the current geolocation permission state

## Accessibility

The Geolocation component follows accessibility best practices:

- Provides proper ARIA attributes for location controls
- Ensures error messages are properly announced by screen readers
- Maintains keyboard navigation for location request buttons
- Provides visible focus indicators for keyboard users

## Browser Compatibility

The Geolocation component works in all modern browsers that support the Geolocation API. For browsers without geolocation support, the component gracefully degrades and displays an appropriate error message.

## Integration with Other Components

The Geolocation component can be combined with other BootstrapBlazor components:

- Use with `Map` components for location visualization
- Combine with `Form` for location-based input
- Use with `Alert` or `Toast` for location notifications
- Integrate with `Table` for location data display