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
    [Route("ApiUtilitario")]
    [ApiController]
    public class ApiUtilitarioListarEmpleadoController : ControllerBase
    {
        private IApiUtilitarioListarEmpleadoService _apiUtilitarioListarEmpleadoService;

        public ApiUtilitarioListarEmpleadoController(IApiUtilitarioListarEmpleadoService ApiUtilitarioListarEmpleadoService)
        {
            _apiUtilitarioListarEmpleadoService = ApiUtilitarioListarEmpleadoService;
        }

        [HttpPost]
        [Route("UtilitarioListarEmpleado")]
        public IActionResult Post([FromBody] clsUtilitarioListarEmpleadoRequest model)
        {
            try
            {
                var datosEmp = _apiUtilitarioListarEmpleadoService.obtenerUtilitarioListar(model.aux);
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
