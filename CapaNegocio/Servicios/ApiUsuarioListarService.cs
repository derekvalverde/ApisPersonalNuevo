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
    public class ApiUsuarioListarService:IApiUsuarioListarService
    {

        private readonly AppSettings _appSettings;
        private readonly ApicacionDbContextCorrespondencia _context;
        public ApiUsuarioListarService(IOptions<AppSettings> appSettings, ApicacionDbContextCorrespondencia context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsUsuarioListarResponse> obtenerUsuarioListar(string UsuCodigo)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            cmd.CommandText = "pa_UsuariosListar";

            cmd.Parameters.Add("@UsuCodigo", System.Data.SqlDbType.Int).Value = UsuCodigo;

            var usuarios = new List<clsUsuarioListarResponse>();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                usuarios.Add(MapToValue(reader));
            }
            conn.Close();


            //
            //Si el buscador no tiene elementos
            if (usuarios == null)
            {
                return null;
            }
            //Si existe Material


            return usuarios;

        }
        private clsUsuarioListarResponse MapToValue(SqlDataReader reader)
        {

            return new clsUsuarioListarResponse()
            {
                
                UsuNombre = reader["UsuNombre"].ToString().Trim(),
                AreCodigo = reader["AreCodigo"].ToString().Trim(),
                
                UsuFechaIni = Convert.ToDateTime(reader["UsuFechaIni"]),
               
            };

        }

    }
}
