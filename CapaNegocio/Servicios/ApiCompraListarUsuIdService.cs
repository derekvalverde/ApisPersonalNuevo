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
    public class ApiCompraListarUsuIdService: IApiCompraListarUsuIdService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiCompraListarUsuIdService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsCompraListarUsuIdResponse> obtenerCompraUsuId(int usuId)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_CompraListarUsuId";

            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId;


            var compraUsuId = new List<clsCompraListarUsuIdResponse>();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                compraUsuId.Add(MapToValue(reader));
            }
            conn.Close();
            //
            //Si el cheque no tiene elementos
            if (compraUsuId == null)
            {
                return null;
            }

            return compraUsuId;

        }
        private clsCompraListarUsuIdResponse MapToValue(SqlDataReader reader)
        {

            return new clsCompraListarUsuIdResponse()
            {

                comId = Convert.ToInt32(reader["comId"]),

                cliCodigo = reader["cliCodigo"].ToString().Trim(),
                          
                matCodigo = reader["matCodigo"].ToString().Trim(),
                         
                comPromedio = Convert.ToDecimal(reader["comPromedio"]),

                comCantidadPromedio = Convert.ToInt32(reader["comCantidadPromedio"]),

                comImportancia = Convert.ToInt32(reader["comImportancia"]),
                

            };

        }
    }
}
