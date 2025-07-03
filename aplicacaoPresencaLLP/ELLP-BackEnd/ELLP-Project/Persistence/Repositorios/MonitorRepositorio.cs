using ELLP_Project.Models;
using ELLP_Project.Persistence.DBContext;
using ELLP_Project.Persistence.Interfaces.InterfacesRepositorio;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace ELLP_Project.Persistence.Repositorios
{
    public class MonitorRepositorio : IMonitorRepositorio
    {
        private readonly AppDbContext _context;

        public MonitorRepositorio(AppDbContext context)
        {
            _context = context;
        }

        public MonitorModel AdicionarMonitor(MonitorModel monitor)
        {
            _context.Monitores.Add(monitor);
            return monitor;
        }

        public MonitorModel AlterarMonitor(int monitorId, MonitorModel monitor)
        {
            MonitorModel getMonitor = _context.Monitores.Include(m => m.Oficina).FirstOrDefault(monitor => monitor.Id == monitorId);
            if (getMonitor == null)
                return null;
            getMonitor.AlterarNome(monitor.Nome);
    
            getMonitor.AdicionarOficina(monitor.Oficina);

            getMonitor.DefinirSalt(monitor.Salt);
            getMonitor.DefinirSenhaHash(monitor.SenhaHash);
            getMonitor.DefinirLogin(monitor.Login);

            return getMonitor;
        }

        public bool DeleteMonitor(int monitorId)
        {
            var monitor = _context.Monitores.Include(m=>m.Oficina).FirstOrDefault(m => m.Id == monitorId);
            if(monitor == null)
                return false;
            _context.Monitores.Remove(monitor);
            return true;
        }

        public IEnumerable<MonitorModel> GetAllMonitor()
        {
            return _context.Monitores.Include(m=>m.Oficina);
        }

        public MonitorModel? GetMonitorById(int id)
        {
            return _context.Monitores.Include(m=>m.Oficina).FirstOrDefault(monitor=>monitor.Id==id);
        }
    }
}
