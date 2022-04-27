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
    public class ApiFacturaNotaClienteListarCodigoService:IApiFacturaNotaClienteListarCodigoService
    {

        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiFacturaNotaClienteListarCodigoService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsFacturaNotaClienteListarCodigoResponse> obtenerNotaCreditoListar(string cliCodigo)
        {

            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_FacturaNotaClienteListarCodigo";

            cmd.Parameters.Add("@cliCodigo", System.Data.SqlDbType.VarChar, 10).Value = cliCodigo;


            var factura = new List<clsFacturaNotaClienteListarCodigoResponse>();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                factura.Add(MapToValue(reader));
            }
            conn.Close();
            //
            //Si la factura no tiene elementos
            if (factura == null)
            {
                return null;
            }

            return factura;

        }
        private clsFacturaNotaClienteListarCodigoResponse MapToValue(SqlDataReader reader)
        {

            return new clsFacturaNotaClienteListarCodigoResponse()
            {
                cliCodigo = reader["cliCodigo"].ToString().Trim(),
                cdfCodigo = reader["cdfCodigo"].ToString().Trim(),
                facCodigo = reader["facCodigo"].ToString().Trim(),   
                facNumero= Convert.ToInt32(reader["facNumero"]),
                clcMontoPago = Convert.ToDecimal(reader["clcMontoPago"]),
                clcFechaBase = Convert.ToDateTime(reader["clcFechaBase"]),
                facEmpresa = reader["facEmpresa"].ToString().Trim(),
                
            };

        }
    }
}
