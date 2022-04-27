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
using WebIntiApi.Models;

namespace CapaNegocio.Servicios
{
    public class ApiPedidoVerificarService:IApiPedidoVerificarService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiPedidoVerificarService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsPedidoVerificarResponse> obtenerPedidoVerificar(int usuId)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_PedidoVerificar";
            
            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId;


            var pedido = new List<clsPedidoVerificarResponse>();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                pedido.Add(MapToValue(reader));
            }
            conn.Close();
            //
            //Si el pedido no tiene elementos
            if (pedido == null)
            {
                return null;
            }

            return pedido;

        }
        private clsPedidoVerificarResponse MapToValue(SqlDataReader reader)
        {

            return new clsPedidoVerificarResponse()
            {
                penId = Convert.ToInt32(reader["penId"]),


            };

        }
    }
}
