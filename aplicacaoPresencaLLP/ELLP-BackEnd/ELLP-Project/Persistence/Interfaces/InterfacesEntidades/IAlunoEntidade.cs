using ELLP_Project.Models;

namespace ELLP_Project.Persistence.Interfaces.InterfacesEntidades
{
    public interface IAlunoEntidade
    {
        int NumeroFaltas();
        List<FaltaModel> FaltasAluno();
        void AdicionarFalta(FaltaModel falta);
        bool RemoverFalta(int faltaId);
        OficinaModel DefinirOficina(OficinaModel oficina);
        void AlterarAlunoNome(string nome);
    }
}
