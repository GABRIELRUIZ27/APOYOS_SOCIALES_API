using AutoMapper;
using APOYOS_SOCIALES.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APOYOS_SOCIALES.Entities;
using APOYOS_SOCIALES.Filters;

namespace APOYOS_SOCIALES.Controllers
{
    [Authorize]
    [Route("api/tipos-incidencias")]
    [ApiController]
    [TokenValidationFilter]

    public class TiposIncidenciasController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public TiposIncidenciasController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper; 
        }

        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<TipoIncidenciaDTO>>> GetAll()
        {
            try
            {
                var tipo = await context.TiposIncidencias.ToListAsync();

                if (!tipo.Any())
                {
                    return NotFound();
                }

                var tiposDTO = mapper.Map<List<TipoIncidenciaDTO>>(tipo);
                
                return Ok(tiposDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500);
            }
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Post(TipoIncidenciaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existeTipo = await context.TiposIncidencias.AnyAsync(n => n.Nombre == dto.Nombre);

            if (existeTipo)
            {
                return Conflict();
            }

            var tipo = mapper.Map<TipoIncidencia>(dto);

            context.Add(tipo);

            try
            {
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("eliminar/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var tipo = await context.TiposIncidencias.FindAsync(id);

            if (tipo == null)
            {
                return NotFound();
            }

            context.TiposIncidencias.Remove(tipo);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("actualizar/{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] TipoIncidenciaDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("El ID de la ruta y el ID del objeto no coinciden");
            }

            var tipo = await context.TiposIncidencias.FindAsync(id);

            if (tipo == null)
            {
                return NotFound();
            }

            mapper.Map(dto, tipo);

            context.Update(tipo);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoExists(id))
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

        private bool TipoExists(int id)
        {
            return context.TiposIncidencias.Any(e => e.Id == id);
        }
    }
}