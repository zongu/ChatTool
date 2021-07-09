
namespace ChatTool.Server.Hubs
{
    using System;
    using System.Threading;
    using ChatTool.Domain.Action;
    using Microsoft.AspNet.SignalR;
    using Newtonsoft.Json;
    using NLog;

    /// <summary>
    /// hubclient 實作
    /// </summary>
    public class HubClient : IHubClient
    {
        private ILogger logger = LogManager.GetLogger("ChatToolServer");

        private IHubContext hubContext
        {
            get
                => GlobalHost.ConnectionManager.GetHubContext<ChatToolHub>();
        }

        /// <summary>
        /// 廣撥用
        /// </summary>
        public void BroadCastAction<A>(A act) where A : ActionBase
        {
            var sendAction = new ActionModule()
            {
                Action = act.Action(),
                Content = act.ToString()
            };

            try
            {
                this.logger.Trace($"{this.GetType().Name} BroadCastAction: {JsonConvert.SerializeObject(sendAction)}");
                this.hubContext.Clients.All.BroadCastAction(sendAction);
            }
            catch (Exception ex)
            {
                this.logger.Error(ex, $"{this.GetType().Name} BroadCastAction Exception");
                bool runing = true;
                while (runing)
                {
                    SpinWait.SpinUntil(() => runing = false, 500);
                }

                this.hubContext.Clients.All.BroadCastAction(sendAction);
            }
        }
    }
}
