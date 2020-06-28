using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SmartSchool.API.Models;
using System.Linq;
using SmartSchool.API.Data;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Dtos;
using AutoMapper;

namespace SmartSchool.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        //private readonly DataContext _context;

        public readonly IRepository _repo;
        private readonly IMapper _mapper;

        public AlunoController(IRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }


        [HttpGet]
        public IActionResult Get()
        {
            var alunos = _repo.GetAllAlunos(true);
            // var alunosRetorno = new List<AlunoDto>();

            // foreach (var aluno in alunos)
            // {
            //     alunosRetorno.Add(
            //         new AlunoDto()
            //         {
            //             Id = aluno.Id
            //             ,
            //             Matricula = aluno.Matricula
            //             ,
            //             Nome = $"{aluno.Nome} {aluno.SobreNome}"
            //             ,
            //             Telefone = aluno.Telefone
            //             ,
            //             DataNasc = aluno.DataNasc
            //             ,
            //             DataIni = aluno.DataIni
            //             ,
            //             Ativo = aluno.Ativo
            //         });
            // }
            return Ok(_mapper.Map<IEnumerable<AlunoDto>>(alunos));
        }
        [HttpGet("GetRegister")]
        public IActionResult GetRegister()
        {

            return Ok(new AlunoRegisterDto());
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = _repo.GetAlunoById(id, false);
            if (aluno == null) return BadRequest("O Aluno não foi encontrado!!");

            var alunoDto = _mapper.Map<AlunoDto>(aluno);
            return Ok(alunoDto);
        }

        // [HttpGet("ByName")]
        // public IActionResult GetById(string nome, string sobrenome)
        // {
        //     var aluno = _context.Alunos.FirstOrDefault(a => a.Nome.Contains(nome) && a.SobreNome.Contains(sobrenome));
        //     if (aluno == null) return BadRequest("O Aluno não foi encontrado!!");
        //     return Ok(aluno);
        // }
        [HttpPost]
        public IActionResult Post(AlunoRegisterDto model)
        {

            var aluno = _mapper.Map<Aluno>(model);

            _repo.Add(aluno);
            if (_repo.SaveChanges())
            {
                //return Ok(aluno);
                return Created($"api/Aluno/{model.Id}",_mapper.Map<AlunoDto>(aluno));
            }
            return BadRequest("O Aluno não cadastrado!!");

        }


        [HttpPatch("{id}")]
        public IActionResult Patch(int id, AlunoRegisterDto model)
        {

            var aluno = _repo.GetAlunoById(id, false);
            if (aluno == null) return BadRequest("O Aluno não foi encontrado!!");

            _mapper.Map(model,aluno);

            _repo.Update(aluno);
            if (_repo.SaveChanges())
            {
                return Created($"api/Aluno/{model.Id}",_mapper.Map<AlunoDto>(aluno));
            }
            return BadRequest("O Aluno não foi Atualizado!!");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, AlunoRegisterDto model)
        {
            var aluno = _repo.GetAlunoById(id, false);
            if (aluno == null) return BadRequest("O Aluno não foi encontrado!!");

            _mapper.Map(model,aluno);

            _repo.Update(aluno);
            if (_repo.SaveChanges())
            {
                return Created($"api/Aluno/{model.Id}",_mapper.Map<AlunoDto>(aluno));
            }
            return BadRequest("O Aluno não foi Atualizado!!");
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = _repo.GetAlunoById(id, false);
            if (aluno == null) return BadRequest("O Aluno não foi encontrado!!");

            _repo.Delete(aluno);
            if (_repo.SaveChanges())
            {
                return Ok("Aluno deletado");
            }
            return BadRequest("O Aluno não foi apagado!!");
        }
    }
}