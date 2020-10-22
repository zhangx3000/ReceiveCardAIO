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
            this.pic_Camera = new System.Windows.Forms.PictureBox();
            this.lbl_tip = new System.Windows.Forms.Label();
            this.pic_Capture = new System.Windows.Forms.PictureBox();
            this.lbl_Capture = new System.Windows.Forms.Label();
            this.lbl_IdCard = new System.Windows.Forms.Label();
            this.pic_IdCard = new System.Windows.Forms.PictureBox();
            this.lbl_msg = new System.Windows.Forms.Label();
            this.lbl_tipcut = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Camera)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Capture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_IdCard)).BeginInit();
            this.SuspendLayout();
            // 
            // pic_Camera
            // 
            this.pic_Camera.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pic_Camera.Location = new System.Drawing.Point(13, 35);
            this.pic_Camera.Name = "pic_Camera";
            this.pic_Camera.Size = new System.Drawing.Size(553, 417);
            this.pic_Camera.TabIndex = 0;
            this.pic_Camera.TabStop = false;
            // 
            // lbl_tip
            // 
            this.lbl_tip.AutoSize = true;
            this.lbl_tip.ForeColor = System.Drawing.Color.Red;
            this.lbl_tip.Location = new System.Drawing.Point(12, 11);
            this.lbl_tip.Name = "lbl_tip";
            this.lbl_tip.Size = new System.Drawing.Size(317, 12);
            this.lbl_tip.TabIndex = 1;
            this.lbl_tip.Text = "请对准摄像头，保持脸部在红框范围之内，不要遮挡脸部。";
            // 
            // pic_Capture
            // 
            this.pic_Capture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pic_Capture.Location = new System.Drawing.Point(610, 38);
            this.pic_Capture.Name = "pic_Capture";
            this.pic_Capture.Size = new System.Drawing.Size(133, 154);
            this.pic_Capture.TabIndex = 2;
            this.pic_Capture.TabStop = false;
            // 
            // lbl_Capture
            // 
            this.lbl_Capture.Location = new System.Drawing.Point(585, 98);
            this.lbl_Capture.Name = "lbl_Capture";
            this.lbl_Capture.Size = new System.Drawing.Size(19, 53);
            this.lbl_Capture.TabIndex = 3;
            this.lbl_Capture.Text = "抓拍头像";
            // 
            // lbl_IdCard
            // 
            this.lbl_IdCard.Location = new System.Drawing.Point(585, 276);
            this.lbl_IdCard.Name = "lbl_IdCard";
            this.lbl_IdCard.Size = new System.Drawing.Size(19, 63);
            this.lbl_IdCard.TabIndex = 4;
            this.lbl_IdCard.Text = "身份证头像";
            // 
            // pic_IdCard
            // 
            this.pic_IdCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pic_IdCard.Location = new System.Drawing.Point(610, 226);
            this.pic_IdCard.Name = "pic_IdCard";
            this.pic_IdCard.Size = new System.Drawing.Size(133, 154);
            this.pic_IdCard.TabIndex = 5;
            this.pic_IdCard.TabStop = false;
            // 
            // lbl_msg
            // 
            this.lbl_msg.AutoSize = true;
            this.lbl_msg.Location = new System.Drawing.Point(585, 422);
            this.lbl_msg.Name = "lbl_msg";
            this.lbl_msg.Size = new System.Drawing.Size(53, 12);
            this.lbl_msg.TabIndex = 6;
            this.lbl_msg.Text = "比对结果";
            // 
            // lbl_tipcut
            // 
            this.lbl_tipcut.AutoSize = true;
            this.lbl_tipcut.ForeColor = System.Drawing.Color.Blue;
            this.lbl_tipcut.Location = new System.Drawing.Point(610, 12);
            this.lbl_tipcut.Name = "lbl_tipcut";
            this.lbl_tipcut.Size = new System.Drawing.Size(137, 12);
            this.lbl_tipcut.TabIndex = 7;
            this.lbl_tipcut.Text = "请刷身份证进行头像抓拍";
            // 
            // IdentityVerify
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(756, 477);
            this.Controls.Add(this.lbl_tipcut);
            this.Controls.Add(this.lbl_msg);
            this.Controls.Add(this.pic_IdCard);
            this.Controls.Add(this.lbl_IdCard);
            this.Controls.Add(this.lbl_Capture);
            this.Controls.Add(this.pic_Capture);
            this.Controls.Add(this.lbl_tip);
            this.Controls.Add(this.pic_Camera);
            this.Name = "IdentityVerify";
            this.Text = "身份核验";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.IdentityVerify_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pic_Camera)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Capture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_IdCard)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pic_Camera;
        private System.Windows.Forms.Label lbl_tip;
        private System.Windows.Forms.PictureBox pic_Capture;
        private System.Windows.Forms.Label lbl_Capture;
        private System.Windows.Forms.Label lbl_IdCard;
        private System.Windows.Forms.PictureBox pic_IdCard;
        private System.Windows.Forms.Label lbl_msg;
        private System.Windows.Forms.Label lbl_tipcut;
    }
}