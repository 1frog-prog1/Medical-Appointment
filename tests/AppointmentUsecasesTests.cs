using Moq;
using Xunit;
using System.Collections.Generic;
using System;

using domain.models.appointment;
using domain.models;
using domain.models.specialisation;
using domain.models.doctor;

namespace tests
{
    public class AppointmentUsecasesTests
    {
        private readonly Mock<IAppointmentRepository> repository;
        private readonly Mock<ISpecialisationRepository> spec_repository;
        private readonly Mock<IDoctorRepository> doc_repository;
        private readonly AppointmentUsecases usecases;

        public AppointmentUsecasesTests() {
            repository = new Mock<IAppointmentRepository>();
            spec_repository =  new Mock<ISpecialisationRepository>();
            doc_repository = new Mock<IDoctorRepository>();
            usecases = new AppointmentUsecases(repository.Object, spec_repository.Object, doc_repository.Object);
        }

        [Fact]
        public async void saveAppointmentByIncorrectTime_Fail()
        {
            // Given
            int doctor_id = 1;
            int patient_id = 1;

            DateTime start = new DateTime(2023, 12, 30, 18, 20, 0);
            Appointment appointment = new Appointment(start, doctor_id, patient_id);
            
            doc_repository.Setup(rep => rep.isExist(doctor_id)).ReturnsAsync(true);
        
            // When
            var res = await usecases.saveAppointmentByDoctorId(appointment);
        
            // Then
            Assert.False(res.Success);
            Assert.Equal(res.Error, "Such time doesn't exist");
        }

        [Fact]
        public async void saveAppointmentByIncorrectDoctorId_Fail()
        {
            // Given
            int doctor_id = 1;
            int patient_id = 1;

            DateTime start = new DateTime(2023, 12, 30, 12, 20, 0);
            Appointment appointment = new Appointment(start, doctor_id, patient_id);
            
            doc_repository.Setup(rep => rep.isExist(doctor_id)).ReturnsAsync(false);
        
            // When
            var res = await usecases.saveAppointmentByDoctorId(appointment);
        
            // Then
            Assert.False(res.Success);
            Assert.Equal(res.Error, "Such doctor doesn't exist");
        }

        [Fact]
        public async void saveAppointmentAtBusyTimeByDoctorId_Fail()
        {
            // Given
            int doctor_id = 1;
            int patient_id = 1;

            DateTime start = new DateTime(2023, 12, 30, 12, 20, 0);
            Appointment appointment = new Appointment(start, doctor_id, patient_id);
            
            doc_repository.Setup(rep => rep.isExist(doctor_id)).ReturnsAsync(true);
            repository.Setup(rep => rep.isDoctorFreeAtTime(start, doctor_id)).ReturnsAsync(false);
        
            // When
            var res = await usecases.saveAppointmentByDoctorId(appointment);
        
            // Then
            Assert.False(res.Success);
            Assert.Equal(res.Error, "The time is already busy");
        }

        [Fact]
        public async void saveAppointmentAtFreeTimeByDoctorId_Ok()
        {
            // Given
            int doctor_id = 1;
            int patient_id = 1;

            DateTime start = new DateTime(2023, 12, 30, 12, 20, 0);
            Appointment appointment = new Appointment(start, doctor_id, patient_id);
            
            doc_repository.Setup(rep => rep.isExist(doctor_id)).ReturnsAsync(true);
            repository.Setup(rep => rep.isDoctorFreeAtTime(start, doctor_id)).ReturnsAsync(true);
        
            // When
            var res = await usecases.saveAppointmentByDoctorId(appointment);
        
            // Then
            Assert.True(res.Success);
            Assert.Equal(res.Error, "");
        }

        [Fact]
        public async void saveAppointmentAtIncorrectTimeToAnyDoctor_Fail()
        {
            // Given
            int patient_id = 1;
            int spec_id = 1;
            DateTime start = new DateTime(2023, 12, 30, 18, 20, 0);
        
            // When
            var res = await usecases.saveAppointmentToAnyDoctor(start, spec_id, patient_id);
        
            // Then
            Assert.False(res.Success);
            Assert.Equal(res.Error, "Such time doesn't exist");
        }

        [Fact]
        public async void saveAppointmentAtNonExistingSpecToAnyDoctor_Fail()
        {
            // Given
            int patient_id = 1;
            int spec_id = 1;
            DateTime start = new DateTime(2023, 12, 30, 12, 20, 0);

            spec_repository.Setup(rep => rep.isExist(spec_id)).ReturnsAsync(false);
        
            // When
            var res = await usecases.saveAppointmentToAnyDoctor(start, spec_id, patient_id);
        
            // Then
            Assert.False(res.Success);
            Assert.Equal(res.Error, "Such specialisation doesn't exist");
        }

        [Fact]
        public async void saveAppointmentAtBusyTimeToAnyDoctor_Fail()
        {
            // Given
            int patient_id = 1;
            int spec_id = 1;
            DateTime start = new DateTime(2023, 12, 30, 12, 20, 0);

            spec_repository.Setup(rep => rep.isExist(spec_id)).ReturnsAsync(true);
            repository.Setup(rep => rep.isAnyDoctorFreeAtTime(spec_id, start)).ReturnsAsync(false);
        
            // When
            var res = await usecases.saveAppointmentToAnyDoctor(start, spec_id, patient_id);
        
            // Then
            Assert.False(res.Success);
            Assert.Equal(res.Error, "The time is already busy");
        }

        [Fact]
        public async void saveAppointmentCorrectDataToAnyDoctor_Ok()
        {
            // Given
            int patient_id = 1;
            int spec_id = 1;
            DateTime start = new DateTime(2023, 12, 30, 12, 20, 0);

            spec_repository.Setup(rep => rep.isExist(spec_id)).ReturnsAsync(true);
            repository.Setup(rep => rep.isAnyDoctorFreeAtTime(spec_id, start)).ReturnsAsync(true);
        
            // When
            var res = await usecases.saveAppointmentToAnyDoctor(start, spec_id, patient_id);
        
            // Then
            Assert.True(res.Success);
            Assert.Equal(res.Error, "");
        }

        [Fact]
        public async void getAppointmentsByIncorrectSpecialisationId_Fail()
        {
            // Given
            int spec_id = 1;

            spec_repository.Setup(rep => rep.isExist(spec_id)).ReturnsAsync(false);        
            // When
            var res = await usecases.getAllFreeTimeBySpecialisationId(spec_id);
        
            // Then
            Assert.False(res.Success);
            Assert.Equal(res.Error, "Such specialisation doesn't exist");
        }

        [Fact]
        public async void getAppointmentsByCorrectSpecialisationId_Ok()
        {
            // Given
            int spec_id = 1;

            spec_repository.Setup(rep => rep.isExist(spec_id)).ReturnsAsync(true);        
            // When
            var res = await usecases.getAllFreeTimeBySpecialisationId(spec_id);
        
            // Then
            Assert.True(res.Success);
            Assert.Equal(res.Error, "");
        }

    }
}