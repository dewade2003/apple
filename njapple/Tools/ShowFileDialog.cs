using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace njapple.Tools
{
    public class ShowFileDialog
    {

        public static readonly string excelExt = "xlsx";
        public static readonly string excelFilter = "Excel文件|*.xlsx";

        public static readonly string pdfExt = "pdf";
        public static readonly string pdfFilter = "PDF文件(*.pdf)|*.pdf";
        public static bool ShowSaveFileDialog(out string fileName,string filter,string ext,string defaultFileName) {
            bool result = false;
            fileName = "";
            OpenFileDialog sfd = new OpenFileDialog();
            sfd.Title = "请选择订单文件";
            sfd.DefaultExt = ext;
            sfd.FileName = defaultFileName;
            sfd.AddExtension = true;
            sfd.Filter = filter;
            if(sfd.ShowDialog()==true){
                fileName = sfd.FileName;
                result = true;
            }
            return result;
        }
    }
}
