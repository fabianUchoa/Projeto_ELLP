using ELLP_Project.Models;

namespace ELLP_Project.Persistence.Interfaces.InterfacesEntidades
{
    public interface IMonitorEntidade
    {
        void AlterarNome(string nome);
        void AdicionarOficina(OficinaModel oficina);
        void DefinirSenhaHash(string senhaHash);
        void DefinirSalt(string salt);
        void DefinirLogin(string login);

    }
}
