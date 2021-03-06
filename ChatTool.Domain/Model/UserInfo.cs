
namespace ChatTool.Domain.Model
{
    using System;

    /// <summary>
    /// 使用者資訊
    /// </summary>
    public class UserInfo
    {
        public static UserInfo GenerateInstance(string NickName)
        {
            return new UserInfo()
            {
                NickName = NickName,
                CreateDateTime = DateTime.Now
            };
        }

        /// <summary>
        /// 暱稱
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 創建時間
        /// </summary>
        public DateTime CreateDateTime { get; set; }
    }
}
