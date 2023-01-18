namespace domain.models.appointment
{
    public class AppointmentUsecases
    {
        private readonly IAppointmentRepository repository;

        AppointmentUsecases(IAppointmentRepository repository) {
            this.repository = repository;
        }

        public Result<Appointment> saveAppointmentByDoctorId(Appointment appointment) {
            if (!repository.isTimeExist(appointment.start))
                return Result.Fail<Appointment>("Such time doesn't exist");

            if (!repository.isDoctorFreeAtTime(appointment.start, appointment.doctor_id))
                return Result.Fail<Appointment>("The time is already busy");

            repository.saveAppointmentToDoctorId(appointment);
            return Result.Ok<Appointment>(appointment);        
        }

        
    }

}
