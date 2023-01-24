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
        public async void getDoctorSheldueByNegativeId_Fail()
        {
            // Given
            int doctor_id = -1;
        
            // When
            var res = await usecases.getDoctorSheldue(doctor_id);
        
            // Then
            Assert.False(res.Success);
            Assert.Equal(res.Error, "The doctor_id is negative");
        }

        [Fact]
        public async void getDoctorSheldueByIncorrectId_Fail()
        {
            // Given
            int doctor_id = 1;
            doctorRepository.Setup(rep => rep.isExist(doctor_id)).ReturnsAsync(false);
        
            // When
            var res = await usecases.getDoctorSheldue(doctor_id);
        
            // Then
            Assert.False(res.Success);
            Assert.Equal(res.Error, "Such doctor doesn't exist");
        }

        [Fact]
        public async void getDoctorSheldueByCorrectId_Ok()
        {
            // Given
            int doctor_id = 1;
            doctorRepository.Setup(rep => rep.isExist(doctor_id)).ReturnsAsync(true);
        
            // When
            var res = await usecases.getDoctorSheldue(doctor_id);
        
            // Then
            Assert.True(res.Success);
            Assert.Equal(res.Error, "");
        }
    }
}