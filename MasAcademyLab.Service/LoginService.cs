using MasAcademyLab.Data.Repositories;
using MasAcademyLab.Service.Models;
using System.Threading.Tasks;

namespace MasAcademyLab.Service
{
    public class LoginService : ILogingService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public LoginService(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<string> ValidateUser(UserModel user)
        {
            var validUser = await _userRepository.GetUser(user.Name, user.Password);

            if (validUser is not null)
            {
                return _tokenService.BuildToken(validUser);
            }

            return null;
        }
    }
}
