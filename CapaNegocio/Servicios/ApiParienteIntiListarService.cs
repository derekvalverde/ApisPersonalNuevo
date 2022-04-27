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
    public class ApiParienteIntiListarService:IApiParienteIntiListarService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContext1 _context;
        public ApiParienteIntiListarService(IOptions<AppSettings> appSettings, AplicacionDbContext1 context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsParienteIntiListarResponse> obtenerParienteIntiListar(int usuId)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.CommandText = "Api_ParienteIntiListar";
            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.NVarChar, 15).Value = usuId;

            var datosEmp = new List<clsParienteIntiListarResponse>();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                datosEmp.Add(MapToValue(reader));
            }

            conn.Close();
            //
            //Si no exixte cliente
            if (datosEmp == null)
            {
                return null;
            }
            //Si existe cliente


            return datosEmp;

        }
        private clsParienteIntiListarResponse MapToValue(SqlDataReader reader)
        {

            return new clsParienteIntiListarResponse()
            {
                empId = Convert.ToInt32(reader["empId"]),
                empNombre = reader["empNombre"].ToString().Trim(),        
                areNombre = reader["areNombre"].ToString().Trim(),
                
            };

        }

    }
}
