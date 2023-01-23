using System.Linq;

using domain.models.specialisation;

namespace domain.models.doctor
{
    public class DoctorUsecases
    {
        private readonly IDoctorRepository repository;
        private readonly ISpecialisationRepository spec_repository;

        public DoctorUsecases(IDoctorRepository repository,
                            ISpecialisationRepository spec_repository) {
            this.repository = repository;
            this.spec_repository = spec_repository;
        }

        public Result<Doctor> createDoctor(Doctor doctor) {
            if (string.IsNullOrEmpty(doctor.fio))
                return Result.Fail<Doctor>("Empty field of FIO");
            if (!spec_repository.isExist(doctor.specialisation_id))
                return Result.Fail<Doctor>("Such specialisation doesn't exist");
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
            var doctor_list = repository.getAll();
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
            if (spec_id <= 0 || !spec_repository.isExist(spec_id))
                return Result.Fail<List<Doctor>>("Incorrect specialisation ID");
            var doctor_container = repository.findDoctorListBySpecialisation(spec_id);
            var doctor_list = doctor_container.ToList<Doctor>();
            return Result.Ok<List<Doctor>>(doctor_list);
        }

    }
}