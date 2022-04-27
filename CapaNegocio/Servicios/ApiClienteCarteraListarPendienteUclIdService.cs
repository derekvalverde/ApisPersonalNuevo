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
    public class ApiClienteCarteraListarPendienteUclIdService : IApiClienteCarteraListarPendienteUclIdService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiClienteCarteraListarPendienteUclIdService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsClienteCarteraListarPendienteUclIdResponse> obtenerPendientes(int uclId)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_ClienteCarteraListarPendienteUclId";
            
            cmd.Parameters.Add("@uclId", System.Data.SqlDbType.Int).Value = uclId;
         

            var pendientes = new List<clsClienteCarteraListarPendienteUclIdResponse>();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                pendientes.Add(MapToValue(reader));
            }
            conn.Close();
            //
            //Si el buscador no tiene elementos
            if (pendientes == null)
            {
                return null;
            }
            //Si existe Material


            return pendientes;

        }
        private clsClienteCarteraListarPendienteUclIdResponse MapToValue(SqlDataReader reader)
        {

            return new clsClienteCarteraListarPendienteUclIdResponse()
            {

                clcId = Convert.ToInt32(reader["clcId"]),
                cliCodigo = reader["cliCodigo"].ToString().Trim(),
                clcDocumento = reader["clcDocumento"].ToString().Trim(),
                facCodigo = reader["facCodigo"].ToString().Trim(),
                facOrigen = reader["facOrigen"].ToString().Trim(),
                clcReferencia = reader["clcReferencia"].ToString().Trim(),
                clcFechaBase = Convert.ToDateTime(reader["clcFechaBase"]),
                clcFechaPago = Convert.ToDateTime(reader["clcFechaPago"]),
                clcMonto = Convert.ToDecimal(reader["clcMonto"]),
                clcMontoPago = Convert.ToDecimal(reader["clcMontoPago"]),
                clcCampana = Convert.ToInt32(reader["clcCampana"]),
                clcFechaCategoriaA = Convert.ToDateTime(reader["clcFechaCategoriaA"]),
                clcFechaCategoriaB = Convert.ToDateTime(reader["clcFechaCategoriaB"]),
                color = reader["color"].ToString().Trim(),
                clcEstado = reader["clcEstado"].ToString().Trim(),


            };

        }
    }
}
