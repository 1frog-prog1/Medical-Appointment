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

        public async Task<Result<Doctor>> createDoctor(Doctor doctor) {
            if (string.IsNullOrEmpty(doctor.fio))
                return Result.Fail<Doctor>("Empty field of FIO");
            if (!(await spec_repository.isExist(doctor.specialisation_id)))
                return Result.Fail<Doctor>("Such specialisation doesn't exist");
            repository.create(doctor);
            return Result.Ok<Doctor>(doctor);
        }

        public async Task<Result<bool>> deleteDoctor(int doctor_id) {
            if (doctor_id <= 0)
                return Result.Fail<bool>("Incorrect ID");

            bool res = await repository.delete(doctor_id);
            return Result.Ok<bool>(res);
        }

        public async Task<Result<List<Doctor>>> getAllDoctors() {
            var doctor_list = await repository.getAll();
            return Result.Ok<List<Doctor>>(doctor_list); // no danger: list can be empty?
        }

        public async Task<Result<Doctor>> getDoctorById(int doctor_id) {
            if (doctor_id <= 0)
                return Result.Fail<Doctor>("Incorrect ID");

            if (!(await repository.isExist(doctor_id)))
                return Result.Fail<Doctor>("Such doctor doesn't exist");

            var doctor = await repository.findDoctorByID(doctor_id);
            return Result.Ok<Doctor>(doctor);
        }

        public async Task<Result<List<Doctor>>> getDoctorsBySpecialisation(int spec_id) {
            if (spec_id <= 0 || !(await spec_repository.isExist(spec_id)))
                return Result.Fail<List<Doctor>>("Incorrect specialisation ID");
            var doctor_list = await repository.findDoctorListBySpecialisation(spec_id);
            return Result.Ok<List<Doctor>>(doctor_list);
        }

    }
}