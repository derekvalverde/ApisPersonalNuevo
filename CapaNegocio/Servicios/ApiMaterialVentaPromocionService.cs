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

namespace CapaNegocio.Servicios
{
    public class ApiMaterialVentaPromocionService: IApiMaterialVentaPromocionService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiMaterialVentaPromocionService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsMaterialVentaPromocionAgenciaResponse> obtenerVentaPromocion(string matCodigo)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_MaterialVentaPromocion";

            cmd.Parameters.Add("@matCodigo", System.Data.SqlDbType.NVarChar, 10).Value = matCodigo;
           

            var clientePendiente = new List<clsMaterialVentaPromocionAgenciaResponse>();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                clientePendiente.Add(MapToValue(reader));
            }
            conn.Close();
            //
            //Si el cheque no tiene elementos
            if (clientePendiente == null)
            {
                return null;
            }

            return clientePendiente;

        }
        private clsMaterialVentaPromocionAgenciaResponse MapToValue(SqlDataReader reader)
        {

            return new clsMaterialVentaPromocionAgenciaResponse()
            {
                facMes = Convert.ToInt32(reader["facMes"]),
                facDia = Convert.ToInt32(reader["facDia"]),
                facCantidad = Convert.ToInt32(reader["facCantidad"])
            };

        }
    }
}
