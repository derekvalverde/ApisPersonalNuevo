using System;
using System.Collections.Generic;
using CapaDatos.Data;
using CapaDatos.Response;
using Microsoft.Extensions.Options;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace CapaNegocio.Servicios
{
   public class ApiUsuarioLoginClienteDatosService : IApiUsuarioLoginClienteDatosService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;


        public ApiUsuarioLoginClienteDatosService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;

        }

        public clsUsuarioLoginClienteDatosResponse Authenticate3(string usuLogin)
        {
            //si el login no  empieza por 10 es error
            //if (usuLogin.Substring(0,2) != "10")
            //{
            //  return null;
            // }

            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.CommandText = "Api_UsuarioLoginClienteDatos";
            cmd.Parameters.Add("@usuLogin", System.Data.SqlDbType.VarChar, 25).Value = usuLogin;

            var datosCliente = new clsUsuarioLoginClienteDatosResponse();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                datosCliente = MapToValue(reader);
            }
            conn.Close();
            //
            //Si el usuario no existe
            if (datosCliente.cliCodigo == null)
            {
                return null;
            }


            //Genero nuevo token y lo guardo en base de datos  
            //Si el usuario SI existe
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Key);
            //var key = Encoding.ASCII.GetBytes(usuLogin + "INTRIX ali");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, datosCliente.cliNombreComercial.ToString()),
                    new Claim(ClaimTypes.Role, datosCliente.prgDetalle.ToString()),
                    new Claim(ClaimTypes.Version,"V3.1")
                }),
                Expires = DateTime.UtcNow.AddDays(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            datosCliente.token = tokenHandler.WriteToken(token);


            return datosCliente;

        }
        private clsUsuarioLoginClienteDatosResponse MapToValue(SqlDataReader reader)
        {

            return new clsUsuarioLoginClienteDatosResponse()
            {
                cliNombreComercial = reader["cliNombreComercial"].ToString().Trim(),
                prgDetalle = reader["prgDetalle"].ToString().Trim(),
                cliCodigo = reader["cliCodigo"].ToString().Trim(),

            };

        }
    }
}
