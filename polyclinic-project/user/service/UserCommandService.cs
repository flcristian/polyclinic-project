using polyclinic_project.system.interfaces.exceptions;
using polyclinic_project.user.model;
using polyclinic_project.user.repository;
using polyclinic_project.user.repository.interfaces;
using polyclinic_project.user.service.interfaces;
using polyclinic_project.system.constants;

namespace polyclinic_project.user.service
{
    public class UserCommandService : IUserCommandService
    {
        private IUserRepository _repository;

        #region CONSTRUCTORS
        
        public UserCommandService()
        {
            _repository = UserRepositorySingleton.Instance;
        }

        public UserCommandService(IUserRepository repository)
        {
            _repository = repository;
        }

        #endregion
        
        #region PUBLIC_METHODS
        
        public void Add(User user)
        {
            List<User> email = null!, phone = null!;
            email = _repository.FindByEmail(user.GetEmail());
            phone = _repository.FindByPhone(user.GetPhone());

            if (email.Count > 0)
                throw new ItemAlreadyExists(Constants.EMAIL_ALREADY_USED);
            if (phone.Count > 0)
                throw new ItemAlreadyExists(Constants.PHONE_ALREADY_USED);
            _repository.Add(user);
        }

        public void Delete(User user)
        {
            List<User> check = _repository.FindById(user.GetId());
            if (check.Count == 0)
                throw new ItemDoesNotExist(Constants.USER_DOES_NOT_EXIST);
            _repository.Delete(user.GetId());
        }

        public void DeleteById(int id)
        {
            List<User> check = _repository.FindById(id);
            if (check.Count == 0)
                throw new ItemDoesNotExist(Constants.USER_DOES_NOT_EXIST);
            _repository.Delete(id);
        }

        public void ClearList()
        {
            _repository.Clear();
        }

        public void Update(User user)
        {
            List<User> check = _repository.FindById(user.GetId());
            if (check.Count == 0)
                throw new ItemDoesNotExist(Constants.USER_DOES_NOT_EXIST);
            _repository.Update(user);
        }
        
        #endregion
    }
}
