namespace domain.models.user.iuserepository
{

    using domain.models;
    using domain.models.user.model;
    public interface IUserRepository : IRepository<User>
    {
         bool isExist(string login);

         bool checkAccount(loginData data);

         User findUserByLogin(string login);

    }
}