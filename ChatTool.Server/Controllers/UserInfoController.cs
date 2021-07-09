
namespace ChatTool.Server.Controllers
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using ChatTool.Domain.Model;
    using ChatTool.Domain.Repository;
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

        public UserInfoController(IUserInfoRepository repo)
        {
            this.repo = repo;
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
    }
}
