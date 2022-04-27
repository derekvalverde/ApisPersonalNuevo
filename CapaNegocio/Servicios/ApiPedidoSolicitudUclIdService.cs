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
    public class ApiPedidoSolicitudUclIdService: IApiPedidoSolicitudUclIdService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiPedidoSolicitudUclIdService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsPedidoSolicitudUclIdCabeceraResponse> obtenerPedidoSol(int uclId, int usuId, string aplicacion)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            if (aplicacion == "PC")
            {
                cmd.CommandText = "Api_PedidoSolicitudUclId";
                cmd.Parameters.Add("@uclId", System.Data.SqlDbType.Int).Value = uclId;
            }
            if (aplicacion == "PP")
            {
                cmd.CommandText = "Api_PedidoPersonalSolicitudUclId";
                cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId;
            }
                var pedidoSol = new List<clsPedidoSolicitudUclIdCabeceraResponse>();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                pedidoSol.Add(MapToValue(reader, aplicacion));
            }
            conn.Close();
            //
            //Si el buscador no tiene elementos
            if (pedidoSol == null)
            {
                return null;
            }
            //Si existe Material


            return pedidoSol;

        }
        private clsPedidoSolicitudUclIdCabeceraResponse MapToValue(SqlDataReader reader, string aplicacion)
        {
            clsPedidoSolicitudUclIdCabeceraResponse objPedido=new clsPedidoSolicitudUclIdCabeceraResponse()         
            {
                id = Convert.ToInt32(reader["id"]),
                pedId = Convert.ToInt32(reader["pedId"]),
                cliCodigo = reader["cliCodigo"].ToString().Trim(),
                pedPrecio = Convert.ToDecimal(reader["pedPrecio"]),
                pedCheque = Convert.ToDecimal(reader["pedCheque"]),
                pedObservacion = reader["pedObservacion"].ToString().Trim(),
                pedFechaEntrega = Convert.ToDateTime(reader["pedFechaEntrega"]),
                pedPeriodo = reader["pedPeriodo"].ToString().Trim()  
            };

            objPedido.detalleSolicitudCliente = obtenerDetallePedido(objPedido.pedId, aplicacion);
            return objPedido;

        }
        public List<clsPedidoSolicitudUclIddetalleResponse> obtenerDetallePedido(int pedId, string aplicacion)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
           
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            if (aplicacion == "PC")
            {
                cmd.CommandText = "Api_PedidoDetalleSolicitudliente";
                cmd.Parameters.Add("@pedId", System.Data.SqlDbType.Int).Value = pedId;
            }
            if (aplicacion == "PP")
            {
                cmd.CommandText = "Api_PedidoDetalleSolicitud";
                cmd.Parameters.Add("@pedId", System.Data.SqlDbType.Int).Value = pedId;
            }
                var detallePedido = new List<clsPedidoSolicitudUclIddetalleResponse>();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                detallePedido.Add(MapToValueDetalle(reader,aplicacion));
            }
           
            //
            //Si  no tiene 
            if (detallePedido == null)
            {
                return null;
            }
            //Si existe Material


            return detallePedido;

        }
        private clsPedidoSolicitudUclIddetalleResponse MapToValueDetalle(SqlDataReader reader, string aplicacion)
        {

            if (aplicacion == "PC")
            {
                return new clsPedidoSolicitudUclIddetalleResponse()
                {

                    pedId = Convert.ToInt32(reader["pedId"]),
                    matCodigo = reader["matCodigo"].ToString().Trim(),
                    pddCantidad = Convert.ToDecimal(reader["pddCantidad"]),
                    pddCantidadAtendida = Convert.ToDecimal(reader["pddCantidadAtendida"]),

                };
            }
            if (aplicacion == "PP")
            {
                return new clsPedidoSolicitudUclIddetalleResponse()
                {

                    pedId = Convert.ToInt32(reader["pedId"]),
                    matCodigo = reader["matCodigo"].ToString().Trim(),
                    pddCantidad = Convert.ToDecimal(reader["pddCantidad"]),
                };
            }

            clsPedidoSolicitudUclIddetalleResponse obj = new clsPedidoSolicitudUclIddetalleResponse();
            return obj; 


        }

    }
}
