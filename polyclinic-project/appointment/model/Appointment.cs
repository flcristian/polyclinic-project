using polyclinic_project.appointment.model.interfaces;
using polyclinic_project.system.interfaces;
using polyclinic_project.system.constants;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace polyclinic_project.appointment.model
{
    public class Appointment : IAppointmentBuilder, IPrototype<Appointment>, IComparable<Appointment>, ISaveable, IHasId
    {
        private int _id;
        private DateTime _startDate;
        private DateTime _endDate;

        #region CONSTRUCTORS
        
        public Appointment(int id, DateTime startDate, DateTime endDate)
        {
            _id = id;
            _startDate = startDate;
            _endDate = endDate;
        }

        public Appointment(int id, String startDate, String endDate)
        {
            _id = id;
            _startDate = DateTime.ParseExact(startDate, Constants.STANDARD_DATE_FORMAT, CultureInfo.InvariantCulture); ;
            _endDate = DateTime.ParseExact(endDate, Constants.STANDARD_DATE_FORMAT, CultureInfo.InvariantCulture);
        }

        public Appointment(Appointment appointment)
        {
            _id = appointment._id;
            _startDate = appointment._startDate;
            _endDate = appointment._endDate;
        }

        public Appointment()
        {
            _id = -1;
            _startDate = DateTime.Now;
            _endDate = DateTime.Now;
        }

        #endregion

        #region ACCESSORS

        public int GetId() { return _id; }

        public DateTime GetStartDate() { return _startDate; }

        public DateTime GetEndDate() { return _endDate; }

        public void SetId(int id) { _id = id; }

        public void SetStartDate(DateTime startDate) { _startDate = startDate; }

        public void SetEndDate(DateTime endDate) { _endDate = endDate; }

        #endregion

        #region BUILDER

        public Appointment Id(int id)
        {
            _id = id;
            return this;
        }

        public Appointment StartDate(DateTime startDate)
        {
            _startDate = startDate;
            return this;
        }

        public Appointment StartDate(String startDate)
        {
            _startDate = DateTime.ParseExact(startDate, Constants.STANDARD_DATE_FORMAT, CultureInfo.InvariantCulture);
            return this;
        }

        public Appointment EndDate(DateTime endDate)
        {
            _endDate = endDate;
            return this;
        }

        public Appointment EndDate(String endDate)
        {
            _endDate = DateTime.ParseExact(endDate, Constants.STANDARD_DATE_FORMAT, CultureInfo.InvariantCulture);
            return this;
        }

        #endregion

        #region PUBLIC_METHODS

        public override Boolean Equals(object? obj)
        {
            Appointment appointment = obj as Appointment;

            if(appointment._startDate > _endDate || appointment._endDate < _startDate)
            {
                return false;
            }

            return true;
        }

        public override String ToString()
        {
            String desc = "";
            desc += $"Appointment Id : {_id}\n";
            desc += $"Start Date : {_startDate.ToString(Constants.STANDARD_DATE_FORMAT)}\n";
            desc += $"End Date : {_endDate.ToString(Constants.STANDARD_DATE_FORMAT)}\n";
            return desc;
        }

        public override Int32 GetHashCode()
        {
            return (int)Math.Pow(_startDate.Day, _startDate.Minute) * (int)Math.PI - _endDate.Minute * _endDate.Hour;
        }

        public Int32 CompareTo(Appointment? appointment)
        {
            return 0;
        }

        public Appointment Clone()
        {
            return new Appointment(this);
        }

        public String ToSave()
        {
            return $"{_id}/{_startDate.ToString(Constants.STANDARD_DATE_FORMAT)}/{_endDate.ToString(Constants.STANDARD_DATE_FORMAT)}\n";
        }

        #endregion
    }
}
