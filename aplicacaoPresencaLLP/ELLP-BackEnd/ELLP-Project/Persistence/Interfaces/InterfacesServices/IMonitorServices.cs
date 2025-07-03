using ELLP_Project.Models;

namespace ELLP_Project.Persistence.Interfaces.InterfacesServices
{
    public interface IMonitorServices
    {
        MonitorModel CadastrarMonitor(MonitorModel monitor);
        MonitorModel AtualizarMonitor(int MonitorId, MonitorModel monitor);
        bool RemoverMonitor(int monitorId);
        IEnumerable<MonitorModel> GetMonitors();
        MonitorModel? GetMonitorById(int monitorId);
        bool AtualizarLogin(int monitorId, string login);
        bool AtualizarSenha(int monitorId, string senha);
        MonitorModel AlterarOficinaVinculada(int monitorId, int oficinaId);
    }
}
