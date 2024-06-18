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
    [Route("api/incidencias")]
    [ApiController]
    [TokenValidationFilter]

    public class IncidenciasController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IAlmacenadorImagenes almacenadorImagenes;
        private readonly string directorioIncidencias = "incidencias";

        public IncidenciasController( 
            ApplicationDbContext context,
            IMapper mapper,
            IAlmacenadorImagenes almacenadorImagenes)
        {
            this.context = context;
            this.mapper = mapper;
            this.almacenadorImagenes = almacenadorImagenes;
        }

        [HttpGet("obtener-por-id/{id:int}")]
        public async Task<ActionResult<IncidenciaDTO>> GetById(int id)
        {
            var incidencia = await context.Incidencias
                .Include(b => b.Comunidad)
                .Include(s => s.Area)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (incidencia == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<IncidenciaDTO>(incidencia));
        }

        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<IncidenciaDTO>>> GetAll()
        {
            try
            {
                var incidencia = await context.Incidencias
                    .Include(v => v.Comunidad)
                    .Include(s => s.Area)
                    .ToListAsync();

                if (!incidencia.Any())
                {
                    return NotFound();
                }

                var incidenciasDTO = mapper.Map<List<IncidenciaDTO>>(incidencia);

                return Ok(incidenciasDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500);
            }
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Post(IncidenciaDTO dto)
        {
            if (!string.IsNullOrEmpty(dto.ImagenBase64))
            {
                dto.Foto = await almacenadorImagenes.GuardarImagen(dto.ImagenBase64, directorioIncidencias);
            }


            var incidencia = mapper.Map<Incidencia>(dto);
            incidencia.Comunidad = await context.Comunidades.SingleOrDefaultAsync(s => s.Id == dto.Comunidad.Id);
            incidencia.Area = await context.Areas.SingleOrDefaultAsync(s => s.Id == dto.Area.Id);

            context.Incidencias.Add(incidencia);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("eliminar/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var incidencia = await context.Incidencias.FindAsync(id);

            if (incidencia == null)
            {
                return NotFound();
            }

            context.Incidencias.Remove(incidencia);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("actualizar/{id:int}")]
        public async Task<ActionResult> Put(int id, IncidenciaDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("El ID de la ruta y el ID del objeto no coinciden");
            }

            var incidencia = await context.Incidencias.FindAsync(id);

            if (incidencia == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(dto.ImagenBase64))
            {
                dto.Foto = await almacenadorImagenes.GuardarImagen(dto.ImagenBase64, directorioIncidencias);
            }
            else
            {
                dto.Foto = incidencia.Foto;
            }

            mapper.Map(dto, incidencia);
            incidencia.Comunidad = await context.Comunidades.SingleOrDefaultAsync(c => c.Id == dto.Comunidad.Id);
            incidencia.Area = await context.Areas.SingleOrDefaultAsync(s => s.Id == dto.Area.Id);

            context.Update(incidencia);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IncidenciaExists(id))
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

        private bool IncidenciaExists(int id)
        {
            return context.Apoyos.Any(e => e.Id == id);
        }

    }
}