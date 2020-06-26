using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SmartSchool.API.Models;
using System.Linq;
using SmartSchool.API.Data;
using Microsoft.EntityFrameworkCore;

namespace SmartSchool.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController : ControllerBase
    {
        private readonly DataContext _context;
        public ProfessorController(DataContext context)
        {
            _context = context;

        }


        public IActionResult Get()
        {
            return Ok(_context.Professores);
        }
        [HttpGet("byId/{id}")]
        public IActionResult GetById(int id)
        {
            var professor = _context.Professores.FirstOrDefault(a => a.Id == id);
            if (professor == null) return BadRequest("O Professor não foi encontrado!!");
            return Ok(professor);
        }
        [HttpGet("ByName")]
        public IActionResult GetById(string nome)
        {
            var professor = _context.Professores.FirstOrDefault(a => a.Nome.Contains(nome));
            if (professor == null) return BadRequest("O Professor não foi encontrado!!");
            return Ok(professor);
        }

        [HttpPost]
        public IActionResult Post(Professor professor)
        {
            _context.Add(professor);
            _context.SaveChanges();

            return Ok(professor);
        }


        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Professor professor)
        {

            var prof = _context.Professores.AsNoTracking().FirstOrDefault(a => a.Id == id);
            if (prof == null) return BadRequest("O Professor não foi encontrado!!");
            _context.Update(professor);
            _context.SaveChanges();
            return Ok(professor);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Professor professor)
        {
            var prof = _context.Professores.AsNoTracking().FirstOrDefault(a => a.Id == id);
            if (prof == null) return BadRequest("O Professor não foi encontrado!!");

            _context.Update(professor);
            _context.SaveChanges();
            return Ok(professor);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var professor = _context.Professores.AsNoTracking().FirstOrDefault(a => a.Id == id);
            if (professor == null) return BadRequest("O Professor não foi encontrado!!");
            _context.Remove(professor);
            _context.SaveChanges();
            return Ok();
        }
    }
}