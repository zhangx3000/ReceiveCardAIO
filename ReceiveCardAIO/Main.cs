using CRCXMODEM;
using ReceiveCardAIO.Common;
using System;
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
            idvForm.setFormResultValue += new setResultValue(IdentityVerifyFormTextValue);
            idvForm.ShowDialog();
        }
        //第五步：实现事件
        void IdentityVerifyFormTextValue(bool retValue)
        {
            if (retValue)
            {
                try
                {
                    //MessageBox.Show(FunLib.将卡移到出卡口持卡位);
                    //SerialHelper.ExCommand(FunLib.将卡移到出卡口持卡位);
                    Console.WriteLine("发卡成功");
                    idvForm.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 进行收卡操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Receive_Click(object sender, EventArgs e)
        {
            Console.WriteLine("回收卡成功");
        }
    }
}
