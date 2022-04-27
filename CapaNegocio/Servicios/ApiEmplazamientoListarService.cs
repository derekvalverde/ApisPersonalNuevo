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
    public class ApiEmplazamientoListarService : IApiEmplazamientoListarService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContextInventario _context;
        public ApiEmplazamientoListarService(IOptions<AppSettings> appSettings, ApplicationDbContextInventario context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsEmplazamientoListarResponse> emplazamientoListar(int usuId)
        {
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_EmplazamientoListar";

            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId;

            List<clsEmplazamientoListarResponse> emplazamientos = new List<clsEmplazamientoListarResponse>();
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                emplazamientos.Add(MapToValue(reader));
            }

            conn.Close();

            return emplazamientos;
        }

        private clsEmplazamientoListarResponse MapToValue(SqlDataReader reader)
        {

            return new clsEmplazamientoListarResponse()
            {
                empId = Convert.ToInt32(reader["empId"]),
                empCodigo = reader["empCodigo"].ToString().Trim(),
                empDenominacion = reader["empDenominacion"].ToString().Trim(),
            };

        }
    }
}