using MasAcademyLab.Domain;

namespace MasAcademyLab.Service
{
    public interface ITokenService
    {
        string BuildToken(User user);
        bool IsTokenValid(string token);
    }
}
