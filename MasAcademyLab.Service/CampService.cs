using AutoMapper;
using MasAcademyLab.Data.Repositories;
using MasAcademyLab.Domain;
using MasAcademyLab.Service.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MasAcademyLab.Service
{
    public class CampService : ICampService
    {
        private readonly ICampRepository _campRepositorie;
        private readonly IMapper _mapper;

        public CampService(ICampRepository campRepositorie, IMapper mapper)
        {
            _campRepositorie = campRepositorie;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CampModel>> GetAllCampsAsync(bool includeTalks = false)
        {
            try
            {
                var camps = await _campRepositorie.GetAllCampsAsync(includeTalks);

                return _mapper.Map<IEnumerable<Camp>, IEnumerable<CampModel>>(camps);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CampModel> GetCampAsync(string moniker, bool includeTalks = false)
        {
            try
            {
                var camp = await _campRepositorie.GetCampAsync(moniker, includeTalks);

                return _mapper.Map<Camp, CampModel>(camp);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<CampModel>> GetAllCampsByEventDate(DateTime dateTime, bool includeTalks = false)
        {
            try
            {
                var camps = await _campRepositorie.GetAllCampsByEventDate(dateTime, includeTalks);

                return _mapper.Map<IEnumerable<Camp>, IEnumerable<CampModel>>(camps);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
