using ELLP_Project.Models;

namespace ELLP_Project.Persistence.Interfaces.InterfacesEntidades
{
    public interface IFaltaEntidade
    {
        void FaltaFoiJustificada();
        void FaltaNaoJustificada();
        void AlterarData(DateOnly data);
        void AlterarJustificativa(string justificativa);
        void AlterarAluno(AlunoModel aluno);

    }
}
