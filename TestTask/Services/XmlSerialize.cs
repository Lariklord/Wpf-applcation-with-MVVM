using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.Intefaces;
using TestTask.Models;
using System.Xml.Serialization;
using System.IO;
using System.Windows;

namespace TestTask.Services
{
     public class XmlSerialize : ISerialize
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Phone>));
        public List<Phone> GetPhonesFromFile(string path)
        {
            List<Phone> phones = new List<Phone>();
            using (FileStream reader = new FileStream(path, FileMode.Open))
            {
                try
                {
                    phones = serializer.Deserialize(reader) as List<Phone>;
                }
                catch (Exception)
                {

                    MessageBox.Show($"Invalid data in {path}");
                }
                
            };
            return phones;

        }

        public void SavePhonesInFile(string path,List<Phone> phones)
        {
            using(FileStream writer = new FileStream(path, FileMode.Create))
            {
                serializer.Serialize(writer, phones);
            }
        }
    }
}
