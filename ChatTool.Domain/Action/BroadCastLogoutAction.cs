﻿
namespace ChatTool.Domain.Action
{
    using ChatTool.Domain.Model;

    /// <summary>
    /// 通知登出
    /// </summary>
    public class BroadCastLogoutAction : ActionBase
    {
        public override string Action()
            => "BroadCastLogout";

        /// <summary>
        /// 被登出暱稱
        /// </summary>
        public string NickName { get; set; }
    }
}
