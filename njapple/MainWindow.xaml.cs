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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFHelper.Window;
using ExcelDao;
using njapple.Model;
using Microsoft.Win32;
using njapple.Tools;
using System.Threading.Tasks;

namespace njapple
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        List<OrderInfo> orderInfoList = new List<OrderInfo>();
        int count = 0;//导入的总件数
        public MainWindow()
        {
            InitializeComponent();
            WindowHelper.RepairWindowBehavior(this);
        }

        #region 最大 最小 关闭 拖动

        //拖动
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.WindowState != WindowState.Maximized)
            {
                try
                {
                    Point mousePoint = e.GetPosition(this);

                    if (e.ButtonState == System.Windows.Input.MouseButtonState.Pressed && e.ChangedButton == System.Windows.Input.MouseButton.Left && mousePoint.Y <= 40)
                    {

                        this.DragMove();
                    }
                }
                catch { }
            }
        }

        //关闭
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("确认退出软件吗？", "系统信息", MessageBoxButton.YesNo, MessageBoxImage.Asterisk) == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }
        //最小化
        private void btnMin_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        //最大化
        private void btnMax_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                double screenHeight = SystemParameters.PrimaryScreenHeight;
                double screenWidth = SystemParameters.PrimaryScreenWidth;

                this.Left = (screenWidth - this.Width) / 2;
                this.Top = (screenHeight - this.Height) / 2;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }
        }

        #endregion

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            orderInfoList = new List<OrderInfo>();
            string fileName="";
            if (ShowFileDialog.ShowSaveFileDialog(out fileName,ShowFileDialog.excelFilter,ShowFileDialog.excelExt,"")==true)
            {
                maskGrid.Visibility = Visibility.Visible;

                Task task = new Task(new Action(
                        () =>
                        {
                            List<List<string>> datas = ExcelUtil.Read(fileName, 6, 2);
                         
                            foreach (var item in datas)
                            {
                                OrderInfo orderInfo = new OrderInfo();
                                orderInfo.Index = item[0];
                                orderInfo.OrderCode = item[1];
                                orderInfo.SJName = item[2];
                                orderInfo.SJNumber = item[3];
                                orderInfo.SJAddress = item[4];
                                orderInfo.SJRemark = item[6];
                                try
                                {
                                    orderInfo.Count = int.Parse(item[5]);
                                    count += orderInfo.Count;
                                }
                                catch
                                {
                                    MessageBox.Show("件数读取出错！", "系统错误", MessageBoxButton.OK, MessageBoxImage.Error);
                                    orderInfoList.Clear();
                                    count = 0;
                                    break;
                                }
                                orderInfoList.Add(orderInfo);
                            }

                            this.Dispatcher.Invoke(new Action(() =>
                            {
                                datagrid.SetBinding(DataGrid.ItemsSourceProperty, new Binding() { Source = orderInfoList });
                                maskGrid.Visibility = Visibility.Hidden;
                                tbStatu.Text = string.Format("共导入{0}条订单，总件数{1}件。", orderInfoList.Count, count);
                            }));
                        }
                    ));
                task.Start();
            }
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            if (orderInfoList.Count==0)
            {
                MessageBox.Show("没有订单信息！","系统信息");
                return;
            }
            PrintWindow printWindow = new PrintWindow();
            printWindow.Owner = this;
            printWindow.OrderInfoList = orderInfoList;
            printWindow.AllCount = count;
            if (printWindow.ShowDialog()==true)
            {
                orderInfoList = printWindow.OrderInfoList;
                datagrid.SetBinding(DataGrid.ItemsSourceProperty, new Binding() { Source = orderInfoList });
            } 
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
