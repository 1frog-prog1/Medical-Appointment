namespace domain.models.appointment
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {

        bool isTimeExist(DateTime start);
        bool isDoctorFreeAtTime(DateTime start, int doctor_id);
         void saveAppointmentToDoctorId(Appointment appointment);
         
         void saveAppointmentToAnyDoctor(int patient_id);

        // я уже сожалею, что выбрала такие длинные имена
         void getAllFreeAppointmentsBySpecialistaionId(int spec_id); 
    }
}