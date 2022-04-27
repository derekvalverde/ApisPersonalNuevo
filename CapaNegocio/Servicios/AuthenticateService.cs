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
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CapaNegocio.Servicios
{
    public class AuthenticateService :IAuthenticateService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public AuthenticateService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsDatosCliente Authenticate(string usuLogin, string usuPassword)
        {            
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "pa_UsuarioLoginClienteGeneral";
            cmd.Parameters.Add("@usuLogin", System.Data.SqlDbType.VarChar, 25).Value = usuLogin;
            cmd.Parameters.Add("@usuPassword", System.Data.SqlDbType.VarChar, 25).Value = usuPassword;
            cmd.Parameters.Add("@usuImei", System.Data.SqlDbType.VarChar, 25).Value = "1";
            cmd.Parameters.Add("@grpId", System.Data.SqlDbType.Int).Value = 1;
            var datosCliente = new clsDatosCliente();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                datosCliente = MapToValue(reader);
            }
            conn.Close();
            //
            //Si el usuario no existe
            if (datosCliente.usuId == null)
            {
                return null;
            }
            //Si el usuario SI existe
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Key);
           // var key = Encoding.ASCII.GetBytes(usuLogin + "INTRIX ali");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, datosCliente.usuNombre.ToString()),
                    new Claim(ClaimTypes.Email, datosCliente.usuCorreo.ToString()),
                    new Claim(ClaimTypes.Role, datosCliente.sgrTipoNombre.ToString()),
                    new Claim(ClaimTypes.Version,"V3.1")
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            datosCliente.Token = tokenHandler.WriteToken(token);
                        
            return datosCliente;

        }
        private clsDatosCliente MapToValue(SqlDataReader reader)
        {

            return new clsDatosCliente()
            {
                usuId = reader["usuId"].ToString(),
                usuNombre = reader["usuNombre"].ToString(),
                sgrTipoNombre = reader["sgrTipoNombre"].ToString(),
                sgrId = reader["sgrId"].ToString(),
                ageId = reader["ageId"].ToString(),
                uscId = reader["uscId"].ToString(),
                ageOficina = reader["ageOficina"].ToString(),
                usuCorreo = reader["usuCorreo"].ToString(),
                uclImei = reader["uclImei"].ToString(),
                appVersion = reader["appVersion"].ToString()
            };

        }
    }
}
