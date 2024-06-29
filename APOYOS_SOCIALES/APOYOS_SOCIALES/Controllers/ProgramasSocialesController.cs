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
    [Route("api/programas")]
    [ApiController]
    [TokenValidationFilter]

    public class ProgramasSocialesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;


        public ProgramasSocialesController(
            ApplicationDbContext context,
            IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("obtener-por-id/{id:int}")]
        public async Task<ActionResult<ProgramaSocialDTO>> GetById(int id)
        {
            var programas = await context.Programassociales
                .Include(s => s.Area)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (programas == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<ProgramaSocialDTO>(programas));
        }

        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<ProgramaSocialDTO>>> GetAll()
        {
            try
            {
                var programas = await context.Programassociales
                    .Include(s => s.Area)
                    .ToListAsync();

                if (!programas.Any())
                {
                    return NotFound();
                }

                var programasDTO = mapper.Map<List<ProgramaSocialDTO>>(programas);

                return Ok(programasDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500);
            }
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Post(ProgramaSocialDTO dto)
        {

            var programas = mapper.Map<ProgramaSocial>(dto);
            programas.Area = await context.Areas.SingleOrDefaultAsync(s => s.Id == dto.Area.Id);

            context.Programassociales.Add(programas);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("eliminar/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var programas = await context.Programassociales.FindAsync(id);

            if (programas == null)
            {
                return NotFound();
            }

            context.Programassociales.Remove(programas);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("actualizar/{id:int}")]
        public async Task<ActionResult> Put(int id, ProgramaSocialDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("El ID de la ruta y el ID del objeto no coinciden");
            }

            var programas = await context.Programassociales.FindAsync(id);

            if (programas == null)
            {
                return NotFound();
            }

            mapper.Map(dto, programas);
            programas.Area = await context.Areas.SingleOrDefaultAsync(s => s.Id == dto.Area.Id);

            context.Update(programas);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProgramaExists(id))
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

        private bool ProgramaExists(int id)
        {
            return context.Programassociales.Any(e => e.Id == id);
        }

    }
}