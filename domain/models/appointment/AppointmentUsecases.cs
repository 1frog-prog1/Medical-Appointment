namespace domain.models.appointment
{
    public class AppointmentUsecases
    {
        private readonly IAppointmentRepository repository;

        public AppointmentUsecases(IAppointmentRepository repository) {
            this.repository = repository;
        }

        public Result<Appointment> saveAppointmentByDoctorId(Appointment appointment) {
            var end_day = new DateTime(1, 1, 1, 18, 0, 0).TimeOfDay;
            if (appointment.start.TimeOfDay > end_day)
                return Result.Fail<Appointment>("Such time doesn't exist");

            if (!repository.isDoctorExist(appointment.doctor_id))
                return Result.Fail<Appointment>("Such doctor doesn't exist");

            if (!repository.isDoctorFreeAtTime(appointment.start, appointment.doctor_id))
                return Result.Fail<Appointment>("The time is already busy");

            repository.saveAppointmentToDoctorId(appointment);
            return Result.Ok<Appointment>(appointment);        
        }


    }

}
