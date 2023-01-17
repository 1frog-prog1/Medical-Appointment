using Moq;
using Xunit;

using domain.models.doctor;
using domain.models;

namespace tests
{
    public class DoctorUsecasesTests
    {
        private readonly DoctorUsecases usecases;
        private readonly Mock<IDoctorRepository> repository;

        public DoctorUsecasesTests() {
            repository = new Mock<IDoctorRepository>();
            usecases = new DoctorUsecases(repository.Object);
        }

        [Fact]
        public void createUserEmptyFio_Fail()
        {
            // Given
            var doctor = new Doctor("", 666);
        
            // When
            var res = usecases.createDoctor(doctor);
        
            // Then
            Assert.False(res.Success);
            Assert.Equal(res.Error, "Incorrect format of data");
        }

        [Fact]
        public void createUserCorrectFio_Ok()
        {
            // Given
            var doctor = new Doctor("something", 666);
        
            // When
            var res = usecases.createDoctor(doctor);
        
            // Then
            Assert.True(res.Success);
            Assert.Equal(res.Error, "");
        }

        [Fact]
        public void deleteUserNegativeId_Fail()
        {
            // Given
            var doctor_id = -1;
        
            // When
            var res = usecases.deleteDoctor(doctor_id);
        
            // Then
            Assert.False(res.Success);
            Assert.Equal(res.Error, "Incorrect format of ID");
        }

        [Fact]
        public void deleteUserCorrectId_Ok()
        {
            // Given
            var doctor_id = 1;
        
            // When
            var res = usecases.deleteDoctor(doctor_id);
        
            // Then
            Assert.True(res.Success);
            Assert.Equal(res.Error, "");
        }

    }
}