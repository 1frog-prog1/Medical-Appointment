using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

using med.Views;
using domain.models.doctor;
using domain.models;

namespace med.Controllers
{
    [ApiController]
    [Route("doctor")]
    public class DoctorController : ControllerBase
    {
        private readonly DoctorUsecases _usecase;

        public DoctorController(DoctorUsecases usecase) {
            _usecase = usecase;
        }

        [HttpGet("createDoctor")]
        public ActionResult<DoctorView> createDoctor([FromQuery]DoctorView doc_v) {

            var doc = new Doctor(
                doc_v.fio,
                doc_v.specialisation_id
            );

            var res = _usecase.createDoctor(doc);

            if (res.IsFailure)
                return Problem(statusCode: 400, detail: res.Error);

            return Ok(new DoctorView {
                fio = doc_v.fio,
                specialisation_id = doc_v.specialisation_id
            });
        }

        [HttpGet("deleteDoctor")]
        public ActionResult<bool> deleteDoctor(int doctor_id) {

            var res = _usecase.deleteDoctor(doctor_id);

            if (res.IsFailure)
                return Problem(statusCode: 400, detail: res.Error);

            return Ok(res);
        }

        [HttpGet("getAllDoctors")]
        public ActionResult<List<DoctorView>> getAllDoctors() {

            var res = _usecase.getAllDoctors();

            if (res.IsFailure)
                return Problem(statusCode: 400, detail: res.Error);

            return Ok(res.Value.Select(doc => new DoctorView {
                Id = doc.Id, 
                fio = doc.fio,
                specialisation_id = doc.specialisation_id
            }));
        }

        [HttpGet("getDoctorById")]
        public ActionResult<DoctorView> getDoctorById(int doctor_id) {

            var res = _usecase.getDoctorById(doctor_id);

            if (res.IsFailure)
                return Problem(statusCode: 400, detail: res.Error);

            return Ok(new DoctorView {
                Id = res.Value.Id,
                fio = res.Value.fio,
                specialisation_id = res.Value.specialisation_id
            });
        }

        [HttpGet("getDoctorsBySpecialisationId")]
        public ActionResult<List<DoctorView>> getDoctorsBySpecialisationId(int spec_id) {

            var res = _usecase.getDoctorsBySpecialisation(spec_id);

            if (res.IsFailure)
                return Problem(statusCode: 400, detail: res.Error);

            return Ok(res.Value.Select(doc => new DoctorView {
                Id = doc.Id, 
                fio = doc.fio,
                specialisation_id = doc.specialisation_id
            }));
        }

    }
}