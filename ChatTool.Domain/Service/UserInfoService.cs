
namespace ChatTool.Domain.Service
{
    using System;
    using System.Net.Http;
    using System.Text;
    using ChatTool.Domain.Model;
    using Newtonsoft.Json;

    /// <summary>
    /// 登入服務介面
    /// </summary>
    public class UserInfoService : IUserInfoService
    {
        /// <summary>
        /// 連線工具
        /// </summary>
        private HttpClient client;

        /// <summary>
        /// source 路由
        /// </summary>
        private string route = @"api/UserInfo";

        public UserInfoService(string serviceUri, int timeout = 5)
        {
            this.client = new HttpClient()
            {
                BaseAddress = new Uri(serviceUri),
                Timeout = TimeSpan.FromSeconds(timeout),
            };
        }

        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public (Exception exception, LoginResultDto response) Login(LoginDto request)
        {
            try
            {
                var content = new StringContent(request.ToString(), Encoding.UTF8, "application/json");
                var response = this.client.PostAsync(this.route, content).Result;

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.Content.ReadAsStringAsync().Result);
                }

                var result = response.Content.ReadAsStringAsync().Result;
                return (null, JsonConvert.DeserializeObject<LoginResultDto>(result));
            }
            catch (Exception ex)
            {
                return (ex, null);
            }
        }
    }
}
