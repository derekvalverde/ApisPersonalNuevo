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
    public class ApiMaterialStockCodigoAlmacenService:IApiMaterialStockCodigoAlmacenService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiMaterialStockCodigoAlmacenService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsMaterialStockCodigoAlmacenResponse> obtenerMaterialAlmacen(int usuId, string matCodigo)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_MaterialStockCodigoAlmacen";

            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId;
            cmd.Parameters.Add("@matCodigo", System.Data.SqlDbType.VarChar,18).Value = matCodigo;

            var materialAlmacen = new List<clsMaterialStockCodigoAlmacenResponse>();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                materialAlmacen.Add(MapToValue(reader));
            }
            conn.Close();
           
            if (materialAlmacen == null)
            {
                return null;
            }

            return materialAlmacen;

        }
        private clsMaterialStockCodigoAlmacenResponse MapToValue(SqlDataReader reader)
        {

            return new clsMaterialStockCodigoAlmacenResponse()
            {
                 matId = Convert.ToInt32(reader["matId"]),

                 matCodigo = reader["matCodigo"].ToString().Trim(),

                masCantidad = Convert.ToInt32(reader["masCantidad"]),

                almCodigo = reader["almCodigo"].ToString().Trim(),

                masEstado = reader["masEstado"].ToString().Trim(),

                masLote = reader["masLote"].ToString().Trim(),

                masFechaVencimiento = Convert.ToDateTime(reader["masFechaVencimiento"]),
               
            };

        }
    }
}
