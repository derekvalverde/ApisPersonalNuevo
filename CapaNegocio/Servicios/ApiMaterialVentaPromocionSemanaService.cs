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
    public class ApiMaterialVentaPromocionSemanaService:IApiMaterialVentaPromocionSemanaService
    {

        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiMaterialVentaPromocionSemanaService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsMaterialVentaPromocionSemanaResponse> obtenerVentaPromocionSemana(string matCodigo, string ageOficina)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_MaterialVentaPromocionSemana";

            cmd.Parameters.Add("@matCodigo", System.Data.SqlDbType.NVarChar, 10).Value = matCodigo;
            cmd.Parameters.Add("@ageOficina", System.Data.SqlDbType.Char, 4).Value = ageOficina;

            var clientePendiente = new List<clsMaterialVentaPromocionSemanaResponse>();
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
        private clsMaterialVentaPromocionSemanaResponse MapToValue(SqlDataReader reader)
        {

            return new clsMaterialVentaPromocionSemanaResponse()
            {
                facMes = Convert.ToInt32(reader["facMes"]),
                facSemana = Convert.ToInt32(reader["facSemana"]),
                facCantidad = Convert.ToInt32(reader["facCantidad"])
            };

        }

    }
}
