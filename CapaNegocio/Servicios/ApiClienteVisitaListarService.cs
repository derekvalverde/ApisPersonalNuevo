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
    public class ApiClienteVisitaListarService: IApiClienteVisitaListarService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiClienteVisitaListarService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsClienteVisitaListarResponse> obtenerClienteVisitaListar(int usuId, DateTime clgFecha, DateTime clgFechaGPS)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            cmd.CommandText = "Api_ClienteVisitaListar";

            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId; 
           cmd.Parameters.Add("@clgFecha", System.Data.SqlDbType.DateTime).Value = clgFecha;
            cmd.Parameters.Add("@clgFechaGPS", System.Data.SqlDbType.DateTime).Value = clgFechaGPS;
            var ubicacion = new List<clsClienteVisitaListarResponse>();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                ubicacion.Add(MapToValue(reader));
            }
            conn.Close();


            //
            //Si el buscador no tiene elementos
            if (ubicacion == null)
            {
                return null;
            }
            //Si existe Material


            return ubicacion;

        }
        private clsClienteVisitaListarResponse MapToValue(SqlDataReader reader)
        {

            return new clsClienteVisitaListarResponse()
            {

                cliNombreComercial = reader["cliNombreComercial"].ToString().Trim(),
                cliNombreFiscal = reader["cliNombreFiscal"].ToString().Trim(),
                clgLatitud= Convert.ToDecimal(reader["clgLatitud"]),
                clgLongitud=Convert.ToDecimal(reader["clgLongitud"]),
                clgFechaGPS= Convert.ToDateTime(reader["clgFechaGPS"]),
                cluLatitud= Convert.ToDecimal(reader["cluLatitud"]),
                cluLongitud= Convert.ToDecimal(reader["cluLongitud"]),
                distancia= Convert.ToDecimal(reader["distancia"]),


            };

        }
    }
}
