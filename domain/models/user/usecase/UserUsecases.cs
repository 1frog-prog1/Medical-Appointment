using domain.models.user.model;
using domain.models.user;
using domain.models;


namespace domain.models.user.usecase
{
    public class UserUsecases
    {

        private readonly IUserRepository repository;

        public UserUsecases(IUserRepository userRepository) {
            repository = userRepository;
        }

        public async Task<Result<User>> signUpUser(User user) {

            if (string.IsNullOrEmpty(user.login) ||
            string.IsNullOrEmpty(user.password) ||
            string.IsNullOrEmpty(user.phone) ||
            string.IsNullOrEmpty(user.fio)) 
                return Result.Fail<User>("There must be no empty fields");

            if (await repository.isLoginExist(user.login))
                return Result.Fail<User>("This login already exists");
            
            repository.create(user);
            return Result.Ok<User>(user);
        }

        public async Task<Result<User>> signInUser(loginData data) {

            if (string.IsNullOrEmpty(data.login) || string.IsNullOrEmpty(data.password))
                return Result.Fail<User>("There must be no empty fields");

            if (!(await repository.checkAccount(data)))
                return Result.Fail<User>("Error. Check your login or password");
            return Result.Ok<User>(await repository.findUserByLogin(data.login));
        }

        public async Task<Result<User>> getUserByLogin(string login) {
            if (string.IsNullOrEmpty(login))
                return Result.Fail<User>("There must be no empty fields");

            if (!(await repository.isLoginExist(login)))
                return Result.Fail<User>("This login doesn't exist");

            var user = await repository.findUserByLogin(login);
            return Result.Ok<User>(user);
        }

    }
}