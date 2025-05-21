# Nav Component

## Overview
The Nav component in BootstrapBlazor provides a flexible navigation interface for organizing links and navigation items. It offers various styles and layouts for creating horizontal or vertical navigation bars, tabs, pills, and other navigation patterns. The Nav component is often used for site navigation, tab interfaces, and secondary navigation menus.

## Features
- Multiple display modes (tabs, pills, links)
- Horizontal and vertical orientation
- Justified and fill width options
- Disabled state for nav items
- Active state indication
- Icon support for nav items
- Dropdown support within nav items
- Custom item templates
- Router integration
- Responsive design

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `Items` | `IEnumerable<NavItem>` | `null` | Collection of navigation items to display |
| `ActiveItem` | `NavItem` | `null` | The currently active navigation item |
| `IsVertical` | `bool` | `false` | When true, displays the nav vertically |
| `IsPills` | `bool` | `false` | When true, styles the nav as pills |
| `IsTabs` | `bool` | `false` | When true, styles the nav as tabs |
| `IsJustified` | `bool` | `false` | When true, nav items are justified (equal width) |
| `IsFill` | `bool` | `false` | When true, nav items expand to fill available width |
| `IsCard` | `bool` | `false` | When true, styles the nav for use with card headers |
| `IsLink` | `bool` | `true` | When true, styles the nav items as links |
| `ItemTemplate` | `RenderFragment<NavItem>` | `null` | Custom template for rendering nav items |
| `ChildContent` | `RenderFragment` | `null` | Custom content for the nav |

## Events

| Event | Description |
| --- | --- |
| `OnClick` | Triggered when a nav item is clicked |
| `OnActiveItemChanged` | Triggered when the active item changes |

## Usage Examples

### Example 1: Basic Horizontal Nav
```html
<Nav>
    <Items>
        <NavItem Text="Home" Url="/" Active="true" />
        <NavItem Text="Products" Url="/products" />
        <NavItem Text="Services" Url="/services" />
        <NavItem Text="About" Url="/about" />
        <NavItem Text="Contact" Url="/contact" />
    </Items>
</Nav>
```
This example shows a basic horizontal navigation bar with five items, where the "Home" item is marked as active.

### Example 2: Nav Pills with Icons
```html
<Nav IsPills="true">
    <Items>
        <NavItem Text="Home" Icon="fa fa-home" Url="/" />
        <NavItem Text="Profile" Icon="fa fa-user" Url="/profile" Active="true" />
        <NavItem Text="Messages" Icon="fa fa-envelope" Url="/messages" />
        <NavItem Text="Settings" Icon="fa fa-cog" Url="/settings" />
        <NavItem Text="Logout" Icon="fa fa-sign-out-alt" Url="/logout" IsDisabled="true" />
    </Items>
</Nav>
```
This example demonstrates a nav styled as pills with Font Awesome icons for each item. The "Profile" item is active, and the "Logout" item is disabled.

### Example 3: Vertical Nav
```html
<div class="row">
    <div class="col-md-3">
        <Nav IsVertical="true" IsPills="true">
            <Items>
                <NavItem Text="Account" Icon="fa fa-user" Url="/account" Active="true" />
                <NavItem Text="Security" Icon="fa fa-lock" Url="/security" />
                <NavItem Text="Notifications" Icon="fa fa-bell" Url="/notifications" />
                <NavItem Text="Billing" Icon="fa fa-credit-card" Url="/billing" />
                <NavItem Text="Subscriptions" Icon="fa fa-calendar-check" Url="/subscriptions" />
            </Items>
        </Nav>
    </div>
    <div class="col-md-9">
        <div class="p-3 border rounded">
            <h4>Account Settings</h4>
            <p>This is the content area where the selected settings page would be displayed.</p>
        </div>
    </div>
</div>
```
This example shows a vertical navigation menu styled as pills, commonly used for settings pages or dashboards, alongside a content area.

### Example 4: Tabs with Content Panels
```html
<div>
    <Nav IsTabs="true" OnActiveItemChanged="@OnTabChanged">
        <Items>
            <NavItem Text="Description" Active="true" />
            <NavItem Text="Specifications" />
            <NavItem Text="Reviews" />
            <NavItem Text="Shipping" />
        </Items>
    </Nav>
    
    <div class="tab-content p-3 border border-top-0 rounded-bottom">
        @if (activeTabIndex == 0)
        {
            <div class="tab-pane active">
                <h4>Product Description</h4>
                <p>This is a detailed description of the product...</p>
            </div>
        }
        else if (activeTabIndex == 1)
        {
            <div class="tab-pane active">
                <h4>Product Specifications</h4>
                <ul>
                    <li>Dimension: 10" x 5" x 2"</li>
                    <li>Weight: 1.2 lbs</li>
                    <li>Material: Aluminum</li>
                </ul>
            </div>
        }
        else if (activeTabIndex == 2)
        {
            <div class="tab-pane active">
                <h4>Customer Reviews</h4>
                <p>Average Rating: 4.5/5</p>
                <!-- Reviews would go here -->
            </div>
        }
        else if (activeTabIndex == 3)
        {
            <div class="tab-pane active">
                <h4>Shipping Information</h4>
                <p>Free shipping on orders over $50.</p>
                <p>Estimated delivery: 3-5 business days</p>
            </div>
        }
    </div>
</div>

@code {
    private int activeTabIndex = 0;
    
    private Task OnTabChanged(NavItem item)
    {
        // Find the index of the clicked tab
        var items = new List<NavItem> { /* your nav items */ };
        activeTabIndex = items.IndexOf(item);
        return Task.CompletedTask;
    }
}
```
This example demonstrates using the Nav component as tabs with corresponding content panels that change when a tab is clicked.

### Example 5: Justified and Fill Width Nav
```html
<h5>Justified Nav (equal width)</h5>
<Nav IsJustified="true" IsPills="true">
    <Items>
        <NavItem Text="Home" Url="/" />
        <NavItem Text="Profile" Url="/profile" Active="true" />
        <NavItem Text="Messages" Url="/messages" />
        <NavItem Text="Settings" Url="/settings" />
    </Items>
</Nav>

<h5 class="mt-4">Fill Width Nav (proportional width)</h5>
<Nav IsFill="true" IsPills="true">
    <Items>
        <NavItem Text="Home" Url="/" />
        <NavItem Text="Profile" Url="/profile" Active="true" />
        <NavItem Text="Messages" Url="/messages" />
        <NavItem Text="Settings" Url="/settings" />
    </Items>
</Nav>
```
This example shows the difference between justified navigation (all items have equal width) and fill width navigation (items expand proportionally to fill the available space).

### Example 6: Nav with Dropdowns
```html
<Nav IsTabs="true">
    <Items>
        <NavItem Text="Home" Url="/" />
        <NavItem Text="Products" Active="true">
            <SubItems>
                <NavItem Text="Category 1" Url="/products/category1" />
                <NavItem Text="Category 2" Url="/products/category2" />
                <NavItem Text="Category 3" Url="/products/category3" />
            </SubItems>
        </NavItem>
        <NavItem Text="Services">
            <SubItems>
                <NavItem Text="Service 1" Url="/services/service1" />
                <NavItem Text="Service 2" Url="/services/service2" />
                <NavItem Text="Service 3" Url="/services/service3" />
            </SubItems>
        </NavItem>
        <NavItem Text="About" Url="/about" />
        <NavItem Text="Contact" Url="/contact" />
    </Items>
</Nav>
```
This example demonstrates a tab navigation with dropdown menus for the "Products" and "Services" items.

### Example 7: Custom Item Template
```html
<Nav IsPills="true">
    <ItemTemplate Context="item">
        <div class="d-flex align-items-center p-2 @(item.Active ? "active" : "")">
            @if (!string.IsNullOrEmpty(item.Icon))
            {
                <i class="@item.Icon mr-2"></i>
            }
            <span>@item.Text</span>
            @if (item.Badge != null)
            {
                <span class="badge badge-@item.Badge.Color ml-2">@item.Badge.Text</span>
            }
        </div>
    </ItemTemplate>
    <Items>
        <NavItem Text="Home" Icon="fa fa-home" Url="/" />
        <NavItem Text="Messages" Icon="fa fa-envelope" Url="/messages" Badge="new Badge { Text = "5", Color = "danger" }" />
        <NavItem Text="Profile" Icon="fa fa-user" Url="/profile" Active="true" />
        <NavItem Text="Tasks" Icon="fa fa-tasks" Url="/tasks" Badge="new Badge { Text = "New", Color = "success" }" />
    </Items>
</Nav>

@code {
    public class Badge
    {
        public string Text { get; set; }
        public string Color { get; set; } // primary, secondary, success, danger, warning, info, etc.
    }
}
```
This example shows how to use a custom template for nav items, adding badges to indicate notifications or status.

## CSS Customization

The Nav component can be customized using the following CSS variables:

```css
--bb-nav-link-padding-y: 0.5rem;
--bb-nav-link-padding-x: 1rem;
--bb-nav-link-color: #007bff;
--bb-nav-link-hover-color: #0056b3;
--bb-nav-link-disabled-color: #6c757d;
--bb-nav-tabs-border-color: #dee2e6;
--bb-nav-tabs-border-width: 1px;
--bb-nav-tabs-border-radius: 0.25rem;
--bb-nav-tabs-link-hover-border-color: #e9ecef #e9ecef #dee2e6;
--bb-nav-tabs-link-active-color: #495057;
--bb-nav-tabs-link-active-bg: #fff;
--bb-nav-tabs-link-active-border-color: #dee2e6 #dee2e6 #fff;
--bb-nav-pills-border-radius: 0.25rem;
--bb-nav-pills-link-active-color: #fff;
--bb-nav-pills-link-active-bg: #007bff;
```

## Notes

1. **Accessibility**: The Nav component includes ARIA attributes for better accessibility. It uses `role="navigation"` for the nav container and appropriate `aria-selected` attributes for active items.

2. **Router Integration**: When using the Nav component with the Blazor Router, you can use the `Match` property on NavItems to control how the active state is determined. Use `NavLinkMatch.All` for exact matches and `NavLinkMatch.Prefix` for prefix matches.

3. **Mobile Considerations**: For mobile devices, consider using a responsive design that adapts the navigation layout based on screen size. For example, you might use a horizontal nav on desktop and a vertical nav on mobile.

4. **Tabs vs. Pills**: Use tabs for content that is related and can be switched between without changing the context. Use pills for navigation between different sections or pages.

5. **Performance**: For navs with a large number of items, consider implementing virtualization or pagination to improve performance.