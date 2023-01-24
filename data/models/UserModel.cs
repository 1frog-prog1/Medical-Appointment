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
    }
}