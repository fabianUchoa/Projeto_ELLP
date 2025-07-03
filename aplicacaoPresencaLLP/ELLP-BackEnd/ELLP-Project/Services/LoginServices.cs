using ELLP_Project.Models;
using ELLP_Project.Interfaces.InterfacesServices;
using ELLP_Project.Persistence.Repositorios;
using System.Security.Cryptography;
using System.Text;
using ELLP_Project.Utils;

namespace ELLP_Project.Services
{
    public class LoginServices : ILoginServices
    {
        private readonly MonitorServices _monitorServices;
        private readonly ProfessorServices _professorServices;

        public LoginServices(
            MonitorServices monitorServices,
            ProfessorServices professorServices)
        {
            _monitorServices = monitorServices;
            _professorServices = professorServices;
        }

        public bool ValidaçãoLogin(string login, string senha)
        {
            if (string.IsNullOrWhiteSpace(login))
                throw new ArgumentNullException("Campo login não pode estar vazio");
            if (string.IsNullOrWhiteSpace(senha))
                throw new ArgumentNullException("Campo senha não pode estar vazio.");

            var user = _monitorServices.GetMonitors().FirstOrDefault(m => m.Login == login);
           
            var user1 = _professorServices.GetProfessores().FirstOrDefault(p => p.Login == login);
            if (user == null && user1 == null)
                throw new ArgumentNullException("Login informado é inválido.");
            else if (user1 != null)
                PasswordUtils.ValidarSenha(senha, user1.Salt, user1.SenhaHash);
            else if (user != null)
                PasswordUtils.ValidarSenha(senha, user.Salt, user.SenhaHash);

            return true;
        }

    }
}