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
    public class ApiUsuarioClienteVericarImeiService:IApiUsuarioClienteVericarImeiService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiUsuarioClienteVericarImeiService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsUsuarioClienteVericarImeiBoolResponse validarImei(int uclId, string uclImei)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_UsuarioClienteVericarImei";
            cmd.Parameters.Add("@uclId", System.Data.SqlDbType.Int).Value = uclId;
            cmd.Parameters.Add("@uclImei", System.Data.SqlDbType.VarChar, 80).Value = uclImei;


            var Imei = new clsUsuarioClienteVericarImeiBoolResponse();
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
