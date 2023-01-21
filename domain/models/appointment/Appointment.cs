namespace domain.models
{
    public class Appointment
    {
        public DateTime start;
        
        public DateTime end;
        public int patient_id;
        public int doctor_id;

        public int duration_min;

        public Appointment(DateTime start, int patient_id, int doctor_id) {
            this.start = start;
            this.patient_id = patient_id;
            this.doctor_id = doctor_id;
            this.duration_min =  20;
            this.end = start.AddMinutes(duration_min);
        }  
        public Appointment(DateTime start, DateTime end, int patient_id, int doctor_id) {
            this.start = start;
            this.end = end;
            this.patient_id = patient_id;
            this.doctor_id = doctor_id;
            this.duration_min =  20;
        }   
    }

    
}