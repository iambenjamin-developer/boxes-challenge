using Application.DTOs.Workshops;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkshopsController : ControllerBase
    {
        private readonly IWorkshopService _workshopService;

        public WorkshopsController(IWorkshopService workshopService)
        {
            _workshopService = workshopService;
        }


        /// <summary>
        /// Obtiene la lista de todos los workshops.
        /// </summary>
        /// <remarks>
        /// Este endpoint devuelve todos los workshops que se consultan por la API externa.
        /// </remarks>
        /// <response code="200">Lista de workshops obtenida correctamente.</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<WorkshopDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<WorkshopDto>>> GetAll()
        {
            var result = await _workshopService.GetActiveWorkshopsAsync();
            return Ok(result);
        }

    }
}
