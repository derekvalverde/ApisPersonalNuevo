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
    public class ApiFacturaDetalleMaterialCantidadPersonalService: IApiFacturaDetalleMaterialCantidadPersonalService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiFacturaDetalleMaterialCantidadPersonalService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsFacturaDetalleMaterialCantidadPersonalResponse obtenerCantidadMaterial( int usuId, string matCodigo)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
           
                cmd.CommandText = "Api_FacturaDetalleMaterialCantidadPersonal";

                cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId;
            cmd.Parameters.Add("@matCodigo", System.Data.SqlDbType.NVarChar, 18).Value = matCodigo;




            var datosCli = new clsFacturaDetalleMaterialCantidadPersonalResponse();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                datosCli = MapToValue(reader);
            }
            conn.Close();
            //
            //Si no exixte cliente
            if (datosCli == null)
            {
                return null;
            }
            //Si existe cliente


            return datosCli;

        }
        private clsFacturaDetalleMaterialCantidadPersonalResponse MapToValue(SqlDataReader reader)
        {

            return new clsFacturaDetalleMaterialCantidadPersonalResponse()
            {


               cantidad = Convert.ToInt32(reader["Id"]),
                
            };

        }
    }
}
