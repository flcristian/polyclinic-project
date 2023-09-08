using polyclinic_project.system.interfaces;
using polyclinic_project.user.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic_project.user.repository
{
    public class UserRepository : IRepository<User>
    {
        private List<User> _list;
        private String _path;

        // Constructors

        public UserRepository(String path)
        {
            _list = new List<User>();
            _path = path;
        }

        public UserRepository(List<User> list, String path)
        {
            _list = list;
            _path = path;
        }

        // Methods

        public void LoadData()
        {
            _list = IDataRepository<User>.LoadData(_path, new UserFactory()); 
        }

        public void SaveData()
        {
            IDataRepository<User>.SaveData(_path, _list);
        }

        public List<User> GetList() { return _list; }
    }
}
