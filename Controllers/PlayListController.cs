using Microsoft.AspNetCore.Mvc;
using NoticiaCadastroAPI.Model;
using NoticiaCadastroAPI.Services;

namespace NoticiaCadastroAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class PlayListController : ControllerBase
    {

        private readonly MongoDBService _mongoDBService;

        public PlayListController(MongoDBService mongoDBService)
        {
            _mongoDBService = mongoDBService;
        }

        [HttpGet]
        public async Task<List<PlayList>> Index() {
            try
            {
                return await _mongoDBService.GetAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PlayList playlist) {
            await _mongoDBService.CreateAsync(playlist);
            return CreatedAtAction(nameof(Index), new { id = playlist.Id }, playlist);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AddToPlaylist(string id, [FromBody] string movieId) {
            await _mongoDBService.AddToPlaylistAsync(id, movieId);
            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _mongoDBService.DeleteAsync(id);
            return NoContent();
        }



    }
}
