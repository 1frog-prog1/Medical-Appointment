using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

using med.Views;
using domain.models.sheldue;
using domain.models;

namespace med.Controllers
{

    [ApiController]
    [Route("sheldue")]
    public class SheldueController : ControllerBase
    {
        private readonly SheldueUsecases _usecase;

        public SheldueController(SheldueUsecases usecase) {
            _usecase = usecase;
        }

        [HttpGet("getDoctorSheldue")]
        public async Task<ActionResult<SheldueView>> getDoctorSheldue(int doctor_id) {

            var res = await _usecase.getDoctorSheldue(doctor_id);

            if (res.IsFailure)
                return Problem(statusCode: 400, detail: res.Error);

            return Ok(new SheldueView {
                Id = res.Value.Id,
                doctor_id = res.Value.doctor_id,
                day_start = res.Value.day_start,
                day_end = res.Value.day_end
            });

        }
    }
}