# Print Component

## Overview

The Print component in BootstrapBlazor provides a convenient way to print specific content from a web page. It allows developers to define printable areas, customize print layouts, and trigger print operations programmatically without requiring users to use browser print functions manually.

## Features

- **Selective Printing**: Print specific sections of a page rather than the entire document
- **Print Triggers**: Multiple ways to trigger printing (button click, programmatic API)
- **Custom Print Styling**: Apply specific styles for printed output that differ from screen display
- **Print Preview**: Option to preview content before printing
- **Media Query Support**: Leverage CSS print media queries for fine-grained control
- **Print Callbacks**: Events for print start, success, and error handling
- **Multiple Print Targets**: Print content from different components on the same page
- **Responsive Print Layouts**: Adjust printed content based on paper size and orientation

## Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `ChildContent` | `RenderFragment` | null | The content to be printed |
| `PrintSelector` | `string` | null | CSS selector to identify printable content when not using ChildContent |
| `Title` | `string` | "Print Document" | Title for the print document/window |
| `ShowPrintButton` | `bool` | true | Whether to show the default print button |
| `ButtonText` | `string` | "Print" | Text to display on the print button |
| `ButtonIcon` | `string` | "fa-print" | Icon to display on the print button |
| `ButtonColor` | `Color` | `Color.Primary` | Color of the print button |
| `UseDefaultPrintStyles` | `bool` | true | Whether to use default print styles |
| `CustomCssUrl` | `string` | null | URL to a custom CSS file for print styling |
| `RemoveAfterPrint` | `bool` | false | Whether to remove the print frame after printing |
| `PreventDefault` | `bool` | true | Whether to prevent default browser print behavior |
| `Delay` | `int` | 250 | Delay in milliseconds before triggering print after preparation |
| `PrintInNewWindow` | `bool` | false | Whether to open a new window for printing |

## Events

| Event | Description |
|-------|-------------|
| `OnBeforePrint` | Triggered before the print operation starts |
| `OnAfterPrint` | Triggered after the print operation completes |
| `OnPrintError` | Triggered when an error occurs during printing |
| `OnPrintCancel` | Triggered when the user cancels the print operation |
| `OnPrintPreview` | Triggered when print preview is shown |

## Usage Examples

### Example 1: Basic Usage

```razor
@page "/print-demo"

<Print>
    <div class="print-content">
        <h2>Invoice #12345</h2>
        <p>Date: @DateTime.Now.ToShortDateString()</p>
        <table class="table">
            <thead>
                <tr>
                    <th>Item</th>
                    <th>Quantity</th>
                    <th>Price</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>Product A</td>
                    <td>2</td>
                    <td>$10.00</td>
                </tr>
                <tr>
                    <td>Product B</td>
                    <td>1</td>
                    <td>$25.00</td>
                </tr>
            </tbody>
            <tfoot>
                <tr>
                    <td colspan="2">Total:</td>
                    <td>$45.00</td>
                </tr>
            </tfoot>
        </table>
    </div>
</Print>
```

### Example 2: Custom Print Button

```razor
<Print ShowPrintButton="false" @ref="printRef">
    <div class="report-container">
        <h3>Monthly Report</h3>
        <p>This report contains sensitive information.</p>
        <div class="chart-container">
            <!-- Chart content here -->
        </div>
        <div class="data-table">
            <!-- Table content here -->
        </div>
    </div>
</Print>

<Button Color="Color.Success" Icon="fa-print" Text="Print Report" OnClick="PrintReport" />

@code {
    private Print printRef;

    private async Task PrintReport()
    {
        await printRef.PrintAsync();
    }
}
```

### Example 3: Print with Custom Styling

```razor
<style>
    /* Styles for screen display */
    .report-card {
        border: 1px solid #ddd;
        padding: 15px;
        margin-bottom: 20px;
    }
    
    /* Styles for print only */
    @media print {
        .report-card {
            break-inside: avoid;
            border: none;
            padding: 0;
        }
        
        .no-print {
            display: none !important;
        }
        
        body {
            font-size: 12pt;
        }
    }
</style>

<div class="no-print">
    <h4>Preview Mode</h4>
    <p>Click the print button to generate a printer-friendly version.</p>
</div>

<Print UseDefaultPrintStyles="false">
    <div class="container">
        @foreach (var report in Reports)
        {
            <div class="report-card">
                <h4>@report.Title</h4>
                <p>@report.Description</p>
                <div class="metrics">
                    <!-- Report metrics -->
                </div>
            </div>
        }
    </div>
</Print>

@code {
    private List<ReportModel> Reports = new();
    
    protected override void OnInitialized()
    {
        // Initialize reports
        Reports.Add(new ReportModel { Title = "Q1 Performance", Description = "First quarter results" });
        Reports.Add(new ReportModel { Title = "Q2 Performance", Description = "Second quarter results" });
    }
    
    public class ReportModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
```

### Example 4: Print External Content by Selector

```razor
<div id="printable-area">
    <h3>Customer Information</h3>
    <div class="customer-details">
        <p><strong>Name:</strong> John Doe</p>
        <p><strong>ID:</strong> CUST-12345</p>
        <p><strong>Email:</strong> john.doe@example.com</p>
        <p><strong>Membership:</strong> Premium</p>
    </div>
</div>

<Print PrintSelector="#printable-area" Title="Customer Details" ButtonText="Print Customer Info" />
```

### Example 5: Print with Callbacks

```razor
<Print OnBeforePrint="HandleBeforePrint" 
      OnAfterPrint="HandleAfterPrint"
      OnPrintError="HandlePrintError">
    <div class="printable-content">
        <h3>Transaction Receipt</h3>
        <p>Transaction ID: @TransactionId</p>
        <p>Amount: $@Amount.ToString("F2")</p>
        <p>Date: @TransactionDate.ToLocalTime().ToString()</p>
        <p>Status: @Status</p>
    </div>
</Print>

<div class="print-status">@PrintStatus</div>

@code {
    private string TransactionId = "TXN-987654";
    private decimal Amount = 125.75m;
    private DateTime TransactionDate = DateTime.UtcNow;
    private string Status = "Completed";
    private string PrintStatus = "";
    
    private void HandleBeforePrint()
    {
        PrintStatus = "Preparing document for printing...";
        // Log print attempt
    }
    
    private void HandleAfterPrint()
    {
        PrintStatus = "Document sent to printer successfully.";
        // Update print count in database
    }
    
    private void HandlePrintError(string errorMessage)
    {
        PrintStatus = $"Print error: {errorMessage}";
        // Log error
    }
}
```

### Example 6: Print in New Window

```razor
<Print PrintInNewWindow="true" Title="Detailed Report">
    <div class="report-content">
        <h2>Detailed Analysis Report</h2>
        <p class="report-date">Generated on: @DateTime.Now.ToString("yyyy-MM-dd HH:mm")</p>
        
        <div class="section">
            <h3>Executive Summary</h3>
            <p>This report provides a comprehensive analysis of quarterly performance...</p>
        </div>
        
        <div class="section">
            <h3>Financial Metrics</h3>
            <!-- Financial data tables and charts -->
        </div>
        
        <div class="section">
            <h3>Operational Insights</h3>
            <!-- Operational data -->
        </div>
        
        <div class="section">
            <h3>Recommendations</h3>
            <ul>
                <li>Increase investment in Product A</li>
                <li>Optimize supply chain for Region B</li>
                <li>Expand marketing efforts in Segment C</li>
            </ul>
        </div>
    </div>
</Print>
```

### Example 7: Multiple Print Areas

```razor
<div class="print-controls">
    <h3>Document Sections</h3>
    <p>Select which sections to print:</p>
</div>

<Print @ref="summaryPrint" ShowPrintButton="false">
    <div class="summary-section">
        <h2>Executive Summary</h2>
        <p>This document summarizes the key findings and recommendations...</p>
        <!-- Summary content -->
    </div>
</Print>

<Print @ref="financialPrint" ShowPrintButton="false">
    <div class="financial-section">
        <h2>Financial Analysis</h2>
        <!-- Financial content -->
    </div>
</Print>

<Print @ref="operationalPrint" ShowPrintButton="false">
    <div class="operational-section">
        <h2>Operational Review</h2>
        <!-- Operational content -->
    </div>
</Print>

<div class="print-buttons">
    <Button Text="Print Summary" OnClick="() => PrintSection(summaryPrint)" />
    <Button Text="Print Financial Analysis" OnClick="() => PrintSection(financialPrint)" />
    <Button Text="Print Operational Review" OnClick="() => PrintSection(operationalPrint)" />
    <Button Text="Print All" OnClick="PrintAll" />
</div>

@code {
    private Print summaryPrint;
    private Print financialPrint;
    private Print operationalPrint;
    
    private async Task PrintSection(Print section)
    {
        await section.PrintAsync();
    }
    
    private async Task PrintAll()
    {
        // Print each section with a delay between them
        await summaryPrint.PrintAsync();
        await Task.Delay(1000);
        await financialPrint.PrintAsync();
        await Task.Delay(1000);
        await operationalPrint.PrintAsync();
    }
}
```

## CSS Customization

The Print component can be customized using CSS, particularly with print-specific media queries:

```css
/* Custom styles for printed output */
@media print {
    /* Hide non-printable elements */
    .no-print, .no-print * {
        display: none !important;
    }
    
    /* Remove backgrounds and shadows to save ink */
    body, .card, .container {
        background: none !important;
        box-shadow: none !important;
    }
    
    /* Ensure text is black for better printing */
    body {
        color: #000 !important;
    }
    
    /* Add page breaks where needed */
    .page-break {
        page-break-after: always;
    }
    
    /* Prevent elements from being split across pages */
    .keep-together {
        break-inside: avoid;
    }
    
    /* Ensure links show their URL when printed */
    a:after {
        content: " (" attr(href) ")";
    }
}
```

## JavaScript Interop

The Print component uses JavaScript interop to handle browser printing functionality. You can also interact with the component programmatically:

```csharp
// Reference to the Print component
@inject IJSRuntime JSRuntime

// In your component
private Print printComponent;

// Trigger printing programmatically
private async Task TriggerPrint()
{
    await printComponent.PrintAsync();
}

// Get print status
private async Task<bool> IsPrinting()
{
    return await printComponent.IsPrintingAsync();
}
```

## Accessibility

The Print component follows accessibility best practices:

1. Print buttons include proper ARIA labels for screen readers
2. Print dialogs maintain keyboard navigation support
3. Printed content preserves semantic HTML structure for assistive technologies
4. High contrast is maintained in printed output for readability

## Browser Compatibility

The Print component is compatible with all modern browsers, but behavior may vary slightly between browsers due to differences in print implementations. Always test printing in your target browsers.

## Integration with Other Components

The Print component works well with other BootstrapBlazor components:

- **Table**: Print formatted table data
- **Chart**: Print visualizations with proper styling
- **Card**: Print card-based layouts
- **Form**: Print form data in a formatted layout
- **Tabs**: Print specific tab content

When integrating with data components, consider using the `OnBeforePrint` event to ensure data is fully loaded before printing begins.