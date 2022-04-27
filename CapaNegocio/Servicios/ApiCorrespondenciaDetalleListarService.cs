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
    public class ApiCorrespondenciaDetalleListarService:IApiCorrespondenciaDetalleListarService
    {
        private readonly AppSettings _appSettings;
        private readonly ApicacionDbContextCorrespondencia _context;
        public ApiCorrespondenciaDetalleListarService(IOptions<AppSettings> appSettings, ApicacionDbContextCorrespondencia context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsCorrespondenciaDetalleListarResponse> obtenerCorrespondenciaDetalleListar(string usuCodigo)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            cmd.CommandText = "pa_CorrespondenciaDetalleListar";

            cmd.Parameters.Add("@UsuCodigo", System.Data.SqlDbType.Int).Value = usuCodigo;

            var informacion = new List<clsCorrespondenciaDetalleListarResponse>();
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
        private clsCorrespondenciaDetalleListarResponse MapToValue(SqlDataReader reader)
        {

            return new clsCorrespondenciaDetalleListarResponse()
            {
                cdeId = Convert.ToInt32(reader["CdeId"]),
                cdeCodigo = reader["CdeCodigo"].ToString().Trim(),
                corCodigo = reader["CorCodigo"].ToString().Trim(),
                corRemitente = reader["CorRemitente"].ToString().Trim(),
                cdeDetalles = reader["CdeDetalles"].ToString().Trim(),
                cdeFechaIni = Convert.ToDateTime(reader["CdeFechaIni"]),
                cdeEstado = reader["Cdeestado"].ToString().Trim(),

                destinatario = reader["Destinatario"].ToString().Trim(),
                usuActual = reader["UsuActual"].ToString().Trim(),
                couActual = reader["CouActual"].ToString().Trim(),
            };

        }
    }
}
