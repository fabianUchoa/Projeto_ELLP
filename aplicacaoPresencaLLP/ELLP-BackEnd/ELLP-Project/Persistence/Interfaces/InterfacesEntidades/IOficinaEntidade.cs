using ELLP_Project.Models;

namespace ELLP_Project.Persistence.Interfaces.InterfacesEntidades
{
    public interface IOficinaEntidade
    {
        void AlterarNomeOficina(string nome);
        bool RemoverAlunoOficina(int AlunoId);
        bool RemoverMonitorOficina(int MonitorId);
        void AlterarProfessorOficina(ProfessorModel professor);

    }
}
