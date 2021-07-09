
namespace ChatTool.Server.Hubs
{
    using ChatTool.Domain.Action;

    /// <summary>
    /// hubclient 介面
    /// </summary>
    public interface IHubClient
    {
        /// <summary>
        /// 廣撥用
        /// </summary>
        void BroadCastAction<A>(A act)
            where A : ActionBase;
    }
}
