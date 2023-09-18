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
    public class UserQueryService : IUserQueryService
    {
        private IRepository<User> _userRepository;

        // Constructors

        public UserQueryService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public UserQueryService()
        {
            _userRepository = UserRepositorySingleton.Instance;
        }

        // Methods

        public User FindById(int id)
        {
            return IQueryServiceUtility<User>.FindById(_userRepository, id);
        }

        public User FindByEmail(String email)
        {
            return _userRepository.GetList().First(i => i.GetEmail() == email);
        }

        public User FindByPhone(String phone)
        {
            return _userRepository.GetList().First(i => i.GetPhone() == phone);
        }

        public int GetCount()
        {
            return IQueryServiceUtility<User>.GetCount(_userRepository);
        }
    }
}
