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
    public class ApiDeudaCompensacionVerificarFaltanteService: IApiDeudaCompensacionVerificarFaltanteService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiDeudaCompensacionVerificarFaltanteService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsDeudaCompensacionVerificarFaltanteResponse> obtenerDeudaCompensacionFaltante(int usuId, int recIdIni, int recIdFin)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_DeudaCompensacionVerficarFaltante";


            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId;
            cmd.Parameters.Add("@recIdIni", System.Data.SqlDbType.Int).Value = recIdIni;
            cmd.Parameters.Add("@recIdFin", System.Data.SqlDbType.Int).Value = recIdFin;


            var deudas = new List<clsDeudaCompensacionVerificarFaltanteResponse>();
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
        private clsDeudaCompensacionVerificarFaltanteResponse MapToValue(SqlDataReader reader)
        {

            return new clsDeudaCompensacionVerificarFaltanteResponse()
            {
                recId = Convert.ToInt32(reader["recId"]),

            };

        }
    }
}
