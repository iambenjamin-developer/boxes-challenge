using Application.DTOs.Leads;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeadsController : ControllerBase
    {
        private readonly ILeadService _leadService;
        private readonly IWorkshopService _workshopService;

        public LeadsController(ILeadService leadService, IWorkshopService workshopService)
        {
            _leadService = leadService;
            _workshopService = workshopService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _leadService.GetAllAsync();
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LeadRequestDto request)
        {
            bool workshopExists = await _workshopService.ExistsAsync(request.PlaceId);
            if (!workshopExists)
            {
                return BadRequest($"El taller con ID:{request.PlaceId} no existe o no está activo");
            }

            var result = await _leadService.AddAsync(request);
            return Ok(result);
        }


    }
}
