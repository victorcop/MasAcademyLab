using MasAcademyLab.Service.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MasAcademyLab.Service
{
    public interface ITrainingService
    {
        Task<IEnumerable<TrainingModel>> GetAllTrainingsAsync(bool includeTalks = false);
        Task<TrainingModel> GetTrainingAsync(string code, bool includeTalks = false);
        Task<IEnumerable<TrainingModel>> GetAllTrainingsByEventDate(DateTime dateTime, bool includeTalks = false);
        Task<TrainingModel> CreateTrainingAsync(TrainingModel trainingModel);
        Task<TrainingModel> UpdateTrainingAsync(string code, TrainingModel trainingModel);
        Task DeleteTrainingAsync(string code);
    }
}