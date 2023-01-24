using domain.models.user.model;
using data.converters;
using domain;
using domain.models.user;
using Microsoft.EntityFrameworkCore;

namespace data.repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext db;

        public UserRepository(ApplicationContext db) {
            this.db = db;
        }

        public async void create(User user) {
            var userModel = UserConverter.toModel(user);
            await db.UserDb.AddAsync(userModel);
            await db.SaveChangesAsync();
        }

        public async Task<List<User>> getAll() { 
            return await db.UserDb.Select(model => UserConverter.toDomain(model)).ToListAsync();
         }

        public async Task<User> update(User user) {
            var _user = await db.UserDb.FirstOrDefaultAsync(_user => _user.Id == user.Id);
            if (_user != null) {
                _user.fio = user.fio;
                _user.login = user.login;
                _user.password = user.password;
                _user.phone = user.phone;
                _user.role_id = user.role_id;
                await db.SaveChangesAsync();
            }
            return user;
        }

        public async Task<bool> delete(int user_id) {
            var _user = await db.UserDb.FirstOrDefaultAsync(_user => _user.Id == user_id);
            if (_user != null) {
                db.UserDb.Remove(_user);
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<User?> findUserByLogin(string login) {
            var user = await db.UserDb.FirstOrDefaultAsync(u => u.login == login);
            return UserConverter.toDomain(user);
        }

        public async Task<bool> isLoginExist(string login) {
            return await db.UserDb.AnyAsync(user => user.login == login);
        }
        
        public async Task<bool> isUserExist(int user_id) {
            return await db.UserDb.AnyAsync(user => user.Id == user_id);
        }

        public async Task<bool> checkAccount(loginData data) {
            return await db.UserDb.AnyAsync(user => user.login == data.login &&
                                user.password == data.password);
        }


    }
}