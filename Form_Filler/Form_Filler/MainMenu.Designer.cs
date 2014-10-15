namespace Form_Filler
{
    partial class MainMenu
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
            this.btnbuytkt = new System.Windows.Forms.Button();
            this.btnupdatetranslog = new System.Windows.Forms.Button();
            this.btnupdatesyndiwin = new System.Windows.Forms.Button();
            this.btnupdatesubpayment = new System.Windows.Forms.Button();
            this.btnsubmit = new System.Windows.Forms.Button();
            this.btncancel = new System.Windows.Forms.Button();
            this.rbtnbuytkt = new System.Windows.Forms.RadioButton();
            this.rbtnupdatetranslog = new System.Windows.Forms.RadioButton();
            this.rbtnupdatesyndiwin = new System.Windows.Forms.RadioButton();
            this.rbtnupdatesubpayment = new System.Windows.Forms.RadioButton();
            this.lblvalidationmsg = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnbuytkt
            // 
            this.btnbuytkt.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnbuytkt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnbuytkt.FlatAppearance.BorderSize = 0;
            this.btnbuytkt.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnbuytkt.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnbuytkt.ForeColor = System.Drawing.SystemColors.Window;
            this.btnbuytkt.Location = new System.Drawing.Point(45, 51);
            this.btnbuytkt.Name = "btnbuytkt";
            this.btnbuytkt.Size = new System.Drawing.Size(231, 41);
            this.btnbuytkt.TabIndex = 0;
            this.btnbuytkt.Text = "Buy Tickets";
            this.btnbuytkt.UseVisualStyleBackColor = false;
            this.btnbuytkt.Click += new System.EventHandler(this.btnbuytkt_Click);
            // 
            // btnupdatetranslog
            // 
            this.btnupdatetranslog.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnupdatetranslog.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnupdatetranslog.FlatAppearance.BorderSize = 0;
            this.btnupdatetranslog.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnupdatetranslog.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnupdatetranslog.ForeColor = System.Drawing.SystemColors.Window;
            this.btnupdatetranslog.Location = new System.Drawing.Point(45, 116);
            this.btnupdatetranslog.Name = "btnupdatetranslog";
            this.btnupdatetranslog.Size = new System.Drawing.Size(231, 41);
            this.btnupdatetranslog.TabIndex = 1;
            this.btnupdatetranslog.Text = " Update Transaction Logs";
            this.btnupdatetranslog.UseVisualStyleBackColor = false;
            this.btnupdatetranslog.Click += new System.EventHandler(this.btnupdatetranslog_Click);
            // 
            // btnupdatesyndiwin
            // 
            this.btnupdatesyndiwin.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnupdatesyndiwin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnupdatesyndiwin.FlatAppearance.BorderSize = 0;
            this.btnupdatesyndiwin.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnupdatesyndiwin.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnupdatesyndiwin.ForeColor = System.Drawing.SystemColors.Window;
            this.btnupdatesyndiwin.Location = new System.Drawing.Point(45, 183);
            this.btnupdatesyndiwin.Name = "btnupdatesyndiwin";
            this.btnupdatesyndiwin.Size = new System.Drawing.Size(231, 41);
            this.btnupdatesyndiwin.TabIndex = 2;
            this.btnupdatesyndiwin.Text = "Update Syndicate Winning";
            this.btnupdatesyndiwin.UseVisualStyleBackColor = false;
            this.btnupdatesyndiwin.Click += new System.EventHandler(this.btnupdatesyndiwin_Click);
            // 
            // btnupdatesubpayment
            // 
            this.btnupdatesubpayment.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnupdatesubpayment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnupdatesubpayment.FlatAppearance.BorderSize = 0;
            this.btnupdatesubpayment.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnupdatesubpayment.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnupdatesubpayment.ForeColor = System.Drawing.SystemColors.Window;
            this.btnupdatesubpayment.Location = new System.Drawing.Point(45, 249);
            this.btnupdatesubpayment.Name = "btnupdatesubpayment";
            this.btnupdatesubpayment.Size = new System.Drawing.Size(231, 56);
            this.btnupdatesubpayment.TabIndex = 3;
            this.btnupdatesubpayment.Text = "Update Subscription Payment";
            this.btnupdatesubpayment.UseVisualStyleBackColor = false;
            this.btnupdatesubpayment.Click += new System.EventHandler(this.btnupdatesubpayment_Click);
            // 
            // btnsubmit
            // 
            this.btnsubmit.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsubmit.Location = new System.Drawing.Point(120, 335);
            this.btnsubmit.Name = "btnsubmit";
            this.btnsubmit.Size = new System.Drawing.Size(122, 33);
            this.btnsubmit.TabIndex = 4;
            this.btnsubmit.Text = "Submit";
            this.btnsubmit.UseVisualStyleBackColor = true;
            this.btnsubmit.Click += new System.EventHandler(this.btnsubmit_Click);
            // 
            // btncancel
            // 
            this.btncancel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncancel.Location = new System.Drawing.Point(326, 335);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(122, 33);
            this.btncancel.TabIndex = 5;
            this.btncancel.Text = "Cancel";
            this.btncancel.UseVisualStyleBackColor = true;
            this.btncancel.Click += new System.EventHandler(this.btncancel_Click);
            // 
            // rbtnbuytkt
            // 
            this.rbtnbuytkt.AutoSize = true;
            this.rbtnbuytkt.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnbuytkt.Location = new System.Drawing.Point(300, 61);
            this.rbtnbuytkt.Name = "rbtnbuytkt";
            this.rbtnbuytkt.Size = new System.Drawing.Size(101, 18);
            this.rbtnbuytkt.TabIndex = 6;
            this.rbtnbuytkt.TabStop = true;
            this.rbtnbuytkt.Text = "Buy Tickets";
            this.rbtnbuytkt.UseVisualStyleBackColor = true;
            this.rbtnbuytkt.CheckedChanged += new System.EventHandler(this.rbtnbuytkt_CheckedChanged);
            // 
            // rbtnupdatetranslog
            // 
            this.rbtnupdatetranslog.AutoSize = true;
            this.rbtnupdatetranslog.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnupdatetranslog.Location = new System.Drawing.Point(300, 126);
            this.rbtnupdatetranslog.Name = "rbtnupdatetranslog";
            this.rbtnupdatetranslog.Size = new System.Drawing.Size(188, 18);
            this.rbtnupdatetranslog.TabIndex = 7;
            this.rbtnupdatetranslog.TabStop = true;
            this.rbtnupdatetranslog.Text = "Update Transaction Logs";
            this.rbtnupdatetranslog.UseVisualStyleBackColor = true;
            this.rbtnupdatetranslog.CheckedChanged += new System.EventHandler(this.rbtnupdatetranslog_CheckedChanged);
            // 
            // rbtnupdatesyndiwin
            // 
            this.rbtnupdatesyndiwin.AutoSize = true;
            this.rbtnupdatesyndiwin.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnupdatesyndiwin.Location = new System.Drawing.Point(300, 193);
            this.rbtnupdatesyndiwin.Name = "rbtnupdatesyndiwin";
            this.rbtnupdatesyndiwin.Size = new System.Drawing.Size(199, 18);
            this.rbtnupdatesyndiwin.TabIndex = 8;
            this.rbtnupdatesyndiwin.TabStop = true;
            this.rbtnupdatesyndiwin.Text = "Update Syndicate Winning";
            this.rbtnupdatesyndiwin.UseVisualStyleBackColor = true;
            this.rbtnupdatesyndiwin.CheckedChanged += new System.EventHandler(this.rbtnupdatesyndiwin_CheckedChanged);
            // 
            // rbtnupdatesubpayment
            // 
            this.rbtnupdatesubpayment.AutoSize = true;
            this.rbtnupdatesubpayment.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnupdatesubpayment.Location = new System.Drawing.Point(300, 267);
            this.rbtnupdatesubpayment.Name = "rbtnupdatesubpayment";
            this.rbtnupdatesubpayment.Size = new System.Drawing.Size(220, 18);
            this.rbtnupdatesubpayment.TabIndex = 9;
            this.rbtnupdatesubpayment.TabStop = true;
            this.rbtnupdatesubpayment.Text = "Update Subscription Payment";
            this.rbtnupdatesubpayment.UseVisualStyleBackColor = true;
            this.rbtnupdatesubpayment.CheckedChanged += new System.EventHandler(this.rbtnupdatesubpayment_CheckedChanged);
            // 
            // lblvalidationmsg
            // 
            this.lblvalidationmsg.AutoSize = true;
            this.lblvalidationmsg.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblvalidationmsg.ForeColor = System.Drawing.Color.Red;
            this.lblvalidationmsg.Location = new System.Drawing.Point(117, 383);
            this.lblvalidationmsg.Name = "lblvalidationmsg";
            this.lblvalidationmsg.Size = new System.Drawing.Size(0, 17);
            this.lblvalidationmsg.TabIndex = 10;
            this.lblvalidationmsg.Visible = false;
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(566, 438);
            this.Controls.Add(this.lblvalidationmsg);
            this.Controls.Add(this.rbtnupdatesubpayment);
            this.Controls.Add(this.rbtnupdatesyndiwin);
            this.Controls.Add(this.rbtnupdatetranslog);
            this.Controls.Add(this.rbtnbuytkt);
            this.Controls.Add(this.btncancel);
            this.Controls.Add(this.btnsubmit);
            this.Controls.Add(this.btnupdatesubpayment);
            this.Controls.Add(this.btnupdatesyndiwin);
            this.Controls.Add(this.btnupdatetranslog);
            this.Controls.Add(this.btnbuytkt);
            this.Name = "MainMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainMenu";
            this.Load += new System.EventHandler(this.MainMenu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnbuytkt;
        private System.Windows.Forms.Button btnupdatetranslog;
        private System.Windows.Forms.Button btnupdatesyndiwin;
        private System.Windows.Forms.Button btnupdatesubpayment;
        private System.Windows.Forms.Button btnsubmit;
        private System.Windows.Forms.Button btncancel;
        private System.Windows.Forms.RadioButton rbtnbuytkt;
        private System.Windows.Forms.RadioButton rbtnupdatetranslog;
        private System.Windows.Forms.RadioButton rbtnupdatesyndiwin;
        private System.Windows.Forms.RadioButton rbtnupdatesubpayment;
        private System.Windows.Forms.Label lblvalidationmsg;
    }
}