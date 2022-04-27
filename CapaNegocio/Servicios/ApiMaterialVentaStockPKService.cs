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
    public class ApiMaterialVentaStockPKService:IApiMaterialVentaStockPKService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiMaterialVentaStockPKService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsMaterialVentaStockPKResponse> obtenerMaterialVentaStockPK(int usuId)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_MaterialVentaStockPK";

            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId;


            var MaterialPK = new List<clsMaterialVentaStockPKResponse>();
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
        private clsMaterialVentaStockPKResponse MapToValue(SqlDataReader reader)
        {

            return new clsMaterialVentaStockPKResponse()
            {
                 matId = Convert.ToInt32(reader["matId"]),

                matCodigo=  reader["matCodigo"].ToString().Trim(),

                 matNombre = reader["matNombre"].ToString().Trim(),

                secCodigo = reader["secCodigo"].ToString().Trim(),

                matEstado = reader["matEstado"].ToString().Trim(),

                masCantidad = Convert.ToInt32(reader["masCantidad"]),
                
                 marValor = Convert.ToDecimal(reader["marValor"]),

                mavLinea = reader["mavLinea"].ToString().Trim(),

                mavOrigen = reader["mavOrigen"].ToString().Trim(),

                mavfamilia = reader["mavfamilia"].ToString().Trim(),

                masFechaVencimiento = Convert.ToDateTime(reader["masFechaVencimiento"]),
                
                mccId = Convert.ToInt32(reader["mccId"]),
            };

        }
    }
}
