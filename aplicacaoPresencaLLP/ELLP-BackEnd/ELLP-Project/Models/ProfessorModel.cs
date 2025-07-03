using ELLP_Project.Persistence.Interfaces.InterfacesEntidades;

namespace ELLP_Project.Models
{
    public class ProfessorModel : IProfessorEntidade
    {
        public ProfessorModel()
        {
            Oficinas = new List<OficinaModel>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public virtual List<OficinaModel> Oficinas { get; set; }
        public string Salt { get; set; }
        public string SenhaHash { get; set; }
        public string Login { get; set; }

        public void AdicionarOficina(OficinaModel oficina)
        {
            Oficinas.Add(oficina);
        }

        public void AlterarNome(string nome)
        {
            Nome = nome;
        }

        public bool RemoverOficina(int oficinaId)
        {
            if (Oficinas.FirstOrDefault(oficina => oficina.OficinaId == oficinaId) == null)
                return false;

            Oficinas.RemoveAll(oficina => oficina.OficinaId == oficinaId);
            return true;
        }

        public void DefinirSenhaHash(string senhaHash)
        {
            SenhaHash = senhaHash;
        }

        public void DefinirSalt(string salt)
        {
            Salt = salt;
        }

        public void DefinirLogin(string login)
        {
            Login = login;
        }
    }
}
