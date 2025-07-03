using ELLP_Project.Models;

namespace ELLP_Project.Persistence.Interfaces.InterfacesRepositorio
{
    public interface IAlunoRepositorio
    {
        IEnumerable<AlunoModel> GetAllAlunos();
        AlunoModel? GetAlunoById(int id);
        AlunoModel AdicionarAluno(AlunoModel aluno);
        AlunoModel AtualizarAluno(int alunoId, AlunoModel aluno);
        bool DeleteAluno(int id);
    }
}
