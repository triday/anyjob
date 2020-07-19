using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using AnyJob.Config;
using Microsoft.Extensions.Logging;

namespace AnyJob.Engine.Impl
{
    [YS.Knife.ServiceClass(Lifetime = Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton)]
    public class DefaultDownloadService : IDownloadService
    {
        readonly PackOption packOption;
        readonly ILogger logger;
        public DefaultDownloadService(PackOption packOption)
        {
            this.packOption = packOption;
        }
        public async Task DownLoadFile(DownloadInfo downloadInfo)
        {
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
            using (var fileStream = new FileStream(targetFile, FileMode.CreateNew))
            {
                source.CopyTo(fileStream);
            }
        }
    }
}
