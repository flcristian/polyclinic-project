using polyclinic_project.user.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic_project.user.repository.interfaces
{
    public interface IUserRepository
    {
        void Add(User user);
        void Delete(int id);
        void Update(User user);
        User GetById(int id);
        User GetByEmail(String email);
        User GetByPhone(String phone);
        List<User> GetList();
        int GetCount();
        void Clear();
    }
}
