using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic_project.system.models
{
    public class TimeInterval
    {
        private DateTime _startTime;
        private DateTime _endTime;

        public TimeInterval(DateTime startTime, DateTime endTime)
        {
            _startTime = startTime;
            _endTime = endTime;
        }

        public DateTime StartTime
        {
            get { return _startTime; }
            set { _startTime = value; }
        }

        public DateTime EndTime
        {
            get { return _endTime; }
            set { _endTime = value; }
        }

        public override bool Equals(object? obj)
        {
            TimeInterval interval = obj as TimeInterval;
            return interval._startTime.Equals(_startTime) && interval._endTime.Equals(_endTime);
        }
    }
}
