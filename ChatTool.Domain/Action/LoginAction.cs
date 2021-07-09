
namespace ChatTool.Domain.Action
{
    /// <summary>
    /// 登入指令
    /// </summary>
    public class LoginAction : ActionBase
    {
        public override string Action()
            => "Login";

        /// <summary>
        /// 暱稱
        /// </summary>
        public string NickName { get; set; }
    }
}
