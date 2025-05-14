# Stack Component

## Overview

The Stack component in BootstrapBlazor provides a simple and flexible way to vertically arrange elements with consistent spacing. It creates a one-dimensional layout that automatically places items in a column with predefined gaps between them. This component is particularly useful for creating forms, lists, and other vertically stacked content where maintaining consistent spacing is important. The Stack component simplifies layout management by eliminating the need to manually add margin or padding to individual elements.

## Features

- **Vertical Arrangement**: Automatically stacks elements vertically
- **Consistent Spacing**: Maintains uniform gaps between elements
- **Responsive Behavior**: Adapts to different screen sizes
- **Customizable Gaps**: Adjustable spacing between stacked elements
- **Alignment Control**: Options for horizontal alignment of stack items
- **Divider Support**: Optional dividers between stack items
- **Nested Stacks**: Support for nested stack components
- **Recursive Spacing**: Consistent spacing applied to all levels of nested content
- **Flexible Content**: Accepts any content including other components
- **Accessibility Support**: Proper semantic structure for screen readers

## Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `Gap` | `int` | `2` | The size of the gap between elements (in Bootstrap spacing units, where 1 = 0.25rem) |
| `HorizontalAlign` | `HorizontalAlign` | `HorizontalAlign.Start` | Horizontal alignment of items within the stack (Start, Center, End) |
| `Divider` | `bool` | `false` | Whether to show dividers between stack items |
| `DividerType` | `DividerType` | `DividerType.Solid` | The style of dividers when enabled (Solid, Dashed, Dotted) |
| `ChildContent` | `RenderFragment` | `null` | The content to be stacked |

## Events

The Stack component does not expose any specific events.

## Usage Examples

### Example 1: Basic Stack

```razor
<Stack>
    <Button Color="Color.Primary">Button 1</Button>
    <Button Color="Color.Secondary">Button 2</Button>
    <Button Color="Color.Success">Button 3</Button>
</Stack>
```

This example shows a basic stack with three buttons arranged vertically with default spacing.

### Example 2: Custom Gap Size

```razor
<Stack Gap="4">
    <Card>
        <BodyTemplate>
            <p>This is the first card in the stack.</p>
        </BodyTemplate>
    </Card>
    <Card>
        <BodyTemplate>
            <p>This is the second card in the stack.</p>
        </BodyTemplate>
    </Card>
    <Card>
        <BodyTemplate>
            <p>This is the third card in the stack.</p>
        </BodyTemplate>
    </Card>
</Stack>
```

This example demonstrates a stack of cards with larger spacing (1rem) between them.

### Example 3: Centered Alignment

```razor
<Stack HorizontalAlign="HorizontalAlign.Center">
    <Badge Color="Color.Primary">Badge 1</Badge>
    <Badge Color="Color.Secondary">Badge 2</Badge>
    <Badge Color="Color.Success">Badge 3</Badge>
</Stack>
```

This example shows badges stacked vertically and centered horizontally.

### Example 4: Stack with Dividers

```razor
<Stack Divider="true">
    <div class="p-3 bg-light">Item 1</div>
    <div class="p-3 bg-light">Item 2</div>
    <div class="p-3 bg-light">Item 3</div>
    <div class="p-3 bg-light">Item 4</div>
</Stack>
```

This example shows a stack with dividers between each item.

### Example 5: Form Layout with Stack

```razor
<Stack Gap="3">
    <div>
        <label for="name" class="form-label">Name</label>
        <input type="text" class="form-control" id="name" placeholder="Enter your name">
    </div>
    <div>
        <label for="email" class="form-label">Email</label>
        <input type="email" class="form-control" id="email" placeholder="Enter your email">
    </div>
    <div>
        <label for="message" class="form-label">Message</label>
        <textarea class="form-control" id="message" rows="3" placeholder="Enter your message"></textarea>
    </div>
    <div>
        <Button Color="Color.Primary">Submit</Button>
    </div>
</Stack>
```

This example demonstrates using Stack to create a simple form layout with consistent spacing.

### Example 6: Nested Stacks

```razor
<Stack Gap="4">
    <h3>Main Section</h3>
    <Stack Gap="2">
        <h5>Subsection 1</h5>
        <p>This is content for subsection 1.</p>
        <Stack Gap="1">
            <div class="p-2 bg-light">Nested item 1</div>
            <div class="p-2 bg-light">Nested item 2</div>
        </Stack>
    </Stack>
    <Stack Gap="2">
        <h5>Subsection 2</h5>
        <p>This is content for subsection 2.</p>
        <Stack Gap="1">
            <div class="p-2 bg-light">Nested item 1</div>
            <div class="p-2 bg-light">Nested item 2</div>
        </Stack>
    </Stack>
</Stack>
```

This example shows nested stacks with different gap sizes for hierarchical content.

### Example 7: Responsive Stack in a Card

```razor
<Card>
    <HeaderTemplate>
        <span>User Profile</span>
    </HeaderTemplate>
    <BodyTemplate>
        <Stack Gap="3">
            <div class="d-flex align-items-center">
                <Avatar Size="Size.Large" Icon="fa-solid fa-user" />
                <div class="ms-3">
                    <h5 class="mb-0">John Doe</h5>
                    <p class="text-muted mb-0">Software Developer</p>
                </div>
            </div>
            <Divider />
            <Stack Gap="2">
                <div class="d-flex justify-content-between">
                    <span>Email:</span>
                    <span>john.doe@example.com</span>
                </div>
                <div class="d-flex justify-content-between">
                    <span>Phone:</span>
                    <span>(123) 456-7890</span>
                </div>
                <div class="d-flex justify-content-between">
                    <span>Location:</span>
                    <span>New York, NY</span>
                </div>
            </Stack>
            <Divider />
            <div>
                <h6>Skills</h6>
                <div class="d-flex flex-wrap gap-2">
                    <Badge Color="Color.Primary">C#</Badge>
                    <Badge Color="Color.Secondary">Blazor</Badge>
                    <Badge Color="Color.Success">HTML/CSS</Badge>
                    <Badge Color="Color.Info">JavaScript</Badge>
                    <Badge Color="Color.Warning">SQL</Badge>
                </div>
            </div>
        </Stack>
    </BodyTemplate>
    <FooterTemplate>
        <Button Color="Color.Primary">Edit Profile</Button>
    </FooterTemplate>
</Card>
```

This example demonstrates a complex user profile card using Stack for layout organization.

## Customization Notes

### CSS Variables

The Stack component likely uses CSS variables for styling, which can be customized to match your application's theme:

```css
.bb-stack {
    --bb-stack-gap: 0.5rem;           /* Default gap size */
    --bb-stack-divider-color: var(--bs-border-color);  /* Divider color */
    --bb-stack-divider-style: solid;   /* Divider style */
    --bb-stack-divider-width: 1px;     /* Divider width */
}
```

You can override these variables in your CSS to customize the appearance of the Stack component according to your design requirements.

### Integration with Other Components

The Stack component works well with other BootstrapBlazor components:

1. **Card**: Use Stack within Card bodies for consistent content spacing
2. **Form**: Organize form fields with consistent spacing
3. **List**: Create lists with uniform spacing between items
4. **Tabs**: Organize content within tab panels
5. **Accordion**: Structure content within accordion sections
6. **Modal**: Organize modal content with proper spacing

### Best Practices

1. **Consistent Spacing**: Use the same gap size throughout your application for visual consistency
2. **Responsive Considerations**: Test stack layouts on different screen sizes
3. **Avoid Overuse**: Don't nest too many stacks as it can lead to excessive spacing
4. **Combine with Flexbox**: Use Stack for vertical arrangement and flexbox for horizontal layouts
5. **Semantic Structure**: Maintain proper heading levels and document structure
6. **Accessibility**: Ensure content within stacks is accessible to screen readers
7. **Performance**: For very long stacks, consider virtualization for better performance
8. **Dividers**: Use dividers sparingly to avoid visual clutter
9. **Gap Sizes**: Choose appropriate gap sizes based on content relationships (related content should have smaller gaps)
10. **Alignment**: Use horizontal alignment to create visual hierarchy and improve readability