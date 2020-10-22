using CRCXMODEM;
using ReceiveCardAIO.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReceiveCardAIO
{
    public partial class Main : Form
    {
        private IdentityVerify idvForm;
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            //主窗口加载方法
        }

        /// <summary>
        /// 进行发卡操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_GiveOut_Click(object sender, EventArgs e)
        {
            //弹出身份验证窗口
            idvForm = new IdentityVerify();
            //第四步：初始化事件
            idvForm.setFormTextValue += new setTextValue(IdentityVerifyFormTextValue);

            idvForm.ShowDialog();
        }

        //第五步：实现事件
        void IdentityVerifyFormTextValue(bool txtValue)
        {
            //具体实现
            //MessageBox.Show(txtValue);

            if(txtValue)
            {
                try
                {
                    //MessageBox.Show(txtValue.ToString());

                    //SerialHelper.ExCommand(FunLib.将卡移到出卡口持卡位);

                    Console.WriteLine("发卡成功");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                
            }
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
            //Application.Exit();
            //Environment.Exit(0);
        }
    }
}
