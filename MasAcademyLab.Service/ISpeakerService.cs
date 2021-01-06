using MasAcademyLab.Domain;
using System.Collections.Generic;

namespace MasAcademyLab.Service
{
    public interface ISpeakerService
    {
        IEnumerable<Speaker> GetSpeakers();
    }
}