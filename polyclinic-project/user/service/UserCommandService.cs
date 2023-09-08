using polyclinic_project.system.interfaces;
using polyclinic_project.user.model;
using polyclinic_project.user.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic_project.user.service
{
    public class UserCommandService : IUserComandService
    {
        IRepository<User> _userRepository = UserRepositorySingleton.Instance;

        public bool Add(User user)
        {
            return ICommandService<User>.Add(_userRepository, user);
        }

        public bool Remove(User user)
        {
            return ICommandService<User>.Remove(_userRepository, user);
        }

        public bool RemoveById(int id)
        {
            //todo:handle with exceptions

            User user = _userRepository.GetList().FirstOrDefault(u => u.GetId() == id);

            if (user == null)
            {
                return false;
            }

            _userRepository.GetList().Remove(user);
            return true;
        }

        public bool ClearList()
        {
            if (!_userRepository.GetList().Any())
            {
                return false;
            }

            _userRepository.GetList().Clear();
            return true;
        }


    }
}
