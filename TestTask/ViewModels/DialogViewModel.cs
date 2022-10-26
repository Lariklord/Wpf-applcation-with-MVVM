using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TestTask.Commands;
using TestTask.Intefaces;
using TestTask.Models;

namespace TestTask.ViewModels
{
    internal class DialogViewModel:INotifyPropertyChanged
    {
        public Phone Phone { get; set; }
        private Window window;

        public string Name
        {
            get { return Phone.Name; }
            set 
            { 
                Phone.Name = value;
                OnPropertyChanged(Name);
            }
        }
        public string Url
        {
            get { return Phone.Url; }
            set
            {
                Phone.Url = value;
                OnPropertyChanged("Url");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public DialogViewModel(IShowDialog showDialog,Window window)
        {
            Phone = new Phone();
            imgFileDialog = showDialog;
            this.window = window;
        }

        IShowDialog imgFileDialog;

        private RelayCommand choosePhoto;
        public RelayCommand ChoosePhoto
        {
            get
            {
                return choosePhoto?? (choosePhoto= new RelayCommand(obj=>
                {
                    if (imgFileDialog.Open())
                    {
                        Url = imgFileDialog.Path;
                    }
                }));
            }
        }

        private RelayCommand savePhone;
        public RelayCommand SavePhone
        {
            get
            {
                return savePhone ?? (savePhone = new RelayCommand(obj =>
                {
                    if (Name == null || Name.Length < 1)
                        MessageBox.Show("Field Name is empty");
                    else if (Url == null)
                        MessageBox.Show("Choose photo");
                    else
                        window.DialogResult = true;
                }));
            }
        }


    }
}
