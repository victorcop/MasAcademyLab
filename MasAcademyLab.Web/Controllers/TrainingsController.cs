using MasAcademyLab.Service;
using MasAcademyLab.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasAcademyLab.Web.Controllers
{
    /// <summary>
    /// Training Controller
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class TrainingsController : ControllerBase
    {
        private readonly ITrainingService _trainingService;
        private readonly LinkGenerator _linkGenerator;

        /// <summary>
        /// Training Constructure
        /// </summary>
        /// <param name="trainingService">Object of the type <see cref="ITrainingService"/></param>
        /// <param name="linkGenerator">Object of the type <see cref="LinkGenerator"/></param>
        public TrainingsController(ITrainingService trainingService, LinkGenerator linkGenerator)
        {
            _trainingService = trainingService;
            _linkGenerator = linkGenerator;
        }

        /// <summary>
        /// Gets all trainings
        /// </summary>
        /// <param name="includeTalks">includeTalks = false</param>
        /// <returns>An ActionResult of a list of TrainingModel</returns>
        /// <response code="200">Returns a List of TrainingModel</response>
        /// <response code="204">No Content</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainingModel>>> Get(bool includeTalks = false)
        {
            var trainings = await _trainingService.GetAllTrainingsAsync(includeTalks);

            if (trainings == null || !trainings.Any())
            {
                return NoContent();
            }

            return Ok(trainings);
        }

        /// <summary>
        /// Gets a Training
        /// </summary>
        /// <param name="code">Training Code</param>
        /// <param name="includeTalks">includeTalks = false</param>
        /// <returns>Returns an ActionResult of TrainingModel</returns>
        /// <response code="200">Returns an ActionResult of TrainingModel</response>
        /// <response code="404">Not Found</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{code}")]
        public async Task<ActionResult<TrainingModel>> Get(string code, bool includeTalks = false)
        {
            var training = await _trainingService.GetTrainingAsync(code, includeTalks);

            if (training == null)
            {
                return NotFound();
            }

            return Ok(training);
        }

        /// <summary>
        /// Search By Date
        /// </summary>
        /// <param name="date">Object of the type <see cref="DateTime"/></param>
        /// <param name="includeTalks">includeTalks = false</param>
        /// <returns>Returns an ActionResult of TrainingModel</returns>
        /// <response code="200">Returns an ActionResult of TrainingModel</response>
        /// <response code="404">Not Found</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<TrainingModel>>> SearchByDate(DateTime date, bool includeTalks = false)
        {
            var trainings = await _trainingService.GetAllTrainingsByEventDate(date, includeTalks);

            if (trainings == null || !trainings.Any())
            {
                return NotFound();
            }

            return Ok(trainings);
        }

        /// <summary>
        /// Creates a Training
        /// </summary>
        /// <param name="trainingModel">Object of the type <see cref="TrainingCreationModel"/></param>
        /// <returns>Returns an ActionResult of TrainingModel and the resource location</returns>
        /// <response code="201">Returns an ActionResult of TrainingModel and the resource location</response>
        /// <response code="400">Bad Request</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<TrainingModel>> Post(TrainingCreationModel trainingModel)
        {
            var location = _linkGenerator.GetPathByAction("Get",
                "Trainings", new { code = trainingModel.Code });

            if (string.IsNullOrWhiteSpace(location))
            {
                return BadRequest();
            }

            if (await _trainingService.Exists(trainingModel.Code))
            {
                return BadRequest("Code is in use.");
            }

            var training = await _trainingService.CreateTrainingAsync(trainingModel);

            return Created(location, training);
        }

        /// <summary>
        /// Updates a Training
        /// </summary>
        /// <param name="code">Training Code</param>
        /// <param name="trainingModel">Object of the type <see cref="TrainingUpdateModel"/></param>
        /// <returns>Returns an ActionResult of TrainingModel</returns>
        /// <response code="200">Returns an ActionResult of TrainingModel</response>
        /// <response code="404">Not Found</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("{code}")]
        public async Task<ActionResult<TrainingModel>> Put(string code, TrainingUpdateModel trainingModel)
        {
            if (!await _trainingService.Exists(code))
            {
                return NotFound();
            }

            var training = await _trainingService.UpdateTrainingAsync(code, trainingModel);

            return Ok(training);
        }

        /// <summary>
        /// Patch Training
        /// </summary>
        /// <param name="code">Training Code</param>
        /// <param name="trainingPatchDocument">Object of the type <see cref="JsonPatchDocument{TrainingUpdateModel}"/></param>
        /// <returns>Returns an ActionResult of TrainingModel</returns>
        /// <response code="200">Returns an ActionResult of TrainingModel</response>
        /// <response code="404">Not Found</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPatch("{code}")]
        public async Task<ActionResult<TrainingModel>> Patch(string code, JsonPatchDocument<TrainingUpdateModel> trainingPatchDocument)
        {
            if (!await _trainingService.Exists(code))
            {
                return NotFound();
            }

            var training = await _trainingService.PatchTrainingAsync(code, trainingPatchDocument);

            return Ok(training);
        }

        /// <summary>
        /// Deletes a Training
        /// </summary>
        /// <param name="code">Training Code</param>
        /// <returns>Returns an IActionResult</returns>
        /// <response code="200">Returns an IActionResult</response>
        /// <response code="404">Not Found</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete("{code}")]
        public async Task<IActionResult> Delete(string code)
        {
            if (!await _trainingService.Exists(code))
            {
                return NotFound();
            }

            await _trainingService.DeleteTrainingAsync(code);

            return Ok();
        }
    }
}
