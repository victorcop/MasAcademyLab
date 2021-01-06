using MasAcademyLab.Service;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace MasAcademyLab.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpeakerController : ControllerBase
    {
        private readonly ICampRepository _campRepository;

        public SpeakerController(ICampRepository campRepository)
        {
            _campRepository = campRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var speakers = await _campRepository.GetAllSpeakersAsync();

            if (speakers == null || !speakers.Any())
            {
                return NoContent();
            }

            return Ok(speakers);
        }
    }
}
