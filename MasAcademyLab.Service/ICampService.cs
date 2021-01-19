using MasAcademyLab.Domain;
using MasAcademyLab.Service.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MasAcademyLab.Service
{
    public interface ICampService
    {
        Task<IEnumerable<CampModel>> GetAllCampsAsync(bool includeTalks = false);
        Task<CampModel> GetCampAsync(string moniker, bool includeTalks = false);
        Task<IEnumerable<CampModel>> GetAllCampsByEventDate(DateTime dateTime, bool includeTalks = false);
        Task<CampModel> CreateCampAsync(CampModel campModel);
        Task<CampModel> UpdateCampAsync(string moniker, CampModel campModel);
        Task DeleteCampAsync(string moniker);
    }
}