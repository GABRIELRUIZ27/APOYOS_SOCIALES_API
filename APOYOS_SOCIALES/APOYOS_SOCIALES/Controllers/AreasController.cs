﻿using AutoMapper;
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
    [Route("api/areas")]
    [ApiController]
    [TokenValidationFilter]

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
            try
            {
                var areas = await context.Areas.ToListAsync();

                if (!areas.Any())
                {
                    return NotFound();
                }

                // Obtener el total de apoyos por área
                var totalApoyosPorArea = await context.Apoyos
                    .GroupBy(apoyo => apoyo.Area.Id)
                    .Select(grupo => new { AreaId = grupo.Key, TotalApoyos = grupo.Count() })
                    .ToDictionaryAsync(x => x.AreaId, x => x.TotalApoyos);

                // Mapear las áreas y asignar el total de apoyos por área
                var areasDTO = mapper.Map<List<AreaDTO>>(areas);
                foreach (var area in areasDTO)
                {
                    if (totalApoyosPorArea.TryGetValue(area.Id ?? 0, out int total))
                    {
                        area.TotalApoyosPorArea = total;
                    }
                    else
                    {
                        area.TotalApoyosPorArea = 0; // Asignar cero si no hay apoyos para el área
                    }
                }

                return Ok(areasDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500);
            }
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Post(AreaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existeArea = await context.Areas.AnyAsync(n => n.Nombre == dto.Nombre);

            if (existeArea)
            {
                return Conflict();
            }

            var area = mapper.Map<Area>(dto);

            context.Add(area);

            try
            {
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("eliminar/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var area = await context.Areas.FindAsync(id);

            if (area == null)
            {
                return NotFound();
            }

            context.Areas.Remove(area);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("actualizar/{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] AreaDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("El ID de la ruta y el ID del objeto no coinciden");
            }

            var area = await context.Areas.FindAsync(id);

            if (area == null)
            {
                return NotFound();
            }

            mapper.Map(dto, area);

            context.Update(area);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AreaExists(id))
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

        private bool AreaExists(int id)
        {
            return context.Areas.Any(e => e.Id == id);
        }
    }
}