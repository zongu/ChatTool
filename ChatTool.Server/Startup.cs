
[assembly: Microsoft.Owin.OwinStartup(typeof(ChatTool.Server.Startup))]

namespace ChatTool.Server
{
    using System.Web.Http;
    using Autofac.Integration.WebApi;
    using ChatTool.Server.Applibs;
    using Microsoft.AspNet.SignalR;
    using Microsoft.Owin.Cors;
    using Owin;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var webApiConfiguration = ConfigureWebApi();
            app.UseWebApi(webApiConfiguration);
            app.UseCors(CorsOptions.AllowAll);

            // 解除限制WS傳輸量
            GlobalHost.Configuration.MaxIncomingWebSocketMessageSize = null;

            GlobalHost.Configuration.DefaultMessageBufferSize = 32; // 每個集線器緩存保留的消息，留存過多會造成緩存變高
            app.MapSignalR();
        }

        private HttpConfiguration ConfigureWebApi()
        {
            var config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional });

            // API DI設定
            config.DependencyResolver = new AutofacWebApiDependencyResolver(AutofacConfig.Container);

            return config;
        }
    }
}
