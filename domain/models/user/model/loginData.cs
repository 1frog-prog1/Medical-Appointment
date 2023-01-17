namespace domain.models.user.model
{
    public class loginData
    {
        public string login;
        public string password;

        public loginData(string login, string password) {
            this.login = login;
            this.password = password;
        }
    }
}