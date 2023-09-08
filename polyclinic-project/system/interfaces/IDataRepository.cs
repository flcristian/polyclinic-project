using polyclinic_project.user.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic_project.system.interfaces
{
    public interface IDataRepository<T> where T : ISaveable
    {
        public static List<T> LoadData(string path, IFactory<T> factory)
        {
            StreamReader sr = new StreamReader(path);
            List<T> list = new List<T>();
            while (!sr.EndOfStream)
            {
                string data = sr.ReadLine();

                list.Add(factory.CreateObject(data));
            }
            sr.Close();
            return list;
        }

        public static void SaveData(string path, List<T> list)
        {
            string save = "";
            list.ForEach(user =>
            {
                save += user.ToSave();
            });
            StreamWriter sw = new StreamWriter(path);
            sw.Write(save);
            sw.Close();
        }
    }
}
