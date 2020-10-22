namespace ReceiveCardAIO
{
    partial class Main
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_GiveOut = new System.Windows.Forms.Button();
            this.btn_Receive = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_GiveOut
            // 
            this.btn_GiveOut.Location = new System.Drawing.Point(67, 27);
            this.btn_GiveOut.Name = "btn_GiveOut";
            this.btn_GiveOut.Size = new System.Drawing.Size(102, 93);
            this.btn_GiveOut.TabIndex = 0;
            this.btn_GiveOut.Text = "发卡";
            this.btn_GiveOut.UseVisualStyleBackColor = true;
            this.btn_GiveOut.Click += new System.EventHandler(this.btn_GiveOut_Click);
            // 
            // btn_Receive
            // 
            this.btn_Receive.Location = new System.Drawing.Point(67, 185);
            this.btn_Receive.Name = "btn_Receive";
            this.btn_Receive.Size = new System.Drawing.Size(102, 93);
            this.btn_Receive.TabIndex = 1;
            this.btn_Receive.Text = "收卡";
            this.btn_Receive.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 313);
            this.Controls.Add(this.btn_Receive);
            this.Controls.Add(this.btn_GiveOut);
            this.Name = "Main";
            this.Text = "收发卡";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_FormClosed);
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_GiveOut;
        private System.Windows.Forms.Button btn_Receive;
    }
}

