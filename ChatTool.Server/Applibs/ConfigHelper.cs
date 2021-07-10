﻿
namespace ChatTool.Server.Applibs
{
    /// <summary>
    /// 設定檔小幫手
    /// </summary>
    internal static class ConfigHelper
    {
        /// <summary>
        /// 主服務URL
        /// </summary>
        public static string ServiceUrl
        {
            get => $"http://*:9001";
        }
    }
}
