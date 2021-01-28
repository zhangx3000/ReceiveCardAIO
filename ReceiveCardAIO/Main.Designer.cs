namespace ReceiveCardAIO
{
    partial class Main
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
            this.btn_Receive = new System.Windows.Forms.Button();
            this.btn_GiveOut = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_Receive
            // 
            this.btn_Receive.BackColor = System.Drawing.Color.Teal;
            this.btn_Receive.Location = new System.Drawing.Point(12, 153);
            this.btn_Receive.Name = "btn_Receive";
            this.btn_Receive.Size = new System.Drawing.Size(102, 93);
            this.btn_Receive.TabIndex = 3;
            this.btn_Receive.Text = "收卡";
            this.btn_Receive.UseVisualStyleBackColor = false;
            this.btn_Receive.Click += new System.EventHandler(this.btn_Receive_Click);
            // 
            // btn_GiveOut
            // 
            this.btn_GiveOut.BackColor = System.Drawing.Color.Teal;
            this.btn_GiveOut.Location = new System.Drawing.Point(12, 26);
            this.btn_GiveOut.Name = "btn_GiveOut";
            this.btn_GiveOut.Size = new System.Drawing.Size(102, 93);
            this.btn_GiveOut.TabIndex = 2;
            this.btn_GiveOut.Text = "发卡";
            this.btn_GiveOut.UseVisualStyleBackColor = false;
            this.btn_GiveOut.Click += new System.EventHandler(this.btn_GiveOut_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(8)))), ((int)(((byte)(26)))));
            this.ClientSize = new System.Drawing.Size(397, 278);
            this.Controls.Add(this.btn_Receive);
            this.Controls.Add(this.btn_GiveOut);
            this.Name = "Main";
            this.Text = "收发卡";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Receive;
        private System.Windows.Forms.Button btn_GiveOut;
    }
}