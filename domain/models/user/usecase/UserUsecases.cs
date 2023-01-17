using domain.models.user.model;
using domain.models.user.iuserepository;
using domain.models;


namespace domain.models.user.usecase
{
    public class UserUsecases
    {

        private readonly IUserRepository UserRepository;

        public UserUsecases(IUserRepository userRepository) {
            UserRepository = userRepository;
        }

        public Result<User> signUpUser(User user) {

            if (string.IsNullOrEmpty(user.login) ||
            string.IsNullOrEmpty(user.password) ||
            string.IsNullOrEmpty(user.phone) ||
            string.IsNullOrEmpty(user.fio)) 
                return Result.Fail<User>("There must be no empty fields");

            if (UserRepository.isExist(user.login))
                return Result.Fail<User>("This login already exists");
            
            UserRepository.create(user);
            return Result.Ok<User>(user);
        }

        public Result<User> signInUser(loginData data) {

            if (string.IsNullOrEmpty(data.login) || string.IsNullOrEmpty(data.password))
                return Result.Fail<User>("There must be no empty fields");

            if (!UserRepository.checkAccount(data))
                return Result.Fail<User>("Error. Check your login or password");
            return Result.Ok<User>(UserRepository.findUserByLogin(data.login));
        }

    }
}