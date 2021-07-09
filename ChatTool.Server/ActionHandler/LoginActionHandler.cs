
namespace ChatTool.Server.ActionHandler
{
    using System;
    using ChatTool.Domain.Action;
    using ChatTool.Domain.Model;
    using ChatTool.Domain.Repository;
    using ChatTool.Server.Model;
    using Newtonsoft.Json;
    using NLog;

    public class LoginActionHandler : IActionHandler
    {
        private ILogger logger = LogManager.GetLogger("ChatToolServer");

        /// <summary>
        /// 會員資訊持久層
        /// </summary>
        private IUserInfoRepository repo;

        public LoginActionHandler(IUserInfoRepository repo)
        {
            this.repo = repo;
        }

        public (Exception exception, NotifyType notifyType, ActionBase actionBase) ExecuteAction(ActionModule action)
        {
            try
            {
                var content = JsonConvert.DeserializeObject<LoginAction>(action.Content);
                var user = UserInfo.GenerateInstance(content.NickName);
                var loginResult = this.repo.Login(user);

                if(loginResult != null)
                {
                    this.logger.Warn($"{this.GetType().Name} ExecuteAction NickName:{content.NickName} LoginResult:{loginResult.Message}");

                    return (null, NotifyType.Single, new LoginResultAction()
                    {
                        Success = false,
                        UserInfo = null,
                        ErrorMessage = loginResult.Message
                    });
                }

                return (null, NotifyType.Single, new LoginResultAction()
                {
                    Success = false,
                    UserInfo = user,
                    ErrorMessage = string.Empty
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
