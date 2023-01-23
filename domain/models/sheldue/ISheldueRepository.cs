namespace domain.models.sheldue
{
    public interface ISheldueRepository : IRepository<Sheldue>
    {
        // я бы возвращала список, когда у него прием
        // а то у него мб запись в начале дня и в конце
        // а все между - свободно. не лучше ли дать врачу об этом знать
        // чтоб он мог спокойно уйти.. куда там обычно врачи уходят подолгу

         public Sheldue getDoctorSheldue(int doctor_id); 

         public Sheldue getSheldueById(int sheldue_id);

    }
}