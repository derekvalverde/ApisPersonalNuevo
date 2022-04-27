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
    [Route("Estudio")]
    [ApiController]
    public class ApiEstudioListarController : ControllerBase
    {
        private IApiEstudioListarService _apiEstudioListarService;

        public ApiEstudioListarController(IApiEstudioListarService ApiEstudioListarService)
        {
            _apiEstudioListarService = ApiEstudioListarService;
        }

        [HttpPost]
        [Route("EstudioListar")]
        public IActionResult Post([FromBody] clsUtilitarioListarRequest model)
        {
            try
            {
                var datosEmp = _apiEstudioListarService.obtenerUsuarioEstudios(model.usuId);
                if (datosEmp == null)
                {
                    return BadRequest(new { message = "No existe la pendientes" });
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
