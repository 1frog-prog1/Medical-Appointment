using Moq;
using Xunit;
using System.Collections.Generic;
using System;

using domain.models.appointment;
using domain.models;

namespace tests
{
    public class AppointmentUsecasesTests
    {
        private readonly Mock<IAppointmentRepository> repository;
        private readonly AppointmentUsecases usecases;

        public AppointmentUsecasesTests() {
            repository = new Mock<IAppointmentRepository>();
            usecases = new AppointmentUsecases(repository.Object);
        }

        [Fact]
        public void saveAppointmentByIncorrectTime_Fail()
        {
            // Given
            int doctor_id = 1;
            int patient_id = 1;

            DateTime start = new DateTime(2023, 12, 30, 18, 20, 0);
            DateTime end = new DateTime(2023, 12, 30, 18, 40, 0);
            Appointment appointment = new Appointment(start, end, doctor_id, patient_id);
            
            repository.Setup(rep => rep.isDoctorExist(doctor_id)).Returns(true);
        
            // When
            var res = usecases.saveAppointmentByDoctorId(appointment);
        
            // Then
            Assert.False(res.Success);
            Assert.Equal(res.Error, "Such time doesn't exist");
        }

        [Fact]
        public void saveAppointmentByIncorrectDoctorId_Fail()
        {
            // Given
            int doctor_id = 1;
            int patient_id = 1;

            DateTime start = new DateTime(2023, 12, 30, 12, 20, 0);
            DateTime end = new DateTime(2023, 12, 30, 12, 40, 0);
            Appointment appointment = new Appointment(start, end, doctor_id, patient_id);
            
            repository.Setup(rep => rep.isDoctorExist(doctor_id)).Returns(false);
        
            // When
            var res = usecases.saveAppointmentByDoctorId(appointment);
        
            // Then
            Assert.False(res.Success);
            Assert.Equal(res.Error, "Such doctor doesn't exist");
        }

        [Fact]
        public void saveAppointmentAtBusyTime_Fail()
        {
            // Given
            int doctor_id = 1;
            int patient_id = 1;

            DateTime start = new DateTime(2023, 12, 30, 12, 20, 0);
            DateTime end = new DateTime(2023, 12, 30, 12, 40, 0);
            Appointment appointment = new Appointment(start, end, doctor_id, patient_id);
            
            repository.Setup(rep => rep.isDoctorExist(doctor_id)).Returns(true);
            repository.Setup(rep => rep.isDoctorFreeAtTime(start, doctor_id)).Returns(false);
        
            // When
            var res = usecases.saveAppointmentByDoctorId(appointment);
        
            // Then
            Assert.False(res.Success);
            Assert.Equal(res.Error, "The time is already busy");
        }

        [Fact]
        public void saveAppointmentAtFreeTime_Ok()
        {
            // Given
            int doctor_id = 1;
            int patient_id = 1;

            DateTime start = new DateTime(2023, 12, 30, 12, 20, 0);
            DateTime end = new DateTime(2023, 12, 30, 12, 40, 0);
            Appointment appointment = new Appointment(start, end, doctor_id, patient_id);
            
            repository.Setup(rep => rep.isDoctorExist(doctor_id)).Returns(true);
            repository.Setup(rep => rep.isDoctorFreeAtTime(start, doctor_id)).Returns(true);
        
            // When
            var res = usecases.saveAppointmentByDoctorId(appointment);
        
            // Then
            Assert.True(res.Success);
            Assert.Equal(res.Error, "");
        }

    }
}