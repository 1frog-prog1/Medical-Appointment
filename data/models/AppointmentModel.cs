namespace data.models
{
    public class AppointmentModel
    {
        public int Id;
        public DateTime start;
        
        public DateTime end;
        public int patient_id;
        public int doctor_id;
    }
}