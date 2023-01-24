namespace domain.models.specialisation
{
    public interface ISpecialisationRepository : IRepository<Specialisation>
    {
         Task<bool> isExist(int specialisation_id);
    }
}