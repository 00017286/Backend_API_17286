using WebApplication_17286.Models;

namespace WebApplication_17286.Repository
{
    public interface ISongRepository
    {
        void InsertSong(Song song); // Add new song to database
        void UpdateSong(Song song); // Update existing song
        void DeleteSong(int songId); // Remove song by ID
        Song GetSongById(int Id); // Retrieve song by ID
        IEnumerable<Song> GetSongs(); // Get all songs
    }

}
