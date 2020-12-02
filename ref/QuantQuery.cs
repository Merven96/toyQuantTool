using System;
using System.Net.Http;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;


namespace JQData.Samples
{
    class QuantQuery
    {
        public static Dictionary<string, string> QuerySecurityInfo(string pyParam,
            string exePath = @"F:\temp_code\biquant\mv_programe.exe")
        {
            /* parameters example:
             * paramStr = "--period=1day  --periodNum=15";
             * pyexePath = @"F:\temp_code\biquant\mv_programe.exe";
             * 
             *  return example:
             *  btc-usdt result:
             *  current-time: Wed Dec  2 14:24:50 2020
             *  current_price: 18774.81
             *  highest-price_in_past_7_day:19888.0$
             *  lowest-price_in_past_7_day:16199.86$
             */
            Dictionary<string, string> res = new Dictionary<string, string>();

            Process p = new Process();
            p.StartInfo.FileName = exePath;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.Arguments = pyParam;
            p.Start();
            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            p.Close();

            //Console.WriteLine(output);
            //Console.WriteLine("test the info:");
            //Console.WriteLine(output.Length);

            if (output.StartsWith("fail"))
            {
                return res;
            }
            else
            {
                string[] resList = output.Split("\n");
                for (int i = 1; i < resList.Length; i++)
                {
                    string[] lineElem;
                    if (resList[i].Length > 0)
                    {
                        lineElem = resList[i].Split(":");
                        if (lineElem.Length > 0)
                            res[lineElem[0]] = lineElem[1];
                    }
                }
                return res;
            }
        }
    };

    class Program
    {
        static void Main(string[] args)
        {
            string paramStr = "--period=1day  --periodNum=15";
            string pyexePath = @"F:\temp_code\biquant\mv_programe.exe";
            Dictionary<string, string> queryRes = QuantQuery.QuerySecurityInfo(paramStr, pyexePath);
            foreach(string keyElem in queryRes.Keys)
            {
                Console.WriteLine(keyElem + " :  " + queryRes[keyElem]);
            }
        }

    }
}
