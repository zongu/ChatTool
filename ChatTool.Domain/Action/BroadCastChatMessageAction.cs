﻿
namespace ChatTool.Domain.Action
{
    using System;

    /// <summary>
    /// 廣播聊天訊息
    /// </summary>
    public class BroadCastChatMessageAction : ActionBase
    {
        public override string Action()
            => "BroadCastChatMessage";

        /// <summary>
        /// 暱稱
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 訊息內容
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 創建時間
        /// </summary>
        public DateTime CreateDateTime { get; set; }
    }
}
