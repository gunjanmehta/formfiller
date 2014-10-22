namespace Form_Filler
{
    partial class The_National_Lottery_Sign_in
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
            this.pnlbrowser = new System.Windows.Forms.Panel();
            this.btnpaste = new System.Windows.Forms.Button();
            this.btngo = new System.Windows.Forms.Button();
            this.txturl = new System.Windows.Forms.TextBox();
            this.lblurl = new System.Windows.Forms.Label();
            this.websignin = new System.Windows.Forms.WebBrowser();
            this.pnlmessage = new System.Windows.Forms.Panel();
            this.lblmessage = new System.Windows.Forms.Label();
            this.lblmessagewindowtitle = new System.Windows.Forms.Label();
            this.pnlbrowser.SuspendLayout();
            this.pnlmessage.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlbrowser
            // 
            this.pnlbrowser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pnlbrowser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlbrowser.Controls.Add(this.btnpaste);
            this.pnlbrowser.Controls.Add(this.btngo);
            this.pnlbrowser.Controls.Add(this.txturl);
            this.pnlbrowser.Controls.Add(this.lblurl);
            this.pnlbrowser.Location = new System.Drawing.Point(1, 12);
            this.pnlbrowser.Name = "pnlbrowser";
            this.pnlbrowser.Size = new System.Drawing.Size(890, 75);
            this.pnlbrowser.TabIndex = 0;
            // 
            // btnpaste
            // 
            this.btnpaste.Enabled = false;
            this.btnpaste.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnpaste.Location = new System.Drawing.Point(768, 5);
            this.btnpaste.Name = "btnpaste";
            this.btnpaste.Size = new System.Drawing.Size(77, 23);
            this.btnpaste.TabIndex = 3;
            this.btnpaste.Text = "Paste";
            this.btnpaste.UseVisualStyleBackColor = true;
            this.btnpaste.Click += new System.EventHandler(this.btnpaste_Click);
            // 
            // btngo
            // 
            this.btngo.Enabled = false;
            this.btngo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btngo.Location = new System.Drawing.Point(674, 5);
            this.btngo.Name = "btngo";
            this.btngo.Size = new System.Drawing.Size(77, 23);
            this.btngo.TabIndex = 2;
            this.btngo.Text = "Go";
            this.btngo.UseVisualStyleBackColor = true;
            this.btngo.Click += new System.EventHandler(this.btngo_Click);
            // 
            // txturl
            // 
            this.txturl.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txturl.Location = new System.Drawing.Point(55, 7);
            this.txturl.Name = "txturl";
            this.txturl.Size = new System.Drawing.Size(601, 22);
            this.txturl.TabIndex = 1;
            // 
            // lblurl
            // 
            this.lblurl.AutoSize = true;
            this.lblurl.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblurl.Location = new System.Drawing.Point(12, 9);
            this.lblurl.Name = "lblurl";
            this.lblurl.Size = new System.Drawing.Size(36, 17);
            this.lblurl.TabIndex = 0;
            this.lblurl.Text = "URL";
            // 
            // websignin
            // 
            this.websignin.Location = new System.Drawing.Point(0, 88);
            this.websignin.MinimumSize = new System.Drawing.Size(20, 20);
            this.websignin.Name = "websignin";
            this.websignin.Size = new System.Drawing.Size(1352, 618);
            this.websignin.TabIndex = 1;
            this.websignin.Url = new System.Uri("", System.UriKind.Relative);
            this.websignin.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.websignin_DocumentCompleted);
            // 
            // pnlmessage
            // 
            this.pnlmessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlmessage.Controls.Add(this.lblmessage);
            this.pnlmessage.Controls.Add(this.lblmessagewindowtitle);
            this.pnlmessage.Location = new System.Drawing.Point(888, 12);
            this.pnlmessage.Name = "pnlmessage";
            this.pnlmessage.Size = new System.Drawing.Size(464, 75);
            this.pnlmessage.TabIndex = 2;
            // 
            // lblmessage
            // 
            this.lblmessage.AutoSize = true;
            this.lblmessage.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblmessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblmessage.Location = new System.Drawing.Point(26, 41);
            this.lblmessage.Name = "lblmessage";
            this.lblmessage.Size = new System.Drawing.Size(0, 17);
            this.lblmessage.TabIndex = 1;
            // 
            // lblmessagewindowtitle
            // 
            this.lblmessagewindowtitle.AutoSize = true;
            this.lblmessagewindowtitle.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblmessagewindowtitle.Location = new System.Drawing.Point(23, 9);
            this.lblmessagewindowtitle.Name = "lblmessagewindowtitle";
            this.lblmessagewindowtitle.Size = new System.Drawing.Size(143, 17);
            this.lblmessagewindowtitle.TabIndex = 0;
            this.lblmessagewindowtitle.Text = "Message Window";
            // 
            // The_National_Lottery_Sign_in
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1353, 708);
            this.Controls.Add(this.pnlmessage);
            this.Controls.Add(this.websignin);
            this.Controls.Add(this.pnlbrowser);
            this.Name = "The_National_Lottery_Sign_in";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "The National Lottery | Sign in";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.The_National_Lottery_Sign_in_Load);
            this.pnlbrowser.ResumeLayout(false);
            this.pnlbrowser.PerformLayout();
            this.pnlmessage.ResumeLayout(false);
            this.pnlmessage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlbrowser;
        private System.Windows.Forms.Button btnpaste;
        private System.Windows.Forms.Button btngo;
        private System.Windows.Forms.TextBox txturl;
        private System.Windows.Forms.Label lblurl;
        private System.Windows.Forms.WebBrowser websignin;
        private System.Windows.Forms.Panel pnlmessage;
        private System.Windows.Forms.Label lblmessagewindowtitle;
        private System.Windows.Forms.Label lblmessage;
    }
}