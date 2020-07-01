using AutoMapper;
using SmartSchool.API.V1.Dtos;
using SmartSchool.API.Models;
using SmartSchool.API.Helpers;

namespace SmartSchool.API.V2.Profiles
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

            // CreateMap<Professor, ProfessorDto>()
            //     .ForMember(
            //         dest => dest.Nome,
            //         opt => opt.MapFrom(src => $"{src.Nome} {src.SobreNome}")
            //     );
            
            // CreateMap<ProfessorDto, Professor>();
            // CreateMap<Professor, ProfessorRegisterDto>().ReverseMap();                
        }


    }
}