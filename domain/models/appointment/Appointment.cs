namespace domain.models.appointment
{
    public class Appointment
    {
        int Id;
        public DateTime start;
        
        public DateTime end;
        public int patient_id;
        public int doctor_id;

        public Appointment(DateTime start, int patient_id, int doctor_id) {
            this.start = start;
            this.patient_id = patient_id;
            this.doctor_id = doctor_id;
            this.end = start.AddMinutes(20);
        }   

        public Appointment(int Id, DateTime start, int patient_id, int doctor_id) {
            this.Id = Id;
            this.start = start;
            this.patient_id = patient_id;
            this.doctor_id = doctor_id;
            this.end = start.AddMinutes(20);
        } 
    }

    
}