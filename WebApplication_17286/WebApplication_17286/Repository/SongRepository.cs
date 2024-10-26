using Microsoft.EntityFrameworkCore;
using WebApplication_17286.DBContexts;
using WebApplication_17286.Models;

namespace WebApplication_17286.Repository
{
    public class SongRepository : ISongRepository
    {
        private readonly MusicContext _dbContext;
        public SongRepository(MusicContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Save changes to the database
        public void Save()
        {
            _dbContext.SaveChanges();
        }

        // Insert song
        public void InsertSong(Song song)
        {
            _dbContext.Add(song);
            Save();
        }

        // Update song
        public void UpdateSong(Song song)
        {
            _dbContext.Entry(song).State = EntityState.Modified;
            Save();
        }

        // Delete song
        public void DeleteSong(int songId)
        {
            var song = _dbContext.Songs.Find(songId);
            _dbContext.Songs.Remove(song);
            Save();
        }

        // Get song by ID
        public Song GetSongById(int songId)
        {
            var song = _dbContext.Songs.Find(songId);
            _dbContext.Entry(song).Reference(s => s.SongPerformer).Load();
            return song;
        }

        // Get all songs
        public IEnumerable<Song> GetSongs()
        {
            return _dbContext.Songs.Include(s => s.SongPerformer).ToList();
        }

    }
}
