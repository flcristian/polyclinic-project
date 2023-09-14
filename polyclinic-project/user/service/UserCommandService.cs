using polyclinic_project.system.interfaces;
using polyclinic_project.user.model;
using polyclinic_project.user.repository;
using polyclinic_project.user.service.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic_project.user.service
{
    public class UserCommandService : IUserComandService
    {
        private IRepository<User> _userRepository = UserRepositorySingleton.Instance;

        public bool Add(User user)
        {
            return ICommandServiceUtility<User>.Add(_userRepository, user);
        }

        public bool Remove(User user)
        {
            return ICommandServiceUtility<User>.Remove(_userRepository, user);
        }

        public bool RemoveById(int id)
        {
            //todo:handle with exceptions

            return ICommandServiceUtility<User>.RemoveById(_userRepository, id);
        }

        public bool ClearList()
        {
            return ICommandServiceUtility<User>.ClearList(_userRepository);
        }

        public int EditById(int id, User user)
        {
            return ICommandServiceUtility<User>.EditById(_userRepository, id, user);
        }
    }
}
