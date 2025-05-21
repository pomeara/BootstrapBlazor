# Collapse Component

## Overview
The Collapse component in BootstrapBlazor provides a way to toggle the visibility of content. It allows users to expand and collapse sections of content, making interfaces cleaner and more organized by hiding content until it's needed. This component is particularly useful for FAQs, accordion-style interfaces, sidebar menus, and any situation where you want to conserve screen space while still providing access to detailed information.

## Features
- Toggle content visibility with smooth animations
- Accordion-style grouping of collapsible items
- Customizable header with icon support
- Multiple collapse modes (hide or height-based)
- Programmatic control of collapse state
- Event callbacks for state changes
- Nested collapse support
- Customizable styling and theming
- Keyboard navigation and accessibility support
- Integration with other BootstrapBlazor components

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `Collapsed` | `bool` | `false` | Whether the content is initially collapsed |
| `CollapseMode` | `CollapseMode` | `CollapseMode.Hide` | The mode of collapse (Hide or Height) |
| `ShowBorder` | `bool` | `true` | Whether to show borders around the collapse component |
| `HeaderText` | `string` | `null` | Text to display in the header |
| `HeaderIcon` | `string` | `null` | Icon to display in the header |
| `HeaderTemplate` | `RenderFragment` | `null` | Custom template for the header content |
| `BodyTemplate` | `RenderFragment` | `null` | Custom template for the body content |
| `ShowHeader` | `bool` | `true` | Whether to display the header section |
| `IsAccordion` | `bool` | `false` | Whether the collapse is part of an accordion group |
| `AccordionGroup` | `string` | `null` | Identifier for the accordion group this collapse belongs to |
| `AnimationDuration` | `int` | `350` | Duration of the collapse/expand animation in milliseconds |
| `ChildContent` | `RenderFragment` | `null` | Content to be displayed inside the collapse body |

## Events

| Event | Description |
| --- | --- |
| `OnCollapsedChanged` | Triggered when the collapsed state changes |
| `OnCollapsing` | Triggered before the content begins collapsing |
| `OnCollapsed` | Triggered after the content has collapsed |
| `OnExpanding` | Triggered before the content begins expanding |
| `OnExpanded` | Triggered after the content has expanded |

## Usage Examples

### Example 1: Basic Collapse

```razor
<Collapse HeaderText="Click to expand/collapse">
    <p>This is the content that will be shown or hidden when the header is clicked.</p>
    <p>You can include any content here, including other components, text, images, etc.</p>
</Collapse>
```

This example shows a basic collapse component with a text header. Clicking the header will toggle the visibility of the content.

### Example 2: Collapse with Custom Header

```razor
<Collapse>
    <HeaderTemplate>
        <div class="d-flex align-items-center">
            <i class="fa fa-info-circle me-2"></i>
            <span>Important Information</span>
            <Badge Color="Color.Danger" Class="ms-2">New</Badge>
        </div>
    </HeaderTemplate>
    <BodyTemplate>
        <div class="p-3">
            <h5>Privacy Policy Update</h5>
            <p>We've updated our privacy policy to comply with new regulations.</p>
            <Button Color="Color.Primary" Text="Read More" />
        </div>
    </BodyTemplate>
</Collapse>
```

This example demonstrates a collapse with a custom header that includes an icon, text, and a badge. The body also uses a custom template with formatted content and a button.

### Example 3: Accordion Group

```razor
<div class="accordion">
    <Collapse HeaderText="Section 1" IsAccordion="true" AccordionGroup="faq">
        <p>Content for section 1. When this is expanded, other sections in the same accordion group will collapse.</p>
    </Collapse>
    
    <Collapse HeaderText="Section 2" IsAccordion="true" AccordionGroup="faq">
        <p>Content for section 2. This section will collapse when any other section in the same accordion group is expanded.</p>
    </Collapse>
    
    <Collapse HeaderText="Section 3" IsAccordion="true" AccordionGroup="faq">
        <p>Content for section 3. Only one section in an accordion group can be expanded at a time.</p>
    </Collapse>
</div>
```

This example shows how to create an accordion-style interface where only one section can be expanded at a time. All collapses share the same `AccordionGroup` identifier.

### Example 4: Programmatically Controlled Collapse

```razor
<Button Color="Color.Primary" OnClick="ToggleCollapse">Toggle Content</Button>

<div class="mt-3">
    <Collapse @bind-Collapsed="isCollapsed" HeaderText="Programmatically Controlled">
        <p>This collapse is controlled by the button above.</p>
    </Collapse>
</div>

@code {
    private bool isCollapsed = true;
    
    private void ToggleCollapse()
    {
        isCollapsed = !isCollapsed;
    }
}
```

This example demonstrates how to programmatically control the collapse state using a button and two-way binding.

### Example 5: Nested Collapses

```razor
<Collapse HeaderText="Main Section">
    <p>This is the main content.</p>
    
    <Collapse HeaderText="Subsection 1" Class="mt-3">
        <p>This is a nested collapse within the main section.</p>
    </Collapse>
    
    <Collapse HeaderText="Subsection 2" Class="mt-3">
        <p>This is another nested collapse within the main section.</p>
        
        <Collapse HeaderText="Sub-subsection" Class="mt-3">
            <p>You can nest collapses as deeply as needed.</p>
        </Collapse>
    </Collapse>
</Collapse>
```

This example shows how to nest collapse components within each other to create a hierarchical structure of collapsible content.

### Example 6: FAQ Section with Collapse

```razor
<div class="faq-section">
    <h3 class="mb-4">Frequently Asked Questions</h3>
    
    @foreach (var faq in faqs)
    {
        <Collapse HeaderText="@faq.Question" Class="mb-3">
            <div class="p-3">
                <p>@faq.Answer</p>
                @if (!string.IsNullOrEmpty(faq.Link))
                {
                    <a href="@faq.Link" target="_blank">Learn more</a>
                }
            </div>
        </Collapse>
    }
</div>

@code {
    private List<FaqItem> faqs = new List<FaqItem>
    {
        new FaqItem
        {
            Question = "How do I reset my password?",
            Answer = "You can reset your password by clicking on the 'Forgot Password' link on the login page and following the instructions sent to your email.",
            Link = "/help/password-reset"
        },
        new FaqItem
        {
            Question = "What payment methods do you accept?",
            Answer = "We accept credit cards (Visa, MasterCard, American Express), PayPal, and bank transfers for business accounts.",
            Link = "/payment-methods"
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
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Link { get; set; }
    }
}
```

This example demonstrates how to use the Collapse component to create a FAQ section with dynamically generated questions and answers.

### Example 7: Collapse with Animation Options

```razor
<div class="mb-3">
    <div class="form-check form-check-inline">
        <input class="form-check-input" type="radio" name="collapseMode" id="modeHide" checked @onchange="() => SetCollapseMode(CollapseMode.Hide)" />
        <label class="form-check-label" for="modeHide">Hide Mode</label>
    </div>
    <div class="form-check form-check-inline">
        <input class="form-check-input" type="radio" name="collapseMode" id="modeHeight" @onchange="() => SetCollapseMode(CollapseMode.Height)" />
        <label class="form-check-label" for="modeHeight">Height Mode</label>
    </div>
</div>

<div class="mb-3">
    <label for="animationDuration" class="form-label">Animation Duration (ms): @animationDuration</label>
    <input type="range" class="form-range" min="0" max="1000" step="50" id="animationDuration" @bind="animationDuration" />
</div>

<Collapse HeaderText="Animation Demo" 
         CollapseMode="@collapseMode"
         AnimationDuration="@animationDuration"
         OnCollapsing="() => LogEvent('Collapsing')"
         OnCollapsed="() => LogEvent('Collapsed')"
         OnExpanding="() => LogEvent('Expanding')"
         OnExpanded="() => LogEvent('Expanded')">
    <div style="height: 200px; padding: 20px;">
        <h5>Animation Test Content</h5>
        <p>This content will animate with the selected options.</p>
    </div>
</Collapse>

<div class="mt-3">
    <h6>Event Log:</h6>
    <ul class="list-group">
        @foreach (var log in eventLogs.AsEnumerable().Reverse())
        {
            <li class="list-group-item">@log</li>
        }
    </ul>
</div>

@code {
    private CollapseMode collapseMode = CollapseMode.Hide;
    private int animationDuration = 350;
    private List<string> eventLogs = new List<string>();
    
    private void SetCollapseMode(CollapseMode mode)
    {
        collapseMode = mode;
    }
    
    private void LogEvent(string eventName)
    {
        eventLogs.Add($"{DateTime.Now:HH:mm:ss.fff} - {eventName}");
        if (eventLogs.Count > 10)
        {
            eventLogs.RemoveAt(0);
        }
    }
}
```

This example shows how to customize the animation behavior of the Collapse component, allowing users to switch between collapse modes and adjust the animation duration. It also demonstrates how to use the various events to track the collapse/expand lifecycle.

## Customization

The Collapse component can be customized using CSS variables:

```css
.accordion {
    /* Button focus shadow */
    --bs-accordion-btn-focus-box-shadow: 0 0 0 0.25rem rgba(13, 110, 253, 0.25);
    
    /* Button padding */
    --bs-accordion-btn-padding-x: 1rem;
    --bs-accordion-btn-padding-y: 0.5rem;
    
    /* Active background color */
    --bs-accordion-active-bg: var(--bs-accordion-active-bg);
    
    /* Border radius */
    --bs-accordion-inner-border-radius: 0;
}
```

You can override these variables in your CSS to customize the appearance of the Collapse component according to your design requirements.

Additionally, you can customize the Collapse component by:

1. Using the `HeaderTemplate` and `BodyTemplate` properties to create custom layouts
2. Using the `CollapseMode` property to change the animation behavior
3. Using the `AnimationDuration` property to adjust the animation speed
4. Using the `ShowBorder` property to control the visibility of borders
5. Using the `IsAccordion` and `AccordionGroup` properties to create accordion-style interfaces
6. Applying custom CSS classes to the component using the `Class` property
7. Using the `HeaderIcon` property to add icons to the header
8. Nesting Collapse components to create hierarchical structures