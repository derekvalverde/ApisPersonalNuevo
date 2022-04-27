using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CapaDatos.Request;
using CapaDatos.Response;
using CapaNegocio.Servicios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace CapaPresentacionEmpleados.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("ApiAgregar")]
    [ApiController]
    public class ApiAgregarUbicacionUsuarioController : ControllerBase
    {
        private IApiAgregarUbicacionUsuarioService _apiAgregarUbicacionUsuarioService;

        public ApiAgregarUbicacionUsuarioController(IApiAgregarUbicacionUsuarioService ApiAgregarUbicacionUsuarioService)
        {
            _apiAgregarUbicacionUsuarioService = ApiAgregarUbicacionUsuarioService;
        }
        [HttpPost]
        [Route("AgregarUbicacionUsuario")]
        public IActionResult Post([FromBody] clsAgregarUbicacionUsuarioRequest model)

        {
            try
            {
                var ubicacion = _apiAgregarUbicacionUsuarioService.actualizarAgregarUbicacion(model.EmpCodigo, model.EmuZona, model.EmuDireccion, model.EmuLat, model.EmuLong, model.EmpCodigoIni);

                if (ubicacion == null)
                {
                    return BadRequest(new { message = "Error" });
                }
                return Ok(ubicacion);

            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                clsAgregarUbicacionUsuarioResponse ubicacion = new clsAgregarUbicacionUsuarioResponse();
                ubicacion.actualizado = false;
                return Ok(ubicacion);
            }

        }

    }
}
