using MasAcademyLab.Service;
using MasAcademyLab.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasAcademyLab.Web.Controllers
{
    /// <summary>
    /// Talks Controller
    /// </summary>
    [Produces("application/json")]
    [Route("api/Trainings/{code}/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class TalksController : ControllerBase
    {
        private readonly ITalkService _talkService;
        private readonly LinkGenerator _linkGenerator;
        private readonly ITrainingService _trainingService;

        /// <summary>
        /// Talks Constructure
        /// </summary>
        /// <param name="talkService">Object of the type <see cref="ITalkService"/></param>
        /// <param name="linkGenerator">Object of the type <see cref="LinkGenerator"/></param>
        /// <param name="trainingService">Object of the type <see cref="ITrainingService"/></param>
        public TalksController(ITalkService talkService, LinkGenerator linkGenerator, ITrainingService trainingService)
        {
            _talkService = talkService;
            _linkGenerator = linkGenerator;
            _trainingService = trainingService;
        }

        /// <summary>
        /// Gets the talks from a training
        /// </summary>
        /// <param name="code">Training Code</param>
        /// <param name="includeSpeakers">includeSpeakers = false</param>
        /// <returns>Returns a List of TalkModel</returns>
        /// <response code="200">Returns a List of TalkModel</response>
        /// <response code="204">No Content</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TalkModel>>> Get(string code, bool includeSpeakers = false)
        {
            var talks = await _talkService.GetTalksAsync(code, includeSpeakers);

            if (talks == null || !talks.Any())
            {
                return NoContent();
            }

            return Ok(talks);
        }

        /// <summary>
        /// Gets a talk from a training
        /// </summary>
        /// <param name="code">Training Code</param>
        /// <param name="talkId">Talk Id</param>
        /// <param name="includeSpeakers">includeSpeakers = false</param>
        /// <returns>Returns a ActionResult of TalkModel</returns>
        /// <response code="200">Returns a ActionResult of TalkModel</response>
        /// <response code="404">No Content</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{talkId:int}")]
        public async Task<ActionResult<TalkModel>> Get(string code, int talkId, bool includeSpeakers = false)
        {
            var talk = await _talkService.GetTalkAsync(code, talkId, includeSpeakers);

            if (talk == null)
            {
                return NotFound();
            }

            return Ok(talk);
        }

        /// <summary>
        /// Creates a training
        /// </summary>
        /// <param name="code">Training Code</param>
        /// <param name="talkModel">Object of the type <see cref="TalkCreationModel"/></param>
        /// <returns>Returns a ActionResult of TalkModel</returns>
        /// <response code="201">Returns a ActionResult of TalkModel</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<ActionResult<TalkModel>> Post(string code, TalkCreationModel talkModel)
        {
            var talk = await _talkService.CreateTalkAsync(code, talkModel);

            var url = _linkGenerator.GetPathByAction(HttpContext,
                                                     "Get",
                                                     values: new { code, talkId = talk.TalkId });

            return Created(url, talk);
        }

        /// <summary>
        /// Updates a talk
        /// </summary>
        /// <param name="code">Training Code</param>
        /// <param name="talkId">Talk id</param>
        /// <param name="talkModel">Oject of the type <see cref="TalkUpdateModel"/></param>
        /// <returns>Returns a ActionResult of TalkModel</returns>
        /// <response code="200">Returns a ActionResult of TalkModel</response>
        /// <response code="404">No Content</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("{talkId:int}")]
        public async Task<ActionResult<TalkModel>> Put(string code, int talkId, TalkUpdateModel talkModel)
        {
            if (!await _trainingService.Exists(code) || !await _talkService.Exists(code, talkId))
            {
                return NotFound();
            }

            var talk = await _talkService.UpdateTalkAsync(code, talkId, talkModel);

            return Ok(talk);
        }

        /// <summary>
        /// Deletes a talk
        /// </summary>
        /// <param name="code">Training Code</param>
        /// <param name="talkId">Talk id</param>
        /// <returns>Returns a IActionResult</returns>
        /// <response code="200">Returns a IActionResult</response>
        /// <response code="404">No Content</response>
        [HttpDelete("{talkId:int}")]
        public async Task<IActionResult> Delete(string code, int talkId)
        {
            if ( !await _trainingService.Exists(code) || !await _talkService.Exists(code, talkId))
            {
                return NotFound();
            }

            await _talkService.DeleteTalkAsync(code, talkId);

            return Ok();
        }
    }
}
