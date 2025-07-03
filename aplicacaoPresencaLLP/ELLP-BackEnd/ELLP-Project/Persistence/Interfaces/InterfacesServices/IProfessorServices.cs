using ELLP_Project.Models;

namespace ELLP_Project.Persistence.Interfaces.InterfacesServices
{
    public interface IProfessorServices
    {
        ProfessorModel CadastrarProfessor(ProfessorModel professor);
        ProfessorModel AtualizarProfessor(int ProfessorId, ProfessorModel professor);
        bool RemoverProfessor(int professorId);
        IEnumerable<ProfessorModel> GetProfessores();
        ProfessorModel? GetProfessorById(int professorId);
        bool AtualizarLogin(int professorId, string login);
        bool AtualizarSenha(int professorId, string senha);

    }
}
