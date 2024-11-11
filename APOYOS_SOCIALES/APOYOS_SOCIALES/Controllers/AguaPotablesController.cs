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
    [Route("api/agua-potable")]
    [ApiController]
    [TokenValidationFilter]

    public class AguaPotablesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public AguaPotablesController(
            ApplicationDbContext context,
            IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("obtener-por-id/{id:int}")]
        public async Task<ActionResult<AguaPotableDTO>> GetById(int id)
        {
            var agua = await context.AguaPotables
                .Include(c => c.Comunidad)
                .Include(s => s.TipoServicio)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (agua == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<AguaPotableDTO>(agua));
        }

        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<AguaPotableDTO>>> GetAll()
        {
            try
            {
                var agua = await context.AguaPotables
                    .Include(c => c.Comunidad)
                    .Include(s => s.TipoServicio)
                    .ToListAsync();

                if (!agua.Any())
                {
                    return NotFound();
                }

                var aguaDTO = mapper.Map<List<AguaPotableDTO>>(agua);

                return Ok(aguaDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500);
            }
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Post(AguaPotableDTO dto)
        {

            var agua = mapper.Map<AguaPotable>(dto);
            agua.Comunidad = await context.Comunidades.SingleOrDefaultAsync(s => s.Id == dto.Comunidad.Id);
            agua.TipoServicio = await context.TipoServicios.SingleOrDefaultAsync(s => s.Id == dto.TipoServicio.Id);

            context.AguaPotables.Add(agua);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("eliminar/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var agua = await context.AguaPotables.FindAsync(id);

            if (agua == null)
            {
                return NotFound();
            }

            context.AguaPotables.Remove(agua);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("actualizar/{id:int}")]
        public async Task<ActionResult> Put(int id, AguaPotableDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("El ID de la ruta y el ID del objeto no coinciden");
            }

            var agua = await context.AguaPotables.FindAsync(id);

            if (agua == null)
            {
                return NotFound();
            }

            mapper.Map(dto, agua);

            // Asegúrate de que los campos Folio y Periodo se actualicen
            agua.Folio = dto.Folio;
            agua.Periodo = dto.Periodo;

            agua.Comunidad = await context.Comunidades.SingleOrDefaultAsync(s => s.Id == dto.Comunidad.Id);
            agua.TipoServicio = await context.TipoServicios.SingleOrDefaultAsync(s => s.Id == dto.TipoServicio.Id);

            context.Update(agua);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AguaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool AguaExists(int id)
        {
            return context.AguaPotables.Any(e => e.Id == id);
        }

    }
}