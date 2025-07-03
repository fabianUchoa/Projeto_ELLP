using ELLP_Project.Models;

namespace ELLP_Project.Persistence.Interfaces.InterfacesRepositorio
{
    public interface IFaltaRepositorio
    {
        FaltaModel AdicionarFalta(FaltaModel falta);
        bool RemoverFalta(int id);
        FaltaModel AtualizarFalta(int faltaId, FaltaModel falta);
        IEnumerable<FaltaModel> GetAllFaltas();
        FaltaModel? GetFaltaById(int id);
        List<FaltaModel> GetFaltaByAluno(int alunoId);
    }
}
