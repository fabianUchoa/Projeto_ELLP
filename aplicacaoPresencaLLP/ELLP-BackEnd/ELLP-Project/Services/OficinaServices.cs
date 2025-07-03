using ELLP_Project.Models;
using ELLP_Project.Persistence.DBContext;
using ELLP_Project.Persistence.Interfaces.InterfacesServices;
using ELLP_Project.Persistence.Repositorios;

namespace ELLP_Project.Services
{
    public class OficinaServices : IOficinaServices
    {

        private readonly OficinaRepositorio _oficinaRepositorio;
        private readonly ProfessorRepositorio _professorRepositorio;
        private readonly MonitorRepositorio _monitorRepositorio;
        private readonly AppDbContext _context;

        public OficinaServices(OficinaRepositorio oficinaRepositorio, ProfessorRepositorio professorRepositorio, MonitorRepositorio monitorRepositorio, AppDbContext context)
        {
            _oficinaRepositorio = oficinaRepositorio;
            _professorRepositorio = professorRepositorio;
            _monitorRepositorio = monitorRepositorio;
            _context = context;
        }

        public OficinaModel AtualizarOficina(int OficinaId, OficinaModel oficina)
        {
            OficinaModel oficinaAtual = _oficinaRepositorio.GetOficinaById(OficinaId);
            if(oficinaAtual == null)
            {
                throw new ArgumentException("Não existe oficina com esse ID");
            }

            if (string.IsNullOrWhiteSpace(oficina.OficinaNome))
            {
                oficina.OficinaNome = oficinaAtual.OficinaNome;
            }

            if (_professorRepositorio.GetProfessorById(oficina.ProfessorId) == null)
            {
                oficina.ProfessorId = oficinaAtual.ProfessorId;
                oficina.Professor = oficinaAtual.Professor;
            }

            oficina.Professor = _professorRepositorio.GetProfessorById(oficina.ProfessorId);

            var novaOficina = _oficinaRepositorio.AtualizarOficina(OficinaId, oficina);

            _context.SaveChanges();

            return novaOficina;
        }

        public OficinaModel CadastrarOficina(OficinaModel oficina)
        {
            if (string.IsNullOrWhiteSpace(oficina.OficinaNome))
            {
                throw new ArgumentException("O campo nome não pode estar vazio.");
            }

            if (_professorRepositorio.GetProfessorById(oficina.ProfessorId) == null)
            {
                throw new ArgumentException("Não é possível criar uma oficina sem um professor vinculado");
            }

            oficina.Professor = _professorRepositorio.GetProfessorById(oficina.ProfessorId);

            var novaOficina = _oficinaRepositorio.AdicionarOficina(oficina);

            _context.SaveChanges();

            return novaOficina;
        }

        public OficinaModel? GetOficinaById(int oficinaId)
        {
            if (_oficinaRepositorio.GetOficinaById(oficinaId) == null)
                throw new ArgumentException("Não existe oficina com esse ID.");
            return _oficinaRepositorio.GetOficinaById(oficinaId);
        }

        public IEnumerable<OficinaModel> GetOficinas()
        {
            return _oficinaRepositorio.GetAllOficinas();
        }

        public bool RemoverAlunoMatriculado(int oficinaId, int alunoId)
        {
            OficinaModel oficina = _oficinaRepositorio.GetOficinaById(oficinaId);
            if(oficina == null)
            {
                throw new ArgumentException("Não existe oficina com esse nome.");
            }

            oficina.RemoverAlunoOficina(alunoId);

            _context.SaveChanges();

            return true;
        }

        public bool RemoverOficina(int oficinaId)
        {
            _oficinaRepositorio.DeleteOficina(oficinaId);
            _context.SaveChanges();
            return true;
        }

        public OficinaModel AlterarProfessor(int oficinaId, int professorId)
        {
            OficinaModel oficina = _oficinaRepositorio.GetOficinaById(oficinaId);
            if (oficina == null)
                throw new ArgumentException("Não existe oficina com esse ID.");
            ProfessorModel professor = _professorRepositorio.GetProfessorById(professorId);
            if (professor == null)
                throw new ArgumentException("Não existe professor com esse ID.");

            oficina = _oficinaRepositorio.AlterarProfessor(oficinaId, professor);

            _context.SaveChanges();
            return oficina;
        }

        public bool RemoverMonitor(int oficinaId, int monitorId)
        {
            MonitorModel monitor = _monitorRepositorio.GetMonitorById(monitorId);
            if (monitor == null)
                throw new ArgumentException("Não existe monitor com esse ID.");
            OficinaModel oficina = _oficinaRepositorio.GetOficinaById(oficinaId);
            if (oficina == null)
                throw new ArgumentException("Não existe oficina com esse ID.");
            _oficinaRepositorio.RemoverMonitor(oficinaId, monitorId);

            _context.SaveChanges();

            return true;
        }
    }
}
