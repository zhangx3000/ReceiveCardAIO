using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeightParaConfig
{
    class Program
    {
        static void Main(string[] args)
        {
            //调用方式
            SerialCommonOperation trans = new SerialCommonOperation("GetPoundParaInfoById", "F5CFC56B-DBB4-4B32-8538-7E71EF790BFF");
            trans.setWeightResultValue += new setResultValue(GetValue);
            Console.ReadLine();
        }
        static void GetValue(double retValue)
        {
            bool isOk = FilterHelper.LimitFilterAD(retValue);
            Console.WriteLine(isOk);
            if (isOk)
            {
                Console.WriteLine(retValue);
            }
        }
    }
}
