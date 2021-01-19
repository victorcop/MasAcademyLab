using MasAcademyLab.Service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MasAcademyLab.Service
{
    public interface ITalkService
    {
        Task<IEnumerable<TalkModel>> GetTalksAsync(string code, bool includeSpeakers = false);
        Task<TalkModel> GetTalkAsync(string code, int talkId, bool includeSpeakers = false);
        Task<TalkModel> CreateTalkAsync(string code, TalkModel talkModel);
        Task<TalkModel> UpdateTalkAsync(string code, int talkId, TalkModel talkModel);
        Task DeleteTalkAsync(string code, int talkId);
    }
}