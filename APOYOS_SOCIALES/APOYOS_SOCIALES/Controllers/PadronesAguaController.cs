using AutoMapper;
using APOYOS_SOCIALES.DTOs;
using APOYOS_SOCIALES.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APOYOS_SOCIALES.Filters;
using APOYOS_SOCIALES.Services;
using APOYOS_SOCIALES;

namespace APOYOS_SOCIALES.Controllers
{
    [Authorize]
    [Route("api/padron-agua")]
    [ApiController]
    [TokenValidationFilter]

    public class PadronesAguaController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public PadronesAguaController(
            ApplicationDbContext context,
            IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("obtener-por-id/{id:int}")]
        public async Task<ActionResult<PadronAguaDTO>> GetById(int id)
        {
            var padron = await context.PadronesAgua
                .Include(s => s.Agua)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (padron == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<PadronAguaDTO>(padron));
        }

        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<PadronAguaDTO>>> GetAll()
        {
            try
            {
                var padron = await context.PadronesAgua
                    .Include(s => s.Agua)
                    .ThenInclude(c => c.Comunidad)
                    .ToListAsync();

                if (!padron.Any())
                {
                    return NotFound();
                }

                var padronDTO = mapper.Map<List<PadronAguaDTO>>(padron);

                return Ok(padronDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500);
            }
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Post(PadronAguaDTO dto)
        {

            var padron = mapper.Map<PadronAgua>(dto);
            padron.Agua = await context.AguaPotables.SingleOrDefaultAsync(s => s.Id == dto.Agua.Id);

            context.PadronesAgua.Add(padron);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("eliminar/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var padron = await context.PadronesAgua.FindAsync(id);

            if (padron == null)
            {
                return NotFound();
            }

            context.PadronesAgua.Remove(padron);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("actualizar/{id:int}")]
        public async Task<ActionResult> Put(int id, PadronAguaDTO dto)
        {
            // Verifica que el ID de la ruta coincida con el ID del objeto DTO
            if (id != dto.Id)
            {
                return BadRequest("El ID de la ruta y el ID del objeto no coinciden");
            }

            // Busca el registro completo en la base de datos, incluyendo la relación con Agua
            var padron = await context.PadronesAgua
                .Include(p => p.Agua) // Incluir el objeto Agua
                .FirstOrDefaultAsync(p => p.Id == id);

            // Verifica si se encontró el registro
            if (padron == null)
            {
                return NotFound();
            }

            // Actualiza los campos de PadronAgua
            padron.Folio = dto.Folio; // Actualiza Folio
            padron.Periodo = dto.Periodo; // Actualiza Periodo
            padron.Pago = dto.Pago ?? false; ;

            // Actualiza los campos en AguaPotable
            if (padron.Agua != null)
            {
                padron.Agua.Folio = dto.Folio; // Actualiza el Folio en AguaPotable
                padron.Agua.Periodo = dto.Periodo; // Actualiza el Periodo en AguaPotable
            }

            // Marca la entidad para actualización
            context.Update(padron);

            // Guarda los cambios en la base de datos
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PadronExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw; // Lanza la excepción si hay un problema en la actualización
                }
            }

            return NoContent(); // Indica que la actualización fue exitosa
        }

        private bool PadronExists(int id)
        {
            return context.PadronesAgua.Any(e => e.Id == id);
        }

    }
}