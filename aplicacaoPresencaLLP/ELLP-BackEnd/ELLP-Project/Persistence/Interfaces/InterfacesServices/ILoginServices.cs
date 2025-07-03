using ELLP_Project.Models;

namespace ELLP_Project.Interfaces.InterfacesServices
{
    public interface ILoginServices
    {
        public bool ValidaçãoLogin(string login, string senha);
    }
}