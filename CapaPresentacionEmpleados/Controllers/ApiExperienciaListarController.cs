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
    [Route("Experiencia")]
    [ApiController]
    public class ApiExperienciaListarController : ControllerBase
    {
        private IApiExperienciaListarService _apiExperienciaListarService;

        public ApiExperienciaListarController(IApiExperienciaListarService ApiExperienciaListarService)
        {
            _apiExperienciaListarService = ApiExperienciaListarService;
        }

        [HttpPost]
        [Route("ExperienciaListar")]
        public IActionResult Post([FromBody] clsUtilitarioListarRequest model)
        {
            try
            {
                var datosEmp = _apiExperienciaListarService.obtenerUsuarioExperiencia(model.usuId);
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
