using System.Text;

using CherryAya_CommandRoute.Entities;
using CherryAya_CommandRoute.Entities.impl;

namespace CherryAya_CommandRoute
{
    /// <summary>
    /// 指令路由
    /// </summary>
    public class CommandRoute
    {
        #region 私有成员

        /// <summary>
        /// 指令组
        /// </summary>
        private readonly List<ICommand> commands;

        /// <summary>
        /// 指令路由配置
        /// </summary>
        private readonly IRouteConfiguration configuration;

        #endregion

        #region 路由方法

        /// <summary>
        /// 路由分发方法
        /// </summary>
        /// <param name="MessageSegment">消息段</param>
        /// <returns>分发执行结果</returns>
        public bool Execute(string MessageSegment)
        {
            // 过滤
            if (MessageSegment.Length < 2) return false;
            if (!this.MatchingPrefix(MessageSegment)) return false;

            // 切割消息段
            SinglyLinkedList? Node = SinglyLinkedList.ToSinglyLinkedList(MessageSegment[1..].Split(this.configuration.CommandSplit));
            ICommandStructure? Cmd = null;
            
            // 消息段为null
            if (Node is null) return false;

            // 匹配Root节点
            foreach (var command in this.commands)
                if (this.NodeEquals(Node, command.Structure.Key))
                    Cmd = command.Structure;
            if (Cmd is null) return false;

            // 匹配Option/Value节点
            while (Node is not null)
            {
                if (Node.next is not null) // 有下一节点
                {
                    if (Cmd.Options is not null) // 有子指令组
                    {
                        bool flag = false;
                        foreach (var option in Cmd.Options) // 遍历子指令组
                        {
                            if (this.NodeEquals(Node.next, option.Key)) // 匹配子指令
                            {
                                Cmd = option;
                                flag = true;
                                Node = Node.next;
                                break;
                            }
                        }
                        if (!flag) // 无匹配结果
                        {
                            if (Cmd.hasValue is true) // 如果当前节点接受值
#pragma warning disable CS8602 // 解引用可能出现空引用。
                                Cmd.Value = Node.next.val;
#pragma warning restore CS8602 // 解引用可能出现空引用。
                            else
                                Cmd.Value = null;
                            break;
                        }
                    }
                    else if (Cmd.hasValue is true) // 无子指令组 当前节点接受值
                    {
                        Cmd.Value = Node.next.val;
                        break;
                    }
                    else // 无子指令组 当前节点不接受值
                    {
                        Cmd.Value = null;
                        break;
                    }
                }
                else // 无下一节点
                {
                    Cmd.Value = null;
                    break;
                }
            }
            
            // 匹配节点终点 Handle方法执行
            Cmd.Handle();
            return true;
        }

        /// <summary>
        /// 匹配指令前缀
        /// </summary>
        /// <param name="MessageSegment">消息段</param>
        /// <returns>匹配结果</returns>
        private bool MatchingPrefix(string MessageSegment)
        {
            foreach (var prefix in this.configuration.CommandPrefix)
                if (MessageSegment.StartsWith(prefix))
                    return true;
            return false;
        }

        /// <summary>
        /// 匹配节点与指令键
        /// </summary>
        /// <param name="node">当前节点</param>
        /// <param name="key">指令键</param>
        /// <returns>匹配结果</returns>
        private bool NodeEquals(SinglyLinkedList node, string key)
        {
            if (!this.configuration.IsCaseSensitive)
            {
                var temp = node.val.ToLower();
                key = key.ToLower();
                return Equals(temp, key);
            }
            else return Equals(node.val, key);
        }

        #endregion

        #region 指令组方法

        /// <summary>
        /// 注册单条指令到指令组
        /// </summary>
        /// <param name="command">目标指令</param>
        /// <returns>注册完成后的指令组</returns>
        public List<ICommand> Register(ICommand command)
        {
            if (!this.Contains(command).Item1)
            {
                this.commands.Add(command);
                return commands;
            }
            else return this.commands;
        }

        /// <summary>
        /// 注册多条指令到指令组
        /// </summary>
        /// <param name="commands">目标指令列表</param>
        /// <returns>注册完成后的指令组</returns>
        public List<ICommand> Register(List<ICommand> commands)
        {
            foreach (var command in commands)
                this.Register(command);
            return this.commands;
        }

        /// <summary>
        /// 从指令组注销单条指令
        /// </summary>
        /// <param name="command">目标指令</param>
        /// <returns>注销完成后的指令组</returns>
        public List<ICommand> Deregister(ICommand command)
{
            (bool, int) isContains = this.Contains(command);
            if (isContains.Item1)
                this.commands.Remove(commands[isContains.Item2]);
            return this.commands;
        }

        /// <summary>
        /// 从指令组注销单条指令
        /// </summary>
        /// <param name="name">目标指令名称</param>
        /// <returns>注销完成后的指令组</returns>
        public List<ICommand> Deregister(string name)
        {
            (bool, int) isContains = this.Contains(name);
            if (isContains.Item1)
                this.commands.Remove(commands[isContains.Item2]);
            return this.commands;
        }

        /// <summary>
        /// 查询指令组中是否存在该指令
        /// </summary>
        /// <param name="command">目标指令</param>
        /// <returns>
        /// <para>bool 是否存在指令</para>
        /// <para>int 索引 (不存在为-1)</para>
        /// </returns>
        public (bool, int) Contains(ICommand command)
        {
            return this.Contains(command.Name);
        }

        /// <summary>
        /// 查询指令组中是否存在该指令
        /// </summary>
        /// <param name="name">目标指令名称</param>
        /// <returns>
        /// <para>bool 是否存在指令</para>
        /// <para>int 索引 (不存在为-1)</para>
        /// </returns>
        public (bool, int) Contains(string name)
        {
            for (int i = 0; i < commands.Count; i++)
                if (commands[i].Name == name) return (true, i);
            return (false, -1);
        }

        /// <summary>
        /// 获得当前指令组
        /// </summary>
        /// <returns>当前指令组</returns>
        public List<ICommand> GetCommands()
        {
            return this.commands;
        }

        /// <summary>
        /// 清空当前指令组
        /// </summary>
        public void Clear() => this.commands.Clear();

        #endregion

        #region 构造方法

        /// <summary>
        /// 无参构造方法
        /// </summary>
        public CommandRoute()
        {
            this.commands = new();
            this.configuration = new defaultRouteConfiguration();
        }

        /// <summary>
        /// 有参构造方法
        /// </summary>
        /// <param name="configuration">指令路由配置</param>
        public CommandRoute(IRouteConfiguration configuration)
        {
            this.commands = new();
            this.configuration = configuration;
        }

        #endregion

        #region 重写

        /// <summary>
        /// ToString方法
        /// </summary>
        /// <returns>指令组Json字符串</returns>
        public override string ToString()
        {
            StringBuilder builder = new();
            builder.Append('{');
            builder.Append($"\"class\":\"{this.GetType().Name}\",");
            builder.Append($"\"commands\":[");
            if (this.commands.Count == 0)
                return builder.Append("]}").ToString();
            builder.Append('{');
            builder.Append($"\"item\":{0},\"class\":\"{this.commands[0].GetType().Name}\",\"name\":\"{this.commands[0].Name}\"");
            builder.Append('}');
            for (int i = 1; i < this.commands.Count; i++)
            {
                builder.Append(",{");
                builder.Append($"\"item\":{i},\"class\":\"{this.commands[i].GetType().Name}\",\"name\":\"{this.commands[i].Name}\"");
                builder.Append('}');
            }
            builder.Append("]}");
            return builder.ToString();
        }

        #endregion
    }
}
