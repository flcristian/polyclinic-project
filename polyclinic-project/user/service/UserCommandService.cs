using polyclinic_project.system.interfaces;
using polyclinic_project.user.model;
using polyclinic_project.user.repository;
using polyclinic_project.user.repository.interfaces;
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
        private IUserRepository _repository = UserRepositorySingleton.Instance;

        public void Add(User user)
        {
            throw new NotImplementedException();
        }

        public void ClearList()
        {
            throw new NotImplementedException();
        }

        public void EditById(int id, User user)
        {
            throw new NotImplementedException();
        }

        public void Remove(User user)
        {
            throw new NotImplementedException();
        }

        public void RemoveById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
