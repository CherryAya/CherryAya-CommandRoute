namespace CherryAya_CommandRoute.Entities.impl
{
    /// <summary>
    /// 指令路由默认配置
    /// </summary>
    internal class defaultRouteConfiguration : IRouteConfiguration
    {
        public string[] CommandPrefix { get; set; } = new string[] { "/" };
        public string CommandSplit { get; set; } = " ";
        public bool IsCaseSensitive { get; set; } = true;
    }
}
