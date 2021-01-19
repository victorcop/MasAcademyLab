﻿using AutoMapper;
using MasAcademyLab.Data.Repositories;
using MasAcademyLab.Domain;
using MasAcademyLab.Service.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MasAcademyLab.Service
{
    public class TrainingService : ITrainingService
    {
        private readonly ITrainingRepository _trainingRepository;
        private readonly IMapper _mapper;

        public TrainingService(ITrainingRepository trainingRepository, IMapper mapper)
        {
            _trainingRepository = trainingRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TrainingModel>> GetAllTrainingsAsync(bool includeTalks = false)
        {
            try
            {
                var trainings = await _trainingRepository.GetAllTrainingsAsync(includeTalks);

                return _mapper.Map<IEnumerable<Training>, IEnumerable<TrainingModel>>(trainings);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<TrainingModel> GetTrainingAsync(string code, bool includeTalks = false)
        {
            try
            {
                var training = await _trainingRepository.GetTrainingAsync(code, includeTalks);

                return _mapper.Map<Training, TrainingModel>(training);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<TrainingModel>> GetAllTrainingsByEventDate(DateTime dateTime, bool includeTalks = false)
        {
            try
            {
                var trainings = await _trainingRepository.GetAllTrainingByEventDate(dateTime, includeTalks);

                return _mapper.Map<IEnumerable<Training>, IEnumerable<TrainingModel>>(trainings);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<TrainingModel> CreateTrainingAsync(TrainingModel trainingModel)
        {
            try
            {
                var existingTraining = await _trainingRepository.GetTrainingAsync(trainingModel.Code);

                if(existingTraining != null)
                {
                    return null;
                }

                var training = _mapper.Map<TrainingModel, Training>(trainingModel);

                _trainingRepository.Add(training);

                await _trainingRepository.SaveChangesAsync();
                return _mapper.Map<Training, TrainingModel>(training);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<TrainingModel> UpdateTrainingAsync(string code, TrainingModel trainingModel)
        {
            try
            {
                var oldTraining = await _trainingRepository.GetTrainingAsync(code);

                if (oldTraining == null)
                {
                    return null;
                }

                var training = _mapper.Map(trainingModel, oldTraining);

                await _trainingRepository.SaveChangesAsync();
                return _mapper.Map<Training, TrainingModel>(training);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteTrainingAsync(string code)
        {
            var oldTraining = await _trainingRepository.GetTrainingAsync(code);

            if (oldTraining != null)
            {
                _trainingRepository.Delete(oldTraining);

                await _trainingRepository.SaveChangesAsync();
            }            
        }
    }
}
