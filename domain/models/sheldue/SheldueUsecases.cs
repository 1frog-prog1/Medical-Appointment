using domain.models.doctor;

namespace domain.models.sheldue
{
    public class SheldueUsecases
    {
        private readonly ISheldueRepository repository;
        private readonly IDoctorRepository doctorRepository;

        public SheldueUsecases(ISheldueRepository repository, 
                                IDoctorRepository doctorRepository) {
            this.repository = repository;
            this.doctorRepository = doctorRepository;
        }

        public Result<Sheldue> getDoctorSheldue(int doctor_id) {
            if (doctor_id <= 0)
                return Result.Fail<Sheldue>("The doctor_id is negative");
            if (!doctorRepository.isExist(doctor_id))
                return Result.Fail<Sheldue>("Such doctor doesn't exist");

            var doctor_sheldue = repository.getDoctorSheldue(doctor_id);
            return Result.Ok<Sheldue>(doctor_sheldue);
        }

        public Result<Appointment> addDoctorAppointment(Appointment appointment) {
            var end_day = new DateTime(1, 1, 1, 18, 0, 0).TimeOfDay;
            if (appointment.doctor_id <= 0)
                return Result.Fail<Appointment>("The doctor_id is negative");

            if (!doctorRepository.isExist(appointment.doctor_id))
                return Result.Fail<Appointment>("Such doctor doesn't exist");
            
            if (appointment.start.TimeOfDay > end_day)
                return Result.Fail<Appointment>("Such time doesn't exist");


            if (repository.isDoctorTimeIsBusy(appointment.doctor_id, appointment.start))
                return Result.Fail<Appointment>("The appointment time is already busy");

            repository.addDoctorAppointment(appointment);
            return Result.Ok<Appointment>(appointment);
        }

        public Result<Appointment> updatePatientInDoctorAppointment(Appointment appointment, int patient_id) {
            if (!repository.isAppointmentExist(appointment))
                return Result.Fail<Appointment>("Such appointment doesn't exist");
            repository.updatePatientInDoctorAppointment(appointment, patient_id);
            appointment.patient_id = patient_id;
            return Result.Ok<Appointment>(appointment);
        }

        public Result<Appointment> deleteDoctorAppointment(Appointment appointment) {
            if (!repository.isAppointmentExist(appointment))
                return Result.Fail<Appointment>("Such appointment doesn't exist");
            repository.deleteDoctorAppointment(appointment);
            return Result.Ok<Appointment>(appointment);
        }
    }
}