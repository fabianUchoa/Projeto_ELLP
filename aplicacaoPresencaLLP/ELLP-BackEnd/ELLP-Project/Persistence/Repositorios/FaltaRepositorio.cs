using ELLP_Project.Models;
using ELLP_Project.Persistence.DBContext;
using ELLP_Project.Persistence.Interfaces.InterfacesRepositorio;
using Microsoft.EntityFrameworkCore;

namespace ELLP_Project.Persistence.Repositorios
{
    public class FaltaRepositorio : IFaltaRepositorio
    {
        private readonly AppDbContext _context;

        public FaltaRepositorio(AppDbContext context)
        {
            _context = context;
        }

        public FaltaModel AdicionarFalta(FaltaModel falta)
        {
            _context.Faltas.Add(falta);
            return falta;
        }

        public FaltaModel AtualizarFalta(int faltaId, FaltaModel falta)
        {
            FaltaModel getFalta = _context.Faltas.Include(f=>f.Aluno).FirstOrDefault(falta => falta.FaltaId == faltaId);
            if(getFalta == null)
                return null;
            getFalta.AlterarAluno(falta.Aluno);
            getFalta.AlterarData(falta.DataFalta);
            getFalta.AlterarJustificativa(falta.JustificativaFalta);
            if (falta.FaltaJustificada == false)
            {
                getFalta.FaltaNaoJustificada();
            }else { getFalta.FaltaFoiJustificada(); }

            return getFalta;
        }

        public IEnumerable<FaltaModel> GetAllFaltas()
        {
            return _context.Faltas.Include(f => f.Aluno);
        }

        public List<FaltaModel> GetFaltaByAluno(int alunoId)
        {
            return _context.Faltas.Include(f=>f.Aluno).Where(f => f.AlunoId==alunoId).ToList();
        }

        public FaltaModel? GetFaltaById(int id)
        {
            return _context.Faltas.Include(f=>f.Aluno).FirstOrDefault(f=> f.FaltaId==id);
        }

        public bool RemoverFalta(int id)
        {
            FaltaModel falta = _context.Faltas.Include(f=>f.Aluno).FirstOrDefault(f=> f.FaltaId==id);
            if(falta == null) return false;
            _context.Faltas.Remove(falta);
            return true;
        }
    }
}
