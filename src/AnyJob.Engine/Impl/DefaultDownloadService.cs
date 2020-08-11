using System.IO;
using System.Net.Http;
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
                HttpUtility.UrlEncode("abc");
                throw Errors.DownLoadFileError(downloadInfo.LocalFilePath, downloadInfo.FileUrl, ex);
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
