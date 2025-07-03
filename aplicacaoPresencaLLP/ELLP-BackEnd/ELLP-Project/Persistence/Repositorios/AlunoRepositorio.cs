
using ELLP_Project.Models;
using ELLP_Project.Persistence.Interfaces.InterfacesRepositorio;
using ELLP_Project.Persistence.DBContext;
using Microsoft.EntityFrameworkCore;

namespace ELLP_Project.Persistence.Repositorios;


public class AlunoRepositorio : IAlunoRepositorio
{

    private readonly AppDbContext _context;

    public AlunoRepositorio(AppDbContext context)
    {
        _context = context;
    }

    public AlunoModel AdicionarAluno(AlunoModel aluno)
    {
        _context.Alunos.Add(aluno);
        return aluno;
    }

    public AlunoModel AtualizarAluno(int alunoId, AlunoModel aluno)
    {
        AlunoModel getAluno = _context.Alunos.Include(a => a.AlunoFaltas).Include(a => a.AlunoOficina)
                              .FirstOrDefault(a => a.AlunoId == alunoId);
        if (getAluno == null)
            return null;
        getAluno.AlterarAlunoNome(aluno.AlunoNome);
        if (aluno.AlunoFaltas.Count!=0)
        {
            getAluno.AlunoFaltas.Clear();
            foreach(var falta in aluno.AlunoFaltas)
                getAluno.AlunoFaltas.Add(falta);
        }

        getAluno.DefinirOficina(aluno.AlunoOficina);

        
        return getAluno;
    }

    public bool DeleteAluno(int id)
    {
        var aluno = _context.Alunos.Include(a=>a.AlunoOficina).Include(a=> a.AlunoFaltas).FirstOrDefault(a=> a.AlunoId ==id);
        if(aluno== null)
            return false;
        _context.Alunos.Remove(aluno);


        return true;
    }

    public IEnumerable<AlunoModel> GetAllAlunos()
    {
        return _context.Alunos.Include(a=>a.AlunoFaltas).Include(a=>a.AlunoOficina);
    }

    public AlunoModel? GetAlunoById(int alunoId)
    {
        return _context.Alunos.Include(a=>a.AlunoFaltas).Include(a=>a.AlunoOficina).FirstOrDefault(aluno => aluno.AlunoId == alunoId);
    }

}