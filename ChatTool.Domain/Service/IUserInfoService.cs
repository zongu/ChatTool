
namespace ChatTool.Domain.Service
{
    using System;
    using System.Collections.Generic;
    using ChatTool.Domain.Model;

    /// <summary>
    /// 會員資訊介面
    /// </summary>
    public interface IUserInfoService
    {
        /// <summary>
        /// 取得使用者資訊
        /// </summary>
        /// <returns></returns>
        (Exception exception, IEnumerable<UserInfo> userInfos) Get();

        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        (Exception exception, LoginResultDto response) Login(LoginDto request);

        /// <summary>
        /// 登出
        /// </summary>
        /// <param name="nickName"></param>
        /// <returns></returns>
        Exception Logout(string nickName);
    }
}
