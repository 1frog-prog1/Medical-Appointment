using domain.models.user;
using data.models;

namespace data.converters
{
    public static class UserConverter
    {
        public static User? toDomain(UserModel model) {
            return new User (
                model.Id,
                model.login,
                model.password,
                model.phone,
                model.fio,
                model.role_id
            );
        }

    public static UserModel toModel(User user) {
        return new UserModel {
            Id = user.Id,
            login = user.login,
            password = user.password,
            phone = user.phone,
            fio = user.fio,
            role_id = user.role_id
        };
    }

    }
}