using System.Security.Claims;
using API_GISSA.Entities;

namespace API_GISSA.Entities
{
    public interface IUtilitarios
    {
        public string ArmarHTML(UserEnt datos, string claveTemporal);

        public string GenerarCodigo();

        public void EnviarCorreo(string Destinatario, string Asunto, string Mensaje);

        public string GenerarToken(string idUser,string IdRol );
       
        public string Encrypt(string texto);

        public string Decrypt(string texto);
        public long ObtenerUsuario(IEnumerable<Claim> valores);
    }
}
