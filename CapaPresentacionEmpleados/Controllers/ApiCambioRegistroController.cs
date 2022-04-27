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
    [Route("ApiCambio")]
    [ApiController]
    public class ApiCambioRegistroController : ControllerBase
    {
        private IApiCambioRegistroService _apiCambioRegistroService;

        public ApiCambioRegistroController(IApiCambioRegistroService ApiCambioRegistroService)
        {
            _apiCambioRegistroService = ApiCambioRegistroService;
        }
        [HttpPost]
        [Route("CambioRegistro")]
        public IActionResult Post([FromBody] clsCambioRegistroRequest model)

        {
            try
            {
                var datos = _apiCambioRegistroService.actualizarDatosEmpleado( model.usuId, model.camTabla, model.camTipo, model.camCampo,model.camRegistroId,model.camAgrupador, model.camAntiguo, model.camNuevo);

                if (datos == null)
                {
                    return BadRequest(new { message = "Error" });
                }
                return Ok(datos);

            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                clsCambioRegistroResponse datos = new clsCambioRegistroResponse();
                datos.actualizado = false;
                return Ok(datos);
            }


        }

    }
}
