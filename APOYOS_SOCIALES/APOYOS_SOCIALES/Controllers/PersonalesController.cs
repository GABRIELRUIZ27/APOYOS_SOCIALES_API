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
    [Route("api/personal")]
    [ApiController]
    [TokenValidationFilter]

    public class PersonalesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public PersonalesController(
            ApplicationDbContext context,
            IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("obtener-por-id/{id:int}")]
        public async Task<ActionResult<PersonalDTO>> GetById(int id)
        {
            var personal = await context.Personales
                .Include(s => s.Area)
                .Include(s => s.Cargo)
                .Include(s => s.Genero)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (personal == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<PersonalDTO>(personal));
        }

        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<PersonalDTO>>> GetAll()
        {
            try
            {
                var personal = await context.Personales
                    .Include(s => s.Area)
                    .Include(s => s.Cargo)
                    .Include(s => s.Genero)
                    .OrderByDescending(u => u.Salario)
                    .ToListAsync();

                if (!personal.Any())
                {
                    return NotFound();
                }

                var personalDTO = mapper.Map<List<PersonalDTO>>(personal);

                return Ok(personalDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500);
            }
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Post(PersonalDTO dto)
        {

            var personal = mapper.Map<Personal>(dto);
            personal.Area = await context.Areas.SingleOrDefaultAsync(s => s.Id == dto.Area.Id);
            personal.Genero = await context.Generos.SingleOrDefaultAsync(s => s.Id == dto.Genero.Id);
            personal.Cargo = await context.Cargos.SingleOrDefaultAsync(s => s.Id == dto.Cargo.Id);

            context.Personales.Add(personal);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("eliminar/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var personal = await context.Personales.FindAsync(id);

            if (personal == null)
            {
                return NotFound();
            }

            context.Personales.Remove(personal);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("actualizar/{id:int}")]
        public async Task<ActionResult> Put(int id, PersonalDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("El ID de la ruta y el ID del objeto no coinciden");
            }

            var personal = await context.Personales.FindAsync(id);

            if (personal == null)
            {
                return NotFound();
            }

            mapper.Map(dto, personal);
            personal.Area = await context.Areas.SingleOrDefaultAsync(s => s.Id == dto.Area.Id);
            personal.Genero = await context.Generos.SingleOrDefaultAsync(s => s.Id == dto.Genero.Id);
            personal.Cargo = await context.Cargos.SingleOrDefaultAsync(s => s.Id == dto.Cargo.Id);

            context.Update(personal);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonalExists(id))
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

        private bool PersonalExists(int id)
        {
            return context.Personales.Any(e => e.Id == id);
        }

    }
}