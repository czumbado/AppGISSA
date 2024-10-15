using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Gissa.Entities;
using System.Net.Http.Headers;


namespace Gissa.Models
{
    public class UsuarioModel : IUsuarioModel
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private string _urlApi;

        public UsuarioModel(HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _HttpContextAccessor = httpContextAccessor;
            _urlApi = _configuration.GetSection("Llaves:urlApi").Value;
        }

        public Usuario? IniciarSesion(Usuario entidad)
        {
            string url = _urlApi + "api/Login/IniciarSesion";
            JsonContent obj = JsonContent.Create(entidad);
            var resp = _httpClient.PostAsync(url, obj).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<Usuario>().Result;
            else
                return null;
        }

        public int RegisterAccount(Usuario entidad)
        {
            string url = _urlApi + "api/Login/RegistrerAccount";
            JsonContent obj = JsonContent.Create(entidad);
            var resp = _httpClient.PostAsync(url, obj).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<int>().Result;
            else
                return 0;
        }



        public List<SelectListItem>? ConsultNationality()
        {
            string url = _urlApi + "api/Login/ConsultNationality";
            var resp = _httpClient.GetAsync(url).Result;
            return resp.Content.ReadFromJsonAsync<List<SelectListItem>?>().Result;

        }
        
        //public List<SelectListItem>? test_ConsultSkills()
        //{
        //    string url = _urlApi + "api/Login/test_ConsultSkills";
        //    var resp = _httpClient.GetAsync(url).Result;
        //    return resp.Content.ReadFromJsonAsync<List<SelectListItem>?>().Result;

        //}
        public int ActualizarPerfil(Usuario entidad)
        {
            string url = _urlApi + "api/Usuario/ActualizarPerfil";
            JsonContent obj = JsonContent.Create(entidad);
            var resp = _httpClient.PostAsync(url, obj).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<int>().Result;
            else
                return 0;
        }

        public int? ChangePassword(UsuarioRecoverEnt entidad)
        {
            string url = _urlApi + "api/Login/ChangePassword";
            JsonContent obj = JsonContent.Create(entidad);
            var resp = _httpClient.PostAsync(url, obj).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<int>().Result;
            else
                return 0;
        }

        public int RecoverAccount(Usuario entidad)
        {
            string url = _urlApi + "api/Login/RecoverAccount";
            JsonContent obj = JsonContent.Create(entidad);
            var resp = _httpClient.PostAsync(url, obj).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<int>().Result;
            else
                return 0;
        }
        public List<Usuario>? ConsultUsers()
        {
            string url = _urlApi + "api/Usuario/ConsultUsers";

            var resp = _httpClient.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<List<Usuario>>().Result;
            else
                return null;
        }

        public int UpdateState(Usuario entidad)
        {
            string url = _urlApi + "api/Usuario/UpdateState";
            JsonContent obj = JsonContent.Create(entidad);
            var resp = _httpClient.PutAsync(url, obj).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<int>().Result;
            else
                return 0;
        }

        public int ChangeRol(Usuario entidad)
        {
            string url = _urlApi + "api/Usuario/ChangeRol";
            JsonContent obj = JsonContent.Create(entidad);
            var resp = _httpClient.PutAsync(url, obj).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<int>().Result;
            else
                return 0;
        }
        public int DeleteUser(int IdUser)
        {
            string url = _urlApi + "api/Usuario/DeleteUser?IdUser=" + IdUser;
            var resp = _httpClient.DeleteAsync(url).Result;


            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<int>().Result;
            else
                return 0;
        }
        public int UpdateUser(Usuario entidad)
        {
            string url = _urlApi + "api/Usuario/UpdateUser";

            JsonContent obj = JsonContent.Create(entidad);
            var resp = _httpClient.PutAsync(url, obj).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<int>().Result;
            else
                return 0;
        }
        public List<SelectListItem>? ConsultRol()
        {
            string url = _urlApi + "api/Usuario/ConsultRol";
            var resp = _httpClient.GetAsync(url).Result;
            return resp.Content.ReadFromJsonAsync<List<SelectListItem>?>().Result;

        }

    }
}
