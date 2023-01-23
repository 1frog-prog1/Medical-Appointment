using domain.models;
using System.ComponentModel.DataAnnotations;

namespace data.models
{
    public class UserModel
    {
    [Key]
    public int Id {get; set;}
    public string login {get; set;}
    public string password {get; set;}
    public string phone {get; set;}
    public string fio {get; set;}
    public Role role_id {get; set;}

    public UserModel(int Id, string login, string password,
                    string phone, string fio, Role role_id) {
        this.Id = Id;
        this.login = login;
        this.password = password;
        this.phone = phone;
        this.fio = fio;
        this.role_id = role_id;
     }

    }
}