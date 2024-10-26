using Microsoft.EntityFrameworkCore;
using WebApplication_17286.DBContexts;
using WebApplication_17286.Models;

namespace WebApplication_17286.Repository
{
    public class PerformerRepository : IPerformerRepository
    {
        private readonly MusicContext _dbContext;
        public PerformerRepository(MusicContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Save changes to the database
        public void Save()
        {
            _dbContext.SaveChanges();
        }

        // Insert Performer
        public void InsertPerformer(Performer performer)
        {
            _dbContext.Add(performer);
            Save();
        }

        // Update Performer
        public void UpdatePerformer(Performer performer)
        {
            _dbContext.Entry(performer).State = EntityState.Modified;
            Save();
        }

        // Delete Performer
        public void DeletePerformer(int performerId)
        {
            var performer = _dbContext.Performers.Find(performerId);
            _dbContext.Performers.Remove(performer);
            Save();
        }

        // Get Performer by ID
        public Performer GetPerformerById(int performerId)
        {
            return _dbContext.Performers.Find(performerId);
        }

        // Get all Performers
        public IEnumerable<Performer> GetPerformers()
        {
            return _dbContext.Performers.ToList();
        }
    }
}
