
namespace ChatTool.UI.Forms
{
    using System;
    using System.Windows.Forms;
    using NLog;

    public partial class Lobby : Form
    {
        private ILogger logger = LogManager.GetLogger("ChatToolUI");

        public Lobby()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
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
                    case "btnBackend":
                    case "btnFrontend":
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                        break;
                    case "btnQuict":
                        this.DialogResult = DialogResult.Cancel;
                        this.Close();
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
    }
}
