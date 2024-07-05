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
    [Route("api/tipos-distribuciones")]
    [ApiController]
    [TokenValidationFilter]

    public class TiposDistribucionesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public TiposDistribucionesController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<TipoDistribucionDTO>>> GetAll()
        {
            var tipo = await context.TiposDistribuciones
                .ToListAsync();

            if (!tipo.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<TipoDistribucionDTO>>(tipo));
        }

    }
}