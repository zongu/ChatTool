
namespace ChatTool.UI.Tests.ActionHandler
{
    using ChatTool.Domain.Action;
    using ChatTool.UI.ActionHandler;
    using ChatTool.UI.Forms;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json;

    [TestClass]
    public class BroadCastLogoutActionHandlerTest
    {
        [TestMethod]
        public void 使用者移除單元測試()
        {
            var handler = new BroadCastLogoutActionHandler(new Main());
            var result = handler.Execute(new ActionModule()
            {
                Content = JsonConvert.SerializeObject(new BroadCastLogoutAction()
                {
                    NickName = "TEST001"
                })
            });

            Assert.IsTrue(result);
        }
    }
}
