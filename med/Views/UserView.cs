using System.Text.Json.Serialization;
using domain.models;

namespace med.Views
{
    public class UserView
    {
        [JsonPropertyName("id")]
        public int Id {get; set;}
        public string login;
        public string password;
        public string phone;
        public string fio;
        public Role role_id;
    }
}