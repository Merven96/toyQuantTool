using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using  JQData.Samples;
using System.Data;

namespace quantTool
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void queryButton_Click(object sender, RoutedEventArgs e)
        {
            displayBlock.DataContext = null;

            Dictionary<string, string> showMsg = getShowMsg();
            if(showMsg != null)
            {
                try
                {
                    displayBlock.Text = DateTime.Now.ToString() +  "\n"
                     + "当前价格:\t" + showMsg["current_price"] + "\n" 
                     + "历史最高价格:\t" + showMsg["highest_price"] + "\n"
                     + "历史最低价格:\t" + showMsg["lowest_price"];
                }
                catch
                {
                    displayBlock.Text = "error";
                }
            }
            else
            {
                displayBlock.Text = "error";
            }


        }

        private Dictionary<string, string> getShowMsg()
        {
            string period = '1' + periodType.SelectedItem.ToString().Split(':')[1].Trim();
            string numStr = periodNum.Text;
            try
            {
                int numVal;
                numVal = Convert.ToInt32(numStr);
            }
            catch
            {
                return null;
            }
            string paramStr = "--period=" + period + "  --periodNum=" + numStr;
            Dictionary<string, string> queryRes = 
                QuantQuery.QuerySecurityInfo(paramStr);
            return queryRes;
        }
    }
}
