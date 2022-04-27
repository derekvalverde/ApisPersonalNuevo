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
    public class ApiDeudaCompensacionVerificarExisteService:IApiDeudaCompensacionVerificarExisteService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiDeudaCompensacionVerificarExisteService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsDeudaCompensacionVerificarExisteResponse> obtenerDeudaCompensacionVerificarExiste(int recId, int usuId)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_DeudaCompensacionVerificarExiste";

            cmd.Parameters.Add("@recId", System.Data.SqlDbType.Int).Value = recId;
            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId;

            var deudas = new List<clsDeudaCompensacionVerificarExisteResponse>();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                deudas.Add(MapToValue(reader));
            }
            conn.Close();
            //
            //Si no tiene deudas
            if (deudas == null)
            {
                return null;
            }

            return deudas;

        }
        private clsDeudaCompensacionVerificarExisteResponse MapToValue(SqlDataReader reader)
        {

            return new clsDeudaCompensacionVerificarExisteResponse()
            {
                decId = Convert.ToInt32(reader["decId"]),
                
            };

        }
    }
}
