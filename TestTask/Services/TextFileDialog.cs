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
    internal class TextFileDialog : IShowDialog
    {
        public string Path { get; set; }
        public bool Open()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files(*.xml)|*.xml";
            openFileDialog.InitialDirectory = Environment.CurrentDirectory;
            if (openFileDialog.ShowDialog() == true)
            {
                Path= openFileDialog.FileName;
                return true;
            }
            return false;
            
        }

        public bool Save()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files(*.xml)|*.xml";
            saveFileDialog.InitialDirectory = Environment.CurrentDirectory;
            if (saveFileDialog.ShowDialog() == true)
            {
                Path = saveFileDialog.FileName;
                return true;
            }
            return false;
        }
        public void Show(string mes)
        {
            MessageBox.Show(mes);
        }
    }
}
