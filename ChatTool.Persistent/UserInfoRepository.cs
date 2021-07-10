
namespace ChatTool.Persistent
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ChatTool.Domain.Model;
    using ChatTool.Domain.Repository;

    /// <summary>
    /// 使用者資訊持久層實作
    /// </summary>
    public class UserInfoRepository : IUserInfoRepository
    {
        /// <summary>
        /// 資源鎖
        /// </summary>
        public object _lck = new object();

        /// <summary>
        /// 登入會員
        /// </summary>
        private List<UserInfo> userInfos = new List<UserInfo>();

        /// <summary>
        /// 取得所有登入會員
        /// </summary>
        /// <returns></returns>
        public (Exception exception, IEnumerable<UserInfo> userInfos) GetAll()
            => (null, this.userInfos);

        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public Exception Login(UserInfo userInfo)
        {
            try
            {
                lock (this._lck)
                {
                    if (userInfos.Any(p => p.NickName.ToLower() == userInfo.NickName.ToLower()))
                    {
                        throw new Exception($"NickName:{userInfo.NickName} Already Exist");
                    }

                    this.userInfos.Add(userInfo);

                    return null;
                }
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <param name="nickName"></param>
        /// <returns></returns>
        public Exception LogOut(string nickName)
        {
            try
            {
                lock (this._lck)
                {
                    var preLogOutUser = this.userInfos.FirstOrDefault(p => p.NickName == nickName);

                    if (preLogOutUser != null)
                    {
                        this.userInfos.Remove(preLogOutUser);
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}
