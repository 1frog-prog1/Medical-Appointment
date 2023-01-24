using System.Text.Json.Serialization;
using domain.models;

namespace med.Views
{
    public class UserView
    {
        [JsonPropertyName("id")]
        public int Id {get; set;}
        [JsonPropertyName("login")]
        public string login {get; set;}
        [JsonPropertyName("password")]
        public string password {get; set;}
        [JsonPropertyName("phone")]
        public string phone {get;set;}
        [JsonPropertyName("fio")]
        public string fio{get;set;}
        [JsonPropertyName("role_id")]
        public Role role_id {get; set;}
    }
}