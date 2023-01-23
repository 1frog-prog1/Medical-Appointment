using domain.models.doctor;
using domain.models;
using data.converters;

namespace data.repository
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ApplicationContext db;

        public DoctorRepository(ApplicationContext db) {
            this.db = db;
        }
        

        public void create(Doctor doc) {
            db.DoctorDb.Add(DoctorConverter.toModel(doc));
            db.SaveChanges();
        }

        public bool isExist(int doctor_id) {
            return db.DoctorDb.Any(d => d.Id == doctor_id);
        }

        public Doctor? findDoctorByID(int doctor_id) {
            return DoctorConverter.toDomain(db.DoctorDb.FirstOrDefault(d => d.Id == doctor_id));
        }

        public List<Doctor> getAll() {
            return db.DoctorDb.Select(model => DoctorConverter.toDomain(model)).ToList();
        }

        public List<Doctor> findDoctorListBySpecialisation(int spec_id) {
            return db.DoctorDb.Where(d => d.specialisation_id == spec_id).Select(d => DoctorConverter.toDomain(d)).ToList();
        }

        public Doctor update(Doctor doc) {
            var _doctor = db.DoctorDb.FirstOrDefault(d => d.Id == doc.Id);
            if (_doctor != null) {
                _doctor.fio = doc.fio;
                _doctor.specialisation_id = doc.specialisation_id;
                db.SaveChanges();
            }
            return doc;
        }

        public bool delete (int doctor_id) {
            var _doctor = db.DoctorDb.FirstOrDefault(d => d.Id == doctor_id);
            if (_doctor != null) {
                db.DoctorDb.Remove(_doctor);
                db.SaveChanges();
                return true;
            }
            return false;
        }

    }
}