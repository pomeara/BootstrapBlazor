# Transition Component

## Overview
The Transition component in BootstrapBlazor provides a way to add smooth animations and transitions when elements enter, leave, or change state in the UI. It enhances the user experience by making interface changes more fluid and visually appealing. The Transition component is particularly useful for creating dynamic interfaces where elements appear, disappear, or transform based on user interactions or application state changes.

## Features
- **Multiple Animation Types**: Supports various transition effects like fade, slide, zoom, etc.
- **Customizable Duration**: Adjustable timing for transitions
- **Delay Support**: Option to delay the start of transitions
- **Conditional Rendering**: Show or hide elements with animated transitions
- **CSS Animation Integration**: Works with CSS animations and transitions
- **Group Transitions**: Apply transitions to groups of elements
- **Custom Easing Functions**: Control the acceleration curve of animations
- **Enter/Leave Hooks**: Events for different stages of the transition
- **Reusable Transition Presets**: Common transition patterns available as presets

## Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `Type` | TransitionType | TransitionType.Fade | The type of transition animation to apply |
| `Duration` | int | 300 | Duration of the transition in milliseconds |
| `Delay` | int | 0 | Delay before starting the transition in milliseconds |
| `IsVisible` | bool | false | Controls whether the content is visible (with transition) |
| `Easing` | string | "ease" | CSS easing function for the transition |
| `EnterClass` | string | "" | CSS class applied at the start of enter transition |
| `EnterActiveClass` | string | "" | CSS class applied during enter transition |
| `EnterToClass` | string | "" | CSS class applied at the end of enter transition |
| `LeaveClass` | string | "" | CSS class applied at the start of leave transition |
| `LeaveActiveClass` | string | "" | CSS class applied during leave transition |
| `LeaveToClass` | string | "" | CSS class applied at the end of leave transition |
| `AppearClass` | string | "" | CSS class applied at the start of initial appear transition |
| `AppearActiveClass` | string | "" | CSS class applied during initial appear transition |
| `AppearToClass` | string | "" | CSS class applied at the end of initial appear transition |
| `Mode` | TransitionMode | TransitionMode.Default | Controls how elements are inserted/removed during transition |
| `DisableOnInitial` | bool | false | When true, disables transition on initial render |

## Events

| Event | Description |
|-------|-------------|
| `OnBeforeEnter` | Triggered right before the enter transition starts |
| `OnEnter` | Triggered at the start of the enter transition |
| `OnAfterEnter` | Triggered when the enter transition completes |
| `OnEnterCancelled` | Triggered when the enter transition is cancelled |
| `OnBeforeLeave` | Triggered right before the leave transition starts |
| `OnLeave` | Triggered at the start of the leave transition |
| `OnAfterLeave` | Triggered when the leave transition completes |
| `OnLeaveCancelled` | Triggered when the leave transition is cancelled |

## Usage Examples

### Example 1: Basic Fade Transition

```html
<div class="mb-3">
    <Button Text="Toggle Content" OnClick="() => isVisible = !isVisible" />
</div>

<Transition Type="TransitionType.Fade" 
           Duration="300"
           IsVisible="@isVisible">
    <div class="p-3 bg-light border">
        <h4>Fade Transition Example</h4>
        <p>This content will fade in and out when the button is clicked.</p>
    </div>
</Transition>
```

```csharp
@code {
    private bool isVisible = false;
}
```

### Example 2: Different Transition Types

```html
<div class="mb-3">
    <Select TValue="TransitionType" @bind-Value="selectedTransition" Items="transitionTypes" />
    <Button Text="Toggle Transition" OnClick="() => isVisible = !isVisible" Class="ms-2" />
</div>

<Transition Type="@selectedTransition"
           Duration="500"
           IsVisible="@isVisible">
    <div class="p-3 bg-info text-white border">
        <h4>@selectedTransition Transition</h4>
        <p>This content demonstrates the @selectedTransition transition type.</p>
    </div>
</Transition>
```

```csharp
@code {
    private bool isVisible = false;
    private TransitionType selectedTransition = TransitionType.Fade;
    
    private List<SelectedItem> transitionTypes = new List<SelectedItem>
    {
        new SelectedItem { Text = "Fade", Value = TransitionType.Fade },
        new SelectedItem { Text = "Zoom", Value = TransitionType.Zoom },
        new SelectedItem { Text = "Slide Down", Value = TransitionType.SlideDown },
        new SelectedItem { Text = "Slide Up", Value = TransitionType.SlideUp },
        new SelectedItem { Text = "Slide Left", Value = TransitionType.SlideLeft },
        new SelectedItem { Text = "Slide Right", Value = TransitionType.SlideRight },
        new SelectedItem { Text = "Collapse", Value = TransitionType.Collapse }
    };
}
```

### Example 3: Transition with Custom Duration and Delay

```html
<div class="mb-3">
    <div class="row">
        <div class="col-md-4">
            <label>Duration (ms):</label>
            <InputNumber @bind-Value="duration" Min="100" Max="2000" Step="100" />
        </div>
        <div class="col-md-4">
            <label>Delay (ms):</label>
            <InputNumber @bind-Value="delay" Min="0" Max="1000" Step="100" />
        </div>
        <div class="col-md-4 d-flex align-items-end">
            <Button Text="Toggle Content" OnClick="() => isVisible = !isVisible" />
        </div>
    </div>
</div>

<Transition Type="TransitionType.Fade"
           Duration="@duration"
           Delay="@delay"
           IsVisible="@isVisible"
           OnBeforeEnter="HandleBeforeEnter"
           OnAfterEnter="HandleAfterEnter"
           OnBeforeLeave="HandleBeforeLeave"
           OnAfterLeave="HandleAfterLeave">
    <div class="p-3 bg-success text-white border">
        <h4>Custom Timing Transition</h4>
        <p>Duration: @duration ms, Delay: @delay ms</p>
    </div>
</Transition>

<div class="mt-3">
    <h5>Transition Events:</h5>
    <ul class="list-group">
        @foreach (var log in eventLogs.TakeLast(5))
        {
            <li class="list-group-item">@log</li>
        }
    </ul>
</div>
```

```csharp
@code {
    private bool isVisible = false;
    private int duration = 500;
    private int delay = 0;
    private List<string> eventLogs = new List<string>();
    
    private void HandleBeforeEnter()
    {
        LogEvent("Before Enter");
    }
    
    private void HandleAfterEnter()
    {
        LogEvent("After Enter");
    }
    
    private void HandleBeforeLeave()
    {
        LogEvent("Before Leave");
    }
    
    private void HandleAfterLeave()
    {
        LogEvent("After Leave");
    }
    
    private void LogEvent(string eventName)
    {
        eventLogs.Add($"{DateTime.Now:HH:mm:ss.fff} - {eventName}");
        StateHasChanged();
    }
}
```

### Example 4: Group Transitions

```html
<div class="mb-3">
    <Button Text="Add Item" OnClick="AddItem" />
    <Button Text="Remove Item" OnClick="RemoveItem" Color="Color.Danger" Class="ms-2" />
    <Button Text="Shuffle Items" OnClick="ShuffleItems" Color="Color.Warning" Class="ms-2" />
</div>

<TransitionGroup Type="TransitionType.Fade" Duration="300">
    @foreach (var item in items)
    {
        <TransitionItem Key="@item.Id">
            <div class="p-2 mb-2 bg-light border d-flex justify-content-between align-items-center">
                <span>Item @item.Id: @item.Name</span>
                <Button Text="×" Size="Size.Small" Color="Color.Danger" 
                        OnClick="() => RemoveSpecificItem(item.Id)" />
            </div>
        </TransitionItem>
    }
</TransitionGroup>
```

```csharp
@code {
    private List<ItemData> items = new List<ItemData>();
    private int nextId = 1;
    
    protected override void OnInitialized()
    {
        // Initialize with a few items
        for (int i = 0; i < 3; i++)
        {
            AddItem();
        }
    }
    
    private void AddItem()
    {
        items.Add(new ItemData
        {
            Id = nextId,
            Name = $"Item {nextId}"
        });
        nextId++;
    }
    
    private void RemoveItem()
    {
        if (items.Any())
        {
            items.RemoveAt(items.Count - 1);
        }
    }
    
    private void RemoveSpecificItem(int id)
    {
        items.RemoveAll(i => i.Id == id);
    }
    
    private void ShuffleItems()
    {
        var rng = new Random();
        items = items.OrderBy(x => rng.Next()).ToList();
    }
    
    private class ItemData
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
```

### Example 5: Custom CSS Transitions

```html
<div class="mb-3">
    <Button Text="Toggle Custom Transition" OnClick="() => isVisible = !isVisible" />
</div>

<Transition EnterClass="custom-enter"
           EnterActiveClass="custom-enter-active"
           EnterToClass="custom-enter-to"
           LeaveClass="custom-leave"
           LeaveActiveClass="custom-leave-active"
           LeaveToClass="custom-leave-to"
           Duration="800"
           IsVisible="@isVisible">
    <div class="p-4 bg-primary text-white border rounded">
        <h4>Custom CSS Transition</h4>
        <p>This example uses custom CSS classes for the transition.</p>
    </div>
</Transition>

<style>
    .custom-enter {
        opacity: 0;
        transform: scale(0.8) rotate(-10deg);
    }
    
    .custom-enter-active {
        transition: opacity 800ms ease, transform 800ms cubic-bezier(0.18, 0.89, 0.32, 1.28);
    }
    
    .custom-enter-to {
        opacity: 1;
        transform: scale(1) rotate(0);
    }
    
    .custom-leave {
        opacity: 1;
        transform: scale(1) rotate(0);
    }
    
    .custom-leave-active {
        transition: opacity 800ms ease, transform 800ms cubic-bezier(0.6, -0.28, 0.74, 0.05);
    }
    
    .custom-leave-to {
        opacity: 0;
        transform: scale(0.8) rotate(10deg);
    }
</style>
```

```csharp
@code {
    private bool isVisible = false;
}
```

### Example 6: Conditional Content with Transitions

```html
<div class="mb-3">
    <div class="btn-group">
        <Button Text="Home" OnClick="() => currentTab = 'home'" 
                Color="@(currentTab == "home" ? Color.Primary : Color.Secondary)" />
        <Button Text="Profile" OnClick="() => currentTab = 'profile'" 
                Color="@(currentTab == "profile" ? Color.Primary : Color.Secondary)" />
        <Button Text="Settings" OnClick="() => currentTab = 'settings'" 
                Color="@(currentTab == "settings" ? Color.Primary : Color.Secondary)" />
    </div>
</div>

<div class="position-relative" style="height: 200px;">
    <Transition Type="TransitionType.Fade" 
               Duration="300"
               IsVisible="@(currentTab == "home")"
               Mode="TransitionMode.OutIn">
        <div class="position-absolute w-100 p-3 bg-light border">
            <h4>Home</h4>
            <p>Welcome to the home page!</p>
        </div>
    </Transition>
    
    <Transition Type="TransitionType.Fade" 
               Duration="300"
               IsVisible="@(currentTab == "profile")"
               Mode="TransitionMode.OutIn">
        <div class="position-absolute w-100 p-3 bg-light border">
            <h4>Profile</h4>
            <p>This is your user profile page.</p>
        </div>
    </Transition>
    
    <Transition Type="TransitionType.Fade" 
               Duration="300"
               IsVisible="@(currentTab == "settings")"
               Mode="TransitionMode.OutIn">
        <div class="position-absolute w-100 p-3 bg-light border">
            <h4>Settings</h4>
            <p>Manage your application settings here.</p>
        </div>
    </Transition>
</div>
```

```csharp
@code {
    private string currentTab = "home";
}
```

### Example 7: Animated Notification System

```html
<div class="mb-3">
    <Button Text="Add Notification" OnClick="AddNotification" />
    <Button Text="Clear All" OnClick="ClearNotifications" Color="Color.Danger" Class="ms-2" />
</div>

<div class="notification-container" style="position: fixed; top: 20px; right: 20px; width: 300px; z-index: 1000;">
    <TransitionGroup Type="TransitionType.SlideLeft" Duration="400">
        @foreach (var notification in notifications)
        {
            <TransitionItem Key="@notification.Id">
                <div class="notification mb-2 p-3 border rounded shadow-sm @GetNotificationClass(notification.Type)">
                    <div class="d-flex justify-content-between align-items-center">
                        <h5 class="mb-1">@notification.Title</h5>
                        <Button Text="×" Size="Size.Small" OnClick="() => RemoveNotification(notification.Id)" />
                    </div>
                    <p class="mb-0">@notification.Message</p>
                </div>
            </TransitionItem>
        }
    </TransitionGroup>
</div>
```

```csharp
@code {
    private List<NotificationItem> notifications = new List<NotificationItem>();
    private int nextId = 1;
    private Random random = new Random();
    
    private void AddNotification()
    {
        var types = new[] { "info", "success", "warning", "danger" };
        var type = types[random.Next(types.Length)];
        
        var notification = new NotificationItem
        {
            Id = nextId++,
            Type = type,
            Title = $"{char.ToUpper(type[0])}{type.Substring(1)} Notification",
            Message = $"This is a sample {type} notification message. #{nextId - 1}",
            Timestamp = DateTime.Now
        };
        
        notifications.Add(notification);
        
        // Auto-remove after 5 seconds
        _ = Task.Delay(5000).ContinueWith(_ => {
            InvokeAsync(() => {
                RemoveNotification(notification.Id);
                StateHasChanged();
            });
        });
    }
    
    private void RemoveNotification(int id)
    {
        notifications.RemoveAll(n => n.Id == id);
    }
    
    private void ClearNotifications()
    {
        notifications.Clear();
    }
    
    private string GetNotificationClass(string type)
    {
        return type switch
        {
            "info" => "bg-info text-white",
            "success" => "bg-success text-white",
            "warning" => "bg-warning",
            "danger" => "bg-danger text-white",
            _ => "bg-light"
        };
    }
    
    private class NotificationItem
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
```

## CSS Customization

The Transition component can be customized using CSS variables and classes:

```css
/* Custom styles for Transition component */
.bb-transition {
    /* Component container */
}

/* Fade transition */
.bb-transition-fade-enter {
    opacity: 0;
}

.bb-transition-fade-enter-active {
    transition: opacity var(--bb-transition-duration, 300ms) ease;
}

.bb-transition-fade-enter-to {
    opacity: 1;
}

.bb-transition-fade-leave {
    opacity: 1;
}

.bb-transition-fade-leave-active {
    transition: opacity var(--bb-transition-duration, 300ms) ease;
}

.bb-transition-fade-leave-to {
    opacity: 0;
}

/* Zoom transition */
.bb-transition-zoom-enter {
    opacity: 0;
    transform: scale(0.8);
}

.bb-transition-zoom-enter-active {
    transition: opacity var(--bb-transition-duration, 300ms) ease,
                transform var(--bb-transition-duration, 300ms) ease;
}

.bb-transition-zoom-enter-to {
    opacity: 1;
    transform: scale(1);
}

.bb-transition-zoom-leave {
    opacity: 1;
    transform: scale(1);
}

.bb-transition-zoom-leave-active {
    transition: opacity var(--bb-transition-duration, 300ms) ease,
                transform var(--bb-transition-duration, 300ms) ease;
}

.bb-transition-zoom-leave-to {
    opacity: 0;
    transform: scale(0.8);
}

/* Slide transitions */
.bb-transition-slide-down-enter {
    opacity: 0;
    transform: translateY(-20px);
}

.bb-transition-slide-down-enter-active {
    transition: opacity var(--bb-transition-duration, 300ms) ease,
                transform var(--bb-transition-duration, 300ms) ease;
}

.bb-transition-slide-down-enter-to {
    opacity: 1;
    transform: translateY(0);
}

.bb-transition-slide-down-leave {
    opacity: 1;
    transform: translateY(0);
}

.bb-transition-slide-down-leave-active {
    transition: opacity var(--bb-transition-duration, 300ms) ease,
                transform var(--bb-transition-duration, 300ms) ease;
}

.bb-transition-slide-down-leave-to {
    opacity: 0;
    transform: translateY(20px);
}

/* Customize transition timing */
:root {
    --bb-transition-duration: 300ms;
    --bb-transition-timing-function: ease;
    --bb-transition-delay: 0ms;
}
```

## JavaScript Interop

The Transition component uses JavaScript interop for managing transitions. You can extend its functionality by using the following methods:

```csharp
// Trigger a transition programmatically
await JSRuntime.InvokeVoidAsync("bootstrapBlazor.transition.enter", elementRef);

// Cancel an ongoing transition
await JSRuntime.InvokeVoidAsync("bootstrapBlazor.transition.cancel", elementRef);

// Check if an element is currently transitioning
var isTransitioning = await JSRuntime.InvokeAsync<bool>("bootstrapBlazor.transition.isTransitioning", elementRef);

// Set custom transition properties
await JSRuntime.InvokeVoidAsync("bootstrapBlazor.transition.setProperties", elementRef, new {
    duration = 500,
    easing = "cubic-bezier(0.68, -0.55, 0.27, 1.55)",
    delay = 100
});
```

## Accessibility

The Transition component is designed with accessibility in mind:

- Ensures content remains accessible during transitions
- Provides options to disable animations for users who prefer reduced motion
- Maintains proper focus management during transitions
- Includes ARIA attributes for screen reader compatibility
- Supports keyboard navigation during and after transitions

To support users who prefer reduced motion:

```css
@media (prefers-reduced-motion: reduce) {
    .bb-transition-fade-enter-active,
    .bb-transition-fade-leave-active,
    .bb-transition-zoom-enter-active,
    .bb-transition-zoom-leave-active,
    .bb-transition-slide-down-enter-active,
    .bb-transition-slide-down-leave-active {
        transition-duration: 0.01ms !important;
    }
}
```

## Browser Compatibility

The Transition component is compatible with all modern browsers:

- Chrome
- Firefox
- Edge
- Safari
- Opera

For older browsers with limited CSS transition support, the component includes fallback mechanisms to ensure content remains visible and functional.

## Integration with Other Components

The Transition component can be integrated with various other BootstrapBlazor components:

- Use with Modal or Dialog for animated popups
- Combine with Collapse for expandable sections
- Integrate with Tab components for tab switching animations
- Pair with Alert or Toast for animated notifications
- Use with List or Table for animated item additions/removals