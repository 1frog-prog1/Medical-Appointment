using domain.models;
using domain.models.user.model;

namespace domain.models.user
{
    public interface IUserRepository : IRepository<User>
    {
         bool isLoginExist(string login);

         bool isUserExist(int user_id);

         bool checkAccount(loginData data);

         User findUserByLogin(string login);

    }
}