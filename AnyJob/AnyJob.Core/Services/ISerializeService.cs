namespace AnyJob
{

    /// <summary>
    /// 表示序列化的服务
    /// </summary>
    public interface ISerializeService
    {
        /// <summary>
        /// 将对象序列化为字符串
        /// </summary>
        /// <param name="obj">要序列化的对象</param>
        /// <returns>返回序列化后的字符串</returns>
        string Serialize(object obj);
        /// <summary>
        /// 将字符串反序列化为指定类型的对象
        /// </summary>
        /// <typeparam name="T">对象的类型</typeparam>
        /// <param name="text">要反序列化的字符串</param>
        /// <returns>返回反序列化后的对象</returns>
        T Deserialize<T>(string text);
    }
}
