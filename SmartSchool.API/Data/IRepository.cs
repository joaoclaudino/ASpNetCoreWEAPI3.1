using SmartSchool.API.Models;

namespace SmartSchool.API.Data
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        bool SaveChanges();

        //ALUNOS
        Aluno[] GetAllAlunos(bool includeProfessor=false);
        Aluno[] GetAllAlunosByDisciplinaId(int disciplinaId, bool includeProfessor=false);
        Aluno GetAlunoById(int alunoId, bool includeProfessor=false);

        //PROFESSOR
        Professor[] GetAllProfessores( bool includeAluno=false);
        Professor[] GetAllProfessoresByDisciplinaId(int disciplinaId, bool includeAlunos=false);
        Professor GetProfessorById(int professorId, bool includeProfessor=false);
    }
}