
namespace ChatTool.Domain.Action
{
    using ChatTool.Domain.Model;

    /// <summary>
    /// 登入結果
    /// </summary>
    public class LoginResultAction : ActionBase
    {
        public override string Action()
            => "LoginResult";

        /// <summary>
        /// 登入成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 登入成功後的回源資訊
        /// </summary>
        public UserInfo UserInfo { get; set; }

        /// <summary>
        /// 錯誤訊息
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}
