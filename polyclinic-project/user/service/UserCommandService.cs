using polyclinic_project.system.interfaces.exceptions;
using polyclinic_project.user.model;
using polyclinic_project.user.repository;
using polyclinic_project.user.repository.interfaces;
using polyclinic_project.user.service.interfaces;

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
            User id = null!, email = null!, phone = null!;
            
            try { id = _repository.FindById(user.GetId()); }
            catch(ItemDoesNotExist ex) { }
            
            try { email = _repository.FindByEmail(user.GetEmail()); }
            catch(ItemDoesNotExist ex) { }
            
            try { phone = _repository.FindByPhone(user.GetPhone()); }
            catch(ItemDoesNotExist ex) { }

            if (id != null) 
                throw new ItemAlreadyExists("Id is already used");
            if (email != null)
                throw new ItemAlreadyExists("Email is already used");
            if (phone != null)
                throw new ItemAlreadyExists("Phone is already used");
            _repository.Add(user);
        }

        public void Delete(User user)
        {
            _repository.FindById(user.GetId());
            _repository.Delete(user.GetId());
        }

        public void DeleteById(int id)
        {
            _repository.FindById(id);
            _repository.Delete(id);
        }

        public void ClearList()
        {
            _repository.Clear();
        }

        public void Update(User user)
        {
            User check = _repository.FindById(user.GetId());
            if (check.Equals(user))
                throw new ItemNotModified("User was not modified, it doesn't require to be updated");
            _repository.Update(user);
        }
        
        #endregion
    }
}
