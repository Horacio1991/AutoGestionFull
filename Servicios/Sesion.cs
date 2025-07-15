using DTOs;

namespace Servicios
{
 

    /// <summary>
    /// Mantiene la sesión del usuario autenticado.
    /// </summary>
    public static class Sesion
    {
        /// <summary>
        /// El usuario que ha iniciado sesión.
        /// </summary>
        public static UsuarioDto UsuarioActual { get; set; }
    }
}
