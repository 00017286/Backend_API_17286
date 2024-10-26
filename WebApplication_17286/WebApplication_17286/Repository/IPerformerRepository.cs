using WebApplication_17286.Models;

namespace WebApplication_17286.Repository
{
    public interface IPerformerRepository
    {
        void InsertPerformer(Performer performer); // Adds new performer to db
        void UpdatePerformer(Performer performer); // Updates existing performer
        void DeletePerformer(int performerId); // Removes performer by ID
        Performer GetPerformerById(int Id); // Retrieves performer by ID
        IEnumerable<Performer> GetPerformers(); // Gets all performers
    }

}
