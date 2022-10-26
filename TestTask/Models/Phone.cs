using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TestTask.Models
{
    [Serializable]
    public class Phone:INotifyPropertyChanged
    {
        private string name;
        private string url;
        public Phone()
        {

        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public string Url
        {
            get { return url; }
            set
            {
                url= value;
                OnPropertyChanged("Url");
            }
        }
        [NonSerialized]
        private bool isselected;
        public bool IsSelected
        {
            get { return isselected; }
            set
            {
                isselected = value;
                OnPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
