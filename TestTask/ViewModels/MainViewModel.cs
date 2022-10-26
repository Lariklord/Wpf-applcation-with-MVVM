using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TestTask.Commands;
using TestTask.Intefaces;
using TestTask.Models;
using TestTask.Services;

namespace TestTask.ViewModels
{
    internal class MainViewModel:INotifyPropertyChanged
    {
        private ObservableCollection<Phone> phones;
        public ObservableCollection<Phone> Phones
        {
            get { return phones; }
            set 
            {
                phones = value;
                OnPropertyChanged("Phones");
            }
        }

        private Phone selectedPhone;
        public Phone SelectedPhone
        {
            get { return selectedPhone; }
            set
            {
                selectedPhone = value;
                OnPropertyChanged("SelectedPhone");
            }
        }
        ISerialize xmlSerializer;
        IShowDialog textFileDialog;

        public MainViewModel(ISerialize serialize, IShowDialog showDialog)
        {
            this.xmlSerializer = serialize;
            this.textFileDialog = showDialog;
            Phones = new ObservableCollection<Phone>();
        }

        private RelayCommand openTextFile;
        public RelayCommand OpenTextFile
        {
            get { return openTextFile ?? (openTextFile = new RelayCommand(obj =>
            {
                if (textFileDialog.Open())
                {
                    var phonesList = xmlSerializer.GetPhonesFromFile(textFileDialog.Path);
                    foreach (var item in phonesList)
                    {
                        Phones.Add(item);
                    }
                    if(phonesList.Count > 0)
                    textFileDialog.Show("Objects loaded");
                }
            }));
            }
        }
        
        private RelayCommand saveTextFile;
        public RelayCommand SaveTextFile
        {
            get
            {
                return saveTextFile ?? (saveTextFile = new RelayCommand(obj =>
                {
                    if(textFileDialog.Save())
                    {
                        xmlSerializer.SavePhonesInFile(textFileDialog.Path, Phones.ToList());
                        textFileDialog.Show($"Objects are stored in {textFileDialog.Path}");
                    }
                },x=>Phones.Count>0));
            }
        }

        private RelayCommand orderBy;
        public RelayCommand OrderBy
        {
            get
            {
                return orderBy ?? (orderBy = new RelayCommand(obj =>
                {
                    switch (obj.ToString())
                    {
                        case "Order by":
                            Phones = new ObservableCollection<Phone>(Phones.OrderBy(x => x.Name));
                            break;
                        case "Order by descending":
                            Phones = new ObservableCollection<Phone>(Phones.OrderByDescending(x => x.Name));
                            break;
                        default:
                            MessageBox.Show("Ошибка");
                            break;
                    }
                }, x => Phones.Count > 1));

            }
        }

        private RelayCommand addPhone;
        public RelayCommand AddPhone
        {
            get
            {
                return addPhone ?? ( addPhone = new RelayCommand(obj=>
                {
                    DialogWindow dialogWindow = new DialogWindow();
                    DialogViewModel dialogViewModel = new DialogViewModel(new ImgFileDialog(), dialogWindow);
                    dialogWindow.DataContext = dialogViewModel;
                    if(dialogWindow.ShowDialog()==true)
                    {
                        Phones.Add(dialogViewModel.Phone);
                        MessageBox.Show("Phone added successfully");
                    }
                }));
            }
        }
        
        private RelayCommand updatePhone;
        public RelayCommand UpdatePhone
        {
            get
            {
                return updatePhone ?? (updatePhone = new RelayCommand(obj =>
                {
                    DialogWindow dialogWindow = new DialogWindow();
                    DialogViewModel dialogViewModel = new DialogViewModel(new ImgFileDialog(), dialogWindow);
                    dialogWindow.DataContext = dialogViewModel;
                    dialogViewModel.Phone = new Phone { Name = SelectedPhone.Name, Url = SelectedPhone.Url };
                    if (dialogWindow.ShowDialog() == true)
                    {
                        SelectedPhone.Name = dialogViewModel.Name;
                        SelectedPhone.Url = dialogViewModel.Url;
                        MessageBox.Show("Phone changed successfully");

                    }
                },x=>SelectedPhone!=null && Phones.Where(y=>y.IsSelected).Count()==1));
            }
        }
        
        private RelayCommand removePhones;
        public RelayCommand RemovePhones
        {
            get
            {
                return removePhones ?? (removePhones = new RelayCommand(obj =>
                {
                    for (int i = 0; i < Phones.Count; i++)
                    {
                        if (Phones[i].IsSelected)
                        {
                            Phones.Remove(Phones[i]);
                            i--;
                        }
                    }
                }, x=>Phones.Where(y => y.IsSelected).Count() >0));
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }


    }
}
