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
    public class ApiActivoUbicacionUsuarioListarService:IApiActivoUbicacionUsuarioListarService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContextInventario _context;
        public ApiActivoUbicacionUsuarioListarService(IOptions<AppSettings> appSettings, ApplicationDbContextInventario context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsActivoUbicacionUsuarioListarResponse> obtenerActivoUbicacion(int usuId)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_ActivoUbicacionUsuarioListar";
            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId;

            var activo = new List<clsActivoUbicacionUsuarioListarResponse>();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                activo.Add(MapToValue(reader));
            }
            conn.Close();
            //
            //Si el buscador no tiene elementos
            if (activo == null)
            {
                return null;
            }
            //Si existe Material


            return activo;

        }
        private clsActivoUbicacionUsuarioListarResponse MapToValue(SqlDataReader reader)
        {

            return new clsActivoUbicacionUsuarioListarResponse()
            {

                actCodigo = reader["actCodigo"].ToString().Trim(),
                actDenominacion = reader["actDenominacion"].ToString().Trim(),
                acuFecha = Convert.ToDateTime(reader["acuFecha"]),
                actEstado = reader["actEstado"].ToString().Trim(),
            };

        }
    }
}
