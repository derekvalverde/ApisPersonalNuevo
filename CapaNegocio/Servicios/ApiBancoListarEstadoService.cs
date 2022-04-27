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
    public class ApiBancoListarEstadoService: IApiBancoListarEstadoService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiBancoListarEstadoService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsBancoListarEstadoResponse> obtenerBancoEstado(string banEstado)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_BancoListarEstado";

            cmd.Parameters.Add("@banEstado", System.Data.SqlDbType.VarChar, 2).Value = banEstado;


            var bancoEstado = new List<clsBancoListarEstadoResponse>();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                bancoEstado.Add(MapToValue(reader));
            }
            conn.Close();
            //
            //Si el banco no tiene elementos
            if (bancoEstado == null)
            {
                return null;
            }
            
            return bancoEstado;

        }
        private clsBancoListarEstadoResponse MapToValue(SqlDataReader reader)
        {

            return new clsBancoListarEstadoResponse()
            {

                banId = Convert.ToInt32(reader["banId"]),
                banCodigo = reader["banCodigo"].ToString().Trim(),
                banNombre = reader["banNombre"].ToString().Trim(),
                
            };

        }
    }
}
