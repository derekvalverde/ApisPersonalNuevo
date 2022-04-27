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
    public class ApiClienteCompletoVentaCobranzaActualizadoService:IApiClienteCompletoVentaCobranzaActualizadoService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiClienteCompletoVentaCobranzaActualizadoService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsClienteCompletoVentaCobranzaActualizadoResponse> obtenerCompletoCobranzaActualizado(int usuId)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_ClienteCompletoVentaCobranzaActualizado";

            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId;


            var chequePendiente = new List<clsClienteCompletoVentaCobranzaActualizadoResponse>();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                chequePendiente.Add(MapToValue(reader));
            }
            conn.Close();
            //
            //Si el cheque no tiene elementos
            if (chequePendiente == null)
            {
                return null;
            }

            return chequePendiente;

        }
        private clsClienteCompletoVentaCobranzaActualizadoResponse MapToValue(SqlDataReader reader)
        {




            return new clsClienteCompletoVentaCobranzaActualizadoResponse()
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



                cluLatitud = float.Parse(reader["cluLatitud"].ToString().Trim()),


                cluLongitud = float.Parse(reader["cluLongitud"].ToString().Trim()),


            };

        }
    }
}
