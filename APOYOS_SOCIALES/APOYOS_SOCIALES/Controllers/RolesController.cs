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
    [Route("api/roles")]
    [ApiController]
    [TokenValidationFilter]

    public class RolesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public RolesController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<RolDTO>>> GetAll()
        {
            var roles = await context.Rols
                .ToListAsync();

            if (!roles.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<RolDTO>>(roles));
        }

    }
}