using polyclinic_project.system.constants;
using polyclinic_project.system.interfaces.exceptions;
using polyclinic_project.user.dtos;
using polyclinic_project.user.exceptions;
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

        public PatientViewAllDoctorsResponse ObtainAllDoctorDetails()
        {
            PatientViewAllDoctorsResponse result = _repository.ObtainAllDoctorDetails();
            if (result.Doctors.Count == 0)
                throw new ItemsDoNotExist(Constants.NO_DOCTORS_AVAILABLE);
            return result;
        }

        public User FindDoctorByName(string name)
        {
            List<User> doctors = _repository.FindDoctorsByName(name);
            if (doctors.Count == 0)
                throw new ItemsDoNotExist(Constants.NO_DOCTORS_WITH_THAT_NAME);
            if (doctors.Count > 1)
                throw new MultipleDoctorsWithThatName(Constants.MULTIPLE_DOCTORS_WITH_THAT_NAME);
            return doctors[0];
        }

        public List<User> GetList()
        {
            List<User> result = _repository.GetList();
            if(result.Count == 0)
            {
                throw new ItemsDoNotExist(Constants.NO_USERS_EXIST);
            }
            return result;
        }

        public List<User> GetPatientList()
        {
            List<User> users = _repository.GetList();
            if (users.Count == 0)
            {
                throw new ItemsDoNotExist(Constants.NO_PATIENTS_EXIST);
            }
            List<User> result = new List<User>();
            foreach(User user in users)
            {
                if(user.GetType() == UserType.PATIENT)
                {
                    result.Add(user);
                }
            }
            return result;
        }

        public List<User> GetDoctorList()
        {
            List<User> users = _repository.GetList();
            if (users.Count == 0)
            {
                throw new ItemsDoNotExist(Constants.NO_DOCTORS_EXIST);
            }
            List<User> result = new List<User>();
            foreach (User user in users)
            {
                if (user.GetType() == UserType.DOCTOR)
                {
                    result.Add(user);
                }
            }
            return result;
        }

        public List<User> GetAdminList()
        {
            List<User> users = _repository.GetList();
            if (users.Count == 0)
            {
                throw new ItemsDoNotExist(Constants.NO_ADMINS_EXIST);
            }
            List<User> result = new List<User>();
            foreach (User user in users)
            {
                if (user.GetType() == UserType.ADMIN)
                {
                    result.Add(user);
                }
            }
            return result;
        }

        #endregion
    }
}
