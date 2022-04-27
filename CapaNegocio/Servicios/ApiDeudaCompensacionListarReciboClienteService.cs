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
    public class ApiDeudaCompensacionListarReciboClienteService:IApiDeudaCompensacionListarReciboClienteService
    {

        private readonly AppSettings _appSettings;                                                                                                                                          
        private readonly ApplicationDbContext _context;
        public ApiDeudaCompensacionListarReciboClienteService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsDeudaCompensacionListarReciboClienteResponse> obtenerRecibosListado(int uclId)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_DeudaCompensacionListarReciboCliente";

            cmd.Parameters.Add("@uclId", System.Data.SqlDbType.Int).Value = uclId;
            

            var reporteListado = new List<clsDeudaCompensacionListarReciboClienteResponse>();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                reporteListado.Add(MapToValue(reader));
            }
            conn.Close();
            //
            //Si el cheque no tiene elementos
            if (reporteListado == null)
            {
                return null;
            }

            return reporteListado;

        }
        private clsDeudaCompensacionListarReciboClienteResponse MapToValue(SqlDataReader reader)
        {

            return new clsDeudaCompensacionListarReciboClienteResponse()
            {

                recId = Convert.ToInt32(reader["recId"]),
                decId = Convert.ToInt32(reader["decId"]),
                decMonto = Convert.ToDecimal(reader["decMonto"]),
                decFecha = Convert.ToDateTime(reader["decFecha"]),
                Estado = reader["Estado"].ToString().Trim(),

            };

        }
    }
}
