using Application.DTOs.Leads;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeadsController : ControllerBase
    {
        private readonly IWorkshopService _workshopService;
        private readonly ILeadRepository _leadRepository;

        public LeadsController(IWorkshopService workshopService, ILeadRepository leadRepository)
        {
            _workshopService = workshopService;
            _leadRepository = leadRepository;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LeadRequestDto request)
        {
            var result = await _workshopService.GetActiveWorkshopsAsync();

            var lead = new Lead
            {
                PlaceId = 2,
                AppointmentAt = DateTime.Parse("2025-07-25T10:30:00Z"),
                ServiceType = "cambio_aceite",
                Contact = new Contact
                {
                    Name = "Juan Pérez",
                    Email = "juan.perez@example.com",
                    Phone = "+54 11 5555-5555"
                },
                Vehicle = new Vehicle
                {
                    Make = "Toyota",
                    Model = "Corolla",
                    Year = 2022,
                    LicensePlate = "ABC123"
                }
            };

            await _leadRepository.AddAsync(lead);
            var leads = await _leadRepository.GetAllAsync();
            return Ok(result);
        }


    }
}
