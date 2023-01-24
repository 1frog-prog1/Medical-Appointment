using Microsoft.EntityFrameworkCore;

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


        public async void create(Appointment app) {
            await db.AppointmentDb.AddAsync(AppoinmentConverter.toModel(app));
            await db.SaveChangesAsync();
        }

        public async Task<bool> isExist(int app_id) {
            return await db.AppointmentDb.AnyAsync(app => app.Id == app_id);
        }

        public async Task<bool> delete(int app_id) {
            var app = await db.AppointmentDb.FirstOrDefaultAsync(app => app.Id == app_id);
            if (app != null) {
                db.AppointmentDb.Remove(app);
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> isDoctorFreeAtTime(DateTime start, int doctor_id) {
            var patient_id = (await db.AppointmentDb.FirstAsync(app => app.start == start && app.doctor_id == doctor_id)).patient_id;
            return patient_id == null;
        }

        public async Task<bool> updatePatientInDoctorAppointment(int app_id, int patient_id) {
            var app = await db.AppointmentDb.FirstOrDefaultAsync(app => app.Id == app_id && app.patient_id == patient_id);
            if (app != null) {
                app.patient_id = patient_id;
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> isAnyDoctorFreeAtTime(int spec_id, DateTime start) {
            var doc_spec = db.DoctorDb.Where(doc => doc.specialisation_id == spec_id);
            var app_spec = db.AppointmentDb.Where(app => doc_spec.Any(doc => app.doctor_id == doc.Id)
                                                 && app.start == start && app.patient_id == null);
            return app_spec.Count() != 0;
        }

        public async Task<List<Appointment>> getAllFreeAppointmentsBySpecialistaionId(int spec_id) {
            var doc_spec = db.DoctorDb.Where(doc => doc.specialisation_id == spec_id);
            var app_spec = db.AppointmentDb.Where(app => doc_spec.Any(doc => app.doctor_id == doc.Id)
                                                 && app.patient_id == null);
            return app_spec.Select(app => AppoinmentConverter.toDomain(app)).ToList();
        }
        
        public async Task<bool> saveAppointmentToDoctorId(int app_id, int patient_id) {
            var _app = await db.AppointmentDb.FirstAsync(ap => ap.Id == app_id);
            if (_app != null) {
                _app.patient_id = patient_id;
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        } 

        public async Task<int> saveAppointmentToAnyDoctor(int spec_id, DateTime start, int patient_id) {
            var free_app = await getAllFreeAppointmentsBySpecialistaionId(spec_id);
            var apps = free_app.Where(app => app.start == start).ToList();
            if (apps != null) {
                apps[0].patient_id = patient_id;
                await db.SaveChangesAsync();
                return apps[0].doctor_id;
            }
            return 0;
        }

        public async Task<List<Appointment>> getAll() {
            return await db.AppointmentDb.Select(app => AppoinmentConverter.toDomain(app)).ToListAsync();
        }


        public async Task<Appointment> update(Appointment app) {
            await updatePatientInDoctorAppointment(app.Id, app.patient_id);
            return app;
        }

        public async Task<Appointment?> getByInfo(Appointment app) {
            return AppoinmentConverter.toDomain(
                 await db.AppointmentDb.FirstOrDefaultAsync(
                _app => _app.doctor_id == app.doctor_id &&
                _app.start == app.start &&
                _app.patient_id == app.patient_id
            ));
        }

    }

}
