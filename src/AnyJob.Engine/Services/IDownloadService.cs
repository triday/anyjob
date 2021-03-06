﻿using System.Threading.Tasks;

namespace AnyJob
{
    public interface IDownloadService
    {
        Task DownLoadFile(DownloadInfo downloadInfo);
    }
    public class DownloadInfo
    {
        public long FileSize { get; set; }
        public string FileHash { get; set; }
        public string FileUrl { get; set; }
        public string LocalFilePath { get; set; }
    }
    public static class DownloadServiceExtensions
    {
        public static Task DownLoadFile(this IDownloadService downloadService, string fileUrl, string LocalFilePath, long fileSize, string fileHash)
        {
            return downloadService.DownLoadFile(new DownloadInfo
            {
                FileSize = fileSize,
                FileHash = fileHash,
                FileUrl = fileUrl,
                LocalFilePath = LocalFilePath,
            });
        }
        public static async Task DownLoadFiles(this IDownloadService downloadService, params DownloadInfo[] downloadInfos)
        {
            foreach (var downloadInfo in downloadInfos)
            {
                await downloadService.DownLoadFile(downloadInfo);
            }
        }
    }
}
