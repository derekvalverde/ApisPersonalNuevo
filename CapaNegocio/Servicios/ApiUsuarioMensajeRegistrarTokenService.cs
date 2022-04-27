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
    public class ApiUsuarioMensajeRegistrarTokenService:IApiUsuarioMensajeRegistrarTokenService
    {

        private readonly AppSettings _appSettings;
        private readonly ApicacionDbContextCorrespondencia _context;
        public ApiUsuarioMensajeRegistrarTokenService(IOptions<AppSettings> appSettings, ApicacionDbContextCorrespondencia context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsUsuarioMensajeRegistrarTokenResponse registrarToken(string UsuCodigo, string UsuToken)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "pa_UsuarioMensajeRegistrarToken";

            cmd.Parameters.Add("@UsuCodigo", System.Data.SqlDbType.VarChar,20).Value = UsuCodigo;
            cmd.Parameters.Add("@UsuToken", System.Data.SqlDbType.NVarChar, 200).Value = UsuToken;
            


            var registrar = new clsUsuarioMensajeRegistrarTokenResponse();
            var reader = cmd.ExecuteReader();

            registrar.tokenRegistrado = true;

            conn.Close();

            return registrar;

        }

    }
}
