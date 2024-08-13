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
    [Route("api/apoyos")]
    [ApiController]
    [TokenValidationFilter]

    public class ApoyosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IAlmacenadorImagenes almacenadorImagenes;
        private readonly string directorioApoyos = "apoyos";

        public ApoyosController( 
            ApplicationDbContext context,
            IMapper mapper,
            IAlmacenadorImagenes almacenadorImagenes)
        {
            this.context = context;
            this.mapper = mapper;
            this.almacenadorImagenes = almacenadorImagenes;
        }

        [HttpGet("obtener-por-id/{id:int}")]
        public async Task<ActionResult<ApoyoDTO>> GetById(int id)
        {
            var apoyo = await context.Apoyos
                .Include(b => b.Comunidad)
                .Include(s => s.Area)
                .Include(s => s.Genero)
                .Include(s => s.ProgramaSocial)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (apoyo == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<ApoyoDTO>(apoyo));
        }

        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<ApoyoDTO>>> GetAll()
        {
            try
            {
                var apoyo = await context.Apoyos
                    .Include(v => v.Comunidad)
                    .Include(s => s.Area)
                    .Include(s => s.Genero)
                    .Include(s => s.ProgramaSocial)
                    .ToListAsync();

                if (!apoyo.Any())
                {
                    return NotFound();
                }

                var apoyosDTO = mapper.Map<List<ApoyoDTO>>(apoyo);

                return Ok(apoyosDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500);
            }
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Post(ApoyoDTO dto)
        {
            if (!string.IsNullOrEmpty(dto.ImagenBase64))
            {
                dto.Foto = await almacenadorImagenes.GuardarImagen(dto.ImagenBase64, directorioApoyos);
            }


            var apoyo = mapper.Map<Apoyo>(dto);
            apoyo.Comunidad = await context.Comunidades.SingleOrDefaultAsync(s => s.Id == dto.Comunidad.Id);
            apoyo.Area = await context.Areas.SingleOrDefaultAsync(s => s.Id == dto.Area.Id);
            apoyo.Genero = await context.Generos.SingleOrDefaultAsync(s => s.Id == dto.Genero.Id);
            apoyo.ProgramaSocial = await context.Programassociales.SingleOrDefaultAsync(s => s.Id == dto.ProgramaSocial.Id);

            context.Apoyos.Add(apoyo);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("eliminar/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var apoyos = await context.Apoyos.FindAsync(id);

            if (apoyos == null)
            {
                return NotFound();
            }

            context.Apoyos.Remove(apoyos);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("actualizar/{id:int}")]
        public async Task<ActionResult> Put(int id, ApoyoDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("El ID de la ruta y el ID del objeto no coinciden");
            }

            var apoyos = await context.Apoyos.FindAsync(id);

            if (apoyos == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(dto.ImagenBase64))
            {
                dto.Foto = await almacenadorImagenes.GuardarImagen(dto.ImagenBase64, directorioApoyos);
            }
            else
            {
                dto.Foto = apoyos.Foto;
            }

            mapper.Map(dto, apoyos);
            apoyos.Comunidad = await context.Comunidades.SingleOrDefaultAsync(c => c.Id == dto.Comunidad.Id);
            apoyos.Area = await context.Areas.SingleOrDefaultAsync(s => s.Id == dto.Area.Id);
            apoyos.Genero = await context.Generos.SingleOrDefaultAsync(s => s.Id == dto.Genero.Id);
            apoyos.ProgramaSocial = await context.Programassociales.SingleOrDefaultAsync(s => s.Id == dto.ProgramaSocial.Id);

            context.Update(apoyos);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApoyosExists(id))
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

        private bool ApoyosExists(int id)
        {
            return context.Apoyos.Any(e => e.Id == id);
        }

    }
}