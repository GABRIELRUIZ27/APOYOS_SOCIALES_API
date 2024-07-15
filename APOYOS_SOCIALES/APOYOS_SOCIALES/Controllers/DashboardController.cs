using APOYOS_SOCIALES.DTOs;
using APOYOS_SOCIALES.Entities;
using APOYOS_SOCIALES.Filters;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        [HttpGet("adquisiciones-por-dia")]
        public async Task<ActionResult<IEnumerable<AdquisicionesPorDiaDTO>>> GetAdquisicionesPorDia()
        {
            try
            {
                // Obtener todas las adquisiciones en memoria
                var adquisiciones = await context.Adquisiciones
                    .Select(a => new {
                        FechaAdquisicion = DateTime.ParseExact(a.FechaAdquisicion, "yyyy-MM-dd", CultureInfo.InvariantCulture)
                    })
                    .ToListAsync();

                // Obtener la fecha mínima y máxima
                var minDate = adquisiciones.Min(a => a.FechaAdquisicion);
                var maxDate = adquisiciones.Max(a => a.FechaAdquisicion);

                // Generar todas las fechas en el rango desde minDate hasta maxDate
                var allDates = Enumerable.Range(0, (maxDate - minDate).Days + 1)
                                         .Select(offset => minDate.AddDays(offset))
                                         .ToList();

                // Agrupar las adquisiciones por fecha
                var adquisicionesPorFecha = adquisiciones
                    .GroupBy(a => a.FechaAdquisicion)
                    .Select(g => new { Fecha = g.Key, Cantidad = g.Count() })
                    .ToList();

                // Generar la lista de DTOs con todas las fechas y sus cantidades de adquisiciones
                var adquisicionesPorDia = allDates.Select(date => new AdquisicionesPorDiaDTO
                {
                    Fecha = date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                    Cantidad = adquisicionesPorFecha.FirstOrDefault(a => a.Fecha == date)?.Cantidad ?? 0
                }).ToList();

                return Ok(adquisicionesPorDia);
            }
            catch (Exception ex)
            {
                // Registro de errores para depuración
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, $"Error al obtener las adquisiciones por día: {ex.Message}");
            }
        }

        [HttpGet("adquisiciones-por-area")]
        public async Task<ActionResult<EmpleadosPorGeneroDTO>> GetAdquisicionesPorArea()
        {
            try
            {
                var adquisicionesPorArea = await context.Adquisiciones
                    .GroupBy(p => p.Area.Nombre)
                    .Select(g => new { Area = g.Key, Count = g.Count() })
                    .ToDictionaryAsync(g => g.Area, g => g.Count);

                var adquisicionesPorAreaDTO = new AdquisicionesPorAreaDTO
                {
                    AdquisicionesPorArea = adquisicionesPorArea
                };

                return Ok(adquisicionesPorAreaDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Error al obtener el número de adquisiciones por área");
            }
        }

        [HttpGet("valor-adquisiciones")]
        public async Task<ActionResult<ValorAdquisicionesDTO>> GetTotalAdquisiciones()
        {
            try
            {
                decimal valorAdquisiciones = await context.Adquisiciones
                    .SumAsync(p => (decimal?)p.PrecioTotal ?? 0);

                var valorAdquisicionesDTO = new ValorAdquisicionesDTO
                {
                    ValorAdquisiciones = valorAdquisiciones
                };

                return Ok(valorAdquisicionesDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Error al obtener el valor de adquisiciones");
            }
        }

        [HttpGet("total-adquisiciones")]
        public async Task<ActionResult<TotalAdquisicionesDTO>> GetValorAdquisiciones() 
        {
            try
            {
                int totalAdquisiciones = await context.Adquisiciones.CountAsync();

                var totalAdquisicionesDTO = new TotalAdquisicionesDTO
                {
                    TotalAdquisiciones = totalAdquisiciones
                };

                return Ok(totalAdquisicionesDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Error al obtener el total de adquisiciones");
            }
        }
    }
}
