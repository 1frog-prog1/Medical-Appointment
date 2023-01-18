using Moq;
using Xunit;
using System.Collections.Generic;
using System;

using domain.models.appointment;
using domain.models;
using domain.models.specialisation;

namespace tests
{
    public class AppointmentUsecasesTests
    {
        private readonly Mock<IAppointmentRepository> repository;
        private readonly Mock<ISpecialisationRepository> spec_repository;
        private readonly AppointmentUsecases usecases;

        public AppointmentUsecasesTests() {
            repository = new Mock<IAppointmentRepository>();
            spec_repository =  new Mock<ISpecialisationRepository>();
            usecases = new AppointmentUsecases(repository.Object, spec_repository.Object);
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
        public void saveAppointmentAtBusyTimeByDoctorId_Fail()
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
        public void saveAppointmentAtFreeTimeByDoctorId_Ok()
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

        [Fact]
        public void saveAppointmentAtIncorrectTimeToAnyDoctor_Fail()
        {
            // Given
            int patient_id = 1;
            int spec_id = 1;
            DateTime start = new DateTime(2023, 12, 30, 18, 20, 0);
        
            // When
            var res = usecases.saveAppointmentToAnyDoctor(start, spec_id, patient_id);
        
            // Then
            Assert.False(res.Success);
            Assert.Equal(res.Error, "Such time doesn't exist");
        }

        [Fact]
        public void saveAppointmentAtNonExistingSpecToAnyDoctor_Fail()
        {
            // Given
            int patient_id = 1;
            int spec_id = 1;
            DateTime start = new DateTime(2023, 12, 30, 12, 20, 0);

            spec_repository.Setup(rep => rep.isExist(spec_id)).Returns(false);
        
            // When
            var res = usecases.saveAppointmentToAnyDoctor(start, spec_id, patient_id);
        
            // Then
            Assert.False(res.Success);
            Assert.Equal(res.Error, "Such specialisation doesn't exist");
        }

        [Fact]
        public void saveAppointmentAtBusyTimeToAnyDoctor_Fail()
        {
            // Given
            int patient_id = 1;
            int spec_id = 1;
            DateTime start = new DateTime(2023, 12, 30, 12, 20, 0);

            spec_repository.Setup(rep => rep.isExist(spec_id)).Returns(true);
            repository.Setup(rep => rep.isAnyDoctorFreeAtTime(spec_id, start)).Returns(false);
        
            // When
            var res = usecases.saveAppointmentToAnyDoctor(start, spec_id, patient_id);
        
            // Then
            Assert.False(res.Success);
            Assert.Equal(res.Error, "The time is already busy");
        }

        [Fact]
        public void saveAppointmentCorrectDataToAnyDoctor_Ok()
        {
            // Given
            int patient_id = 1;
            int spec_id = 1;
            DateTime start = new DateTime(2023, 12, 30, 12, 20, 0);

            spec_repository.Setup(rep => rep.isExist(spec_id)).Returns(true);
            repository.Setup(rep => rep.isAnyDoctorFreeAtTime(spec_id, start)).Returns(true);
        
            // When
            var res = usecases.saveAppointmentToAnyDoctor(start, spec_id, patient_id);
        
            // Then
            Assert.True(res.Success);
            Assert.Equal(res.Error, "");
        }

        [Fact]
        public void getAppointmentsByIncorrectSpecialisationId_Fail()
        {
            // Given
            int spec_id = 1;

            spec_repository.Setup(rep => rep.isExist(spec_id)).Returns(false);        
            // When
            var res = usecases.getAllFreeTimeBySpecialisationId(spec_id);
        
            // Then
            Assert.False(res.Success);
            Assert.Equal(res.Error, "Such specialisation doesn't exist");
        }

        [Fact]
        public void getAppointmentsByCorrectSpecialisationId_Ok()
        {
            // Given
            int spec_id = 1;

            spec_repository.Setup(rep => rep.isExist(spec_id)).Returns(true);        
            // When
            var res = usecases.getAllFreeTimeBySpecialisationId(spec_id);
        
            // Then
            Assert.True(res.Success);
            Assert.Equal(res.Error, "");
        }

    }
}