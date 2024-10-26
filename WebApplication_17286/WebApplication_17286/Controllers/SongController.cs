using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using WebApplication_17286.Models;
using WebApplication_17286.Repository;

namespace WebApplication_17286.Controllers
{
    // Define API route where [controller] will be replaced by controller's name (Song)
    [Route("api/[controller]")]
    public class SongController : Controller
    {
        // Dependency injection of song repository interface
        private readonly ISongRepository _songRepository;

        // Constructor to initialize repository dependency
        public SongController(ISongRepository songRepository)
        {
            _songRepository = songRepository;
        }

        // GET: api/Song - retrieve a list of all songs
        [HttpGet]
        public IActionResult Get()
        {
            var songs = _songRepository.GetSongs(); // Call repository to get all songs
            return new OkObjectResult(songs); // Return songs list with HTTP status 200
        }

        // GET: api/Song/{songId} - retrieve song by ID
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var song = _songRepository.GetSongById(id); // Call to repository to get song by ID
            return new OkObjectResult(song); // Return song with HTTP status 200
        }

        // POST: api/Song - add new song to collection
        [HttpPost]
        public IActionResult Post([FromBody] Song song)
        {
            // Start new transaction scope for data consistency
            using (var scope = new TransactionScope())
            {
                _songRepository.InsertSong(song); // Insert new song using repository
                scope.Complete(); // Complete transaction
                                  // Return created song with ID and HTTP status 201.
                return CreatedAtAction(nameof(Get), new { id = song.Id }, song);
            }
        }

        // PUT: api/Song/{songId} - update existing song with specified ID
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Song song)
        {
            if (song != null)
            {
                // Start new transaction scope for data consistency
                using (var scope = new TransactionScope())
                {
                    _songRepository.UpdateSong(song); // Update song in repository
                    scope.Complete(); // Complete transaction
                    return new OkResult(); // Return HTTP status 200 if update is successful
                }
            }
            return new NoContentResult(); // Return HTTP status 204 if song is null
        }

        // DELETE: api/Song/{songId} - delete song with specified ID.
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _songRepository.DeleteSong(id); // Call repository to delete song by ID
            return new OkResult(); // Return HTTP status 200 if deletion is successful
        }
    }

}
