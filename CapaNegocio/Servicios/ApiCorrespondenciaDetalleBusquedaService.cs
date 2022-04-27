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
    public class ApiCorrespondenciaDetalleBusquedaService:IApiCorrespondenciaDetalleBusquedaService
    {

        private readonly AppSettings _appSettings;
        private readonly ApicacionDbContextCorrespondencia _context;
        public ApiCorrespondenciaDetalleBusquedaService(IOptions<AppSettings> appSettings, ApicacionDbContextCorrespondencia context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsCorrespondenciaDetalleBusquedaResponse> obtenerCorrespondenciaDetalleBusqueda(string UsuCodigo, string  codigo, string detalle, string remitente, DateTime fechaIni, DateTime fechaFin)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            cmd.CommandText = "pa_CorrespondenciaDetalleBusqueda";

            cmd.Parameters.Add("@UsuCodigo", System.Data.SqlDbType.VarChar,20).Value = UsuCodigo;
            cmd.Parameters.Add("@codigo", System.Data.SqlDbType.VarChar,20).Value = codigo;
            cmd.Parameters.Add("@detalle", System.Data.SqlDbType.VarChar,100).Value = detalle;
            cmd.Parameters.Add("@remitente", System.Data.SqlDbType.VarChar,150).Value = remitente;
            cmd.Parameters.Add("@fechaIni", System.Data.SqlDbType.DateTime).Value = fechaIni;
            cmd.Parameters.Add("@fechaFin", System.Data.SqlDbType.DateTime).Value = fechaFin;

            var busqueda = new List<clsCorrespondenciaDetalleBusquedaResponse>();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                busqueda.Add(MapToValue(reader));
            }
            conn.Close();


            //
            //Si el buscador no tiene elementos
            if (busqueda == null)
            {
                return null;
            }
            //Si existe Material


            return busqueda;

        }
        private clsCorrespondenciaDetalleBusquedaResponse MapToValue(SqlDataReader reader)
        {

            return new clsCorrespondenciaDetalleBusquedaResponse()
            {
                cdeId = Convert.ToInt32(reader["Cdeid"]),
                cdeCodigo = reader["Cdecodigo"].ToString().Trim(),
                cdeDestinatario = reader["CdeDestinatario"].ToString().Trim(),
                cdeDetalles = reader["Cdedetalles"].ToString().Trim(),
                corRemitente = reader["corRemitente"].ToString().Trim(),
               corCodigo= reader["corCodigo"].ToString().Trim(),
                
                cdeFechaIni = Convert.ToDateTime(reader["CdeFechaIni"]),
                usunombre = reader["usunombre"].ToString().Trim(),
                couActual = reader["CouActual"].ToString().Trim(),
                cdeEstado = reader["CdeEstado"].ToString().Trim(),
                
                
            };

        }

    }
}
