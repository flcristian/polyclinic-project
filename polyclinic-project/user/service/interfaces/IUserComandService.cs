using polyclinic_project.user.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic_project.user.service.interfaces
{
    public interface IUserComandService
    {
        void Add(User user);

        void Remove(User user);

        void RemoveById(int id);

        void ClearList();

        void EditById(int id, User user);
    }
}
