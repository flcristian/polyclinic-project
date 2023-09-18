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

        public void Add(User user)
        {
            ICommandServiceUtility<User>.Add(_userRepository, user);
        }

        public void Remove(User user)
        {
            ICommandServiceUtility<User>.Remove(_userRepository, user);
        }

        public void RemoveById(int id)
        {
            ICommandServiceUtility<User>.RemoveById(_userRepository, id);
        }

        public void ClearList()
        {
            ICommandServiceUtility<User>.ClearList(_userRepository);
        }

        public void EditById(int id, User user)
        {
            ICommandServiceUtility<User>.EditById(_userRepository, id, user);
        }
    }
}
