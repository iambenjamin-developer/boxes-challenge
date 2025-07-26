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
        /// Obtiene un lead específico por su ID.
        /// </summary>
        /// <param name="id">ID del lead a obtener.</param>
        /// <remarks>
        /// Este endpoint devuelve un lead específico basado en su ID único.
        /// </remarks>
        /// <response code="200">Lead obtenido correctamente.</response>
        /// <response code="404">Lead no encontrado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(LeadResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LeadResponseDto>> GetById(long id)
        {
            var result = await _leadService.GetByIdAsync(id);

            if (result == null)
            {
                return NotFound();
            }

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
            bool workshopExists = await _workshopService.ExistsAsync(request.PlaceId);
            if (!workshopExists)
            {
                return BadRequest("El PlaceId no corresponde a un taller que exista o esté activo.");
            }

            var result = await _leadService.AddAsync(request);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
    }
}
