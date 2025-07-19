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

        /// <summary>
        /// Obtiene la lista de todos los leads registrados.
        /// </summary>
        /// <remarks>
        /// Este endpoint devuelve todos los leads almacenados en el sistema.
        /// </remarks>
        /// <response code="200">Lista de leads obtenida correctamente.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<LeadResponseDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<LeadResponseDto>>> GetAll()
        {
            var result = await _leadService.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// Crea un nuevo lead.
        /// </summary>
        /// <param name="request">Datos del lead a crear.</param>
        /// <remarks>
        /// Crea un lead en la base de datos. Si la validación falla, se retorna un error 400 con los detalles.
        /// </remarks>
        /// <response code="201">Lead creado correctamente.</response>
        /// <response code="400">Datos inválidos para el lead.</response>
        [HttpPost]
        [ProducesResponseType(typeof(LeadResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LeadResponseDto>> Create([FromBody] LeadRequestDto request)
        {
            var created = await _leadService.AddAsync(request);
            return CreatedAtAction(nameof(GetAll), null, created);
        }
    }
}
