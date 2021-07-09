
namespace ChatTool.Domain.Model
{
    using Newtonsoft.Json;

    /// <summary>
    /// 登入結構
    /// </summary>
    public class LoginDto
    {
        /// <summary>
        /// 暱稱
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => JsonConvert.SerializeObject(this);
    }
}
