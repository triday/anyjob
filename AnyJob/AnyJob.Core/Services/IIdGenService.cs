namespace AnyJob
{
    /// <summary>
    /// 表示生成ID的服务
    /// </summary>
    public interface IIdGenService
    {
        /// <summary>
        /// 新生成一个ID
        /// </summary>
        /// <returns>返回新生成的ID</returns>
        string NewId();
    }
}
