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
    [Route("api/genero")]
    [ApiController]
    [TokenValidationFilter]

    public class GeneroController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public GeneroController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<GeneroDTO>>> GetAll()
        {
            try
            {
                var genero = await context.Generos.ToListAsync();

                if (!genero.Any())
                {
                    return NotFound();
                }

                var tiposDTO = mapper.Map<List<GeneroDTO>>(genero);

                return Ok(tiposDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500);
            }
        }
    }
}