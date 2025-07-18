using System.Text;

namespace Servicios
{

    public static class Encriptacion
    {
        public static string EncriptarPassword(string password)
        {
            if (password == null)
                throw new ArgumentNullException(nameof(password));

            try
            {
                // 1) Convertir a bytes Unicode
                byte[] bytes = Encoding.Unicode.GetBytes(password);
                // 2) Codificar a Base64
                return Convert.ToBase64String(bytes);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error al encriptar contraseña: {ex.Message}", ex);
            }
        }

        public static string DesencriptarPassword(string passwordEncriptado)
        {
            if (passwordEncriptado == null)
                throw new ArgumentNullException(nameof(passwordEncriptado));

            try
            {
                // 1) Decodificar Base64 a bytes
                byte[] bytes = Convert.FromBase64String(passwordEncriptado);
                // 2) Reconstruir string Unicode original
                return Encoding.Unicode.GetString(bytes);
            }
            catch (FormatException ex)
            {
                throw new ApplicationException("La contraseña encriptada tiene formato inválido.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error al desencriptar contraseña: {ex.Message}", ex);
            }
        }
    }
}
