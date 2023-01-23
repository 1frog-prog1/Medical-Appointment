using domain.models;
using domain.models.user.model;

namespace domain.models.user
{
    public interface IUserRepository : IRepository<User>
    {
         bool isExist(string login);

         bool checkAccount(loginData data);

         User findUserByLogin(string login);

    }
}