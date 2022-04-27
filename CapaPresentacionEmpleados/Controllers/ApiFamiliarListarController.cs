using CapaDatos.Request;
using CapaDatos.Response;
using CapaNegocio.Servicios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;


namespace CapaPresentacionEmpleados.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("ApiFamiliar")]
    [ApiController]
    public class ApiFamiliarListarController : ControllerBase
    {
        private IApiFamiliarListarService _apiFamiliarListarService;
        private IApiAgregarFamiliarUsuarioService _apiAgregarFamiliarUsuarioService;
        public ApiFamiliarListarController(
            IApiFamiliarListarService ApiFamiliarListarService,
            IApiAgregarFamiliarUsuarioService ApiAgregarFamiliarUsuarioService

            )
        {
            _apiFamiliarListarService = ApiFamiliarListarService;
            _apiAgregarFamiliarUsuarioService = ApiAgregarFamiliarUsuarioService;

        }

        [HttpPost]
        [Route("FamiliarListar")]
        public IActionResult Post([FromBody] clsUtilitarioListarRequest model)
        {
            try
            {
                var datosEmp = _apiFamiliarListarService.obtenerUsuarioFamiliares(model.usuId);
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
        [HttpPost]
        [Route("FamiliarAgregarUsuario")]
        public IActionResult Post([FromBody] clsAgregarFamiliarUsuarioRequest model)

        {
            try
            {
                var datos = _apiAgregarFamiliarUsuarioService.actualizarFamiliaresUsuario(model.EmpCodigo, model.FamNombre, model.FamCarnet, model.FamCarnetExt, model.FamCelular, model.FamTipo, model.FamNacimiento, model.FamSexo, model.FamGradoActual, model.FarmCursoActual, model.EmpCodigoIni);

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
