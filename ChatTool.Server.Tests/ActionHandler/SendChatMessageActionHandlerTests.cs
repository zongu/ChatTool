
namespace ChatTool.Server.Tests.ActionHandler
{
    using System;
    using ChatTool.Domain.Action;
    using ChatTool.Server.ActionHandler;
    using ChatTool.Server.Model;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json;

    [TestClass]
    public class SendChatMessageActionHandlerTests
    {
        [TestMethod]
        public void 發送訊息單元測試()
        {
            var handler = new SendChatMessageActionHandler();
            var result = handler.ExecuteAction(new ActionModule()
            {
                Content = JsonConvert.SerializeObject(new SendChatMessageAction()
                {
                    NickName = "TEST001",
                    Message = "TEST123456",
                    CreateDateTime = DateTime.Now
                })
            });

            Assert.IsNull(result.exception);
            Assert.AreEqual(result.notifyType, NotifyType.BroadCast);
            Assert.IsNotNull(result.actionBase);
        }
    }
}
