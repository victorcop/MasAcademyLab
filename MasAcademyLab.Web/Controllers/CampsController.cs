using MasAcademyLab.Service;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Get()
        {
            var camps = await _campService.GetAllCampsAsync();

            if(camps == null || !camps.Any())
            {
                return NoContent();
            }

            return Ok(camps);
        }
    }
}
