using APOYOS_SOCIALES.DTOs;
using APOYOS_SOCIALES.Entities;
using APOYOS_SOCIALES.Filters;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APOYOS_SOCIALES.Controllers
{
    [Authorize]
    [Route("api/dashboard")]
    [ApiController]
    [TokenValidationFilter]
    public class DashboardController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public DashboardController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("total-empleados")]
        public async Task<ActionResult<TotalEmpleadosDTO>> GetTotalEmpleados()
        {
            try
            {
                int totalEmpleados = await context.Personales.CountAsync();

                var totalEmpleadosDTO = new TotalEmpleadosDTO
                {
                    TotalEmpleados = totalEmpleados
                };

                return Ok(totalEmpleadosDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Error al obtener el total de empleados");
            }
        }

        [HttpGet("total-salarios")]
        public async Task<ActionResult<TotalSalariosDTO>> GetTotalSalarios()
        {
            try
            {
                decimal totalSalarios = await context.Personales
                    .SumAsync(p => (decimal?)p.Salario ?? 0);

                var totalSalariosDTO = new TotalSalariosDTO
                {
                    TotalSalarios = totalSalarios
                };

                return Ok(totalSalariosDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Error al obtener el total de salarios");
            }
        }

        [HttpGet("total-areas")]
        public async Task<ActionResult<TotalAreasDTO>> GetTotalAreas()
        {
            try
            {
                int totalAreas = await context.Areas.CountAsync();

                var totalAreasDTO = new TotalAreasDTO
                {
                    TotalAreas = totalAreas
                };

                return Ok(totalAreasDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Error al obtener el total de áreas");
            }
        }

        [HttpGet("empleados-por-genero")]
        public async Task<ActionResult<EmpleadosPorGeneroDTO>> GetEmpleadosPorGenero()
        {
            try
            {
                var empleadosPorGenero = await context.Personales
                    .GroupBy(p => p.Genero.Nombre)
                    .Select(g => new { Genero = g.Key, Count = g.Count() })
                    .ToDictionaryAsync(g => g.Genero, g => g.Count);

                var empleadosPorGeneroDTO = new EmpleadosPorGeneroDTO
                {
                    EmpleadosPorGenero = empleadosPorGenero
                };

                return Ok(empleadosPorGeneroDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Error al obtener el número de empleados por género");
            }
        }

        [HttpGet("empleados-por-area")]
        public async Task<ActionResult<EmpleadosPorAreaDTO>> GetEmpleadosPorArea()
        {
            try
            {
                var empleadosPorArea = await context.Personales
                    .GroupBy(p => p.Area.Nombre)
                    .Select(a => new { Area = a.Key, Count = a.Count() })
                    .ToDictionaryAsync(a => a.Area, a => a.Count);

                var empleadosPorAreaDTO = new EmpleadosPorAreaDTO
                {
                    EmpleadosPorArea = empleadosPorArea
                };

                return Ok(empleadosPorAreaDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Error al obtener el número de empleados por área");
            }
        }
    }
}
