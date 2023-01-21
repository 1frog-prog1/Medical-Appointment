namespace domain.models.specialisation
{
    public interface ISpecialisationRepository : IRepository<Specialisation>
    {
         bool isExist(int specialisation_id);
    }
}