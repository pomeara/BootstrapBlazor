// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Extensions;

public class DirectoryInfoExtensionsTest
{
    [Fact]
    public void Copy_Ok()
    {
        var rootDir = Path.Combine(AppContext.BaseDirectory, "test");
        if (!Directory.Exists(rootDir))
        {
            Directory.CreateDirectory(rootDir);
        }


        // Create SourceDir
        var sourceDir = CreateDir(Path.Combine(rootDir, "test1"));
        // Create temporary test directory
        CreateDir(Path.Combine(sourceDir, "test"));
        // Create temporary test file
        using var file = File.OpenWrite(Path.Combine(sourceDir, "test.log"));
        file.Close();

        var destDir = Path.Combine(rootDir, "test2");
        if (Directory.Exists(destDir))
        {
            Directory.Delete(destDir, true);
        }

        var sourceDirInfo = new DirectoryInfo(sourceDir);
        sourceDirInfo.Copy(destDir);
        Assert.True(Directory.Exists(destDir));
    }

    private static string CreateDir(string dirName)
    {
        if (!Directory.Exists(dirName))
        {
            Directory.CreateDirectory(dirName);
        }
        return dirName;
    }
}
