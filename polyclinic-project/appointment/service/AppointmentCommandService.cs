using polyclinic_project.appointment.model;
using polyclinic_project.appointment.repository;
using polyclinic_project.appointment.service.interfaces;
using polyclinic_project.system.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic_project.appointment.service
{
    public class AppointmentCommandService : IAppointmentCommandService
    {
        private IRepository<Appointment> _repository = AppointmentRepositorySingleton.Instance;

        public void Add(Appointment appointment)
        {
            ICommandServiceUtility<Appointment>.Add(_repository, appointment);
        }

        public void Remove(Appointment appointment)
        {
            ICommandServiceUtility<Appointment>.Remove(_repository, appointment);
        }

        public void RemoveById(int id)
        {
            ICommandServiceUtility<Appointment>.RemoveById(_repository, id);
        }

        public void ClearList()
        {
            ICommandServiceUtility<Appointment>.ClearList(_repository);
        }

        public void EditById(int id, Appointment appointment)
        {
            ICommandServiceUtility<Appointment>.EditById(_repository, id, appointment);
        }
    }
}
