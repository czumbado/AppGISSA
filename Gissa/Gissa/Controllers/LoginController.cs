using Microsoft.AspNetCore.Mvc;
using Gissa.Entities;
using Gissa.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gissa.Controllers
{
    [ResponseCache(NoStore = true, Duration = 0)]
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IUsuarioModel _usuarioModel;
        readonly IConfiguration _configuration;

        public LoginController(ILogger<LoginController> logger, IUsuarioModel usuarioModel, IConfiguration configuration)
        {
            _logger = logger;
            _usuarioModel = usuarioModel;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult IniciarSesion()
        {
            ClaimsPrincipal c = HttpContext.User;
            if (c.Identity != null)
            {
                if (c.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        public IActionResult ChangePassword(string email, string? mensaje)
        {
            if (email != null)
            {
                if (mensaje != null)
                {
                    ViewBag.MensajePantalla = "Ocurrió un error validando la información";
                }
                ViewBag.Email = email;
                return View();
            }
            else
            {
                return RedirectToAction("IniciarSesion", "Login");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Change(UsuarioRecoverEnt entidad)
        {
            try
            {
                if (entidad.PasswordUser != entidad.ConfirmPassword)
                {
                    return RedirectToAction("ChangePassword", "Login", new { email = entidad.Email, mensaje = "Las contraseñas no son iguales" });
                }
                var resp = _usuarioModel.ChangePassword(entidad);

                return RedirectToAction("IniciarSesion", "Login");
            }
            catch (System.Exception ex)
            {
                return RedirectToAction("ChangePassword", "Login", new { email = entidad.Email, mensaje = "Ocurrió un error validando la información" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> IniciarSesion(Usuario entidad)
        {
            try
            {
                if (string.IsNullOrEmpty(entidad.Email) || string.IsNullOrEmpty(entidad.PasswordUser))
                {
                    ViewBag.MensajePantalla = "Introduce los datos obligatorios";
                    return View();
                }
                if (ModelState.IsValid)
                {
                    var resp = _usuarioModel.IniciarSesion(entidad);

                    if (resp != null)
                    {
                        HttpContext.Session.SetInt32("NombreRol", (int)resp.IdRol);

                        if (resp.NameUser != null)
                        {
                            List<Claim> c = new List<Claim>(){
                        new Claim(ClaimTypes.NameIdentifier, resp.NameUser),
                        new Claim("LastName", resp.LastName),
                        new Claim("Email", resp.Email),
                        new Claim("Phone", resp.Phone),
                        new Claim("Identification", resp.Identification),
                        new Claim("IdUser", resp.IdUser.ToString())
                    };
                            ClaimsIdentity ci = new(c, CookieAuthenticationDefaults.AuthenticationScheme);
                            AuthenticationProperties p = new();
                            p.AllowRefresh = true;
                            p.IsPersistent = true;
                            //La sesión dura activa máximo 1 día
                            p.ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1);

                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(ci), p);

                            HttpContext.Session.SetInt32("NombreRol", (int)resp.IdRol);
                            HttpContext.Session.SetString("NombreUsuario", resp.NameUser);
                            HttpContext.Session.SetString("TokenUsuario", resp.Token);

                            return RedirectToAction("Index", "Home");
                        }
                        ViewBag.MensajePantalla = "Correo o contraseña incorrectos";
                        return View();
                    }
                    else
                    {
                        ViewBag.MensajePantalla = "Correo o contraseña incorrectos";
                        return View();
                    }
                }
            }
            catch (System.Exception ex)
            {
                ViewBag.MensajePantalla = "Ocurrió un error validando la sesión";
            }
            return View();
        }


        [Seguridad]
        public async Task<IActionResult> CerrarSesion()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("IniciarSesion", "Login");
        }

        [HttpGet]
        public IActionResult RegisterAccount()
        {
            ViewBag.XYZ = _usuarioModel.ConsultNationality();
            return View();
        }

        [HttpPost]
        public IActionResult RegisterAccount(Usuario entidad)
        {
            var resp = _usuarioModel.RegisterAccount(entidad);

            // Inicializar ViewBag.XYZ para asegurar su disponibilidad en la vista
            ViewBag.XYZ = _usuarioModel.ConsultNationality();

            if (resp == 1)
            {
                // Registro exitoso, redireccionar a la página de inicio de sesión
                return RedirectToAction("IniciarSesion", "Login");
            }
            else if (resp == -1)
            {
                // Usuario ya existe, mostrar mensaje de error
                ViewBag.UserExistsError = "El correo electrónico ya está registrado.";
                return View();
            }
            else
            {
                // Ocurrió un error durante el registro, mostrar mensaje de error
                ViewBag.MensajePantalla = "No se pudo registrar su cuenta";
                return View();
            }
        }


        [HttpGet]
        public IActionResult RecoverAccount()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RecoverAccount(Usuario user)
        {
            var resp = _usuarioModel.RecoverAccount(user);

            if (resp == 1)
                return RedirectToAction("ChangePassword", "Login", new { email = user.Email });
            else
            {
                ViewBag.MensajePantalla = "No se pudo validar su cuenta";
                return View();
            }
        }
    }
}