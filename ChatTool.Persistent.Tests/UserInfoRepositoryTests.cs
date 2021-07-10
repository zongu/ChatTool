
namespace ChatTool.Persistent.Tests
{
    using System;
    using System.Linq;
    using ChatTool.Domain.Model;
    using ChatTool.Domain.Repository;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class UserInfoRepositoryTests
    {
        private IUserInfoRepository repo;

        [TestInitialize]
        public void Init()
        {
            this.repo = new UserInfoRepository();
        }

        [TestMethod]
        public void 會員登入測試()
        {
            var userInfo = UserInfo.GenerateInstance("TEST001");
            var loginResult = this.repo.Login(userInfo);

            Assert.IsNull(loginResult);
        }

        [TestMethod]
        public void 會員登入重複暱稱測試()
        {
            var userInfo = UserInfo.GenerateInstance("TEST001");
            var loginResult = this.repo.Login(userInfo);

            Assert.IsNull(loginResult);

            var repeatUserInfo = UserInfo.GenerateInstance("TEST001");
            var loginFailResult = this.repo.Login(repeatUserInfo);

            Assert.IsNotNull(loginFailResult);
        }

        [TestMethod]
        public void 取的全部登入會員測試()
        {
            var userInfo = UserInfo.GenerateInstance("TEST001");
            var loginResult = this.repo.Login(userInfo);

            Assert.IsNull(loginResult);

            var getResult = this.repo.GetAll();

            Assert.IsNull(getResult.exception);
            Assert.IsNotNull(getResult.userInfos);
            Assert.AreEqual(getResult.userInfos.Count(), 1);

            userInfo = UserInfo.GenerateInstance("TEST002");
            loginResult = this.repo.Login(userInfo);

            Assert.IsNull(loginResult);

            getResult = this.repo.GetAll();

            Assert.IsNull(getResult.exception);
            Assert.IsNotNull(getResult.userInfos);
            Assert.AreEqual(getResult.userInfos.Count(), 2);
        }

        [TestMethod]
        public void 會員登出測試()
        {
            var userInfo = UserInfo.GenerateInstance("TEST001");
            var loginResult = this.repo.Login(userInfo);

            Assert.IsNull(loginResult);

            var getResult = this.repo.GetAll();

            Assert.IsNull(getResult.exception);
            Assert.IsNotNull(getResult.userInfos);
            Assert.AreEqual(getResult.userInfos.Count(), 1);

            userInfo = UserInfo.GenerateInstance("TEST002");
            loginResult = this.repo.Login(userInfo);

            Assert.IsNull(loginResult);

            getResult = this.repo.GetAll();

            Assert.IsNull(getResult.exception);
            Assert.IsNotNull(getResult.userInfos);
            Assert.AreEqual(getResult.userInfos.Count(), 2);

            var logoutResult = this.repo.LogOut(userInfo.NickName);

            Assert.IsNull(logoutResult);

            getResult = this.repo.GetAll();

            Assert.IsNull(getResult.exception);
            Assert.IsNotNull(getResult.userInfos);
            Assert.AreEqual(getResult.userInfos.Count(), 1);

            var logoutFailResult = this.repo.LogOut(string.Empty);

            Assert.IsNull(logoutFailResult);

            getResult = this.repo.GetAll();

            Assert.IsNull(getResult.exception);
            Assert.IsNotNull(getResult.userInfos);
            Assert.AreEqual(getResult.userInfos.Count(), 1);
        }
    }
}
