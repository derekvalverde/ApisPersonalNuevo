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
    public class ApiClienteVentaCliCodigoService: IApiClienteVentaCliCodigoService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiClienteVentaCliCodigoService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsClienteVentaCliCodigoResponse obtenerDatosCliente(string cliCodigo, int usuId, string aplicacion)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            if (aplicacion == "PC")
            {
                cmd.CommandText = "Api_ClienteVentaCliCodigo";
                cmd.Parameters.Add("@cliCodigo", System.Data.SqlDbType.NVarChar, 15).Value = cliCodigo;

            }
            if (aplicacion == "PP")
            {
                cmd.CommandText = "Api_ClienteVentaPersonalCliCodigo";
                cmd.Parameters.Add("@usuId", System.Data.SqlDbType.NVarChar, 15).Value = usuId;

            }
            var datosCli = new clsClienteVentaCliCodigoResponse();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                datosCli =MapToValue(reader);
            }
            conn.Close();
            //
            //Si no exixte cliente
            if (datosCli == null)
            {
                return null;
            }
            //Si existe cliente


            return datosCli;

        }
        private clsClienteVentaCliCodigoResponse MapToValue(SqlDataReader reader)
        {

            return new clsClienteVentaCliCodigoResponse()
            {


                cliId = Convert.ToInt32(reader["cliId"]),
                clvOficina = reader["clvOficina"].ToString().Trim(),
                cliCiudad = reader["cliCiudad"].ToString().Trim(),
                cliCodigo = reader["cliCodigo"].ToString().Trim(),
                cloInterlocutor = reader["cloInterlocutor"].ToString().Trim(),
                cliNombreComercial = reader["cliNombreComercial"].ToString().Trim(),
                cliNombreFiscal = reader["cliNombreFiscal"].ToString().Trim(),
                cliRol = reader["cliRol"].ToString().Trim(),
                cliIdentificacionNumero = reader["cliIdentificacionNumero"].ToString().Trim(),
                cliDireccionComercial = reader["cliDireccionComercial"].ToString().Trim(),
                cliTelefono = reader["cliTelefono"].ToString().Trim(),
                cliEmail = reader["cliEmail"].ToString().Trim(),
                cliFechaModificacion = Convert.ToDateTime(reader["cliFechaModificacion"]),
                cadCodigo = reader["cadCodigo"].ToString().Trim(),
                secCodigo = reader["secCodigo"].ToString().Trim(),
                grpCodigo = reader["grpCodigo"].ToString().Trim(),
                clvFormaPago = reader["clvFormaPago"].ToString().Trim(),
                clvZona = reader["clvZona"].ToString().Trim(),
                clvGrupoVendedores = reader["clvGrupoVendedores"].ToString().Trim(),
                clvEstado = reader["clvEstado"].ToString().Trim(),
                prgCodigo = reader["prgCodigo"].ToString().Trim(),
                cheMonto = Convert.ToDecimal(reader["cheMonto"]),

                perComprado= Convert.ToDecimal(reader["perComprado"]),
                perLimite= Convert.ToDecimal(reader["perLimite"]),

            };

        }
    }
}
