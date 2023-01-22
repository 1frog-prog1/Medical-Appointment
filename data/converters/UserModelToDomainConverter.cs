using domain;
using data.models;

namespace data.converters
{
    public class UserModelToDomainConverter
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
    }
}