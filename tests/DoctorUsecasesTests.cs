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
        public void createDoctorEmptyFio_Fail()
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
        public void createDoctorCorrectFio_Ok()
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
    }
}