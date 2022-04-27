using CapaDatos.Request;
using CapaNegocio.Servicios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;


namespace CapaPresentacionEmpleados.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("ApiConocimientoOtro")]
    [ApiController]
    public class ApiConocimientoOtroListarController : ControllerBase
    {
        private IApiConocimientoOtroListarService _apiConocimientoOtroListarService;

        public ApiConocimientoOtroListarController(IApiConocimientoOtroListarService apiConocimientoOtroListarService)
        {
            _apiConocimientoOtroListarService = apiConocimientoOtroListarService;
        }

        [HttpPost]
        [Route("ConocimientoOtroListar")]
        public IActionResult Post([FromBody] clsUtilitarioListarRequest model)
        {
            try
            {
                var datosEmp = _apiConocimientoOtroListarService.ObtenerUsuarioEstudiosOtro(model.usuId);
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
