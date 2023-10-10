using polyclinic_project.system.constants;
using polyclinic_project.appointment.model;
using polyclinic_project.appointment.repository;
using polyclinic_project.appointment.repository.interfaces;
using polyclinic_project.appointment.service.interfaces;
using polyclinic_project.system.interfaces.exceptions;

namespace polyclinic_project.appointment.service
{
    public class AppointmentCommandService : IAppointmentCommandService
    {
        private IAppointmentRepository _repository;

        #region CONSTRUCTORS
        
        public AppointmentCommandService()
        {
            _repository = AppointmentRepositorySingleton.Instance;
        }

        public AppointmentCommandService(IAppointmentRepository repository)
        {
            _repository = repository;
        }
        
        #endregion

        #region PUBLIC_METHODS
        
        public void Add(Appointment appointment)
        {
            if (appointment.GetStartDate() >= appointment.GetEndDate())
                throw new InvalidAppointmentSchedule(Constants.INVALID_APPOINTMENT_DATES);
            
            List<Appointment> id = null!, startDate = null!, endDate = null!;
            id = _repository.FindById(appointment.GetId());
            startDate = _repository.FindByDate(appointment.GetStartDate());
            endDate = _repository.FindByDate(appointment.GetEndDate());

            if (id.Count > 0) 
                throw new ItemAlreadyExists(Constants.ID_ALREADY_USED);
            if (startDate.Count > 0) 
                throw new ItemAlreadyExists(Constants.ANOTHER_APPOINTMENT_ALREADY_SCHEDULED);
            if (endDate.Count > 0)
                throw new ItemAlreadyExists(Constants.ANOTHER_APPOINTMENT_ALREADY_SCHEDULED);
            _repository.Add(appointment);
        }

        public void ClearList()
        {
            _repository.Clear();
        }

        public void Update(Appointment appointment)
        {
            List<Appointment> check = _repository.FindById(appointment.GetId());
            if (check.Count == 0)
                throw new ItemDoesNotExist(Constants.APPOINTMENT_DOES_NOT_EXIST);
            if (check[0].Equals(appointment)) 
                throw new ItemNotModified(Constants.APPOINTMENT_NOT_MODIFIED);
            _repository.Update(appointment);
        }

        public void Delete(Appointment appointment)
        {
            List<Appointment> check = _repository.FindById(appointment.GetId());
            if (check.Count == 0)
                throw new ItemDoesNotExist(Constants.USER_DOES_NOT_EXIST);
            _repository.Delete(appointment.GetId());
        }

        public void DeleteById(int id)
        {
            List<Appointment> check = _repository.FindById(id);
            if (check.Count == 0)
                throw new ItemDoesNotExist(Constants.USER_DOES_NOT_EXIST);
            _repository.Delete(id);
        }
        
        #endregion
    }
}
