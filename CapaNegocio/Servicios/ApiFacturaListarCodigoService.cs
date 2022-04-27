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
    public class ApiFacturaListarCodigoService:IApiFacturaListarCodigoService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiFacturaListarCodigoService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsFacturaListarCodigoResponse> obtenerFacturaListar(string cliCodigo)
        {

            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_FacturaListarCodigo";

            cmd.Parameters.Add("@cliCodigo", System.Data.SqlDbType.VarChar,10).Value = cliCodigo;


            var factura = new List<clsFacturaListarCodigoResponse>();
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
        private clsFacturaListarCodigoResponse MapToValue(SqlDataReader reader)
        {

            return new clsFacturaListarCodigoResponse()
            {
                cliCodigo= reader["cliCodigo"].ToString().Trim(),
                facCodigo = reader["facCodigo"].ToString().Trim(),

                facNumero = reader["facNumero"].ToString().Trim(),
                facMonto = Convert.ToDecimal(reader["facMonto"]),

                facFecha = Convert.ToDateTime(reader["facFecha"]),
                facFechaPago = reader["facFechaPago"].ToString().Trim(),

                facEmpresa = reader["facEmpresa"].ToString().Trim(),
                matCodigo = reader["matCodigo"].ToString().Trim(),

                fadCantidad = Convert.ToInt32(reader["fadCantidad"]),
                fadPrecio = Convert.ToDecimal(reader["fadPrecio"]),

                fadCheque = Convert.ToDecimal(reader["fadCheque"]),
                facCategoria = reader["facCategoria"].ToString().Trim(),

            };

        }
    }
}
