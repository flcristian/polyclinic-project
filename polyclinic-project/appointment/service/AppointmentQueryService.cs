using polyclinic_project.appointment.model;
using polyclinic_project.appointment.repository;
using polyclinic_project.appointment.repository.interfaces;
using polyclinic_project.appointment.service.interfaces;
using polyclinic_project.system.constants;
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
            return _repository.FindById(id);
        }

        public Appointment FindByDate(DateTime date)
        {
            return _repository.FindByDate(date);
        }

        public int GetCount()
        {
            return _repository.GetCount();
        }

        public Appointment FindByDate(String date)
        {
            return _repository.FindByDate(DateTime.ParseExact(date, Constants.SQL_DATE_FORMAT, CultureInfo.InvariantCulture));
        }
        
        #endregion
    }
}
