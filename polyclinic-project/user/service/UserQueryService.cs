using polyclinic_project.system.constants;
using polyclinic_project.system.interfaces.exceptions;
using polyclinic_project.user.dtos;
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
            List<User> result = _repository.FindByEmail(email);
            if (result.Count == 0)
                throw new ItemDoesNotExist(Constants.USER_DOES_NOT_EXIST);
            return result[0];
        }

        public User FindById(int id)
        {
            List<User> result = _repository.FindById(id);
            if (result.Count == 0)
                throw new ItemDoesNotExist(Constants.USER_DOES_NOT_EXIST);
            return result[0];
        }

        public User FindByPhone(string phone)
        {
            List<User> result = _repository.FindByPhone(phone);
            if (result.Count == 0)
                throw new ItemDoesNotExist(Constants.USER_DOES_NOT_EXIST);
            return result[0];
        }

        public int GetCount()
        {
            return _repository.GetCount();
        }

        public PatientViewAllDoctorsResponse ObtainAllDoctorNames()
        {
            PatientViewAllDoctorsResponse result = _repository.ObtainAllDoctorNames();
            if (result.Doctors.Count == 0)
                throw new ItemsDoNotExist(Constants.NO_DOCTORS_AVAILABLE);
            return result;
        }
        
        #endregion
    }
}
