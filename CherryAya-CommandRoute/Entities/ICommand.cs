namespace CherryAya_CommandRoute.Entities
{
    /// <summary>
    /// 指令声明接口
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// 指令名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 指令描述
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 指令结构
        /// </summary>
        public ICommandStructure Structure { get; set; }
    }
}
