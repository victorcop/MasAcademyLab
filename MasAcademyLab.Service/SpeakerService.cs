using MasAcademyLab.Data;
using MasAcademyLab.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MasAcademyLab.Service
{
    public class SpeakerService : ISpeakerService
    {
        private readonly MasAcademyLabContext _masAcademyLabContext;

        public SpeakerService(MasAcademyLabContext masAcademyLabContext)
        {
            _masAcademyLabContext = masAcademyLabContext;
        }

        public IEnumerable<Speaker> GetSpeakers()
        {
            try
            {
                return _masAcademyLabContext.Speakers.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
