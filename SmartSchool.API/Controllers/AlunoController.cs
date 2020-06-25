using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SmartSchool.API.Models;
using System.Linq;

namespace SmartSchool.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        public List<Aluno> Alunos = new List<Aluno>(){
            new Aluno(){
                Id = 1,
                Nome = "Marcos",
                SobreNome="Claudino",
                Telefone = "123456"
            },
            new Aluno(){
                Id = 2,
                Nome = "João",
                SobreNome="Borges",
                Telefone = "123456"
            },
            new Aluno(){
                Id = 3,
                Nome = "Guaderão",
                SobreNome="Borges Claudino 2",
                Telefone = "123456"
            }                        
        };
        public AlunoController(){}

        public IActionResult Get(){
            return Ok(Alunos);
        }
        [HttpGet("byId/{id}")]
        public IActionResult GetById(int id){
            var aluno = Alunos.FirstOrDefault(a => a.Id==id);
            if (aluno==null) return BadRequest("O Aluno não foi encontrado!!");
            return Ok(aluno);
        }        
        [HttpGet("ByName")]
        public IActionResult GetById(string nome, string sobrenome){
            var aluno = Alunos.FirstOrDefault(a => a.Nome.Contains(nome) &&  a.SobreNome.Contains(sobrenome)   );
            if (aluno==null) return BadRequest("O Aluno não foi encontrado!!");
            return Ok(aluno);
        } 

        [HttpPost]       
        public IActionResult Post(Aluno aluno)
        {
            return Ok(aluno);
        }


        [HttpPatch("{id}")]       
        public IActionResult Patch(int id, Aluno aluno)
        {
            return Ok(aluno);
        }           

        [HttpPut("{id}")]       
        public IActionResult Put(int id, Aluno aluno)
        {
            return Ok(aluno);
        }    


        [HttpDelete("{id}")]       
        public IActionResult Delete(int id)
        {
            return Ok();
        }                 
    }
}