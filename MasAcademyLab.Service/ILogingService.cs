using MasAcademyLab.Service.Models;
using System.Threading.Tasks;

namespace MasAcademyLab.Service
{
    public interface ILogingService
    {
        Task<string> ValidateUser(UserModel user);
    }
}
