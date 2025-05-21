# Tab Component

## Overview
The Tab component in BootstrapBlazor provides a way to organize and navigate between multiple sections of content within the same page. It creates a tabbed interface where only one content panel is visible at a time, allowing users to switch between different views without navigating to a new page. This component is useful for organizing related content, settings panels, and multi-step forms.

## Features
- Multiple display modes (tabs, pills, cards)
- Horizontal and vertical orientation
- Dynamic tab generation
- Closable tabs
- Custom tab templates
- Lazy loading of tab content
- Event handling for tab changes
- Programmatic tab control
- Responsive design
- Accessibility support

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `Items` | `IEnumerable<TabItem>` | `null` | Collection of tab items to display |
| `ActiveTab` | `string` | `null` | ID of the currently active tab |
| `ActiveTabIndex` | `int` | `0` | Index of the currently active tab (0-based) |
| `IsVertical` | `bool` | `false` | When true, displays tabs vertically instead of horizontally |
| `IsBorderCard` | `bool` | `false` | When true, displays tabs with a card-like border style |
| `IsCard` | `bool` | `false` | When true, displays tabs with a card style |
| `IsPills` | `bool` | `false` | When true, displays tabs with a pill style |
| `IsLazyLoad` | `bool` | `false` | When true, only loads tab content when the tab is activated |
| `ShowClose` | `bool` | `false` | When true, shows a close button on each tab |
| `ShowExtendButtons` | `bool` | `false` | When true, shows additional buttons (e.g., add new tab) |
| `TabItemTemplate` | `RenderFragment<TabItem>` | `null` | Custom template for rendering tab items |
| `BodyTemplate` | `RenderFragment<TabItem>` | `null` | Custom template for rendering tab content |
| `ChildContent` | `RenderFragment` | `null` | Content to display within the component |

## TabItem Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `Text` | `string` | `null` | Display text for the tab |
| `Icon` | `string` | `null` | Icon class for the tab |
| `IsActive` | `bool` | `false` | Whether the tab is currently active |
| `IsDisabled` | `bool` | `false` | Whether the tab is disabled |
| `Closable` | `bool` | `false` | Whether the tab can be closed |
| `ChildContent` | `RenderFragment` | `null` | Content to display in the tab panel |
| `Template` | `RenderFragment` | `null` | Custom template for this specific tab |
| `Id` | `string` | `null` | Unique identifier for the tab |

## Events

| Event | Description |
| --- | --- |
| `OnTabClick` | Triggered when a tab is clicked |
| `OnTabChanged` | Triggered when the active tab changes |
| `OnTabClose` | Triggered when a tab is closed |

## Usage Examples

### Example 1: Basic Tabs
```csharp
<Tab>
    <TabItem Text="Home" Icon="fa fa-home" IsActive="true">
        <h3>Home Content</h3>
        <p>This is the content for the Home tab. Lorem ipsum dolor sit amet, consectetur adipiscing elit.</p>
    </TabItem>
    <TabItem Text="Profile" Icon="fa fa-user">
        <h3>Profile Content</h3>
        <p>This is the content for the Profile tab. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</p>
    </TabItem>
    <TabItem Text="Messages" Icon="fa fa-envelope">
        <h3>Messages Content</h3>
        <p>This is the content for the Messages tab. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris.</p>
    </TabItem>
    <TabItem Text="Settings" Icon="fa fa-cog">
        <h3>Settings Content</h3>
        <p>This is the content for the Settings tab. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore.</p>
    </TabItem>
</Tab>
```
This example shows a basic tab component with four tabs, each with an icon and content. The "Home" tab is set as active by default.

### Example 2: Pill-Style Tabs
```csharp
<Tab IsPills="true">
    <TabItem Text="Overview" IsActive="true">
        <h3>Product Overview</h3>
        <p>This is an overview of the product. Lorem ipsum dolor sit amet, consectetur adipiscing elit.</p>
    </TabItem>
    <TabItem Text="Specifications">
        <h3>Product Specifications</h3>
        <ul>
            <li>Dimension: 10" x 5" x 2"</li>
            <li>Weight: 1.2 lbs</li>
            <li>Material: Aluminum</li>
            <li>Battery Life: 8 hours</li>
        </ul>
    </TabItem>
    <TabItem Text="Reviews">
        <h3>Customer Reviews</h3>
        <p>Average Rating: 4.5/5</p>
        <div class="review">
            <div class="review-header">
                <span class="reviewer">John D.</span>
                <span class="rating">★★★★★</span>
            </div>
            <p>Great product! Exactly what I needed.</p>
        </div>
        <div class="review">
            <div class="review-header">
                <span class="reviewer">Sarah M.</span>
                <span class="rating">★★★★☆</span>
            </div>
            <p>Works well but battery life could be better.</p>
        </div>
    </TabItem>
</Tab>
```
This example demonstrates pill-style tabs for displaying product information, specifications, and reviews.

### Example 3: Vertical Tabs
```csharp
<div class="row">
    <Tab IsVertical="true">
        <TabItem Text="Account" Icon="fa fa-user" IsActive="true">
            <h3>Account Settings</h3>
            <form>
                <div class="mb-3">
                    <label for="username" class="form-label">Username</label>
                    <input type="text" class="form-control" id="username" value="johndoe" />
                </div>
                <div class="mb-3">
                    <label for="email" class="form-label">Email</label>
                    <input type="email" class="form-control" id="email" value="john.doe@example.com" />
                </div>
                <button type="submit" class="btn btn-primary">Save Changes</button>
            </form>
        </TabItem>
        <TabItem Text="Security" Icon="fa fa-lock">
            <h3>Security Settings</h3>
            <form>
                <div class="mb-3">
                    <label for="current-password" class="form-label">Current Password</label>
                    <input type="password" class="form-control" id="current-password" />
                </div>
                <div class="mb-3">
                    <label for="new-password" class="form-label">New Password</label>
                    <input type="password" class="form-control" id="new-password" />
                </div>
                <div class="mb-3">
                    <label for="confirm-password" class="form-label">Confirm Password</label>
                    <input type="password" class="form-control" id="confirm-password" />
                </div>
                <button type="submit" class="btn btn-primary">Change Password</button>
            </form>
        </TabItem>
        <TabItem Text="Notifications" Icon="fa fa-bell">
            <h3>Notification Settings</h3>
            <div class="form-check mb-3">
                <input class="form-check-input" type="checkbox" id="email-notifications" checked />
                <label class="form-check-label" for="email-notifications">
                    Email Notifications
                </label>
            </div>
            <div class="form-check mb-3">
                <input class="form-check-input" type="checkbox" id="push-notifications" checked />
                <label class="form-check-label" for="push-notifications">
                    Push Notifications
                </label>
            </div>
            <div class="form-check mb-3">
                <input class="form-check-input" type="checkbox" id="sms-notifications" />
                <label class="form-check-label" for="sms-notifications">
                    SMS Notifications
                </label>
            </div>
            <button class="btn btn-primary">Save Preferences</button>
        </TabItem>
        <TabItem Text="Privacy" Icon="fa fa-shield-alt">
            <h3>Privacy Settings</h3>
            <div class="form-check mb-3">
                <input class="form-check-input" type="checkbox" id="profile-visibility" checked />
                <label class="form-check-label" for="profile-visibility">
                    Make profile visible to others
                </label>
            </div>
            <div class="form-check mb-3">
                <input class="form-check-input" type="checkbox" id="search-visibility" checked />
                <label class="form-check-label" for="search-visibility">
                    Allow profile to appear in search results
                </label>
            </div>
            <div class="form-check mb-3">
                <input class="form-check-input" type="checkbox" id="data-collection" checked />
                <label class="form-check-label" for="data-collection">
                    Allow data collection for personalized experience
                </label>
            </div>
            <button class="btn btn-primary">Save Privacy Settings</button>
        </TabItem>
    </Tab>
</div>
```
This example shows vertical tabs for a settings page, with tabs for account, security, notifications, and privacy settings.

### Example 4: Card-Style Tabs
```csharp
<Tab IsCard="true">
    <TabItem Text="HTML" Icon="fa fa-html5" IsActive="true">
        <div class="code-block">
            <pre><code>&lt;div class="container"&gt;
    &lt;h1&gt;Hello, World!&lt;/h1&gt;
    &lt;p&gt;This is a sample HTML code.&lt;/p&gt;
&lt;/div&gt;</code></pre>
        </div>
    </TabItem>
    <TabItem Text="CSS" Icon="fa fa-css3">
        <div class="code-block">
            <pre><code>.container {
    max-width: 1200px;
    margin: 0 auto;
    padding: 20px;
}

h1 {
    color: #333;
    font-size: 24px;
}

p {
    line-height: 1.5;
}</code></pre>
        </div>
    </TabItem>
    <TabItem Text="JavaScript" Icon="fa fa-js">
        <div class="code-block">
            <pre><code>document.addEventListener('DOMContentLoaded', function() {
    const heading = document.querySelector('h1');
    heading.addEventListener('click', function() {
        alert('Hello from JavaScript!');
    });
});</code></pre>
        </div>
    </TabItem>
</Tab>

<style>
    .code-block {
        background-color: #f8f9fa;
        border-radius: 4px;
        padding: 15px;
    }
    
    pre {
        margin: 0;
    }
    
    code {
        font-family: 'Courier New', Courier, monospace;
    }
</style>
```
This example demonstrates card-style tabs for displaying code snippets in different languages.

### Example 5: Closable Tabs with Dynamic Tab Generation
```csharp
<div class="mb-3">
    <Button Color="Color.Primary" OnClick="AddNewTab">Add New Tab</Button>
</div>

<Tab ShowClose="true" OnTabClose="CloseTab" ActiveTabIndex="@activeTabIndex" OnTabChanged="OnTabChanged">
    <Items>
        @foreach (var tab in tabs)
        {
            <TabItem Text="@tab.Title" Closable="@tab.Closable" IsActive="@(activeTabIndex == tab.Index)">
                <div class="p-3">
                    <h3>@tab.Title Content</h3>
                    <p>This is the content for @tab.Title (Tab @(tab.Index + 1)). Created at @tab.CreatedTime.ToString("HH:mm:ss").</p>
                </div>
            </TabItem>
        }
    </Items>
</Tab>

@code {
    private List<TabData> tabs = new();
    private int activeTabIndex = 0;
    private int tabCounter = 0;
    
    protected override void OnInitialized()
    {
        // Initialize with a default tab that cannot be closed
        tabs.Add(new TabData
        {
            Title = "Home",
            Index = 0,
            Closable = false,
            CreatedTime = DateTime.Now
        });
    }
    
    private void AddNewTab()
    {
        tabCounter++;
        var newTab = new TabData
        {
            Title = $"Tab {tabCounter}",
            Index = tabs.Count,
            Closable = true,
            CreatedTime = DateTime.Now
        };
        
        tabs.Add(newTab);
        activeTabIndex = newTab.Index;
    }
    
    private Task CloseTab(TabItem tab)
    {
        var index = tabs.FindIndex(t => t.Title == tab.Text);
        if (index >= 0)
        {
            tabs.RemoveAt(index);
            
            // Update indices for remaining tabs
            for (int i = 0; i < tabs.Count; i++)
            {
                tabs[i].Index = i;
            }
            
            // Set active tab to the previous one or the first one
            if (activeTabIndex >= tabs.Count)
            {
                activeTabIndex = tabs.Count - 1;
            }
        }
        
        return Task.CompletedTask;
    }
    
    private Task OnTabChanged(int index)
    {
        activeTabIndex = index;
        return Task.CompletedTask;
    }
    
    private class TabData
    {
        public string Title { get; set; }
        public int Index { get; set; }
        public bool Closable { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
```
This example shows how to implement closable tabs with dynamic tab generation. Users can add new tabs and close existing ones, with the first tab being non-closable.

### Example 6: Lazy Loading Tab Content
```csharp
<Tab IsLazyLoad="true" OnTabChanged="OnTabChanged">
    <TabItem Text="Dashboard" Icon="fa fa-tachometer-alt" IsActive="true">
        <div class="p-3">
            <h3>Dashboard</h3>
            <p>Dashboard content loaded at: @GetLoadTime(0)</p>
            <div class="row">
                <div class="col-md-4">
                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title">Users</h5>
                            <p class="card-text">1,234</p>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title">Revenue</h5>
                            <p class="card-text">$45,678</p>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title">Orders</h5>
                            <p class="card-text">567</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </TabItem>
    <TabItem Text="Reports" Icon="fa fa-chart-bar">
        <div class="p-3">
            <h3>Reports</h3>
            <p>Reports content loaded at: @GetLoadTime(1)</p>
            <div class="alert alert-info">
                This content was lazily loaded when you clicked on the Reports tab.
            </div>
            <div class="chart-placeholder">
                [Chart Placeholder]
            </div>
        </div>
    </TabItem>
    <TabItem Text="Analytics" Icon="fa fa-chart-line">
        <div class="p-3">
            <h3>Analytics</h3>
            <p>Analytics content loaded at: @GetLoadTime(2)</p>
            <div class="alert alert-info">
                This content was lazily loaded when you clicked on the Analytics tab.
            </div>
            <div class="chart-placeholder">
                [Analytics Placeholder]
            </div>
        </div>
    </TabItem>
    <TabItem Text="Settings" Icon="fa fa-cog">
        <div class="p-3">
            <h3>Settings</h3>
            <p>Settings content loaded at: @GetLoadTime(3)</p>
            <div class="alert alert-info">
                This content was lazily loaded when you clicked on the Settings tab.
            </div>
            <form>
                <div class="mb-3">
                    <label for="setting1" class="form-label">Setting 1</label>
                    <input type="text" class="form-control" id="setting1" />
                </div>
                <div class="mb-3">
                    <label for="setting2" class="form-label">Setting 2</label>
                    <input type="text" class="form-control" id="setting2" />
                </div>
                <button type="submit" class="btn btn-primary">Save Settings</button>
            </form>
        </div>
    </TabItem>
</Tab>

<style>
    .chart-placeholder {
        height: 200px;
        background-color: #f8f9fa;
        border: 1px dashed #dee2e6;
        display: flex;
        align-items: center;
        justify-content: center;
        margin-top: 20px;
    }
</style>

@code {
    private Dictionary<int, string> tabLoadTimes = new();
    
    protected override void OnInitialized()
    {
        // Initialize the first tab's load time
        tabLoadTimes[0] = DateTime.Now.ToString("HH:mm:ss.fff");
    }
    
    private string GetLoadTime(int tabIndex)
    {
        if (tabLoadTimes.TryGetValue(tabIndex, out var time))
        {
            return time;
        }
        return "Not loaded yet";
    }
    
    private Task OnTabChanged(int index)
    {
        // Record the load time for this tab if it hasn't been loaded yet
        if (!tabLoadTimes.ContainsKey(index))
        {
            tabLoadTimes[index] = DateTime.Now.ToString("HH:mm:ss.fff");
        }
        
        return Task.CompletedTask;
    }
}
```
This example demonstrates lazy loading of tab content, where each tab's content is only loaded when the tab is activated. The load time is displayed to show when each tab's content was initialized.

### Example 7: Custom Tab Templates
```csharp
<Tab>
    <TabItemTemplate Context="tab">
        <div class="custom-tab @(tab.IsActive ? "active" : "")">
            @if (!string.IsNullOrEmpty(tab.Icon))
            {
                <i class="@tab.Icon"></i>
            }
            <span class="tab-text">@tab.Text</span>
            @if (tab.Badge != null)
            {
                <span class="badge bg-@tab.Badge.Color">@tab.Badge.Text</span>
            }
        </div>
    </TabItemTemplate>
    <TabItem Text="Home" Icon="fa fa-home" IsActive="true" Badge="new Badge { Text = "New", Color = "primary" }">
        <h3>Home Content</h3>
        <p>This is the content for the Home tab with a custom template.</p>
    </TabItem>
    <TabItem Text="Messages" Icon="fa fa-envelope" Badge="new Badge { Text = "5", Color = "danger" }">
        <h3>Messages Content</h3>
        <p>This is the content for the Messages tab with a custom template.</p>
    </TabItem>
    <TabItem Text="Profile" Icon="fa fa-user">
        <h3>Profile Content</h3>
        <p>This is the content for the Profile tab with a custom template.</p>
    </TabItem>
    <TabItem Text="Settings" Icon="fa fa-cog" Badge="new Badge { Text = "Update", Color = "warning" }">
        <h3>Settings Content</h3>
        <p>This is the content for the Settings tab with a custom template.</p>
    </TabItem>
</Tab>

<style>
    .custom-tab {
        display: flex;
        align-items: center;
        padding: 10px 15px;
        border-radius: 4px 4px 0 0;
        cursor: pointer;
        transition: background-color 0.3s;
    }
    
    .custom-tab:hover {
        background-color: rgba(0, 123, 255, 0.1);
    }
    
    .custom-tab.active {
        background-color: #007bff;
        color: white;
    }
    
    .custom-tab i {
        margin-right: 8px;
    }
    
    .tab-text {
        margin-right: 8px;
    }
    
    .badge {
        font-size: 0.75rem;
        padding: 0.25em 0.6em;
    }
</style>

@code {
    public class Badge
    {
        public string Text { get; set; }
        public string Color { get; set; } // primary, secondary, success, danger, warning, info, etc.
    }
}
```
This example shows how to create custom tab templates with badges, allowing for more complex tab designs beyond the default styling.

## CSS Customization

The Tab component can be customized using the following CSS variables:

```css
--bb-tab-border-width: 1px;
--bb-tab-border-color: #dee2e6;
--bb-tab-border-radius: 0.25rem;
--bb-tab-padding-x: 1rem;
--bb-tab-padding-y: 0.5rem;
--bb-tab-font-size: 1rem;
--bb-tab-color: #495057;
--bb-tab-bg: transparent;
--bb-tab-hover-color: #495057;
--bb-tab-hover-bg: #e9ecef;
--bb-tab-active-color: #007bff;
--bb-tab-active-bg: #fff;
--bb-tab-active-border-color: #dee2e6 #dee2e6 #fff;
--bb-tab-disabled-color: #6c757d;
--bb-tab-disabled-bg: transparent;
--bb-tab-disabled-border-color: #dee2e6;
--bb-tab-content-padding: 1rem;
--bb-tab-content-bg: #fff;
--bb-tab-content-border-color: #dee2e6;
--bb-tab-content-border-width: 1px;
--bb-tab-content-border-radius: 0 0 0.25rem 0.25rem;
--bb-tab-close-button-color: #6c757d;
--bb-tab-close-button-hover-color: #343a40;
--bb-tab-icon-margin-right: 0.5rem;
--bb-tab-vertical-width: 200px;
```

## Service Integration

The Tab component can be integrated with the `TabService` for more advanced scenarios:

```csharp
@inject TabService TabService

<Tab @ref="tabComponent">
    <Items>
        @foreach (var tab in TabService.Tabs)
        {
            <TabItem Text="@tab.Text" Icon="@tab.Icon" IsActive="@tab.IsActive">
                @tab.Content
            </TabItem>
        }
    </Items>
</Tab>

@code {
    private Tab tabComponent;
    
    protected override void OnInitialized()
    {
        // Subscribe to tab events
        TabService.OnTabsChanged += HandleTabsChanged;
        
        // Initialize tabs if needed
        if (!TabService.Tabs.Any())
        {
            TabService.AddTab(new TabOption
            {
                Text = "Home",
                Icon = "fa fa-home",
                IsActive = true,
                Content = "Home content"
            });
            
            TabService.AddTab(new TabOption
            {
                Text = "Profile",
                Icon = "fa fa-user",
                Content = "Profile content"
            });
        }
    }
    
    private void HandleTabsChanged()
    {
        StateHasChanged();
    }
    
    public void Dispose()
    {
        // Unsubscribe from events
        TabService.OnTabsChanged -= HandleTabsChanged;
    }
}
```

To use the `TabService`, you need to register it in your application's service collection:

```csharp
builder.Services.AddBootstrapBlazor();
// or specifically
builder.Services.AddSingleton<TabService>();
```

## Notes

1. **Accessibility**: The Tab component includes ARIA attributes for better accessibility. It uses `role="tablist"`, `role="tab"`, and `role="tabpanel"` attributes, along with appropriate `aria-selected` and `aria-controls` attributes.

2. **Performance**: When using the IsLazyLoad property, tab content is only rendered when the tab is activated, which can improve performance for tabs with complex content.

3. **Mobile Considerations**: On smaller screens, consider using responsive design techniques to adapt the tab layout. For example, you might switch from horizontal to vertical tabs, or use a dropdown menu instead of tabs.

4. **State Persistence**: For complex applications, consider implementing state persistence to remember the active tab across page refreshes or navigation.

5. **Integration with Routing**: The Tab component can be integrated with Blazor's routing system to synchronize the active tab with the URL, allowing for direct linking to specific tabs.