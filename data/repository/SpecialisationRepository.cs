using Microsoft.EntityFrameworkCore;

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

        public async Task<bool> isExist(int spec_id) {
            return await db.SpecialisationsDb.AnyAsync(spec => spec.Id == spec_id);
        }

        public async Task<List<Specialisation>> getAll() {
            return await db.SpecialisationsDb.Select(spec => SpecialisationConverter.toDomain(spec)).ToListAsync();
        }

        public async void create(Specialisation spec) {
            await db.SpecialisationsDb.AddAsync(SpecialisationConverter.toModel(spec));
            await db.SaveChangesAsync();
        }

        public async Task<Specialisation> update(Specialisation spec) {
            var _spec = await db.SpecialisationsDb.FirstOrDefaultAsync(sp => sp.Id == spec.Id);
            if (_spec != null) {
                _spec.name = spec.name;
                await db.SaveChangesAsync();
            }
            return spec;
        }

        public async Task<bool> delete(int spec_id) {
            var _spec = await db.SpecialisationsDb.FirstOrDefaultAsync(sp => sp.Id == spec_id);
            if (_spec != null) {
                db.SpecialisationsDb.Remove(_spec);
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }

    }
}