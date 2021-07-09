
namespace ChatTool.UI.Forms
{
    using System;
    using System.Text;
    using System.Windows.Forms;
    using Autofac;
    using ChatTool.Domain.Action;
    using ChatTool.Domain.Model;
    using ChatTool.Domain.Service;
    using ChatTool.UI.Applibs;
    using ChatTool.UI.Signalr;
    using Microsoft.AspNet.SignalR.Client;
    using NLog;

    public partial class Main : Form
    {
        /// <summary>
        /// 是否為管理者
        /// </summary>
        public bool IsAdmin = false;

        /// <summary>
        /// 當前使用者
        /// </summary>
        public UserInfo User = null;

        private ILogger logger = LogManager.GetLogger("ChatToolUI");

        /// <summary>
        /// 委派傳遞字串
        /// </summary>
        /// <param name="text"></param>
        private delegate void SafeCallDelegate(string text);

        /// <summary>
        /// 聊天室內容
        /// </summary>
        private StringBuilder sb = new StringBuilder();

        /// <summary>
        /// hub client
        /// </summary>
        private IHubClient hubClient;

        /// <summary>
        /// svc
        /// </summary>
        private IUserInfoService svc;

        /// <summary>
        /// 背景計時器
        /// </summary>
        private Timer timer;

        public Main()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            this.hubClient = AutofacConfig.Container.Resolve<IHubClient>();
            this.svc = AutofacConfig.Container.Resolve<IUserInfoService>();

            this.timer = new Timer();
            this.timer.Interval = 500;
            this.timer.Tick += (object sender, EventArgs e) =>
            {
                this.ChangeLoginStatus();
            };
        }

        /// <summary>
        /// 加入聊天訊息
        /// </summary>
        /// <param name="message"></param>
        public void ChatMessageAppend(BroadCastChatMessageAction message)
        {
            if (this.User != null)
            {
                // 太長砍掉
                if (this.sb.Length > 99999)
                {
                    this.sb.Clear();
                }

                this.sb.Append($"{message.NickName}-{message.CreateDateTime.ToString("HH:mm:ss")}:{message.Message}");
                this.sb.AppendLine();

                this.UpdateMessage($"{message.NickName}-{message.CreateDateTime.ToString("HH:mm:ss")}:{message.Message}");
            }
        }

        /// <summary>
        /// 按鈕點擊事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ButtonOnClick(object sender, EventArgs e)
        {
            try
            {
                var btn = (Button)sender;

                switch (btn.Name)
                {
                    case "btnLogin":

                        if (this.hubClient.State != ConnectionState.Connected)
                        {
                            MessageBox.Show("請先確認有連上伺服器!");
                            return;
                        }

                        if (string.IsNullOrEmpty(this.tbNickName.Text))
                        {
                            MessageBox.Show("請輸入暱稱!");
                            return;
                        }

                        var loginResult = this.svc.Login(new LoginDto() { NickName = this.tbNickName.Text });

                        if (loginResult.exception != null)
                        {
                            throw loginResult.exception;
                        }

                        if (!loginResult.response.Success)
                        {
                            MessageBox.Show(loginResult.response.ErrorMessage);
                            return;
                        }

                        this.User = loginResult.response.UserInfo;

                        break;
                    case "btnSend":

                        if (this.User == null)
                        {
                            MessageBox.Show("請先登入!");
                            return;
                        }

                        if (string.IsNullOrEmpty(this.tbSendMessage.Text))
                        {
                            MessageBox.Show("請輸入訊息!");
                            return;
                        }

                        this.hubClient.SendAction(new SendChatMessageAction()
                        {
                            NickName = this.User.NickName,
                            Message = this.tbSendMessage.Text,
                            CreateDateTime = DateTime.Now
                        });

                        this.tbSendMessage.Clear();

                        break;
                    default:
                        MessageBox.Show("無效的選項");
                        break;
                }
            }
            catch (Exception ex)
            {
                this.logger.Error(ex, $"{this.GetType().Name} ButtonOnClick Exception");
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 切換登入狀態
        /// </summary>
        private void ChangeLoginStatus()
        {
            var hubStatus =
                this.hubClient.State == ConnectionState.Connecting ? "連接中" :
                this.hubClient.State == ConnectionState.Reconnecting ? "重連中" :
                this.hubClient.State == ConnectionState.Connected ? "已連接" : "未連接";

            var loginStatus = this.User != null ? "已登入" : "未登入";

            this.tbStatus.Text = $"{hubStatus}-{loginStatus}";

            if (this.User == null || this.hubClient.State != ConnectionState.Connected)
            {
                this.User = null;
                this.tbNickName.Enabled = true;
                this.btnLogin.Enabled = true;
                this.tbSendMessage.Enabled = false;
                this.btnSend.Enabled = false;
            }
            else
            {
                this.tbNickName.Enabled = false;
                this.btnLogin.Enabled = false;
                this.tbSendMessage.Enabled = true;
                this.btnSend.Enabled = true;
            }
        }

        /// <summary>
        /// 更新聊天訊息
        /// </summary>
        /// <param name="text"></param>
        private void UpdateMessage(string text)
        {
            if (this.tbMessage.InvokeRequired)
            {
                this.tbMessage.Invoke(new SafeCallDelegate(UpdateMessage), text);
            }
            else
            {
                if(this.tbMessage.TextLength > 9999)
                {
                    this.tbMessage.Clear();
                }

                this.tbMessage.AppendText($"{text}\r\n");
            }
        }

        /// <summary>
        /// 畫面展開時觸發
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_Shown(object sender, EventArgs e)
        {
            this.timer.Start();
            this.hubClient.StartAsync();
        }
    }
}
