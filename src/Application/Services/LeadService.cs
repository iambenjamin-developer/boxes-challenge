using Application.DTOs.Leads;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Application.Services
{
    public class LeadService : ILeadService
    {
        private readonly IWorkshopService _workshopService;
        private readonly ILeadRepository _leadRepository;
        private readonly IMapper _mapper;

        public LeadService(IWorkshopService workshopService, ILeadRepository leadRepository, IMapper mapper)
        {
            _workshopService = workshopService;
            _leadRepository = leadRepository;
            _mapper = mapper;
        }


        public async Task<List<LeadResponseDto>> GetAllAsync()
        {
            var entities = await _leadRepository.GetAllAsync();

            return _mapper.Map<List<Lead>, List<LeadResponseDto>>(entities);
        }


        public async Task<LeadResponseDto> AddAsync(LeadRequestDto request)
        {
            var workshops = await _workshopService.GetActiveWorkshopsAsync();
            var entity = _mapper.Map<LeadRequestDto, Lead>(request);

            entity = await _leadRepository.AddAsync(entity);

            return _mapper.Map<Lead, LeadResponseDto>(entity);
        }

    }
}
