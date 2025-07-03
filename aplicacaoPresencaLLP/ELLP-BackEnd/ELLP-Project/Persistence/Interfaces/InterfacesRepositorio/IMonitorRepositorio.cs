using ELLP_Project.Models;

namespace ELLP_Project.Persistence.Interfaces.InterfacesRepositorio
{
    public interface IMonitorRepositorio
    {
        IEnumerable<MonitorModel> GetAllMonitor();
        MonitorModel? GetMonitorById(int id);
        MonitorModel AdicionarMonitor(MonitorModel monitor);
        MonitorModel AlterarMonitor(int monitorId, MonitorModel monitor);
        bool DeleteMonitor(int monitorId);
    }
}
