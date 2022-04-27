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
    public class ApiPedidoSolicitudVerificarEstadoService:IApiPedidoSolicitudVerificarEstadoService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiPedidoSolicitudVerificarEstadoService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsPedidoSolicitudVerificarEstadoResponse> obtenerPedidoSolicitudVerificarEstado(string pedEstado, int usuId)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_PedidoSolicitudVerificarEstado";
            cmd.Parameters.Add("@pedEstado", System.Data.SqlDbType.VarChar, 2).Value = pedEstado;
            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId;


            var pedido = new List<clsPedidoSolicitudVerificarEstadoResponse>();
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
        private clsPedidoSolicitudVerificarEstadoResponse MapToValue(SqlDataReader reader)
        {

            return new clsPedidoSolicitudVerificarEstadoResponse()
            {
                pedId = Convert.ToInt32(reader["pedId"]),

            };

        }
    }
}
