using domain;
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
        return new UserModel (
            user.Id,
            user.login,
            user.password,
            user.phone,
            user.fio,
            user.role_id
        );
    }

    }
}