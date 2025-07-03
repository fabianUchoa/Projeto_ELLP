using ELLP_Project.Models;
using ELLP_Project.Persistence.DBContext;
using ELLP_Project.Persistence.Interfaces.InterfacesRepositorio;
using Microsoft.EntityFrameworkCore;

namespace ELLP_Project.Persistence.Repositorios
{
    public class OficinaRepositorio : IOficinaRepositorio
    {

        private readonly AppDbContext _context;

        public OficinaRepositorio(AppDbContext context)
        {
            _context = context;
        }

        public OficinaModel AdicionarOficina(OficinaModel oficina)
        {
            _context.Oficinas.Add(oficina);
            return oficina;
        }

        public OficinaModel AtualizarOficina(int oficinaId, OficinaModel oficina)
        {
            OficinaModel getOficina = _context.Oficinas.Include(o=>o.Alunos).Include(o=>o.Monitores).Include(o=>o.Professor).FirstOrDefault(f=> f.OficinaId == oficinaId);
            if (getOficina == null)
                return null;
            getOficina.AlterarNomeOficina(oficina.OficinaNome);
            getOficina.Professor = _context.Professores.Include(p=>p.Oficinas).FirstOrDefault(p => p.Id == oficina.ProfessorId);
            getOficina.AlterarProfessorOficina(getOficina.Professor);

            if (oficina.Alunos.Count != 0)
            {
                getOficina.Alunos.Clear();
                foreach (var aluno in oficina.Alunos)
                    getOficina.Alunos.Add(aluno);
            }


            if (oficina.Monitores.Count != 0)
            {
                getOficina.Monitores.Clear();
                foreach (var monitores in oficina.Monitores)
                    getOficina.Monitores.Add(monitores);
            }

            return getOficina;
        }

        public bool DeleteOficina(int oficinaId)
        {
            OficinaModel oficinaDelete = _context.Oficinas.Include(o=>o.Alunos).Include(o=>o.Monitores).Include(o=>o.Professor).FirstOrDefault(of=> of.OficinaId==oficinaId);
            if (oficinaDelete == null)
                return false;
            _context.Oficinas.Remove(oficinaDelete);
            return true;
        }

        public IEnumerable<OficinaModel> GetAllOficinas()
        {
            return _context.Oficinas.Include(o => o.Alunos).Include(o => o.Monitores).Include(o => o.Professor);
        }

        public OficinaModel? GetOficinaById(int oficinaId)
        {
            return _context.Oficinas.Include(o => o.Alunos).Include(o => o.Monitores).Include(o => o.Professor).FirstOrDefault(of => of.OficinaId == oficinaId);
        }

        public OficinaModel AlterarProfessor(int oficinaId, ProfessorModel professor)
        {
            OficinaModel oficina = _context.Oficinas.Include(o => o.Alunos).Include(o => o.Monitores).Include(o => o.Professor).FirstOrDefault(of=> of.OficinaId== oficinaId);
            oficina.AlterarProfessorOficina(professor);
            return oficina;
        }

        public bool RemoverMonitor(int oficinaId, int monitorId)
        {
            return _context.Oficinas.Include(o => o.Alunos).Include(o => o.Monitores).Include(o => o.Professor).FirstOrDefault(of=> of.OficinaId==oficinaId).RemoverMonitorOficina(monitorId);
        }
    }
}
