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
    public class ApiCorreoClienteAdicionarService: IApiCorreoClienteAdicionarService
    {

        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiCorreoClienteAdicionarService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsCorreoClienteAdicionarResponse obtenerCorreo(int uclId, string coeAsunto, string coeDetalle)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_CorreoClienteAdicionar";
            cmd.Parameters.Add("@uclId", System.Data.SqlDbType.Int).Value = uclId;
            cmd.Parameters.Add("@coeAsunto", System.Data.SqlDbType.NVarChar, 50).Value = coeAsunto;
            cmd.Parameters.Add("@coeDetalle", System.Data.SqlDbType.NVarChar, 120).Value = coeDetalle;

            var correo = new clsCorreoClienteAdicionarResponse();
            var reader = cmd.ExecuteReader();

            
                correo.envioCorreo = true;
            
            conn.Close();




            return correo;

        }
    }
}
