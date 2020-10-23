namespace ReceiveCardAIO
{
    partial class IdentityVerify
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
            this.lbl_tip = new System.Windows.Forms.Label();
            this.IDPbox = new System.Windows.Forms.PictureBox();
            this.videoSource = new AForge.Controls.VideoSourcePlayer();
            this.lbl_msg = new System.Windows.Forms.Label();
            this.similarity = new System.Windows.Forms.Label();
            this.lbl_idInfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.IDPbox)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_tip
            // 
            this.lbl_tip.AutoSize = true;
            this.lbl_tip.ForeColor = System.Drawing.Color.Red;
            this.lbl_tip.Location = new System.Drawing.Point(12, 9);
            this.lbl_tip.Name = "lbl_tip";
            this.lbl_tip.Size = new System.Drawing.Size(173, 12);
            this.lbl_tip.TabIndex = 2;
            this.lbl_tip.Text = "请对准摄像头，不要遮挡脸部。";
            // 
            // IDPbox
            // 
            this.IDPbox.Location = new System.Drawing.Point(601, 28);
            this.IDPbox.Name = "IDPbox";
            this.IDPbox.Size = new System.Drawing.Size(147, 174);
            this.IDPbox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.IDPbox.TabIndex = 9;
            this.IDPbox.TabStop = false;
            // 
            // videoSource
            // 
            this.videoSource.Location = new System.Drawing.Point(12, 28);
            this.videoSource.Name = "videoSource";
            this.videoSource.Size = new System.Drawing.Size(555, 404);
            this.videoSource.TabIndex = 8;
            this.videoSource.Text = "videoSource";
            this.videoSource.VideoSource = null;
            this.videoSource.Paint += new System.Windows.Forms.PaintEventHandler(this.videoSource_Paint);
            // 
            // lbl_msg
            // 
            this.lbl_msg.AutoSize = true;
            this.lbl_msg.Location = new System.Drawing.Point(599, 420);
            this.lbl_msg.Name = "lbl_msg";
            this.lbl_msg.Size = new System.Drawing.Size(23, 12);
            this.lbl_msg.TabIndex = 10;
            this.lbl_msg.Text = "msg";
            // 
            // similarity
            // 
            this.similarity.AutoSize = true;
            this.similarity.Location = new System.Drawing.Point(601, 388);
            this.similarity.Name = "similarity";
            this.similarity.Size = new System.Drawing.Size(23, 12);
            this.similarity.TabIndex = 11;
            this.similarity.Text = "...";
            // 
            // lbl_idInfo
            // 
            this.lbl_idInfo.AutoSize = true;
            this.lbl_idInfo.Location = new System.Drawing.Point(601, 218);
            this.lbl_idInfo.Name = "lbl_idInfo";
            this.lbl_idInfo.Size = new System.Drawing.Size(65, 12);
            this.lbl_idInfo.TabIndex = 12;
            this.lbl_idInfo.Text = "lbl_idInfo";
            // 
            // IdentityVerify
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 443);
            this.Controls.Add(this.lbl_idInfo);
            this.Controls.Add(this.similarity);
            this.Controls.Add(this.lbl_msg);
            this.Controls.Add(this.IDPbox);
            this.Controls.Add(this.videoSource);
            this.Controls.Add(this.lbl_tip);
            this.Name = "IdentityVerify";
            this.Text = "身份核验";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.IdentityVerify_FormClosed);
            this.Load += new System.EventHandler(this.IdentityVerify_Load);
            ((System.ComponentModel.ISupportInitialize)(this.IDPbox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_tip;
        private System.Windows.Forms.PictureBox IDPbox;
        private AForge.Controls.VideoSourcePlayer videoSource;
        private System.Windows.Forms.Label lbl_msg;
        private System.Windows.Forms.Label similarity;
        private System.Windows.Forms.Label lbl_idInfo;
    }
}