using MasAcademyLab.Domain;
using System.Threading.Tasks;

namespace MasAcademyLab.Data.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUser(string name, string password);
    }
}
