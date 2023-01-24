using domain.models.sheldue;
using domain.models;
using data.converters;
using data.models;

namespace data.repository
{
    public class SheldueRepository : ISheldueRepository
    {
        private readonly ApplicationContext db;

        public SheldueRepository(ApplicationContext db) {
            this.db = db;
        }

        public void create(Sheldue sheldue) {
            db.SheldueDb.Add(SheldueConverter.toModel(sheldue));
            db.SaveChanges();
        }

        public bool delete(int shel_id) {
            SheldueModel sheldue = db.SheldueDb.FirstOrDefault(shel => shel.Id == shel_id);
            if (sheldue != null) {
                db.SheldueDb.Remove(sheldue);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public Sheldue getSheldueById(int sheldue_id) {
            var sheldue = db.SheldueDb.FirstOrDefault(shel => shel.Id == sheldue_id);
            return SheldueConverter.toDomain(sheldue);
        }


        public Sheldue getDoctorSheldue(int doctor_id) {
            var doc_sheldue = db.SheldueDb.FirstOrDefault(shel => shel.doctor_id == doctor_id);
            return SheldueConverter.toDomain(doc_sheldue);
        }

        public List<Sheldue> getAll() {
            return db.SheldueDb.Select(shel => SheldueConverter.toDomain(shel)).ToList();
        }

        public Sheldue update(Sheldue shel) {
            SheldueModel _shel = db.SheldueDb.FirstOrDefault(_shel => _shel.Id == shel.Id);
            if (_shel != null) {
            _shel.doctor_id = shel.doctor_id;
            _shel.day_start = shel.day_start;
            _shel.day_end = shel.day_end;
            db.SaveChanges();
            }
            return shel;
        }

    }
}