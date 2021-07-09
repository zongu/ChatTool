
namespace ChatTool.Server.Applibs
{
    using System.Reflection;
    using Autofac;
    using Autofac.Integration.WebApi;
    using ChatTool.Domain.Repository;
    using ChatTool.Persistent;
    using ChatTool.Server.Model;

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
            builder.RegisterApiControllers(asm); //把 api-controller 通通註冊進來

            // 取出當前執行assembly, 讓繼承IActionHandler且名稱結尾為ActionHandler的對應事件名稱
            // ex 事件對應的是
            builder.RegisterAssemblyTypes(asm)
                .Named<IActionHandler>(t => t.Name.Replace("ActionHandler", string.Empty).ToLower())
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
                .SingleInstance();

            builder.RegisterType<UserInfoRepository>()
                .As<IUserInfoRepository>()
                .SingleInstance();

            container = builder.Build();
        }
    }
}
