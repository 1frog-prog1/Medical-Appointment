using domain.models.user.model;
using data.converters;
using domain;
using domain.models.user;

namespace data.repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext db;

        public UserRepository(ApplicationContext db) {
            this.db = db;
        }

        public void create(User user) {
            var userModel = UserConverter.toModel(user);
            db.UserDb.Add(userModel);
            db.SaveChanges();
        }

        public IEnumerable<User> getAll() { 
            return db.UserDb.Select(model => UserConverter.toDomain(model)).ToList();
         }

        public User update(User user) {
            var _user = db.UserDb.FirstOrDefault(_user => _user.Id == user.Id);
            if (_user != null) {
                _user.fio = user.fio;
                _user.login = user.login;
                _user.password = user.password;
                _user.phone = user.phone;
                _user.role_id = user.role_id;
                db.SaveChanges();
            }
            return user;
        }

        public bool delete(int user_id) {
            var _user = db.UserDb.FirstOrDefault(_user => _user.Id == user_id);
            if (_user != null) {
                db.UserDb.Remove(_user);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public User? findUserByLogin(string login) {
            var user = db.UserDb.FirstOrDefault(u => u.login == login);
            return UserConverter.toDomain(user);
        }

        public bool isLoginExist(string login) {
            return db.UserDb.Any(user => user.login == login);
        }
        
        public bool isUserExist(int user_id) {
            return db.UserDb.Any(user => user.Id == user_id);
        }

        public bool checkAccount(loginData data) {
            return db.UserDb.Any(user => user.login == data.login &&
                                user.password == data.password);
        }


    }
}