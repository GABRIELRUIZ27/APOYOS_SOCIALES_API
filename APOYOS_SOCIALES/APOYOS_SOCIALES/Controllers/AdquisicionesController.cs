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
    [Route("api/adquisiciones")]
    [ApiController]
    [TokenValidationFilter]

    public class AdquisicionesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public AdquisicionesController(
            ApplicationDbContext context,
            IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("obtener-por-id/{id:int}")]
        public async Task<ActionResult<AdquisicionDTO>> GetById(int id)
        {
            var adquisicion = await context.Adquisiciones
                .Include(s => s.Area)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (adquisicion == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<AdquisicionDTO>(adquisicion));
        }

        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<AdquisicionDTO>>> GetAll()
        {
            try
            {
                var adquisicion = await context.Adquisiciones
                    .Include(s => s.Area)
                    .OrderBy(f => f.Id)
                    .ToListAsync();

                if (!adquisicion.Any())
                {
                    return NotFound();
                }

                var adquisicionDTO = mapper.Map<List<AdquisicionDTO>>(adquisicion);

                return Ok(adquisicionDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500);
            }
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Post(AdquisicionDTO dto)
        {

            var adquisicion = mapper.Map<Adquisicion>(dto);
            adquisicion.Area = await context.Areas.SingleOrDefaultAsync(s => s.Id == dto.Area.Id);

            context.Adquisiciones.Add(adquisicion);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("eliminar/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var adquisicion = await context.Adquisiciones.FindAsync(id);

            if (adquisicion == null)
            {
                return NotFound();
            }

            context.Adquisiciones.Remove(adquisicion);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("actualizar/{id:int}")]
        public async Task<ActionResult> Put(int id, AdquisicionDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("El ID de la ruta y el ID del objeto no coinciden");
            }

            var adquisicion = await context.Adquisiciones.FindAsync(id);

            if (adquisicion == null)
            {
                return NotFound();
            }

            mapper.Map(dto, adquisicion);
            adquisicion.Area = await context.Areas.SingleOrDefaultAsync(s => s.Id == dto.Area.Id);

            context.Update(adquisicion);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdquisicionExists(id)) 
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

        private bool AdquisicionExists(int id)
        {
            return context.Adquisiciones.Any(e => e.Id == id);
        }

    }
}