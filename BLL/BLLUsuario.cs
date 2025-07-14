using BE;
using BE.BEComposite;
using Mapper;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class BLLUsuario
    {
        private readonly MPPUsuario _mppUsuario;
        private readonly BLLComponente _bllComponente;

        public BLLUsuario()
        {
            _mppUsuario = new MPPUsuario();
            _bllComponente = new BLLComponente();
        }

        // Encripta con nuestro servicio de Encriptacion
        public string EncriptarPassword(string password)
            => Encriptacion.EncriptarPassword(password);

        public string DesencriptarPassword(string pwdEncriptado)
            => Encriptacion.DesencriptarPassword(pwdEncriptado);

        // Valida credenciales: encripta la que trae el usuario antes de verificar
        public bool ValidarLogin(Usuario usuario)
        {
            if (usuario == null) throw new ArgumentNullException(nameof(usuario));
            usuario.Password = EncriptarPassword(usuario.Password);
            return _mppUsuario.VerificarUsuario(usuario.Username, usuario.Password);
        }

        // Listado completo de usuarios (sin contraseña)
        public List<Usuario> ListarUsuarios()
        {
            return _mppUsuario.ListarTodo();
        }

        // Registro: encripta antes de mandar al MPP
        public void RegistrarUsuario(Usuario u)
        {
            if (u == null) throw new ArgumentNullException(nameof(u));
            u.Password = EncriptarPassword(u.Password);
            _mppUsuario.RegistrarUsuario(u);
        }

        public bool ExisteUsuario(string username)
            => _mppUsuario.ExisteUsuario(username);

        public void ModificarUsuario(string usernameOriginal, Usuario modificado)
        {
            if (modificado == null) throw new ArgumentNullException(nameof(modificado));
            // si cambió contraseña, encriptar la nueva
            modificado.Password = EncriptarPassword(modificado.Password);
            _mppUsuario.ModificarUsuario(usernameOriginal, modificado);
        }

        public void EliminarUsuario(string username)
            => _mppUsuario.EliminarUsuario(username);

        /// <summary>
        /// Devuelve solo los permisos (no roles) que tiene asignado directa o indirectamente
        /// </summary>
        public List<BEPermiso> ListarPermisosUsuario(int idUsuario)
        {
            var componentes = _bllComponente.ListarPermisosUsuario(idUsuario);
            var permisos = new List<BEPermiso>();

            foreach (var comp in componentes)
                RecolectarPermisos(comp, permisos);

            return permisos;
        }

        private void RecolectarPermisos(BEComponente comp, List<BEPermiso> salida)
        {
            if (comp is BEPermiso p)
            {
                if (!salida.Any(x => x.Id == p.Id))
                    salida.Add(p);
            }
            else if (comp is BERol r)
            {
                foreach (var hijo in r.Hijos)
                    RecolectarPermisos(hijo, salida);
            }
        }
    }
}
