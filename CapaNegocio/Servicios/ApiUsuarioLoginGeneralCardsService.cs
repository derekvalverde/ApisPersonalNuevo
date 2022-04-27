using CapaDatos.Data;
using CapaDatos.Response;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;



namespace CapaNegocio.Servicios
{
    public class ApiUsuarioLoginGeneralCardsService:IApiUsuarioLoginGeneralCardsService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiUsuarioLoginGeneralCardsService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsUsuarioLoginGeneralCardsResponse Authenticate2(string usuLogin, string usuPassword, int grpId)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_UsuarioLoginClienteCards";
            cmd.Parameters.Add("@usuLogin", System.Data.SqlDbType.VarChar, 25).Value = usuLogin;
            cmd.Parameters.Add("@usuPassword", System.Data.SqlDbType.VarChar, 25).Value = usuPassword;           
            cmd.Parameters.Add("@grpId", System.Data.SqlDbType.Int).Value = grpId;
            var datosCliente = new clsUsuarioLoginGeneralCardsResponse();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                datosCliente = MapToValue(reader);
            }
            conn.Close();
            //
            //Si el usuario no existe
            if (datosCliente.usuId ==null)
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
                     new Claim(ClaimTypes.Email, datosCliente.usuCorreo.ToString()),
                    new Claim(ClaimTypes.Role, datosCliente.usuCI.ToString()),
                    new Claim(ClaimTypes.Version,"V3.1")
                }),
                Expires = DateTime.UtcNow.AddDays(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            datosCliente.token = tokenHandler.WriteToken(token);

            return datosCliente;

        }
        private clsUsuarioLoginGeneralCardsResponse MapToValue(SqlDataReader reader)
        {

            return new clsUsuarioLoginGeneralCardsResponse()
            {               

                usuId = reader["usuId"].ToString(),

                usuNombre = reader["usuNombre"].ToString(),
                usuCI= reader["usuCI"].ToString(),
                usuTelefono= reader["usuTelefono"].ToString(),
                usuCorreo = reader["usuCorreo"].ToString(),
                usuFecha= Convert.ToDateTime(reader["usuFecha"]),

            };

        }
    }
}
