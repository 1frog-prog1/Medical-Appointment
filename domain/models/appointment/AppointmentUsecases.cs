using domain.models.specialisation;
using domain.models.doctor;

namespace domain.models.appointment
{
    public class AppointmentUsecases
    {
        private readonly IAppointmentRepository repository;
        private readonly ISpecialisationRepository spec_repository;
        private readonly IDoctorRepository doc_repository;

        public AppointmentUsecases(IAppointmentRepository repository, ISpecialisationRepository spec_repository,
                                    IDoctorRepository doc_repository) {
            this.repository = repository;
            this.spec_repository = spec_repository;
            this.doc_repository = doc_repository;
        }

        public async Task<Result<Appointment>> saveAppointmentByDoctorId(Appointment appointment) {
            var end_day = new DateTime(1, 1, 1, 18, 0, 0).TimeOfDay;
            if (appointment.start.TimeOfDay > end_day)
                return Result.Fail<Appointment>("Such time doesn't exist");

            if (!(await doc_repository.isExist(appointment.doctor_id)))
                return Result.Fail<Appointment>("Such doctor doesn't exist");

            if (!(await repository.isDoctorFreeAtTime(appointment.start, appointment.doctor_id)))
                return Result.Fail<Appointment>("The time is already busy");

            await repository.saveAppointmentToDoctorId(appointment.Id, appointment.patient_id);
            return Result.Ok<Appointment>(appointment);        
        }

        public async Task<Result<Appointment>> saveAppointmentToAnyDoctor(DateTime start, int spec_id, int patient_id) {
            var end_day = new DateTime(1, 1, 1, 18, 0, 0).TimeOfDay;
            if (start.TimeOfDay > end_day)
                return Result.Fail<Appointment>("Such time doesn't exist");

            if (!(await spec_repository.isExist(spec_id)))
                return Result.Fail<Appointment>("Such specialisation doesn't exist");

            if (!(await repository.isAnyDoctorFreeAtTime(spec_id, start)))
                return Result.Fail<Appointment>("The time is already busy");

            int doctor_id = await repository.saveAppointmentToAnyDoctor(spec_id, start, patient_id);
            var appointment = new Appointment(start, patient_id, doctor_id);
            return Result.Ok<Appointment>(appointment);        
        }

        public async Task<Result<List<Appointment>>> getAllFreeTimeBySpecialisationId(int spec_id) {
            if (!(await spec_repository.isExist(spec_id)))
                return Result.Fail<List<Appointment>>("Such specialisation doesn't exist");

            List<Appointment> free_appointments = await repository.getAllFreeAppointmentsBySpecialistaionId(spec_id);
            return Result.Ok<List<Appointment>>(free_appointments);
        }
    }

}
