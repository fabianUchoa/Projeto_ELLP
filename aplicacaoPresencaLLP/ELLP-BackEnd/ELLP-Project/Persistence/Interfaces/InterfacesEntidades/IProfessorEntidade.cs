using ELLP_Project.Models;

namespace ELLP_Project.Persistence.Interfaces.InterfacesEntidades
{
    public interface IProfessorEntidade
    {
        void AlterarNome(string nome);
        void AdicionarOficina(OficinaModel oficina);
        bool RemoverOficina(int oficinaId);
        void DefinirSenhaHash(string senhaHash);
        void DefinirSalt(string salt);
        void DefinirLogin(string login);
    }
}
