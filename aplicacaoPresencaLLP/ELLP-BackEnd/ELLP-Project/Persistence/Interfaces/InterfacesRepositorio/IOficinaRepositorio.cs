using ELLP_Project.Models;

namespace ELLP_Project.Persistence.Interfaces.InterfacesRepositorio
{
    public interface IOficinaRepositorio
    {
        IEnumerable<OficinaModel> GetAllOficinas();
        OficinaModel? GetOficinaById(int oficinaId);
        OficinaModel AdicionarOficina(OficinaModel oficina);
        bool DeleteOficina(int oficinaId);
        OficinaModel AtualizarOficina(int oficinaId, OficinaModel oficina);
    }
}
