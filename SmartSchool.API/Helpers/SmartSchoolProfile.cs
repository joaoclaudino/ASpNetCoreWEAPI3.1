using AutoMapper;
using SmartSchool.API.Dtos;
using SmartSchool.API.Models;

namespace SmartSchool.API.Helpers
{
    public class SmartSchoolProfile: Profile
    {
        public SmartSchoolProfile()
        {
            CreateMap<Aluno,AlunoDto>()
                .ForMember(
                    dest =>dest.Nome,
                    opt => opt.MapFrom(scr =>  $"{scr.Nome} {scr.SobreNome}")
                )
                .ForMember(
                    dest => dest.Idade,
                    opt =>opt.MapFrom(src => src.DataNasc.GetCurrentAge())
                );

                CreateMap<AlunoDto,Aluno>();


                CreateMap<Aluno,AlunoRegisterDto>().ReverseMap();

            CreateMap<Professor, ProfessorDto>()
                .ForMember(
                    dest => dest.Nome,
                    opt => opt.MapFrom(src => $"{src.Nome} {src.SobreNome}")
                );
            
            CreateMap<ProfessorDto, Professor>();
            CreateMap<Professor, ProfessorRegisterDto>().ReverseMap();                
        }


    }
}