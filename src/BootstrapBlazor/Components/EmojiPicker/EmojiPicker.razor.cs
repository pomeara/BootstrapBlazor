// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.Components;

/// <summary>
/// Emoji picker — click the current value to open a categorised grid.
/// </summary>
public sealed partial class EmojiPicker
{
    /// <summary>
    /// Gets or sets the currently selected emoji.
    /// </summary>
    [Parameter]
    public string? Value { get; set; }

    /// <summary>
    /// Gets or sets the callback fired when an emoji is selected.
    /// </summary>
    [Parameter]
    public EventCallback<string?> ValueChanged { get; set; }

    /// <summary>
    /// Gets or sets the placeholder shown when no emoji is selected.
    /// </summary>
    [Parameter]
    public string? Placeholder { get; set; } = "Click to select emoji";

    private bool _showPicker;
    private string _selectedCategory = "Other";

    private static readonly Dictionary<string, string[]> _emojiCategories = new()
    {
        ["Other"] = new[] { "⭐", "❤️", "✅", "❌", "⚠️", "🔔", "🎯", "🔒", "🗑️", "📌", "💬", "📣", "🚀", "🌟", "💫" },
        ["Work"] = new[] { "💼", "📊", "📋", "🖨️", "📁", "📎", "✒️", "📌", "🗓️", "⏰", "📆", "🏢", "📞", "📧", "🖇️" },
        ["Home"] = new[] { "🏠", "🏡", "🏢", "🔑", "🛋️", "🛏️", "🚿", "🧹", "💡", "🔌", "📺", "🖥️", "🪴", "🧺", "🚗" },
        ["Food"] = new[] { "🍕", "🍔", "🍎", "🥗", "☕", "🍺", "🛒", "🥡", "🍳", "🍰", "🍷", "🥐", "🌮", "🍣", "🥤" },
        ["Health"] = new[] { "💊", "🏥", "🩺", "🏃", "💪", "🧘", "🦷", "👓", "🩹", "💉", "🧬", "🩻", "🏋️", "🚴", "⚕️" },
        ["Entertainment"] = new[] { "🎮", "🎬", "🎵", "📚", "🎨", "🎭", "🎪", "🎤", "📱", "🎲", "🎯", "🎳", "🎸", "🎺", "🎧" },
        ["Travel"] = new[] { "✈️", "🚗", "🚌", "🚇", "🚂", "⛽", "🏨", "🗺️", "🧳", "🌴", "🏖️", "⛷️", "🚢", "🚁", "🛴" },
        ["Shopping"] = new[] { "🛍️", "👗", "👔", "👟", "💄", "💍", "🎁", "🛒", "📦", "🏪", "🏬", "💐", "⌚", "👜", "🧢" },
        ["Education"] = new[] { "📚", "🎓", "✏️", "📝", "🔬", "🧪", "💻", "📐", "🎒", "🏫", "📖", "🖊️", "📏", "🔭", "🧮" },
        ["Family"] = new[] { "👶", "👧", "🧒", "👦", "🐕", "🐈", "🎂", "🎄", "🎃", "💝", "👨‍👩‍👧", "👨‍👩‍👦", "🧸", "🎈", "🪁" },
        ["Utilities"] = new[] { "📱", "💡", "🔌", "📡", "🌐", "📶", "☎️", "📞", "💻", "🖥️", "⚡", "🔋", "📻", "🛜", "📠" },
        ["Finance"] = new[] { "💰", "💵", "💳", "🏦", "💎", "📈", "📉", "🪙", "💲", "🏧", "💸", "🤑", "📊", "🧾", "💹" }
    };

    private string? ClassString => CssBuilder.Default("emoji-picker-container")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private void TogglePicker()
    {
        _showPicker = !_showPicker;
    }

    private void ClosePicker()
    {
        _showPicker = false;
    }

    private async Task SelectEmoji(string emoji)
    {
        Value = emoji;
        _showPicker = false;
        await ValueChanged.InvokeAsync(emoji);
    }

    private string GetCategoryIcon(string category) => category switch
    {
        "Finance" => "💰",
        "Home" => "🏠",
        "Food" => "🍕",
        "Health" => "💊",
        "Entertainment" => "🎮",
        "Travel" => "✈️",
        "Shopping" => "🛍️",
        "Education" => "📚",
        "Family" => "👶",
        "Work" => "💼",
        "Utilities" => "📱",
        "Other" => "⭐",
        _ => "📦"
    };
}
