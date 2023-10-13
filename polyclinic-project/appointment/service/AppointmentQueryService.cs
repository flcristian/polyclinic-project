using polyclinic_project.appointment.model;
using polyclinic_project.appointment.repository;
using polyclinic_project.appointment.repository.interfaces;
using polyclinic_project.appointment.service.interfaces;
using polyclinic_project.system.constants;
using polyclinic_project.system.interfaces.exceptions;
using System.Globalization;

namespace polyclinic_project.appointment.service
{
    public class AppointmentQueryService : IAppointmentQueryService
    {
        private IAppointmentRepository _repository;

        #region CONSTRUCTORS
        
        public AppointmentQueryService(IAppointmentRepository repository)
        {
            _repository = repository;
        }

        public AppointmentQueryService()
        {
            _repository = AppointmentRepositorySingleton.Instance;
        }
        
        #endregion

        #region PUBLIC_METHODS

        public Appointment FindById(int id)
        {
            List<Appointment> result = _repository.FindById(id);
            if (result.Count == 0)
                throw new ItemDoesNotExist(Constants.APPOINTMENT_DOES_NOT_EXIST);
            return result[0];
        }

        public int GetCount()
        {
            return _repository.GetCount();
        }

        public bool CanAddAppointment(Appointment appointment)
        {
            List<Appointment> appointments = _repository.GetList();
            foreach(Appointment check in appointments)
            {
                if (check.Equals(appointment))
                {
                    return false;
                }
            }
            return true;
        }
        
        #endregion
    }
}
