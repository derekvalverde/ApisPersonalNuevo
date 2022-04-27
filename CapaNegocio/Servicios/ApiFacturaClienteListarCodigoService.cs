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
    public class ApiFacturaClienteListarCodigoService : IApiFacturaClienteListarCodigoService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiFacturaClienteListarCodigoService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsFacturaClienteListarCodigoCabeceraResponse> obtenerFactura(string cliCodigo)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_FacturaClienteListarCodigo";
            cmd.Parameters.Add("@cliCodigo", System.Data.SqlDbType.VarChar,10).Value = cliCodigo;
            var facturas = new List<clsFacturaClienteListarCodigoCabeceraResponse>();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                facturas.Add(MapToValue(reader));
            }
            conn.Close();
            //
            //Si el buscador no tiene elementos
            if (facturas == null)
            {
                return null;
            }
            //Si existe Material


            return facturas;

        }
        private clsFacturaClienteListarCodigoCabeceraResponse MapToValue(SqlDataReader reader)
        {
            clsFacturaClienteListarCodigoCabeceraResponse objFactura = new clsFacturaClienteListarCodigoCabeceraResponse()
            {

                cliCodigo = reader["cliCodigo"].ToString().Trim(),
                facCodigo = reader["facCodigo"].ToString().Trim(),
                facNumero = reader["facNumero"].ToString().Trim(),
                facMonto = Convert.ToDecimal(reader["facMonto"]),
                facFecha = Convert.ToDateTime(reader["facFecha"]),
                facFechaPago = Convert.ToDateTime(reader["facFechaPago"]),
                facEmpresa = reader["facEmpresa"].ToString().Trim(),
                facCategoria = reader["facCategoria"].ToString().Trim(),
                color = reader["color"].ToString().Trim(),
            };

            objFactura.detalleFactura = obtenerFacturaDetalle(objFactura.facCodigo);
            return objFactura;

        }
        public List<clsFacturaClienteListarCodigoDetalleResponse> obtenerFacturaDetalle(string facCodigo)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_FacturaDetalleListarCodigo";
            cmd.Parameters.Add("@facCodigo", System.Data.SqlDbType.VarChar,20).Value = facCodigo;
            var detalleFactura = new List<clsFacturaClienteListarCodigoDetalleResponse>();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                detalleFactura.Add(MapToValueDetalle(reader));
            }

            //
            //Si  no tiene 
            if (detalleFactura == null)
            {
                return null;
            }
            //Si existe Material


            return detalleFactura;

        }

        private clsFacturaClienteListarCodigoDetalleResponse MapToValueDetalle(SqlDataReader reader)
        {
            return new clsFacturaClienteListarCodigoDetalleResponse()
            {

                matCodigo = reader["matCodigo"].ToString().Trim(),
                fadCantidad = Convert.ToDecimal(reader["fadCantidad"]),
                fadPrecio = Convert.ToDecimal(reader["fadPrecio"]),
                fadCheque = Convert.ToDecimal(reader["fadCheque"]),

            };

        }

    }
}
