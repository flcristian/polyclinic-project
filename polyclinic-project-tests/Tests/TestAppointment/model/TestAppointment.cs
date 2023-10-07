using polyclinic_project.appointment.model.interfaces;
using System.Globalization;
using polyclinic_project.system.constants;
using polyclinic_project.appointment.model.comparators;
using polyclinic_project.appointment.model;

namespace polyclinic_project_tests.Tests.TestAppointment
{
    public class TestAppointment
    {
        [Fact]
        public void TestEquals_FirstEndsBeforeSecond_ReturnsFalse()
        {
            Appointment x = IAppointmentBuilder.BuildAppointment()
            .Id(1)
            .StartDate(DateTime.ParseExact("14.09.2023 21:10", Constants.STANDARD_DATE_FORMAT, CultureInfo.InvariantCulture))
            .EndDate(DateTime.ParseExact("14.09.2023 21:40", Constants.STANDARD_DATE_FORMAT, CultureInfo.InvariantCulture));

            Appointment y = IAppointmentBuilder.BuildAppointment()
                .Id(1)
                .StartDate(DateTime.ParseExact("14.09.2023 21:50", Constants.STANDARD_DATE_FORMAT, CultureInfo.InvariantCulture))
                .EndDate(DateTime.ParseExact("14.09.2023 22:30", Constants.STANDARD_DATE_FORMAT, CultureInfo.InvariantCulture));

            Assert.NotEqual(x, y, new AppointmentEqualityComparer());
        }

        [Fact]
        public void TestEquals_FirstStartsAfterSecond_ReturnsFalse()
        {
            Appointment x = IAppointmentBuilder.BuildAppointment()
            .Id(1)
            .StartDate(DateTime.ParseExact("14.09.2023 21:40", Constants.STANDARD_DATE_FORMAT, CultureInfo.InvariantCulture))
            .EndDate(DateTime.ParseExact("14.09.2023 22:10", Constants.STANDARD_DATE_FORMAT, CultureInfo.InvariantCulture));

            Appointment y = IAppointmentBuilder.BuildAppointment()
                .Id(1)
                .StartDate(DateTime.ParseExact("14.09.2023 21:00", Constants.STANDARD_DATE_FORMAT, CultureInfo.InvariantCulture))
                .EndDate(DateTime.ParseExact("14.09.2023 21:30", Constants.STANDARD_DATE_FORMAT, CultureInfo.InvariantCulture));

            Assert.NotEqual(x, y, new AppointmentEqualityComparer());
        }

        [Fact]
        public void TestEquals_FirstStartsMidSecond_ReturnsTrue()
        {
            Appointment x = IAppointmentBuilder.BuildAppointment()
            .Id(1)
            .StartDate(DateTime.ParseExact("14.09.2023 21:40", Constants.STANDARD_DATE_FORMAT, CultureInfo.InvariantCulture))
            .EndDate(DateTime.ParseExact("14.09.2023 22:30", Constants.STANDARD_DATE_FORMAT, CultureInfo.InvariantCulture));

            Appointment y = IAppointmentBuilder.BuildAppointment()
                .Id(1)
                .StartDate(DateTime.ParseExact("14.09.2023 21:00", Constants.STANDARD_DATE_FORMAT, CultureInfo.InvariantCulture))
                .EndDate(DateTime.ParseExact("14.09.2023 22:00", Constants.STANDARD_DATE_FORMAT, CultureInfo.InvariantCulture));

            Assert.Equal(x, y, new AppointmentEqualityComparer());
        }

        [Fact]
        public void TestEquals_FirstEndsMidSecond_ReturnsTrue()
        {
            Appointment x = IAppointmentBuilder.BuildAppointment()
            .Id(1)
            .StartDate(DateTime.ParseExact("14.09.2023 21:00", Constants.STANDARD_DATE_FORMAT, CultureInfo.InvariantCulture))
            .EndDate(DateTime.ParseExact("14.09.2023 21:30", Constants.STANDARD_DATE_FORMAT, CultureInfo.InvariantCulture));

            Appointment y = IAppointmentBuilder.BuildAppointment()
                .Id(1)
                .StartDate(DateTime.ParseExact("14.09.2023 21:20", Constants.STANDARD_DATE_FORMAT, CultureInfo.InvariantCulture))
                .EndDate(DateTime.ParseExact("14.09.2023 22:00", Constants.STANDARD_DATE_FORMAT, CultureInfo.InvariantCulture));

            Assert.Equal(x, y, new AppointmentEqualityComparer());
        }

        [Fact]
        public void TestEquals_FirstIsMidSecond_ReturnsTrue()
        {
            Appointment x = IAppointmentBuilder.BuildAppointment()
            .Id(1)
            .StartDate(DateTime.ParseExact("14.09.2023 21:00", Constants.STANDARD_DATE_FORMAT, CultureInfo.InvariantCulture))
            .EndDate(DateTime.ParseExact("14.09.2023 22:00", Constants.STANDARD_DATE_FORMAT, CultureInfo.InvariantCulture));

            Appointment y = IAppointmentBuilder.BuildAppointment()
                .Id(1)
                .StartDate(DateTime.ParseExact("14.09.2023 20:00", Constants.STANDARD_DATE_FORMAT, CultureInfo.InvariantCulture))
                .EndDate(DateTime.ParseExact("14.09.2023 23:00", Constants.STANDARD_DATE_FORMAT, CultureInfo.InvariantCulture));

            Assert.Equal(x, y, new AppointmentEqualityComparer());
        }

        [Fact]
        public void TestEquals_SecondIsMidFirst_ReturnsTrue()
        {
            Appointment x = IAppointmentBuilder.BuildAppointment()
            .Id(1)
            .StartDate(DateTime.ParseExact("14.09.2023 21:00", Constants.STANDARD_DATE_FORMAT, CultureInfo.InvariantCulture))
            .EndDate(DateTime.ParseExact("14.09.2023 22:00", Constants.STANDARD_DATE_FORMAT, CultureInfo.InvariantCulture));

            Appointment y = IAppointmentBuilder.BuildAppointment()
                .Id(1)
                .StartDate(DateTime.ParseExact("14.09.2023 21:20", Constants.STANDARD_DATE_FORMAT, CultureInfo.InvariantCulture))
                .EndDate(DateTime.ParseExact("14.09.2023 21:40", Constants.STANDARD_DATE_FORMAT, CultureInfo.InvariantCulture));

            Assert.Equal(x, y, new AppointmentEqualityComparer());
        }
    }
}
