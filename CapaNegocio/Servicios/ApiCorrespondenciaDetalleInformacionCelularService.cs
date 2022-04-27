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
    public class ApiCorrespondenciaDetalleInformacionCelularService:IApiCorrespondenciaDetalleInformacionCelularService
    {
        private readonly AppSettings _appSettings;
        private readonly ApicacionDbContextCorrespondencia _context;
        public ApiCorrespondenciaDetalleInformacionCelularService(IOptions<AppSettings> appSettings, ApicacionDbContextCorrespondencia context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsCorrespondenciaDetalleInformacionCelularResponse> obtenerCorrespondenciaDetalleInformacionCelular(int CdeId)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            cmd.CommandText = "pa_CorrespondenciaDetalleInformacionCelular";

            cmd.Parameters.Add("@CdeId", System.Data.SqlDbType.Int).Value = CdeId;

            var informacion = new List<clsCorrespondenciaDetalleInformacionCelularResponse>();
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
        private clsCorrespondenciaDetalleInformacionCelularResponse MapToValue(SqlDataReader reader)
        {

            return new clsCorrespondenciaDetalleInformacionCelularResponse()
            {

                nombre = reader["nombre"].ToString().Trim(),
                ubicacion = reader["ubicacion"].ToString().Trim(),
                fecha = Convert.ToDateTime(reader["fecha"]),
              
            };

        }

    }
}
