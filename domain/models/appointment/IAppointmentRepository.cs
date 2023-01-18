namespace domain.models.appointment
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
        public bool isDoctorExist(int doctor_id);
        public bool isDoctorFreeAtTime(DateTime start, int doctor_id);
        public void saveAppointmentToDoctorId(Appointment appointment);
         
        public void saveAppointmentToAnyDoctor(int patient_id);

        // я уже сожалею, что выбрала такие длинные имена
        public void getAllFreeAppointmentsBySpecialistaionId(int spec_id); 
    }
}