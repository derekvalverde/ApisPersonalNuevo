using System;
using System.Collections.Generic;
using CapaDatos.Data;
using CapaDatos.Response;
using Microsoft.Extensions.Options;
using WebIntiApi.Models;
using CapaDatos.Request;
using CapaDatos.Models;
using System.IO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CapaNegocio.Servicios
{
    public class ApiUsuarioReseteoPasswordService:IApiUsuarioReseteoPasswordService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiUsuarioReseteoPasswordService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsUsuarioReseteoPasswordResponse obtenerNuevoPassword(int usuId, string usuPassword, string usuPasswordNuevo, string aplicacion)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            if(aplicacion == "PC")
            {
                cmd.CommandText = "Api_UsuarioClienteReseteoPassword";
                cmd.Parameters.Add("@uclId", System.Data.SqlDbType.Int).Value = usuId;
                cmd.Parameters.Add("@usuPassword", System.Data.SqlDbType.VarChar, 25).Value = usuPassword;
                cmd.Parameters.Add("@usuPasswordNuevo", System.Data.SqlDbType.VarChar, 25).Value = usuPasswordNuevo;
            }

            if (aplicacion == "PP")
            {
                cmd.CommandText = "Api_UsuarioReseteoPassword";
                cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId;
                cmd.Parameters.Add("@usuPassword", System.Data.SqlDbType.VarChar, 25).Value = usuPassword;
                cmd.Parameters.Add("@usuPasswordNuevo", System.Data.SqlDbType.VarChar, 25).Value = usuPasswordNuevo;
            }
            var usuario = new clsUsuarioReseteoPasswordResponse();
            var reader = cmd.ExecuteReader();
            

            while (reader.Read())
            {
                usuario = MapToValue(reader);
            }

            conn.Close();

            return usuario;

        }
        private clsUsuarioReseteoPasswordResponse MapToValue(SqlDataReader reader)
        {

            return new clsUsuarioReseteoPasswordResponse()
            {

                sw = Convert.ToInt32(reader["sw"]),

            };

        }

    }
}
