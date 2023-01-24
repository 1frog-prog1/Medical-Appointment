using Microsoft.EntityFrameworkCore;

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
        

        public async void create(Doctor doc) {
            await db.DoctorDb.AddAsync(DoctorConverter.toModel(doc));
            await db.SaveChangesAsync();
        }

        public async Task<bool> isExist(int doctor_id) {
            return await db.DoctorDb.AnyAsync(d => d.Id == doctor_id);
        }

        public async Task<Doctor?> findDoctorByID(int doctor_id) {
            return DoctorConverter.toDomain(await db.DoctorDb.FirstOrDefaultAsync(d => d.Id == doctor_id));
        }

        public Task<List<Doctor>> getAll() {
            return db.DoctorDb.Select(model => DoctorConverter.toDomain(model)).ToListAsync();
        }

        public Task<List<Doctor>> findDoctorListBySpecialisation(int spec_id) {
            return db.DoctorDb.Where(d => d.specialisation_id == spec_id).Select(d => DoctorConverter.toDomain(d)).ToListAsync();
        }

        public async Task<Doctor> update(Doctor doc) {
            var _doctor = await db.DoctorDb.FirstOrDefaultAsync(d => d.Id == doc.Id);
            if (_doctor != null) {
                _doctor.fio = doc.fio;
                _doctor.specialisation_id = doc.specialisation_id;
                await db.SaveChangesAsync();
            }
            return doc;
        }

        public async Task<bool> delete (int doctor_id) {
            var _doctor = await db.DoctorDb.FirstOrDefaultAsync(d => d.Id == doctor_id);
            if (_doctor != null) {
                db.DoctorDb.Remove(_doctor);
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }

    }
}