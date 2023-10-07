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

        public AppointmentCommandService()
        {
            _repository = AppointmentRepositorySingleton.Instance;
        }

        public AppointmentCommandService(IAppointmentRepository repository)
        {
            _repository = repository;
        }

        public void Add(Appointment appointment)
        {
            Appointment id = null!, startDate = null!, endDate = null!;
            try { id = _repository.FindById(appointment.GetId()); }
            catch(ItemDoesNotExist ex) { }

            try { startDate = _repository.FindByDate(appointment.GetStartDate()); }
            catch(ItemDoesNotExist ex) { }
            
            try { endDate = _repository.FindByDate(appointment.GetEndDate()); }
            catch(ItemDoesNotExist ex) { }

            if (id != null) throw new ItemAlreadyExists("Id is already used");
            if (startDate != null) throw new ItemAlreadyExists("Another appointment is scheduled in that date");
            if (endDate != null) throw new ItemAlreadyExists("Another appointment is scheduled in that date");
            _repository.Add(appointment);
        }

        public void ClearList()
        {
            _repository.Clear();
        }

        public void Update(Appointment appointment)
        {
            Appointment check = _repository.FindById(appointment.GetId());
            if(check.Equals(appointment)) throw new ItemNotModified("Appointment was not modified, it doesn't require to be updated");
            _repository.Update(appointment);
        }

        public void Delete(Appointment appointment)
        {
            _repository.FindById(appointment.GetId());
            _repository.Delete(appointment.GetId());
        }

        public void DeleteById(int id)
        {
            _repository.FindById(id);
            _repository.Delete(id);
        }
    }
}
