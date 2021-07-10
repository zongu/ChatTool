
namespace ChatTool.Server.Tests.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using ChatTool.Domain.Model;
    using ChatTool.Domain.Repository;
    using ChatTool.Server.Controllers;
    using ChatTool.Server.Hubs;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Newtonsoft.Json;

    [TestClass]
    public class UserInfoControllerTests
    {
        [TestMethod]
        public void 取得使用者資料測試()
        {
            var repo = new Mock<IUserInfoRepository>();
            repo.Setup(p => p.GetAll())
                .Returns((null, Enumerable.Range(1, 10).Select(index => new UserInfo()
                {
                    NickName = $"TEST{index}",
                    CreateDateTime = DateTime.Now
                })));

            var hubClient = new Mock<IHubClient>();

            var controller = new UserInfoController(repo.Object, hubClient.Object);
            var get = controller.Get();
            var response = JsonConvert.DeserializeObject<IEnumerable<UserInfo>>(get.Content.ReadAsStringAsync().Result);

            Assert.AreEqual(get.StatusCode, HttpStatusCode.OK);
            Assert.IsNotNull(response);
            Assert.AreEqual(response.Count(), 10);
        }

        [TestMethod]
        public void 新增會員測試()
        {
            var repo = new Mock<IUserInfoRepository>();
            repo.Setup(p => p.Login(It.IsAny<UserInfo>()))
                .Returns((Exception)null);

            var hubClient = new Mock<IHubClient>();

            var controller = new UserInfoController(repo.Object, hubClient.Object);
            var login = controller.Login(new LoginDto() { NickName = "TEST001" });
            var response = JsonConvert.DeserializeObject<LoginResultDto>(login.Content.ReadAsStringAsync().Result);

            Assert.AreEqual(login.StatusCode, HttpStatusCode.OK);
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public void 移除帳號測試()
        {
            var repo = new Mock<IUserInfoRepository>();
            repo.Setup(p => p.LogOut(It.IsAny<string>()))
                .Returns((Exception)null);

            var hubClient = new Mock<IHubClient>();

            var controller = new UserInfoController(repo.Object, hubClient.Object);
            var logout = controller.Logout("TEST001");

            Assert.AreEqual(logout.StatusCode, HttpStatusCode.OK);
        }
    }
}
