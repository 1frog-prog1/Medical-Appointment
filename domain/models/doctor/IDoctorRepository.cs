namespace domain.models.doctor
{
    public interface IDoctorRepository : IRepository<Doctor>
    {

        public Task<bool> isExist(int doctor_id);
        public Task<Doctor> findDoctorByID(int doctor_id);

        public Task<List<Doctor>> findDoctorListBySpecialisation(int specialisation_id);

        // create, delete, get (all) are ?inherited? from IRepository

    }
}