using System.Collections.Generic;

namespace AnyJob
{
    public interface IPackageProviderService
    {
        string GetLatestPackageVersion(string provider, string package);
        List<PackageFileInfo> GetAllPackageFiles(string provider, string package, string version);

    }
    public class PackageFileInfo
    {
        public string Path { get; set; }
        public long FileSize { get; set; }
        public string FileHash { get; set; }
        public string FileHashType { get; set; }
        public string FileUrl { get; set; }
    }
}
