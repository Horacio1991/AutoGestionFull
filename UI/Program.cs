// Program.cs
using AutoGestion.UI;
using BLL;
using System;
using System.Linq;
using System.Windows.Forms;

static class Program
{
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        // *** Seed admin user si no existe ninguno ***
        var bllUsuario = new BLLUsuario();
        if (!bllUsuario.ListarUsuariosDto().Any())
        {
            // crea el admin con pwd "123"
            bllUsuario.RegistrarUsuario("admin", "123");
        }

        Application.Run(new FormLogin());
    }
}
