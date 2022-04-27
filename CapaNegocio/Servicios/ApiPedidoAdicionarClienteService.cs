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
using CapaDatos.Request;

namespace CapaNegocio.Servicios
{
    public class ApiPedidoAdicionarClienteService:IApiPedidoAdicionarClienteService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiPedidoAdicionarClienteService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsPedidoAdicionarClienteResponse registrarPedido(clsPedidoAdicionarClienteCabeceraRequest pedido)
        {
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            // Paso 1 Ejecutar el procedimiento almacenado Api_PedidoObtenerPenIdUclId para obtener el numero de pedido (penId)
            cmd.CommandText = "Api_PedidoObtenerPenIdUclId";
            cmd.Parameters.Add("@uclId", System.Data.SqlDbType.Int).Value = pedido.uclId;
            var reader = cmd.ExecuteReader();
            int penId = 0;
            int swValida = 0;    // 0 Ejecuta procedimento almacenado;   <>0 no Ejecuta procedmiento almacendado
            while (reader.Read())
            {
                penId = Convert.ToInt32(reader["penId"]);               
            }
            penId = penId + 1; //Sumamos 1 al pedido obtenido
            if (pedido.aplicacion == "PP")
            {
                penId = 0;
            }
            //Paso 2 Ejecutar el procedimiento almacenado Api_PedidoAdicionarCliente
            SqlCommand cmd1 = conn.CreateCommand();
            cmd1.CommandType = System.Data.CommandType.StoredProcedure;
            
            if (pedido.aplicacion=="PC")
            {
                swValida = 0;
                cmd1.CommandText = "Api_PedidoAdicionarCliente";
                cmd1.Parameters.Add("@penId", System.Data.SqlDbType.Int).Value = penId;
                cmd1.Parameters.Add("@pedCodigo", System.Data.SqlDbType.NVarChar, 80).Value = "";
                cmd1.Parameters.Add("@cadCodigo", System.Data.SqlDbType.Char, 2).Value = "10";
                cmd1.Parameters.Add("@secCodigo", System.Data.SqlDbType.Char, 2).Value = "00";
                cmd1.Parameters.Add("@pedOrdenCompra", System.Data.SqlDbType.NVarChar, 35).Value = "C_" + DateTime.Now.ToString("yyyyMMdd") + "_" + penId + "_" + pedido.uclId;
                cmd1.Parameters.Add("@pedFechaEntrega", System.Data.SqlDbType.DateTime).Value = pedido.pedFechaEntrega;
                cmd1.Parameters.Add("@pedOrigen", System.Data.SqlDbType.NVarChar, 15).Value = "Z20";
                cmd1.Parameters.Add("@pedFormaPago", System.Data.SqlDbType.Char, 10).Value = "";
                cmd1.Parameters.Add(new SqlParameter("@pedPrecio", System.Data.SqlDbType.Decimal) { Precision = 18, Scale = 2 }).Value = pedido.pedPrecio;
                cmd1.Parameters.Add(new SqlParameter("@pedCheque", System.Data.SqlDbType.Decimal) { Precision = 18, Scale = 2 }).Value = 0;
                cmd1.Parameters.Add("@pedObservacion", System.Data.SqlDbType.NVarChar, 100).Value = "";
                cmd1.Parameters.Add("@pedFechaPedido", System.Data.SqlDbType.DateTime).Value = pedido.pedFechaPedido;
                cmd1.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = pedido.usuId;
                cmd1.Parameters.Add("@pedPeriodo", System.Data.SqlDbType.Char, 2).Value = "AM";
                cmd1.Parameters.Add("@pedEstado", System.Data.SqlDbType.Char, 2).Value = "ES";
                cmd1.Parameters.Add("@cliCodigo", System.Data.SqlDbType.NVarChar, 10).Value = pedido.cliCodigo;
                cmd1.Parameters.Add("@pedPago", System.Data.SqlDbType.Char, 4).Value = "";
            }
            if (pedido.aplicacion == "PP")
            {
                swValida = 0;
                cmd1.CommandText = "Api_PedidoAdicionarPersonal";
                cmd1.Parameters.Add("@penId", System.Data.SqlDbType.Int).Value = penId;
                cmd1.Parameters.Add("@pedCodigo", System.Data.SqlDbType.NVarChar, 80).Value = "";
                cmd1.Parameters.Add("@cadCodigo", System.Data.SqlDbType.Char, 2).Value = "40";
                cmd1.Parameters.Add("@secCodigo", System.Data.SqlDbType.Char, 2).Value = "00";
                cmd1.Parameters.Add("@pedOrdenCompra", System.Data.SqlDbType.NVarChar, 35).Value = "T_" + DateTime.Now.ToString("yyyyMMdd");
                cmd1.Parameters.Add("@pedFechaEntrega", System.Data.SqlDbType.DateTime).Value = pedido.pedFechaEntrega;
                cmd1.Parameters.Add("@pedOrigen", System.Data.SqlDbType.NVarChar, 15).Value = "Z23";
                cmd1.Parameters.Add("@pedFormaPago", System.Data.SqlDbType.Char, 10).Value = "";
                cmd1.Parameters.Add(new SqlParameter("@pedPrecio", System.Data.SqlDbType.Decimal) { Precision = 18, Scale = 2 }).Value = pedido.pedPrecio;
                cmd1.Parameters.Add(new SqlParameter("@pedCheque", System.Data.SqlDbType.Decimal) { Precision = 18, Scale = 2 }).Value = 0;
                cmd1.Parameters.Add("@pedObservacion", System.Data.SqlDbType.NVarChar, 100).Value = "";
                cmd1.Parameters.Add("@pedFechaPedido", System.Data.SqlDbType.DateTime).Value = pedido.pedFechaPedido;
                cmd1.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = pedido.usuId;
                cmd1.Parameters.Add("@pedPeriodo", System.Data.SqlDbType.Char, 2).Value = "AM";
                cmd1.Parameters.Add("@pedEstado", System.Data.SqlDbType.Char, 2).Value = "ES";
                cmd1.Parameters.Add("@cliCodigo", System.Data.SqlDbType.NVarChar, 10).Value = pedido.cliCodigo;
                cmd1.Parameters.Add("@pedPago", System.Data.SqlDbType.Char, 4).Value = "";
            }
            if (pedido.aplicacion == "PA")
            {
                swValida = 0;
                //Validamos el penId recibido
                SqlCommand cmd2 = conn.CreateCommand();
                cmd2.CommandType = System.Data.CommandType.StoredProcedure;
                cmd2.CommandText = "pa_PedidoVerificarExiste";
                cmd2.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = pedido.usuId;
                cmd2.Parameters.Add("@penId", System.Data.SqlDbType.Int).Value = pedido.penId;
                var readerValida = cmd2.ExecuteReader();
                while (readerValida.Read())
                {
                    swValida = Convert.ToInt32(readerValida["penId"]);
                }

                cmd1.CommandText = "Api_PedidoAdicionar";
                cmd1.Parameters.Add("@penId", System.Data.SqlDbType.Int).Value = pedido.penId;
                cmd1.Parameters.Add("@pedCodigo", System.Data.SqlDbType.NVarChar, 80).Value = "";
                cmd1.Parameters.Add("@cadCodigo", System.Data.SqlDbType.Char, 2).Value = "10";
                cmd1.Parameters.Add("@secCodigo", System.Data.SqlDbType.Char, 2).Value = "00";                   
                cmd1.Parameters.Add("@pedOrdenCompra", System.Data.SqlDbType.NVarChar, 35).Value = "A_" + DateTime.Now.ToString("yyyyMMdd") + "_" + pedido.penId + "_" + pedido.usuId;                
                cmd1.Parameters.Add("@pedFechaEntrega", System.Data.SqlDbType.DateTime).Value = pedido.pedFechaEntrega;
                cmd1.Parameters.Add("@pedOrigen", System.Data.SqlDbType.NVarChar, 15).Value = "Z20";
                cmd1.Parameters.Add("@pedFormaPago", System.Data.SqlDbType.Char, 10).Value = "";
                cmd1.Parameters.Add(new SqlParameter("@pedPrecio", System.Data.SqlDbType.Decimal) { Precision = 18, Scale = 2 }).Value = pedido.pedPrecio;
                cmd1.Parameters.Add(new SqlParameter("@pedCheque", System.Data.SqlDbType.Decimal) { Precision = 18, Scale = 2 }).Value = pedido.pedCheque;
                cmd1.Parameters.Add("@pedObservacion", System.Data.SqlDbType.NVarChar, 100).Value = pedido.observacion;
                cmd1.Parameters.Add("@pedFechaPedido", System.Data.SqlDbType.DateTime).Value = pedido.pedFechaPedido;
                cmd1.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = pedido.usuId;
                cmd1.Parameters.Add("@pedPeriodo", System.Data.SqlDbType.Char, 2).Value = pedido.pedPeriodo;
                cmd1.Parameters.Add("@pedEstado", System.Data.SqlDbType.Char, 2).Value = "";
                cmd1.Parameters.Add("@cliCodigo", System.Data.SqlDbType.NVarChar, 10).Value = pedido.cliCodigo;
                cmd1.Parameters.Add("@pedPago", System.Data.SqlDbType.Char, 4).Value = "";

               
            }

            int pedId = 0;
            if (swValida == 0)  //para PC swValida esta en 0 siempre  PP swValida esta en 0 simpre  para PA swValida puede variar   
            {
                reader = cmd1.ExecuteReader();
                while (reader.Read())
                {
                    pedId = Convert.ToInt32(reader["pedId"]);
                }
            }                    
            
            clsPedidoAdicionarClienteResponse respu = new clsPedidoAdicionarClienteResponse();
            if (pedId > 0)
            {
                //Paso 3 Ejecutar el procedimiento almacenado Api_ClienteInterlocutorPago para obtener un numero de cliente
                SqlCommand cmd2 = conn.CreateCommand();
                cmd2.CommandType = System.Data.CommandType.StoredProcedure;
                cmd2.CommandText = "Api_ClienteInterlocutorPago";
                cmd2.Parameters.Add("@cliCodigo", System.Data.SqlDbType.NVarChar, 10).Value = pedido.cliCodigo;
                reader = cmd2.ExecuteReader();

                string cloInterlocutor = "";
                while (reader.Read())
                {
                    cloInterlocutor = reader["cloInterlocutor"].ToString().Trim();
                }

                if (pedido.cliCodigo == cloInterlocutor)
                {
                    //Paso 4 Ejecutar el procedimiento almacenado Api_PedidoInterlocutorAdicionar 
                    SqlCommand cmd3 = conn.CreateCommand();
                    cmd3.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd3.CommandText = "Api_PedidoInterlocutorAdicionar";
                    cmd3.Parameters.Add("@pedId", System.Data.SqlDbType.Int).Value = pedId;
                    cmd3.Parameters.Add("@cliCodigo", System.Data.SqlDbType.NVarChar, 10).Value = pedido.cliCodigo;
                    cmd3.Parameters.Add("@cloInterlocutor", System.Data.SqlDbType.NVarChar, 10).Value = pedido.cliCodigo;
                    cmd3.Parameters.Add("@intCodigo", System.Data.SqlDbType.Char, 2).Value = "";
                    cmd3.Parameters.Add("@pdiItem", System.Data.SqlDbType.Int).Value = 0;
                    reader = cmd3.ExecuteReader();

                }
                else
                {

                    // vector de cadenas
                    string[] vectorInterlocutor = new string[] { "RG", "RE", "AG", "WE" };
                    for (int i = 0; i < vectorInterlocutor.Length ; i++)
                    {
                        SqlCommand cmd3 = conn.CreateCommand();
                        cmd3.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd3.CommandText = "Api_PedidoInterlocutorAdicionar";
                        cmd3.Parameters.Add("@pedId", System.Data.SqlDbType.Int).Value = pedId;
                        cmd3.Parameters.Add("@cliCodigo", System.Data.SqlDbType.NVarChar, 10).Value = pedido.cliCodigo;
                       if(vectorInterlocutor[i]=="RG")
                        {
                            cmd3.Parameters.Add("@cloInterlocutor", System.Data.SqlDbType.NVarChar, 10).Value = cloInterlocutor;
                        }
                       else
                        {
                            cmd3.Parameters.Add("@cloInterlocutor", System.Data.SqlDbType.NVarChar, 10).Value = pedido.cliCodigo;
                        }
                       
                        cmd3.Parameters.Add("@intCodigo", System.Data.SqlDbType.Char, 2).Value = vectorInterlocutor[i];
                        cmd3.Parameters.Add("@pdiItem", System.Data.SqlDbType.Int).Value = 0;
                        reader = cmd3.ExecuteReader();
                    }

                }

                // Paso 5 Ejecutar el procedimiento almacenado pa_PedidoDetalleAdicionar
                

                int h = 10;
                foreach (clsPedidoAdicionarClienteDetalleRequest producto in pedido.detallePedidoAdicionar)
                {
                    SqlCommand cmd4 = conn.CreateCommand();
                    cmd4.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd4.CommandText = "Api_PedidoDetalleAdicionar";
                    cmd4.Parameters.Add("@pedId", System.Data.SqlDbType.Int).Value = pedId;
                    cmd4.Parameters.Add("@pddItem", System.Data.SqlDbType.Int).Value = h;
                    cmd4.Parameters.Add("@pddOrden", System.Data.SqlDbType.Int).Value = h;
                    cmd4.Parameters.Add("@prcCodigo", System.Data.SqlDbType.Char, 4).Value = "OT";
                    cmd4.Parameters.Add("@matCodigo", System.Data.SqlDbType.NVarChar, 18).Value = producto.matCodigo;
                    cmd4.Parameters.Add("@pddCantidad", System.Data.SqlDbType.Int).Value = producto.pddCantidad;
                    cmd4.Parameters.Add("@pddCantidadAtendida", System.Data.SqlDbType.Int).Value = 0;
                    cmd4.Parameters.Add(new SqlParameter("@pddPrecio", System.Data.SqlDbType.Decimal) { Precision = 9, Scale = 2 }).Value = producto.pddPrecio;
                    cmd4.Parameters.Add(new SqlParameter("@pddPrecioVenta", System.Data.SqlDbType.Decimal) { Precision = 9, Scale = 2 }).Value = producto.pddPrecioVenta;
                    cmd4.Parameters.Add("@lotCodigo", System.Data.SqlDbType.NVarChar, 18).Value = "";
                    reader = cmd4.ExecuteReader();

                    h = h + 10;
                }                
                respu.pedId = pedId;
            }
            else
            {
                respu.pedId = -1;
            }
            conn.Close();
            return respu;
        }

    }
}
