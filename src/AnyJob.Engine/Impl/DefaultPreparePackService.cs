using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AnyJob.Impl
{
    [YS.Knife.ServiceClass(Lifetime = Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton)]
    public class DefaultPreparePackService : IPreparePackService
    {
        const string LOCK_FILENAME = ".anyjob_download_lock";
        const string READY_FILENAME = ".anyjob_download_ready";
        readonly IDownloadService downloadService;
        readonly IPackageProviderService packageProviderService;
        public DefaultPreparePackService(IDownloadService downloadService, IPackageProviderService packageProviderService)
        {
            this.downloadService = downloadService;
            this.packageProviderService = packageProviderService;
        }
        public void PreparePack(IActionName actionName, IActionRuntime actionRuntime)
        {
            _ = actionName ?? throw new ArgumentNullException(nameof(actionName));
            string lockFile = System.IO.Path.Combine(actionRuntime.WorkingDirectory, LOCK_FILENAME);
            string readyFile = System.IO.Path.Combine(actionRuntime.WorkingDirectory, READY_FILENAME);
            if (File.Exists(readyFile))
            {
                // package is download already;
                return;
            }
            else if (File.Exists(lockFile))
            {
                // other thread or process download the package
                this.WaitForDownLoadReady(readyFile).Wait(TimeSpan.FromMinutes(15));
            }
            else
            {
                // begin download files
                this.DownLoadPackage(actionName, actionRuntime.WorkingDirectory, lockFile, readyFile);
            }
        }
        private async Task WaitForDownLoadReady(string readyFile)
        {
            while (!File.Exists(readyFile))
            {
                await Task.Delay(1000);
            }
        }
        private void DownLoadPackage(IActionName actionName, string WorkingDirectory, string lockFile, string readyFile)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(lockFile));
                using (new FileStream(lockFile, FileMode.CreateNew)) { };
                var packFiles = packageProviderService.GetAllPackageFiles(actionName.Provider, actionName.Pack, actionName.Version);
                var downLoadInfos = packFiles.Select(p => new DownloadInfo
                {
                    FileHash = p.FileHash,
                    FileUrl = p.FileUrl,
                    LocalFilePath = Path.Combine(WorkingDirectory, p.Path)
                }).ToArray();
                downloadService.DownLoadFiles(downLoadInfos).Wait();
                File.WriteAllText(readyFile, "download ready.");
            }
            finally
            {
                //remove lockFile
                File.Delete(lockFile);
            }
        }
    }
}
