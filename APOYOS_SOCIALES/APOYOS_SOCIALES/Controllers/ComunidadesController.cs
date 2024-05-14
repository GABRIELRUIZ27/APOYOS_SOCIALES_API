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
    [Route("api/comunidades")]
    [ApiController]
    [TokenValidationFilter]

    public class ComunidadesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ComunidadesController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<ComunidadDTO>>> GetAll()
        {
            var comunidades = await context.Comunidades
                .ToListAsync();

            if (!comunidades.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<ComunidadDTO>>(comunidades));
        }

    }
}