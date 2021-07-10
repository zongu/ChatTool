
namespace ChatTool.Server.Controllers
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using ChatTool.Domain.Action;
    using ChatTool.Domain.Model;
    using ChatTool.Domain.Repository;
    using ChatTool.Server.Hubs;
    using Newtonsoft.Json;
    using NLog;

    /// <summary>
    /// 使用者短連結服務
    /// </summary>
    public class UserInfoController : ApiController
    {
        private ILogger logger = LogManager.GetLogger("ChatToolServer");

        /// <summary>
        /// 使用者資訊持久層
        /// </summary>
        private IUserInfoRepository repo;

        /// <summary>
        /// 長連接服務
        /// </summary>
        private IHubClient hubClinet;

        public UserInfoController(IUserInfoRepository repo, IHubClient hubClinet)
        {
            this.repo = repo;
            this.hubClinet = hubClinet;
        }

        /// <summary>
        /// 取得使用者資料
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage Get()
        {
            try
            {
                var getResult = this.repo.GetAll();

                if(getResult.exception != null)
                {
                    throw getResult.exception;
                }

                var result = new HttpResponseMessage(HttpStatusCode.OK);
                result.Content = new StringContent(JsonConvert.SerializeObject(getResult.userInfos));
                return result;
            }
            catch (Exception ex)
            {
                this.logger.Error(ex, $"{this.GetType().Name} Get Exception");
                return this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// 登入
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage Login([FromBody] LoginDto input)
        {
            try
            {
                var user = UserInfo.GenerateInstance(input.NickName);
                var loginResult = this.repo.Login(user);
                LoginResultDto response;

                if (loginResult != null)
                {
                    this.logger.Warn($"{this.GetType().Name} ExecuteAction NickName:{input.NickName} LoginResult:{loginResult.Message}");

                    response = new LoginResultDto()
                    {
                        Success = false,
                        UserInfo = user,
                        ErrorMessage = loginResult.Message
                    };
                }
                else
                {
                    response = new LoginResultDto()
                    {
                        Success = true,
                        UserInfo = user,
                        ErrorMessage = string.Empty
                    };
                }

                var result = new HttpResponseMessage(HttpStatusCode.OK);
                result.Content = new StringContent(response.ToString());
                return result;
            }
            catch (Exception ex)
            {
                this.logger.Error(ex, $"{this.GetType().Name} Login Exception");
                return this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
        
        /// <summary>
        /// 登出
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpDelete]
        public HttpResponseMessage Logout([FromUri]string nickName)
        {
            try
            {
                var logoutResult = this.repo.LogOut(nickName);

                if(logoutResult != null)
                {
                    throw logoutResult;
                }

                this.hubClinet.BroadCastAction(new BroadCastLogoutAction()
                {
                    NickName = nickName
                });

                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                this.logger.Error(ex, $"{this.GetType().Name} Logout Exception");
                return this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
