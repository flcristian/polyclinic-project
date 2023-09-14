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
        bool Add(User user);

        bool Remove(User user);

        bool RemoveById(int id);

        bool ClearList();

        int EditById(int id, User user);
    }
}
