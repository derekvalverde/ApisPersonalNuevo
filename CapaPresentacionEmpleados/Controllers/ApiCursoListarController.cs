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
    [Route("ApiCurso")]
    [ApiController]
    public class ApiCursoListarController : ControllerBase
    {

        private IApiCursoListarService _apiCursoListarService;

        public ApiCursoListarController(IApiCursoListarService ApiCursoListarService)
        {
            _apiCursoListarService = ApiCursoListarService;
        }

        [HttpPost]
        [Route("CursoListar")]
        public IActionResult Post([FromBody] clsUtilitarioListarRequest model)
        {
            try
            {
                var datosEmp = _apiCursoListarService.obtenerUsuarioCurso(model.usuId);
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
