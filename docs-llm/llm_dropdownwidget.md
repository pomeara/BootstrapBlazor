# DropdownWidget Component

## Overview
The DropdownWidget component in BootstrapBlazor extends the standard Dropdown functionality by providing a more versatile container for complex content. Unlike the regular Dropdown which is primarily designed for menu items, DropdownWidget allows for embedding any content within a dropdown panel, making it suitable for notifications, user profiles, settings panels, and other rich interactive elements.

## Features
- Support for complex content within dropdown panel
- Multiple trigger methods (click, hover)
- Customizable dropdown button/trigger
- Placement options (top, bottom, left, right)
- Width and height customization
- Backdrop customization
- Auto-close behavior configuration
- Animation effects
- Keyboard navigation support

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `Color` | `Color` | `Color.Primary` | Sets the color theme of the dropdown button |
| `Icon` | `string` | `null` | Icon to display on the dropdown button |
| `Text` | `string` | `null` | Text to display on the dropdown button |
| `IsDisabled` | `bool` | `false` | Disables the dropdown when set to true |
| `IsDropup` | `bool` | `false` | When true, the dropdown panel appears above instead of below |
| `IsRight` | `bool` | `false` | When true, the dropdown panel is right-aligned |
| `IsOutsideClick` | `bool` | `true` | When true, clicking outside the dropdown closes it |
| `IsBackdrop` | `bool` | `false` | When true, shows a backdrop behind the dropdown panel |
| `TriggerType` | `TriggerType` | `TriggerType.Click` | Sets how the dropdown is triggered (Click or Hover) |
| `Width` | `int` | `0` | Sets the width of the dropdown panel in pixels (0 means auto) |
| `Height` | `int` | `0` | Sets the height of the dropdown panel in pixels (0 means auto) |
| `Placement` | `Placement` | `Placement.BottomStart` | Sets the placement of the dropdown panel |
| `ChildContent` | `RenderFragment` | `null` | Content to display inside the dropdown panel |
| `ButtonTemplate` | `RenderFragment` | `null` | Custom template for the dropdown trigger button |

## Events

| Event | Description |
| --- | --- |
| `OnClick` | Triggered when the dropdown button is clicked |
| `OnVisibleChanged` | Triggered when the dropdown visibility changes |

## Usage Examples

### Example 1: Basic DropdownWidget
```html
<DropdownWidget Text="Settings">
    <div class="p-3" style="width: 300px;">
        <h5>Application Settings</h5>
        <hr>
        <div class="mb-3">
            <label>Theme</label>
            <select class="form-control">
                <option>Light</option>
                <option>Dark</option>
                <option>System</option>
            </select>
        </div>
        <div class="mb-3">
            <label>Language</label>
            <select class="form-control">
                <option>English</option>
                <option>Spanish</option>
                <option>Chinese</option>
            </select>
        </div>
        <button class="btn btn-primary">Save</button>
    </div>
</DropdownWidget>
```
This example shows a basic dropdown widget with a settings panel containing form controls.

### Example 2: Notification Panel
```html
<DropdownWidget Icon="fa fa-bell" IsRight="true">
    <div class="p-2" style="width: 350px;">
        <h6 class="dropdown-header">Notifications</h6>
        <div class="notification-item p-2 border-bottom">
            <div class="d-flex">
                <div class="mr-3">
                    <i class="fa fa-user-plus text-info fa-lg"></i>
                </div>
                <div>
                    <div class="font-weight-bold">New User Registration</div>
                    <div class="small text-muted">John Doe just registered</div>
                    <div class="small text-muted">2 hours ago</div>
                </div>
            </div>
        </div>
        <div class="notification-item p-2 border-bottom">
            <div class="d-flex">
                <div class="mr-3">
                    <i class="fa fa-comment text-success fa-lg"></i>
                </div>
                <div>
                    <div class="font-weight-bold">New Comment</div>
                    <div class="small text-muted">Mary commented on your post</div>
                    <div class="small text-muted">4 hours ago</div>
                </div>
            </div>
        </div>
        <div class="notification-item p-2">
            <div class="d-flex">
                <div class="mr-3">
                    <i class="fa fa-exclamation-circle text-warning fa-lg"></i>
                </div>
                <div>
                    <div class="font-weight-bold">System Alert</div>
                    <div class="small text-muted">Server load is high</div>
                    <div class="small text-muted">1 day ago</div>
                </div>
            </div>
        </div>
        <a href="#" class="dropdown-item text-center small text-muted mt-2">View all notifications</a>
    </div>
</DropdownWidget>
```
This example demonstrates a notification panel with styled notification items, icons, and timestamps.

### Example 3: User Profile Dropdown
```html
<DropdownWidget IsRight="true">
    <ButtonTemplate>
        <div class="d-flex align-items-center">
            <img src="/images/avatar.jpg" class="rounded-circle" style="width: 32px; height: 32px;" />
            <span class="ml-2 d-none d-md-inline">John Doe</span>
            <i class="fa fa-chevron-down ml-1 small"></i>
        </div>
    </ButtonTemplate>
    <div class="p-0" style="width: 250px;">
        <div class="p-3 text-center border-bottom">
            <img src="/images/avatar.jpg" class="rounded-circle mb-2" style="width: 64px; height: 64px;" />
            <h6 class="mb-0">John Doe</h6>
            <small class="text-muted">Administrator</small>
        </div>
        <a href="/profile" class="dropdown-item py-2">
            <i class="fa fa-user mr-2"></i> My Profile
        </a>
        <a href="/settings" class="dropdown-item py-2">
            <i class="fa fa-cog mr-2"></i> Settings
        </a>
        <a href="/activity" class="dropdown-item py-2">
            <i class="fa fa-list mr-2"></i> Activity Log
        </a>
        <div class="dropdown-divider"></div>
        <a href="/logout" class="dropdown-item py-2">
            <i class="fa fa-sign-out mr-2"></i> Logout
        </a>
    </div>
</DropdownWidget>
```
This example shows a user profile dropdown with a custom button template displaying the user's avatar and name, and a dropdown panel with profile options.

### Example 4: Shopping Cart Widget
```html
<DropdownWidget Icon="fa fa-shopping-cart" IsRight="true" Width="320">
    <div class="p-3">
        <h6 class="mb-3">Shopping Cart (3 items)</h6>
        <div class="cart-item d-flex mb-3 pb-3 border-bottom">
            <img src="/images/product1.jpg" style="width: 50px; height: 50px; object-fit: cover;" />
            <div class="ml-3 flex-grow-1">
                <div class="font-weight-bold">Wireless Headphones</div>
                <div class="small">1 × $59.99</div>
            </div>
            <button class="btn btn-sm btn-link text-danger">
                <i class="fa fa-trash"></i>
            </button>
        </div>
        <div class="cart-item d-flex mb-3 pb-3 border-bottom">
            <img src="/images/product2.jpg" style="width: 50px; height: 50px; object-fit: cover;" />
            <div class="ml-3 flex-grow-1">
                <div class="font-weight-bold">Smartphone Case</div>
                <div class="small">2 × $19.99</div>
            </div>
            <button class="btn btn-sm btn-link text-danger">
                <i class="fa fa-trash"></i>
            </button>
        </div>
        <div class="d-flex justify-content-between mb-3">
            <span class="font-weight-bold">Total:</span>
            <span class="font-weight-bold">$99.97</span>
        </div>
        <div class="d-flex">
            <a href="/cart" class="btn btn-outline-primary flex-grow-1 mr-2">View Cart</a>
            <a href="/checkout" class="btn btn-primary flex-grow-1">Checkout</a>
        </div>
    </div>
</DropdownWidget>
```
This example demonstrates a shopping cart dropdown widget with product items, quantities, prices, and checkout options.

### Example 5: Hover-Triggered Dropdown
```html
<DropdownWidget Text="Hover Me" TriggerType="TriggerType.Hover">
    <div class="p-3" style="width: 200px;">
        <p>This dropdown appears when you hover over the button.</p>
        <p>Move your mouse away to close it.</p>
    </div>
</DropdownWidget>
```
This example shows a dropdown widget that is triggered by hovering over the button rather than clicking it.

### Example 6: Dropdown with Backdrop
```html
<DropdownWidget Text="Open Modal-like Dropdown" IsBackdrop="true" Width="400">
    <div class="p-4">
        <h4>Important Information</h4>
        <p>This dropdown has a backdrop that dims the rest of the page, similar to a modal dialog.</p>
        <p>It helps focus the user's attention on the dropdown content.</p>
        <button class="btn btn-primary">Acknowledge</button>
    </div>
</DropdownWidget>
```
This example demonstrates a dropdown widget with a backdrop that dims the rest of the page, creating a modal-like experience.

### Example 7: Custom Placement Dropdown
```html
<div class="d-flex justify-content-between">
    <DropdownWidget Text="Bottom Start" Placement="Placement.BottomStart">
        <div class="p-3">Default placement (bottom-start)</div>
    </DropdownWidget>
    
    <DropdownWidget Text="Bottom End" Placement="Placement.BottomEnd">
        <div class="p-3">Bottom-end placement</div>
    </DropdownWidget>
    
    <DropdownWidget Text="Top Start" Placement="Placement.TopStart">
        <div class="p-3">Top-start placement</div>
    </DropdownWidget>
    
    <DropdownWidget Text="Top End" Placement="Placement.TopEnd">
        <div class="p-3">Top-end placement</div>
    </DropdownWidget>
</div>
```
This example shows different placement options for the dropdown panel: bottom-start (default), bottom-end, top-start, and top-end.

## CSS Customization

The DropdownWidget component can be customized using the following CSS variables:

```css
--bb-dropdown-widget-min-width: 10rem;
--bb-dropdown-widget-padding: 0.5rem 0;
--bb-dropdown-widget-color: #212529;
--bb-dropdown-widget-bg: #fff;
--bb-dropdown-widget-border-color: rgba(0, 0, 0, 0.15);
--bb-dropdown-widget-border-radius: 0.25rem;
--bb-dropdown-widget-border-width: 1px;
--bb-dropdown-widget-box-shadow: 0 0.5rem 1rem rgba(0, 0, 0, 0.175);
--bb-dropdown-widget-backdrop-bg: rgba(0, 0, 0, 0.3);
--bb-dropdown-widget-backdrop-zindex: 1040;
--bb-dropdown-widget-zindex: 1050;
--bb-dropdown-widget-animation-duration: 0.2s;
```

## Notes

1. **Accessibility**: The DropdownWidget component includes ARIA attributes for better accessibility. It uses `aria-haspopup="true"`, `aria-expanded`, and proper keyboard navigation support.

2. **Performance**: For dropdowns with complex content or many elements, consider implementing lazy loading to improve performance. Only render complex content when the dropdown is actually opened.

3. **Mobile Considerations**: On mobile devices, ensure the dropdown panel is appropriately sized and that touch targets are large enough. Consider making the dropdown panel full-width on small screens.

4. **Backdrop Usage**: The backdrop option (`IsBackdrop="true"`) is useful for important dropdowns that require user attention, but use it sparingly as it blocks interaction with the rest of the page.

5. **Hover Trigger**: When using `TriggerType.Hover`, ensure there's enough delay before closing the dropdown to prevent accidental closures when the user briefly moves the mouse away.