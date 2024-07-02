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
    [Route("api/cargos")]
    [ApiController]
    [TokenValidationFilter]

    public class CargosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CargosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<CargoDTO>>> GetAll()
        {
            var cargos = await context.Cargos
                .ToListAsync();

            if (!cargos.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<CargoDTO>>(cargos));
        }

    }
}