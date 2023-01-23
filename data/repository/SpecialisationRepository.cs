using domain.models.specialisation;
using data.converters;

namespace data.repository
{
    public class SpecialisationRepository : ISpecialisationRepository
    {
        private readonly ApplicationContext db;

        public SpecialisationRepository(ApplicationContext db) {
            this.db = db;
        }

        public bool isExist(int spec_id) {
            return db.SpecialisationsDb.Any(spec => spec.Id == spec_id);
        }

        public List<Specialisation> getAll() {
            return db.SpecialisationsDb.Select(spec => SpecialisationConverter.toDomain(spec)).ToList();
        }

        public void create(Specialisation spec) {
            db.SpecialisationsDb.Add(SpecialisationConverter.toModel(spec));
            db.SaveChanges();
        }

        public Specialisation update(Specialisation spec) {
            var _spec = db.SpecialisationsDb.FirstOrDefault(sp => sp.Id == spec.Id);
            if (_spec != null) {
                _spec.name = spec.name;
                db.SaveChanges();
            }
            return spec;
        }

        public bool delete(int spec_id) {
            var _spec = db.SpecialisationsDb.FirstOrDefault(sp => sp.Id == spec_id);
            if (_spec != null) {
                db.SpecialisationsDb.Remove(_spec);
                db.SaveChanges();
                return true;
            }
            return false;
        }

    }
}