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

        public LeadsController(ILeadService leadService)
        {
            _leadService = leadService;
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
            var result = await _leadService.AddAsync(request);
            return Ok(result);
        }


    }
}
