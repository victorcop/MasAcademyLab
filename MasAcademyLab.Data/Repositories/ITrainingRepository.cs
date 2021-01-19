using MasAcademyLab.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MasAcademyLab.Data.Repositories
{
    public interface ITrainingRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<List<Training>> GetAllTrainingsAsync(bool includeTalks = false);
        Task<List<Training>> GetAllTrainingByEventDate(DateTime dateTime, bool includeTalks = false);
        Task<List<Speaker>> GetAllSpeakersAsync();
        Task<Training> GetTrainingAsync(string code, bool includeTalks = false);
        Task<Speaker> GetSpeakerAsync(int speakerId);
        Task<List<Speaker>> GetSpeakersByCodeAsync(string code);
        Task<Talk> GetTalkByCodeAsync(string code, int talkId, bool includeSpeakers = false);
        Task<List<Talk>> GetTalksByCodeAsync(string code, bool includeSpeakers = false);
        Task<int> SaveChangesAsync();
    }
}