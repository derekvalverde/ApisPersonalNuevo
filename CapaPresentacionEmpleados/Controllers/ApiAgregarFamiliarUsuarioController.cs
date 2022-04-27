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
    public class ApiAgregarFamiliarUsuarioController : ControllerBase
    {
        private IApiAgregarFamiliarUsuarioService _apiAgregarFamiliarUsuarioService;

        public ApiAgregarFamiliarUsuarioController(IApiAgregarFamiliarUsuarioService ApiAgregarFamiliarUsuarioService)
        {
            _apiAgregarFamiliarUsuarioService = ApiAgregarFamiliarUsuarioService;
        }
        [HttpPost]
        [Route("AgregarFamiliarUsuario")]
        public IActionResult Post([FromBody] clsAgregarFamiliarUsuarioRequest model)

        {
            try
            {
                var datos = _apiAgregarFamiliarUsuarioService.actualizarFamiliaresUsuario(model.EmpCodigo, model.FamNombre,model.FamCarnet, model.FamCarnetExt, model.FamCelular, model.FamTipo, model.FamNacimiento, model.FamSexo, model.FamGradoActual, model.FarmCursoActual, model.EmpCodigoIni);

                if (datos == null)
                {
                    return BadRequest(new { message = "Error" });
                }
                return Ok(datos);

            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                clsAgregarFamiliarUsuarioResponse datos = new clsAgregarFamiliarUsuarioResponse();
                datos.actualizado = false;
                return Ok(datos);
            }

        }


    }
}
