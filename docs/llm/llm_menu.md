# Menu Component

## Overview
The Menu component in BootstrapBlazor provides a hierarchical navigation structure that allows users to browse through different sections of an application. It supports multiple levels of navigation, various display modes, and customizable styling, making it suitable for both horizontal navigation bars and vertical sidebar menus.

## Features
- Multiple display modes (horizontal, vertical, inline)
- Support for multi-level nested menus
- Collapsible sub-menus
- Icon support for menu items
- Active state indication
- Disabled state for menu items
- Accordion mode for vertical menus
- Custom item templates
- Router integration
- Keyboard navigation support
- Mobile-friendly responsive design

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `Items` | `IEnumerable<MenuItem>` | `null` | Collection of menu items to display |
| `IsVertical` | `bool` | `false` | When true, displays the menu vertically |
| `IsAccordion` | `bool` | `false` | When true, only one submenu can be expanded at a time (accordion mode) |
| `IsCollapsed` | `bool` | `false` | When true, collapses the menu to show only icons (for vertical menus) |
| `IsNavbar` | `bool` | `false` | When true, styles the menu as a navbar |
| `IsOnlyIcon` | `bool` | `false` | When true, shows only icons for menu items |
| `IsExpandAll` | `bool` | `false` | When true, expands all submenus by default |
| `DisableNavigation` | `bool` | `false` | When true, disables navigation when clicking menu items |
| `OnClick` | `EventCallback<MenuItem>` | `null` | Event triggered when a menu item is clicked |
| `OnCollapsedChanged` | `EventCallback<bool>` | `null` | Event triggered when the collapsed state changes |
| `ActiveItem` | `MenuItem` | `null` | The currently active menu item |
| `IndentSize` | `int` | `16` | Indentation size in pixels for nested menu items |
| `ItemTemplate` | `RenderFragment<MenuItem>` | `null` | Custom template for rendering menu items |
| `SubMenuTemplate` | `RenderFragment<MenuItem>` | `null` | Custom template for rendering submenus |

## Events

| Event | Description |
| --- | --- |
| `OnClick` | Triggered when a menu item is clicked |
| `OnCollapsedChanged` | Triggered when the collapsed state changes |

## Usage Examples

### Example 1: Basic Horizontal Menu
```html
<Menu>
    <Items>
        <MenuItem Text="Home" Icon="fa fa-home" Url="/" />
        <MenuItem Text="Products" Icon="fa fa-shopping-cart" Url="/products" />
        <MenuItem Text="Services" Icon="fa fa-cogs" Url="/services" />
        <MenuItem Text="About" Icon="fa fa-info-circle" Url="/about" />
        <MenuItem Text="Contact" Icon="fa fa-envelope" Url="/contact" />
    </Items>
</Menu>
```
This example shows a basic horizontal menu with five top-level items, each with an icon and a URL.

### Example 2: Vertical Menu with Nested Items
```html
<Menu IsVertical="true">
    <Items>
        <MenuItem Text="Dashboard" Icon="fa fa-tachometer-alt" Url="/dashboard" />
        <MenuItem Text="User Management" Icon="fa fa-users">
            <Items>
                <MenuItem Text="View Users" Url="/users" />
                <MenuItem Text="Add User" Url="/users/add" />
                <MenuItem Text="User Roles" Url="/users/roles" />
                <MenuItem Text="Permissions" Url="/users/permissions" />
            </Items>
        </MenuItem>
        <MenuItem Text="Content" Icon="fa fa-file-alt">
            <Items>
                <MenuItem Text="Pages" Url="/content/pages" />
                <MenuItem Text="Blog Posts" Url="/content/blog" />
                <MenuItem Text="Media Library" Url="/content/media" />
            </Items>
        </MenuItem>
        <MenuItem Text="Settings" Icon="fa fa-cog" Url="/settings" />
    </Items>
</Menu>
```
This example demonstrates a vertical menu with nested submenus for User Management and Content sections.

### Example 3: Accordion-Style Vertical Menu
```html
<Menu IsVertical="true" IsAccordion="true">
    <Items>
        <MenuItem Text="Products" Icon="fa fa-box">
            <Items>
                <MenuItem Text="Electronics" Url="/products/electronics" />
                <MenuItem Text="Clothing" Url="/products/clothing" />
                <MenuItem Text="Home & Garden" Url="/products/home-garden" />
            </Items>
        </MenuItem>
        <MenuItem Text="Categories" Icon="fa fa-tags">
            <Items>
                <MenuItem Text="New Arrivals" Url="/categories/new" />
                <MenuItem Text="Best Sellers" Url="/categories/best-sellers" />
                <MenuItem Text="Clearance" Url="/categories/clearance" />
            </Items>
        </MenuItem>
        <MenuItem Text="Customer Service" Icon="fa fa-headset">
            <Items>
                <MenuItem Text="Contact Us" Url="/service/contact" />
                <MenuItem Text="Returns" Url="/service/returns" />
                <MenuItem Text="FAQ" Url="/service/faq" />
            </Items>
        </MenuItem>
    </Items>
</Menu>
```
This example shows an accordion-style vertical menu where only one submenu can be expanded at a time.

### Example 4: Collapsible Sidebar Menu
```html
<div class="d-flex">
    <Button Icon="fa fa-bars" OnClick="@ToggleMenu" />
    
    <div class="sidebar" style="@(isCollapsed ? "width: 60px;" : "width: 250px;")">
        <Menu IsVertical="true" IsCollapsed="@isCollapsed" OnCollapsedChanged="@OnMenuCollapsedChanged">
            <Items>
                <MenuItem Text="Dashboard" Icon="fa fa-tachometer-alt" Url="/dashboard" />
                <MenuItem Text="Analytics" Icon="fa fa-chart-bar">
                    <Items>
                        <MenuItem Text="Sales" Url="/analytics/sales" />
                        <MenuItem Text="Traffic" Url="/analytics/traffic" />
                        <MenuItem Text="Conversions" Url="/analytics/conversions" />
                    </Items>
                </MenuItem>
                <MenuItem Text="Customers" Icon="fa fa-users" Url="/customers" />
                <MenuItem Text="Orders" Icon="fa fa-shopping-cart" Url="/orders" />
                <MenuItem Text="Products" Icon="fa fa-box" Url="/products" />
                <MenuItem Text="Reports" Icon="fa fa-file-alt" Url="/reports" />
                <MenuItem Text="Settings" Icon="fa fa-cog" Url="/settings" />
            </Items>
        </Menu>
    </div>
    
    <div class="content-area flex-grow-1 p-3">
        <h2>Main Content Area</h2>
        <p>This is where your page content would go.</p>
    </div>
</div>

@code {
    private bool isCollapsed = false;
    
    private void ToggleMenu()
    {
        isCollapsed = !isCollapsed;
    }
    
    private void OnMenuCollapsedChanged(bool collapsed)
    {
        isCollapsed = collapsed;
    }
}
```
This example demonstrates a collapsible sidebar menu that can be toggled between a full view and a collapsed icon-only view.

### Example 5: Custom Item Template
```html
<Menu>
    <ItemTemplate Context="item">
        <div class="d-flex align-items-center py-2 px-3 @(item.IsActive ? "active-item" : "")">
            @if (!string.IsNullOrEmpty(item.Icon))
            {
                <i class="@item.Icon mr-2"></i>
            }
            <span>@item.Text</span>
            @if (item.IsNew)
            {
                <span class="badge badge-pill badge-danger ml-2">New</span>
            }
        </div>
    </ItemTemplate>
    <Items>
        <MenuItem Text="Home" Icon="fa fa-home" Url="/" />
        <MenuItem Text="Products" Icon="fa fa-shopping-cart" Url="/products" IsNew="true" />
        <MenuItem Text="Services" Icon="fa fa-cogs" Url="/services" />
        <MenuItem Text="About" Icon="fa fa-info-circle" Url="/about" />
        <MenuItem Text="Contact" Icon="fa fa-envelope" Url="/contact" />
    </Items>
</Menu>

<style>
    .active-item {
        background-color: #f0f0f0;
        font-weight: bold;
    }
</style>
```
This example shows how to use a custom template for menu items, adding a "New" badge to specific items and custom styling for active items.

### Example 6: Menu with Router Integration
```html
@using Microsoft.AspNetCore.Components.Routing

<Router AppAssembly="@typeof(Program).Assembly">
    <Found Context="routeData">
        <div class="d-flex">
            <div class="sidebar">
                <Menu IsVertical="true" ActiveItem="@GetActiveItem(routeData)">
                    <Items>
                        <MenuItem Text="Home" Icon="fa fa-home" Url="/" Match="NavLinkMatch.All" />
                        <MenuItem Text="Counter" Icon="fa fa-plus-circle" Url="/counter" />
                        <MenuItem Text="Fetch Data" Icon="fa fa-table" Url="/fetchdata" />
                    </Items>
                </Menu>
            </div>
            <div class="content-area flex-grow-1 p-3">
                <RouteView RouteData="@routeData" DefaultLayout="@typeof(MainLayout)" />
            </div>
        </div>
    </Found>
    <NotFound>
        <LayoutView Layout="@typeof(MainLayout)">
            <p>Sorry, there's nothing at this address.</p>
        </LayoutView>
    </NotFound>
</Router>

@code {
    private MenuItem GetActiveItem(RouteData routeData)
    {
        var relativePath = routeData.RouteValues["page"]?.ToString() ?? "";
        return MenuItems.FirstOrDefault(item => item.Url.TrimStart('/').Equals(relativePath, StringComparison.OrdinalIgnoreCase));
    }
    
    private List<MenuItem> MenuItems => new List<MenuItem>
    {
        new MenuItem { Text = "Home", Icon = "fa fa-home", Url = "/", Match = NavLinkMatch.All },
        new MenuItem { Text = "Counter", Icon = "fa fa-plus-circle", Url = "/counter" },
        new MenuItem { Text = "Fetch Data", Icon = "fa fa-table", Url = "/fetchdata" }
    };
}
```
This example demonstrates how to integrate the Menu component with the Blazor Router, automatically highlighting the active menu item based on the current route.

### Example 7: Responsive Mobile Menu
```html
<div>
    <!-- Mobile Toggle Button (visible on small screens) -->
    <div class="d-md-none">
        <Button Icon="fa fa-bars" OnClick="@ToggleMobileMenu" />
    </div>
    
    <!-- Desktop Menu (visible on medium and larger screens) -->
    <div class="d-none d-md-block">
        <Menu>
            <Items>
                <MenuItem Text="Home" Icon="fa fa-home" Url="/" />
                <MenuItem Text="Products" Icon="fa fa-shopping-cart">
                    <Items>
                        <MenuItem Text="Category 1" Url="/products/category1" />
                        <MenuItem Text="Category 2" Url="/products/category2" />
                        <MenuItem Text="Category 3" Url="/products/category3" />
                    </Items>
                </MenuItem>
                <MenuItem Text="Services" Icon="fa fa-cogs" Url="/services" />
                <MenuItem Text="About" Icon="fa fa-info-circle" Url="/about" />
                <MenuItem Text="Contact" Icon="fa fa-envelope" Url="/contact" />
            </Items>
        </Menu>
    </div>
    
    <!-- Mobile Menu (initially hidden, toggled by button) -->
    <div class="mobile-menu @(isMobileMenuVisible ? "show" : "")">
        <div class="mobile-menu-header d-flex justify-content-between align-items-center p-3 border-bottom">
            <h5 class="m-0">Menu</h5>
            <Button Icon="fa fa-times" OnClick="@ToggleMobileMenu" />
        </div>
        <Menu IsVertical="true" IsAccordion="true">
            <Items>
                <MenuItem Text="Home" Icon="fa fa-home" Url="/" />
                <MenuItem Text="Products" Icon="fa fa-shopping-cart">
                    <Items>
                        <MenuItem Text="Category 1" Url="/products/category1" />
                        <MenuItem Text="Category 2" Url="/products/category2" />
                        <MenuItem Text="Category 3" Url="/products/category3" />
                    </Items>
                </MenuItem>
                <MenuItem Text="Services" Icon="fa fa-cogs" Url="/services" />
                <MenuItem Text="About" Icon="fa fa-info-circle" Url="/about" />
                <MenuItem Text="Contact" Icon="fa fa-envelope" Url="/contact" />
            </Items>
        </Menu>
    </div>
</div>

<style>
    .mobile-menu {
        position: fixed;
        top: 0;
        left: -280px;
        width: 280px;
        height: 100vh;
        background-color: white;
        box-shadow: 2px 0 5px rgba(0,0,0,0.2);
        transition: left 0.3s ease;
        z-index: 1050;
    }
    
    .mobile-menu.show {
        left: 0;
    }
</style>

@code {
    private bool isMobileMenuVisible = false;
    
    private void ToggleMobileMenu()
    {
        isMobileMenuVisible = !isMobileMenuVisible;
    }
}
```
This example shows how to create a responsive menu that displays horizontally on desktop screens and as a slide-out drawer on mobile devices.

## CSS Customization

The Menu component can be customized using the following CSS variables:

```css
--bb-menu-item-padding-y: 0.5rem;
--bb-menu-item-padding-x: 1rem;
--bb-menu-item-color: #212529;
--bb-menu-item-hover-color: #16181b;
--bb-menu-item-hover-bg: #f8f9fa;
--bb-menu-item-active-color: #fff;
--bb-menu-item-active-bg: #0d6efd;
--bb-menu-item-disabled-color: #6c757d;
--bb-menu-item-disabled-bg: transparent;
--bb-menu-item-icon-width: 1.5rem;
--bb-menu-item-icon-margin-right: 0.5rem;
--bb-menu-submenu-padding-left: 1.5rem;
--bb-menu-border-color: rgba(0, 0, 0, 0.125);
--bb-menu-box-shadow: 0 0.5rem 1rem rgba(0, 0, 0, 0.15);
--bb-menu-transition-duration: 0.2s;
```

## Notes

1. **Accessibility**: The Menu component includes ARIA attributes for better accessibility. It uses `role="menu"`, `role="menuitem"`, and proper keyboard navigation support.

2. **Performance**: For menus with a large number of items or deep nesting, consider implementing virtualization or lazy loading to improve performance.

3. **Mobile Considerations**: On mobile devices, ensure menu items have sufficient touch target size (at least 44x44 pixels) for better usability. Consider using a different layout for mobile devices, such as a hamburger menu or a drawer.

4. **Nesting Depth**: While the Menu component supports unlimited nesting depth, it's generally recommended to limit nesting to 2-3 levels for better usability.

5. **Router Integration**: When using the Menu component with the Blazor Router, use the `Match` property to control how the active state is determined. Use `NavLinkMatch.All` for exact matches and `NavLinkMatch.Prefix` for prefix matches.