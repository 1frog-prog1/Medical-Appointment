using System.Text.Json.Serialization;

namespace med.Views
{
    public class AppointmentView
    {
        [JsonPropertyName("id")]
        public int Id {get; set;}
        [JsonPropertyName("start")]
        public DateTime start {get; set;}
        [JsonPropertyName("end")]
        public DateTime end {get; set;}
        [JsonPropertyName("patient_id")]
        public int patient_id {get; set;}
        [JsonPropertyName("doctor_id")]
        public int doctor_id {get; set;}
    }
}