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
    public class ApiCompraListarUclIdService: IApiCompraListarUclIdService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiCompraListarUclIdService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsCompraListarUclIdResponse> obtenerCompra(int uclId)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_CompraListarUclId";
            cmd.Parameters.Add("@uclId", System.Data.SqlDbType.Int).Value = uclId;


            var compras = new List<clsCompraListarUclIdResponse>();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                compras.Add(MapToValue(reader));
            }
            conn.Close();
            //
            //Si no exixte cliente
            if (compras == null)
            {
                return null;
            }
            //Si existe cliente


            return compras;

        }
        private clsCompraListarUclIdResponse MapToValue(SqlDataReader reader)
        {

            return new clsCompraListarUclIdResponse()
            {

                Id = Convert.ToInt32(reader["Id"]),
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
