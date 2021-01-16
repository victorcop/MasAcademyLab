using AutoMapper;
using MasAcademyLab.Domain;
using MasAcademyLab.Service.Models;

namespace MasAcademyLab.Service.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Camp, CampModel>().ReverseMap();
        }           
    }
}
