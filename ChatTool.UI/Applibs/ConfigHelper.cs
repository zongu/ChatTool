
namespace ChatTool.UI.Applibs
{
    /// <summary>
    /// 設定檔小幫手
    /// </summary>
    internal static class ConfigHelper
    {
        /// <summary>
        /// 服務網址
        /// </summary>
        public static string ServiceUrl = "http://localhost:9001";

        /// <summary>
        /// Signalr 連結網址
        /// </summary>
        public static string SignalrUrl = "http://localhost:9001/signalr";

        /// <summary>
        /// Signalr hub name
        /// </summary>
        public static string SignalrHubName = "ChatToolHub";
    }
}
