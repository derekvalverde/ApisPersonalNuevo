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
    public class ApiUsuarioRegistrarFCMService:IApiUsuarioRegistrarFCMService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiUsuarioRegistrarFCMService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsUsuarioRegistrarFCMResponse obtenerUsuarioFCM(int usuId, string usmGCM)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_UsuarioRegistrarFCM";
            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId;
           
            cmd.Parameters.Add("@usmGCM", System.Data.SqlDbType.VarChar, 200).Value = usmGCM;

            var usuario = new clsUsuarioRegistrarFCMResponse();
            var reader = cmd.ExecuteReader();


            usuario.tieneRegistro = true;

            conn.Close();

            return usuario;

        }
    }
}
