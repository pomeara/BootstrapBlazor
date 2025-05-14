# Timeline Component

## Overview
The Timeline component in BootstrapBlazor provides a visual representation of a sequence of events in chronological order. It displays a series of items along a vertical or horizontal line, making it ideal for showcasing histories, processes, roadmaps, or any sequence of events that occur over time. The component offers various layout options, customizable nodes, and content formatting to effectively communicate temporal information in an intuitive and visually appealing manner.

## Features
- **Vertical Layout**: Traditional top-to-bottom timeline display
- **Alternate Layout**: Alternating left-right display of timeline items
- **Left Layout**: Items aligned to the right with the timeline on the left
- **Customizable Nodes**: Support for different node styles and icons
- **Connecting Lines**: Visual lines connecting timeline events
- **Timestamps**: Optional timestamp display for each event
- **Rich Content Support**: Ability to include text, images, and other components in timeline items
- **Custom Node Colors**: Color customization for timeline nodes
- **Responsive Design**: Adapts to different screen sizes
- **Accessibility Support**: Proper semantic structure for screen readers
- **Item Templates**: Customizable templates for timeline items
- **Interactive Elements**: Support for clickable timeline items
- **Animation Support**: Optional animations for timeline items
- **Reverse Chronology**: Option to display events in reverse chronological order

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `Items` | `IEnumerable<TItem>` | `null` | Collection of items to display in the timeline |
| `Mode` | `TimelineMode` | `TimelineMode.Left` | Layout mode of the timeline (Left, Alternate, Right) |
| `Reverse` | `bool` | `false` | Whether to display items in reverse chronological order |
| `ShowTimestamp` | `bool` | `true` | Whether to show timestamps for timeline items |
| `TimestampFormat` | `string` | `null` | Format string for displaying timestamps |
| `TimestampProperty` | `string` | `"Timestamp"` | Property name to use for item timestamps |
| `ContentProperty` | `string` | `"Content"` | Property name to use for item content |
| `TitleProperty` | `string` | `"Title"` | Property name to use for item titles |
| `IconProperty` | `string` | `"Icon"` | Property name to use for item icons |
| `ColorProperty` | `string` | `"Color"` | Property name to use for item colors |
| `NodeTemplate` | `RenderFragment<TItem>` | `null` | Custom template for timeline nodes |
| `ItemTemplate` | `RenderFragment<TItem>` | `null` | Custom template for timeline items |
| `TimestampTemplate` | `RenderFragment<TItem>` | `null` | Custom template for timestamps |
| `ShowLine` | `bool` | `true` | Whether to show connecting lines between nodes |
| `LineWidth` | `string` | `"2px"` | Width of the timeline connecting line |
| `NodeSize` | `string` | `"12px"` | Size of timeline nodes |

## Events

| Event | Description |
| --- | --- |
| `OnItemClick` | Triggered when a timeline item is clicked |
| `OnNodeClick` | Triggered when a timeline node is clicked |

## Usage Examples

### Example 1: Basic Timeline

```razor
<Timeline>
    <TimelineItem Timestamp="2023-01-15" Title="Project Kickoff">
        <p>Initial project planning and team formation completed.</p>
    </TimelineItem>
    
    <TimelineItem Timestamp="2023-02-10" Title="Design Phase">
        <p>UI/UX design and architecture planning finalized.</p>
    </TimelineItem>
    
    <TimelineItem Timestamp="2023-03-20" Title="Development">
        <p>Core functionality implemented and unit tests created.</p>
    </TimelineItem>
    
    <TimelineItem Timestamp="2023-04-15" Title="Testing">
        <p>QA testing completed with minor issues identified.</p>
    </TimelineItem>
    
    <TimelineItem Timestamp="2023-05-01" Title="Launch">
        <p>Product successfully launched to production.</p>
    </TimelineItem>
</Timeline>
```

This example shows a basic vertical timeline with five events, each with a timestamp, title, and description.

### Example 2: Timeline with Alternate Layout and Icons

```razor
<Timeline Mode="TimelineMode.Alternate">
    <TimelineItem Timestamp="2023-01-01" Title="Idea Generation" Icon="fa fa-lightbulb" Color="#FFD700">
        <p>Initial concept and market research completed.</p>
    </TimelineItem>
    
    <TimelineItem Timestamp="2023-02-15" Title="Funding Secured" Icon="fa fa-money-bill" Color="#32CD32">
        <p>Venture capital funding of $2M secured for development.</p>
    </TimelineItem>
    
    <TimelineItem Timestamp="2023-03-30" Title="Team Expansion" Icon="fa fa-users" Color="#4169E1">
        <p>Development team expanded to 12 members.</p>
    </TimelineItem>
    
    <TimelineItem Timestamp="2023-05-10" Title="Beta Release" Icon="fa fa-rocket" Color="#FF6347">
        <p>Beta version released to 100 test users.</p>
    </TimelineItem>
    
    <TimelineItem Timestamp="2023-06-20" Title="Public Launch" Icon="fa fa-flag-checkered" Color="#9932CC">
        <p>Official product launch with marketing campaign.</p>
    </TimelineItem>
</Timeline>
```

This example demonstrates a timeline with alternating left-right layout and custom icons and colors for each node.

### Example 3: Timeline with Custom Templates

```razor
<Timeline>
    <ItemTemplate>
        <div class="custom-timeline-item">
            <h4>@context.Title</h4>
            <div class="timeline-content">@context.Description</div>
            <div class="timeline-metrics">
                <span><i class="fa fa-user"></i> @context.AssignedTo</span>
                <span><i class="fa fa-calendar"></i> @context.DueDate.ToString("MMM dd, yyyy")</span>
            </div>
        </div>
    </ItemTemplate>
    
    <NodeTemplate>
        <div class="custom-node @GetStatusClass(context.Status)">
            <i class="@GetStatusIcon(context.Status)"></i>
        </div>
    </NodeTemplate>
    
    <TimestampTemplate>
        <div class="custom-timestamp">
            <div class="date">@context.Timestamp.ToString("MMM dd")</div>
            <div class="year">@context.Timestamp.Year</div>
        </div>
    </TimestampTemplate>
</Timeline>

@code {
    private List<ProjectMilestone> milestones = new List<ProjectMilestone>
    {
        new ProjectMilestone
        {
            Title = "Requirements Gathering",
            Description = "Collect and document all project requirements",
            Timestamp = new DateTime(2023, 1, 10),
            AssignedTo = "John Smith",
            DueDate = new DateTime(2023, 1, 20),
            Status = "Completed"
        },
        new ProjectMilestone
        {
            Title = "Design Phase",
            Description = "Create wireframes and design documents",
            Timestamp = new DateTime(2023, 2, 5),
            AssignedTo = "Jane Doe",
            DueDate = new DateTime(2023, 2, 25),
            Status = "Completed"
        },
        new ProjectMilestone
        {
            Title = "Development Sprint 1",
            Description = "Implement core functionality",
            Timestamp = new DateTime(2023, 3, 1),
            AssignedTo = "Dev Team",
            DueDate = new DateTime(2023, 3, 15),
            Status = "InProgress"
        },
        new ProjectMilestone
        {
            Title = "Testing",
            Description = "Perform QA testing and fix issues",
            Timestamp = new DateTime(2023, 3, 20),
            AssignedTo = "QA Team",
            DueDate = new DateTime(2023, 4, 5),
            Status = "Pending"
        }
    };
    
    private string GetStatusClass(string status)
    {
        return status switch
        {
            "Completed" => "status-completed",
            "InProgress" => "status-in-progress",
            "Pending" => "status-pending",
            _ => ""
        };
    }
    
    private string GetStatusIcon(string status)
    {
        return status switch
        {
            "Completed" => "fa fa-check",
            "InProgress" => "fa fa-spinner fa-spin",
            "Pending" => "fa fa-clock",
            _ => "fa fa-circle"
        };
    }
    
    public class ProjectMilestone
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Timestamp { get; set; }
        public string AssignedTo { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
    }
}

<style>
    .custom-timeline-item {
        padding: 1rem;
        border-radius: 0.5rem;
        background-color: #f8f9fa;
        box-shadow: 0 2px 4px rgba(0, 0, 0, 0.05);
    }
    
    .timeline-content {
        margin: 0.5rem 0;
    }
    
    .timeline-metrics {
        display: flex;
        justify-content: space-between;
        font-size: 0.875rem;
        color: #6c757d;
    }
    
    .custom-node {
        display: flex;
        align-items: center;
        justify-content: center;
        width: 24px;
        height: 24px;
        border-radius: 50%;
    }
    
    .status-completed {
        background-color: #28a745;
        color: white;
    }
    
    .status-in-progress {
        background-color: #007bff;
        color: white;
    }
    
    .status-pending {
        background-color: #ffc107;
        color: white;
    }
    
    .custom-timestamp {
        display: flex;
        flex-direction: column;
        align-items: center;
    }
    
    .date {
        font-weight: bold;
    }
    
    .year {
        font-size: 0.875rem;
        color: #6c757d;
    }
</style>
```

This example shows a timeline with custom templates for items, nodes, and timestamps, creating a project milestone tracker with status indicators.

### Example 4: Interactive Timeline with Events

```razor
<Timeline Items="@historyEvents"
          TimestampProperty="Date"
          TitleProperty="Title"
          ContentProperty="Description"
          OnItemClick="HandleItemClick">
</Timeline>

<Modal @ref="detailsModal">
    <ModalHeader>
        <ModalTitle>@selectedEvent?.Title</ModalTitle>
    </ModalHeader>
    <ModalBody>
        @if (selectedEvent != null)
        {
            <div>
                <p><strong>Date:</strong> @selectedEvent.Date.ToString("MMMM d, yyyy")</p>
                <p><strong>Location:</strong> @selectedEvent.Location</p>
                <p>@selectedEvent.Description</p>
                @if (!string.IsNullOrEmpty(selectedEvent.ImageUrl))
                {
                    <img src="@selectedEvent.ImageUrl" alt="@selectedEvent.Title" class="img-fluid mt-3" />
                }
            </div>
        }
    </ModalBody>
    <ModalFooter>
        <Button Color="Color.Secondary" OnClick="() => detailsModal.Hide()">Close</Button>
    </ModalFooter>
</Modal>

@code {
    private List<HistoricalEvent> historyEvents = new List<HistoricalEvent>
    {
        new HistoricalEvent
        {
            Title = "Declaration of Independence",
            Date = new DateTime(1776, 7, 4),
            Location = "Philadelphia, Pennsylvania",
            Description = "The United States Declaration of Independence was adopted by the Second Continental Congress.",
            ImageUrl = "/images/declaration.jpg"
        },
        new HistoricalEvent
        {
            Title = "Constitution Signed",
            Date = new DateTime(1787, 9, 17),
            Location = "Philadelphia, Pennsylvania",
            Description = "The Constitution of the United States was signed by 39 delegates.",
            ImageUrl = "/images/constitution.jpg"
        },
        new HistoricalEvent
        {
            Title = "Louisiana Purchase",
            Date = new DateTime(1803, 4, 30),
            Location = "Paris, France",
            Description = "The United States purchased the Louisiana Territory from France, doubling the size of the country.",
            ImageUrl = "/images/louisiana.jpg"
        },
        new HistoricalEvent
        {
            Title = "Civil War Begins",
            Date = new DateTime(1861, 4, 12),
            Location = "Fort Sumter, South Carolina",
            Description = "The American Civil War began with the Battle of Fort Sumter.",
            ImageUrl = "/images/civilwar.jpg"
        },
        new HistoricalEvent
        {
            Title = "World War II Ends",
            Date = new DateTime(1945, 9, 2),
            Location = "Tokyo Bay, Japan",
            Description = "Japan formally surrendered, ending World War II.",
            ImageUrl = "/images/wwii.jpg"
        }
    };
    
    private Modal detailsModal;
    private HistoricalEvent selectedEvent;
    
    private void HandleItemClick(HistoricalEvent histEvent)
    {
        selectedEvent = histEvent;
        detailsModal.Show();
    }
    
    public class HistoricalEvent
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
```

This example demonstrates an interactive timeline of historical events that shows a modal with detailed information when a timeline item is clicked.

### Example 5: Timeline with Reverse Chronology

```razor
<Timeline Reverse="true">
    <TimelineItem Timestamp="2023-06-01" Title="Current Status">
        <p>Project is now in maintenance mode with regular updates.</p>
    </TimelineItem>
    
    <TimelineItem Timestamp="2023-05-15" Title="Version 2.0 Release">
        <p>Major update with new features and performance improvements.</p>
    </TimelineItem>
    
    <TimelineItem Timestamp="2023-04-10" Title="Beta Testing">
        <p>Beta testing with selected users to gather feedback.</p>
    </TimelineItem>
    
    <TimelineItem Timestamp="2023-03-05" Title="Development">
        <p>Core development of version 2.0 features.</p>
    </TimelineItem>
    
    <TimelineItem Timestamp="2023-02-01" Title="Planning">
        <p>Planning and requirements gathering for version 2.0.</p>
    </TimelineItem>
</Timeline>
```

This example shows a timeline in reverse chronological order, displaying the most recent events first.

### Example 6: Timeline with Custom Styling

```razor
<Timeline Class="custom-timeline" LineWidth="3px" NodeSize="16px">
    <TimelineItem Timestamp="2023-01-10" Title="Phase 1" Color="#8E44AD">
        <p>Initial research and planning.</p>
    </TimelineItem>
    
    <TimelineItem Timestamp="2023-02-15" Title="Phase 2" Color="#2980B9">
        <p>Design and prototyping.</p>
    </TimelineItem>
    
    <TimelineItem Timestamp="2023-03-20" Title="Phase 3" Color="#27AE60">
        <p>Development and testing.</p>
    </TimelineItem>
    
    <TimelineItem Timestamp="2023-04-25" Title="Phase 4" Color="#F39C12">
        <p>Deployment and user training.</p>
    </TimelineItem>
    
    <TimelineItem Timestamp="2023-05-30" Title="Phase 5" Color="#E74C3C">
        <p>Evaluation and refinement.</p>
    </TimelineItem>
</Timeline>

<style>
    .custom-timeline {
        --bb-timeline-item-padding: 2rem 0;
        --bb-timeline-node-bg: white;
        --bb-timeline-timestamp-color: #6c757d;
    }
    
    .custom-timeline .timeline-item-wrapper {
        background-color: #f8f9fa;
        border-radius: 8px;
        padding: 1rem;
        box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
        transition: transform 0.3s ease;
    }
    
    .custom-timeline .timeline-item-wrapper:hover {
        transform: translateY(-5px);
    }
</style>
```

This example demonstrates a timeline with custom styling using CSS variables and additional styles for hover effects and visual enhancements.

### Example 7: Responsive Timeline for Mobile

```razor
@inject IJSRuntime JSRuntime

<div class="timeline-container">
    <div class="d-flex justify-content-end mb-3">
        <Button Color="Color.Secondary" OnClick="ToggleMode">
            <i class="@(isAlternateMode ? "fa fa-align-left" : "fa fa-exchange-alt")"></i>
            @(isAlternateMode ? "Switch to Vertical" : "Switch to Alternate")
        </Button>
    </div>
    
    <Timeline Mode="@currentMode" Items="@roadmapItems">
        <ItemTemplate>
            <div class="card">
                <div class="card-body">
                    <h5 class="card-title">@context.Title</h5>
                    <h6 class="card-subtitle mb-2 text-muted">@context.Timestamp.ToString("MMMM yyyy")</h6>
                    <p class="card-text">@context.Description</p>
                    @if (context.Features != null && context.Features.Any())
                    {
                        <div class="mt-2">
                            <strong>Key Features:</strong>
                            <ul class="mt-1">
                                @foreach (var feature in context.Features)
                                {
                                    <li>@feature</li>
                                }
                            </ul>
                        </div>
                    }
                </div>
            </div>
        </ItemTemplate>
    </Timeline>
</div>

@code {
    private bool isAlternateMode = false;
    private TimelineMode currentMode = TimelineMode.Left;
    private List<RoadmapItem> roadmapItems = new List<RoadmapItem>();
    
    protected override void OnInitialized()
    {
        // Initialize roadmap items
        roadmapItems = new List<RoadmapItem>
        {
            new RoadmapItem
            {
                Title = "Q1 2023 Release",
                Timestamp = new DateTime(2023, 1, 1),
                Description = "Initial platform launch with core functionality.",
                Features = new List<string>
                {
                    "User authentication",
                    "Basic dashboard",
                    "Data import/export"
                }
            },
            new RoadmapItem
            {
                Title = "Q2 2023 Release",
                Timestamp = new DateTime(2023, 4, 1),
                Description = "Enhanced reporting and analytics features.",
                Features = new List<string>
                {
                    "Custom reports",
                    "Data visualization",
                    "Scheduled exports"
                }
            },
            new RoadmapItem
            {
                Title = "Q3 2023 Release",
                Timestamp = new DateTime(2023, 7, 1),
                Description = "Mobile app launch and API improvements.",
                Features = new List<string>
                {
                    "iOS and Android apps",
                    "API v2 with enhanced security",
                    "Offline capabilities"
                }
            },
            new RoadmapItem
            {
                Title = "Q4 2023 Release",
                Timestamp = new DateTime(2023, 10, 1),
                Description = "Enterprise features and integration capabilities.",
                Features = new List<string>
                {
                    "SSO integration",
                    "Advanced permissions",
                    "Third-party integrations"
                }
            }
        };
    }
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            // Check screen size and set appropriate mode
            var isMobile = await JSRuntime.InvokeAsync<bool>("isScreenMobile");
            if (isMobile)
            {
                currentMode = TimelineMode.Left;
                isAlternateMode = false;
                StateHasChanged();
            }
        }
    }
    
    private void ToggleMode()
    {
        isAlternateMode = !isAlternateMode;
        currentMode = isAlternateMode ? TimelineMode.Alternate : TimelineMode.Left;
    }
    
    public class RoadmapItem
    {
        public string Title { get; set; }
        public DateTime Timestamp { get; set; }
        public string Description { get; set; }
        public List<string> Features { get; set; }
    }
}

<style>
    .timeline-container {
        max-width: 1200px;
        margin: 0 auto;
    }
    
    @media (max-width: 768px) {
        .timeline-container .card {
            max-width: 100%;
        }
    }
</style>
```

This example shows a responsive timeline that adapts to different screen sizes, with a toggle button to switch between vertical and alternate layouts.

## Customization Notes

### CSS Variables

The Timeline component can be customized using CSS variables:

```css
:root {
    --bb-timeline-item-padding: 0 0 20px 0;
    --bb-timeline-line-width: 2px;
    --bb-timeline-node-bg: #fff;
    --bb-timeline-timestamp-color: #6c757d;
}
```

### Layout Modes

The Timeline component supports three layout modes:

1. **Left Mode**: The default mode where the timeline is on the left and content is on the right
2. **Alternate Mode**: Content alternates between left and right sides of the timeline
3. **Right Mode**: Timeline is on the right and content is on the left

### Node Customization

Timeline nodes can be customized in several ways:

1. **Color**: Set the `Color` property on TimelineItem to change the node color
2. **Icon**: Use the `Icon` property to display an icon inside the node
3. **Custom Template**: Use the `NodeTemplate` to completely customize the node appearance

### Responsive Design

For better mobile experience:

1. Consider using the Left mode on small screens, even if using Alternate mode on larger screens
2. Adjust padding and spacing using CSS variables for different screen sizes
3. Use media queries to modify the timeline appearance on mobile devices

### Integration with Other Components

The Timeline component works well with:

1. **Card Component**: For styled timeline item content
2. **Modal Component**: For displaying detailed information about timeline items
3. **Icon Component**: For visual indicators in timeline nodes
4. **Button Component**: For interactive elements within timeline items
5. **Image Component**: For visual content in timeline items