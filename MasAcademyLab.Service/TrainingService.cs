using AutoMapper;
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
        private readonly ITrainingRepository _trainingRepositorie;
        private readonly IMapper _mapper;

        public TrainingService(ITrainingRepository trainingRepositorie, IMapper mapper)
        {
            _trainingRepositorie = trainingRepositorie;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TrainingModel>> GetAllTrainingsAsync(bool includeTalks = false)
        {
            try
            {
                var trainings = await _trainingRepositorie.GetAllTrainingsAsync(includeTalks);

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
                var training = await _trainingRepositorie.GetTrainingAsync(code, includeTalks);

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
                var trainings = await _trainingRepositorie.GetAllTrainingByEventDate(dateTime, includeTalks);

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
                var existingTraining = await _trainingRepositorie.GetTrainingAsync(trainingModel.Code);

                if(existingTraining != null)
                {
                    return null;
                }

                var training = _mapper.Map<TrainingModel, Training>(trainingModel);

                _trainingRepositorie.Add(training);

                var response = await _trainingRepositorie.SaveChangesAsync();

                if (response == 1)
                {
                    return _mapper.Map<Training, TrainingModel>(training);
                }

                return null;
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
                var oldTraining = await _trainingRepositorie.GetTrainingAsync(code);

                if (oldTraining == null)
                {
                    return null;
                }

                var training = _mapper.Map(trainingModel, oldTraining);

                var response = await _trainingRepositorie.SaveChangesAsync();

                if (response == 1)
                {
                    return _mapper.Map<Training, TrainingModel>(training);
                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteTrainingAsync(string code)
        {
            var oldTraining = await _trainingRepositorie.GetTrainingAsync(code);

            if (oldTraining != null)
            {
                _trainingRepositorie.Delete(oldTraining);

                await _trainingRepositorie.SaveChangesAsync();
            }            
        }
    }
}
