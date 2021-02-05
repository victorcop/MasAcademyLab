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
        Task<TrainingModel> CreateTrainingAsync(TrainingCreationModel trainingModel);
        Task<TrainingModel> UpdateTrainingAsync(string code, TrainingUpdateModel trainingModel);
        Task DeleteTrainingAsync(string code);
        Task<bool> Exists(string code);
    }
}