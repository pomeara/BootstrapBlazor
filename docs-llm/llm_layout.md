# Layout Component

## Overview
The Layout component in BootstrapBlazor provides a flexible and responsive page layout system that helps organize content in a structured manner. It follows a common web application layout pattern with header, sidebar, content, and footer sections. The component is highly customizable and supports various layout configurations, including fixed and responsive layouts, collapsible sidebars, and custom styling options.

## Key Features
- **Responsive Design**: Automatically adapts to different screen sizes and devices
- **Flexible Layout Options**: Supports various layout configurations including fixed and fluid layouts
- **Collapsible Sidebar**: Sidebar can be collapsed to maximize content space
- **Multiple Sidebar Positions**: Sidebar can be positioned on the left or right side
- **Fixed Header and Footer**: Options for fixed header and footer for better navigation
- **Custom Styling**: Extensive customization options through CSS variables
- **Nested Layouts**: Support for nested layouts for complex page structures
- **Content Scrolling**: Independent scrolling for different layout sections
- **Sidebar Toggle**: Built-in sidebar toggle functionality for mobile responsiveness
- **Theme Integration**: Seamless integration with BootstrapBlazor themes

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `IsFixedHeader` | `bool` | `false` | When true, fixes the header at the top of the viewport |
| `IsFixedFooter` | `bool` | `false` | When true, fixes the footer at the bottom of the viewport |
| `IsFullSide` | `bool` | `false` | When true, extends the sidebar to full height |
| `IsPage` | `bool` | `false` | When true, applies page-specific layout styles |
| `ShowFooter` | `bool` | `true` | When true, displays the footer section |
| `ShowGotoTop` | `bool` | `false` | When true, displays a "Go to Top" button |
| `ShowCollapseBar` | `bool` | `true` | When true, displays the sidebar collapse toggle |
| `SideWidth` | `int` | `240` | Width of the sidebar in pixels |
| `SideMinWidth` | `int` | `0` | Minimum width of the sidebar when collapsed |
| `SideMaxWidth` | `int` | `0` | Maximum width of the sidebar when expanded |
| `HeaderHeight` | `int` | `0` | Height of the header in pixels (0 for auto) |
| `FooterHeight` | `int` | `0` | Height of the footer in pixels (0 for auto) |
| `SideBarColor` | `Color` | `Color.None` | Color theme for the sidebar |
| `SideExpanded` | `bool` | `true` | When true, expands the sidebar by default |
| `HeaderTemplate` | `RenderFragment` | `null` | Template for the header content |
| `SideBarTemplate` | `RenderFragment` | `null` | Template for the sidebar content |
| `FooterTemplate` | `RenderFragment` | `null` | Template for the footer content |
| `ChildContent` | `RenderFragment` | `null` | Content for the main section |
| `SideWidth` | `string` | `null` | Width of the sidebar as a CSS value (e.g., "250px", "20%") |
| `AdditionalAttributes` | `IReadOnlyDictionary<string, object>` | `null` | Additional attributes for the layout container |

## Events

| Event | Description |
| --- | --- |
| `OnCollapsed` | Triggered when the sidebar is collapsed or expanded |

## Usage Examples

### Example 1: Basic Layout
```razor
@using BootstrapBlazor.Components

<Layout>
    <HeaderTemplate>
        <div class="d-flex align-items-center">
            <img src="_content/BootstrapBlazor.Shared/images/logo.png" width="36" height="36" alt="logo" />
            <div class="ms-2 text-white">BootstrapBlazor</div>
        </div>
    </HeaderTemplate>
    <SideBarTemplate>
        <div class="menu">
            <Menu Items="@Menus" />
        </div>
    </SideBarTemplate>
    <ChildContent>
        <div class="p-4">
            <h3>Main Content Area</h3>
            <p>This is the main content of the page.</p>
        </div>
    </ChildContent>
    <FooterTemplate>
        <div class="text-center">© 2023 BootstrapBlazor. All rights reserved.</div>
    </FooterTemplate>
</Layout>

@code {
    private List<MenuItem> Menus { get; set; } = new List<MenuItem>
    {
        new MenuItem { Text = "Home", Icon = "fa-solid fa-home", Url = "/" },
        new MenuItem { Text = "About", Icon = "fa-solid fa-info-circle", Url = "/about" },
        new MenuItem { Text = "Contact", Icon = "fa-solid fa-envelope", Url = "/contact" }
    };
}
```

### Example 2: Fixed Header and Footer Layout
```razor
<Layout IsFixedHeader="true" IsFixedFooter="true">
    <HeaderTemplate>
        <div class="d-flex align-items-center">
            <img src="_content/BootstrapBlazor.Shared/images/logo.png" width="36" height="36" alt="logo" />
            <div class="ms-2 text-white">BootstrapBlazor</div>
        </div>
    </HeaderTemplate>
    <SideBarTemplate>
        <div class="menu">
            <Menu Items="@Menus" IsVertical="true" />
        </div>
    </SideBarTemplate>
    <ChildContent>
        <div class="p-4">
            <h3>Fixed Header and Footer</h3>
            <p>This layout has a fixed header and footer. The content area scrolls independently.</p>
            
            <!-- Add enough content to demonstrate scrolling -->
            @for (int i = 1; i <= 20; i++)
            {
                <div class="card mb-3">
                    <div class="card-body">
                        <h5 class="card-title">Content Section @i</h5>
                        <p class="card-text">This is a content section to demonstrate scrolling with fixed header and footer.</p>
                    </div>
                </div>
            }
        </div>
    </ChildContent>
    <FooterTemplate>
        <div class="text-center">© 2023 BootstrapBlazor. All rights reserved.</div>
    </FooterTemplate>
</Layout>
```

### Example 3: Collapsible Sidebar Layout
```razor
<Layout ShowCollapseBar="true" @bind-SideExpanded="sideExpanded" OnCollapsed="HandleSidebarCollapsed">
    <HeaderTemplate>
        <div class="d-flex align-items-center">
            <img src="_content/BootstrapBlazor.Shared/images/logo.png" width="36" height="36" alt="logo" />
            <div class="ms-2 text-white">BootstrapBlazor</div>
        </div>
    </HeaderTemplate>
    <SideBarTemplate>
        <div class="menu">
            <Menu Items="@Menus" IsVertical="true" />
        </div>
    </SideBarTemplate>
    <ChildContent>
        <div class="p-4">
            <h3>Collapsible Sidebar</h3>
            <p>Click the toggle button to collapse or expand the sidebar.</p>
            <p>Current sidebar state: @(sideExpanded ? "Expanded" : "Collapsed")</p>
        </div>
    </ChildContent>
    <FooterTemplate>
        <div class="text-center">© 2023 BootstrapBlazor. All rights reserved.</div>
    </FooterTemplate>
</Layout>

@code {
    private bool sideExpanded = true;
    
    private List<MenuItem> Menus { get; set; } = new List<MenuItem>
    {
        new MenuItem { Text = "Home", Icon = "fa-solid fa-home", Url = "/" },
        new MenuItem { Text = "About", Icon = "fa-solid fa-info-circle", Url = "/about" },
        new MenuItem { Text = "Contact", Icon = "fa-solid fa-envelope", Url = "/contact" }
    };
    
    private void HandleSidebarCollapsed(bool collapsed)
    {
        Console.WriteLine($"Sidebar collapsed: {collapsed}");
    }
}
```

### Example 4: Custom Sidebar Width and Colors
```razor
<Layout SideWidth="300" SideBarColor="Color.Primary">
    <HeaderTemplate>
        <div class="d-flex align-items-center">
            <img src="_content/BootstrapBlazor.Shared/images/logo.png" width="36" height="36" alt="logo" />
            <div class="ms-2 text-white">BootstrapBlazor</div>
        </div>
    </HeaderTemplate>
    <SideBarTemplate>
        <div class="menu">
            <Menu Items="@Menus" IsVertical="true" />
        </div>
    </SideBarTemplate>
    <ChildContent>
        <div class="p-4">
            <h3>Custom Sidebar Width and Colors</h3>
            <p>This layout has a custom sidebar width of 300px and uses the primary color theme.</p>
        </div>
    </ChildContent>
    <FooterTemplate>
        <div class="text-center">© 2023 BootstrapBlazor. All rights reserved.</div>
    </FooterTemplate>
</Layout>
```

### Example 5: Layout with Go to Top Button
```razor
<Layout ShowGotoTop="true" IsFixedHeader="true">
    <HeaderTemplate>
        <div class="d-flex align-items-center">
            <img src="_content/BootstrapBlazor.Shared/images/logo.png" width="36" height="36" alt="logo" />
            <div class="ms-2 text-white">BootstrapBlazor</div>
        </div>
    </HeaderTemplate>
    <SideBarTemplate>
        <div class="menu">
            <Menu Items="@Menus" IsVertical="true" />
        </div>
    </SideBarTemplate>
    <ChildContent>
        <div class="p-4">
            <h3>Layout with Go to Top Button</h3>
            <p>This layout includes a "Go to Top" button that appears when scrolling down.</p>
            
            <!-- Add enough content to demonstrate scrolling -->
            @for (int i = 1; i <= 20; i++)
            {
                <div class="card mb-3">
                    <div class="card-body">
                        <h5 class="card-title">Content Section @i</h5>
                        <p class="card-text">Scroll down to see the "Go to Top" button appear.</p>
                    </div>
                </div>
            }
        </div>
    </ChildContent>
    <FooterTemplate>
        <div class="text-center">© 2023 BootstrapBlazor. All rights reserved.</div>
    </FooterTemplate>
</Layout>
```

### Example 6: Nested Layouts
```razor
<Layout>
    <HeaderTemplate>
        <div class="d-flex align-items-center">
            <img src="_content/BootstrapBlazor.Shared/images/logo.png" width="36" height="36" alt="logo" />
            <div class="ms-2 text-white">BootstrapBlazor</div>
        </div>
    </HeaderTemplate>
    <SideBarTemplate>
        <div class="menu">
            <Menu Items="@Menus" IsVertical="true" />
        </div>
    </SideBarTemplate>
    <ChildContent>
        <!-- Nested Layout -->
        <Layout IsPage="true" ShowFooter="false">
            <HeaderTemplate>
                <div class="p-2 bg-light border-bottom">
                    <h4>Nested Layout Header</h4>
                </div>
            </HeaderTemplate>
            <ChildContent>
                <div class="p-4">
                    <h3>Nested Layout Content</h3>
                    <p>This is a nested layout within the main layout.</p>
                </div>
            </ChildContent>
        </Layout>
    </ChildContent>
    <FooterTemplate>
        <div class="text-center">© 2023 BootstrapBlazor. All rights reserved.</div>
    </FooterTemplate>
</Layout>
```

### Example 7: Layout with Tabs in Content Area
```razor
<Layout>
    <HeaderTemplate>
        <div class="d-flex align-items-center">
            <img src="_content/BootstrapBlazor.Shared/images/logo.png" width="36" height="36" alt="logo" />
            <div class="ms-2 text-white">BootstrapBlazor</div>
        </div>
    </HeaderTemplate>
    <SideBarTemplate>
        <div class="menu">
            <Menu Items="@Menus" IsVertical="true" />
        </div>
    </SideBarTemplate>
    <ChildContent>
        <div class="p-4">
            <h3>Layout with Tabs</h3>
            <p>This layout includes tabs in the content area for organizing content.</p>
            
            <Tab>
                <Items>
                    <TabItem Text="Dashboard">
                        <div class="p-3">
                            <h4>Dashboard Content</h4>
                            <p>This is the dashboard tab content.</p>
                        </div>
                    </TabItem>
                    <TabItem Text="Profile">
                        <div class="p-3">
                            <h4>Profile Content</h4>
                            <p>This is the profile tab content.</p>
                        </div>
                    </TabItem>
                    <TabItem Text="Settings">
                        <div class="p-3">
                            <h4>Settings Content</h4>
                            <p>This is the settings tab content.</p>
                        </div>
                    </TabItem>
                </Items>
            </Tab>
        </div>
    </ChildContent>
    <FooterTemplate>
        <div class="text-center">© 2023 BootstrapBlazor. All rights reserved.</div>
    </FooterTemplate>
</Layout>
```

## CSS Customization

The Layout component can be customized using CSS variables:

```css
/* Layout custom styling */
.layout {
  --bb-layout-header-height: 56px;
  --bb-layout-footer-height: 48px;
  --bb-layout-sidebar-width: 240px;
  --bb-layout-sidebar-collapsed-width: 80px;
  --bb-layout-transition: all 0.3s ease-in-out;
  
  /* Header variables */
  --bb-layout-header-background: #343a40;
  --bb-layout-header-color: #fff;
  --bb-layout-header-padding: 0 1rem;
  --bb-layout-header-border-bottom: 1px solid rgba(0, 0, 0, 0.1);
  
  /* Sidebar variables */
  --bb-layout-sidebar-background: #f8f9fa;
  --bb-layout-sidebar-color: #212529;
  --bb-layout-sidebar-padding: 1rem 0;
  --bb-layout-sidebar-border-right: 1px solid rgba(0, 0, 0, 0.1);
  
  /* Footer variables */
  --bb-layout-footer-background: #f8f9fa;
  --bb-layout-footer-color: #6c757d;
  --bb-layout-footer-padding: 0.75rem 1rem;
  --bb-layout-footer-border-top: 1px solid rgba(0, 0, 0, 0.1);
  
  /* Content variables */
  --bb-layout-content-background: #fff;
  --bb-layout-content-padding: 0;
}

/* Custom sidebar colors */
.layout-sidebar-primary {
  --bb-layout-sidebar-background: var(--bs-primary);
  --bb-layout-sidebar-color: #fff;
}

.layout-sidebar-dark {
  --bb-layout-sidebar-background: #343a40;
  --bb-layout-sidebar-color: #fff;
}
```

## Notes

### Responsive Design
- The Layout component is fully responsive and adapts to different screen sizes.
- On mobile devices, the sidebar is automatically collapsed and can be toggled with the collapse button.
- Use media queries to customize the layout for different screen sizes.

### Accessibility
- Ensure that the layout is accessible to all users, including those using screen readers.
- Use appropriate ARIA attributes for interactive elements like the sidebar toggle button.
- Maintain sufficient color contrast for text and background colors.

### Performance
- For large applications, consider using lazy loading for content to improve performance.
- Use the `IsPage` property for nested layouts to optimize rendering.

### Integration with Other Components
- The Layout component works seamlessly with other BootstrapBlazor components like Menu, Tab, and Button.
- Use the Menu component in the sidebar for navigation.
- Use the Tab component in the content area for organizing content.

### Best Practices
- Keep the layout consistent across different pages for a better user experience.
- Use fixed header and footer for better navigation in long pages.
- Use the sidebar for main navigation and the header for global actions.
- Consider using a collapsible sidebar for mobile devices and to maximize content space when needed.
- Use appropriate colors and spacing for different layout sections to create visual hierarchy.