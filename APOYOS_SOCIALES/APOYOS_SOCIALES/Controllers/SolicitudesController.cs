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
    [Route("api/solicitudes")]
    [ApiController]
    [TokenValidationFilter]

    public class SolicitudesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public SolicitudesController(
            ApplicationDbContext context,
            IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("obtener-por-id/{id:int}")]
        public async Task<ActionResult<SolicitudDTO>> GetById(int id)
        {
            var solicitud = await context.Solicitudes
                .Include(b => b.Comunidad)
                .Include(s => s.Area)
                .Include(s => s.Genero)
                .Include(s => s.ProgramaSocial)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (solicitud == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<SolicitudDTO>(solicitud));
        }

        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<SolicitudDTO>>> GetAll()
        {
            try
            {
                var solicitud = await context.Solicitudes
                    .Include(v => v.Comunidad)
                    .Include(s => s.Area)
                    .Include(s => s.Genero)
                    .Include(s => s.ProgramaSocial)
                    .ToListAsync();

                if (!solicitud.Any())
                {
                    return NotFound();
                }

                var solicitudDTO = mapper.Map<List<SolicitudDTO>>(solicitud);

                return Ok(solicitudDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500);
            }
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Post(SolicitudDTO dto)
        {

            var solicitud = mapper.Map<Solicitud>(dto);
            solicitud.Comunidad = await context.Comunidades.SingleOrDefaultAsync(s => s.Id == dto.Comunidad.Id);
            solicitud.Area = await context.Areas.SingleOrDefaultAsync(s => s.Id == dto.Area.Id);
            solicitud.Genero = await context.Generos.SingleOrDefaultAsync(s => s.Id == dto.Genero.Id);
            solicitud.ProgramaSocial = await context.Programassociales.SingleOrDefaultAsync(s => s.Id == dto.ProgramaSocial.Id);

            context.Solicitudes.Add(solicitud);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("eliminar/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var solicitud = await context.Solicitudes.FindAsync(id);

            if (solicitud == null)
            {
                return NotFound();
            }

            context.Solicitudes.Remove(solicitud);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("actualizar/{id:int}")]
        public async Task<ActionResult> Put(int id, SolicitudDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("El ID de la ruta y el ID del objeto no coinciden");
            }

            var solicitud = await context.Solicitudes.FindAsync(id);

            if (solicitud == null)
            {
                return NotFound();
            }

            mapper.Map(dto, solicitud);
            solicitud.Comunidad = await context.Comunidades.SingleOrDefaultAsync(c => c.Id == dto.Comunidad.Id);
            solicitud.Area = await context.Areas.SingleOrDefaultAsync(s => s.Id == dto.Area.Id);
            solicitud.Genero = await context.Generos.SingleOrDefaultAsync(s => s.Id == dto.Genero.Id);
            solicitud.ProgramaSocial = await context.Programassociales.SingleOrDefaultAsync(s => s.Id == dto.ProgramaSocial.Id);

            context.Update(solicitud);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SolicitudExists(id))
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

        private bool SolicitudExists(int id)
        {
            return context.Solicitudes.Any(e => e.Id == id);
        }

    }
}