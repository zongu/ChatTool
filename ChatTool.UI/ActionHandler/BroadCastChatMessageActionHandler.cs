
namespace ChatTool.UI.ActionHandler
{
    using System;
    using ChatTool.Domain.Action;
    using ChatTool.UI.Forms;
    using ChatTool.UI.Model;
    using Newtonsoft.Json;
    using NLog;

    /// <summary>
    /// 廣播聊天訊息處理
    /// </summary>
    public class BroadCastChatMessageActionHandler : IActionHandler
    {
        /// <summary>
        /// 紀錄Log
        /// </summary>
        private ILogger logger = LogManager.GetLogger("ChatToolUI");

        /// <summary>
        /// 主要視窗
        /// </summary>
        private Main main;

        public BroadCastChatMessageActionHandler(Main main)
        {
            this.main = main;
        }

        public bool Execute(ActionModule actionModule)
        {
            try
            {
                var content = JsonConvert.DeserializeObject<BroadCastChatMessageAction>(actionModule.Content);
                this.main.ChatMessageAppend(content);
                return true;
            }
            catch (Exception ex)
            {
                this.logger.Error(ex, $"{this.GetType().Name} Excute Exception");
                return false;
            }
        }
    }
}
