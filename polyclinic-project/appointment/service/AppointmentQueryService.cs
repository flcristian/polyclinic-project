using polyclinic_project.appointment.model;
using polyclinic_project.appointment.repository;
using polyclinic_project.appointment.service.interfaces;
using polyclinic_project.system.constants;
using polyclinic_project.system.interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic_project.appointment.service
{
    public class AppointmentQueryService : IAppointmentQueryService
    {
        private IRepository<Appointment> _repository;

        // Constructors

        public AppointmentQueryService(IRepository<Appointment> repository)
        {
            _repository = repository;
        }

        public AppointmentQueryService()
        {
            _repository = AppointmentRepositorySingleton.Instance;
        }

        // Methods

        public Appointment FindById(int id)
        {
            return IQueryServiceUtility<Appointment>.FindById(_repository, id);
        }

        public Appointment FindByDate(DateTime date)
        {
            return _repository.GetList().First(i => i.GetStartDate() <= date && i.GetEndDate() >= date);
        }

        public Appointment FindByDate(String date)
        {
            DateTime parsed = DateTime.ParseExact(date, Constants.STANDARD_DATE_FORMAT, CultureInfo.InvariantCulture);
            return _repository.GetList().First(i => i.GetStartDate() <= parsed && i.GetEndDate() >= parsed);
        }

        public int GetCount()
        {
            return IQueryServiceUtility<Appointment>.GetCount(_repository);
        }
    }
}
