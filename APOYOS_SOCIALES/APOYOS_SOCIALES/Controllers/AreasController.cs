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
    [Route("api/areas")]
    [ApiController]

    public class AreasController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public AreasController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<AreaDTO>>> GetAll()
        {
            var areas = await context.Areas
                .ToListAsync();

            if (!areas.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<AreaDTO>>(areas));
        }

    }
}