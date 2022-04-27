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
    
        public class ApiUsuarioLoginGeneralService : IApiUsuarioLoginGeneralService
        {
            private readonly AppSettings _appSettings;
            private readonly ApplicationDbContext _context;
            public ApiUsuarioLoginGeneralService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
            {
                _appSettings = appSettings.Value;
                _context = context;
            }
            public clsUsuarioLoginGeneralResponse Authenticate2(string usuLogin, string usuPassword, int grpId)
            {
                //
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Api_UsuarioLoginGeneral";
                cmd.Parameters.Add("@usuLogin", System.Data.SqlDbType.VarChar, 25).Value = usuLogin;
                cmd.Parameters.Add("@usuPassword", System.Data.SqlDbType.VarChar, 25).Value = usuPassword;
                cmd.Parameters.Add("@grpId", System.Data.SqlDbType.Int).Value = grpId;
                var datosCliente = new clsUsuarioLoginGeneralResponse();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    datosCliente = MapToValue(reader);
                }
                conn.Close();
                //
                //Si el usuario no existe
                if (datosCliente.usuCodigo == null)
                {
                    return null;
                }
                //Si el usuario SI existe
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Key);
                //var key = Encoding.ASCII.GetBytes(usuLogin);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new System.Security.Claims.ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, datosCliente.usuNombre.ToString()),
                     new Claim(ClaimTypes.Email, datosCliente.ageOficina.ToString()),
                    new Claim(ClaimTypes.Role, datosCliente.sgrTipoNombre.ToString()),
                    new Claim(ClaimTypes.Version,"V3.1")
                }),
                    Expires = DateTime.UtcNow.AddDays(2),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                datosCliente.token = tokenHandler.WriteToken(token);

                return datosCliente;

            }
            private clsUsuarioLoginGeneralResponse MapToValue(SqlDataReader reader)
            {

                return new clsUsuarioLoginGeneralResponse()
                {
                    usuId = Convert.ToInt32(reader["usuId"]),

                    ageId = Convert.ToInt32(reader["ageId"]),

                    usuCodigo = reader["usuCodigo"].ToString(),

                    usuNombre = reader["usuNombre"].ToString(),

                    sgrTipoNombre = reader["sgrTipoNombre"].ToString(),

                    grpInicio = reader["grpInicio"].ToString(),

                    sgrId = Convert.ToInt32(reader["sgrId"]),

                    uscId = Convert.ToInt32(reader["uscId"]),

                    ageOficina = reader["ageOficina"].ToString(),


                };

            }
        }
}

