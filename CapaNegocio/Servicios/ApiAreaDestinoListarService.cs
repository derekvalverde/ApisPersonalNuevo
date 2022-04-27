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
    public class ApiAreaDestinoListarService:IApiAreaDestinoListarService
    {

        private readonly AppSettings _appSettings;
        private readonly ApicacionDbContextCorrespondencia _context;
        public ApiAreaDestinoListarService(IOptions<AppSettings> appSettings, ApicacionDbContextCorrespondencia context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsAreaDestinoListarResponse> obtenerAreaDestinoListar()
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "pa_AreaDestinoListar";

            var destinoC = new List<clsAreaDestinoListarResponse>();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                destinoC.Add(MapToValue(reader));
            }
            conn.Close();
            //
            //Si el buscador no tiene elementos
            if (destinoC == null)
            {
                return null;
            }
            //Si existe Material


            return destinoC;

        }
        private clsAreaDestinoListarResponse MapToValue(SqlDataReader reader)
        {

            return new clsAreaDestinoListarResponse()
            {

                areid = Convert.ToInt32(reader["areid"]),
                AreNombre = reader["AreNombre"].ToString().Trim(),


            };

        }

    }
}
