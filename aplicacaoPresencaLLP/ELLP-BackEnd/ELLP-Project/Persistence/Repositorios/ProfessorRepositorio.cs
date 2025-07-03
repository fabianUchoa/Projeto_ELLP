using ELLP_Project.Models;
using ELLP_Project.Persistence.DBContext;
using ELLP_Project.Persistence.Interfaces.InterfacesRepositorio;
using Microsoft.EntityFrameworkCore;

namespace ELLP_Project.Persistence.Repositorios
{
    public class ProfessorRepositorio : IProfessorRepositorio
    {

        private readonly AppDbContext _context;

        public ProfessorRepositorio(AppDbContext context)
        {
            _context = context;
        }

        public ProfessorModel AdicionarProfessor(ProfessorModel professor)
        {
            _context.Professores.Add(professor);
            return professor;
        }

        public ProfessorModel AlterarProfessor(int id, ProfessorModel professor)
        {
            ProfessorModel getProfessor = _context.Professores.Include(p => p.Oficinas).FirstOrDefault(p => p.Id == id);
            if (getProfessor == null)
                return null;
            getProfessor.AlterarNome(professor.Nome);
            getProfessor.DefinirLogin(professor.Login);
            getProfessor.DefinirSalt(professor.Salt);
            getProfessor.DefinirSenhaHash(professor.SenhaHash);

            if (professor.Oficinas.Count != 0) 
            {
                getProfessor.Oficinas.Clear();
                foreach (var oficina in professor.Oficinas)
                    getProfessor.Oficinas.Add(oficina);
            }

            return getProfessor;
        }

        public bool DeleteProfessor(int id)
        {
            ProfessorModel getProfessor = _context.Professores.Include(p => p.Oficinas).FirstOrDefault(p => p.Id==id);
            if(getProfessor == null)
                return false;
            _context.Professores.Remove(getProfessor);
            return true;
        }

        public IEnumerable<ProfessorModel> GetAllProfessores()
        {
            return _context.Professores.Include(p => p.Oficinas);
        }

        public ProfessorModel? GetProfessorById(int id)
        {
            return _context.Professores.Include(p => p.Oficinas).FirstOrDefault(p => p.Id == id);
        }
    }
}
