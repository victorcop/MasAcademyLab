using MasAcademyLab.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace MasAcademyLab.Data.Repositories
{
    public class UserRepository : GenericRepository, IUserRepository
    {
        private readonly MasAcademyLabContext _context;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(MasAcademyLabContext context, ILogger<UserRepository> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public Task<User> GetUser(string name, string password)
        {
            _logger.LogInformation($"Getting User");

            IQueryable<User> query = _context.Users;

            // Add Query
            query = query
              .Where(t => t.Name == name && t.Password == password);

            return query.FirstOrDefaultAsync();
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

    }
}
