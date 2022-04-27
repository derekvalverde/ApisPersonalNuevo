using CapaDatos.Request;
using CapaNegocio.Servicios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CapaPresentacionEmpleados.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("ApiUsuario")]
    [ApiController]
    public class ApiUsuarioCambioVerificarController: ControllerBase
    {

        private IApiUsuarioCambioVerificarService _apiUsuarioCambioVerificarService;

        public ApiUsuarioCambioVerificarController(IApiUsuarioCambioVerificarService ApiUsuarioCambioVerificarService)
        {
            _apiUsuarioCambioVerificarService = ApiUsuarioCambioVerificarService;
        }

        [HttpPost]
        [Route("UsuarioCambioVerificar")]
        public IActionResult Post([FromBody] clsUsuarioCambioVerificarRequest model)
        {
            try
            {
                var datosEmp = _apiUsuarioCambioVerificarService.verificaCambios(model.usuId);
                if (datosEmp == null)
                {
                    return BadRequest(new { message = "No existe datos" });
                }
                return Ok(datosEmp);
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                return BadRequest(new { message = "Error al ejecutar Procedimiento Almacenado" });
            }


        }
    }
}
