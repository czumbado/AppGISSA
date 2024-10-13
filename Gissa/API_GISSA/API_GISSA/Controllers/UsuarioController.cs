using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using API_GISSA.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace API_GISSA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUtilitarios _utilitarios;
        private string _connection;

        public UsuarioController(IConfiguration configuration, IUtilitarios utilitarios)
        {
            _configuration = configuration;
            _connection = _configuration.GetConnectionString("DefaultConnection");
            _utilitarios = utilitarios;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("ActualizarPerfil")]
        public IActionResult ActualizarPerfil(UserEnt entidad)
        {
            try
            {
                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Execute("ActualizarPerfil",
                       new { entidad.NameUser, entidad.LastName, entidad.Email, entidad.Phone, entidad.Identification },
                        commandType: CommandType.StoredProcedure);
                    
                    return Ok(datos);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]     
        [Route("ConsultUsers")]
        public IActionResult ConsultUsers(int IdUser)
        {
            try
            {
                //long IdUser = long.Parse(_utilitarios.Decrypt(User.Identity.Name.ToString()));
                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Query<UserEnt>("ConsultUsers",
                        new { IdUser },
                        commandType: CommandType.StoredProcedure).ToList();

                    return Ok(datos);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]  
        [Route("UpdateState")]
        public IActionResult UpdateState(UserEnt entidad)
        {
            try
            {
                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Execute("UpdateState",
                        new { entidad.IdUser },
                        commandType: CommandType.StoredProcedure);

                    return Ok(datos);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("ChangeRol")]
        public IActionResult ChangeRol(UserEnt entidad)
        {
            try
            {
                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Execute("ChangeRol",
                        new { entidad.IdUser },
                        commandType: CommandType.StoredProcedure);

                    return Ok(datos);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteUser")]
        public IActionResult DeleteUser(int IdUser)
        {
            try
            {

                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Execute("DeleteUser",
                        new { IdUser },
                        commandType: CommandType.StoredProcedure);
                    return Ok(datos);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPut]
        [Route("UpdateUser")]
        public IActionResult UpdateUser(UserEnt entidad)
        {
            try
            {
                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Execute("UpdateUser",
                        new { entidad.Identification, entidad.NameUser, entidad.LastName, entidad.Email, entidad.IdRol, entidad.IdUser },
                        commandType: CommandType.StoredProcedure);

                    return Ok(datos);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("ConsultRol")]
        public IActionResult ConsultRol()
        {
            try
            {

                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Query<SelectListItem>("ConsultRol",
                        new { },
                        commandType: CommandType.StoredProcedure).ToList();
                    return Ok(datos);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}




