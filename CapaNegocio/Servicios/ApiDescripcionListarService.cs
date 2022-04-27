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
    public class ApiDescripcionListarService:IApiDescripcionListarService
    {

        private readonly AppSettings _appSettings;
        private readonly ApicacionDbContextCorrespondencia _context;
        public ApiDescripcionListarService(IOptions<AppSettings> appSettings, ApicacionDbContextCorrespondencia context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsDescripcionListarResponse> obtenerDescripcionListar()
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "pa_DescripciónListar";

            var descripcionC = new List<clsDescripcionListarResponse>();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                descripcionC.Add(MapToValue(reader));
            }
            conn.Close();
            //
            //Si el buscador no tiene elementos
            if (descripcionC == null)
            {
                return null;
            }
            //Si existe Material


            return descripcionC;

        }
        private clsDescripcionListarResponse MapToValue(SqlDataReader reader)
        {

            return new clsDescripcionListarResponse()
            {

                Codid = Convert.ToInt32(reader["CodId"]),
                codnombre = reader["CodNombre"].ToString().Trim(),
                

            };

        }
    }
}
