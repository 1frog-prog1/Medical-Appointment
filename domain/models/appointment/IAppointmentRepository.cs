namespace domain.models.appointment
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
        public bool isExist (int appointment_id);

        public bool updatePatientInDoctorAppointment(int appointment_id, int patient_id);

       // public void deleteDoctorAppointment(Appointment appointment);

        public bool isDoctorFreeAtTime(DateTime start, int doctor_id);

        public bool isAnyDoctorFreeAtTime(int spec_id, DateTime start);
        public bool saveAppointmentToDoctorId(int appointment_id, int patient_id);
         
        public int saveAppointmentToAnyDoctor(int spec_id, DateTime start, int patient_id); // return doctor_id

        // я уже сожалею, что выбрала такие длинные имена
        public List<Appointment> getAllFreeAppointmentsBySpecialistaionId(int spec_id); 

        public Appointment getByInfo(Appointment app);

    }
}