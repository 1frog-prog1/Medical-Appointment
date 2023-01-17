namespace domain.models.doctor
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
         public Doctor findDoctorByID(int doctor_id);

        public IEnumerable<Doctor> findDoctorListBySpecialisation(int specialisation_id);

        // create, delete, get (all) are ?inherited? from IRepository

    }
}