using domain.models;
using domain.models.user.model;

namespace domain.models.user
{
    public interface IUserRepository : IRepository<User>
    {
         Task<bool> isLoginExist(string login);

         Task<bool> isUserExist(int user_id);

         Task<bool> checkAccount(loginData data);

         Task<User> findUserByLogin(string login);

    }
}