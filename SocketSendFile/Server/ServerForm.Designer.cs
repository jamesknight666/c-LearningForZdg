namespace Server
{
    partial class ServerForm
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
            this.IPNum = new System.Windows.Forms.Label();
            this.IPText = new System.Windows.Forms.TextBox();
            this.PortText = new System.Windows.Forms.TextBox();
            this.PortNum = new System.Windows.Forms.Label();
            this.StartBtn = new System.Windows.Forms.Button();
            this.TextBox = new System.Windows.Forms.TextBox();
            this.MsgBox = new System.Windows.Forms.TextBox();
            this.SendBtn = new System.Windows.Forms.Button();
            this.FileBtn = new System.Windows.Forms.Button();
            this.ClientList = new System.Windows.Forms.Label();
            this.ClientListBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // IPNum
            // 
            this.IPNum.AutoSize = true;
            this.IPNum.Location = new System.Drawing.Point(31, 51);
            this.IPNum.Name = "IPNum";
            this.IPNum.Size = new System.Drawing.Size(26, 20);
            this.IPNum.TabIndex = 0;
            this.IPNum.Text = "IP:";
            // 
            // IPText
            // 
            this.IPText.Location = new System.Drawing.Point(63, 48);
            this.IPText.Name = "IPText";
            this.IPText.Size = new System.Drawing.Size(125, 27);
            this.IPText.TabIndex = 1;
            this.IPText.Text = "127.0.0.1";
            // 
            // PortText
            // 
            this.PortText.Location = new System.Drawing.Point(260, 48);
            this.PortText.Name = "PortText";
            this.PortText.Size = new System.Drawing.Size(125, 27);
            this.PortText.TabIndex = 3;
            this.PortText.Text = "8080";
            // 
            // PortNum
            // 
            this.PortNum.AutoSize = true;
            this.PortNum.Location = new System.Drawing.Point(210, 51);
            this.PortNum.Name = "PortNum";
            this.PortNum.Size = new System.Drawing.Size(44, 20);
            this.PortNum.TabIndex = 2;
            this.PortNum.Text = "Port:";
            // 
            // StartBtn
            // 
            this.StartBtn.Location = new System.Drawing.Point(408, 48);
            this.StartBtn.Name = "StartBtn";
            this.StartBtn.Size = new System.Drawing.Size(94, 29);
            this.StartBtn.TabIndex = 4;
            this.StartBtn.Text = "Start";
            this.StartBtn.UseVisualStyleBackColor = true;
            this.StartBtn.Click += new System.EventHandler(this.StartBtn_Click);
            // 
            // TextBox
            // 
            this.TextBox.Location = new System.Drawing.Point(31, 92);
            this.TextBox.Multiline = true;
            this.TextBox.Name = "TextBox";
            this.TextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TextBox.Size = new System.Drawing.Size(735, 225);
            this.TextBox.TabIndex = 5;
            // 
            // MsgBox
            // 
            this.MsgBox.Location = new System.Drawing.Point(31, 341);
            this.MsgBox.Name = "MsgBox";
            this.MsgBox.Size = new System.Drawing.Size(464, 27);
            this.MsgBox.TabIndex = 6;
            // 
            // SendBtn
            // 
            this.SendBtn.Location = new System.Drawing.Point(523, 341);
            this.SendBtn.Name = "SendBtn";
            this.SendBtn.Size = new System.Drawing.Size(94, 29);
            this.SendBtn.TabIndex = 7;
            this.SendBtn.Text = "Send";
            this.SendBtn.UseVisualStyleBackColor = true;
            this.SendBtn.Click += new System.EventHandler(this.SendBtn_Click);
            // 
            // FileBtn
            // 
            this.FileBtn.Location = new System.Drawing.Point(633, 341);
            this.FileBtn.Name = "FileBtn";
            this.FileBtn.Size = new System.Drawing.Size(133, 29);
            this.FileBtn.TabIndex = 17;
            this.FileBtn.Text = "SendFile";
            this.FileBtn.UseVisualStyleBackColor = true;
            this.FileBtn.Click += new System.EventHandler(this.FileBtn_Click);
            // 
            // ClientList
            // 
            this.ClientList.AutoSize = true;
            this.ClientList.Location = new System.Drawing.Point(523, 51);
            this.ClientList.Name = "ClientList";
            this.ClientList.Size = new System.Drawing.Size(76, 20);
            this.ClientList.TabIndex = 18;
            this.ClientList.Text = "ClientList";
            // 
            // ClientListBox
            // 
            this.ClientListBox.FormattingEnabled = true;
            this.ClientListBox.Location = new System.Drawing.Point(615, 49);
            this.ClientListBox.Name = "ClientListBox";
            this.ClientListBox.Size = new System.Drawing.Size(151, 28);
            this.ClientListBox.TabIndex = 19;
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ClientListBox);
            this.Controls.Add(this.ClientList);
            this.Controls.Add(this.FileBtn);
            this.Controls.Add(this.SendBtn);
            this.Controls.Add(this.MsgBox);
            this.Controls.Add(this.TextBox);
            this.Controls.Add(this.StartBtn);
            this.Controls.Add(this.PortText);
            this.Controls.Add(this.PortNum);
            this.Controls.Add(this.IPText);
            this.Controls.Add(this.IPNum);
            this.Name = "ServerForm";
            this.Text = "ServerForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServerForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label IPNum;
        private TextBox IPText;
        private TextBox PortText;
        private Label PortNum;
        private Button StartBtn;
        private TextBox TextBox;
        private TextBox MsgBox;
        private Button SendBtn;
        private Button FileBtn;
        private Label ClientList;
        private ComboBox ClientListBox;
    }
}