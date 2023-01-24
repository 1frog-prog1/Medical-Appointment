using System.Text.Json.Serialization;

namespace med.Views
{
    public class SheldueView
    {
        [JsonPropertyName("id")]
        public int Id {get; set;}
        [JsonPropertyName("doctor_id")]
        public int doctor_id {get; set;}
        [JsonPropertyName("day_start")]
        public DateTime day_start {get; set;}
        [JsonPropertyName("day_end")]
        public DateTime day_end {get; set;}
    }
}