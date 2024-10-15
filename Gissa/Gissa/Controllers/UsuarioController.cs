using Gissa.Models;
using Microsoft.AspNetCore.Mvc;
using Gissa.Entities;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Gissa.Controllers
{
    public class UsuarioController : Controller
    {

        private readonly ILogger<UsuarioController> _logger;
        private readonly IUsuarioModel _usuarioModel;
        //private readonly ITypeReviewModel _typeReviewModel;
        private readonly IJSRuntime _jsRuntime;

        public UsuarioController(ILogger<UsuarioController> logger, IUsuarioModel usuarioModel, IJSRuntime jsRuntime)
        {
            _logger = logger;
            _usuarioModel = usuarioModel;         
            _jsRuntime = jsRuntime; 
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult ActualizarPerfil()
        {
            if (User.Identity.IsAuthenticated)
            {
                // Obtener el nombre de usuario
                string userName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                string lastName = User.FindFirst("LastName")?.Value;
                string email = User.FindFirst("Email")?.Value;
                string phone = User.FindFirst("Phone")?.Value;
                string identification = User.FindFirst("Identification")?.Value;

                Usuario usuario = new Usuario();
                usuario.NameUser = userName;
                usuario.LastName = lastName;
                usuario.Email = email;
                usuario.Phone = phone;
                usuario.Identification = identification;
                return View(usuario);
            }
            else
            {
                // Si el usuario no está autenticado, redirigirlo al inicio de sesión
                return RedirectToAction("IniciarSesion", "Login");
            }
            return View();
        }

        [Authorize]
        [HttpPost]
        public int ActualizarPerfil(Usuario usuario)
        {
            try
            {
                var resp = _usuarioModel.ActualizarPerfil(usuario);
                if (resp == -1)
                {
                    ViewBag.MensajePantalla = "El correo ya está en uso";
                    return -1;
                    //return View();
                }
                var httpContext = HttpContext;
                if (resp != null)
                {
                    var identity = (ClaimsIdentity)httpContext.User.Identity;

                    var oldNameIdentifierClaim = identity.FindFirst(ClaimTypes.NameIdentifier);
                    if (oldNameIdentifierClaim != null)
                    {
                        identity.RemoveClaim(oldNameIdentifierClaim);
                    }

                    // Agregar el nuevo reclamo NameIdentifier (si es necesario)
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.NameUser));

                    var oldLastNameClaim = identity.FindFirst("LastName");

                    if (oldLastNameClaim != null)
                    {
                        identity.RemoveClaim(oldLastNameClaim);
                        identity.AddClaim(new Claim("LastName", usuario.LastName));
                    }

                    var oldEmailClaim = identity.FindFirst("Email");

                    if (oldEmailClaim != null)
                    {
                        identity.RemoveClaim(oldEmailClaim);
                        identity.AddClaim(new Claim("Email", usuario.Email));

                    }

                    var oldPhoneClaim = identity.FindFirst("Phone");

                    if (oldPhoneClaim != null)
                    {
                        identity.RemoveClaim(oldPhoneClaim);
                        identity.AddClaim(new Claim("Phone", usuario.Phone));
                    }
                    // Actualizar la sesión del usuario
                    httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));



                    return 1;

                }
  
                return 0;
 
            }
            catch (System.Exception ex)
            {

                return 0;

            }
        }



        [HttpGet]
        public IActionResult ConsultUsers(int page = 1)
        {
            const int pageSize = 4;
            var datos = _usuarioModel.ConsultUsers();

            var paginatedData = datos
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(datos.Count / (double)pageSize);

            return View(paginatedData);
        }

        [HttpGet]

        public IActionResult UpdateState(long q)
        {
            var entidad = new Usuario();
            entidad.IdUser = q;

            _usuarioModel.UpdateState(entidad);
            return RedirectToAction("ConsultUsers", "Usuario");
        }

        [HttpGet]

        public IActionResult ChangeRol(long q)
        {
            var entidad = new Usuario();
            entidad.IdUser = q;

            _usuarioModel.ChangeRol(entidad);
            return RedirectToAction("ConsultUsers", "Usuario");
        }

        [HttpGet]
        public IActionResult UpdateUser(long q)
        {
            var roles = _usuarioModel.ConsultRol();
            var userData = _usuarioModel.ConsultUsers().FirstOrDefault(x => x.IdUser == q);

            if (userData != null)
            {
                ViewBag.ROL = new SelectList(roles, "Value", "Text", userData.IdRol);
            }
            else
            {
                ViewBag.ROL = new SelectList(roles, "Value", "Text");
            }

            return View(userData);
        }


        [HttpPost]
        public IActionResult UpdateUser(Usuario entidad)
        {

            var resp = _usuarioModel.UpdateUser(entidad);

            if (resp == 1)
            {
                return RedirectToAction("ConsultUsers", "Usuario");
            }
            else
            {
                ViewBag.MensajePantalla = "No se pudo actualizar el Usuario";
                return View();
            }
        }

        [HttpPost]
        public IActionResult DeleteUser(int IdUser)
        {
            var delete = _usuarioModel.DeleteUser(IdUser);
            return Json(delete);
        }
    }
}