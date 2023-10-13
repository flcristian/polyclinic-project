using polyclinic_project.appointment.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic_project.appointment.repository.interfaces
{
    public interface IAppointmentRepository
    {
        void Add(Appointment appointment);
        void Delete(int id);
        void Update(Appointment appointment);
        List<Appointment> FindById(int id);
        List<Appointment> GetList();
        int GetCount();
        void Clear();
    }
}
