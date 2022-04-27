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
    public class ApiActivoBuscarService:IApiActivoBuscarService
    {

        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContextInventario _context;
        public ApiActivoBuscarService(IOptions<AppSettings> appSettings, ApplicationDbContextInventario context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsActivoBuscarResponse> obtenerActivoBuscar(string actCodigo, string actDenomi)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_ActivoBuscar";
            cmd.Parameters.Add("@actCodigo", System.Data.SqlDbType.VarChar, 15).Value = actCodigo;
            cmd.Parameters.Add("@actDenomi", System.Data.SqlDbType.VarChar, 150).Value = actDenomi;
            var activo = new List<clsActivoBuscarResponse>();
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
        private clsActivoBuscarResponse MapToValue(SqlDataReader reader)
        {

            return new clsActivoBuscarResponse()
            {
                actCodigo = reader["actCodigo"].ToString().Trim(),
                actDenominacion = reader["actDenominacion"].ToString().Trim(),
                acuFecha = Convert.ToDateTime(reader["acuFecha"]),

            };

        }
    }
}
