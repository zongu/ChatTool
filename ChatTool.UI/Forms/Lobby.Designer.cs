
namespace ChatTool.UI.Forms
{
    partial class Lobby
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnBackend = new System.Windows.Forms.Button();
            this.btnFrontend = new System.Windows.Forms.Button();
            this.btnQuict = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnBackend
            // 
            this.btnBackend.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnBackend.Location = new System.Drawing.Point(12, 12);
            this.btnBackend.Name = "btnBackend";
            this.btnBackend.Size = new System.Drawing.Size(260, 31);
            this.btnBackend.TabIndex = 0;
            this.btnBackend.Text = "後台";
            this.btnBackend.UseVisualStyleBackColor = true;
            this.btnBackend.Click += new System.EventHandler(this.ButtonOnClick);
            // 
            // btnFrontend
            // 
            this.btnFrontend.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnFrontend.Location = new System.Drawing.Point(12, 49);
            this.btnFrontend.Name = "btnFrontend";
            this.btnFrontend.Size = new System.Drawing.Size(260, 31);
            this.btnFrontend.TabIndex = 1;
            this.btnFrontend.Text = "前台";
            this.btnFrontend.UseVisualStyleBackColor = true;
            this.btnFrontend.Click += new System.EventHandler(this.ButtonOnClick);
            // 
            // btnQuict
            // 
            this.btnQuict.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnQuict.Location = new System.Drawing.Point(12, 86);
            this.btnQuict.Name = "btnQuict";
            this.btnQuict.Size = new System.Drawing.Size(260, 31);
            this.btnQuict.TabIndex = 2;
            this.btnQuict.Text = "離開";
            this.btnQuict.UseVisualStyleBackColor = true;
            this.btnQuict.Click += new System.EventHandler(this.ButtonOnClick);
            // 
            // Lobby
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 126);
            this.Controls.Add(this.btnQuict);
            this.Controls.Add(this.btnFrontend);
            this.Controls.Add(this.btnBackend);
            this.Name = "Lobby";
            this.Text = "Lobby";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnBackend;
        private System.Windows.Forms.Button btnFrontend;
        private System.Windows.Forms.Button btnQuict;
    }
}