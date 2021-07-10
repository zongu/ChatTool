
namespace ChatTool.UI.Applibs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using Autofac;
    using ChatTool.Domain.Service;
    using ChatTool.UI.Forms;
    using ChatTool.UI.Model;
    using ChatTool.UI.Signalr;

    /// <summary>
    /// autofac 設定檔
    /// </summary>
    internal static class AutofacConfig
    {
        /// <summary>
        /// autofac 裝載介面跟實作的容器
        /// </summary>
        private static IContainer container;

        public static IContainer Container
        {
            get
            {
                if (container == null)
                {
                    Register();
                }

                return container;
            }
        }

        /// <summary>
        /// 註冊容器
        /// </summary>
        public static void Register()
        {
            var builder = new ContainerBuilder();
            var asm = Assembly.GetExecutingAssembly();

            // 取出當前執行assembly, 讓繼承IActionHandler且名稱結尾為ActionHandler的對應事件名稱
            // ex LoginResultAction對應的是LoginResultActionHandler
            builder.RegisterAssemblyTypes(asm)
                .Where(t => t.IsAssignableTo<IActionHandler>())
                .Named<IActionHandler>(t => t.Name.Replace("ActionHandler", string.Empty).ToLower())
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
                .SingleInstance();

            builder.RegisterType<HubClient>()
                .WithParameter("url", ConfigHelper.SignalrUrl)
                .WithParameter("hubName", ConfigHelper.SignalrHubName)
                .As<IHubClient>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
                .SingleInstance();

            builder.RegisterType<UserInfoService>()
                .WithParameter("serviceUri", ConfigHelper.ServiceUrl)
                .WithParameter("timeout", 5)
                .As<IUserInfoService>()
                .SingleInstance();

            builder.RegisterType<Main>()
                .SingleInstance();

            builder.RegisterType<Lobby>()
                .SingleInstance();

            builder.RegisterType<UserInfoList>()
                .SingleInstance();

            container = builder.Build();
        }
    }
}
