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
    public class ApiClienteResponsableUbicacionUsuIdService: IApiClienteResponsableUbicacionUsuIdService
    {

        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiClienteResponsableUbicacionUsuIdService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsClienteResponsableUbicacionUsuIdResponse> obtenerResponsableUbicacion(int usuId)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            
           
                cmd.CommandText = "Api_ClienteResponsableUbicacionUsuId";
               
                cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId;
            
            var ubicacion = new List<clsClienteResponsableUbicacionUsuIdResponse>();
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
        private clsClienteResponsableUbicacionUsuIdResponse MapToValue(SqlDataReader reader)
        {
            
                return new clsClienteResponsableUbicacionUsuIdResponse()
                {

                    cliNombreComercial = reader["cliNombreComercial"].ToString().Trim(),
                    cliNombreFiscal = reader["cliNombreFiscal"].ToString().Trim(),
                    cliCodigo = reader["cliCodigo"].ToString().Trim(),
                    cluLatitud = Convert.ToDecimal(reader["cluLatitud"]),
                    cluLongitud = Convert.ToDecimal(reader["cluLongitud"]),
                    cliDireccionComercial = reader["cliDireccionComercial"].ToString().Trim(),
                    
                };
            
        }
    }
}
