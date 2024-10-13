using Azure;
using Azure.Communication.Email;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace API_GISSA.Entities
{
    public class Utilitarios : IUtilitarios
    {
        private readonly IConfiguration _configuration;
        private IHostEnvironment _hostingEnvironment;

        public Utilitarios(IConfiguration configuration, IHostEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        public string ArmarHTML(UserEnt datos, string claveTemporal)
        {
            string rutaArchivo = Path.Combine(_hostingEnvironment.ContentRootPath, "CorreosTemplate\\Contrasenna.html");
            string htmlArchivo = System.IO.File.ReadAllText(rutaArchivo);
            htmlArchivo = htmlArchivo.Replace("@@Nombre", datos.NameUser);
            htmlArchivo = htmlArchivo.Replace("@@ClaveTemporal", claveTemporal);
            
            return htmlArchivo;
        }

        public string GenerarCodigo()
        {
            int length = 4;
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        //public void EnviarCorreo(string Destinatario, string Asunto, string Mensaje)
        //{
        //    string connectionString = "endpoint=https://cscarnesdonadrian1.unitedstates.communication.azure.com/;accesskey=e+5LeYCwmXrvFVBjc71FwSdX3JN7pRDRxhLhcrwxLhPLzrC9SflSK1h70PQvlCrvIw7Ji2EdYtA4r705f9+uHg==";
        //    var emailClient = new EmailClient(connectionString);

        //    EmailSendOperation emailSendOperation = emailClient.Send(
        //        WaitUntil.Completed,
        //        senderAddress: "DoNotReply@3429f633-b0e1-4e15-b5f9-e9a9728e5eda.azurecomm.net",
        //        recipientAddress: Destinatario,
        //        subject: Asunto,
        //        htmlContent: Mensaje);
        //}
        public void EnviarCorreo(string Destinatario, string Asunto, string Mensaje)
        {
            string connectionString = "endpoint=https://cscarnesdonadrian1.unitedstates.communication.azure.com/;accesskey=e+5LeYCwmXrvFVBjc71FwSdX3JN7pRDRxhLhcrwxLhPLzrC9SflSK1h70PQvlCrvIw7Ji2EdYtA4r705f9+uHg==";
            var emailClient = new EmailClient(connectionString);

            EmailSendOperation emailSendOperation = emailClient.Send(
                WaitUntil.Completed,
                senderAddress: "DoNotReply@bdbc1771-6be5-48b9-9488-2bd71a700827.azurecomm.net",
                recipientAddress: Destinatario,
                subject: Asunto,
                htmlContent: Mensaje);
        }

        public string GenerarToken(string idUser, string IdRol)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("username", Encrypt(idUser)));
            claims.Add(new Claim("userrol", Encrypt(IdRol)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Ty1UELmVFKQmMD4af0a4jvfZS30cXu3U"));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: cred);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public string Encrypt(string texto)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes("EzfjS0IHnNSjv0jo");
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(texto);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

        public string Decrypt(string texto)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(texto);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes("EzfjS0IHnNSjv0jo");
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }
        public long ObtenerUsuario(IEnumerable<Claim> valores)
        {
            var claims = valores.Select(Claim => new { Claim.Type, Claim.Value }).ToArray();
            return long.Parse(Decrypt(claims.Where(x => x.Type == "username").ToList().FirstOrDefault()!.Value));
        }

    }
}
