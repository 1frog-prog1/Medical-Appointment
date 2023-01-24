using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

using domain.models.user.usecase;
using med.Views;

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
            if (login == string.Empty)
                return Problem(statusCode: 404, detail: "There is no login");

            var user_res = _usecase.getUserByLogin(login);

            if(user_res.IsFailure) 
                return Problem(statusCode: 404, detail: user_res.Error);

            return Ok(new UserView {
                Id = user_res.Value.Id,
                login = user_res.Value.login
            });
        }

    }
}