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
    public class ApiDeudaCompensacionListarReciboService: IApiDeudaCompensacionListarReciboService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiDeudaCompensacionListarReciboService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsDeudaCompensacionListarReciboResponse obtenerlistarRecibos(int decId)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_DeudaCompensacionListarRecibo";
            cmd.Parameters.Add("@decId", System.Data.SqlDbType.Int).Value = decId;
            var recibos = new clsDeudaCompensacionListarReciboResponse();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                recibos = MapToValue(reader);
            }
            conn.Close();
            // 
            //Si el buscador no tiene elementos
            if (recibos == null)
            {
                return null;
            }
            //Si existe Material


            return recibos;

        }
        private clsDeudaCompensacionListarReciboResponse MapToValue(SqlDataReader reader)
        {
            clsDeudaCompensacionListarReciboResponse objRecibo = new clsDeudaCompensacionListarReciboResponse()
            {

                recId = Convert.ToInt32(reader["recId"]),
                decId = Convert.ToInt32(reader["decId"]),
                decMonto = Convert.ToDecimal(reader["decMonto"]),
                decFecha = Convert.ToDateTime(reader["decFecha"]),
                cliNombreComercial = reader["cliNombreComercial"].ToString().Trim(),
                cliNombreFiscal = reader["cliNombreFiscal"].ToString().Trim(),                
                cliCodigo = reader["cliCodigo"].ToString().Trim(),
                dedNumero = Convert.ToInt32(reader["dedNumero"]),
                cliIdentificacionNumero = reader["cliIdentificacionNumero"].ToString().Trim(),
                cliCiudad = reader["cliCiudad"].ToString().Trim(),

                cliDireccionComercial = reader["cliDireccionComercial"].ToString().Trim(),
            };

            objRecibo.detalleRecibo = obtenerRecibosDetalle(objRecibo.decId);
            return objRecibo;

        }
        public List<clsDeudaCompensacionListarReciboDetalleResponse> obtenerRecibosDetalle(int decId)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_DeudaCompensacionListarReciboDetalle";
            cmd.Parameters.Add("@decId", System.Data.SqlDbType.Int).Value = decId;
            var detalleRecibo = new List<clsDeudaCompensacionListarReciboDetalleResponse>();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                detalleRecibo.Add(MapToValueDetalle(reader));
            }

            //
            //Si  no tiene 
            if (detalleRecibo == null)
            {
                return null;
            }
            //Si existe Material


            return detalleRecibo;

        }

        private clsDeudaCompensacionListarReciboDetalleResponse MapToValueDetalle(SqlDataReader reader)
        {
            return new clsDeudaCompensacionListarReciboDetalleResponse()
            {

                dedMonto = Convert.ToDecimal(reader["dedMonto"]),
                facNumero = Convert.ToInt32(reader["facNumero"]),
                facOrigen = reader["facOrigen"].ToString().Trim(),
            };

        }
    }
}
