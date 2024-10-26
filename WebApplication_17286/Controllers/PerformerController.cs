using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using WebApplication_17286.Models;
using WebApplication_17286.Repository;

namespace WebApplication_17286.Controllers
{
    // Route for API where [controller] is "Performer"
    [Route("api/[controller]")]
    public class PerformerController : Controller
    {
        // Dependency injection for performer repository
        private readonly IPerformerRepository _performerRepository;

        // Constructor to initialize repository
        public PerformerController(IPerformerRepository performerRepository)
        {
            _performerRepository = performerRepository;
        }

        // GET: api/Performer - retrieve all performers
        [HttpGet]
        public IActionResult GetPerformers()
        {
            var performers = _performerRepository.GetPerformers();
            return new OkObjectResult(performers);
        }

        // GET: api/Performer/{performerId} - retrieve performer by ID
        [HttpGet("{id}", Name = "GetPerformer")]
        public IActionResult GetPerformer(int id)
        {
            var performer = _performerRepository.GetPerformerById(id);
            if (performer == null)
            {
                return NotFound(); // Return 404 if performer not found
            }
            return new OkObjectResult(performer);
        }

        // POST: api/Performer - add new performer
        [HttpPost]
        public IActionResult PostPerformer([FromBody] Performer performer)
        {
            if (performer == null)
            {
                return BadRequest(); // Return 400 if input is invalid
            }

            using (var scope = new TransactionScope())
            {
                _performerRepository.InsertPerformer(performer);
                scope.Complete();
                return CreatedAtAction(nameof(GetPerformer), new { id = performer.Id }, performer);
            }
        }

        // PUT: api/Performer/{performerId} - update performer by ID
        [HttpPut("{id}")]
        public IActionResult PutPerformer(int id, [FromBody] Performer performer)
        {
            if (performer == null || performer.Id != id)
            {
                return BadRequest(); // Return 400 if input is invalid
            }

            using (var scope = new TransactionScope())
            {
                _performerRepository.UpdatePerformer(performer);
                scope.Complete();
                return new OkResult();
            }
        }

        // DELETE: api/Performer/{performerId} - delete performer by ID
        [HttpDelete("{id}")]
        public IActionResult DeletePerformer(int id)
        {
            _performerRepository.DeletePerformer(id);
            return new OkResult();
        }
    }

}
