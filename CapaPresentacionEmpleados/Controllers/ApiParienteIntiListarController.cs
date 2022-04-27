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
    [Route("ApiPariente")]
    [ApiController]
    public class ApiParienteIntiListarController : ControllerBase
    {

        private IApiParienteIntiListarService _apiParienteIntiListarService;

        public ApiParienteIntiListarController(IApiParienteIntiListarService ApiParienteIntiListarService)
        {
            _apiParienteIntiListarService = ApiParienteIntiListarService;
        }

        [HttpPost]
        [Route("ParienteIntiListar")]
        public IActionResult Post([FromBody] clsUtilitarioListarRequest model)
        {
            try
            {
                var datosEmp = _apiParienteIntiListarService.obtenerParienteIntiListar(model.usuId);
                if (datosEmp == null)
                {
                    return BadRequest(new { message = "No existe" });
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
