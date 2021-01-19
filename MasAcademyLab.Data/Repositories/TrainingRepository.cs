using MasAcademyLab.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasAcademyLab.Data.Repositories
{
    public class TrainingRepository : ITrainingRepository
    {
        private readonly MasAcademyLabContext _context;
        private readonly ILogger<TrainingRepository> _logger;

        public TrainingRepository(MasAcademyLabContext context, ILogger<TrainingRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void Add<T>(T entity) where T : class
        {
            _logger.LogInformation($"Adding an object of type {entity.GetType()} to the context.");
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _logger.LogInformation($"Removing an object of type {entity.GetType()} to the context.");
            _context.Remove(entity);
        }

        public Task<int> SaveChangesAsync()
        {
            _logger.LogInformation($"Attempitng to save the changes in the context");

            // Only return success if at least one row was changed
            return _context.SaveChangesAsync();
        }

        public Task<List<Training>> GetAllTrainingByEventDate(DateTime dateTime, bool includeTalks = false)
        {
            _logger.LogInformation($"Getting all Training");

            IQueryable<Training> query = _context.Trainings
                .Include(c => c.Location);

            if (includeTalks)
            {
                query = query
                  .Include(c => c.Talks)
                  .ThenInclude(t => t.Speaker);
            }

            // Order It
            query = query.OrderByDescending(c => c.EventDate)
              .Where(c => c.EventDate.Date == dateTime.Date);

            return query.ToListAsync();
        }

        public Task<List<Training>> GetAllTrainingsAsync(bool includeTalks = false)
        {
            _logger.LogInformation($"Getting all Training");

            IQueryable<Training> query = _context.Trainings
                .Include(c => c.Location);

            if (includeTalks)
            {
                query = query
                  .Include(c => c.Talks)
                  .ThenInclude(t => t.Speaker);
            }

            // Order It
            query = query.OrderByDescending(c => c.EventDate);

            return query.ToListAsync();
        }

        public Task<Training> GetTrainingAsync(string code, bool includeTalks = false)
        {
            _logger.LogInformation($"Getting a Training for {code}");

            IQueryable<Training> query = _context.Trainings
                .Include(c => c.Location);

            if (includeTalks)
            {
                query = query.Include(c => c.Talks)
                  .ThenInclude(t => t.Speaker);
            }

            // Query It
            query = query.Where(c => c.Code == code);

            return query.FirstOrDefaultAsync();
        }

        public Task<List<Talk>> GetTalksByCodeAsync(string code, bool includeSpeakers = false)
        {
            _logger.LogInformation($"Getting all Talks for a Training");

            IQueryable<Talk> query = _context.Talks;

            if (includeSpeakers)
            {
                query = query
                  .Include(t => t.Speaker);
            }

            // Add Query
            query = query
              .Where(t => t.Training.Code == code)
              .OrderByDescending(t => t.Title);

            return query.ToListAsync();
        }

        public Task<Talk> GetTalkByCodeAsync(string code, int talkId, bool includeSpeakers = false)
        {
            _logger.LogInformation($"Getting all Talks for a Training");

            IQueryable<Talk> query = _context.Talks;

            if (includeSpeakers)
            {
                query = query
                  .Include(t => t.Speaker);
            }

            // Add Query
            query = query
              .Where(t => t.TalkId == talkId && t.Training.Code == code);

            return query.FirstOrDefaultAsync();
        }

        public Task<List<Speaker>> GetSpeakersByCodeAsync(string code)
        {
            _logger.LogInformation($"Getting all Speakers for a Training");

            IQueryable<Speaker> query = _context.Talks
              .Where(t => t.Training.Code == code)
              .Select(t => t.Speaker)
              .Where(s => s != null)
              .OrderBy(s => s.LastName)
              .Distinct();

            return query.ToListAsync();
        }

        public Task<List<Speaker>> GetAllSpeakersAsync()
        {
            _logger.LogInformation($"Getting Speaker");

            var query = _context.Speakers
              .OrderBy(t => t.LastName);

            return query.ToListAsync();
        }

        public Task<Speaker> GetSpeakerAsync(int speakerId)
        {
            _logger.LogInformation($"Getting Speaker");

            var query = _context.Speakers
              .Where(t => t.SpeakerId == speakerId);

            return query.FirstOrDefaultAsync();
        }
    }
}
