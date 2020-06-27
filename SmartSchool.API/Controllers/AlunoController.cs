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
        //private readonly DataContext _context;

        public readonly IRepository _repo;

        public AlunoController(                            IRepository repo)
        {
            _repo = repo;
        }


        [HttpGet]
        public IActionResult Get()
        {
            var result = _repo.GetAllAlunos(true);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = _repo.GetAlunoById(id, false);
            if (aluno == null) return BadRequest("O Aluno não foi encontrado!!");
            return Ok(aluno);
        }
        // [HttpGet("ByName")]
        // public IActionResult GetById(string nome, string sobrenome)
        // {
        //     var aluno = _context.Alunos.FirstOrDefault(a => a.Nome.Contains(nome) && a.SobreNome.Contains(sobrenome));
        //     if (aluno == null) return BadRequest("O Aluno não foi encontrado!!");
        //     return Ok(aluno);
        // }

        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            _repo.Add(aluno);
            if (_repo.SaveChanges()){
                return Ok(aluno);
            }
            return BadRequest("O Aluno não foi encontrado!!");
            
        }


        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {

            var alu = _repo.GetAlunoById(id, false);
            if (alu == null) return BadRequest("O Aluno não foi encontrado!!");

            _repo.Update(aluno);
            if (_repo.SaveChanges()){
                return Ok(aluno);
            }
            return BadRequest("O Aluno não foi Atualizado!!");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            var alu = _repo.GetAlunoById(id, false);
            if (alu == null) return BadRequest("O Aluno não foi encontrado!!");

            _repo.Update(aluno);
            if (_repo.SaveChanges()){
                return Ok(aluno);
            }
            return BadRequest("O Aluno não foi Atualizado!!");
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var alu = _repo.GetAlunoById(id, false);
            if (alu == null) return BadRequest("O Aluno não foi encontrado!!");

            _repo.Delete(alu);
            if (_repo.SaveChanges()){
                return Ok("Aluno deletado");
            }
            return BadRequest("O Aluno não foi apagado!!");
        }
    }
}