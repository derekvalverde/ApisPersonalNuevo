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
// hoja verde 
namespace CapaNegocio.Servicios
{
    public class ApiCursoListarService:IApiCursoListarService
    {

        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContext1 _context;
        public ApiCursoListarService(IOptions<AppSettings> appSettings, AplicacionDbContext1 context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsCursoListarResponse> obtenerUsuarioCurso(int usuId)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.CommandText = "Api_CursoListar";
            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId;

            var datosEmp = new List<clsCursoListarResponse>();
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
        private clsCursoListarResponse MapToValue(SqlDataReader reader)
        {

            return new clsCursoListarResponse()
            {
                curId = Convert.ToInt32(reader["curId"]),
                curNombre = reader["curNombre"].ToString().Trim(),
                curInstitucion = reader["curInstitucion"].ToString().Trim(),
                curExplicacion = reader["curExplicacion"].ToString().Trim(),
                curFecha = Convert.ToDateTime(reader["curFecha"]),
            };
        }
    }
}
