using polyclinic_project.appointment.model.interfaces;
using polyclinic_project.system.interfaces;
using polyclinic_project.system.constants;
using System;
using System.Globalization;

namespace polyclinic_project.appointment.model
{
    public class Appointment : IAppointmentBuilder, IPrototype<Appointment>, IComparable<Appointment>
    {
        private int id;
        private DateTime startDate;
        private DateTime endDate;

        #region CONSTRUCTORS
        
        public Appointment(int id, DateTime startDate, DateTime endDate)
        {
            this.id = id;
            this.startDate = startDate;
            this.endDate = endDate;
        }

        public Appointment(int id, String startDate, String endDate)
        {
            this.id = id;
            this.startDate = DateTime.ParseExact(startDate, Constants.STANDARD_DATE_FORMAT, CultureInfo.InvariantCulture); ;
            this.endDate = DateTime.ParseExact(endDate, Constants.STANDARD_DATE_FORMAT, CultureInfo.InvariantCulture);
        }

        public Appointment(Appointment appointment)
        {
            this.id = appointment.id;
            this.startDate = appointment.startDate;
            this.endDate = appointment.endDate;
        }

        public Appointment()
        {
            this.id = -1;
            this.startDate = DateTime.Now;
            this.endDate = DateTime.Now;
        }

        #endregion

        #region ACCESSORS

        public int GetId() { return this.id; }

        public DateTime GetStartDate() { return this.startDate; }

        public DateTime GetEndDate() { return this.endDate; }

        public void SetId(int id) { this.id = id; }

        public void SetStartDate(DateTime startDate) { this.startDate = startDate; }

        public void SetEndDate(DateTime endDate) { this.endDate = endDate; }

        #endregion

        #region BUILDER

        public Appointment Id(int id)
        {
            this.id = id;
            return this;
        }

        public Appointment StartDate(DateTime startDate)
        {
            this.startDate = startDate;
            return this;
        }

        public Appointment StartDate(String startDate)
        {
            this.startDate = DateTime.ParseExact(startDate, Constants.STANDARD_DATE_FORMAT, CultureInfo.InvariantCulture);
            return this;
        }

        public Appointment EndDate(DateTime endDate)
        {
            this.endDate = endDate;
            return this;
        }

        public Appointment EndDate(String endDate)
        {
            this.endDate = DateTime.ParseExact(endDate, Constants.STANDARD_DATE_FORMAT, CultureInfo.InvariantCulture);
            return this;
        }

        #endregion

        #region PUBLIC_METHODS

        public override Boolean Equals(object? obj)
        {
            Appointment appointment = obj as Appointment;

            if(appointment.startDate >= this.endDate || appointment.endDate <= this.startDate)
            {
                return false;
            }

            return true;
        }

        public override String ToString()
        {
            String desc = "";
            desc += $"Appointment Id : {this.id}\n";
            desc += $"Start Date : {this.startDate.ToString(Constants.STANDARD_DATE_FORMAT)}\n";
            desc += $"End Date : {this.endDate.ToString(Constants.STANDARD_DATE_FORMAT)}\n";
            return desc;
        }

        public override Int32 GetHashCode()
        {
            return (int)Math.Pow(this.startDate.Day, this.startDate.Minute) * (int)Math.PI - this.endDate.Minute * this.endDate.Hour;
        }

        public Int32 CompareTo(Appointment? appointment)
        {
            return 0;
        }

        public Appointment Clone()
        {
            return new Appointment(this);
        }

        #endregion
    }
}
