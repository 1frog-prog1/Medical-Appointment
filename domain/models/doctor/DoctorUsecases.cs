namespace domain.models.doctor
{
    public class DoctorUsecases
    {
        private readonly IDoctorRepository repository;

        public DoctorUsecases(IDoctorRepository repository) {
            this.repository = repository;
        }

        public Result<Doctor> createDoctor(Doctor doctor) {
            if (string.IsNullOrEmpty(doctor.fio)) // todo: add check that specialisation really exists after
                return Result.Fail<Doctor>("Incorrect format of data");
            
            repository.create(doctor);
            return Result.Ok<Doctor>(doctor);
        }

        public Result<int> deleteDoctor(int doctor_id) {
            if (doctor_id <= 0)
                return Result.Fail<int>("Incorrect format of ID");
            
            repository.delete(doctor_id); // existence can check DB
            return Result.Ok<int>(doctor_id);
        }


    }
}