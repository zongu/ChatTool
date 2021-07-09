
namespace ChatTool.Server.ActionHandler
{
    using System;
    using ChatTool.Domain.Action;
    using ChatTool.Server.Model;
    using Newtonsoft.Json;
    using NLog;

    /// <summary>
    /// 發送聊天訊息處理
    /// </summary>
    public class SendChatMessageActionHandler : IActionHandler
    {
        private ILogger logger = LogManager.GetLogger("ChatToolServer");

        public (Exception exception, NotifyType notifyType, ActionBase actionBase) ExecuteAction(ActionModule action)
        {
            try
            {
                var content = JsonConvert.DeserializeObject<SendChatMessageAction>(action.Content);

                return (null, NotifyType.BroadCast, new BroadCastChatMessageAction()
                {
                    NickName = content.NickName,
                    Message = content.Message,
                    CreateDateTime = content.CreateDateTime
                });
            }
            catch (Exception ex)
            {
                this.logger.Error(ex, $"{this.GetType().Name} ExcuteAction Exception");
                return (ex, NotifyType.None, null);
            }
        }
    }
}
