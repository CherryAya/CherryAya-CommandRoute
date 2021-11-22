namespace CherryAya_CommandRoute.Entities
{
    /// <summary>
    /// 指令路由配置
    /// </summary>
    public interface IRouteConfiguration
    {
        /// <summary>
        /// 指令匹配前缀
        /// </summary>
        public string[] CommandPrefix { get; set; }

        /// <summary>
        /// 消息段分割符号
        /// </summary>
        public string CommandSplit { get; set; }

        /// <summary>
        /// 指令匹配是否严格大小写
        /// </summary>
        public bool IsCaseSensitive { get; set;}
    }
}
