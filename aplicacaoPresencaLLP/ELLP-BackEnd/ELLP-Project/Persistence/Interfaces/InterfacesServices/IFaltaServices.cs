using ELLP_Project.Models;

namespace ELLP_Project.Persistence.Interfaces.InterfacesServices;

public interface IFaltaServices
{
    FaltaModel CadastrarFalta(FaltaModel falta);
    FaltaModel AtualizarFalta(int FaltaId, FaltaModel falta);
    bool RemoverFalta(int faltaId);
    IEnumerable<FaltaModel> GetFaltas();
    FaltaModel? GetFaltaById(int faltaId);
    List<FaltaModel> GetFaltasByAluno(int alunoId);
}
