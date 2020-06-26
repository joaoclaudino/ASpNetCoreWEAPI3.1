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
    public class AlunoController : ControllerBase
    {
        private readonly DataContext _context;
        public AlunoController(DataContext context)
        {
            _context = context;

        }


        public IActionResult Get()
        {
            return Ok(_context.Alunos);
        }
        [HttpGet("byId/{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = _context.Alunos.FirstOrDefault(a => a.Id == id);
            if (aluno == null) return BadRequest("O Aluno não foi encontrado!!");
            return Ok(aluno);
        }
        [HttpGet("ByName")]
        public IActionResult GetById(string nome, string sobrenome)
        {
            var aluno = _context.Alunos.FirstOrDefault(a => a.Nome.Contains(nome) && a.SobreNome.Contains(sobrenome));
            if (aluno == null) return BadRequest("O Aluno não foi encontrado!!");
            return Ok(aluno);
        }

        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            _context.Add(aluno);
            _context.SaveChanges();

            return Ok(aluno);
        }


        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {

            var alu = _context.Alunos.AsNoTracking().FirstOrDefault(a => a.Id == id);
            if (alu == null) return BadRequest("O Aluno não foi encontrado!!");
            _context.Update(aluno);
            _context.SaveChanges();
            return Ok(aluno);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            var alu = _context.Alunos.AsNoTracking().FirstOrDefault(a => a.Id == id);
            if (alu == null) return BadRequest("O Aluno não foi encontrado!!");

            _context.Update(aluno);
            _context.SaveChanges();
            return Ok(aluno);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = _context.Alunos.AsNoTracking().FirstOrDefault(a => a.Id == id);
            if (aluno == null) return BadRequest("O Aluno não foi encontrado!!");
            _context.Remove(aluno);
            _context.SaveChanges();
            return Ok();
        }
    }
}