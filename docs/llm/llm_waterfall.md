# Waterfall Component

## Overview
The Waterfall component in BootstrapBlazor is a layout component that arranges content in a cascading, masonry-like grid. It automatically positions elements in optimal positions based on available vertical space, creating a dynamic, Pinterest-style layout that efficiently uses screen real estate.

## Features
- **Dynamic Layout**: Automatically arranges items in a masonry-style grid layout
- **Responsive Design**: Adapts to different screen sizes and orientations
- **Column Configuration**: Customizable number of columns based on viewport size
- **Gap Control**: Adjustable spacing between items
- **Item Sizing**: Support for items of varying heights and widths
- **Lazy Loading**: Optional integration with lazy loading for improved performance
- **Animation Support**: Smooth transitions when items are added or removed

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `Items` | `IEnumerable<TItem>` | `null` | Collection of items to display in the waterfall layout |
| `ItemTemplate` | `RenderFragment<TItem>` | `null` | Template for rendering each item in the collection |
| `EmptyTemplate` | `RenderFragment` | `null` | Template to display when the Items collection is empty |
| `Columns` | `int` | `4` | Number of columns in the waterfall layout |
| `ColumnWidth` | `int?` | `null` | Fixed width for each column (in pixels) |
| `Gap` | `int` | `16` | Space between items (in pixels) |
| `ResizeDebounceTime` | `int` | `200` | Debounce time in milliseconds for handling resize events |
| `LoadingTemplate` | `RenderFragment` | `null` | Template to display while items are loading |
| `EnableLazyLoad` | `bool` | `false` | Enables lazy loading of items as they enter the viewport |
| `AnimationDuration` | `int` | `300` | Duration of transition animations in milliseconds |
| `AnimationType` | `AnimationType` | `AnimationType.Fade` | Type of animation to use when items appear |

## Events

| Event | Description |
| --- | --- |
| `OnItemsChanged` | Triggered when the Items collection changes |
| `OnLayoutComplete` | Triggered when the layout calculation is complete |
| `OnItemClick` | Triggered when an item in the waterfall is clicked |
| `OnScroll` | Triggered when the user scrolls the waterfall component |
| `OnResize` | Triggered when the component is resized |
| `OnItemsRendered` | Triggered after all items have been rendered |
| `OnLazyLoadTrigger` | Triggered when lazy loading should fetch more items |

## Usage Examples

### Example 1: Basic Waterfall Layout

```razor
@page "/waterfall-demo"

<Waterfall Items="@_images" Columns="3" Gap="10">
    <ItemTemplate Context="item">
        <div class="waterfall-item">
            <img src="@item.Url" alt="@item.Description" style="width: 100%;" />
            <div class="item-description">@item.Description</div>
        </div>
    </ItemTemplate>
</Waterfall>

@code {
    private List<ImageItem> _images = new List<ImageItem>();
    
    protected override void OnInitialized()
    {
        // Populate with sample images
        for (int i = 1; i <= 20; i++)
        {
            _images.Add(new ImageItem
            {
                Url = $"/images/sample-{i}.jpg",
                Description = $"Image {i} description"
            });
        }
    }
    
    class ImageItem
    {
        public string Url { get; set; }
        public string Description { get; set; }
    }
}
```

### Example 2: Responsive Waterfall with Dynamic Column Count

```razor
<Waterfall Items="@_cards" 
          Columns="@GetColumnCount()" 
          Gap="16">
    <ItemTemplate Context="card">
        <div class="card">
            <div class="card-header">@card.Title</div>
            <div class="card-body" style="height: @(card.Height)px">
                @card.Content
            </div>
            <div class="card-footer">@card.Footer</div>
        </div>
    </ItemTemplate>
</Waterfall>

@code {
    private List<CardItem> _cards = new List<CardItem>();
    private Random _random = new Random();
    
    protected override void OnInitialized()
    {
        // Create cards with random heights
        for (int i = 1; i <= 30; i++)
        {
            _cards.Add(new CardItem
            {
                Title = $"Card {i}",
                Content = $"This is the content for card {i}",
                Footer = $"Footer {i}",
                Height = _random.Next(100, 300) // Random heights
            });
        }
    }
    
    private int GetColumnCount()
    {
        // Adjust columns based on viewport width
        var width = JSRuntime.InvokeAsync<int>("getViewportWidth").Result;
        if (width < 576) return 1;      // xs
        if (width < 768) return 2;      // sm
        if (width < 992) return 3;      // md
        if (width < 1200) return 4;     // lg
        return 5;                       // xl
    }
    
    class CardItem
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Footer { get; set; }
        public int Height { get; set; }
    }
}
```

### Example 3: Lazy Loading with Waterfall

```razor
<Waterfall Items="@_loadedItems" 
          Columns="4" 
          Gap="20"
          EnableLazyLoad="true"
          OnLazyLoadTrigger="LoadMoreItems">
    <ItemTemplate Context="item">
        <div class="product-card">
            <img src="@item.ImageUrl" alt="@item.Name" />
            <h3>@item.Name</h3>
            <p>@item.Description</p>
            <span class="price">$@item.Price.ToString("F2")</span>
        </div>
    </ItemTemplate>
    <LoadingTemplate>
        <div class="loading-indicator">
            <Spinner Type="SpinnerType.Border" />
            <span>Loading more items...</span>
        </div>
    </LoadingTemplate>
</Waterfall>

@code {
    private List<ProductItem> _allItems = new List<ProductItem>();
    private List<ProductItem> _loadedItems = new List<ProductItem>();
    private int _currentPage = 0;
    private int _itemsPerPage = 12;
    private bool _hasMoreItems = true;
    
    protected override void OnInitialized()
    {
        // Initialize with sample product data
        for (int i = 1; i <= 100; i++)
        {
            _allItems.Add(new ProductItem
            {
                Id = i,
                Name = $"Product {i}",
                Description = $"This is a description for product {i}",
                Price = 10.99m + (i % 10),
                ImageUrl = $"/images/products/product-{i}.jpg"
            });
        }
        
        // Load initial batch
        LoadMoreItems();
    }
    
    private async Task LoadMoreItems()
    {
        if (!_hasMoreItems) return;
        
        // Simulate network delay
        await Task.Delay(800);
        
        var nextItems = _allItems
            .Skip(_currentPage * _itemsPerPage)
            .Take(_itemsPerPage)
            .ToList();
            
        if (nextItems.Any())
        {
            _loadedItems.AddRange(nextItems);
            _currentPage++;
            
            // Check if we've reached the end
            if (_currentPage * _itemsPerPage >= _allItems.Count)
            {
                _hasMoreItems = false;
            }
            
            StateHasChanged();
        }
        else
        {
            _hasMoreItems = false;
        }
    }
    
    class ProductItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
    }
}
```

### Example 4: Animated Waterfall with Filtering

```razor
<div class="filter-buttons">
    <Button OnClick="() => FilterItems(null)">All</Button>
    <Button OnClick="() => FilterItems(\"Category1\")">Category 1</Button>
    <Button OnClick="() => FilterItems(\"Category2\")">Category 2</Button>
    <Button OnClick="() => FilterItems(\"Category3\")">Category 3</Button>
</div>

<Waterfall Items="@_filteredItems" 
          Columns="3" 
          Gap="15"
          AnimationType="AnimationType.FadeInUp"
          AnimationDuration="500">
    <ItemTemplate Context="item">
        <div class="gallery-item">
            <img src="@item.ImageUrl" alt="@item.Title" />
            <div class="overlay">
                <h4>@item.Title</h4>
                <span class="category">@item.Category</span>
            </div>
        </div>
    </ItemTemplate>
    <EmptyTemplate>
        <div class="no-items-found">
            <Icon Name="fa-search" />
            <h3>No items found</h3>
            <p>Try selecting a different category</p>
        </div>
    </EmptyTemplate>
</Waterfall>

@code {
    private List<GalleryItem> _allItems = new List<GalleryItem>();
    private List<GalleryItem> _filteredItems = new List<GalleryItem>();
    
    protected override void OnInitialized()
    {
        // Initialize gallery items
        _allItems = new List<GalleryItem>
        {
            new GalleryItem { Id = 1, Title = "Mountain Landscape", Category = "Category1", ImageUrl = "/images/gallery/landscape1.jpg" },
            new GalleryItem { Id = 2, Title = "Urban Architecture", Category = "Category2", ImageUrl = "/images/gallery/urban1.jpg" },
            new GalleryItem { Id = 3, Title = "Abstract Art", Category = "Category3", ImageUrl = "/images/gallery/abstract1.jpg" },
            new GalleryItem { Id = 4, Title = "Ocean View", Category = "Category1", ImageUrl = "/images/gallery/landscape2.jpg" },
            new GalleryItem { Id = 5, Title = "City Skyline", Category = "Category2", ImageUrl = "/images/gallery/urban2.jpg" },
            new GalleryItem { Id = 6, Title = "Modern Design", Category = "Category3", ImageUrl = "/images/gallery/abstract2.jpg" },
            // Add more items as needed
        };
        
        // Initially show all items
        _filteredItems = _allItems.ToList();
    }
    
    private void FilterItems(string category)
    {
        if (string.IsNullOrEmpty(category))
        {
            _filteredItems = _allItems.ToList();
        }
        else
        {
            _filteredItems = _allItems.Where(i => i.Category == category).ToList();
        }
    }
    
    class GalleryItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string ImageUrl { get; set; }
    }
}
```

### Example 5: Waterfall with Custom Item Sizing

```razor
<Waterfall Items="@_tiles" Columns="4" Gap="8">
    <ItemTemplate Context="tile">
        <div class="tile @GetTileClass(tile.Size)" 
             style="background-color: @tile.Color">
            <h3>@tile.Title</h3>
            <p>@tile.Content</p>
        </div>
    </ItemTemplate>
</Waterfall>

<style>
    .tile {
        padding: 15px;
        border-radius: 4px;
        color: white;
    }
    .tile-small {
        height: 150px;
    }
    .tile-medium {
        height: 250px;
    }
    .tile-large {
        height: 350px;
    }
</style>

@code {
    private List<TileItem> _tiles = new List<TileItem>();
    private Random _random = new Random();
    private string[] _colors = new[] { "#3498db", "#2ecc71", "#e74c3c", "#f39c12", "#9b59b6", "#1abc9c" };
    
    protected override void OnInitialized()
    {
        // Create tiles with different sizes
        for (int i = 1; i <= 24; i++)
        {
            var size = (TileSize)(_random.Next(0, 3)); // Random size
            _tiles.Add(new TileItem
            {
                Id = i,
                Title = $"Tile {i}",
                Content = $"This is content for tile {i}",
                Size = size,
                Color = _colors[_random.Next(0, _colors.Length)]
            });
        }
    }
    
    private string GetTileClass(TileSize size)
    {
        return size switch
        {
            TileSize.Small => "tile-small",
            TileSize.Medium => "tile-medium",
            TileSize.Large => "tile-large",
            _ => "tile-small"
        };
    }
    
    class TileItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public TileSize Size { get; set; }
        public string Color { get; set; }
    }
    
    enum TileSize
    {
        Small,
        Medium,
        Large
    }
}
```

### Example 6: Waterfall with Drag and Drop Reordering

```razor
<Waterfall Items="@_notes" 
          Columns="3" 
          Gap="12"
          OnItemsRendered="InitializeDragDrop">
    <ItemTemplate Context="note">
        <div class="note-card" data-id="@note.Id">
            <div class="note-header">
                <h4>@note.Title</h4>
                <Button Circle="true" Size="Size.Small" OnClick="() => RemoveNote(note)">
                    <i class="fa fa-times"></i>
                </Button>
            </div>
            <div class="note-content">@note.Content</div>
            <div class="note-footer">@note.CreatedDate.ToShortDateString()</div>
        </div>
    </ItemTemplate>
</Waterfall>

<Button Icon="fa-plus" Text="Add Note" OnClick="AddNewNote" />

@code {
    private List<NoteItem> _notes = new List<NoteItem>();
    private int _nextId = 1;
    
    protected override void OnInitialized()
    {
        // Sample notes
        _notes = new List<NoteItem>
        {
            new NoteItem { Id = _nextId++, Title = "Shopping List", Content = "Milk, Eggs, Bread, Cheese", CreatedDate = DateTime.Now.AddDays(-2) },
            new NoteItem { Id = _nextId++, Title = "Meeting Notes", Content = "Discuss project timeline and resource allocation", CreatedDate = DateTime.Now.AddDays(-1) },
            new NoteItem { Id = _nextId++, Title = "Ideas", Content = "New feature: implement drag and drop for notes", CreatedDate = DateTime.Now },
            // Add more notes
        };
    }
    
    private void AddNewNote()
    {
        var newNote = new NoteItem
        {
            Id = _nextId++,
            Title = $"New Note {_nextId}",
            Content = "Click to edit this note",
            CreatedDate = DateTime.Now
        };
        
        _notes.Add(newNote);
    }
    
    private void RemoveNote(NoteItem note)
    {
        _notes.Remove(note);
    }
    
    private async Task InitializeDragDrop()
    {
        // Initialize drag and drop functionality via JS interop
        await JSRuntime.InvokeVoidAsync("initNoteDragDrop", DotNetObjectReference.Create(this));
    }
    
    [JSInvokable]
    public void UpdateNoteOrder(int[] noteIds)
    {
        var reorderedNotes = new List<NoteItem>();
        foreach (var id in noteIds)
        {
            var note = _notes.FirstOrDefault(n => n.Id == id);
            if (note != null)
            {
                reorderedNotes.Add(note);
            }
        }
        
        _notes = reorderedNotes;
        StateHasChanged();
    }
    
    class NoteItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
```

### Example 7: Waterfall with Infinite Scroll and Data Loading

```razor
<Waterfall Items="@_articles" 
          Columns="@_columnCount" 
          Gap="20"
          EnableLazyLoad="true"
          OnLazyLoadTrigger="LoadMoreArticles"
          OnResize="HandleResize">
    <ItemTemplate Context="article">
        <div class="article-card">
            <img src="@article.ImageUrl" alt="@article.Title" class="article-image" />
            <div class="article-content">
                <span class="article-category">@article.Category</span>
                <h3 class="article-title">@article.Title</h3>
                <p class="article-excerpt">@article.Excerpt</p>
                <div class="article-meta">
                    <span class="article-date">@article.PublishedDate.ToShortDateString()</span>
                    <span class="article-author">By @article.Author</span>
                </div>
                <Button Text="Read More" OnClick="() => NavigateToArticle(article.Id)" />
            </div>
        </div>
    </ItemTemplate>
    <LoadingTemplate>
        <div class="loading-container">
            <Spinner />
            <p>Loading more articles...</p>
        </div>
    </LoadingTemplate>
    <EmptyTemplate>
        <div class="no-articles">
            <Icon Name="fa-newspaper" Size="IconSize.Large" />
            <h3>No Articles Found</h3>
            <p>Check back later for new content</p>
        </div>
    </EmptyTemplate>
</Waterfall>

@code {
    private List<ArticleItem> _articles = new List<ArticleItem>();
    private int _page = 1;
    private bool _isLoading = false;
    private bool _hasMoreArticles = true;
    private int _columnCount = 3;
    
    protected override async Task OnInitializedAsync()
    {
        await LoadMoreArticles();
    }
    
    private async Task LoadMoreArticles()
    {
        if (_isLoading || !_hasMoreArticles) return;
        
        _isLoading = true;
        
        try
        {
            // Simulate API call to fetch articles
            await Task.Delay(1000); // Simulate network delay
            
            var newArticles = await FetchArticlesFromApi(_page, 10);
            
            if (newArticles.Count > 0)
            {
                _articles.AddRange(newArticles);
                _page++;
            }
            else
            {
                _hasMoreArticles = false;
            }
        }
        finally
        {
            _isLoading = false;
            StateHasChanged();
        }
    }
    
    private Task<List<ArticleItem>> FetchArticlesFromApi(int page, int pageSize)
    {
        // This would be an actual API call in a real application
        // Here we're just generating sample data
        var articles = new List<ArticleItem>();
        
        // Simulate end of data after page 5
        if (page > 5) return Task.FromResult(articles);
        
        var random = new Random();
        var categories = new[] { "Technology", "Science", "Health", "Business", "Entertainment" };
        var authors = new[] { "John Smith", "Jane Doe", "Alex Johnson", "Maria Garcia", "David Kim" };
        
        for (int i = 1; i <= pageSize; i++)
        {
            var id = ((page - 1) * pageSize) + i;
            articles.Add(new ArticleItem
            {
                Id = id,
                Title = $"Article {id}: The Future of Technology",
                Excerpt = $"This is a sample excerpt for article {id}. It provides a brief overview of what the article is about...",
                ImageUrl = $"/images/articles/article-{(id % 20) + 1}.jpg",
                Category = categories[random.Next(categories.Length)],
                Author = authors[random.Next(authors.Length)],
                PublishedDate = DateTime.Now.AddDays(-random.Next(1, 30))
            });
        }
        
        return Task.FromResult(articles);
    }
    
    private void NavigateToArticle(int articleId)
    {
        // Navigate to article detail page
        NavigationManager.NavigateTo($"/article/{articleId}");
    }
    
    private void HandleResize()
    {
        // Adjust column count based on viewport width
        var width = JSRuntime.InvokeAsync<int>("getViewportWidth").Result;
        
        _columnCount = width switch
        {
            < 576 => 1,  // xs
            < 768 => 2,  // sm
            < 992 => 2,  // md
            < 1200 => 3, // lg
            _ => 4       // xl
        };
    }
    
    class ArticleItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Excerpt { get; set; }
        public string ImageUrl { get; set; }
        public string Category { get; set; }
        public string Author { get; set; }
        public DateTime PublishedDate { get; set; }
    }
}
```

## CSS Customization

The Waterfall component can be customized using CSS variables and classes:

```css
/* Custom styling for Waterfall component */
.bb-waterfall {
    --bb-waterfall-gap: 12px;
    --bb-waterfall-item-bg: #ffffff;
    --bb-waterfall-item-border-radius: 8px;
    --bb-waterfall-item-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
}

.bb-waterfall-item {
    transition: transform 0.3s ease, box-shadow 0.3s ease;
    background-color: var(--bb-waterfall-item-bg);
    border-radius: var(--bb-waterfall-item-border-radius);
    box-shadow: var(--bb-waterfall-item-shadow);
    overflow: hidden;
}

.bb-waterfall-item:hover {
    transform: translateY(-5px);
    box-shadow: 0 5px 15px rgba(0, 0, 0, 0.15);
}
```

## JavaScript Interop

The Waterfall component uses JavaScript interop for layout calculations and animations. You can extend its functionality with custom JS functions:

```javascript
// Example of custom JS function for Waterfall component
window.getViewportWidth = function() {
    return window.innerWidth;
};

// Example of drag and drop initialization for notes
window.initNoteDragDrop = function(dotNetRef) {
    const container = document.querySelector('.bb-waterfall');
    
    // Initialize drag and drop library (e.g., SortableJS)
    const sortable = new Sortable(container, {
        animation: 150,
        onEnd: function() {
            const noteIds = Array.from(container.querySelectorAll('.note-card'))
                .map(el => parseInt(el.dataset.id));
            
            dotNetRef.invokeMethodAsync('UpdateNoteOrder', noteIds);
        }
    });
};
```

## Accessibility

The Waterfall component supports accessibility features:

- Keyboard navigation for interactive elements within the waterfall items
- ARIA attributes for screen readers
- Focus management for interactive elements
- Support for high contrast mode

## Browser Compatibility

The Waterfall component is compatible with modern browsers:

- Chrome (latest)
- Firefox (latest)
- Safari (latest)
- Edge (latest)
- Opera (latest)

## Integration with Other Components

The Waterfall component can be integrated with other BootstrapBlazor components:

- Use with `LazyLoad` for optimized image loading
- Combine with `IntersectionObserver` for advanced scroll-based effects
- Integrate with `DragDrop` for reordering items
- Use with `Filters` for filtering waterfall items by category
- Combine with `Search` for searching within waterfall items