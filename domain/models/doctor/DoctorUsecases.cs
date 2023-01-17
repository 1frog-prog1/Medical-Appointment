using System.Linq;

using domain.models.specialisation;

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
                return Result.Fail<int>("Incorrect ID");
            
            repository.delete(doctor_id); // existence can check DB
            return Result.Ok<int>(doctor_id);
        }

        public Result<List<Doctor>> getAllDoctors() {
            var doctor_container = repository.Get();
            List<Doctor> doctor_list = doctor_container.ToList<Doctor>();
            return Result.Ok<List<Doctor>>(doctor_list); // no danger: list can be empty?
        }

        public Result<Doctor> getDoctorById(int doctor_id) {
            if (doctor_id <= 0)
                return Result.Fail<Doctor>("Incorrect ID");

            if (!repository.isExist(doctor_id))
                return Result.Fail<Doctor>("Such doctor doesn't exist");

            var doctor = repository.findDoctorByID(doctor_id);
            return Result.Ok<Doctor>(doctor);
        }

        public Result<List<Doctor>> getDoctorsBySpecialisation(int spec_id) {
            ISpecialisationRepository spec_repository;
            if (spec_id <= 0)
                return Result.Fail<List<Doctor>>("Incorrect specialisation ID");
            // сделать проверку на существование этой специализации
            var doctor_container = repository.findDoctorListBySpecialisation(spec_id);
            var doctor_list = doctor_container.ToList<Doctor>();
            return Result.Ok<List<Doctor>>(doctor_list);
        }

    }
}