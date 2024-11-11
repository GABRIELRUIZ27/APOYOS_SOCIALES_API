using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APOYOS_SOCIALES.Filters;
using APOYOS_SOCIALES.DTOs;
using APOYOS_SOCIALES;

namespace simpatizantes_api.Controllers
{
    [Authorize]
    [Route("api/tipo-servicio")]
    [ApiController]
    [TokenValidationFilter]

    public class TipoServiciosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public TipoServiciosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<TipoServicioDTO>>> GetAll()
        {
            var comunidades = await context.TipoServicios
                .ToListAsync();

            if (!comunidades.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<TipoServicioDTO>>(comunidades));
        }

    }
}