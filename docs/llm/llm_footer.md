# Footer Component

## Overview

The Footer component in BootstrapBlazor provides a consistent way to add a footer section to your application. It's designed to display copyright information, links, or other content typically found at the bottom of a page. The component includes built-in support for a "Go to Top" button, making it easy for users to navigate back to the top of long pages. The Footer component is flexible and can be customized with either simple text or complex content through templates.

## Features

- **Simple Text Display**: Easily display copyright or other text information
- **Custom Content Support**: Add complex content through templates
- **Go to Top Integration**: Built-in support for a "Go to Top" button
- **Target Scrolling**: Configure which element the Go to Top button should target
- **Flexible Styling**: Customize appearance through CSS variables
- **Responsive Design**: Adapts to different screen sizes
- **Consistent Positioning**: Properly aligns at the bottom of content
- **Accessibility Support**: Proper semantic structure for screen readers
- **Theme Integration**: Automatically adapts to light/dark themes
- **Minimal Footprint**: Lightweight implementation with minimal overhead

## Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `Text` | `string` | `null` | The text to display in the footer when no custom content is provided. |
| `Target` | `string` | `null` | CSS selector for the element that the Go to Top button should scroll to the top of. Only applies when `ShowGoto` is true. |
| `ChildContent` | `RenderFragment` | `null` | Custom content to render within the footer. When provided, it replaces the default text display. |
| `ShowGoto` | `bool` | `true` | Whether to show the Go to Top button in the footer. |

## Events

The Footer component does not expose any specific events.

## Usage Examples

### Example 1: Basic Footer with Default Text

```razor
<Footer Text="© 2023 My Company. All rights reserved." />
```

This example shows a simple footer with copyright text and the default Go to Top button.

### Example 2: Footer with Custom Content

```razor
<Footer>
    <div class="d-flex justify-content-between align-items-center w-100">
        <div>© 2023 My Company</div>
        <div>
            <a href="/privacy" class="me-3">Privacy Policy</a>
            <a href="/terms" class="me-3">Terms of Service</a>
            <a href="/contact">Contact Us</a>
        </div>
    </div>
</Footer>
```

This example demonstrates a footer with custom content, including copyright text and links to important pages.

### Example 3: Footer without Go to Top Button

```razor
<Footer Text="© 2023 My Company. All rights reserved." ShowGoto="false" />
```

This example shows a footer with text but without the Go to Top button.

### Example 4: Footer with Custom Target for Go to Top

```razor
<div id="main-content" style="height: 2000px; overflow-y: auto;">
    <!-- Long scrollable content here -->
    
    <Footer Text="© 2023 My Company. All rights reserved." Target="#main-content" />
</div>
```

This example demonstrates how to set a specific target element for the Go to Top button, useful when the footer is inside a scrollable container rather than the main page.

### Example 5: Footer with Social Media Links

```razor
<Footer>
    <div class="d-flex flex-column flex-md-row justify-content-between align-items-center w-100">
        <div class="mb-3 mb-md-0">© 2023 My Company. All rights reserved.</div>
        <div class="d-flex">
            <a href="https://twitter.com/mycompany" class="me-3" target="_blank">
                <i class="fa-brands fa-twitter"></i>
            </a>
            <a href="https://facebook.com/mycompany" class="me-3" target="_blank">
                <i class="fa-brands fa-facebook"></i>
            </a>
            <a href="https://linkedin.com/company/mycompany" class="me-3" target="_blank">
                <i class="fa-brands fa-linkedin"></i>
            </a>
            <a href="https://github.com/mycompany" target="_blank">
                <i class="fa-brands fa-github"></i>
            </a>
        </div>
    </div>
</Footer>
```

This example shows a footer with social media links using Font Awesome icons.

### Example 6: Multi-Column Footer

```razor
<Footer ShowGoto="false">
    <div class="row w-100">
        <div class="col-12 col-md-4 mb-3">
            <h5>About Us</h5>
            <p>We are a company dedicated to providing high-quality products and services to our customers.</p>
        </div>
        <div class="col-12 col-md-4 mb-3">
            <h5>Quick Links</h5>
            <ul class="list-unstyled">
                <li><a href="/">Home</a></li>
                <li><a href="/products">Products</a></li>
                <li><a href="/services">Services</a></li>
                <li><a href="/about">About</a></li>
                <li><a href="/contact">Contact</a></li>
            </ul>
        </div>
        <div class="col-12 col-md-4">
            <h5>Contact Us</h5>
            <address>
                <strong>My Company, Inc.</strong><br>
                1234 Main Street<br>
                Anytown, USA 12345<br>
                <abbr title="Phone">P:</abbr> (123) 456-7890
            </address>
            <address>
                <strong>Email:</strong><br>
                <a href="mailto:info@mycompany.com">info@mycompany.com</a>
            </address>
        </div>
    </div>
    <div class="row w-100 mt-3 border-top pt-3">
        <div class="col-12 text-center">
            © 2023 My Company. All rights reserved.
        </div>
    </div>
</Footer>
```

This example demonstrates a multi-column footer with different sections for about, links, and contact information.

### Example 7: Footer in a Layout Component

```razor
@inherits LayoutComponentBase

<div class="page d-flex flex-column min-vh-100">
    <header>
        <NavMenu />
    </header>

    <main class="flex-grow-1">
        <div class="container mt-4">
            @Body
        </div>
    </main>

    <Footer Text="© 2023 My Application. Built with BootstrapBlazor." />
</div>
```

This example shows how to integrate the Footer component into a Blazor layout component, ensuring it stays at the bottom of the page.

## Customization Notes

### CSS Variables

The Footer component uses CSS variables for styling, which can be customized to match your application's theme:

```css
.footer {
    --bb-footer-bg: rgba(0, 0, 0, 0.05);    /* Background color */
    --bb-footer-padding: 1rem;                /* Padding around content */
}
```

You can override these variables in your CSS to customize the appearance of the Footer component according to your design requirements.

### Integration with Other Components

The Footer component works well with other BootstrapBlazor components:

1. **GoTop**: Already integrated for easy navigation back to the top of the page
2. **Layout**: Use within Layout components to ensure proper positioning
3. **Card**: Place inside Card footers for consistent styling
4. **Button**: Add buttons for important actions
5. **Icon**: Include icons for visual enhancement
6. **Link**: Add navigation links to other parts of your application

### Best Practices

1. **Keep it Simple**: Avoid overcrowding the footer with too much information
2. **Responsive Design**: Ensure the footer looks good on all device sizes
3. **Consistent Styling**: Maintain visual consistency with the rest of your application
4. **Accessibility**: Ensure all links and buttons are accessible
5. **Legal Requirements**: Include necessary legal information like copyright notices
6. **Navigation**: Consider adding important links that users might need
7. **Go to Top**: For long pages, keep the Go to Top button enabled for better user experience
8. **Custom Content**: Use the ChildContent parameter for complex layouts rather than simple text
9. **Proper Positioning**: Ensure the footer stays at the bottom of the page, even when content is short
10. **Dark Mode Support**: Test the footer in both light and dark themes