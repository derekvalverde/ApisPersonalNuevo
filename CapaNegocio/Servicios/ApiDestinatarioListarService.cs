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
    public class ApiDestinatarioListarService:IApiDestinatarioListarService
    {

        private readonly AppSettings _appSettings;
        private readonly ApicacionDbContextCorrespondencia _context;
        public ApiDestinatarioListarService(IOptions<AppSettings> appSettings, ApicacionDbContextCorrespondencia context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsDestinatarioListarResponse> obteneDestinatarioListar(string usuCodigo)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            cmd.CommandText = "pa_DestinatarioListar";

            cmd.Parameters.Add("@UsuCodigo", System.Data.SqlDbType.Int).Value = usuCodigo;

            var destinatario = new List<clsDestinatarioListarResponse>();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                destinatario.Add(MapToValue(reader));
            }
            conn.Close();


            //
            //Si el buscador no tiene elementos
            if (destinatario == null)
            {
                return null;
            }
            //Si existe Material


            return destinatario;

        }
        private clsDestinatarioListarResponse MapToValue(SqlDataReader reader)
        {

            return new clsDestinatarioListarResponse()
            {

                usuId = Convert.ToInt32(reader["UsuId"]),
                usuNombre = reader["UsuNombre"].ToString().Trim(),
               

            };

        }

    }
}
