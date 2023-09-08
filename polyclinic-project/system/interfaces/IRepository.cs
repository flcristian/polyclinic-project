using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic_project.system.interfaces
{
    public interface IRepository<T>
    {
        void LoadData();

        void SaveData();

        List<T> GetList();
    }
}
