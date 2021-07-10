
namespace ChatTool.Server.Applibs
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.Caching;
    using System.Threading;
    using System.Threading.Tasks;
    using Autofac;
    using ChatTool.Domain.Repository;
    using NLog;

    /// <summary>
    /// 主程式
    /// </summary>
    internal static class ChatToolServer
    {
        private static ILogger logger = LogManager.GetLogger("ChatToolServer");

        /// <summary>
        /// 開始工作
        /// </summary>
        public static void ProcessStart()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    DrawView();
                    SpinWait.SpinUntil(() => false, 1000);
                }
            });

            logger.Info($"ChatToolServer ProcessStart");
        }

        /// <summary>
        /// 下班
        /// </summary>
        public static void ProcessStop()
        {
            logger.Info($"ChatToolServer ProcessStop");
        }

        /// <summary>
        /// 畫畫面
        /// </summary>
        public static void DrawView()
        {
            Console.Clear();
            Console.WriteLine($"Listen On:{ConfigHelper.ServiceUrl}");

            using (var scope = AutofacConfig.Container.BeginLifetimeScope())
            {
                var repo = scope.Resolve<IUserInfoRepository>();
                var getResult = repo.GetAll();

                if (getResult.exception != null)
                {
                    logger.Error(getResult.exception, $"ChatToolServer DrawView IUserInfoRepository GetAll Exception");
                }

                Console.WriteLine($"Conect:{getResult.userInfos.Count()}");
            }

            Console.WriteLine($"Current Memory Usage:{(int)((GC.GetTotalMemory(true) / 1024f))}(KB)");
            Console.WriteLine($"Process Memory Usage:{(int)((Process.GetCurrentProcess().PrivateMemorySize64 / 1024f))}(KB)");
            Console.WriteLine($"Memory Cache Usage:{(int)(MemoryCache.Default.GetApproximateSize() / 1024f)}(KB)");
            Console.WriteLine($"Handle count:{Process.GetCurrentProcess().HandleCount}");
            Console.WriteLine($"{DateTime.Now}");
        }
    }
}
