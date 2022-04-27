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
    public class ApiMaterialVentaStockPkActualizadoService:IApiMaterialVentaStockPkActualizadoService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiMaterialVentaStockPkActualizadoService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsMaterialVentaStockPkActualizadoResponse> obtenerMaterialVentaStockPK(int usuId)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_MaterialVentaStockPKActualizado";

            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId;


            var MaterialPK = new List<clsMaterialVentaStockPkActualizadoResponse>();
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
        private clsMaterialVentaStockPkActualizadoResponse MapToValue(SqlDataReader reader)
        {

            return new clsMaterialVentaStockPkActualizadoResponse()
            {
                matId = Convert.ToInt32(reader["matId"]),

                matCodigo = reader["matCodigo"].ToString().Trim(),

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
