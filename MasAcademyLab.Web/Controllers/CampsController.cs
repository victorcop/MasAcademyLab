using MasAcademyLab.Service;
using Microsoft.AspNetCore.Mvc;
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

        public CampsController(ICampService campService)
        {
            _campService = campService;
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
    }
}
