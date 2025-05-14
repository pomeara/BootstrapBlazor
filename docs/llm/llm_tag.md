# Tag Component Documentation

## Overview
The Tag component in BootstrapBlazor is used to categorize or mark content with small colored labels. Tags are useful for highlighting status, filtering data, or labeling items in lists or tables.

## Features
- Multiple color themes (primary, secondary, success, danger, warning, info, dark)
- Closable tags with remove button
- Icon support
- Custom content support
- Size variants
- Click event handling
- Programmatic control (add/remove)

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| Color | Color | Primary | Sets the color theme of the tag |
| Icon | string | null | Icon to display within the tag |
| Text | string | null | Text content of the tag |
| ShowClose | bool | false | Whether to show a close button |
| IsOutline | bool | false | Whether to show the tag with outline style |
| Size | Size | Medium | Size of the tag (Small, Medium, Large) |
| OnClose | EventCallback | null | Callback when the close button is clicked |
| OnClick | EventCallback | null | Callback when the tag is clicked |

## Events

| Event | Description |
| --- | --- |
| OnClose | Triggered when the close button is clicked |
| OnClick | Triggered when the tag is clicked |

## Usage Examples

### Example 1: Basic Tags with Different Colors

```razor
<Tag Color="Color.Primary">Primary</Tag>
<Tag Color="Color.Secondary">Secondary</Tag>
<Tag Color="Color.Success">Success</Tag>
<Tag Color="Color.Danger">Danger</Tag>
<Tag Color="Color.Warning">Warning</Tag>
<Tag Color="Color.Info">Info</Tag>
<Tag Color="Color.Dark">Dark</Tag>
```

### Example 2: Tags with Icons

```razor
<Tag Color="Color.Primary" Icon="fa-solid fa-user">User</Tag>
<Tag Color="Color.Success" Icon="fa-solid fa-check">Completed</Tag>
<Tag Color="Color.Warning" Icon="fa-solid fa-exclamation-triangle">Warning</Tag>
<Tag Color="Color.Info" Icon="fa-solid fa-info-circle">Information</Tag>
<Tag Color="Color.Danger" Icon="fa-solid fa-times">Error</Tag>
```

### Example 3: Closable Tags

```razor
<div>
    @foreach (var tag in tags)
    {
        <Tag Color="@tag.Color" ShowClose="true" OnClose="() => RemoveTag(tag)">
            @tag.Text
        </Tag>
    }
</div>

<Button Color="Color.Primary" OnClick="AddTag">Add Tag</Button>

@code {
    private List<TagItem> tags = new List<TagItem>
    {
        new TagItem { Text = "Tag 1", Color = Color.Primary },
        new TagItem { Text = "Tag 2", Color = Color.Success },
        new TagItem { Text = "Tag 3", Color = Color.Warning }
    };

    private void RemoveTag(TagItem tag)
    {
        tags.Remove(tag);
    }

    private void AddTag()
    {
        var colors = Enum.GetValues(typeof(Color)).Cast<Color>().ToList();
        var random = new Random();
        var color = colors[random.Next(colors.Count)];
        tags.Add(new TagItem { Text = $"Tag {tags.Count + 1}", Color = color });
    }

    private class TagItem
    {
        public string Text { get; set; } = "";
        public Color Color { get; set; } = Color.Primary;
    }
}
```

### Example 4: Outline Style Tags

```razor
<Tag Color="Color.Primary" IsOutline="true">Primary</Tag>
<Tag Color="Color.Success" IsOutline="true">Success</Tag>
<Tag Color="Color.Danger" IsOutline="true">Danger</Tag>
<Tag Color="Color.Warning" IsOutline="true" Icon="fa-solid fa-star">Featured</Tag>
```

### Example 5: Different Sized Tags

```razor
<Tag Color="Color.Primary" Size="Size.Small">Small</Tag>
<Tag Color="Color.Primary">Default</Tag>
<Tag Color="Color.Primary" Size="Size.Large">Large</Tag>

<Tag Color="Color.Success" Size="Size.Small" Icon="fa-solid fa-check">Small</Tag>
<Tag Color="Color.Success" Icon="fa-solid fa-check">Default</Tag>
<Tag Color="Color.Success" Size="Size.Large" Icon="fa-solid fa-check">Large</Tag>
```

### Example 6: Clickable Tags

```razor
<div>
    @foreach (var tag in filterTags)
    {
        <Tag Color="@(tag.IsActive ? Color.Primary : Color.Secondary)" 
             OnClick="() => ToggleTag(tag)">
            @tag.Text
        </Tag>
    }
</div>

<div class="mt-3">
    <p>Selected filters: @string.Join(", ", filterTags.Where(t => t.IsActive).Select(t => t.Text))</p>
</div>

@code {
    private List<FilterTag> filterTags = new List<FilterTag>
    {
        new FilterTag { Text = "Electronics", IsActive = false },
        new FilterTag { Text = "Clothing", IsActive = false },
        new FilterTag { Text = "Books", IsActive = false },
        new FilterTag { Text = "Home & Kitchen", IsActive = false },
        new FilterTag { Text = "Toys", IsActive = false }
    };

    private void ToggleTag(FilterTag tag)
    {
        tag.IsActive = !tag.IsActive;
    }

    private class FilterTag
    {
        public string Text { get; set; } = "";
        public bool IsActive { get; set; }
    }
}
```

### Example 7: Tags in a Form

```razor
<Form Model="@model">
    <div class="mb-3">
        <BootstrapInput @bind-Value="@model.Title" placeholder="Enter title" />
    </div>
    
    <div class="mb-3">
        <p>Categories:</p>
        <div class="d-flex flex-wrap gap-2">
            @foreach (var category in availableCategories)
            {
                <Tag Color="@(model.Categories.Contains(category) ? Color.Primary : Color.Secondary)" 
                     OnClick="() => ToggleCategory(category)">
                    @category
                </Tag>
            }
        </div>
    </div>
    
    <Button Color="Color.Primary" Type="ButtonType.Submit">Submit</Button>
</Form>

@code {
    private PostModel model = new PostModel();
    
    private List<string> availableCategories = new List<string>
    {
        "Technology", "Business", "Science", "Health", "Sports", "Entertainment", "Travel"
    };
    
    private void ToggleCategory(string category)
    {
        if (model.Categories.Contains(category))
        {
            model.Categories.Remove(category);
        }
        else
        {
            model.Categories.Add(category);
        }
    }
    
    private class PostModel
    {
        public string Title { get; set; } = "";
        public List<string> Categories { get; set; } = new List<string>();
    }
}
```

## CSS Customization

The Tag component uses the following CSS variables that can be customized:

```css
:root {
    --bb-tag-btn-close-margin-left: 0.25rem;
    --bb-tag-btn-close-width: 0.75rem;
    --bb-tag-btn-close-height: 0.75rem;
    --bb-tag-text-margin-left: 0.25rem;
    --bb-tag-padding-x: 0.5rem;
    --bb-tag-padding-y: 0.25rem;
    --bb-tag-line-height: 1.5;
    --bb-tag-font-size: 0.875rem;
    --bb-tag-align: center;
}
```

You can override these variables in your own CSS to customize the appearance of the Tag component.

## Notes

- Tags are often used in conjunction with other components like Tables, Lists, or Forms for categorization and filtering.
- When using `ShowClose="true"`, make sure to handle the `OnClose` event to remove the tag from your collection.
- For accessibility, consider adding appropriate ARIA attributes when using tags for interactive elements.
- The Tag component supports custom content through child content, allowing for more complex tag designs beyond simple text and icons.