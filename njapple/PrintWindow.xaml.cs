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
using njapple.Model;

namespace njapple
{
    /// <summary>
    /// PrintWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PrintWindow : Window
    {
        int orderProgress = 0;
        int currentOrderCount = 0;
        public PrintWindow()
        {
            InitializeComponent();
        }

        public List<OrderInfo> OrderInfoList
        {
            get;
            set;
        }

        public int AllCount
        {
            get;
            set;
        }

        #region 拖动关闭
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
            this.DialogResult = false;
            this.Close();
        }
        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateProgress();
        }

        private void UpdateProgress() {
            OrderInfo orderInfo = OrderInfoList[orderProgress];
            orderInfoGrid.SetBinding(Grid.DataContextProperty, new Binding() { Source = orderInfo });

        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            OrderInfo orderInfo = OrderInfoList[orderProgress];
            PrintPreviewWindow pWindow = new PrintPreviewWindow();
            pWindow.PrintOrderInfo = OrderInfoList[orderProgress];
            if (pWindow.ShowDialog()==true)
            {
                MessageBox.Show("打印成功！");
            }
        }
    }
}
