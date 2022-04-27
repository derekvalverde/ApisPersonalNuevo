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
    public class ApiUsuarioVericarImeiService:IApiUsuarioVericarImeiService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiUsuarioVericarImeiService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsUsuarioVericarImeiResponse validarImei(int usuId, string usuImei)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_UsuarioVericarImei";
            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId;
            cmd.Parameters.Add("@usuImei", System.Data.SqlDbType.VarChar, 80).Value = usuImei;


            var Imei = new clsUsuarioVericarImeiResponse();
            var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                Imei.ImeiValido = true;
            }
            else
            {
                Imei.ImeiValido = false;
            }
            conn.Close();

            return Imei;

        }
    }
}
