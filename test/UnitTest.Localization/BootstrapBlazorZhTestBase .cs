// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Localization;

public class BootstrapBlazorZhTestBase : BootstrapBlazorTestBase
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        // Supports Microsoft resx format resource files
        services.AddLocalization(option => option.ResourcesPath = "Resources");
        services.AddBootstrapBlazor(localizationConfigure: options =>
        {
            options.ResourceManagerStringLocalizerType = typeof(BootstrapBlazorZhTestBase);
        });
    }

    protected override void ConfigureConfiguration(IServiceCollection services)
    {
        // Add unit test appsettings.json configuration file
        services.AddConfiguration("zh-CN");
    }
}
