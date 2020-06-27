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
        //private readonly DataContext _context;

        public readonly IRepository _repo;

        public ProfessorController(IRepository repo)
        {
            _repo = repo;
        }


        [HttpGet]
        public IActionResult Get()
        {
            var result = _repo.GetAllProfessores(true);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var Professor = _repo.GetProfessorById(id, false);
            if (Professor == null) return BadRequest("O Professor não foi encontrado!!");
            return Ok(Professor);
        }
        // [HttpGet("ByName")]
        // public IActionResult GetById(string nome, string sobrenome)
        // {
        //     var Professor = _context.Professors.FirstOrDefault(a => a.Nome.Contains(nome) && a.SobreNome.Contains(sobrenome));
        //     if (Professor == null) return BadRequest("O Professor não foi encontrado!!");
        //     return Ok(Professor);
        // }

        [HttpPost]
        public IActionResult Post(Professor Professor)
        {
            _repo.Add(Professor);
            if (_repo.SaveChanges()){
                return Ok(Professor);
            }
            return BadRequest("O Professor não foi encontrado!!");
            
        }


        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Professor Professor)
        {

            var prof = _repo.GetProfessorById(id, false);
            if (prof == null) return BadRequest("O Professor não foi encontrado!!");

            _repo.Update(Professor);
            if (_repo.SaveChanges()){
                return Ok(Professor);
            }
            return BadRequest("O Professor não foi Atualizado!!");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Professor Professor)
        {
            var prof = _repo.GetProfessorById(id, false);
            if (prof == null) return BadRequest("O Professor não foi encontrado!!");

            _repo.Update(Professor);
            if (_repo.SaveChanges()){
                return Ok(Professor);
            }
            return BadRequest("O Professor não foi Atualizado!!");
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var prof = _repo.GetProfessorById(id, false);
            if (prof == null) return BadRequest("O Professor não foi encontrado!!");

            _repo.Delete(prof);
            if (_repo.SaveChanges()){
                return Ok("Professor deletado");
            }
            return BadRequest("O Professor não foi apagado!!");
        }
    }
}