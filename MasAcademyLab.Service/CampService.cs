﻿using AutoMapper;
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

        public async Task<CampModel> CreateCampAsync(CampModel campModel)
        {
            try
            {
                var existingCamp = await _campRepositorie.GetCampAsync(campModel.Moniker);

                if(existingCamp != null)
                {
                    return null;
                }

                var camp = _mapper.Map<CampModel, Camp>(campModel);

                _campRepositorie.Add(camp);

                var response = await _campRepositorie.SaveChangesAsync();

                if (response == 1)
                {
                    return _mapper.Map<Camp, CampModel>(camp);
                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CampModel> UpdateCampAsync(string moniker, CampModel campModel)
        {
            try
            {
                var oldCamp = await _campRepositorie.GetCampAsync(moniker);

                if (oldCamp == null)
                {
                    return null;
                }

                var camp = _mapper.Map(campModel, oldCamp);

                var response = await _campRepositorie.SaveChangesAsync();

                if (response == 1)
                {
                    return _mapper.Map<Camp, CampModel>(camp);
                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteCampAsync(string moniker)
        {
            var oldCamp = await _campRepositorie.GetCampAsync(moniker);

            if (oldCamp != null)
            {
                _campRepositorie.Delete(oldCamp);

                await _campRepositorie.SaveChangesAsync();
            }            
        }
    }
}
