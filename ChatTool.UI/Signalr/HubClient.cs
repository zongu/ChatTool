
namespace ChatTool.UI.Signalr
{
    using System;
    using System.Threading.Tasks;
    using Autofac.Features.Indexed;
    using ChatTool.Domain.Action;
    using ChatTool.UI.Model;
    using Microsoft.AspNet.SignalR.Client;
    using NLog;

    /// <summary>
    /// hubclient 實作
    /// </summary>
    public class HubClient : IHubClient
    {
        /// <summary>
        /// 連結狀態
        /// </summary>
        public ConnectionState State
            => this.hubConnection?.State ?? ConnectionState.Disconnected;

        /// <summary>
        /// 紀錄Log
        /// </summary>
        private ILogger logger = LogManager.GetLogger("ChatToolUI");

        /// <summary>
        /// HubClient連線物件
        /// </summary>
        private HubConnection hubConnection;

        /// <summary>
        /// HubProxy
        /// </summary>
        private IHubProxy hubProxy;

        /// <summary>
        /// 連線字串
        /// </summary>
        private string url;

        /// <summary>s
        /// HubName
        /// </summary>
        private string hubName;

        /// <summary>
        /// IOC 關連表
        /// </summary>
        private IIndex<string, IActionHandler> handlerSets;

        public HubClient(
            string url,
            string hubName,
            IIndex<string, IActionHandler> handlerSets)
        {
            this.url = url;
            this.hubName = hubName;
            this.handlerSets = handlerSets;
        }

        /// <summary>
        /// 發送action
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="act"></param>
        public void SendAction<T>(T act) where T : ActionBase
        {
            var sendAction = new ActionModule()
            {
                Action = act.Action(),
                Content = act.ToString()
            };

            if (State != ConnectionState.Connected)
            {
                this.logger.Warn($"{this.GetType().Name} SendAction State:{State}, Action: {sendAction.Action}, Content: {sendAction.Content}");
                return;
            }

            this.hubProxy.Invoke<ActionModule>("SendAction", sendAction).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    this.logger.Error(task.Exception, $"{this.GetType().Name} SendAction Fail Action: {sendAction.Action}, Content: {sendAction.Content}");
                }
                else
                {
                    this.logger.Trace($"{this.GetType().Name} SendAction >> Action: {sendAction.Action}, Content: {sendAction.Content}");
                }
            });
        }

        /// <summary>
        /// 啟動hubClient
        /// </summary>
        public async Task StartAsync()
        {
            this.hubConnection = new HubConnection(this.url);
            this.hubConnection.TransportConnectTimeout = TimeSpan.FromSeconds(30);
            this.hubConnection.Error += HubConnection_Error;
            this.hubConnection.StateChanged += HubConnection_StateChanged;
            this.hubProxy = this.hubConnection.CreateHubProxy(this.hubName);
            this.hubProxy.On<ActionModule>("BroadCastAction", action => this.BroadCastAction(action));
            // 連線開啟
            await this.hubConnection.Start().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    this.logger.Error(task.Exception, $"{this.GetType().Name} HubConnection 啟動失敗");
                }
                else
                {

                }
            });
        }

        /// <summary>
        /// 監聽 BroadCastAction
        /// </summary>
        /// <param name="action"></param>
        private void BroadCastAction(ActionModule action)
        {
            try
            {
                this.logger.Trace($"{this.GetType().Name} BroadCastAction Action: {action.Action}, Content: {action.Content}");

                if (this.handlerSets.TryGetValue(action.Action.ToLower(), out var handler))
                {
                    handler.Execute(action);
                }
            }
            catch (Exception ex)
            {
                this.logger.Error(ex, $"{this.GetType()} BroadCastAction Exception");
            }
        }

        /// <summary>
        /// 狀態切換
        /// </summary>
        /// <param name="obj"></param>
        private void HubConnection_StateChanged(StateChange obj)
        {
        }

        /// <summary>
        /// signalr連線上遇到錯誤
        /// </summary>
        /// <param name="obj"></param>
        private void HubConnection_Error(Exception obj)
        {
            this.logger.Error(obj, $"{this.GetType().Name} HubConnection_Error");
        }
    }
}
