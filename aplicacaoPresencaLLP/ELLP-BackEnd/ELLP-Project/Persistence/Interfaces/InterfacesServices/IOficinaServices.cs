using ELLP_Project.Models;

namespace ELLP_Project.Persistence.Interfaces.InterfacesServices
{
    public interface IOficinaServices
    {
        OficinaModel CadastrarOficina(OficinaModel oficina);
        OficinaModel AtualizarOficina(int OficinaId, OficinaModel oficina);
        bool RemoverOficina(int oficinaId);
        IEnumerable<OficinaModel> GetOficinas();
        OficinaModel? GetOficinaById(int oficinaId);
        bool RemoverAlunoMatriculado(int oficinaId, int alunoId);
    }
}
