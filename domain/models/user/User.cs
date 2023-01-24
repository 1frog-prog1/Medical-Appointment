using domain.models;

namespace domain.models.user;
public class User
{
    public int Id;
    public string login;
    public string password;
    public string phone;
    public string fio;
    public Role role_id;

    public User(int Id, string login, string password, string phone, 
                string fio, Role role_id) {
        this.Id = Id;
        this.login = login;
        this.password = password;
        this.phone = phone;
        this.fio = fio;
        this.role_id = role_id;
    }

    public User(string login, string password, string phone, 
                string fio, Role role_id) {
        this.Id = Id;
        this.login = login;
        this.password = password;
        this.phone = phone;
        this.fio = fio;
        this.role_id = role_id;
    }
}
