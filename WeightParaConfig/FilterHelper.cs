using System;

namespace WeightParaConfig
{
    public class FilterHelper
    {
        /// <summary>
        /// 限幅防抖滤波法
        /// </summary>
        /// <param name="ADNum"采样值></param>
        /// RANG：幅度  LPNUM :测试计数
        /// <returns></returns>
        private const double RANGE = 0.002;
        private const int LPNUM = 5;
        private static double currentValue = 0;
        private static int tmpCount = 0;
        private static bool isFirstLF = true;

        private static int okCount = 0;//稳定了多少次
        /// <summary>
        /// 对结果进行过滤
        /// </summary>
        /// <param name="ADNum"></param>
        /// <returns></returns>
        public static bool LimitFilterAD(double ADNum)
        {
            if (isFirstLF)
            {
                isFirstLF = false;
                currentValue = ADNum;
                //Console.WriteLine("第一次计数：" + currentValue);
            }
            if (Math.Abs(ADNum - currentValue) > RANGE)//如果平均值大于幅度
            {
                if (tmpCount++ > LPNUM)//临时数是否大于测试数，如果大于测试数，进行下一次过滤
                {
                    isFirstLF = true;    // 初始化滑动平均值
                    tmpCount = 0;
                    currentValue = ADNum;
                    //Console.WriteLine("又一次重新计数，当前值：" + currentValue);
                }
                //Console.WriteLine("不稳定,当前值：" + currentValue + "," + "差距：" + Math.Abs(ADNum - currentValue));
                okCount = 0;
                return false;
            }
            else//如果小于幅度，说明稳定
            {
                //Console.WriteLine("稳定：" + currentValue);
                if (okCount++ > 150)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                //return true;
            }
        }
    }
}
