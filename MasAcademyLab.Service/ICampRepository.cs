using MasAcademyLab.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MasAcademyLab.Service
{
    public interface ICampRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<List<Camp>> GetAllCampsAsync(bool includeTalks = false);
        Task<List<Camp>> GetAllCampsByEventDate(DateTime dateTime, bool includeTalks = false);
        Task<List<Speaker>> GetAllSpeakersAsync();
        Task<Camp> GetCampAsync(string moniker, bool includeTalks = false);
        Task<Speaker> GetSpeakerAsync(int speakerId);
        Task<List<Speaker>> GetSpeakersByMonikerAsync(string moniker);
        Task<Talk> GetTalkByMonikerAsync(string moniker, int talkId, bool includeSpeakers = false);
        Task<List<Talk>> GetTalksByMonikerAsync(string moniker, bool includeSpeakers = false);
        Task<int> SaveChangesAsync();
    }
}