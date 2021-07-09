
namespace ChatTool.UI.Model
{
    using ChatTool.Domain.Action;

    /// <summary>
    /// Action處理工廠
    /// </summary>
    public interface IActionHandler
    {
        /// <summary>
        /// 執行action
        /// </summary>
        /// <param name="actionModule"></param>
        /// <returns></returns>
        bool Execute(ActionModule actionModule);
    }
}
