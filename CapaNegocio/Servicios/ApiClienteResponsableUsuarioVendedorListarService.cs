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
    public class ApiClienteResponsableUsuarioVendedorListarService : IApiClienteResponsableUsuarioVendedorListarService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiClienteResponsableUsuarioVendedorListarService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsClienteResponsableUsuarioVendedorListarResponse> obtenerClientesDeVendedores(int ageId,string cagCodigo)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_ClienteResponsableUsuarioVendedorListar";
            cmd.Parameters.Add("@ageId", System.Data.SqlDbType.Int).Value = ageId;
            cmd.Parameters.Add("@cagCodigo", System.Data.SqlDbType.Char, 3).Value = cagCodigo;
            var vendedores = new List<clsClienteResponsableUsuarioVendedorListarResponse>();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                vendedores.Add(MapToValue(reader));
            }
            conn.Close();
            //
            //Si el buscador no tiene elementos
            if (vendedores == null)
            {
                return null;
            }
            //Si existe Material


            return vendedores;

        }
        private clsClienteResponsableUsuarioVendedorListarResponse MapToValue(SqlDataReader reader)
        {
            clsClienteResponsableUsuarioVendedorListarResponse objVendedor = new clsClienteResponsableUsuarioVendedorListarResponse()
            {
                usuIdR = Convert.ToInt32(reader["usuIdR"]),
                usuNombre = reader["usuNombre"].ToString().Trim(),
            };

            objVendedor.detalleClientes = obtenerClientesDetalle(objVendedor.usuIdR);
            return objVendedor;

        }
        public List<clsClienteResponsableUbicacionUsuarioResponse> obtenerClientesDetalle(int usuIdR)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_ClienteResponsableUbicacionUsuario";
            cmd.Parameters.Add("@usuIdR", System.Data.SqlDbType.Int).Value = usuIdR;
            var detalleClientes = new List<clsClienteResponsableUbicacionUsuarioResponse>();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                detalleClientes.Add(MapToValueDetalle(reader));
            }

            //
            //Si  no tiene 
            if (detalleClientes == null)
            {
                return null;
            }
            //Si existe Material


            return detalleClientes;

        }

        private clsClienteResponsableUbicacionUsuarioResponse MapToValueDetalle(SqlDataReader reader)
        {
            return new clsClienteResponsableUbicacionUsuarioResponse()
            {

                cliNombreComercial = reader["cliNombreComercial"].ToString().Trim(),
                cliNombreFiscal = reader["cliCodigo"].ToString().Trim(),
                cliCodigo = reader["cliNombreComercial"].ToString().Trim(),
                cluLatitud = Convert.ToDecimal(reader["cluLatitud"]),
                cluLongitud = Convert.ToDecimal(reader["cluLongitud"]),
                cliDireccionComercial = reader["cliDireccionComercial"].ToString().Trim(),

            };

        }
    }
}
