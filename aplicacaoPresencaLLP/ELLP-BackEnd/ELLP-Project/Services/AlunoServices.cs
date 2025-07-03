using ELLP_Project.Models;
using ELLP_Project.Persistence.DBContext;
using ELLP_Project.Persistence.Interfaces.InterfacesRepositorio;
using ELLP_Project.Persistence.Interfaces.InterfacesServices;
using ELLP_Project.Persistence.Repositorios;

namespace ELLP_Project.Services
{
    public class AlunoServices : IAlunoServices
    {

        private readonly AlunoRepositorio _alunoRepositorio;
        private readonly OficinaRepositorio _oficinaRepositorio;
        private readonly AppDbContext _context;

        public AlunoServices(AlunoRepositorio alunoRepositorio, OficinaRepositorio oficinaRepositorio, AppDbContext context)
        {
            _alunoRepositorio = alunoRepositorio;
            _oficinaRepositorio = oficinaRepositorio;
            _context = context;
        }

        public AlunoModel AtualizarAluno(int alunoId, AlunoModel aluno)
        {
            if (string.IsNullOrWhiteSpace(aluno.AlunoNome))
                throw new ArgumentException("Nome do aluno é obrigatório.");

            if (_alunoRepositorio.GetAlunoById(alunoId) == null)
                throw new ArgumentException("Não existe aluno com o ID informado");

            if (aluno.AlunoOficina == null)
            {
                aluno.AlunoOficina = _oficinaRepositorio.GetOficinaById(aluno.OficinaId);
            }

            var novoAluno = _alunoRepositorio.AtualizarAluno(alunoId, aluno);

            _context.SaveChanges();
            return novoAluno;
        }
            
        public AlunoModel CadastrarAluno(AlunoModel aluno)
        {
            if (string.IsNullOrWhiteSpace(aluno.AlunoNome))
                throw new ArgumentException("Nome do aluno é obrigatório");
            var novoAluno = _alunoRepositorio.AdicionarAluno(aluno);
            _context.SaveChanges();
            return novoAluno;     
        }

        public AlunoModel? GetAlunoById(int alunoId)
        {
            AlunoModel aluno = _alunoRepositorio.GetAlunoById(alunoId);
            if (aluno == null)
                throw new ArgumentException("Não existe aluno com esse ID.");
            return aluno;
        }

        public IEnumerable<AlunoModel> GetAlunos()
        {
            return _alunoRepositorio.GetAllAlunos();
        }

        public bool RemoverAluno(int alunoId)
        {
            if (_alunoRepositorio.GetAlunoById(alunoId) == null)
                throw new ArgumentException("Não existe aluno com esse ID");

            _alunoRepositorio.DeleteAluno(alunoId);

            _context.SaveChanges();
            return true;
        }
    }
}
