
namespace ChatTool.UI.ActionHandler
{
    using System;
    using ChatTool.Domain.Action;
    using ChatTool.UI.Forms;
    using ChatTool.UI.Model;
    using Newtonsoft.Json;
    using NLog;

    /// <summary>
    /// 使用者登出通知
    /// </summary>
    public class BroadCastLogoutActionHandler : IActionHandler
    {
        /// <summary>
        /// 紀錄Log
        /// </summary>
        private ILogger logger = LogManager.GetLogger("ChatToolUI");

        /// <summary>
        /// 主要視窗
        /// </summary>
        private Main main;

        public BroadCastLogoutActionHandler(Main main)
        {
            this.main = main;
        }

        public bool Execute(ActionModule actionModule)
        {
            try
            {
                var content = JsonConvert.DeserializeObject<BroadCastLogoutAction>(actionModule.Content);

                if (this.main.User?.NickName == content.NickName)
                {
                    this.main.User = null;
                }

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
