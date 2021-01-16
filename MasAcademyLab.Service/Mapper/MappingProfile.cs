using AutoMapper;
using MasAcademyLab.Domain;
using MasAcademyLab.Service.Models;

namespace MasAcademyLab.Service.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Speaker, SpeakerModel>().ReverseMap();
            CreateMap<Talk, TalkModel>().ReverseMap();
            CreateMap<Camp, CampModel>().ReverseMap();
        }           
    }
}
