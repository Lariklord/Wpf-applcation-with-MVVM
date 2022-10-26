using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TestTask.Intefaces;

namespace TestTask.Services
{
    internal class ImgFileDialog : IShowDialog
    {
        public string Path { get; set; }
        public bool Open()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files(*.jpg;*.png)|*.jpg;*.png";
            openFileDialog.InitialDirectory=Environment.CurrentDirectory;
            if (openFileDialog.ShowDialog() == true)
            {
                Path = openFileDialog.FileName;
                return true;
            }
            return false;

        }

        public bool Save()
        {
            return false;
        }
        public void Show(string mes)
        {
            MessageBox.Show(mes);
        }
    }
}
