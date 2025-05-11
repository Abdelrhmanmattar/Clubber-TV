using code_quests.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace code_quests.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class PlaylistController : ControllerBase
    {
        private readonly IPlayListServices _playListServices;

        public PlaylistController(IPlayListServices playListServices)
        {
            _playListServices = playListServices;
        }
        [HttpPost("{idMatch}")]
        public async Task<IActionResult> AddMatch([FromRoute] int idMatch)
        {
            var res = await _playListServices.AddMatch2Playlist(User, idMatch);
            return res.StatusCode == HttpStatusCode.OK ? Ok(res) : BadRequest(res);
        }

        [HttpDelete("{idMatch}")]
        public async Task<IActionResult> remMatch([FromRoute] int idMatch)
        {
            var res = await _playListServices.remMatchfromPlaylist(User, idMatch);
            return res.StatusCode == HttpStatusCode.OK ? Ok(res) : BadRequest(res);
        }

        [HttpGet]
        public async Task<IActionResult> getAll()
        {
            var res = await _playListServices.getPlayList(User);
            return Ok(res);
        }
    }
}
