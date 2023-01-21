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
        public void createDoctorEmptyFio_Fail()
        {
            // Given
            var doctor = new Doctor("", 666);
            spec_repository.Setup(rep => rep.isExist(666)).Returns(true);
        
            // When
            var res = usecases.createDoctor(doctor);
        
            // Then
            Assert.False(res.Success);
            Assert.Equal(res.Error, "Empty field of FIO");
        }

        [Fact]
        public void createDoctorCorrectFio_Ok()
        {
            // Given
            var doctor = new Doctor("something", 666);
            spec_repository.Setup(rep => rep.isExist(666)).Returns(true);
        
            // When
            var res = usecases.createDoctor(doctor);
        
            // Then
            Assert.True(res.Success);
            Assert.Equal(res.Error, "");
        }

        [Fact]
        public void deleteDoctorNegativeId_Fail()
        {
            // Given
            var doctor_id = -1;
        
            // When
            var res = usecases.deleteDoctor(doctor_id);
        
            // Then
            Assert.False(res.Success);
            Assert.Equal(res.Error, "Incorrect ID");
        }

        [Fact]
        public void deleteDoctorCorrectId_Ok()
        {
            // Given
            var doctor_id = 1;
        
            // When
            var res = usecases.deleteDoctor(doctor_id);
        
            // Then
            Assert.True(res.Success);
            Assert.Equal(res.Error, "");
        }

        [Fact]
        public void getAllDoctors_Ok()
        {
            // Given
            
            // When
            var res = usecases.getAllDoctors();
        
            // Then
            Assert.True(res.Success);
            Assert.Equal(res.Error, "");
        }

        [Fact]
        public void getDoctorByIncorrectId_Fail()
        {
            // Given
            var doctor_id = -2;
            
            // When
            var res = usecases.getDoctorById(doctor_id);
        
            // Then
            Assert.False(res.Success);
            Assert.Equal(res.Error, "Incorrect ID");
        }

        [Fact]
        public void getDoctorByNonExistingId_Fail()
        {
            // Given
            var doctor_id = 2;
            repository.Setup(rep => rep.isExist(doctor_id)).Returns(false);
            
            // When
            var res = usecases.getDoctorById(doctor_id);
        
            // Then
            Assert.False(res.Success);
            Assert.Equal(res.Error, "Such doctor doesn't exist");
        }

        [Fact]
        public void getDoctorByCorrectId_Ok()
        {
            // Given
            var doctor_id = 2;
            repository.Setup(rep => rep.isExist(doctor_id)).Returns(true);
            
            // When
            var res = usecases.getDoctorById(doctor_id);
        
            // Then
            Assert.True(res.Success);
            Assert.Equal(res.Error, "");
        }

        [Fact]
        public void getDoctorsByNegativeSpecId_Fail()
        {
            // Given
            var spec_id = -1;
            
            // When
            var res = usecases.getDoctorsBySpecialisation(spec_id);
        
            // Then
            Assert.False(res.Success);
            Assert.Equal(res.Error, "Incorrect specialisation ID");
        }

        [Fact]
        public void getDoctorsByNonExistingSpecId_Fail()
        {
            // Given
            var spec_id = 3;
            spec_repository.Setup(rep => rep.isExist(spec_id)).Returns(false);
            
            // When
            var res = usecases.getDoctorsBySpecialisation(spec_id);
        
            // Then
            Assert.False(res.Success);
            Assert.Equal(res.Error, "Incorrect specialisation ID");
        }

        [Fact]
        public void getDoctorsByCorrectSpecId_Ok()
        {
            // Given
            var spec_id = 1;
            List<Doctor> doctor_list = new List<Doctor>() {new Doctor("Vasya", 1)};
            spec_repository.Setup(rep => rep.isExist(spec_id)).Returns(true);
            repository.Setup(rep => rep.findDoctorListBySpecialisation(spec_id)).Returns(doctor_list);

            
            // When
            var res = usecases.getDoctorsBySpecialisation(spec_id);
        
            // Then
            Assert.True(res.Success);
            Assert.Equal(res.Error, "");
        }
        
    }
}