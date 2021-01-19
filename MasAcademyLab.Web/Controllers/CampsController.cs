using MasAcademyLab.Service;
using MasAcademyLab.Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MasAcademyLab.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampsController : ControllerBase
    {
        private readonly ICampService _campService;
        private readonly LinkGenerator _linkGenerator;

        public CampsController(ICampService campService, LinkGenerator linkGenerator)
        {
            _campService = campService;
            _linkGenerator = linkGenerator;
        }

        [HttpGet]
        public async Task<IActionResult> Get(bool includeTalks = false)
        {
            var camps = await _campService.GetAllCampsAsync(includeTalks);

            if(camps == null || !camps.Any())
            {
                return NoContent();
            }

            return Ok(camps);
        }

        [HttpGet("{moniker}")]
        public async Task<IActionResult> Get(string moniker)
        {
            var camp = await _campService.GetCampAsync(moniker);

            if(camp == null)
            {
                return NotFound();
            }

            return Ok(camp);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchByDate(DateTime date, bool includeTalks = false)
        {
            var camps = await _campService.GetAllCampsByEventDate(date, includeTalks);

            if (camps == null || !camps.Any())
            {
                return NotFound();
            }

            return Ok(camps);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CampModel campModel)
        {
            var location = _linkGenerator.GetPathByAction("Get",
                "Camps", new { moniker = campModel.Moniker });

            if (string.IsNullOrWhiteSpace(location))
            {
                return BadRequest();
            }

            var camp = await _campService.CreateCampAsync(campModel);

            if (camp == null)
            {
                return BadRequest("Moniker is in use.");
            }

            return Created(location, camp);
        }
    }
}
