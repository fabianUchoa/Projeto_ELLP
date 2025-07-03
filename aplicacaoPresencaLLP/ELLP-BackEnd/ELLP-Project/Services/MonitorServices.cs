using ELLP_Project.Models;
using ELLP_Project.Persistence.DBContext;
using ELLP_Project.Persistence.Interfaces.InterfacesServices;
using ELLP_Project.Persistence.Repositorios;
using ELLP_Project.Utils;
using Microsoft.AspNetCore.Identity;

namespace ELLP_Project.Services
{
    public class MonitorServices : IMonitorServices
    {

        private readonly MonitorRepositorio _monitorRepositorio;
        private readonly OficinaRepositorio _oficinaRepositorio;
        private readonly AppDbContext _context;
        public MonitorServices(MonitorRepositorio monitorRepositorio, OficinaRepositorio oficinaRepositorio, AppDbContext context)
        {
            _monitorRepositorio = monitorRepositorio;
            _oficinaRepositorio = oficinaRepositorio;
            _context = context;
        }


        public MonitorModel AtualizarMonitor(int MonitorId, MonitorModel monitor)
        {
            MonitorModel monitorAtual = _monitorRepositorio.GetMonitorById(MonitorId);

            if (monitorAtual == null)
            {
                throw new Exception("Não existe monitor com o ID informado");
            }

            if(monitor.OficinaId==null)
                monitor.OficinaId = monitorAtual.OficinaId;

            if (monitor.Oficina == null) 
            {
                monitor.Oficina = monitorAtual.Oficina;
            }

            if (string.IsNullOrWhiteSpace(monitor.Nome))
            {
                monitor.Nome = monitorAtual.Nome;
            }

            if(monitor.Login == null)
            {
                monitor.Login = monitorAtual.Login;
            }

            if(monitor.SenhaHash == null)
            {
                monitor.SenhaHash = monitorAtual.SenhaHash;
                monitor.Salt = monitorAtual.Salt;
            }
            monitorAtual = _monitorRepositorio.AlterarMonitor(MonitorId, monitor)
            _context.SaveChanges();
            return monitorAtual;

        }

        public MonitorModel CadastrarMonitor(MonitorModel monitor)
        {
            if (string.IsNullOrWhiteSpace(monitor.Nome))
            {
                throw new ArgumentException("O campo nome não pode estar vazio.");
            }

            if (string.IsNullOrWhiteSpace(monitor.Login))
            {
                throw new ArgumentException("O campo login não pode estar vazio.");
            }

            if (string.IsNullOrWhiteSpace(monitor.SenhaHash))
            {
                throw new ArgumentException("O campo de senha não pode estar vazia.");
            }

            monitor.Salt = PasswordUtils.CriarSalt();
            monitor.SenhaHash = PasswordUtils.GerarHash(monitor.SenhaHash, monitor.Salt);
            var newMonitor = _monitorRepositorio.AdicionarMonitor(monitor)
            _context.SaveChanges();
            return newMonitor;

        }

        public MonitorModel? GetMonitorById(int monitorId)
        {
            if (_monitorRepositorio.GetMonitorById(monitorId) == null)
                throw new ArgumentException("Não existe monitor com esse ID.");

            return _monitorRepositorio.GetMonitorById(monitorId);
        }

        public IEnumerable<MonitorModel> GetMonitors()
        {
            return _monitorRepositorio.GetAllMonitor();
        }

        public bool RemoverMonitor(int monitorId)
        {
            if (_monitorRepositorio.GetMonitorById(monitorId) == null)
                throw new ArgumentException("Não existe monitor com esse ID.");
            _monitorRepositorio.DeleteMonitor(monitorId)
            _context.SaveChanges();
            return true;
        }

        public bool AtualizarLogin(int monitorId, string login)
        {
            MonitorModel monitor = _monitorRepositorio.GetMonitorById(monitorId);
            if (monitor == null)
                throw new ArgumentException("Não existe monitor com esse ID");
            monitor.Login = login;
            _monitorRepositorio.AlterarMonitor(monitorId, monitor);
            _context.SaveChanges();
            return true;
        }

        public bool AtualizarSenha(int monitorId, string senha)
        {
            MonitorModel monitor = _monitorRepositorio.GetMonitorById(monitorId);
            if (monitor == null)
                throw new ArgumentException("Não existe monitor com esse ID");

            monitor.Salt = PasswordUtils.CriarSalt();

            monitor.SenhaHash = PasswordUtils.GerarHash(senha, monitor.Salt);

            _monitorRepositorio.AlterarMonitor(monitorId, monitor);

            _context.SaveChanges();
            return true;
        }

        public MonitorModel AlterarOficinaVinculada(int monitorId, int oficinaId)
        {
            MonitorModel monitor = _monitorRepositorio.GetMonitorById(monitorId);
            if (monitor == null)
                throw new ArgumentException("Não existe monitor com esse ID");
            monitor.OficinaId = oficinaId;

            monitor.Oficina = _oficinaRepositorio.GetOficinaById(oficinaId);

            monitor = _monitorRepositorio.AlterarMonitor(monitorId, monitor);

            _context.SaveChanges() ;
            return monitor;
        }
    }
}
