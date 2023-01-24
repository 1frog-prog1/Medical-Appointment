using Moq;
using Xunit;
using System.Collections.Generic;

using domain.models.doctor;
using domain.models;
using domain.models.specialisation;

namespace tests
{
    public class DoctorUsecasesTests
    {
        private readonly DoctorUsecases usecases;
        private readonly Mock<IDoctorRepository> repository;
        private readonly Mock<ISpecialisationRepository> spec_repository;

        public DoctorUsecasesTests() {
            repository = new Mock<IDoctorRepository>();
            spec_repository = new Mock<ISpecialisationRepository>();
            usecases = new DoctorUsecases(repository.Object, spec_repository.Object);
        }

        [Fact]
        public async void createDoctorEmptyFio_Fail()
        {
            // Given
            var doctor = new Doctor("", 666);
            spec_repository.Setup(rep => rep.isExist(666)).ReturnsAsync(true);
        
            // When
            var res = await usecases.createDoctor(doctor);
        
            // Then
            Assert.False(res.Success);
            Assert.Equal(res.Error, "Empty field of FIO");
        }

        [Fact]
        public async void createDoctorCorrectFio_Ok()
        {
            // Given
            var doctor = new Doctor("something", 666);
            spec_repository.Setup(rep => rep.isExist(666)).ReturnsAsync(true);
        
            // When
            var res = await usecases.createDoctor(doctor);
        
            // Then
            Assert.True(res.Success);
            Assert.Equal(res.Error, "");
        }

        [Fact]
        public async void deleteDoctorNegativeId_Fail()
        {
            // Given
            var doctor_id = -1;
        
            // When
            var res = await usecases.deleteDoctor(doctor_id);
        
            // Then
            Assert.False(res.Success);
            Assert.Equal(res.Error, "Incorrect ID");
        }

        [Fact]
        public async void deleteDoctorCorrectId_Ok()
        {
            // Given
            var doctor_id = 1;
        
            // When
            var res = await usecases.deleteDoctor(doctor_id);
        
            // Then
            Assert.True(res.Success);
            Assert.Equal(res.Error, "");
        }

        [Fact]
        public async void getAllDoctors_Ok()
        {
            // Given
            
            // When
            var res = await usecases.getAllDoctors();
        
            // Then
            Assert.True(res.Success);
            Assert.Equal(res.Error, "");
        }

        [Fact]
        public async void getDoctorByIncorrectId_Fail()
        {
            // Given
            var doctor_id = -2;
            
            // When
            var res = await usecases.getDoctorById(doctor_id);
        
            // Then
            Assert.False(res.Success);
            Assert.Equal(res.Error, "Incorrect ID");
        }

        [Fact]
        public async void getDoctorByNonExistingId_Fail()
        {
            // Given
            var doctor_id = 2;
            repository.Setup(rep => rep.isExist(doctor_id)).ReturnsAsync(false);
            
            // When
            var res = await usecases.getDoctorById(doctor_id);
        
            // Then
            Assert.False(res.Success);
            Assert.Equal(res.Error, "Such doctor doesn't exist");
        }

        [Fact]
        public async void getDoctorByCorrectId_Ok()
        {
            // Given
            var doctor_id = 2;
            repository.Setup(rep => rep.isExist(doctor_id)).ReturnsAsync(true);
            
            // When
            var res = await usecases.getDoctorById(doctor_id);
        
            // Then
            Assert.True(res.Success);
            Assert.Equal(res.Error, "");
        }

        [Fact]
        public async void getDoctorsByNegativeSpecId_Fail()
        {
            // Given
            var spec_id = -1;
            
            // When
            var res = await usecases.getDoctorsBySpecialisation(spec_id);
        
            // Then
            Assert.False(res.Success);
            Assert.Equal(res.Error, "Incorrect specialisation ID");
        }

        [Fact]
        public async void getDoctorsByNonExistingSpecId_Fail()
        {
            // Given
            var spec_id = 3;
            spec_repository.Setup(rep => rep.isExist(spec_id)).ReturnsAsync(false);
            
            // When
            var res = await usecases.getDoctorsBySpecialisation(spec_id);
        
            // Then
            Assert.False(res.Success);
            Assert.Equal(res.Error, "Incorrect specialisation ID");
        }

        [Fact]
        public async void getDoctorsByCorrectSpecId_Ok()
        {
            // Given
            var spec_id = 1;
            List<Doctor> doctor_list = new List<Doctor>() {new Doctor("Vasya", 1)};
            spec_repository.Setup(rep => rep.isExist(spec_id)).ReturnsAsync(true);
            repository.Setup(rep => rep.findDoctorListBySpecialisation(spec_id)).ReturnsAsync(doctor_list);

            
            // When
            var res = await usecases.getDoctorsBySpecialisation(spec_id);
        
            // Then
            Assert.True(res.Success);
            Assert.Equal(res.Error, "");
        }
        
    }
}