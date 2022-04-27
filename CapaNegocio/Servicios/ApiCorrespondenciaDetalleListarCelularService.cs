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
    public class ApiCorrespondenciaDetalleListarCelularService:IApiCorrespondenciaDetalleListarCelularService
    {
        private readonly AppSettings _appSettings;
        private readonly ApicacionDbContextCorrespondencia _context;
        public ApiCorrespondenciaDetalleListarCelularService(IOptions<AppSettings> appSettings, ApicacionDbContextCorrespondencia context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsCorrespondenciaDetalleListarCelularResponse> obtenerCorrespondenciaDetalleListarCelular(string UsuCodigo)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            cmd.CommandText = "pa_CorrespondenciaDetalleListarCelular";

            cmd.Parameters.Add("@UsuCodigo", System.Data.SqlDbType.VarChar, 20).Value = UsuCodigo;

            var informacion = new List<clsCorrespondenciaDetalleListarCelularResponse>();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                informacion.Add(MapToValue(reader));
            }
            conn.Close();


            //
            //Si el buscador no tiene elementos
            if (informacion == null)
            {
                return null;
            }
            //Si existe Material


            return informacion;

        }
        private clsCorrespondenciaDetalleListarCelularResponse MapToValue(SqlDataReader reader)
        {

            return new clsCorrespondenciaDetalleListarCelularResponse()
            {
                cdeId = Convert.ToInt32(reader["CdeId"]),
                cdeCodigo = reader["CdeCodigo"].ToString().Trim(),
                corRemitente = reader["CorRemitente"].ToString().Trim(),
                cdeDetalles = reader["CdeDetalles"].ToString().Trim(),
                cdeFechaIni = Convert.ToDateTime(reader["CdeFechaIni"]),
                cdeEstado = reader["Cdeestado"].ToString().Trim(),
            };

        }


    }
}
