﻿using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Scroll
    {
        /// <summary>
        /// Scroll 组件 DOM 实例
        /// </summary>
        private ElementReference ScrollElement { get; set; }

        /// <summary>
        /// 获得/设置 是否强制使用滚动条 默认为 true
        /// </summary>
        [Parameter]
        public bool IsForce { get; set; } = true;

        /// <summary>
        /// OnAfterRenderAsync 方法
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync(ScrollElement, "bb_scroll", IsForce);
            }
        }
    }
}
