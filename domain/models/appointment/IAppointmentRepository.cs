namespace domain.models.appointment
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
        public bool isDoctorExist(int doctor_id);
        public bool isDoctorFreeAtTime(DateTime start, int doctor_id);

        public bool isAnyDoctorFreeAtTime(int spec_id, DateTime start);
        public void saveAppointmentToDoctorId(Appointment appointment);
         
        public int saveAppointmentToAnyDoctor(int spec_id, DateTime start, int patient_id); // return doctor_id

        // я уже сожалею, что выбрала такие длинные имена
        public void getAllFreeAppointmentsBySpecialistaionId(int spec_id); 
    }
}