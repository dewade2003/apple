using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace WPFHelper
{
    public class UIElementTools
    {
        /// <summary>
        /// 保存ui控件为图片文件
        /// </summary>
        /// <param name="fileName">保存的文件名称，包括完整路径</param>
        /// <param name="uiElement">要保存的ui控件</param>
        public static bool CreateImageStreamFromUIElement(FrameworkElement uiElement)
        {
            string imgPath = AppDomain.CurrentDomain.BaseDirectory + "\\temp.jpg";
            try
            {
                System.IO.FileStream fs = new System.IO.FileStream(imgPath, System.IO.FileMode.Create);
                int imgWidth = int.Parse(uiElement.Width.ToString());
                int imgHeight = int.Parse(uiElement.Height.ToString());
                RenderTargetBitmap bmp = new RenderTargetBitmap(imgWidth, imgHeight, 96, 96, PixelFormats.Pbgra32);
                bmp.Render(uiElement);
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bmp));
                encoder.Save(fs);
                fs.Close();
                return true;
            }
            catch {
                return false;
            }
        }

        public static T FindFirstVisualChild2<T>(DependencyObject obj, string childName) where T : DependencyObject
        {

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {

                DependencyObject child = VisualTreeHelper.GetChild(obj, i);

                if (child != null && child is T && child.GetValue(FrameworkElement.NameProperty).ToString() == childName)
                {

                    return (T)child;

                }

                else
                {

                    T childOfChild = FindFirstVisualChild2<T>(child, childName);

                    if (childOfChild != null)
                    {

                        return childOfChild;

                    }

                }

            }

            return null;

        }

        public static childItem FindVisualChild<childItem>(DependencyObject obj)
     where childItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem)
                    return (childItem)child;
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }


    }
}
