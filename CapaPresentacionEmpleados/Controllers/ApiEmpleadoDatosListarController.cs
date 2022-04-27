using CapaDatos.Request;
using CapaNegocio.Servicios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CapaPresentacionEmpleados.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("ApiEmpleado")]
    [ApiController]
    public class ApiEmpleadoDatosListarController : ControllerBase
    {

        private IApiEmpleadoDatosListarService _apiEmpleadoDatosListarService;
        private IApiEmpleadoUbicacionListarService _apiEmpleadoUbicacionListarService;
        private IApiEmpleadoListarService _apiEmpleadoListarService;

        public ApiEmpleadoDatosListarController(
            IApiEmpleadoDatosListarService ApiEmpleadoDatosListarService,
            IApiEmpleadoUbicacionListarService ApiEmpleadoUbicacionListarService,
            IApiEmpleadoListarService ApiEmpleadoListarService
            )
        {
            _apiEmpleadoDatosListarService = ApiEmpleadoDatosListarService;
            _apiEmpleadoUbicacionListarService = ApiEmpleadoUbicacionListarService;
            _apiEmpleadoListarService = ApiEmpleadoListarService;
        }

        [HttpPost]
        [Route("EmpleadoDatosListar")]
        public IActionResult Post([FromBody] clsUtilitarioListarRequest model)
        {
            try
            {
                var datosEmp = _apiEmpleadoDatosListarService.obtenerDatosEmpleados(model.usuId);
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
        [HttpPost]
        [Route("EmpleadoUbicacionListar")]
        public IActionResult Post([FromBody] clsEmpleadoUbicacionListarRequest model)
        {
            try
            {
                var ubicacionEmp = _apiEmpleadoUbicacionListarService.obtenerUsuarioUbicacion(model.usuId);
                if (ubicacionEmp == null)
                {
                    return BadRequest(new { message = "No existe ubicacion" });
                }
                return Ok(ubicacionEmp);
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                return BadRequest(new { message = "Error al ejecutar Procedimiento Almacenado" });
            }
        }
        [HttpPost]
        [Route("EmpleadoListar")]
        public IActionResult Post()     
        {
            try
            {
                var datosEmp = _apiEmpleadoListarService.obtenerListaEmpleados();
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
