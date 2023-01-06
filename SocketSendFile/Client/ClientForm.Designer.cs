namespace Client
{
    partial class ClientForm
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
            this.SendBtn = new System.Windows.Forms.Button();
            this.MsgBox = new System.Windows.Forms.TextBox();
            this.TextBox = new System.Windows.Forms.TextBox();
            this.ContBtn = new System.Windows.Forms.Button();
            this.PortText = new System.Windows.Forms.TextBox();
            this.PortNum = new System.Windows.Forms.Label();
            this.IPText = new System.Windows.Forms.TextBox();
            this.IPNum = new System.Windows.Forms.Label();
            this.FileBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SendBtn
            // 
            this.SendBtn.Location = new System.Drawing.Point(599, 358);
            this.SendBtn.Name = "SendBtn";
            this.SendBtn.Size = new System.Drawing.Size(94, 29);
            this.SendBtn.TabIndex = 15;
            this.SendBtn.Text = "Send";
            this.SendBtn.UseVisualStyleBackColor = true;
            this.SendBtn.Click += new System.EventHandler(this.SendBtn_Click);
            // 
            // MsgBox
            // 
            this.MsgBox.Location = new System.Drawing.Point(107, 360);
            this.MsgBox.Name = "MsgBox";
            this.MsgBox.Size = new System.Drawing.Size(464, 27);
            this.MsgBox.TabIndex = 14;
            // 
            // TextBox
            // 
            this.TextBox.Location = new System.Drawing.Point(107, 123);
            this.TextBox.Multiline = true;
            this.TextBox.Name = "TextBox";
            this.TextBox.Size = new System.Drawing.Size(586, 207);
            this.TextBox.TabIndex = 13;
            // 
            // ContBtn
            // 
            this.ContBtn.Location = new System.Drawing.Point(599, 64);
            this.ContBtn.Name = "ContBtn";
            this.ContBtn.Size = new System.Drawing.Size(94, 29);
            this.ContBtn.TabIndex = 12;
            this.ContBtn.Text = "Connect";
            this.ContBtn.UseVisualStyleBackColor = true;
            this.ContBtn.Click += new System.EventHandler(this.ContBtn_Click);
            // 
            // PortText
            // 
            this.PortText.Location = new System.Drawing.Point(424, 65);
            this.PortText.Name = "PortText";
            this.PortText.Size = new System.Drawing.Size(125, 27);
            this.PortText.TabIndex = 11;
            this.PortText.Text = "8080";
            // 
            // PortNum
            // 
            this.PortNum.AutoSize = true;
            this.PortNum.Location = new System.Drawing.Point(351, 68);
            this.PortNum.Name = "PortNum";
            this.PortNum.Size = new System.Drawing.Size(44, 20);
            this.PortNum.TabIndex = 10;
            this.PortNum.Text = "Port:";
            // 
            // IPText
            // 
            this.IPText.Location = new System.Drawing.Point(180, 65);
            this.IPText.Name = "IPText";
            this.IPText.Size = new System.Drawing.Size(125, 27);
            this.IPText.TabIndex = 9;
            this.IPText.Text = "127.0.0.1";
            // 
            // IPNum
            // 
            this.IPNum.AutoSize = true;
            this.IPNum.Location = new System.Drawing.Point(107, 68);
            this.IPNum.Name = "IPNum";
            this.IPNum.Size = new System.Drawing.Size(26, 20);
            this.IPNum.TabIndex = 8;
            this.IPNum.Text = "IP:";
            // 
            // FileBtn
            // 
            this.FileBtn.Location = new System.Drawing.Point(438, 393);
            this.FileBtn.Name = "FileBtn";
            this.FileBtn.Size = new System.Drawing.Size(133, 29);
            this.FileBtn.TabIndex = 16;
            this.FileBtn.Text = "SendFile";
            this.FileBtn.UseVisualStyleBackColor = true;
            this.FileBtn.Click += new System.EventHandler(this.FileBtn_Click);
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.FileBtn);
            this.Controls.Add(this.SendBtn);
            this.Controls.Add(this.MsgBox);
            this.Controls.Add(this.TextBox);
            this.Controls.Add(this.ContBtn);
            this.Controls.Add(this.PortText);
            this.Controls.Add(this.PortNum);
            this.Controls.Add(this.IPText);
            this.Controls.Add(this.IPNum);
            this.Name = "ClientForm";
            this.Text = "ClientForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ClientForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button SendBtn;
        private TextBox MsgBox;
        private TextBox TextBox;
        private Button ContBtn;
        private TextBox PortText;
        private Label PortNum;
        private TextBox IPText;
        private Label IPNum;
        private Button FileBtn;
    }
}