using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Printing;
using njapple.Model;

namespace njapple
{
    /// <summary>
    /// PrintWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PrintPreviewWindow : Window
    {
        static PrintDialog printDialog = new PrintDialog();
        public PrintPreviewWindow()
        {
            InitializeComponent();
        }

        public OrderInfo PrintOrderInfo
        {
            get;
            set;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tbSJAddress.Text = PrintOrderInfo.SJAddress;
            tbSJCity.Text = "";
            tbSJName.Text = PrintOrderInfo.SJName;
            tbSJNumber.Text = PrintOrderInfo.SJNumber;
            tbSJRemark.Text = PrintOrderInfo.SJRemark;
            try
            {
                if (printDialog.ShowDialog() == true)
                {
                    printDialog.PrintVisual(printGrid, "测试");
                }
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ee)
            {
                this.DialogResult = false;
            }
           
        }
    }
}
