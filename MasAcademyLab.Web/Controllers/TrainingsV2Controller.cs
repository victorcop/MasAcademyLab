﻿using MasAcademyLab.Service;
using MasAcademyLab.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MasAcademyLab.Web.Controllers
{
    [Route("api/Trainings")]
    [ApiVersion("2.0")]
    [ApiController]
    [Authorize]
    public class TrainingsV2Controller : ControllerBase
    {
        private readonly ITrainingService _trainingService;
        private readonly LinkGenerator _linkGenerator;

        public TrainingsV2Controller(ITrainingService trainingService, LinkGenerator linkGenerator)
        {
            _trainingService = trainingService;
            _linkGenerator = linkGenerator;
        }

        [HttpGet]
        public async Task<IActionResult> Get(bool includeTalks = false)
        {
            var trainings = await _trainingService.GetAllTrainingsAsync(includeTalks);

            if (trainings == null || !trainings.Any())
            {
                return NoContent();
            }

            return Ok(trainings);
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> Get(string code, bool includeTalks = false)
        {
            var training = await _trainingService.GetTrainingAsync(code, includeTalks);

            if (training == null)
            {
                return NotFound();
            }

            return Ok(training);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchByDate(DateTime date, bool includeTalks = false)
        {
            var trainings = await _trainingService.GetAllTrainingsByEventDate(date, includeTalks);

            if (trainings == null || !trainings.Any())
            {
                return NotFound();
            }

            return Ok(trainings);
        }

        [HttpPost]
        public async Task<IActionResult> Post(TrainingCreationModel trainingModel)
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

        [HttpPut("{code}")]
        public async Task<IActionResult> Put(string code, TrainingUpdateModel trainingModel)
        {
            if (!await _trainingService.Exists(code))
            {
                return NotFound();
            }

            var training = await _trainingService.UpdateTrainingAsync(code, trainingModel);

            return Ok(training);
        }

        [HttpDelete("{code}")]
        public async Task<IActionResult> Delete(string code)
        {
            if (await _trainingService.Exists(code))
            {
                return NotFound();
            }

            await _trainingService.DeleteTrainingAsync(code);

            return Ok();
        }
    }
}
