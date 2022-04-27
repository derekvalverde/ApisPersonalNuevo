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
    public class ApiPedidoSolicitudEstadoService: IApiPedidoSolicitudEstadoService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiPedidoSolicitudEstadoService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsPedidoSolicitudEstadoResponse> obtenerPedidoSolicitudEstado(string pedEstado, int usuId)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            
                cmd.CommandText = "Api_PedidoSolicitudEstado";
            cmd.Parameters.Add("@pedEstado", System.Data.SqlDbType.VarChar, 2).Value = pedEstado;
            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId;

            var pedidoSol = new List<clsPedidoSolicitudEstadoResponse>();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                pedidoSol.Add(MapToValue(reader));
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
        private clsPedidoSolicitudEstadoResponse MapToValue(SqlDataReader reader)
        {
            clsPedidoSolicitudEstadoResponse objPedido = new clsPedidoSolicitudEstadoResponse()
            {
                pedId = Convert.ToInt32(reader["pedId"]),

                cliCodigo = reader["cliCodigo"].ToString().Trim(),

                pedPrecio = Convert.ToDecimal(reader["pedPrecio"]),

                pedCheque = Convert.ToDecimal(reader["pedCheque"]),

                pedObservacion = reader["pedObservacion"].ToString().Trim(),

                pedFechaEntrega = Convert.ToDateTime(reader["pedFechaEntrega"]),

                pedPeriodo = reader["pedPeriodo"].ToString().Trim(),
            };

            objPedido.detalleSolicitudEstado = obtenerPedidoDEtalleSolicitud(objPedido.pedId);
            return objPedido;

        }
        public List<clsPedidoDetalleSolicitudResponse> obtenerPedidoDEtalleSolicitud(int pedId)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            
                cmd.CommandText = "Api_PedidoDetalleSolicitud";
                cmd.Parameters.Add("@pedId", System.Data.SqlDbType.Int).Value = pedId;
           
            var detallePedido = new List<clsPedidoDetalleSolicitudResponse>();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                detallePedido.Add(MapToValueDetalle(reader));
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
        private clsPedidoDetalleSolicitudResponse MapToValueDetalle(SqlDataReader reader)
        {


            return new clsPedidoDetalleSolicitudResponse()
            {
                pedId = Convert.ToInt32(reader["pedId"]),

                matCodigo = reader["matCodigo"].ToString().Trim(),

                pddCantidad = Convert.ToInt32(reader["pddCantidad"]),

            };

         }
        /*private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiPedidoSolicitudEstadoService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsPedidoSolicitudEstadoResponse> obtenerPedidoSolicitudEstado(string pedEstado, int usuId)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_PedidoSolicitudEstado";
            cmd.Parameters.Add("@pedEstado", System.Data.SqlDbType.VarChar,2).Value = pedEstado;
            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId;


            var pedido = new List<clsPedidoSolicitudEstadoResponse>();
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
        private clsPedidoSolicitudEstadoResponse MapToValue(SqlDataReader reader)
        {

            return new clsPedidoSolicitudEstadoResponse()
            {
                pedId = Convert.ToInt32(reader["pedId"]),

                cliCodigo = reader["cliCodigo"].ToString().Trim(),

                pedPrecio = Convert.ToDecimal(reader["pedPrecio"]),

                pedCheque = Convert.ToDecimal(reader["pedCheque"]),

                pedObservacion = reader["pedObservacion"].ToString().Trim(),

                pedFechaEntrega = Convert.ToDateTime(reader["pedFechaEntrega"]),

                pedPeriodo = reader["pedPeriodo"].ToString().Trim(),

            };

        }*/

    }
}
