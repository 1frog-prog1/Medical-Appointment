using domain.models.doctor;

namespace domain.models.sheldue
{
    public class SheldueUsecases
    {
        private readonly ISheldueRepository repository;
        private readonly IDoctorRepository doc_repository;

        public SheldueUsecases(ISheldueRepository repository, 
                                IDoctorRepository doctorRepository) {
            this.repository = repository;
            this.doc_repository = doctorRepository;
        }

        public Result<Sheldue> getDoctorSheldue(int doctor_id) {
            if (doctor_id <= 0)
                return Result.Fail<Sheldue>("The doctor_id is negative");
            if (!doc_repository.isExist(doctor_id))
                return Result.Fail<Sheldue>("Such doctor doesn't exist");

            var doctor_sheldue = repository.getDoctorSheldue(doctor_id);
            return Result.Ok<Sheldue>(doctor_sheldue);
        }
    }
}