using Xunit;

using domain.models;
using domain.models.user;
using data;
using data.repository;
using domain.models.user.model;

namespace tests
{
    public class UserRepositoryTests
    {
        private readonly ApplicationContextFactory dbFatory;
        private readonly ApplicationContext db;
        private readonly UserRepository user_rep;

        public UserRepositoryTests() {
            dbFatory = new ApplicationContextFactory();
            db = dbFatory.CreateDbContext();
            user_rep = new UserRepository(db);
        }

        [Fact]
        public void UserRepositoryCreateDelete() {
            User user = new User("crocodile", "111", "(569) 284-3176", "Ava Vandervort", Role.Patient);
            if (user_rep.isLoginExist(user.login)) {
                int user_id = user_rep.findUserByLogin("crocodile").Id;
                user_rep.delete(user_id);
            }
            user_rep.create(user);
            Assert.True(user_rep.isLoginExist(user.login));
            user = user_rep.findUserByLogin("crocodile");
            var res = user_rep.delete(user.Id);
            Assert.False(user_rep.isLoginExist(user.login));
        }

        [Fact]
        public void UserRepositoryUpdate() {
            User user = new User("crocodile", "111", "(569) 284-3176", "Ava Vandervort", Role.Patient);
            if (user_rep.isLoginExist(user.login)) {
                int user_id = user_rep.findUserByLogin("crocodile").Id;
                user_rep.delete(user_id);
            }
            user_rep.create(user);

            user = user_rep.findUserByLogin("crocodile");
            user.phone = "1-913-580-8642";
            user_rep.update(user);
            user = user_rep.findUserByLogin("crocodile");
            Assert.True(user != null && user.phone == "1-913-580-8642");
            user_rep.delete(user.Id);

        }

        [Fact]
        public void UserRepositoryCheckAcconunt() {
            User user = new User("crocodile", "111", "(569) 284-3176", "Ava Vandervort", Role.Patient);
            if (user_rep.isLoginExist(user.login)) {
                int user_id = user_rep.findUserByLogin("crocodile").Id;
                user_rep.delete(user_id);
            }
            user_rep.create(user);

            Assert.False(user_rep.checkAccount(new loginData("crocodile", "1")));
            Assert.True(user_rep.checkAccount(new loginData("crocodile", "111")));

            user_rep.delete(user_rep.findUserByLogin("crocodile").Id);
        }

    }
}