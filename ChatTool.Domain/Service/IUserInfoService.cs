
namespace ChatTool.Domain.Service
{
    using System;
    using ChatTool.Domain.Model;

    /// <summary>
    /// 登入服務介面
    /// </summary>
    public interface IUserInfoService
    {
        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        (Exception exception, LoginResultDto response) Login(LoginDto request);
    }
}
