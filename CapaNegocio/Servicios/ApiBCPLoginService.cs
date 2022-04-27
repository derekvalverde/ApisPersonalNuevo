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
    public class ApiBCPLoginService: IApiBCPLoginService
    {
        private readonly AppSettings _appSettings;
        
        public ApiBCPLoginService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;            
        }
        public clsApiBCPLoginResponse autenticar(string usuLogin, string usuPassword)
        {
            if (usuLogin == "UserBCP" && usuPassword == "pass2021!")
            {
                //Si el usuario SI existe
                var tokenHandler = new JwtSecurityTokenHandler();
                //var key = Encoding.ASCII.GetBytes(_appSettings.Key);
                var key = Encoding.ASCII.GetBytes("Apis INTI-BCP 2021");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new System.Security.Claims.ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, "BCP"),
                    new Claim(ClaimTypes.Email, "BCP@BCP.COM"),
                    new Claim(ClaimTypes.Role, "APIS"),
                    new Claim(ClaimTypes.Version,"V3.1")
                }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                clsApiBCPLoginResponse loginResponse = new clsApiBCPLoginResponse();
                loginResponse.TokenBCP = tokenHandler.WriteToken(token);
                return loginResponse;
            }
            else {
                return null;
            }
            
        }        
    }
}
