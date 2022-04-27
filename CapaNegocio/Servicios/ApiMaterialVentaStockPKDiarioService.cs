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
    public class ApiMaterialVentaStockPKDiarioService:IApiMaterialVentaStockPKDiarioService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiMaterialVentaStockPKDiarioService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsMaterialVentaStockPKDiarioResponse> obtenerMaterialVentaStockPK(int usuId)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_MaterialVentaStockPKDiario";

            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId;


            var MaterialPK = new List<clsMaterialVentaStockPKDiarioResponse>();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                MaterialPK.Add(MapToValue(reader));
            }
            conn.Close();
            //
            //Si no hay material
            if (MaterialPK == null)
            {
                return null;
            }

            return MaterialPK;

        }
        private clsMaterialVentaStockPKDiarioResponse MapToValue(SqlDataReader reader)
        {

            return new clsMaterialVentaStockPKDiarioResponse()
            {
                

                matCodigo = reader["matCodigo"].ToString().Trim(),

               masCantidad = Convert.ToInt32(reader["masCantidad"]),

                marValor = Convert.ToDecimal(reader["marValor"]),
                                
                masFechaVencimiento = Convert.ToDateTime(reader["masFechaVencimiento"]),

            };

        }
    }
}
