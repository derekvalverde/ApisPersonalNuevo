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
    public class ApiUsuarioClienteRegistrarFCMService: IApiUsuarioClienteRegistrarFCMService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiUsuarioClienteRegistrarFCMService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsUsuarioClienteRegistrarFCMBoolResponse obtenerUsuario(int uclId, string ucmFCM)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_UsuarioClienteRegistrarFCM";
            cmd.Parameters.Add("@uclId", System.Data.SqlDbType.Int).Value =uclId;
            cmd.Parameters.Add("@ucmFCM", System.Data.SqlDbType.VarChar, 200).Value = ucmFCM;


            var usuario = new clsUsuarioClienteRegistrarFCMBoolResponse();
            var reader = cmd.ExecuteReader();

            
                usuario.tieneRegistro = true;
            
            conn.Close();

            return usuario;

        }
    }
}
