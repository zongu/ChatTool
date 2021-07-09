
namespace ChatTool.Server.Model
{
    using System;
    using ChatTool.Domain.Action;

    public enum NotifyType
    {
        None,
        Single,
        BroadCast
    }

    /// <summary>
    /// Action 處理介面
    /// </summary>
    public interface IActionHandler
    {
        /// <summary>
        /// 處理action
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        (Exception exception, NotifyType notifyType, ActionBase actionBase) ExecuteAction(ActionModule action);
    }
}
