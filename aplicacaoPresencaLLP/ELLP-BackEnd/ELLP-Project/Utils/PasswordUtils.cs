namespace ELLP_Project.Utils
{
    using System.Text;
    using System.Security.Cryptography;
    public static class PasswordUtils
    {
        public static string CriarSalt(int tamanho = 16)
        {
            var saltBytes = new byte[tamanho];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(saltBytes);

            return Convert.ToBase64String(saltBytes);
        }

        public static string GerarHash(string senha, string salt)
        {
            var senhaComSalt = senha + salt;
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(senhaComSalt);
            var hash = sha256.ComputeHash(bytes);

            return Convert.ToBase64String(hash);
        }

        public static bool ValidarSenha(string senhaDigitada, string salt, string hashArmazenado)
        {
            string novoHash = GerarHash(senhaDigitada, salt);
            return novoHash == hashArmazenado;
        }
    }
}
