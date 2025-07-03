using ELLP_Project.Models;

namespace ELLP_Project.Persistence.Interfaces.InterfacesServices
{
    public interface IAlunoServices
    {
        AlunoModel CadastrarAluno(AlunoModel aluno);
        AlunoModel AtualizarAluno(int AlunoId,AlunoModel aluno);
        bool RemoverAluno(int alunoId);
        AlunoModel? GetAlunoById(int alunoId);
        IEnumerable<AlunoModel> GetAlunos();

    }
}
