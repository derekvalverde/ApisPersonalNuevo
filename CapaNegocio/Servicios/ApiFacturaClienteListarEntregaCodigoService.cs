using System;
using System.Collections.Generic;
using CapaDatos.Data;
using CapaDatos.Request;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace CapaNegocio.Servicios
{
    public class ApiFacturaClienteListarEntregaCodigoService : IApiFacturaClienteListarEntregaCodigoService
    {
        private readonly ApplicationDbContext context;
        public ApiFacturaClienteListarEntregaCodigoService(ApplicationDbContext context) {
            this.context = context;
        }
        public IEnumerable<ClsFacturaClienteListarEntregaCodigoResponse> Listar(string cliCodigo)
        {
            SqlConnection conn = (SqlConnection)context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_FacturaClienteListarEntregaCodigo";

            cmd.Parameters.Add("@cliCodigo", System.Data.SqlDbType.VarChar, 10).Value = cliCodigo;

            var facturas = new List<ClsFacturaClienteListarEntregaCodigoResponse>();
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                facturas.Add(MapToValue(reader));
            }

            conn.Close();

            return facturas;
        }

        private ClsFacturaClienteListarEntregaCodigoResponse MapToValue(SqlDataReader reader)
        {
            return new ClsFacturaClienteListarEntregaCodigoResponse()
            {
                cliCodigo = reader["cliCodigo"].ToString().Trim(),
                facCodigo = reader["facCodigo"].ToString().Trim(),
                facNumero = reader["facNumero"].ToString().Trim(),
                facMonto = Convert.ToDouble(reader["facMonto"]),
                facFecha = Convert.ToDateTime(reader["facFecha"]),
                facFechaPago = Convert.ToDateTime(reader["facFechaPago"]),
                facEmpresa = reader["facEmpresa"].ToString().Trim(),
                facCategoria = reader["facCategoria"].ToString().Trim(),
                color = reader["color"].ToString().Trim()
            };
        }
    }
}
