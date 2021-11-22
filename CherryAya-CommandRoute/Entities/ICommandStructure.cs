namespace CherryAya_CommandRoute.Entities
{
    /// <summary>
    /// 指令结构接口
    /// </summary>
    public interface ICommandStructure
    {
        /// <summary>
        /// 指令键
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 是否有值
        /// </summary>
        public bool hasValue { get; set; }

        /// <summary>
        /// 指令值
        /// </summary>
        public object? Value { get; set; }
        
        /// <summary>
        /// 子指令组
        /// </summary>
        public List<ICommandStructure>? Options { get; set; }

        /// <summary>
        /// 指令执行方法
        /// </summary>
        public void Handle();
    }
}
