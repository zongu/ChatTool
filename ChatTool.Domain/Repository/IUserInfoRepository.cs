
namespace ChatTool.Domain.Repository
{
    using System;
    using System.Collections.Generic;
    using ChatTool.Domain.Model;

    /// <summary>
    /// 使用者資訊持久層介面
    /// </summary>
    public interface IUserInfoRepository
    {
        /// <summary>
        /// 取得所有登入會員
        /// </summary>
        /// <returns></returns>
        (Exception exception, IEnumerable<UserInfo> userInfos) GetAll();

        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        Exception Login(UserInfo userInfo);

        /// <summary>
        /// 登出
        /// </summary>
        /// <param name="nickName"></param>
        /// <returns></returns>
        Exception LogOut(string nickName);
    }
}
