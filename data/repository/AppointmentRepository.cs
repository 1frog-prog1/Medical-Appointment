using domain.models.appointment;
using data.converters;

namespace data.repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationContext db;

        public AppointmentRepository(ApplicationContext db) {
            this.db = db;
        }


        public void create(Appointment app) {
            db.AppointmentDb.Add(AppoinmentConverter.toModel(app));
            db.SaveChanges();
        }

        public bool isExist(int app_id) {
            return db.AppointmentDb.Any(app => app.Id == app_id);
        }

        public bool delete(int app_id) {
            var app = db.AppointmentDb.FirstOrDefault(app => app.Id == app_id);
            if (app != null) {
                db.AppointmentDb.Remove(app);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool isDoctorFreeAtTime(DateTime start, int doctor_id) {
            var patient_id = db.AppointmentDb.First(app => app.start == start && app.doctor_id == doctor_id).patient_id;
            return patient_id == null;
        }

        public bool updatePatientInDoctorAppointment(int app_id, int patient_id) {
            var app = db.AppointmentDb.FirstOrDefault(app => app.Id == app_id && app.patient_id == patient_id);
            if (app != null) {
                app.patient_id = patient_id;
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool isAnyDoctorFreeAtTime(int spec_id, DateTime start) {
            var doc_spec = db.DoctorDb.Where(doc => doc.specialisation_id == spec_id);
            var app_spec = db.AppointmentDb.Where(app => doc_spec.Any(doc => app.doctor_id == doc.Id)
                                                 && app.start == start && app.patient_id == null);
            return app_spec.Count() != 0;
        }

        public List<Appointment> getAllFreeAppointmentsBySpecialistaionId(int spec_id) {
            var doc_spec = db.DoctorDb.Where(doc => doc.specialisation_id == spec_id);
            var app_spec = db.AppointmentDb.Where(app => doc_spec.Any(doc => app.doctor_id == doc.Id)
                                                 && app.patient_id == null);
            return app_spec.Select(app => AppoinmentConverter.toDomain(app)).ToList();
        }
        
        public bool saveAppointmentToDoctorId(int app_id, int patient_id) {
            var _app = db.AppointmentDb.First(ap => ap.Id == app_id);
            if (_app != null) {
                _app.patient_id = patient_id;
                db.SaveChanges();
                return true;
            }
            return false;
        } 

        public int saveAppointmentToAnyDoctor(int spec_id, DateTime start, int patient_id) {
            var free_app = getAllFreeAppointmentsBySpecialistaionId(spec_id);
            var apps = free_app.Where(app => app.start == start).ToList();
            if (apps != null) {
                apps[0].patient_id = patient_id;
                db.SaveChanges();
                return apps[0].doctor_id;
            }
            return 0;
        }

        public List<Appointment> getAll() {
            return db.AppointmentDb.Select(app => AppoinmentConverter.toDomain(app)).ToList();
        }


        public Appointment update(Appointment app) {
            updatePatientInDoctorAppointment(app.Id, app.patient_id);
            return app;
        }

        public Appointment? getByInfo(Appointment app) {
            return AppoinmentConverter.toDomain(
                 db.AppointmentDb.FirstOrDefault(
                _app => _app.doctor_id == app.doctor_id &&
                _app.start == app.start &&
                _app.patient_id == app.patient_id
            ));
        }

    }

}
