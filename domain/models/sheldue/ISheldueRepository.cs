namespace domain.models.sheldue
{
    public interface ISheldueRepository : IRepository<Sheldue>
    {
        // я бы возвращала список, когда у него прием
        // а то у него мб запись в начале дня и в конце
        // а все между - свободно. не лучше ли дать врачу об этом знать
        // чтоб он мог спокойно уйти.. куда там обычно врачи уходят подолгу

        public bool isAppointmentExist (Appointment appointment);

         public Sheldue getDoctorSheldue(int doctor_id); 

        //для админов как я понимаю
         public void addDoctorAppointment(Appointment appointment);

         public void updatePatientInDoctorAppointment(Appointment appointment, int patient_id);

         public void deleteDoctorAppointment(Appointment appointment);
    }
}