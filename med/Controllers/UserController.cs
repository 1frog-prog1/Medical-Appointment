using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

using domain.models.user.usecase;
using med.Views;
using domain.models.user;
using domain.models.user.model;

namespace med.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly UserUsecases _usecase;

        public UserController(UserUsecases usecases) {
            _usecase = usecases;
        }

        [HttpGet("login")]
        public ActionResult<UserView> getUserByLogin(string login) {

            var user_res = _usecase.getUserByLogin(login);

            if(user_res.IsFailure) 
                return Problem(statusCode: 400, detail: user_res.Error);

            return Ok(new UserView {
                Id = user_res.Value.Id,
                login = user_res.Value.login,
                fio = user_res.Value.fio,
                phone = user_res.Value.phone,
                role_id = user_res.Value.role_id
            });
        }

        [HttpGet("signUp")]
        public ActionResult<UserView> signUpUser([FromQuery]UserView user_v) {
            
            var user = new User(
                user_v.login,
                user_v.password, 
                user_v.phone, 
                user_v.fio, 
                user_v.role_id
            );

            var createRes = _usecase.signUpUser(user);

            if (createRes.IsFailure)
                return Problem(statusCode: 400, detail: createRes.Error);

            return Ok(user_v);
        }

        [HttpGet("signIn")]
        public ActionResult<UserView> signInUser(string login, string password) {

            var user_res = _usecase.signInUser(new loginData(login, password));

            if (user_res.IsFailure)
                return Problem(statusCode: 400, detail: user_res.Error);

            return Ok(new UserView {
                Id = user_res.Value.Id,
                login = user_res.Value.login,
                fio = user_res.Value.fio,
                phone = user_res.Value.phone,
                role_id = user_res.Value.role_id
            });
        }

    }
}