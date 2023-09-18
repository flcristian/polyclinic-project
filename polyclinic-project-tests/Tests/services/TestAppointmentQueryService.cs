using polyclinic_project.appointment.model;
using polyclinic_project.appointment.model.comparators;
using polyclinic_project.appointment.model.interfaces;
using polyclinic_project.appointment.repository;
using polyclinic_project.appointment.service;
using polyclinic_project.appointment.service.interfaces;
using polyclinic_project.system.constants;
using polyclinic_project.system.interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic_project_tests.Tests.services
{
    public class TestAppointmentQueryService
    {
        private IRepository<Appointment> _repository = new AppointmentRepository("path");
        private IAppointmentQueryService _query;

        [Fact]
        public void TestFindByDateDateTime_AppointmentNotFound_ThrowsInvalidOperationException()
        {
            // Arrange
            _query = new AppointmentQueryService(_repository);
            DateTime date = DateTime.ParseExact("14.09.2023 21:10", Constants.STANDARD_DATE_FORMAT, CultureInfo.InvariantCulture);

            // Assert
            Assert.Throws<InvalidOperationException>(() => _query.FindByDate(date));
        }

        [Fact]
        public void TestFindByDateDateTime_AppointmentFound_ReturnsAppointment()
        {
            // Arrange
            _query = new AppointmentQueryService(_repository);
            DateTime date = DateTime.ParseExact("14.09.2023 22:00", Constants.STANDARD_DATE_FORMAT, CultureInfo.InvariantCulture);
            Appointment appointment = IAppointmentBuilder.BuildAppointment()
                .Id(1)
                .StartDate(DateTime.ParseExact("14.09.2023 21:50", Constants.STANDARD_DATE_FORMAT, CultureInfo.InvariantCulture))
                .EndDate(DateTime.ParseExact("14.09.2023 22:30", Constants.STANDARD_DATE_FORMAT, CultureInfo.InvariantCulture));
            _repository.GetList().Add(appointment);

            // Act
            Appointment found = _query.FindByDate(date);

            // Assert
            Assert.NotNull(found);
            Assert.Equal(appointment, found, new AppointmentEqualityComparer());
            _repository.GetList().Remove(appointment);
        }

        [Fact]
        public void TestFindByDateTime_MultipleAppointments_ReturnsCorrectAppointment()
        {
            // Arrange
            _query = new AppointmentQueryService(_repository);
            DateTime date = DateTime.ParseExact("14.09.2023 22:00", Constants.STANDARD_DATE_FORMAT, CultureInfo.InvariantCulture);
            Appointment appointment = IAppointmentBuilder.BuildAppointment()
                .Id(1)
                .StartDate(DateTime.ParseExact("14.09.2023 21:50", Constants.STANDARD_DATE_FORMAT, CultureInfo.InvariantCulture))
                .EndDate(DateTime.ParseExact("14.09.2023 22:30", Constants.STANDARD_DATE_FORMAT, CultureInfo.InvariantCulture));
            Appointment another = IAppointmentBuilder.BuildAppointment()
                .Id(2)
                .StartDate(DateTime.ParseExact("14.09.2023 18:50", Constants.STANDARD_DATE_FORMAT, CultureInfo.InvariantCulture))
                .EndDate(DateTime.ParseExact("14.09.2023 19:30", Constants.STANDARD_DATE_FORMAT, CultureInfo.InvariantCulture));
            _repository.GetList().Add(appointment);
            _repository.GetList().Add(another);

            // Act
            Appointment found = _query.FindByDate(date);

            // Assert
            Assert.NotNull(found);
            Assert.Equal(appointment, found, new AppointmentEqualityComparer());
            Assert.NotEqual(another, found, new AppointmentEqualityComparer());
            _repository.GetList().Remove(appointment);
            _repository.GetList().Remove(another);
        }

        [Fact]
        public void TestFindByDateString_AppointmentNotFound_ThrowsInvalidOperationException()
        {
            // Arrange
            _query = new AppointmentQueryService(_repository);
            String date = "14.09.2023 21:10";

            // Assert
            Assert.Throws<InvalidOperationException>(() => _query.FindByDate(date));
        }

        [Fact]
        public void TestFindByDateString_AppointmentFound_ReturnsAppointment()
        {
            // Arrange
            _query = new AppointmentQueryService(_repository);
            String date = "14.09.2023 22:00";
            Appointment appointment = IAppointmentBuilder.BuildAppointment()
                .Id(1)
                .StartDate(DateTime.ParseExact("14.09.2023 21:50", Constants.STANDARD_DATE_FORMAT, CultureInfo.InvariantCulture))
                .EndDate(DateTime.ParseExact("14.09.2023 22:30", Constants.STANDARD_DATE_FORMAT, CultureInfo.InvariantCulture));
            _repository.GetList().Add(appointment);

            // Act
            Appointment found = _query.FindByDate(date);

            // Assert
            Assert.NotNull(found);
            Assert.Equal(appointment, found, new AppointmentEqualityComparer());
            _repository.GetList().Remove(appointment);
        }

        [Fact]
        public void TestFindByString_MultipleAppointments_ReturnsCorrectAppointment()
        {
            // Arrange
            _query = new AppointmentQueryService(_repository);
            String date = "14.09.2023 22:00";
            Appointment appointment = IAppointmentBuilder.BuildAppointment()
                .Id(1)
                .StartDate(DateTime.ParseExact("14.09.2023 21:50", Constants.STANDARD_DATE_FORMAT, CultureInfo.InvariantCulture))
                .EndDate(DateTime.ParseExact("14.09.2023 22:30", Constants.STANDARD_DATE_FORMAT, CultureInfo.InvariantCulture));
            Appointment another = IAppointmentBuilder.BuildAppointment()
                .Id(2)
                .StartDate(DateTime.ParseExact("14.09.2023 18:50", Constants.STANDARD_DATE_FORMAT, CultureInfo.InvariantCulture))
                .EndDate(DateTime.ParseExact("14.09.2023 19:30", Constants.STANDARD_DATE_FORMAT, CultureInfo.InvariantCulture));
            _repository.GetList().Add(appointment);
            _repository.GetList().Add(another);

            // Act
            Appointment found = _query.FindByDate(date);

            // Assert
            Assert.NotNull(found);
            Assert.Equal(appointment, found, new AppointmentEqualityComparer());
            Assert.NotEqual(another, found, new AppointmentEqualityComparer());
            _repository.GetList().Remove(appointment);
            _repository.GetList().Remove(another);
        }
    }
}
