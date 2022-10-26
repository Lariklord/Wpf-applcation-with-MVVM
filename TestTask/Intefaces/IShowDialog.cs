using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.Intefaces
{
    internal interface IShowDialog
    {
        string Path { get; set; }
        bool Open();
        bool Save();
        void Show(string mes);

    }
}
