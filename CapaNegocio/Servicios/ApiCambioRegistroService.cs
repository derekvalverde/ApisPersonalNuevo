using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CapaDatos.Data;
using CapaDatos.Response;
using CapaNegocio.Servicios;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CapaNegocio.Servicios
{
    public class ApiCambioRegistroService: IApiCambioRegistroService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContext1 _context;
        public ApiCambioRegistroService(IOptions<AppSettings> appSettings, AplicacionDbContext1 context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsCambioRegistroResponse actualizarDatosEmpleado(int usuId, string camTabla, string camTipo,string camCampo, int camRegistroId, int camAgrupador, string camAntiguo, string camNuevo)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_CambioRegistro";
           
            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId;
            cmd.Parameters.Add("@camTabla", System.Data.SqlDbType.VarChar, 120).Value = camTabla;
            cmd.Parameters.Add("@camTipo", System.Data.SqlDbType.Char, 1).Value = camTipo;
            cmd.Parameters.Add("@camCampo", System.Data.SqlDbType.VarChar, 100).Value = camCampo;
            cmd.Parameters.Add("@camRegistroId", System.Data.SqlDbType.Int).Value = camRegistroId;
            cmd.Parameters.Add("@camAgrupador", System.Data.SqlDbType.Int).Value = camAgrupador;
            cmd.Parameters.Add("@camAntiguo", System.Data.SqlDbType.VarChar, 100).Value = camAntiguo;
            cmd.Parameters.Add("@camNuevo", System.Data.SqlDbType.VarChar, 150).Value = camNuevo;
            //cmd.Parameters.Add("@camEstado", System.Data.SqlDbType.Char,2).Value = camEstado;
           // cmd.Parameters.Add("@usuIdR", System.Data.SqlDbType.Int).Value = usuIdR;
            //cmd.Parameters.Add("@camFechaInicial", System.Data.SqlDbType.DateTime).Value = camFechaInicial;
            //cmd.Parameters.Add("@camFechaRevision", System.Data.SqlDbType.DateTime).Value = camFechaRevision;





            var datosUsuario = new clsCambioRegistroResponse();
            var reader = cmd.ExecuteReader();

            datosUsuario.actualizado = true;

            conn.Close();

            return datosUsuario;

        }
    }
}
