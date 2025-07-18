using Application.DTOs.Leads;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeadsController : ControllerBase
    {
        private readonly IWorkshopService _workshopService;

        public LeadsController(IWorkshopService workshopService)
        {
            _workshopService = workshopService;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LeadRequestDto request)
        {
            var result = await _workshopService.GetActiveWorkshopsAsync();
            return Ok(result);
        }


    }
}
