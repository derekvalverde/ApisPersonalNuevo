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
    public class ApiExperienciaListarService:IApiExperienciaListarService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContext1 _context;
        public ApiExperienciaListarService(IOptions<AppSettings> appSettings, AplicacionDbContext1 context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsExperienciaListarResponse> obtenerUsuarioExperiencia(int usuId)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.CommandText = "Api_ExperienciaListar";
            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.NVarChar, 15).Value = usuId;

            var datosEmp = new List<clsExperienciaListarResponse>();
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
        private clsExperienciaListarResponse MapToValue(SqlDataReader reader)
        {

            return new clsExperienciaListarResponse()
            {

                
               
                expInstitucion = reader["ExpInstitucion"].ToString().Trim(),

                expCargo = reader["ExpCargo"].ToString().Trim(),
                expFecha = Convert.ToDateTime(reader["ExpFecha"]),
                expFechaFin = Convert.ToDateTime(reader["ExpFechaFin"]),
                expMotivoRet = reader["ExpMotivoRet"].ToString().Trim(),
               



            };

        }
    }
}
