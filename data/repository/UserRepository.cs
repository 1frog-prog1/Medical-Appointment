using domain.models;
using data.converters;
using domain;

namespace data.repository
{
    public class UserRepository
    {
        private readonly ApplicationContext context;

        public UserRepository(ApplicationContext context) {
            this.context = context;
        }

        public User? getByLogin(string login) {
            var user = context.Users.FirstOrDefault(u => u.login == login);
            return UserModelToDomainConverter.toDomain(user);
        }

    }
}