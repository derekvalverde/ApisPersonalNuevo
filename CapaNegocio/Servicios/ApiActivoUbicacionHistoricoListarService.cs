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
    public class ApiActivoUbicacionHistoricoListarService:IApiActivoUbicacionHistoricoListarService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContextInventario _context;
        public ApiActivoUbicacionHistoricoListarService(IOptions<AppSettings> appSettings, ApplicationDbContextInventario context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsActivoUbicacionHistoricoListarResponse> obtenerActivoUbicacion(string actCodigo)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_ActivoUbicacionHistoricoListar";
            cmd.Parameters.Add("@actCodigo", System.Data.SqlDbType.VarChar,15).Value = actCodigo;

            var activo = new List<clsActivoUbicacionHistoricoListarResponse>();
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
        private clsActivoUbicacionHistoricoListarResponse MapToValue(SqlDataReader reader)
        {

            return new clsActivoUbicacionHistoricoListarResponse()
            {
                usuNombre = reader["usuNombre"].ToString().Trim(),               
                acuFecha = Convert.ToDateTime(reader["acuFecha"]),

            };

        }
    }
}
