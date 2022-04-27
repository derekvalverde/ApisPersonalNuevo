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
    public class ApiCorrespondenciaBusquedaService: IApiCorrespondenciaBusquedaService
    {
        private readonly AppSettings _appSettings;
        private readonly ApicacionDbContextCorrespondencia _context;
        public ApiCorrespondenciaBusquedaService(IOptions<AppSettings> appSettings, ApicacionDbContextCorrespondencia context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsCorrespondenciaBusquedaResponse> obtenerCorrespondenciaBusqueda(string UsuCodigo, string corCodigo, string corRemitente,string areCodigo, DateTime fechaIni, DateTime fechaFin)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.CommandText = "pa_CorrespondenciaBusqueda";
            cmd.Parameters.Add("@UsuCodigo", System.Data.SqlDbType.VarChar, 20).Value = UsuCodigo;
            cmd.Parameters.Add("@corCodigo", System.Data.SqlDbType.VarChar, 15).Value = corCodigo;
            cmd.Parameters.Add("@corRemitente", System.Data.SqlDbType.VarChar, 45).Value = corRemitente;
            cmd.Parameters.Add("@areCodigo", System.Data.SqlDbType.Char, 5).Value = areCodigo;
          
            cmd.Parameters.Add("@fechaIni", System.Data.SqlDbType.DateTime).Value = fechaIni;
            cmd.Parameters.Add("@fechaFin", System.Data.SqlDbType.DateTime).Value = fechaFin;

            var busqueda = new List<clsCorrespondenciaBusquedaResponse>();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                busqueda.Add(MapToValue(reader));
            }
            conn.Close();


            //
            //Si el buscador no tiene elementos
            if (busqueda == null)
            {
                return null;
            }
            //Si existe Material

            return busqueda;

        }
        private clsCorrespondenciaBusquedaResponse MapToValue(SqlDataReader reader)
        {

            return new clsCorrespondenciaBusquedaResponse()
            {
                corCodigo = reader["corCodigo"].ToString().Trim(),
                corNumGuia = reader["corNumGuia"].ToString().Trim(),
                corRemitente = reader["corRemitente"].ToString().Trim(),
                codNombre = reader["codNombre"].ToString().Trim(),                
                areCodigo = reader["areCodigo"].ToString().Trim(),
                corFechaIni = Convert.ToDateTime(reader["corFechaIni"]),
                nroDiv = Convert.ToInt32(reader["nroDiv"]),
                corEstado = reader["corEstado"].ToString().Trim(),
                urgente = reader["urgente"].ToString().Trim(),
               
            };

        }

    }
}
