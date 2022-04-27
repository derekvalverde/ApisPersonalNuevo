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
    public class ApiClienteCarteraPendienteService: IApiClienteCarteraPendienteService

    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiClienteCarteraPendienteService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsClienteCarteraPendienteResponse> obtenerClientePendiente(int usuId)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_ClienteCarteraPendiente";

            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId;


            var clientePendiente = new List<clsClienteCarteraPendienteResponse>();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                clientePendiente.Add(MapToValue(reader));
            }
            conn.Close();
            //
            //Si el cheque no tiene elementos
            if (clientePendiente == null)
            {
                return null;
            }

            return clientePendiente;

        }
        private clsClienteCarteraPendienteResponse MapToValue(SqlDataReader reader)
        {

            return new clsClienteCarteraPendienteResponse()
            {
                 clcId = Convert.ToInt32(reader["clcId"]),
                 cliCodigo = reader["cliCodigo"].ToString().Trim(),
                 cdfCodigo = reader["cdfCodigo"].ToString().Trim(),
                 clcDocumento = reader["clcDocumento"].ToString().Trim(),
                 clcPosicion = Convert.ToInt32(reader["clcPosicion"]),
                 
                
                 facCodigo = reader["facCodigo"].ToString().Trim(),
                 facOrigen = reader["facOrigen"].ToString().Trim(),
                 clcReferencia = reader["clcReferencia"].ToString().Trim(),
                
                 clcFechaContabilizacion =  Convert.ToDateTime(reader["clcFechaContabilizacion"]),
                 clcFechaBase = Convert.ToDateTime(reader["clcFechaBase"]),
                 clcTiempoMora = Convert.ToInt32(reader["clcTiempoMora"]),
                clcFechaPago = Convert.ToDateTime(reader["clcFechaPago"]),
               
                clcMonto = Convert.ToDecimal(reader["clcMonto"]),
                
                clcEstado = reader["clcEstado"].ToString().Trim(),
                clcCam = Convert.ToInt32(reader["clcCam"]),
            };

        }
    }
}
