using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using AnyJob.Config;
using Microsoft.Extensions.Logging;

namespace AnyJob.Engine.Impl
{
    [YS.Knife.ServiceClass(Lifetime = Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton)]
    public class DefaultDownloadService : IDownloadService
    {
        readonly PackOption packOption;
        readonly ILogger logger;
        public DefaultDownloadService(PackOption packOption, ILogger<DefaultDownloadService> logger)
        {
            this.packOption = packOption;
            this.logger = logger;
        }
        public async Task DownLoadFile(DownloadInfo downloadInfo)
        {
            try
            {
                if (!NeedDownLoad(downloadInfo))
                {
                    return;
                }
                string cacheFile = Path.Combine(packOption.DownLoadCacheDir, downloadInfo.FileHash);

                if (File.Exists(cacheFile))
                {
                    CopyToTarget(cacheFile, downloadInfo.LocalFilePath);
                    logger.LogInformation($"Downloaded file {downloadInfo.LocalFilePath} from local cache.");
                }
                else
                {
                    // TODO 需要优化为IHttpClientFactory
                    using (var client = new HttpClient())
                    {
                        var stream = await client.GetStreamAsync(downloadInfo.FileUrl);
                        CopyToTarget(stream, downloadInfo.LocalFilePath);
                        logger.LogInformation($"Downloaded file {downloadInfo.LocalFilePath} from {downloadInfo.FileUrl}.");
                    }
                    if (!File.Exists(cacheFile))
                    {
                        CopyToTarget(downloadInfo.LocalFilePath, cacheFile);
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw Errors.DownLoadFileError(downloadInfo.LocalFilePath, downloadInfo.FileUrl, ex);
            }
        }

        private bool NeedDownLoad(DownloadInfo downloadInfo)
        {
            if (File.Exists(downloadInfo.LocalFilePath) &&
                GetFileSize(downloadInfo.LocalFilePath) == downloadInfo.FileSize &&
                string.Equals(GetFileHash(downloadInfo.LocalFilePath), downloadInfo.FileHash, StringComparison.InvariantCultureIgnoreCase))
            {
                return false;
            }
            return true;
        }
        private long GetFileSize(string filePath)
        {
            return new FileInfo(filePath).Length;
        }
        private string GetFileHash(string filePath)
        {
            using (var hash = SHA256.Create())
            {
                using (var stream = new FileStream(filePath, FileMode.Open))
                {
                    var bytes = hash.ComputeHash(stream);
                    return string.Join("", bytes.Select(p => p.ToString("X2", CultureInfo.InvariantCulture)).ToArray());
                }
            }
        }
        private void CopyToTarget(string source, string targetFile)
        {
            var directoryName = Path.GetDirectoryName(targetFile);
            Directory.CreateDirectory(directoryName);
            File.Copy(source, targetFile, true);
        }
        private void CopyToTarget(Stream source, string targetFile)
        {
            var directoryName = Path.GetDirectoryName(targetFile);
            Directory.CreateDirectory(directoryName);

            using (var fileStream = new FileStream(targetFile, FileMode.Create))
            {
                source.CopyTo(fileStream);
            }
        }
    }
}
