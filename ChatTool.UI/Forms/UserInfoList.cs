
namespace ChatTool.UI.Forms
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Forms;
    using Autofac;
    using ChatTool.Domain.Model;
    using ChatTool.Domain.Service;
    using ChatTool.UI.Applibs;
    using NLog;

    public partial class UserInfoList : Form
    {
        private ILogger logger = LogManager.GetLogger("ChatToolUI");

        /// <summary>
        /// svc
        /// </summary>
        private IUserInfoService svc;

        private UserInfo[] userInfos;

        public UserInfoList()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            this.svc = AutofacConfig.Container.Resolve<IUserInfoService>();
        }

        /// <summary>
        /// DataGridView點擊
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvUserInfo_Click(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1 && 
                    e.RowIndex < this.userInfos.Length && 
                    MessageBox.Show($"是否刪除{this.userInfos[e.RowIndex].NickName}", "選擇", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    var logoutResult = this.svc.Logout(this.userInfos[e.RowIndex].NickName);

                    if (logoutResult != null)
                    {
                        throw logoutResult;
                    }
                }
            }
            catch (Exception ex)
            {
                this.logger.Error(ex, $"{this.GetType().Name} DgvUserInfo_Click Exception");
                MessageBox.Show(ex.Message);
            }

            this.Initialize();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Initialize()
        {
            try
            {
                var getResult = this.svc.Get();

                if (getResult.exception != null)
                {
                    throw getResult.exception;
                }

                this.userInfos = getResult.userInfos.ToArray();

                var bind = new BindingList<UserInfo>(this.userInfos);
                var source = new BindingSource(bind, null);
                this.dgvUserInfo.DataSource = source;
            }
            catch (Exception ex)
            {
                this.logger.Error(ex, $"{this.GetType().Name} UserInfoList_Shown Exception");
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 畫面展開時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserInfoList_Shown(object sender, EventArgs e)
        {
            this.Initialize();
        }
    }
}
