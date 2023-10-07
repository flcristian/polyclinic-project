using polyclinic_project.user.model;
using polyclinic_project.user.repository;
using polyclinic_project.user.repository.interfaces;
using polyclinic_project.user.service.interfaces;

namespace polyclinic_project.user.service
{
    public class UserQueryService : IUserQueryService
    {
        private IUserRepository _repository;

        #region CONSTRUCTORS
        
        public UserQueryService()
        {
            _repository = UserRepositorySingleton.Instance;
        }

        public UserQueryService(IUserRepository repository)
        {
            _repository = repository;
        }
        
        #endregion
        
        #region PUBLIC_METHODS
        
        public User FindByEmail(string email)
        {
            return _repository.FindByEmail(email);
        }

        public User FindById(int id)
        {
            return _repository.FindById(id);
        }

        public User FindByPhone(string phone)
        {
            return _repository.FindByPhone(phone);
        }

        public int GetCount()
        {
            return _repository.GetCount();
        }
        
        #endregion
    }
}
