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

        public Result<Appointment> saveAppointmentByAnyDoctor(DateTime start, int spec_id, int patient_id) {
            var end_day = new DateTime(1, 1, 1, 18, 0, 0).TimeOfDay;
            if (start.TimeOfDay > end_day)
                return Result.Fail<Appointment>("Such time doesn't exist");

            if (!repository.isAnyDoctorFreeAtTime(spec_id, start))
                return Result.Fail<Appointment>("The time is already busy");

            int doctor_id = repository.saveAppointmentToAnyDoctor(spec_id, start, patient_id);
            var appointment = new Appointment(start, patient_id, doctor_id);
            return Result.Ok<Appointment>(appointment);        
        }


    }

}
