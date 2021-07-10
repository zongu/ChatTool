
namespace ChatTool.UI.Tests.ActionHandler
{
    using System;
    using ChatTool.Domain.Action;
    using ChatTool.UI.ActionHandler;
    using ChatTool.UI.Forms;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Newtonsoft.Json;

    [TestClass]
    public class BroadCastChatMessageActionHandlerTests
    {
        [TestMethod]
        public void 接收聊天訊息測試()
        {
            var handler = new BroadCastChatMessageActionHandler(new Main());
            var result = handler.Execute(new ActionModule()
            {
                Content = JsonConvert.SerializeObject(new BroadCastChatMessageAction()
                {
                    NickName = "TEST001",
                    Message = "123456",
                    CreateDateTime = DateTime.Now
                })
            });

            Assert.IsTrue(result);
        }
    }
}
