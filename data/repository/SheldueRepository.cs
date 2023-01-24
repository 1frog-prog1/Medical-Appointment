using Microsoft.EntityFrameworkCore;

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

        public async void create(Sheldue sheldue) {
            await db.SheldueDb.AddAsync(SheldueConverter.toModel(sheldue));
            await db.SaveChangesAsync();
        }

        public async Task<bool> delete(int shel_id) {
            SheldueModel sheldue = await db.SheldueDb.FirstOrDefaultAsync(shel => shel.Id == shel_id);
            if (sheldue != null) {
                db.SheldueDb.Remove(sheldue);
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Sheldue> getSheldueById(int sheldue_id) {
            var sheldue = await db.SheldueDb.FirstOrDefaultAsync(shel => shel.Id == sheldue_id);
            return SheldueConverter.toDomain(sheldue);
        }


        public async Task<Sheldue> getDoctorSheldue(int doctor_id) {
            var doc_sheldue = await db.SheldueDb.FirstOrDefaultAsync(shel => shel.doctor_id == doctor_id);
            return SheldueConverter.toDomain(doc_sheldue);
        }

        public async Task<List<Sheldue>> getAll() {
            return await db.SheldueDb.Select(shel => SheldueConverter.toDomain(shel)).ToListAsync();
        }

        public async Task<Sheldue> update(Sheldue shel) {
            SheldueModel _shel = await db.SheldueDb.FirstOrDefaultAsync(_shel => _shel.Id == shel.Id);
            if (_shel != null) {
            _shel.doctor_id = shel.doctor_id;
            _shel.day_start = shel.day_start;
            _shel.day_end = shel.day_end;
            await db.SaveChangesAsync();
            }
            return shel;
        }

    }
}