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
    public class ApiPedidoDetalleSolicitudService: IApiPedidoDetalleSolicitudService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiPedidoDetalleSolicitudService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsPedidoDetalleSolicitudResponse> obtenerPedidoDEtalleSolicitud(int pedId)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_PedidoDetalleSolicitud";

            cmd.Parameters.Add("@pedId", System.Data.SqlDbType.Int).Value = pedId;


            var pedido = new List<clsPedidoDetalleSolicitudResponse>();
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
        private clsPedidoDetalleSolicitudResponse MapToValue(SqlDataReader reader)
        {

            return new clsPedidoDetalleSolicitudResponse()
            {
                pedId = Convert.ToInt32(reader["pedId"]),
                
                matCodigo = reader["matCodigo"].ToString().Trim(),

                pddCantidad = Convert.ToInt32(reader["pddCantidad"]),

            };

        }

    }
}
