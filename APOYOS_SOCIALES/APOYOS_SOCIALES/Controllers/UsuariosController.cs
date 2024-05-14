using AutoMapper;
using APOYOS_SOCIALES.DTOs;
using APOYOS_SOCIALES.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using APOYOS_SOCIALES.Filters;
using APOYOS_SOCIALES;
using APOYOSSOCIALES.DTOs;

namespace APOYOS_SOCIALES.Controllers
{
    [Authorize]
    [Route("api/usuarios")]
    [ApiController]
    [TokenValidationFilter]
    public class UsuariosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public UsuariosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("obtener-por-id/{id:int}")]
        public async Task<ActionResult<UsuarioDTO>> GetById(int id)
        {
            var usuario = await context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<UsuarioDTO>(usuario));
        }


        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<UsuarioDTO>>> GetAll()
        {
            var usuarios = await context.Usuarios
                .Include(i => i.Rol)
                .Include(i => i.Area)
                .OrderBy(u => u.Id)
                .ToListAsync();

            if (!usuarios.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<UsuarioDTO>>(usuarios));
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Post(UsuarioDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var usuario = mapper.Map<Usuario>(dto);
                usuario.Rol = await context.Rols.SingleOrDefaultAsync(r => r.Id == dto.Rol.Id);
                usuario.AreaId = null;
                usuario.Area = null;

                // Si es  empleado
                if (dto.Rol.Id == 2)
                {

                    if (await context.Usuarios.AnyAsync(c => c.Area.Id == dto.Area.Id))
                    {
                        return Conflict();
                    }

                    usuario.Area = await context.Areas.SingleOrDefaultAsync(o => o.Id == dto.Area.Id);
                }

                context.Add(usuario);
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error interno del servidor al guardar al usuario.", details = ex.Message });
            }
        }

        [HttpDelete("eliminar/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var usuario = await context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            context.Usuarios.Remove(usuario);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("actualizar/{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] UsuarioDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("El ID de la ruta y el ID del objeto no coinciden");
            }

            var usuario = await context.Usuarios.FindAsync(id);

            var currentAreaId = usuario?.AreaId;

            if (usuario == null)
            {
                return NotFound();
            }

            // Mapea los datos del DTO al usuario existente
            mapper.Map(dto, usuario);
            usuario.Rol = await context.Rols.SingleOrDefaultAsync(r => r.Id == dto.Rol.Id);
            usuario.AreaId = null;
            usuario.Area = null;

            // Si es  empleado
            if (dto.Rol.Id == 2)
            {

                usuario.Area = await context.Areas.SingleOrDefaultAsync(o => o.Id == dto.Area.Id);
                usuario.AreaId = usuario.Area.Id;
            }

            context.Update(usuario);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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

        private bool UsuarioExists(int id)
        {
            return context.Usuarios.Any(e => e.Id == id);
        }

    }
}