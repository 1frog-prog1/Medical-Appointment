namespace domain.models.appointment
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
        public Task<bool> isExist (int appointment_id);

        public Task<bool> updatePatientInDoctorAppointment(int appointment_id, int patient_id);

       // public void deleteDoctorAppointment(Appointment appointment);

        public Task<bool> isDoctorFreeAtTime(DateTime start, int doctor_id);

        public Task<bool> isAnyDoctorFreeAtTime(int spec_id, DateTime start);
        public Task<bool> saveAppointmentToDoctorId(int appointment_id, int patient_id);
         
        public Task<int> saveAppointmentToAnyDoctor(int spec_id, DateTime start, int patient_id); // return doctor_id

        // я уже сожалею, что выбрала такие длинные имена
        public Task<List<Appointment>> getAllFreeAppointmentsBySpecialistaionId(int spec_id); 

        public Task<Appointment> getByInfo(Appointment app);

    }
}