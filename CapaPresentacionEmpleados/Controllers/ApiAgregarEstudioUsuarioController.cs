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
    public class ApiAgregarEstudioUsuarioController : ControllerBase
    {
        private IApiAgregarEstudioUsuarioService _apiAgregarEstudioUsuarioService;

        public ApiAgregarEstudioUsuarioController(IApiAgregarEstudioUsuarioService ApiAgregarEstudioUsuarioService)
        {
            _apiAgregarEstudioUsuarioService = ApiAgregarEstudioUsuarioService;
        }
        [HttpPost]
        [Route("AgregarEstudioUsuario")]
        public IActionResult Post([FromBody] clsAgregarEstudioUsuarioRequest model)

        {
            try
            {
                var datos = _apiAgregarEstudioUsuarioService.actualizarEstudiosUsuario(model.EmpCodigo, model.EstInstitucion, model.EstFecha, model.EstFechaFin, model.EstExplicacion, model.EstNombre, model.EstNivel, model.EttId, model.EmpCodigoIni);

                if (datos == null)
                {
                    return BadRequest(new { message = "Error" });
                }
                return Ok(datos);

            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                clsAgregarEstudioUsuarioResponse datos = new clsAgregarEstudioUsuarioResponse();
                datos.actualizado = false;
                return Ok(datos);
            }

        }

    }
}
