using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

using med.Views;
using domain.models.appointment;

namespace med.Controllers
{
    [ApiController]
    [Route("appointment")]
    public class AppointmentController : ControllerBase
    {
        private readonly AppointmentUsecases _usecase;

        public AppointmentController(AppointmentUsecases usecase) {
            _usecase = usecase;
        }

        [HttpGet("saveAppointmentByDoctorId")]
        public ActionResult<bool> saveAppointmentByDoctorId([FromQuery]AppointmentView app) {
            
            var _app = new Appointment(app.start, app.patient_id, app.doctor_id);

            var res = _usecase.saveAppointmentByDoctorId(_app);

            if (res.IsFailure)
                return Problem(statusCode: 400, detail: res.Error);

            return Ok(res.Value);
        }

        [HttpGet("saveAppointmentToAnyDoctor")]
        public ActionResult<AppointmentView> saveAppointmentToAnyDoctor(DateTime start, int spec_id, int patient_id) {
            
            var res = _usecase.saveAppointmentToAnyDoctor(start, spec_id, patient_id);

            if (res.IsFailure)
                return Problem(statusCode: 400, detail: res.Error);

            return Ok( new AppointmentView {
                start = res.Value.start,
                end = res.Value.end,
                doctor_id = res.Value.doctor_id,
                patient_id = res.Value.patient_id
            });
        }

        [HttpGet("getAllFreeTimeBySpecialisationId")]
        public ActionResult<List<AppointmentView>> getAllFreeTimeBySpecialisationId(int spec_id) {
            
            var res = _usecase.getAllFreeTimeBySpecialisationId(spec_id);

            if (res.IsFailure)
                return Problem(statusCode: 400, detail: res.Error);

            return Ok(res.Value.Select(app => new AppointmentView {
                Id = app.Id,
                start = app.start,
                end = app.end,
                doctor_id = app.doctor_id
            }));

        }
    }
}