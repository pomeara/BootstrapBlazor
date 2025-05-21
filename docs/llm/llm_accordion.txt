# Accordion Component

## Overview

The Accordion component in BootstrapBlazor provides a way to organize content into collapsible sections, where only one section can be expanded at a time. It helps conserve screen space and improves content organization by hiding content until it's needed. This component is particularly useful for FAQs, navigation menus, settings panels, and any interface where you want to present multiple content sections in a limited space.

In BootstrapBlazor, the Accordion functionality is implemented through the Collapse component with its `IsAccordion` property set to `true`.

## Features

- **Single-Section Expansion**: Only one section can be expanded at a time
- **Smooth Animations**: Content expands and collapses with smooth transitions
- **Customizable Headers**: Support for text, icons, and custom templates in section headers
- **Event Callbacks**: Notifications when sections expand or collapse
- **Nested Content Support**: Ability to include complex content and other components within sections
- **Programmatic Control**: Methods to expand or collapse sections through code
- **Styling Options**: Customizable appearance through CSS variables
- **Accessibility Support**: Keyboard navigation and proper ARIA attributes
- **Dynamic Content**: Support for dynamically generated accordion items
- **Responsive Design**: Adapts to different screen sizes

## Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `IsAccordion` | `bool` | `true` | Enables accordion behavior (only one section open at a time) |
| `CollapseItems` | `RenderFragment` | `null` | Template for defining accordion items |
| `OnCollapseChanged` | `Func<CollapseItem, Task>` | `null` | Callback when an item expands or collapses |

### CollapseItem Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `Text` | `string` | `null` | Text to display in the header |
| `Icon` | `string` | `null` | CSS class for an icon to display in the header |
| `IsCollapsed` | `bool` | `true` | Whether the item is initially collapsed |
| `TitleColor` | `Color` | `Color.None` | Background color for the header |
| `HeaderClass` | `string` | `null` | Additional CSS class for the header |
| `Class` | `string` | `null` | Additional CSS class for the item |
| `HeaderTemplate` | `RenderFragment` | `null` | Custom template for the header |
| `ChildContent` | `RenderFragment` | `null` | Content to display when the item is expanded |

## Usage Examples

### Example 1: Basic Accordion

```razor
<Collapse IsAccordion="true">
    <CollapseItems>
        <CollapseItem Text="Section 1" IsCollapsed="false">
            <p>This is the content for section 1. It's initially expanded.</p>
            <p>Only one section can be expanded at a time in accordion mode.</p>
        </CollapseItem>
        
        <CollapseItem Text="Section 2">
            <p>This is the content for section 2.</p>
            <p>When you expand this section, the other sections will automatically collapse.</p>
        </CollapseItem>
        
        <CollapseItem Text="Section 3">
            <p>This is the content for section 3.</p>
            <p>The accordion ensures only one section is visible at a time.</p>
        </CollapseItem>
    </CollapseItems>
</Collapse>
```

This example shows a basic accordion with three sections. Only one section can be expanded at a time.

### Example 2: Accordion with Icons

```razor
<Collapse IsAccordion="true">
    <CollapseItems>
        <CollapseItem Text="Personal Information" Icon="fa-solid fa-user">
            <div class="p-3">
                <p>Edit your personal details here.</p>
                <div class="mb-3">
                    <label class="form-label">Full Name</label>
                    <input type="text" class="form-control" placeholder="Enter your name" />
                </div>
                <div class="mb-3">
                    <label class="form-label">Email Address</label>
                    <input type="email" class="form-control" placeholder="Enter your email" />
                </div>
            </div>
        </CollapseItem>
        
        <CollapseItem Text="Security Settings" Icon="fa-solid fa-shield-alt">
            <div class="p-3">
                <p>Manage your account security settings.</p>
                <div class="mb-3">
                    <label class="form-label">Password</label>
                    <input type="password" class="form-control" placeholder="Enter new password" />
                </div>
                <div class="mb-3">
                    <label class="form-label">Two-Factor Authentication</label>
                    <div class="form-check form-switch">
                        <input class="form-check-input" type="checkbox" id="twoFactorAuth" />
                        <label class="form-check-label" for="twoFactorAuth">Enable</label>
                    </div>
                </div>
            </div>
        </CollapseItem>
        
        <CollapseItem Text="Notification Preferences" Icon="fa-solid fa-bell">
            <div class="p-3">
                <p>Control how you receive notifications.</p>
                <div class="form-check mb-2">
                    <input class="form-check-input" type="checkbox" id="emailNotif" checked />
                    <label class="form-check-label" for="emailNotif">Email Notifications</label>
                </div>
                <div class="form-check mb-2">
                    <input class="form-check-input" type="checkbox" id="pushNotif" />
                    <label class="form-check-label" for="pushNotif">Push Notifications</label>
                </div>
            </div>
        </CollapseItem>
    </CollapseItems>
</Collapse>
```

This example demonstrates an accordion with icons in the headers, used for a settings panel interface.

### Example 3: Accordion with Custom Header Templates

```razor
<Collapse IsAccordion="true">
    <CollapseItems>
        <CollapseItem>
            <HeaderTemplate>
                <div class="d-flex align-items-center justify-content-between w-100">
                    <div>
                        <i class="fa-solid fa-credit-card me-2"></i>
                        <span>Payment Methods</span>
                    </div>
                    <Badge Color="Color.Success">Active</Badge>
                </div>
            </HeaderTemplate>
            <p>Manage your payment methods here.</p>
            <Button Color="Color.Primary" Text="Add Payment Method" />
        </CollapseItem>
        
        <CollapseItem>
            <HeaderTemplate>
                <div class="d-flex align-items-center justify-content-between w-100">
                    <div>
                        <i class="fa-solid fa-truck me-2"></i>
                        <span>Shipping Addresses</span>
                    </div>
                    <Badge Color="Color.Info">3 Saved</Badge>
                </div>
            </HeaderTemplate>
            <p>Manage your shipping addresses here.</p>
            <Button Color="Color.Primary" Text="Add Address" />
        </CollapseItem>
    </CollapseItems>
</Collapse>
```

This example shows an accordion with custom header templates that include badges and custom layouts.

### Example 4: Programmatically Controlled Accordion

```razor
<div class="mb-3">
    <Button Color="Color.Primary" Text="Open Section 1" OnClick="() => OpenSection(0)" />
    <Button Color="Color.Secondary" Text="Open Section 2" OnClick="() => OpenSection(1)" />
    <Button Color="Color.Success" Text="Open Section 3" OnClick="() => OpenSection(2)" />
</div>

<Collapse IsAccordion="true" @ref="accordion">
    <CollapseItems>
        <CollapseItem @ref="section1" Text="Section 1">
            <p>This is section 1 content.</p>
        </CollapseItem>
        
        <CollapseItem @ref="section2" Text="Section 2">
            <p>This is section 2 content.</p>
        </CollapseItem>
        
        <CollapseItem @ref="section3" Text="Section 3">
            <p>This is section 3 content.</p>
        </CollapseItem>
    </CollapseItems>
</Collapse>

@code {
    private Collapse? accordion;
    private CollapseItem? section1;
    private CollapseItem? section2;
    private CollapseItem? section3;
    
    private List<CollapseItem?> Sections => new() { section1, section2, section3 };
    
    private void OpenSection(int index)
    {
        if (index >= 0 && index < Sections.Count)
        {
            var section = Sections[index];
            if (section != null)
            {
                // Close all sections
                foreach (var s in Sections)
                {
                    if (s != null && s != section && !s.IsCollapsed)
                    {
                        s.SetCollapsed(true);
                    }
                }
                
                // Open the selected section
                section.SetCollapsed(false);
            }
        }
    }
}
```

This example demonstrates how to programmatically control which accordion section is open using buttons.

### Example 5: FAQ Accordion

```razor
<Collapse IsAccordion="true">
    <CollapseItems>
        @foreach (var faq in faqs)
        {
            <CollapseItem Text="@faq.Question">
                <div class="p-3">
                    <p>@faq.Answer</p>
                    @if (!string.IsNullOrEmpty(faq.Link))
                    {
                        <a href="@faq.Link" class="btn btn-link p-0">Learn more</a>
                    }
                </div>
            </CollapseItem>
        }
    </CollapseItems>
</Collapse>

@code {
    private List<FaqItem> faqs = new()
    {
        new FaqItem
        {
            Question = "How do I create an account?",
            Answer = "To create an account, click the 'Sign Up' button in the top-right corner of the homepage and follow the instructions.",
            Link = "/signup"
        },
        new FaqItem
        {
            Question = "What payment methods do you accept?",
            Answer = "We accept Visa, Mastercard, American Express, and PayPal for all purchases.",
            Link = "/payment"
        },
        new FaqItem
        {
            Question = "How can I track my order?",
            Answer = "You can track your order by logging into your account and visiting the 'Order History' section.",
            Link = "/orders"
        },
        new FaqItem
        {
            Question = "What is your return policy?",
            Answer = "We offer a 30-day return policy for all unused items in their original packaging.",
            Link = "/returns"
        },
        new FaqItem
        {
            Question = "How can I contact customer support?",
            Answer = "Our customer support team is available 24/7 via live chat, email at support@example.com, or by phone at +1-800-123-4567.",
            Link = "/contact"
        }
    };
    
    private class FaqItem
    {
        public string? Question { get; set; }
        public string? Answer { get; set; }
        public string? Link { get; set; }
    }
}
```

This example shows how to create a dynamic FAQ section using an accordion with data from a collection.

### Example 6: Nested Accordions

```razor
<Collapse IsAccordion="true">
    <CollapseItems>
        <CollapseItem Text="Products" Icon="fa-solid fa-box">
            <div class="p-3">
                <Collapse IsAccordion="true">
                    <CollapseItems>
                        <CollapseItem Text="Electronics" Icon="fa-solid fa-laptop">
                            <ul class="list-group list-group-flush">
                                <li class="list-group-item">Laptops</li>
                                <li class="list-group-item">Smartphones</li>
                                <li class="list-group-item">Tablets</li>
                                <li class="list-group-item">Accessories</li>
                            </ul>
                        </CollapseItem>
                        
                        <CollapseItem Text="Clothing" Icon="fa-solid fa-tshirt">
                            <ul class="list-group list-group-flush">
                                <li class="list-group-item">Men's</li>
                                <li class="list-group-item">Women's</li>
                                <li class="list-group-item">Children's</li>
                            </ul>
                        </CollapseItem>
                    </CollapseItems>
                </Collapse>
            </div>
        </CollapseItem>
        
        <CollapseItem Text="Services" Icon="fa-solid fa-cogs">
            <div class="p-3">
                <Collapse IsAccordion="true">
                    <CollapseItems>
                        <CollapseItem Text="Consulting" Icon="fa-solid fa-comments">
                            <p>Our consulting services help businesses optimize their operations.</p>
                        </CollapseItem>
                        
                        <CollapseItem Text="Training" Icon="fa-solid fa-graduation-cap">
                            <p>We offer comprehensive training programs for individuals and teams.</p>
                        </CollapseItem>
                    </CollapseItems>
                </Collapse>
            </div>
        </CollapseItem>
    </CollapseItems>
</Collapse>
```

This example demonstrates nested accordions for creating hierarchical navigation menus.

### Example 7: Styled Accordion with Color Variants

```razor
<Collapse IsAccordion="true">
    <CollapseItems>
        <CollapseItem Text="Primary Section" TitleColor="Color.Primary">
            <div class="p-3 bg-primary bg-opacity-10">
                <p>This section has a primary-colored header.</p>
            </div>
        </CollapseItem>
        
        <CollapseItem Text="Success Section" TitleColor="Color.Success">
            <div class="p-3 bg-success bg-opacity-10">
                <p>This section has a success-colored header.</p>
            </div>
        </CollapseItem>
        
        <CollapseItem Text="Danger Section" TitleColor="Color.Danger">
            <div class="p-3 bg-danger bg-opacity-10">
                <p>This section has a danger-colored header.</p>
            </div>
        </CollapseItem>
        
        <CollapseItem Text="Warning Section" TitleColor="Color.Warning">
            <div class="p-3 bg-warning bg-opacity-10">
                <p>This section has a warning-colored header.</p>
            </div>
        </CollapseItem>
        
        <CollapseItem Text="Info Section" TitleColor="Color.Info">
            <div class="p-3 bg-info bg-opacity-10">
                <p>This section has an info-colored header.</p>
            </div>
        </CollapseItem>
    </CollapseItems>
</Collapse>
```

This example shows an accordion with different color variants for the headers, matching the content styling.

## Customization Notes

### CSS Variables

The Accordion component (implemented through Collapse) can be customized using CSS variables:

```css
.accordion {
    /* Button focus shadow */
    --bs-accordion-btn-focus-box-shadow: 0 0 0 0.25rem rgba(13, 110, 253, 0.25);
    
    /* Button padding */
    --bs-accordion-btn-padding-x: 1rem;
    --bs-accordion-btn-padding-y: 0.5rem;
    
    /* Active background color */
    --bs-accordion-active-bg: rgba(0, 0, 0, 0.03);
    
    /* Border radius */
    --bs-accordion-inner-border-radius: 0;
}
```

You can override these variables in your CSS to customize the appearance of the Accordion component according to your design requirements.

### Integration with Other Components

The Accordion component works well with other BootstrapBlazor components:

1. **Card**: Place the Accordion inside a Card for additional styling and structure
2. **Tabs**: Combine Tabs and Accordion for multi-level navigation
3. **Form**: Use Accordion to organize form sections
4. **Badge**: Add badges to accordion headers to show counts or status
5. **Button**: Include buttons within accordion content for actions
6. **Alert**: Place alerts inside accordion sections for important messages

### Best Practices

1. **Content Organization**: Group related content together in accordion sections
2. **Header Clarity**: Use clear, concise headers that describe the content
3. **Icon Usage**: Add icons to headers to provide visual cues about the content
4. **Default State**: Consider which section should be expanded by default (if any)
5. **Nesting**: Avoid excessive nesting of accordions to prevent confusion
6. **Accessibility**: Ensure accordion content is accessible to screen readers
7. **Performance**: For large content, consider lazy loading or virtualization
8. **Mobile Considerations**: Test accordion behavior on small screens
9. **Animation Speed**: Adjust animation duration for smoother transitions
10. **Event Handling**: Use event callbacks to respond to user interactions