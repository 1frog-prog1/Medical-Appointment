namespace domain.models
{
    public class Appointment
    {
        public DateTime start;
        public int patient_id;
        public int doctor_id;

        public TimeOnly duration;

        public Appointment(DateTime start, DateTime end, int patient_id, int doctor_id) {
            this.start = start;
            this.patient_id = patient_id;
            this.doctor_id = doctor_id;
            this.duration =  new TimeOnly(0, 20);
        }   
    }

    
}