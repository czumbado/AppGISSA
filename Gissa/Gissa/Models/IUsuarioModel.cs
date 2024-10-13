using Gissa.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gissa.Models
{
    public interface IUsuarioModel
    {
        public Usuario? IniciarSesion(Usuario entidad);

        public List<SelectListItem>? ConsultNationality();
        public int RegisterAccount(Usuario entidad);
        public int ActualizarPerfil(Usuario entidad);
        public int RecoverAccount(Usuario entidad);
        public int? ChangePassword(UsuarioRecoverEnt entidad);

        public List<Usuario>? ConsultUsers();

        public int UpdateState(Usuario entidad);

        public int ChangeRol(Usuario entidad);

        public int DeleteUser(int IdUser);

        public int UpdateUser(Usuario entidad);

        public List<SelectListItem>? ConsultRol();
       
    }
}
