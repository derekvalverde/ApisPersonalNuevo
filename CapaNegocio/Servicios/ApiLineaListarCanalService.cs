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
    public class ApiLineaListarCanalService:IApiLineaListarCanalService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiLineaListarCanalService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsLineaListarCanalResponse> obtenerLineaCanal(string cadCodigo)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.CommandText = "Api_LineaListarCanal";
            cmd.Parameters.Add("@cadCodigo", System.Data.SqlDbType.Char,2).Value = cadCodigo;

            var lineas = new List<clsLineaListarCanalResponse>();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                lineas.Add(MapToValue(reader));
            }
            conn.Close();
            //
            //Si el buscador no tiene elementos
            if (lineas == null)
            {
                return null;
            }
            //Si existe Material


            return lineas;

        }
        private clsLineaListarCanalResponse MapToValue(SqlDataReader reader)
        {

            return new clsLineaListarCanalResponse()
            {

                linId = Convert.ToInt32(reader["linId"]),
                linCodigo = reader["linCodigo"].ToString().Trim(),
                linDescripcion = reader["linDescripcion"].ToString().Trim()

            };

        }
    }
}
