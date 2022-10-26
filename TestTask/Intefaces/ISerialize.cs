using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.Models;

namespace TestTask.Intefaces
{
    internal interface ISerialize
    {
        List<Phone> GetPhonesFromFile(string path);
        void SavePhonesInFile(string path,List<Phone> phones);
    }
}
