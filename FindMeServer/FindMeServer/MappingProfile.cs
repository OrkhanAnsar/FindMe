using ApplicationCore.DataTransferObjects;
using ApplicationCore.Models;
using AutoMapper;

namespace FindMeServer
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Lost, LostDTO>();
            CreateMap<Institution, InstitutionDTO>();
            CreateMap<City, CityDTO>();
            CreateMap<InstitutionType, InstitutionTypeDTO>();
        }
    }
}