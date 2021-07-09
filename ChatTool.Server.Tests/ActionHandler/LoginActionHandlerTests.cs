
namespace ChatTool.Server.Tests.ActionHandler
{
    using System;
    using ChatTool.Domain.Action;
    using ChatTool.Domain.Model;
    using ChatTool.Domain.Repository;
    using ChatTool.Server.ActionHandler;
    using ChatTool.Server.Model;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class LoginActionHandlerTests
    {

        [TestMethod]
        public void 登入失敗測試()
        {
            var repo = new Mock<IUserInfoRepository>();
            repo.Setup(p => p.Login(It.IsAny<UserInfo>()))
                .Returns(new Exception("登入失敗"));

            var handler = new LoginActionHandler(repo.Object);
            var result = handler.ExecuteAction(new ActionModule()
            {
                Content = new LoginAction()
                {
                    NickName = "TEST001"
                }.ToString()
            });

            Assert.IsNull(result.exception);
            Assert.AreEqual(result.notifyType, NotifyType.Single);
            Assert.IsNotNull(result.actionBase);
        }

        [TestMethod]
        public void 登入成功測試()
        {
            var repo = new Mock<IUserInfoRepository>();
            repo.Setup(p => p.Login(It.IsAny<UserInfo>()))
                .Returns((Exception)null);

            var handler = new LoginActionHandler(repo.Object);
            var result = handler.ExecuteAction(new ActionModule()
            {
                Content = new LoginAction()
                {
                    NickName = "TEST001"
                }.ToString()
            });

            Assert.IsNull(result.exception);
            Assert.AreEqual(result.notifyType, NotifyType.Single);
            Assert.IsNotNull(result.actionBase);
        }
    }
}
