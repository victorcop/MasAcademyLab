using MasAcademyLab.Domain;
using MasAcademyLab.Service;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MasAcademyLab.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpeakerController : ControllerBase
    {
        private readonly ISpeakerService _speakerService;

        public SpeakerController(ISpeakerService speakerService)
        {
            _speakerService = speakerService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Speaker>> Get()
        {
            var speakers = _speakerService.GetSpeakers();

            if (speakers == null)
            {
                return NoContent();
            }

            return Ok(speakers);
        }
    }
}
