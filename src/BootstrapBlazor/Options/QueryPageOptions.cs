// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// Query condition entity class
/// </summary>
[JsonConverter(typeof(JsonQueryPageOptionsConverter))]
public class QueryPageOptions
{
    /// <summary>
    /// Gets/Sets fuzzy search keyword
    /// </summary>
    public string? SearchText { get; set; }

    /// <summary>
    /// Gets sort field name set by <see cref="Table{TItem}.SortName"/>
    /// </summary>
    public string? SortName { get; set; }

    /// <summary>
    /// Gets sort order set by <see cref="Table{TItem}.SortOrder"/>
    /// </summary>
    public SortOrder SortOrder { get; set; }

    /// <summary>
    /// Gets/Sets multi-column sort collection, default Empty with internal format like "Name" "Age desc", set by <see cref="Table{TItem}.SortString"/>
    /// </summary>
    public List<string> SortList { get; } = new(10);

    /// <summary>
    /// Gets/Sets custom multi-column sort collection, default Empty with internal format like "Name" "Age desc", set by <see cref="Table{TItem}.AdvancedSortItems"/>
    /// </summary>
    public List<string> AdvancedSortList { get; } = new(10);

    /// <summary>
    /// Gets search condition binding model, uses <see cref="Table{TItem}"/> generic model when <see cref="Table{TItem}.CustomerSearchModel"/> is not set
    /// </summary>
    public object? SearchModel { get; set; }

    /// <summary>
    /// Gets current page number, first page is 1
    /// </summary>
    public int PageIndex { get; set; } = 1;

    /// <summary>
    /// Gets request data start row, default 0
    /// </summary>
    /// <remarks><see cref="Table{TItem}.ScrollMode"/> 开启虚拟滚动 <see cref="ScrollMode.Virtual"/> 时使用</remarks>
    public int StartIndex { get; set; }

    /// <summary>
    /// Gets items per page, set by <see cref="Table{TItem}._pageItems"/> and <see cref="Table{TItem}.PageItemsSource"/>
    /// </summary>
    public int PageItems { get; set; } = 20;

    /// <summary>
    /// Gets whether in pagination mode, default false, set by <see cref="Table{TItem}.IsPagination"/>
    /// </summary>
    public bool IsPage { get; set; }

    /// <summary>
    /// Gets whether in virtual scroll mode, default false, set by <see cref="Table{TItem}.ScrollMode"/>
    /// </summary>
    public bool IsVirtualScroll { get; set; }

    /// <summary>
    /// 获得 通过列集合中的 <see cref="ITableColumn.Searchable"/> 列与 <see cref="SearchText"/> 拼装 IFilterAction 集合
    /// </summary>
    [Obsolete("This property is obsolete. Use Searches instead. 已过期，请使用 Searches 参数")]
    [ExcludeFromCodeCoverage]
    public List<IFilterAction> Searchs => Searches;

    /// <summary>
    /// 获得 通过列集合中的 <see cref="ITableColumn.Searchable"/> 列与 <see cref="SearchText"/> 拼装 IFilterAction 集合
    /// </summary>
    public List<IFilterAction> Searches { get; } = new(20);

    /// <summary>
    /// 获得 <see cref="Table{TItem}.CustomerSearchModel"/> 中过滤条件 <see cref="Table{TItem}.SearchTemplate"/> 模板中的条件请使用 <see cref="AdvanceSearches" />获得
    /// </summary>
    [Obsolete("This property is obsolete. Use CustomerSearches instead. 已过期，请使用 CustomerSearches 参数")]
    [ExcludeFromCodeCoverage]
    public List<IFilterAction> CustomerSearchs => CustomerSearches;

    /// <summary>
    /// 获得 <see cref="Table{TItem}.CustomerSearchModel"/> 中过滤条件 <see cref="Table{TItem}.SearchTemplate"/> 模板中的条件请使用 <see cref="AdvanceSearches" />获得
    /// </summary>
    public List<IFilterAction> CustomerSearches { get; } = new(20);

    /// <summary>
    /// 获得 <see cref="Table{TItem}.SearchModel"/> 中过滤条件
    /// </summary>
    [Obsolete("This property is obsolete. Use AdvanceSearches instead. 已过期，请使用 AdvanceSearches 参数")]
    [ExcludeFromCodeCoverage]
    public List<IFilterAction> AdvanceSearchs => AdvanceSearches;

    /// <summary>
    /// 获得 <see cref="Table{TItem}.SearchModel"/> 中过滤条件
    /// </summary>
    public List<IFilterAction> AdvanceSearches { get; } = new(20);

    /// <summary>
    /// 获得 过滤条件集合 等同于 <see cref="Table{TItem}.Filters"/> 值
    /// </summary>
    public List<IFilterAction> Filters { get; } = new(20);

    /// <summary>
    /// 获得 是否为首次查询 默认 false
    /// </summary>
    /// <remarks><see cref="Table{TItem}"/> 组件首次查询数据时为 true</remarks>
    [Obsolete("This property is obsolete. Use IsFirstQuery. 已弃用单词拼写错误，请使用 IsFirstQuery")]
    [ExcludeFromCodeCoverage]
    public bool IsFristQuery { get => IsFirstQuery; set => IsFirstQuery = value; }

    /// <summary>
    /// 获得 是否为首次查询 默认 false
    /// </summary>
    /// <remarks><see cref="Table{TItem}"/> 组件首次查询数据时为 true</remarks>
    public bool IsFirstQuery { get; set; }

    /// <summary>
    /// 获得 是否为刷新分页查询 默认 false 
    /// </summary>
    public bool IsTriggerByPagination { get; set; }
}
