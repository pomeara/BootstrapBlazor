# BootstrapBlazor Project Context for AI Models

This file provides context about the BootstrapBlazor library for AI models. It contains information about the project's architecture, components, conventions, and workflows to help AI models better understand and contribute to the codebase.

## Version Tracking

<!-- VERSION_INFO -->
Last documentation update: <!-- DATE_UPDATED -->
Commit hash: <!-- COMMIT_HASH -->

This section contains automatically updated information about the last Git commit that affected the documentation. When documentation is updated, these placeholders will be replaced with actual values to help track which version of the codebase the documentation reflects.

## Detailed Component Documentation

Detailed documentation for each component is available in individual Markdown files (`.md`) in the `docs\llm` folder. Each file is named according to the component (e.g., `llm_alert.md`, `llm_button.md`) and contains comprehensive information including:

- Component overview and purpose
- Key features and capabilities
- Properties and parameters
- Events and callbacks
- Multiple usage examples (3-7 examples per component)
- CSS customization options

Refer to these files for in-depth information about specific components.

For guidance on setting up new projects and installing necessary services, please refer to the [BootstrapBlazor Project Setup and Service Installation Guide](docs\llm/setup_guide.md).

## Recommended Documentation Format

For optimal readability and processing by LLMs, all new discovery documents should be created in **Markdown (.md)** format. Existing `.txt` files in the `docs\llm` folder should be progressively converted to `.md`.

## Project Overview

BootstrapBlazor is a Blazor UI component library based on Bootstrap and built for .NET Core. The project aims to provide a comprehensive set of UI components for Blazor applications, supporting both Blazor Server and Blazor WebAssembly.

- **GitHub Repository**: https://github.com/dotnetcore/BootstrapBlazor
- **Documentation**: https://www.blazor.zone/
- **NuGet Package**: https://www.nuget.org/packages/BootstrapBlazor/

## Architectural Structure

The project follows a component-based architecture:

```
BootstrapBlazor/
├── src/                    # Source code
│   ├── BootstrapBlazor/    # Main library project
│   ├── BootstrapBlazor.Server/  # Server components
│   ├── Extensions/         # Extension libraries
│   └── Web/                # Demo websites
├── test/                   # Test projects
├── docs/                   # Documentation
├── components/             # Component implementations
└── locales/                # Localization resources
```

## Core Technologies and Dependencies

- .NET Core / .NET 6+
- Blazor (Server and WebAssembly)
- Bootstrap CSS Framework
- JavaScript interop for enhanced functionality
- Font Awesome and other icon libraries

## Component List and Functionality

### Layout Components

1. **Layout**
   - Provides the main layout structure for applications
   - Supports side navigation, top navigation, and combinations
   - Configurable header, footer, sidebar, and content areas
   - Responsive design with collapsible sidebars

2. **Grid System**
   - Responsive grid system based on Bootstrap
   - Row and column components for layout
   - Configurable gutters and spacing
   - Breakpoint-specific column sizing

3. **Split**
   - Creates resizable split panels
   - Horizontal or vertical orientation
   - Configurable split sizes and constraints
   - Drag handle for user resizing

4. **Card**
   - Container component with header, body, and footer sections
   - Supports images, links, and various content
   - Collapsible and expandable options
   - Customizable borders and shadows

5. **Divider**
   - Creates horizontal or vertical separators
   - Customizable styles, colors, and thickness
   - Optional text content in the middle of divider
   - Various alignment options

6. **Tabs**
   - Tab navigation with content panes
   - Supports dynamic tab creation and removal
   - Customizable tab appearance and position
   - Events for tab activation and navigation

7. **Accordion**
   - Collapsible content panels
   - Single or multiple open items
   - Custom header templates
   - Animation support for expanding/collapsing

### Form Components

1. **AutoComplete**
   - Text input with suggestion dropdown
   - Client-side or server-side data source
   - Configurable filtering and matching
   - Custom item templates

2. **Checkbox**
   - Single checkbox or checkbox groups
   - Indeterminate state support
   - Custom label templates
   - Validation integration

3. **ColorPicker**
   - Visual color selection tool
   - RGB, HEX, and HSL color formats
   - Alpha channel support
   - Color presets and history

4. **DatePicker/DateTimePicker**
   - Date and time selection components
   - Various format options
   - Range selection capability
   - Localization support

5. **Editor**
   - Rich text editor integration
   - Toolbar customization
   - Image upload support
   - HTML content editing

6. **Form**
   - Form container with validation
   - Auto layout capabilities
   - Horizontal, vertical, and inline layouts
   - Built-in validation display

7. **Input**
   - Text input with various types
   - Addon support (prefix/suffix)
   - Clear button option
   - Size variants

8. **InputNumber**
   - Numeric input with formatting
   - Min/max constraints
   - Step increment/decrement
   - Precision control

9. **Mentions**
   - Text input with @mention functionality
   - Customizable triggers and templates
   - Data source integration
   - Selection events

10. **Radio**
    - Single radio buttons or radio groups
    - Custom label templates
    - Disabled state support
    - Inline or stacked layout

11. **Rate**
    - Star rating component
    - Custom icons support
    - Half-star precision
    - Read-only mode

12. **Select**
    - Dropdown selection component
    - Single or multiple selection
    - Search/filter capability
    - Custom rendering of options

13. **Slider**
    - Range slider with single or dual handles
    - Min/max/step configuration
    - Tooltip display
    - Vertical or horizontal orientation

14. **Switch**
    - Toggle switch component
    - Custom on/off text
    - Size variants
    - Loading state

15. **Transfer**
    - Dual list box for item transfer
    - Search/filter functionality
    - Batch operations
    - Custom item rendering

16. **Upload**
    - File upload component
    - Drag and drop support
    - Multiple file upload
    - File type filtering

17. **TimePicker**
    - Time selection component
    - 12/24-hour format
    - Minute/second precision
    - Custom time ranges

18. **Cascader**
    - Multi-level cascading selection
    - Dynamic loading support
    - Single or multiple selection
    - Custom rendering of options

### Data Display Components

1. **Avatar**
   - User or item avatar component
   - Image, icon, or text content
   - Size variants
   - Shape options (circle, square)

2. **Badge**
   - Numerical or status indicator
   - Standalone or attached to other components
   - Dot style option
   - Custom colors

3. **Carousel**
   - Image/content slideshow
   - Auto-play capability
   - Navigation controls
   - Transition effects

4. **Collapse**
   - Collapsible panels for content
   - Single or multiple panel expansion
   - Custom header/content templates
   - Animation support

5. **Description**
   - Structured description list
   - Horizontal or vertical layout
   - Responsive design
   - Customizable labels and content

6. **Empty**
   - Empty state placeholder
   - Custom image and description
   - Action buttons
   - Responsive sizing

7. **Image**
   - Enhanced image display
   - Lazy loading
   - Preview functionality
   - Fallback support

8. **List**
   - Structured data list component
   - Grid or vertical layout
   - Pagination integration
   - Custom item templates

9. **Popover**
   - Pop-up content container
   - Multiple trigger methods
   - Positioning options
   - Custom content templates

10. **QRCode**
    - QR code generation component
    - Customizable size and color
    - Error correction level
    - Logo overlay support

11. **Table**
    -
