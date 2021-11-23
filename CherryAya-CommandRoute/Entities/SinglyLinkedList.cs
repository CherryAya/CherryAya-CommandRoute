namespace CherryAya_CommandRoute.Entities
{
    /// <summary>
    /// 单链表的定义
    /// </summary>
    internal class SinglyLinkedList
    {
        /// <summary>
        ///  值
        /// </summary>
        public string val { get; set; } = "";

        /// <summary>
        /// 下一节点
        /// </summary>
        public SinglyLinkedList? next { get; set; }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="val">值</param>
        public SinglyLinkedList(string val) { this.val = val; }

        /// <summary>
        /// 数组转单链表
        /// </summary>
        /// <param name="arr">数组</param>
        /// <returns>单链表</returns>
        public static SinglyLinkedList? ToSinglyLinkedList(string[] arr)
        {
            if (arr is null || arr.Length <= 0) return null;
            SinglyLinkedList sentineNode = new("");
            SinglyLinkedList tempNode = sentineNode;
            foreach (var val in arr)
            {
                SinglyLinkedList newNode = new(val);
                tempNode.next = newNode;
                tempNode = newNode;
            }
            return sentineNode.next;
        }
    }
}
