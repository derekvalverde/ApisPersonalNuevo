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
    public class ApiEstudioListarService: IApiEstudioListarService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContext1 _context;
        public ApiEstudioListarService(IOptions<AppSettings> appSettings, AplicacionDbContext1 context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsEstudioListarResponse> obtenerUsuarioEstudios(int usuId)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.CommandText = "Api_EstudioListar";
            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.NVarChar, 15).Value = usuId;

            var datosEmp = new List<clsEstudioListarResponse>();
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
        private clsEstudioListarResponse MapToValue(SqlDataReader reader)
        {

            return new clsEstudioListarResponse()
            {
                estId = Convert.ToInt32(reader["estId"]),
                estInstitucion = reader["estInstitucion"].ToString().Trim(),
                estFecha = Convert.ToDateTime(reader["estFecha"]),
                estFechaFin = Convert.ToDateTime(reader["estFechaFin"]),
                
                 estNombre = reader["estNombre"].ToString().Trim(),
                esgNombre = reader["esgNombre"].ToString().Trim(),
                
            


            };

        }

    }
}
