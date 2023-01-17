namespace domain.models.doctor
{
    public class DoctorUsecases
    {
        private readonly IDoctorRepository repository;

        public DoctorUsecases(IDoctorRepository repository) {
            this.repository = repository;
        }

        public Result<Doctor> createDoctor(Doctor doctor) {
            if (string.IsNullOrEmpty(doctor.fio)) // todo: add check that specialisation really exists
                return Result.Fail<Doctor>("Incorrect format of data");
            repository.create(doctor);
            
            return Result.Ok<Doctor>(doctor);
        }


    }
}