using System.Text.Json.Serialization;

namespace med.Views
{
    public class DoctorView
    {
        [JsonPropertyName("id")]
        public int Id {get; set;}
        [JsonPropertyName("fio")]
        public string fio {get; set;}
        [JsonPropertyName("specialisation_id")]
        public int specialisation_id {get; set;}
    }
}