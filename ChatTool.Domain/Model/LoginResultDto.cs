
namespace ChatTool.Domain.Model
{
    using Newtonsoft.Json;

    /// <summary>
    /// 登入結果
    /// </summary>
    public class LoginResultDto
    {
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

        /// <summary>
        /// 序列化
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => JsonConvert.SerializeObject(this);
    }
}
