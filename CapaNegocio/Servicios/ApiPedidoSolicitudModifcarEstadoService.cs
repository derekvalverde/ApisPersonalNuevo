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
    public class ApiPedidoSolicitudModifcarEstadoService:IApiPedidoSolicitudModifcarEstadoService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiPedidoSolicitudModifcarEstadoService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsPedidoSolicitudModifcarEstadoResponse obtenerPedidoSolicitudModificarEstado(int pedId, string pedEstado, int usuId)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_PedidoSolicitudModifcarEstado";
            cmd.Parameters.Add("@pedId", System.Data.SqlDbType.Int).Value = pedId;
            cmd.Parameters.Add("@pedEstado", System.Data.SqlDbType.VarChar, 2).Value = pedEstado;
            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId;

            var usuario = new clsPedidoSolicitudModifcarEstadoResponse();
            var reader = cmd.ExecuteReader();


            usuario.solicitudModificada = true;

            conn.Close();

            return usuario;

        }
    }
}
