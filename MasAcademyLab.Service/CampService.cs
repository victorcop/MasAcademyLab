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

        public async Task<IEnumerable<CampModel>> GetAllCampsAsync()
        {
            try
            {
                var camps = await _campRepositorie.GetAllCampsAsync();

                return _mapper.Map<IEnumerable<Camp>, IEnumerable<CampModel>>(camps);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
