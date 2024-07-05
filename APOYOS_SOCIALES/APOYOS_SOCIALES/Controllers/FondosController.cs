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
    [Route("api/fondos")]
    [ApiController]
    [TokenValidationFilter]

    public class FondosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public FondosController(
            ApplicationDbContext context,
            IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("obtener-por-id/{id:int}")]
        public async Task<ActionResult<FondoDTO>> GetById(int id)
        {
            var fondo = await context.Fondos
                .Include(s => s.TipoDistribucion)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (fondo == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<FondoDTO>(fondo));
        }

        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<FondoDTO>>> GetAll()
        {
            try
            {
                var fondo = await context.Fondos
                    .Include(s => s.TipoDistribucion)
                    .OrderBy(f => f.Id)
                    .ToListAsync();

                if (!fondo.Any())
                {
                    return NotFound();
                }

                var fondoDTO = mapper.Map<List<FondoDTO>>(fondo);

                return Ok(fondoDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500);
            }
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Post(FondoDTO dto)
        {

            var fondo = mapper.Map<Fondo>(dto);
            fondo.TipoDistribucion = await context.TiposDistribuciones.SingleOrDefaultAsync(s => s.Id == dto.TipoDistribucion.Id);

            context.Fondos.Add(fondo);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("eliminar/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var fondo = await context.Fondos.FindAsync(id);

            if (fondo == null)
            {
                return NotFound();
            }

            context.Fondos.Remove(fondo);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("actualizar/{id:int}")]
        public async Task<ActionResult> Put(int id, FondoDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("El ID de la ruta y el ID del objeto no coinciden");
            }

            var fondo = await context.Fondos.FindAsync(id);

            if (fondo == null)
            {
                return NotFound();
            }

            mapper.Map(dto, fondo);
            fondo.TipoDistribucion = await context.TiposDistribuciones.SingleOrDefaultAsync(s => s.Id == dto.TipoDistribucion.Id);

            context.Update(fondo);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FondoExists(id))
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

        private bool FondoExists(int id)
        {
            return context.Fondos.Any(e => e.Id == id);
        }

    }
}