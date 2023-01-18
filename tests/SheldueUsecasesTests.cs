using Moq;
using Xunit;
using System.Collections.Generic;
using System;

using domain.models.sheldue;
using domain.models.doctor;
using domain.models;

namespace tests
{
    public class SheldueUsecasesTests
    {
        private readonly Mock<ISheldueRepository> repository;
        private readonly Mock<IDoctorRepository> doctorRepository;
        
        private readonly SheldueUsecases usecases;

        public SheldueUsecasesTests() {
            repository = new Mock<ISheldueRepository>();
            doctorRepository = new Mock<IDoctorRepository>();
            usecases = new SheldueUsecases(repository.Object, doctorRepository.Object);
        }

        [Fact]
        public void getDoctorSheldueByNegativeId_Fail()
        {
            // Given
            int doctor_id = -1;
        
            // When
            var res = usecases.getDoctorSheldue(doctor_id);
        
            // Then
            Assert.False(res.Success);
            Assert.Equal(res.Error, "The doctor_id is negative");
        }

        [Fact]
        public void getDoctorSheldueByIncorrectId_Fail()
        {
            // Given
            int doctor_id = 1;
            doctorRepository.Setup(rep => rep.isExist(doctor_id)).Returns(false);
        
            // When
            var res = usecases.getDoctorSheldue(doctor_id);
        
            // Then
            Assert.False(res.Success);
            Assert.Equal(res.Error, "Such doctor doesn't exist");
        }

        [Fact]
        public void getDoctorSheldueByCorrectId_Ok()
        {
            // Given
            int doctor_id = 1;
            doctorRepository.Setup(rep => rep.isExist(doctor_id)).Returns(true);
        
            // When
            var res = usecases.getDoctorSheldue(doctor_id);
        
            // Then
            Assert.True(res.Success);
            Assert.Equal(res.Error, "");
        }

        [Fact]
        public void addDoctorAppointmentByNegativeDoctorId_Fail()
        {
            // Given
            Appointment app = new Appointment(new DateTime(1, 1, 1, 14, 20, 0), 1, 0);
        
            // When
            var res = usecases.addDoctorAppointment(app);
        
            // Then
            Assert.False(res.Success);
            Assert.Equal(res.Error, "The doctor_id is negative");
        }

        [Fact]
        public void addDoctorAppointmentByIncorrectDoctorId_Fail()
        {
            // Given
            Appointment app = new Appointment(new DateTime(1, 1, 1, 14, 20, 0), 1, 1);
            doctorRepository.Setup(rep => rep.isExist(app.doctor_id)).Returns(false);
        
            // When
            var res = usecases.addDoctorAppointment(app);
        
            // Then
            Assert.False(res.Success);
            Assert.Equal(res.Error, "Such doctor doesn't exist");
        }

        [Fact]
        public void addDoctorAppointmentByIncorrectTime_Fail()
        {
            // Given
            Appointment app = new Appointment(new DateTime(1, 1, 1, 18, 20, 0), 1, 1);
            doctorRepository.Setup(rep => rep.isExist(app.doctor_id)).Returns(true);
        
            // When
            var res = usecases.addDoctorAppointment(app);
        
            // Then
            Assert.False(res.Success);
            Assert.Equal(res.Error, "Such time doesn't exist");
        }

        [Fact]
        public void addDoctorAppointmentByBusyTime_Fail()
        {
            // Given
            Appointment app = new Appointment(new DateTime(1, 1, 1, 17, 20, 0), 1, 1);
            doctorRepository.Setup(rep => rep.isExist(app.doctor_id)).Returns(true);
            repository.Setup(rep => rep.isDoctorTimeIsBusy(app.doctor_id, app.start)).Returns(true);
        
            // When
            var res = usecases.addDoctorAppointment(app);
        
            // Then
            Assert.False(res.Success);
            Assert.Equal(res.Error, "The appointment time is already busy");
        }

        [Fact]
        public void addDoctorAppointmentCorrectData_Ok()
        {
            // Given
            Appointment app = new Appointment(new DateTime(1, 1, 1, 17, 20, 0), 1, 1);
            doctorRepository.Setup(rep => rep.isExist(app.doctor_id)).Returns(true);
            repository.Setup(rep => rep.isDoctorTimeIsBusy(app.doctor_id, app.start)).Returns(false);
        
            // When
            var res = usecases.addDoctorAppointment(app);
        
            // Then
            Assert.True(res.Success);
            Assert.Equal(res.Error, "");
        }

    }
}