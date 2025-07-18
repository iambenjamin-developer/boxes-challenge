using Application.DTOs;
using Application.DTOs.Leads;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Movies
            CreateMap<Movie, MovieDto>();

            CreateMap<LeadRequestDto, Lead>();
            CreateMap<Lead, LeadResponseDto>();

            CreateMap<Contact, ContactDto>().ReverseMap();
            CreateMap<Vehicle, VehicleDto>().ReverseMap();
            CreateMap<Lead, LeadResponseDto>().ReverseMap();

            CreateMap<LeadRequestDto, Lead>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Lead, LeadResponseDto>().ReverseMap();
        }
    }
}
