using MasAcademyLab.Service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MasAcademyLab.Service
{
    public interface ICampService
    {
        Task<IEnumerable<CampModel>> GetAllCampsAsync();
    }
}