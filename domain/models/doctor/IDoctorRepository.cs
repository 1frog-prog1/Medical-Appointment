namespace domain.models.doctor
{
    public interface IDoctorRepository : IRepository<Doctor>
    {

        public bool isExist(int doctor_id);
         public Doctor findDoctorByID(int doctor_id);

        public List<Doctor> findDoctorListBySpecialisation(int specialisation_id);

        // create, delete, get (all) are ?inherited? from IRepository

    }
}