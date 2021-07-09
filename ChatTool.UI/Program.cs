
namespace ChatTool.UI
{
    using System;
    using System.Windows.Forms;
    using Autofac;
    using ChatTool.UI.Applibs;
    using ChatTool.UI.Forms;

    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (var scope = AutofacConfig.Container.BeginLifetimeScope())
            {
                var lobby = scope.Resolve<Lobby>();
                var main = scope.Resolve<Main>();

                if (lobby.ShowDialog() == DialogResult.OK)
                {
                    main.ShowDialog();
                }
            }
        }
    }
}
