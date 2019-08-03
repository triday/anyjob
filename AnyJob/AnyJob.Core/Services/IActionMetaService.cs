namespace AnyJob
{
    /// <summary>
    /// 表示动作的元数据服务
    /// </summary>
    public interface IActionMetaService
    {
        /// <summary>
        /// 根据动作名称查询动作的元数据信息
        /// </summary>
        /// <param name="actionName">动作名称</param>
        /// <returns>返回查询动作的元数据信息</returns>
        IActionMeta GetActionMeta(IActionName actionName);
    }
}
