using System.Threading.Tasks;

namespace AnyJob
{
    /// <summary>
    /// 表示准备pack的服务
    /// </summary>
    public interface IPreparePackService
    {
        void PreparePack(IActionName actionName, IActionRuntime actionRuntime);
    }
}
