
namespace ChatTool.Domain.Model
{
    using System;

    /// <summary>
    /// 聊天訊息
    /// </summary>
    public class ChatMessage
    {
        /// <summary>
        /// 發送者暱稱
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 創建時間
        /// </summary>
        public DateTime CreateDateTime { get; set; }
    }
}
