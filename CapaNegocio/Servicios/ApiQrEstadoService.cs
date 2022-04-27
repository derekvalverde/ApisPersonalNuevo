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
using WebIntiApi.Models;
using CapaDatos.Request;

namespace CapaNegocio.Servicios
{
    public class ApiQrEstadoService:IApiQrEstadoService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiQrEstadoService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsQrEstadoResponse obtenerQrEstado(int qrdid)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_QrEstado";
            cmd.Parameters.Add("@qrdid", System.Data.SqlDbType.Int).Value = qrdid;


            var estadoQR = new clsQrEstadoResponse();
            var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                var qrEstado = Convert.ToInt32(reader["qrdid"]);


                if (qrEstado != 0)
                {
                    estadoQR.respuesta = true;
                }
                else
                {
                    estadoQR.respuesta = false;
                }
              
            }
            else
            {
                estadoQR.respuesta = false;
            }
            conn.Close();

            return estadoQR;

        }

    }
}
