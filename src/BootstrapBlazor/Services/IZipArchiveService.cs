// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.IO.Compression;
using System.Text;

namespace BootstrapBlazor.Components;

/// <summary>
/// ZIP archive service
/// </summary>
public interface IZipArchiveService
{
    /// <summary>
    /// Archive files method
    /// </summary>
    /// <param name="files">Files to be archived</param>
    /// <param name="options">Archive configuration</param>
    /// <returns>Archive data stream</returns>
    Task<Stream> ArchiveAsync(IEnumerable<string> files, ArchiveOptions? options = null);

    /// <summary>
    /// Archive files method
    /// </summary>
    /// <param name="archiveFileName">Archive file name</param>
    /// <param name="files">Files to be archived</param>
    /// <param name="options">Archive configuration</param>
    Task ArchiveAsync(string archiveFileName, IEnumerable<string> files, ArchiveOptions? options = null);

    /// <summary>
    /// Archive directory method
    /// </summary>
    /// <param name="archiveFileName">Archive file name</param>
    /// <param name="directoryName">Directory to be archived</param>
    /// <param name="compressionLevel"></param>
    /// <param name="includeBaseDirectory"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    Task ArchiveDirectory(string archiveFileName, string directoryName, CompressionLevel compressionLevel = CompressionLevel.Optimal, bool includeBaseDirectory = false, Encoding? encoding = null);

    /// <summary>
    /// Extract archive to directory
    /// </summary>
    /// <param name="archiveFile">归档文件</param>
    /// <param name="destinationDirectoryName">Destination directory</param>
    /// <param name="overwriteFiles">Whether to overwrite files, default false</param>
    /// <param name="encoding">Encoding method, default null uses UTF-8</param>
    /// <returns></returns>
    bool ExtractToDirectory(string archiveFile, string destinationDirectoryName, bool overwriteFiles = false, Encoding? encoding = null);

    /// <summary>
    /// Get specific file from archive
    /// </summary>
    /// <param name="archiveFile">归档文件</param>
    /// <param name="entryFile">File to extract</param>
    /// <param name="overwriteFiles">Whether to overwrite files, default false</param>
    /// <param name="encoding">Encoding method, default null uses UTF-8</param>
    /// <returns></returns>
    ZipArchiveEntry? GetEntry(string archiveFile, string entryFile, bool overwriteFiles = false, Encoding? encoding = null);
}
