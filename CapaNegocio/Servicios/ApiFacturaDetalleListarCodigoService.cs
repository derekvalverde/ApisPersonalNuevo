using System;
using System.Collections.Generic;

using CapaDatos.Data;
using CapaDatos.Request;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace CapaNegocio.Servicios
{
    public class ApiFacturaDetalleListarCodigoService : IApiFacturaDetalleListarCodigoService
    {
        private readonly ApplicationDbContext context;
        public ApiFacturaDetalleListarCodigoService(ApplicationDbContext context) {
            this.context = context;
        }

        public IEnumerable<ClsFacturaDetalleListarCodigoResponse> Listar(string facCodigo)
        {
            SqlConnection conn = (SqlConnection)context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_FacturaDetalleListarCodigo";
            cmd.Parameters.Add("@facCodigo", System.Data.SqlDbType.NVarChar, 10).Value = facCodigo;
            
            var materiales = new List<ClsFacturaDetalleListarCodigoResponse>();
            var reader = cmd.ExecuteReader();
            
            while (reader.Read())
            {
                materiales.Add(MapToValue(reader));
            }

            conn.Close();

            return materiales;
        }

        private ClsFacturaDetalleListarCodigoResponse MapToValue(SqlDataReader reader) {
            return new ClsFacturaDetalleListarCodigoResponse()
            {
                matCodigo = reader["matCodigo"].ToString().Trim(),
                fadCantidad = Convert.ToInt32(reader["fadCantidad"]),
                fadPrecio = Convert.ToDouble(reader["fadPrecio"]),
                fadCheque = Convert.ToDouble(reader["fadCheque"])
            };
        } 
    }
}
