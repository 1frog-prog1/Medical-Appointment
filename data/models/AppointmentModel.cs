using System.ComponentModel.DataAnnotations;

namespace data.models
{
    public class AppointmentModel
    {
        [Key]
        public int Id {get; set;}
        public DateTime start {get; set;}
        
        public DateTime end {get; set;}
        public int patient_id {get; set;}
        public int doctor_id {get; set;}
    }
}