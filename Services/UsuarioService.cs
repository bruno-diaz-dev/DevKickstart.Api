using DevKickstart.Api.Models;

namespace DevKickstart.Api.Services;

public class UsuarioService
{
    public List<Usuario> FiltrarUsuarios(List<Usuario> usuarios, int edadMinima)
    {
        return usuarios
            .Where(u => u.Activo && u.Edad > edadMinima)
            .ToList();
    }

    public int ContarUsuariosActivos(List<Usuario> usuarios)
    {
        return usuarios.Count(u => u.Activo);
    }
}